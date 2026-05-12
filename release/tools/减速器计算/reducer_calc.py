# -*- coding: utf-8 -*-
"""
减速器计算引擎模块
包含传动比、扭矩、功率、齿轮参数、强度校核等计算
"""
import math


# ═══════════════════════════════════════════════════════════════
# 1. 传动比计算
# ═══════════════════════════════════════════════════════════════

def calc_ratio(n_in, n_out):
    """计算传动比 i = n_in / n_out"""
    if n_out == 0:
        raise ValueError("输出转速不能为零")
    return n_in / n_out


def calc_output_speed(n_in, ratio):
    """计算输出转速 n2 = n1 / i"""
    if ratio == 0:
        raise ValueError("传动比不能为零")
    return n_in / ratio


def calc_total_ratio(ratios):
    """多级传动总传动比 i_total = i1 * i2 * ... * in"""
    result = 1.0
    for r in ratios:
        result *= r
    return result


def calc_gear_ratio(z2, z1):
    """齿数比 u = z2 / z1"""
    if z1 == 0:
        raise ValueError("小齿轮齿数不能为零")
    return z2 / z1


# ═══════════════════════════════════════════════════════════════
# 2. 扭矩与功率计算
# ═══════════════════════════════════════════════════════════════

def calc_output_torque(t_in, ratio, efficiency):
    """计算输出扭矩 T_out = T_in * i * η"""
    return t_in * ratio * efficiency


def calc_power_from_torque(torque, speed):
    """由扭矩和转速计算功率 P = T * n / 9550 (kW)"""
    return torque * speed / 9550.0


def calc_torque_from_power(power, speed):
    """由功率和转速计算扭矩 T = 9550 * P / n (N·m)"""
    if speed == 0:
        raise ValueError("转速不能为零")
    return 9550.0 * power / speed


def calc_input_torque(t_out, ratio, efficiency):
    """由输出扭矩反推输入扭矩"""
    if ratio == 0 or efficiency == 0:
        raise ValueError("传动比和效率不能为零")
    return t_out / (ratio * efficiency)


# 传动类型效率默认值
EFFICIENCY_TABLE = {
    "单级圆柱齿轮": 0.97,
    "两级圆柱齿轮": 0.95,
    "三级圆柱齿轮": 0.93,
    "行星齿轮":     0.97,
    "蜗轮蜗杆":     0.70,
    "单级圆柱齿轮_范围": (0.96, 0.99),
    "两级圆柱齿轮_范围": (0.94, 0.96),
    "三级圆柱齿轮_范围": (0.92, 0.94),
    "行星齿轮_范围":     (0.95, 0.98),
    "蜗轮蜗杆_范围":     (0.40, 0.85),
}

# 载荷系数对应
LOAD_FACTOR_TABLE = {
    "均匀":       1.0,
    "中等冲击":   1.25,
    "较大冲击":   1.5,
}


def get_efficiency(trans_type):
    """获取传动类型的默认效率"""
    return EFFICIENCY_TABLE.get(trans_type, 0.97)


def get_load_factor(load_type):
    """获取载荷系数"""
    return LOAD_FACTOR_TABLE.get(load_type, 1.0)


def calc_transmission(power_in, speed_in, ratio, trans_type, load_type="均匀"):
    """综合传动计算
    返回: dict 包含 n2, T1, T2, eta, P_out 等
    """
    eta = get_efficiency(trans_type)
    load_factor = get_load_factor(load_type)

    n_out = calc_output_speed(speed_in, ratio)
    t_in = calc_torque_from_power(power_in, speed_in)
    t_out = calc_output_torque(t_in, ratio, eta)
    p_out = power_in * eta

    return {
        "n_in": speed_in,
        "n_out": round(n_out, 2),
        "ratio": round(ratio, 4),
        "trans_type": trans_type,
        "load_type": load_type,
        "load_factor": load_factor,
        "efficiency": eta,
        "t_in": round(t_in, 2),
        "t_out": round(t_out, 2),
        "p_in": power_in,
        "p_out": round(p_out, 3),
        "p_loss": round(power_in - p_out, 3),
    }


# ═══════════════════════════════════════════════════════════════
# 3. 齿轮参数计算
# ═══════════════════════════════════════════════════════════════

def calc_gear_params(z1, z2, module, pressure_angle=20.0, phi_d=1.0):
    """计算齿轮几何参数
    z1: 小齿轮齿数
    z2: 大齿轮齿数
    module: 模数 m (mm)
    pressure_angle: 压力角 (度)
    phi_d: 齿宽系数
    """
    m = module
    alpha = math.radians(pressure_angle)

    # 分度圆直径
    d1 = m * z1
    d2 = m * z2

    # 中心距
    a = m * (z1 + z2) / 2.0

    # 齿顶高 / 齿根高 / 齿全高
    ha = m
    hf = 1.25 * m
    h = ha + hf  # 2.25m

    # 齿顶圆直径
    da1 = m * (z1 + 2)
    da2 = m * (z2 + 2)

    # 齿根圆直径
    df1 = m * (z1 - 2.5)
    df2 = m * (z2 - 2.5)

    # 齿距
    p = math.pi * m

    # 基圆直径
    db1 = d1 * math.cos(alpha)
    db2 = d2 * math.cos(alpha)

    # 齿宽
    b = phi_d * d1
    b = round(b, 1)

    # 重合度 (近似)
    epsilon_alpha = (1.88 - 3.2 * (1.0 / z1 + 1.0 / z2))

    return {
        "z1": z1,
        "z2": z2,
        "module": m,
        "pressure_angle": pressure_angle,
        "d1": round(d1, 2),
        "d2": round(d2, 2),
        "a": round(a, 2),
        "ha": round(ha, 2),
        "hf": round(hf, 2),
        "h": round(h, 2),
        "da1": round(da1, 2),
        "da2": round(da2, 2),
        "df1": round(df1, 2),
        "df2": round(df2, 2),
        "p": round(p, 3),
        "db1": round(db1, 2),
        "db2": round(db2, 2),
        "b": b,
        "phi_d": phi_d,
        "u": round(z2 / z1, 4),
        "epsilon_alpha": round(epsilon_alpha, 3),
    }


# ═══════════════════════════════════════════════════════════════
# 4. 强度校核
# ═══════════════════════════════════════════════════════════════

def calc_contact_strength(t1, m, z1, b, K=1.4, Z_H=2.5, Z_E=189.8):
    """齿面接触强度 (Hertz公式简化)
    σ_H = Z_H * Z_E * sqrt(2*K*T1 / (b*d1^2)) * sqrt((u+1)/u)
    返回 σ_H (MPa)
    """
    d1 = m * z1
    u = z1  # u = z2/z1, 但这里用传入参数，需外部提供z2
    if d1 == 0 or b == 0:
        raise ValueError("分度圆直径和齿宽不能为零")
    # 实际使用时由外部调用 calc_contact_strength_detail
    sigma = Z_H * Z_E * math.sqrt(2 * K * t1 / (b * d1 ** 2))
    return round(sigma, 2)


def calc_contact_strength_detail(t1, m, z1, z2, b, K=1.4, Z_H=2.5, Z_E=189.8):
    """齿面接触强度（含齿数比）
    σ_H = Z_H * Z_E * sqrt(2*K*T1 / (b*d1^2)) * sqrt((u+1)/u)
    """
    d1 = m * z1
    u = z2 / z1
    if d1 == 0 or b == 0:
        raise ValueError("分度圆直径和齿宽不能为零")
    sigma = Z_H * Z_E * math.sqrt(2 * K * t1 * 1000 / (b * d1 ** 2)) * math.sqrt((u + 1) / u)
    return round(sigma, 2)


def calc_bending_strength(t1, m, z1, b, Y_F=2.6, Y_S=1.6, K=1.4):
    """齿根弯曲强度
    σ_F = (2*K*T1) / (b*m*d1) * Y_F * Y_S
    返回 σ_F (MPa)
    """
    d1 = m * z1
    if d1 == 0 or b == 0 or m == 0:
        raise ValueError("分度圆直径、齿宽和模数不能为零")
    sigma = (2 * K * t1 * 1000) / (b * m * d1) * Y_F * Y_S
    return round(sigma, 2)


def check_strength(sigma, sigma_lim, safety_factor):
    """强度校核
    sigma: 计算应力 (MPa)
    sigma_lim: 极限应力 (MPa)
    safety_factor: 安全系数
    返回: dict
    """
    sigma_allow = sigma_lim / safety_factor
    passed = sigma <= sigma_allow
    actual_safety = sigma_lim / sigma if sigma > 0 else float("inf")
    return {
        "sigma_calc": round(sigma, 2),
        "sigma_lim": sigma_lim,
        "safety_factor": safety_factor,
        "sigma_allow": round(sigma_allow, 2),
        "actual_safety": round(actual_safety, 3),
        "passed": passed,
        "conclusion": "合格" if passed else "不合格",
    }


def full_strength_check(t1, m, z1, z2, b, material_data, K=1.4,
                        S_H=1.2, S_F=1.4, Y_F=2.6, Y_S=1.6):
    """完整强度校核
    material_data: dict with sigma_hlim, sigma_flim
    """
    sigma_H = calc_contact_strength_detail(t1, m, z1, z2, b, K=K)
    sigma_F = calc_bending_strength(t1, m, z1, b, Y_F=Y_F, Y_S=Y_S, K=K)

    h_check = check_strength(sigma_H, material_data["sigma_hlim"], S_H)
    f_check = check_strength(sigma_F, material_data["sigma_flim"], S_F)

    return {
        "contact": {
            "sigma_H": sigma_H,
            **h_check,
        },
        "bending": {
            "sigma_F": sigma_F,
            **f_check,
        },
        "overall_passed": h_check["passed"] and f_check["passed"],
    }


# ═══════════════════════════════════════════════════════════════
# 5. 齿形系数估算
# ═══════════════════════════════════════════════════════════════

def estimate_form_factor(z):
    """根据齿数估算齿形系数 Y_F (ISO 标准近似)"""
    # 常用齿数范围的近似值
    if z <= 12:
        return 3.6
    elif z <= 14:
        return 3.2
    elif z <= 17:
        return 3.0
    elif z <= 20:
        return 2.8
    elif z <= 25:
        return 2.65
    elif z <= 30:
        return 2.55
    elif z <= 40:
        return 2.45
    elif z <= 60:
        return 2.35
    elif z <= 100:
        return 2.25
    else:
        return 2.15


def estimate_stress_correction_factor(z):
    """根据齿数估算应力修正系数 Y_S"""
    if z <= 12:
        return 1.45
    elif z <= 17:
        return 1.52
    elif z <= 25:
        return 1.58
    elif z <= 40:
        return 1.62
    elif z <= 60:
        return 1.65
    else:
        return 1.68
