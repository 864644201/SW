# -*- coding: utf-8 -*-
"""
液压缸标准数据库查询模块
hydraulic_db.py - 查询液压缸标准件数据库
"""
import os
import sqlite3
from init_db import DB_PATH, init_database


def _get_conn():
    """获取数据库连接, 如不存在则自动初始化"""
    if not os.path.exists(DB_PATH):
        init_database()
    conn = sqlite3.connect(DB_PATH)
    conn.row_factory = sqlite3.Row
    return conn


# ═══════════════════════════════════════════════════════════════
#  标准缸筒内径 GB/T 2348
# ═══════════════════════════════════════════════════════════════
def get_std_bores():
    """获取全部标准缸筒内径"""
    conn = _get_conn()
    rows = conn.execute("SELECT dia FROM std_bore ORDER BY dia").fetchall()
    conn.close()
    return [r["dia"] for r in rows]


def get_nearest_bore(D_calc):
    """向上取最接近的标准缸筒内径"""
    bores = get_std_bores()
    for b in bores:
        if b >= D_calc:
            return b
    return bores[-1]


# ═══════════════════════════════════════════════════════════════
#  标准活塞杆直径 GB/T 2348
# ═══════════════════════════════════════════════════════════════
def get_std_rods():
    """获取全部标准活塞杆直径"""
    conn = _get_conn()
    rows = conn.execute("SELECT dia FROM std_rod ORDER BY dia").fetchall()
    conn.close()
    return [r["dia"] for r in rows]


def get_nearest_rod(d_calc):
    """向上取最接近的标准活塞杆直径"""
    rods = get_std_rods()
    for r in rods:
        if r >= d_calc:
            return r
    return rods[-1]


# ═══════════════════════════════════════════════════════════════
#  HSG系列查询
# ═══════════════════════════════════════════════════════════════
def query_hsg(bore_d=None, pressure_mpa=None, force_kn=None):
    """查询HSG系列液压缸, 支持按缸径/压力/推力筛选"""
    conn = _get_conn()
    sql = "SELECT * FROM hsg_series WHERE 1=1"
    params = []
    if bore_d is not None:
        sql += " AND bore_dia_D >= ?"
        params.append(bore_d)
    if pressure_mpa is not None:
        sql += " AND max_pressure >= ?"
        params.append(pressure_mpa)
    if force_kn is not None:
        sql += " AND push_force_16MPa >= ?"
        params.append(force_kn)
    sql += " ORDER BY bore_dia_D"
    rows = conn.execute(sql, params).fetchall()
    conn.close()
    return [dict(r) for r in rows]


def get_all_hsg():
    """获取全部HSG系列数据"""
    conn = _get_conn()
    rows = conn.execute("SELECT * FROM hsg_series ORDER BY bore_dia_D").fetchall()
    conn.close()
    return [dict(r) for r in rows]


# ═══════════════════════════════════════════════════════════════
#  密封件查询
# ═══════════════════════════════════════════════════════════════
def query_seals(bore_d, rod_d, pressure_mpa=None):
    """根据缸径/杆径查询适用密封件"""
    conn = _get_conn()
    sql = """SELECT * FROM seal_spec
             WHERE bore_d_min <= ? AND bore_d_max >= ?
               AND rod_d_min <= ?  AND rod_d_max >= ?"""
    params = [bore_d, bore_d, rod_d, rod_d]
    if pressure_mpa is not None:
        sql += " AND pressure_max >= ?"
        params.append(pressure_mpa)
    rows = conn.execute(sql, params).fetchall()
    conn.close()
    return [dict(r) for r in rows]
