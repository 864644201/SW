# -*- coding: utf-8 -*-
"""
液压缸计算引擎
Hydraulic Cylinder Calculation Engine
"""
import math


# ─── 标准尺寸表 GB/T 2348 ─────────────────────────────────
STD_BORES = [8, 10, 12, 16, 20, 25, 32, 40, 50, 63,
             80, 100, 125, 140, 160, 180, 200, 220,
             250, 320, 400, 500]

STD_RODS = [4, 5, 6, 8, 10, 12, 14, 16, 18, 20, 22, 25,
            28, 32, 36, 40, 45, 50, 56, 63, 70, 80, 90,
            100, 110, 125, 140, 160, 180, 200, 220, 250, 280]

SPEED_RATIOS = [1.25, 1.33, 1.46, 1.66, 2.0]

# 安装形式长度系数 μ
MOUNT_MU = {
    "固定-固定": 0.5,
    "固定-铰支": 0.7,
    "铰支-铰支": 1.0,
    "固定-自由": 2.0,
}


def _nearest_std(val, std_list):
    """向上取最接近的标准值"""
    for s in std_list:
        if s >= val:
            return s
    return std_list[-1]


# ═══════════════════════════════════════════════════════════
#  缸筒内径 D 计算
# ═══════════════════════════════════════════════════════════

def calc_bore_single(F_N, p_Pa, eta_m):
    """单作用缸: D = sqrt(4F / (π·p·η_m)), 返回 mm"""
    D_m = math.sqrt(4.0 * F_N / (math.pi * p_Pa * eta_m))
    return D_m * 1000.0


def calc_bore_double(F1_N, p_Pa, eta_m, d_mm):
    """双作用缸推力侧: D = sqrt(4F1/(π·p·η_m) + d²), 返回 mm"""
    d_m = d_mm / 1000.0
    D_m = math.sqrt(4.0 * F1_N / (math.pi * p_Pa * eta_m) + d_m ** 2)
    return D_m * 1000.0


def standardize_bore(D_calc):
    return _nearest_std(D_calc, STD_BORES)


# ═══════════════════════════════════════════════════════════
#  活塞杆直径 d 计算
# ═══════════════════════════════════════════════════════════

def calc_rod_by_ratio(D_mm, phi):
    """速比法: d = D * sqrt((φ-1)/φ), 返回 mm"""
    return D_mm * math.sqrt((phi - 1.0) / phi)


def calc_rod_by_pressure(D_mm, p_MPa):
    """按压力等级选取 d/D 比值"""
    if p_MPa <= 10.0:
        ratio = 0.5
    elif p_MPa <= 20.0:
        ratio = 0.6
    else:
        ratio = 0.7
    return D_mm * ratio


def standardize_rod(d_calc):
    return _nearest_std(d_calc, STD_RODS)


# ═══════════════════════════════════════════════════════════
#  推力/拉力计算
# ═══════════════════════════════════════════════════════════

def calc_push_force(D_mm, p_MPa, eta_m=0.9):
    """无杆腔推力 F1 = π/4·D²·p·η_m, 返回 kN"""
    D_m = D_mm / 1000.0
    p_Pa = p_MPa * 1e6
    return math.pi / 4.0 * D_m ** 2 * p_Pa * eta_m / 1000.0


def calc_pull_force(D_mm, d_mm, p_MPa, eta_m=0.9):
    """有杆腔拉力 F2 = π/4·(D²-d²)·p·η_m, 返回 kN"""
    D_m = D_mm / 1000.0
    d_m = d_mm / 1000.0
    p_Pa = p_MPa * 1e6
    return math.pi / 4.0 * (D_m ** 2 - d_m ** 2) * p_Pa * eta_m / 1000.0


# ═══════════════════════════════════════════════════════════
#  速度计算
# ═══════════════════════════════════════════════════════════

def calc_advance_speed(Q_Lmin, D_mm):
    """前进速度 v1 = Q/(π/4·D²)×10⁶, 返回 mm/s"""
    A_mm2 = math.pi / 4.0 * D_mm ** 2
    return Q_Lmin * 1e6 / (60.0 * A_mm2)


def calc_retract_speed(Q_Lmin, D_mm, d_mm):
    """返回速度 v2 = Q/(π/4·(D²-d²))×10⁶, 返回 mm/s"""
    A_mm2 = math.pi / 4.0 * (D_mm ** 2 - d_mm ** 2)
    return Q_Lmin * 1e6 / (60.0 * A_mm2)


# ═══════════════════════════════════════════════════════════
#  流量计算
# ═══════════════════════════════════════════════════════════

def calc_flow(A_mm2, v_mms):
    """Q = A·v/10⁶×60, 返回 L/min"""
    return A_mm2 * v_mms * 60.0 / 1e6


# ═══════════════════════════════════════════════════════════
#  功率计算
# ═══════════════════════════════════════════════════════════

def calc_power(p_MPa, Q_Lmin):
    """液压功率 Ph = p·Q/60, 返回 kW"""
    return p_MPa * Q_Lmin / 60.0


# ═══════════════════════════════════════════════════════════
#  缸筒壁厚计算
# ═══════════════════════════════════════════════════════════

def calc_wall_lame(D_mm, p_MPa, sigma_MPa):
    """Lamé厚壁公式: δ = D/2·((σ+0.4p)/(σ-1.3p)-1), 返回 mm"""
    denom = sigma_MPa - 1.3 * p_MPa
    if denom <= 0:
        return float('inf')
    return D_mm / 2.0 * ((sigma_MPa + 0.4 * p_MPa) / denom - 1.0)


def calc_wall_simplified(D_mm, p_MPa, sigma_MPa, safety=1.5):
    """简化公式: δ ≥ p·D/(2σ-p)·安全系数, 返回 mm"""
    denom = 2.0 * sigma_MPa - p_MPa
    if denom <= 0:
        return float('inf')
    return p_MPa * D_mm / denom * safety


# ═══════════════════════════════════════════════════════════
#  稳定性校核
# ═══════════════════════════════════════════════════════════

def calc_critical_force(d_mm, L_mm, mu, E_GPa=210.0):
    """欧拉公式 F_cr = π²·E·I/(μ·L)², 返回 kN"""
    d_m = d_mm / 1000.0
    L_m = L_mm / 1000.0
    E_Pa = E_GPa * 1e9
    I = math.pi / 64.0 * d_m ** 4
    F_cr = math.pi ** 2 * E_Pa * I / (mu * L_m) ** 2
    return F_cr / 1000.0


def buckling_check(d_mm, L_mm, mu, F_kN, safety_min=2.0):
    """稳定性校核, 返回 (F_cr_kN, 安全系数, 是否通过)"""
    F_cr = calc_critical_force(d_mm, L_mm, mu)
    if F_kN <= 0:
        return F_cr, float('inf'), True
    sf = F_cr / F_kN
    return F_cr, sf, sf >= safety_min


# ═══════════════════════════════════════════════════════════
#  综合计算
# ═══════════════════════════════════════════════════════════

def full_calculation(F_kN, p_MPa, phi, Q_Lmin,
                     mount_type="铰支-铰支", L_mm=1000.0,
                     eta_m=0.9):
    """
    综合计算入口
    返回字典包含所有计算结果
    """
    F_N = F_kN * 1000.0
    p_Pa = p_MPa * 1e6

    # 1. 初算缸筒内径(单作用)
    D_pre = calc_bore_single(F_N, p_Pa, eta_m)

    # 2. 按速比算杆径
    d_calc = calc_rod_by_ratio(D_pre, phi)

    # 3. 双作用缸精确内径
    D_calc = calc_bore_double(F_N, p_Pa, eta_m, d_calc)

    # 4. 标准化
    D_std = standardize_bore(D_calc)
    d_std = standardize_rod(d_calc)

    # 5. 实际推拉力
    F1 = calc_push_force(D_std, p_MPa, eta_m)
    F2 = calc_pull_force(D_std, d_std, p_MPa, eta_m)

    # 6. 实际速比
    actual_phi = F1 / F2 if F2 > 0 else float('inf')

    # 7. 速度
    v1 = calc_advance_speed(Q_Lmin, D_std)
    v2 = calc_retract_speed(Q_Lmin, D_std, d_std)

    # 8. 功率
    Ph = calc_power(p_MPa, Q_Lmin)

    # 9. 壁厚(默认45#钢 σ=120MPa)
    sigma = 120.0
    wall_lame = calc_wall_lame(D_std, p_MPa, sigma)
    wall_simple = calc_wall_simplified(D_std, p_MPa, sigma)

    # 10. 稳定性
    mu = MOUNT_MU.get(mount_type, 1.0)
    F_cr, sf, ok = buckling_check(d_std, L_mm, mu, F1)

    return {
        "D_calc": round(D_calc, 2),
        "D_std": D_std,
        "d_calc": round(d_calc, 2),
        "d_std": d_std,
        "F1": round(F1, 2),
        "F2": round(F2, 2),
        "actual_phi": round(actual_phi, 3),
        "v1": round(v1, 2),
        "v2": round(v2, 2),
        "Ph": round(Ph, 3),
        "wall_lame": round(wall_lame, 2),
        "wall_simple": round(wall_simple, 2),
        "sigma": sigma,
        "F_cr": round(F_cr, 2),
        "sf": round(sf, 2),
        "buckling_ok": ok,
        "mount_type": mount_type,
        "mu": mu,
    }
