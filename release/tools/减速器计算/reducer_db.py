# -*- coding: utf-8 -*-
"""
减速器数据库访问模块
"""
import sqlite3
import os

DB_PATH = os.path.join(os.path.dirname(os.path.abspath(__file__)), "reducer.db")


def get_connection():
    """获取数据库连接，若不存在则先初始化"""
    if not os.path.exists(DB_PATH):
        from init_db import init_database
        init_database()
    conn = sqlite3.connect(DB_PATH)
    conn.row_factory = sqlite3.Row
    return conn


def get_all_modules(series=None):
    """获取标准模数列表
    series: 1=第一系列, 2=第二系列, None=全部
    """
    conn = get_connection()
    if series:
        rows = conn.execute(
            "SELECT * FROM std_module WHERE series=? ORDER BY module", (series,)
        ).fetchall()
    else:
        rows = conn.execute(
            "SELECT * FROM std_module ORDER BY series, module"
        ).fetchall()
    conn.close()
    return [dict(r) for r in rows]


def get_first_series_modules():
    """获取第一系列标准模数"""
    return [r["module"] for r in get_all_modules(series=1)]


def get_all_materials():
    """获取所有齿轮材料"""
    conn = get_connection()
    rows = conn.execute(
        "SELECT * FROM gear_material ORDER BY sigma_hlim DESC"
    ).fetchall()
    conn.close()
    return [dict(r) for r in rows]


def get_material_names():
    """获取材料名称列表"""
    conn = get_connection()
    rows = conn.execute(
        "SELECT name FROM gear_material ORDER BY sigma_hlim DESC"
    ).fetchall()
    conn.close()
    return [r["name"] for r in rows]


def get_material_by_name(name):
    """根据名称获取材料"""
    conn = get_connection()
    row = conn.execute(
        "SELECT * FROM gear_material WHERE name=?", (name,)
    ).fetchone()
    conn.close()
    return dict(row) if row else None


def get_std_ratios():
    """获取标准传动比列表"""
    conn = get_connection()
    rows = conn.execute("SELECT ratio FROM std_ratio ORDER BY ratio").fetchall()
    conn.close()
    return [r["ratio"] for r in rows]


def _query_reducer(table, ratio=None, max_speed=None):
    """通用减速器查询"""
    conn = get_connection()
    sql = f"SELECT * FROM {table}"
    params = []
    conditions = []
    if ratio:
        conditions.append("ratio=?")
        params.append(ratio)
    if max_speed:
        conditions.append("max_input_speed>=?")
        params.append(max_speed)
    if conditions:
        sql += " WHERE " + " AND ".join(conditions)
    sql += " ORDER BY rated_power ASC"
    rows = conn.execute(sql, params).fetchall()
    conn.close()
    return [dict(r) for r in rows]


def query_reducer_zdy(ratio=None, max_speed=None):
    """查询 ZDY 单级圆柱齿轮减速器"""
    return _query_reducer("reducer_zdy", ratio, max_speed)


def query_reducer_zly(ratio=None, max_speed=None):
    """查询 ZLY 两级圆柱齿轮减速器"""
    return _query_reducer("reducer_zly", ratio, max_speed)


def query_reducer_zsy(ratio=None, max_speed=None):
    """查询 ZSY 三级圆柱齿轮减速器"""
    return _query_reducer("reducer_zsy", ratio, max_speed)


def query_reducer_ngw(ratio=None, max_speed=None):
    """查询 NGW 行星齿轮减速器"""
    return _query_reducer("reducer_ngw", ratio, max_speed)


def query_reducer_cwu(ratio=None, max_speed=None):
    """查询 CWU 蜗轮蜗杆减速器"""
    return _query_reducer("reducer_cwu", ratio, max_speed)


def select_reducer(power_kw, input_speed, ratio, reducer_type="auto"):
    """减速器选型
    power_kw: 输入功率 (kW)
    input_speed: 输入转速 (RPM)
    ratio: 传动比
    reducer_type: "auto" / "zdy" / "zly" / "zsy" / "ngw" / "cwu"
    返回: [(type_name, model, ratio, rated_power, rated_torque, margin, ...), ...]
    """
    # 自动选型逻辑
    if reducer_type == "auto":
        if ratio <= 6.3:
            types = [("zdy", "单级圆柱齿轮")]
        elif ratio <= 25:
            types = [("zly", "两级圆柱齿轮"), ("ngw", "行星齿轮")]
        elif ratio <= 100:
            types = [("zsy", "三级圆柱齿轮"), ("ngw", "行星齿轮"), ("cwu", "蜗轮蜗杆")]
        else:
            types = [("cwu", "蜗轮蜗杆")]
    else:
        type_map = {
            "zdy": ("zdy", "单级圆柱齿轮"),
            "zly": ("zly", "两级圆柱齿轮"),
            "zsy": ("zsy", "三级圆柱齿轮"),
            "ngw": ("ngw", "行星齿轮"),
            "cwu": ("cwu", "蜗轮蜗杆"),
        }
        types = [type_map.get(reducer_type, (reducer_type, reducer_type))]

    query_map = {
        "zdy": query_reducer_zdy,
        "zly": query_reducer_zly,
        "zsy": query_reducer_zsy,
        "ngw": query_reducer_ngw,
        "cwu": query_reducer_cwu,
    }

    results = []
    # 找最接近的标准比
    std_ratios = get_std_ratios()
    closest_ratio = min(std_ratios, key=lambda r: abs(r - ratio))

    for type_code, type_name in types:
        query_fn = query_map.get(type_code)
        if not query_fn:
            continue
        # 先查精确比
        reducers = query_fn(ratio=closest_ratio, max_speed=input_speed)
        if not reducers:
            # 查附近比值
            for r in std_ratios:
                if abs(r - ratio) / ratio < 0.1:  # 10% 偏差
                    reducers = query_fn(ratio=r, max_speed=input_speed)
                    if reducers:
                        break
        for red in reducers:
            if red["rated_power"] >= power_kw:
                margin = round((red["rated_power"] - power_kw) / power_kw * 100, 1)
                results.append({
                    "type_name": type_name,
                    "model": red["model"],
                    "ratio": red["ratio"],
                    "rated_power": red["rated_power"],
                    "rated_torque": red["rated_torque"],
                    "max_input_speed": red["max_input_speed"],
                    "efficiency": red["efficiency"],
                    "mass_kg": red["mass_kg"],
                    "dimensions": f'{red["length_mm"]}x{red["width_mm"]}x{red["height_mm"]}',
                    "power_margin": margin,
                })

    # 按功率裕度排序（裕度小的优先）
    results.sort(key=lambda x: x["power_margin"])
    return results
