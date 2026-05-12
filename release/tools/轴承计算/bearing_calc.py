"""
轴承计算引擎 - Bearing Calculation Engine
依据 ISO 281 / GB/T 6391 标准实现轴承寿命计算与选型
"""

import bearing_db


# ============================================================
# 可靠性系数 a1 (ISO 281)
# ============================================================
RELIABILITY_FACTORS = {
    0.90: 1.00,
    0.95: 0.62,
    0.99: 0.21,
}

# 轴承类型对应的寿命指数 p
LIFE_EXPONENT = {
    "深沟球轴承": 3.0,
    "角接触球轴承": 3.0,
    "圆锥滚子轴承": 10.0 / 3.0,
}

# 轴承类型对应的当量动载荷 X 默认值
DEFAULT_X = {
    "深沟球轴承": 0.56,
    "角接触球轴承": 0.44,
    "圆锥滚子轴承": 0.40,
}


def calc_equivalent_load(Fr, Fa, X, Y):
    """计算当量动载荷 P = X * Fr + Y * Fa
    Fr, Fa 单位: kN
    返回 P 单位: kN
    """
    return X * Fr + Y * Fa


def calc_basic_life(C, P, p):
    """计算基本额定寿命 L10 (百万转)
    L10 = (C / P)^p
    C, P 单位: kN
    返回: 百万转
    """
    if P <= 0:
        return float('inf')
    return (C / P) ** p


def calc_life_hours(L10_million_rev, n):
    """将基本额定寿命转换为小时 Lh
    Lh = L10 * 10^6 / (60 * n)
    L10_million_rev: 百万转
    n: RPM
    返回: 小时
    """
    if n <= 0:
        return float('inf')
    return L10_million_rev * 1e6 / (60.0 * n)


def calc_modified_life(L10, reliability, a_iso=1.0):
    """计算修正额定寿命 Lna
    Lna = a1 * a_ISO * L10
    reliability: 0.90 / 0.95 / 0.99
    a_ISO: ISO 润滑条件系数，默认 1.0
    返回: 百万转
    """
    a1 = RELIABILITY_FACTORS.get(reliability, 1.0)
    return a1 * a_iso * L10


def calc_static_check(Fr, Fa, C0, S0=1.0):
    """静载荷校核
    P0 = 0.6 * Fr + 0.5 * Fa
    判断 P0 <= C0 / S0
    返回: (P0, C0/S0, 是否合格)
    """
    P0 = 0.6 * Fr + 0.5 * Fa
    allowable = C0 / S0
    return P0, allowable, P0 <= allowable


def get_load_factors(bearing_type, Fa_Fr_ratio, e_value, Y_value, X_value=None):
    """根据 Fa/Fr 与 e 的关系确定 X, Y 系数
    如果 Fa/Fr <= e: X=1, Y=0 (纯径向载荷为主)
    如果 Fa/Fr > e: X=0.56 (或轴承默认), Y=给定值
    """
    if X_value is None:
        X_value = DEFAULT_X.get(bearing_type, 0.56)

    if Fa_Fr_ratio <= e_value:
        return 1.0, 0.0
    else:
        return X_value, Y_value


def calculate_bearing_life(bearing_data, Fr, Fa, n, reliability=0.90, ft=1.0):
    """完整轴承寿命计算

    参数:
        bearing_data: dict, 包含轴承数据库字段
        Fr: 径向载荷 kN
        Fa: 轴向载荷 kN
        n: 转速 RPM
        reliability: 可靠度 0.90/0.95/0.99
        ft: 温度系数

    返回: dict 包含所有计算结果
    """
    result = {}

    bearing_type = bearing_data["type"]
    C = bearing_data["dynamic_load_C"]
    C0 = bearing_data["static_load_C0"]
    e = bearing_data["e_value"]
    Y = bearing_data["Y_value"]
    X_default = bearing_data.get("X_value", 0.56)
    p = LIFE_EXPONENT.get(bearing_type, 3.0)

    # 确定 X, Y
    if Fr <= 0:
        Fr = 0.001  # 避免除零

    Fa_Fr = Fa / Fr
    X, Y_coeff = get_load_factors(bearing_type, Fa_Fr, e, Y, X_default)

    result["Fa_Fr_ratio"] = Fa_Fr
    result["e_value"] = e
    result["X"] = X
    result["Y"] = Y_coeff
    result["load_condition"] = "Fa/Fr <= e (主要承受径向载荷)" if Fa_Fr <= e else "Fa/Fr > e (轴向载荷不可忽略)"

    # 当量动载荷
    P = calc_equivalent_load(Fr, Fa, X, Y_coeff)
    # 温度修正
    P_corrected = P / ft if ft > 0 else P
    result["P"] = P
    result["P_corrected"] = P_corrected

    # 基本额定寿命
    L10 = calc_basic_life(C, P_corrected, p)
    result["L10_million"] = L10

    # 修正额定寿命
    Lna = calc_modified_life(L10, reliability)
    result["Lna_million"] = Lna

    # 寿命换算成小时
    Lh = calc_life_hours(L10, n)
    Lh_modified = calc_life_hours(Lna, n)
    result["Lh"] = Lh
    result["Lh_modified"] = Lh_modified

    # 寿命换算成年 (按每年 8000 小时)
    result["years"] = Lh / 8000.0 if Lh != float('inf') else float('inf')

    # 静载荷校核
    P0, allowable, passed = calc_static_check(Fr, Fa, C0)
    result["P0"] = P0
    result["C0_allowable"] = allowable
    result["static_check_passed"] = passed

    # 轴承信息
    result["bearing_code"] = bearing_data["bearing_code"]
    result["bearing_type"] = bearing_type
    result["C"] = C
    result["C0"] = C0
    result["inner_dia"] = bearing_data["inner_dia"]
    result["outer_dia"] = bearing_data["outer_dia"]
    result["width"] = bearing_data["width"]
    result["limit_speed_grease"] = bearing_data["limit_speed_grease"]
    result["limit_speed_oil"] = bearing_data["limit_speed_oil"]
    result["life_exponent"] = p
    result["reliability"] = reliability
    result["a1"] = RELIABILITY_FACTORS.get(reliability, 1.0)
    result["ft"] = ft

    return result


def auto_select_bearing(bearing_type, Fr, Fa, n, Lh_required, reliability=0.90, ft=1.0):
    """自动选型

    从数据库中查找满足寿命要求的轴承，按余量排序，返回前 5 个

    参数:
        bearing_type: 轴承类型
        Fr, Fa: 载荷 kN
        n: 转速 RPM
        Lh_required: 要求寿命 小时
        reliability: 可靠度
        ft: 温度系数

    返回: list of dict, 每个包含轴承数据和计算结果，按寿命余量排序
    """
    bearings = bearing_db.get_bearings_by_type(bearing_type)
    candidates = []

    for b in bearings:
        res = calculate_bearing_life(b, Fr, Fa, n, reliability, ft)
        res["Lh_required"] = Lh_required
        res["life_ratio"] = res["Lh"] / Lh_required if Lh_required > 0 else float('inf')
        res["meets_requirement"] = res["Lh"] >= Lh_required and res["static_check_passed"]
        candidates.append(res)

    # 满足要求的排前面，再按寿命余量排序（不要过大）
    candidates.sort(key=lambda x: (-int(x["meets_requirement"]), abs(x["life_ratio"] - 1.0)))

    return candidates[:5]


def format_result(result):
    """格式化计算结果为可读文本"""
    lines = []
    lines.append("=" * 56)
    lines.append(f"  轴承计算结果  |  型号: {result['bearing_code']}")
    lines.append("=" * 56)

    lines.append("")
    lines.append("【基本信息】")
    lines.append(f"  轴承类型    : {result['bearing_type']}")
    lines.append(f"  内径 d      : {result['inner_dia']} mm")
    lines.append(f"  外径 D      : {result['outer_dia']} mm")
    lines.append(f"  宽度 B      : {result['width']} mm")
    lines.append(f"  基本额定动载荷 C : {result['C']:.1f} kN")
    lines.append(f"  基本额定静载荷 C0: {result['C0']:.1f} kN")

    lines.append("")
    lines.append("【载荷系数】")
    lines.append(f"  Fa/Fr       : {result['Fa_Fr_ratio']:.4f}")
    lines.append(f"  e           : {result['e_value']:.2f}")
    lines.append(f"  X           : {result['X']:.2f}")
    lines.append(f"  Y           : {result['Y']:.2f}")
    lines.append(f"  判定        : {result['load_condition']}")

    lines.append("")
    lines.append("【当量动载荷】")
    lines.append(f"  P = X·Fr + Y·Fa = {result['P']:.4f} kN")
    if result['ft'] != 1.0:
        lines.append(f"  P (温度修正 ft={result['ft']:.2f}) = {result['P_corrected']:.4f} kN")

    lines.append("")
    lines.append("【寿命计算】")
    lines.append(f"  寿命指数 p  : {result['life_exponent']:.4f}")
    lines.append(f"  基本额定寿命 L10  : {result['L10_million']:.2f} 百万转")
    lines.append(f"  修正额定寿命 Lna  : {result['Lna_million']:.2f} 百万转  (a1={result['a1']:.2f}, 可靠度={result['reliability']:.0%})")
    lines.append(f"  L10 折算寿命 Lh   : {result['Lh']:.0f} 小时  ({result['years']:.1f} 年 @8000h/年)")
    lines.append(f"  Lna 折算寿命 Lh   : {result['Lh_modified']:.0f} 小时")

    lines.append("")
    lines.append("【静载荷校核】")
    lines.append(f"  P0 = 0.6·Fr + 0.5·Fa = {result['P0']:.4f} kN")
    lines.append(f"  C0/S0 允许值       = {result['C0_allowable']:.4f} kN")
    status = "合格" if result['static_check_passed'] else "不合格"
    lines.append(f"  校核结果           : {status}")

    lines.append("")
    lines.append("【转速限制】")
    lines.append(f"  脂润滑极限转速 : {result['limit_speed_grease']} RPM")
    lines.append(f"  油润滑极限转速 : {result['limit_speed_oil']} RPM")

    lines.append("=" * 56)

    return "\n".join(lines)


def format_selection_table(candidates):
    """格式化自动选型结果为表格"""
    lines = []
    lines.append("=" * 72)
    lines.append("  轴承自动选型结果")
    lines.append("=" * 72)
    lines.append("")

    header = f"{'型号':<10} {'C(kN)':>7} {'C0(kN)':>8} {'P(kN)':>8} {'Lh(小时)':>12} {'寿命比':>8} {'静校核':>6} {'推荐':>4}"
    lines.append(header)
    lines.append("-" * 72)

    for c in candidates:
        check = "OK" if c["static_check_passed"] else "NG"
        mark = " *" if c["meets_requirement"] else ""
        lines.append(
            f"{c['bearing_code']:<10} "
            f"{c['C']:>7.1f} "
            f"{c['C0']:>8.1f} "
            f"{c['P']:>8.3f} "
            f"{c['Lh']:>12,.0f} "
            f"{c['life_ratio']:>7.1f}x "
            f"{check:>6}"
            f"{mark:>4}"
        )

    lines.append("-" * 72)
    lines.append("  * 标记表示满足寿命和静载荷要求")
    lines.append("")

    # 输出详细信息
    for i, c in enumerate(candidates):
        lines.append(f"--- 候选 {i+1}: {c['bearing_code']} ---")
        lines.append(format_result(c))
        lines.append("")

    return "\n".join(lines)
