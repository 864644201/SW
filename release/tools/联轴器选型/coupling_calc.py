# -*- coding: utf-8 -*-
"""
联轴器选型计算引擎
"""

from coupling_db import SERIES_MAP, get_coupling_data, get_standard

# ============================================================
# 系数查找表
# ============================================================

# 载荷性质 → KA (工况系数)
LOAD_FACTOR = {
    "平稳载荷":   (1.0, 1.5),
    "轻度冲击":   (1.5, 2.0),
    "中等冲击":   (2.0, 2.5),
    "重度冲击":   (2.5, 3.0),
}

# 工作温度 → K (温度系数)
TEMP_FACTOR = {
    "≤100°C": 1.0,
    "≤150°C": 1.1,
    "≤200°C": 1.2,
    "≤250°C": 1.3,
}

# 启动工况 → Kw (启动系数)
START_FACTOR = {
    "启动不频繁": (1.0, 1.0),
    "频繁启动":   (1.3, 1.5),
}


def calc_torque_from_power(power_kw, speed_rpm):
    """
    由功率和转速计算工作扭矩
    T = 9550 * P / n  (N·m)
    """
    if speed_rpm <= 0:
        raise ValueError("转速必须大于 0")
    return 9550.0 * power_kw / speed_rpm


def calc_ka(load_type):
    """返回工况系数 (取范围中值)"""
    low, high = LOAD_FACTOR[load_type]
    return (low + high) / 2.0


def calc_k(temp_type):
    """返回温度系数"""
    return TEMP_FACTOR[temp_type]


def calc_kw(start_type):
    """返回启动系数 (取范围中值)"""
    low, high = START_FACTOR[start_type]
    return (low + high) / 2.0


def calc_calculation_torque(T, KA, K, Kw):
    """
    计算扭矩 Tca = KA * K * Kw * T
    """
    return KA * K * Kw * T


def select_couplings(Tca, n_working, d1, d2, series_name,
                     angular_need=0, radial_need=0, axial_need=0):
    """
    从指定系列中筛选满足条件的联轴器

    条件:
      1. Tca <= Tn (公称扭矩)
      2. n_working <= n_max (许用转速)
      3. d1 在 [d_min, d_max] 范围内
      4. d2 在 [d_min, d_max] 范围内
      5. 补偿量满足要求

    返回: [(型号, 数据, 校核结果), ...] 按 Tn 升序
    """
    data = get_coupling_data(series_name)
    if not data:
        return []

    results = []
    for row in data:
        model, Tn, n_max, d_min, d_max = row[0], row[1], row[2], row[3], row[4]
        ang_comp, rad_comp, ax_comp = row[9], row[10], row[11]

        checks = {}

        # 扭矩校核
        torque_ok = Tca <= Tn
        checks["扭矩校核"] = (torque_ok, f"Tca={Tca:.1f} vs Tn={Tn}")

        # 转速校核
        speed_ok = n_working <= n_max
        checks["转速校核"] = (speed_ok, f"n={n_working} vs n_max={n_max}")

        # 主动端轴径校核
        bore1_ok = d_min <= d1 <= d_max
        checks["主动端轴径"] = (bore1_ok, f"d1={d1} vs 范围[{d_min}, {d_max}]")

        # 从动端轴径校核
        bore2_ok = d_min <= d2 <= d_max
        checks["从动端轴径"] = (bore2_ok, f"d2={d2} vs 范围[{d_min}, {d_max}]")

        # 补偿量校核 (如用户未指定需求则跳过)
        if angular_need > 0:
            ang_ok = ang_comp >= angular_need
            checks["角向补偿"] = (ang_ok, f"许用={ang_comp}° vs 需求={angular_need}°")
        if radial_need > 0:
            rad_ok = rad_comp >= radial_need
            checks["径向补偿"] = (rad_ok, f"许用={rad_comp}mm vs 需求={radial_need}mm")
        if axial_need > 0:
            ax_ok = ax_comp >= axial_need
            checks["轴向补偿"] = (ax_ok, f"许用={ax_comp}mm vs 需求={axial_need}mm")

        all_ok = all(v[0] for v in checks.values())
        results.append((row, checks, all_ok))

    # 排序: 合格的优先, 然后按 Tn 升序
    results.sort(key=lambda x: (not x[2], x[0][1]))
    return results


def full_selection(input_mode, power_kw, speed_rpm, torque_nm,
                   load_type, temp_type, start_type,
                   d1, d2, series_name,
                   angular_need=0, radial_need=0, axial_need=0):
    """
    完整选型流程

    返回: dict {
        "T": 工作扭矩,
        "KA": 工况系数,
        "K": 温度系数,
        "Kw": 启动系数,
        "Tca": 计算扭矩,
        "results": [(行数据, 校核字典, 是否全部通过), ...],
        "series_name": 系列名,
        "standard": 标准号,
    }
    """
    # 计算工作扭矩
    if input_mode == "power":
        T = calc_torque_from_power(power_kw, speed_rpm)
    else:
        T = torque_nm

    KA = calc_ka(load_type)
    K = calc_k(temp_type)
    Kw = calc_kw(start_type)

    Tca = calc_calculation_torque(T, KA, K, Kw)

    # 使用默认转速 (若按扭矩输入)
    n = speed_rpm if speed_rpm > 0 else 999999

    results = select_couplings(
        Tca, n, d1, d2, series_name,
        angular_need, radial_need, axial_need,
    )

    return {
        "T": T,
        "KA": KA,
        "K": K,
        "Kw": Kw,
        "Tca": Tca,
        "results": results,
        "series_name": series_name,
        "standard": get_standard(series_name),
    }
