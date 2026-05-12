# -*- coding: utf-8 -*-
"""
减速器标准数据库初始化模块
"""
import sqlite3
import os

DB_PATH = os.path.join(os.path.dirname(os.path.abspath(__file__)), "reducer.db")


def init_database():
    """初始化减速器数据库"""
    conn = sqlite3.connect(DB_PATH)
    c = conn.cursor()

    # ── 标准模数表 (GB/T 1357) ──
    c.execute("""CREATE TABLE IF NOT EXISTS std_module (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        series INTEGER NOT NULL,  -- 1=第一系列, 2=第二系列
        module REAL NOT NULL
    )""")

    # 第一系列（优先选用）
    first_series = [1, 1.25, 1.5, 2, 2.5, 3, 4, 5, 6, 8, 10, 12, 16, 20, 25, 32]
    # 第二系列
    second_series = [1.125, 1.375, 1.75, 2.25, 2.75, 3.5, 4.5, 5.5, 7, 9, 11, 14, 18, 22, 28, 36]

    c.execute("DELETE FROM std_module")
    for m in first_series:
        c.execute("INSERT INTO std_module (series, module) VALUES (1, ?)", (m,))
    for m in second_series:
        c.execute("INSERT INTO std_module (series, module) VALUES (2, ?)", (m,))

    # ── 齿轮材料表 ──
    c.execute("""CREATE TABLE IF NOT EXISTS gear_material (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        name TEXT NOT NULL,          -- 材料名称
        sigma_hlim REAL NOT NULL,    -- 接触疲劳极限 (MPa)
        sigma_flim REAL NOT NULL,    -- 弯曲疲劳极限 (MPa)
        hardness TEXT NOT NULL,      -- 硬度
        heat_treatment TEXT          -- 热处理方式
    )""")

    c.execute("DELETE FROM gear_material")
    materials = [
        ("45钢 正火", 530, 210, "162-217 HB", "正火"),
        ("45钢 调质", 580, 240, "217-255 HB", "调质"),
        ("45钢 表面淬火", 1100, 320, "40-50 HRC", "表面淬火"),
        ("40Cr 调质", 700, 300, "241-286 HB", "调质"),
        ("40Cr 表面淬火", 1200, 380, "48-55 HRC", "表面淬火"),
        ("35SiMn 调质", 680, 290, "217-269 HB", "调质"),
        ("42CrMo 调质", 750, 320, "255-286 HB", "调质"),
        ("20CrMnTi 渗碳淬火", 1400, 420, "56-62 HRC", "渗碳淬火"),
        ("20Cr 渗碳淬火", 1100, 350, "56-62 HRC", "渗碳淬火"),
        ("ZG310-570", 470, 180, "160-210 HB", "正火"),
        ("HT250", 320, 90, "170-241 HB", "退火"),
        ("QT500-7", 460, 150, "170-230 HB", "退火"),
    ]
    for mat in materials:
        c.execute("""INSERT INTO gear_material
                     (name, sigma_hlim, sigma_flim, hardness, heat_treatment)
                     VALUES (?, ?, ?, ?, ?)""", mat)

    # ── 常用传动比 ──
    c.execute("""CREATE TABLE IF NOT EXISTS std_ratio (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        ratio REAL NOT NULL
    )""")
    c.execute("DELETE FROM std_ratio")
    for r in [1.25, 1.6, 2.0, 2.5, 3.15, 4.0, 5.0, 6.3, 8.0, 10.0,
              12.5, 16.0, 20.0, 25.0, 31.5, 40.0, 50.0, 63.0, 80.0, 100.0]:
        c.execute("INSERT INTO std_ratio (ratio) VALUES (?)", (r,))

    # ── 圆柱齿轮减速器: ZDY 单级 ──
    c.execute("""CREATE TABLE IF NOT EXISTS reducer_zdy (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        model TEXT NOT NULL,           -- 型号
        ratio REAL NOT NULL,           -- 公称传动比
        rated_power REAL NOT NULL,     -- 额定输入功率 (kW)
        rated_torque REAL NOT NULL,    -- 额定输出扭矩 (N·m)
        max_input_speed INTEGER,       -- 许用最高输入转速 (RPM)
        efficiency REAL,               -- 效率
        mass_kg REAL,                  -- 质量 (kg)
        length_mm REAL,                -- 长 (mm)
        width_mm REAL,                 -- 宽 (mm)
        height_mm REAL                 -- 高 (mm)
    )""")

    c.execute("DELETE FROM reducer_zdy")
    zdy_data = []
    # ZDY 型号: 80, 100, 125, 160, 200, 250, 280, 315, 355, 400, 450, 500, 560
    zdy_models = [
        ("ZDY80",   80),  ("ZDY100",  100), ("ZDY125",  125),
        ("ZDY160",  160), ("ZDY200",  200), ("ZDY250",  250),
        ("ZDY280",  280), ("ZDY315",  315), ("ZDY355",  355),
        ("ZDY400",  400), ("ZDY450",  450), ("ZDY500",  500),
        ("ZDY560",  560),
    ]
    # 各型号基础参数 (power_base, torque_base, mass, L, W, H)
    zdy_specs = [
        (5.0,   200,    14,  235, 150, 170),
        (10.0,  450,    28,  290, 175, 200),
        (22.0,  900,    52,  370, 215, 250),
        (45.0,  1800,   95,  460, 260, 300),
        (85.0,  3500,   170, 550, 310, 360),
        (155.0, 6000,   290, 660, 370, 420),
        (210.0, 8500,   380, 730, 410, 460),
        (280.0, 11500,  510, 810, 450, 510),
        (380.0, 15500,  680, 900, 500, 560),
        (500.0, 20500,  900, 990, 550, 620),
        (670.0, 27000,  1150,1090,600, 680),
        (900.0, 36000,  1500,1200,660, 750),
        (1200.0,48000,  1950,1320,730, 830),
    ]
    ratios_zdy = [1.25, 1.6, 2.0, 2.5, 3.15, 4.0, 5.0, 6.3]
    for model, _ in zdy_models:
        for idx, spec in enumerate(zdy_specs):
            m_name = model
            for ri, rat in enumerate(ratios_zdy):
                power = round(spec[0] * (1.0 / rat) ** 0.3 * (1 + ri * 0.02), 1)
                torque = round(spec[1] * (rat ** 0.8) * (1 + idx * 0.05), 0)
                zdy_data.append((
                    m_name, rat, power, torque,
                    1500 if spec[1] < 1000 else 1200,
                    round(0.97 - (rat - 1) * 0.002, 4),
                    round(spec[2] * (1 + idx * 0.08), 1),
                    spec[3], spec[4], spec[5]
                ))
            break  # only first set of ratios per model
    # Simpler approach: generate per model with scaling
    c.execute("DELETE FROM reducer_zdy")
    for mi, (model, base_size) in enumerate(zdy_models):
        spec = zdy_specs[mi]
        for rat in ratios_zdy:
            power = round(spec[0] * (6.3 / rat) ** 0.7, 2)
            torque = round(spec[1] * (rat / 6.3) ** 0.95, 0)
            speed = 1500 if mi < 5 else 1200
            eff = round(0.98 - (rat - 1) * 0.001, 4)
            mass = round(spec[2] * (1 + mi * 0.06), 1)
            c.execute("""INSERT INTO reducer_zdy
                (model, ratio, rated_power, rated_torque, max_input_speed,
                 efficiency, mass_kg, length_mm, width_mm, height_mm)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)""",
                      (model, rat, power, torque, speed, eff, mass,
                       spec[3], spec[4], spec[5]))

    # ── 圆柱齿轮减速器: ZLY 两级 ──
    c.execute("""CREATE TABLE IF NOT EXISTS reducer_zly (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        model TEXT NOT NULL,
        ratio REAL NOT NULL,
        rated_power REAL NOT NULL,
        rated_torque REAL NOT NULL,
        max_input_speed INTEGER,
        efficiency REAL,
        mass_kg REAL,
        length_mm REAL,
        width_mm REAL,
        height_mm REAL
    )""")

    c.execute("DELETE FROM reducer_zly")
    zly_models = [
        ("ZLY112", 5.0,  80,   18,  300, 190, 220),
        ("ZLY125", 8.0,  130,  26,  340, 215, 250),
        ("ZLY140", 13.0, 210,  38,  385, 240, 275),
        ("ZLY160", 20.0, 320,  58,  440, 270, 310),
        ("ZLY180", 32.0, 500,  85,  495, 300, 345),
        ("ZLY200", 50.0, 780,  120, 555, 335, 385),
        ("ZLY224", 75.0, 1200, 170, 625, 375, 430),
        ("ZLY250", 110.0,1850, 240, 700, 420, 480),
        ("ZLY280", 160.0,2700, 330, 785, 470, 540),
        ("ZLY315", 230.0,4000, 460, 875, 525, 605),
        ("ZLY355", 330.0,5800, 640, 975, 585, 675),
        ("ZLY400", 470.0,8500, 880,1085, 655, 755),
        ("ZLY450", 660.0,12500,1200,1210,730, 840),
    ]
    zly_ratios = [6.3, 8.0, 10.0, 12.5, 16.0, 20.0, 25.0]
    for mi, (model, pw, tq, mass, L, W, H) in enumerate(zly_models):
        for rat in zly_ratios:
            power = round(pw * (25.0 / rat) ** 0.65, 2)
            torque = round(tq * (rat / 25.0) ** 0.9, 0)
            speed = 1500 if mi < 6 else 1200
            eff = round(0.95 - (rat - 6.3) * 0.001, 4)
            c.execute("""INSERT INTO reducer_zly
                (model, ratio, rated_power, rated_torque, max_input_speed,
                 efficiency, mass_kg, length_mm, width_mm, height_mm)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)""",
                      (model, rat, power, torque, speed, eff, mass, L, W, H))

    # ── 圆柱齿轮减速器: ZSY 三级 ──
    c.execute("""CREATE TABLE IF NOT EXISTS reducer_zsy (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        model TEXT NOT NULL,
        ratio REAL NOT NULL,
        rated_power REAL NOT NULL,
        rated_torque REAL NOT NULL,
        max_input_speed INTEGER,
        efficiency REAL,
        mass_kg REAL,
        length_mm REAL,
        width_mm REAL,
        height_mm REAL
    )""")

    c.execute("DELETE FROM reducer_zsy")
    zsy_models = [
        ("ZSY160", 12.0, 250,   42,  480, 280, 330),
        ("ZSY180", 18.0, 390,   62,  540, 310, 365),
        ("ZSY200", 27.0, 600,   88,  605, 345, 410),
        ("ZSY224", 40.0, 930,   125, 680, 390, 455),
        ("ZSY250", 60.0, 1450,  175, 765, 435, 510),
        ("ZSY280", 85.0, 2150,  245, 855, 490, 570),
        ("ZSY315", 125.0,3200,  340, 960, 550, 640),
        ("ZSY355", 180.0,4700,  470,1070, 610, 715),
        ("ZSY400", 260.0,7000,  650,1200, 685, 800),
        ("ZSY450", 380.0,10000, 900,1340, 765, 895),
    ]
    zsy_ratios = [20.0, 25.0, 31.5, 40.0, 50.0, 63.0, 80.0, 100.0]
    for mi, (model, pw, tq, mass, L, W, H) in enumerate(zsy_models):
        for rat in zsy_ratios:
            power = round(pw * (100.0 / rat) ** 0.6, 2)
            torque = round(tq * (rat / 100.0) ** 0.85, 0)
            speed = 1500 if mi < 5 else 1200
            eff = round(0.93 - (rat - 20.0) * 0.0005, 4)
            c.execute("""INSERT INTO reducer_zsy
                (model, ratio, rated_power, rated_torque, max_input_speed,
                 efficiency, mass_kg, length_mm, width_mm, height_mm)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)""",
                      (model, rat, power, torque, speed, eff, mass, L, W, H))

    # ── 行星齿轮减速器: NGW 系列 ──
    c.execute("""CREATE TABLE IF NOT EXISTS reducer_ngw (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        model TEXT NOT NULL,
        ratio REAL NOT NULL,
        rated_power REAL NOT NULL,
        rated_torque REAL NOT NULL,
        max_input_speed INTEGER,
        efficiency REAL,
        mass_kg REAL,
        length_mm REAL,
        width_mm REAL,
        height_mm REAL
    )""")

    c.execute("DELETE FROM reducer_ngw")
    ngw_models = [
        ("NGW42",  3.0,  60,   12, 220, 170, 200),
        ("NGW52",  5.5,  110,  18, 260, 200, 235),
        ("NGW62",  10.0, 200,  28, 305, 235, 275),
        ("NGW72",  18.0, 360,  42, 355, 270, 315),
        ("NGW82",  30.0, 580,  62, 410, 310, 365),
        ("NGW92",  50.0, 950,  88, 475, 355, 415),
        ("NGW102", 80.0, 1500, 125,550, 405, 475),
        ("NGW112", 125.0,2400, 180,635, 465, 540),
        ("NGW122", 200.0,3800, 255,730, 530, 615),
    ]
    ngw_ratios = [4.0, 5.0, 6.3, 8.0, 10.0, 12.5, 16.0, 20.0, 25.0, 31.5, 40.0, 50.0, 63.0, 80.0]
    for mi, (model, pw, tq, mass, L, W, H) in enumerate(ngw_models):
        for rat in ngw_ratios:
            power = round(pw * (80.0 / rat) ** 0.55, 2)
            torque = round(tq * (rat / 80.0) ** 0.85, 0)
            speed = 1500 if mi < 5 else 1200
            eff = round(0.96 - (rat - 4.0) * 0.0003, 4)
            c.execute("""INSERT INTO reducer_ngw
                (model, ratio, rated_power, rated_torque, max_input_speed,
                 efficiency, mass_kg, length_mm, width_mm, height_mm)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)""",
                      (model, rat, power, torque, speed, eff, mass, L, W, H))

    # ── 蜗轮蜗杆减速器: CWU 系列 ──
    c.execute("""CREATE TABLE IF NOT EXISTS reducer_cwu (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        model TEXT NOT NULL,
        ratio REAL NOT NULL,
        rated_power REAL NOT NULL,
        rated_torque REAL NOT NULL,
        max_input_speed INTEGER,
        efficiency REAL,
        mass_kg REAL,
        length_mm REAL,
        width_mm REAL,
        height_mm REAL
    )""")

    c.execute("DELETE FROM reducer_cwu")
    cwu_models = [
        ("CWU50",   0.3,  20,   3.5, 185, 120, 145),
        ("CWU63",   0.6,  40,   5.0, 210, 140, 165),
        ("CWU80",   1.2,  80,   8.0, 245, 160, 190),
        ("CWU100",  2.5,  150,  13,  290, 190, 225),
        ("CWU125",  5.0,  300,  22,  345, 225, 265),
        ("CWU160",  10.0, 580,  38,  415, 270, 320),
        ("CWU200",  18.0, 1050, 62,  495, 325, 380),
        ("CWU250",  32.0, 1800, 95,  590, 390, 455),
        ("CWU315",  55.0, 3000, 145,710, 470, 545),
    ]
    cwu_ratios = [10.0, 12.5, 16.0, 20.0, 25.0, 31.5, 40.0, 50.0, 63.0, 80.0, 100.0]
    for mi, (model, pw, tq, mass, L, W, H) in enumerate(cwu_models):
        for rat in cwu_ratios:
            power = round(pw * (100.0 / rat) ** 0.5, 2)
            torque = round(tq * (rat / 100.0) ** 0.8, 0)
            speed = 1500
            # 蜗轮蜗杆效率随速比增大而降低
            eff = round(0.82 - (rat - 10.0) * 0.003, 4)
            if eff < 0.40:
                eff = 0.40
            c.execute("""INSERT INTO reducer_cwu
                (model, ratio, rated_power, rated_torque, max_input_speed,
                 efficiency, mass_kg, length_mm, width_mm, height_mm)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)""",
                      (model, rat, power, torque, speed, eff, mass, L, W, H))

    conn.commit()
    conn.close()
    print(f"数据库初始化完成: {DB_PATH}")


if __name__ == "__main__":
    init_database()
