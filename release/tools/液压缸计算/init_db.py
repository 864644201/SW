# -*- coding: utf-8 -*-
"""
液压缸标准数据库初始化模块
init_db.py - 创建并填充液压缸标准件数据库
"""
import os
import sqlite3

DB_DIR = os.path.dirname(os.path.abspath(__file__))
DB_PATH = os.path.join(DB_DIR, "hydraulic.db")


def init_database():
    """初始化液压缸标准数据库"""
    conn = sqlite3.connect(DB_PATH)
    c = conn.cursor()

    # ── 标准缸筒内径 GB/T 2348 ──
    c.execute("DROP TABLE IF EXISTS std_bore")
    c.execute("""CREATE TABLE std_bore (
        id   INTEGER PRIMARY KEY AUTOINCREMENT,
        dia  REAL NOT NULL,
        note TEXT DEFAULT ''
    )""")
    bores = [8, 10, 12, 16, 20, 25, 32, 40, 50, 63,
             80, 100, 125, 140, 160, 180, 200, 220,
             250, 320, 400, 500]
    for d in bores:
        c.execute("INSERT INTO std_bore (dia) VALUES (?)", (d,))

    # ── 标准活塞杆直径 GB/T 2348 ──
    c.execute("DROP TABLE IF EXISTS std_rod")
    c.execute("""CREATE TABLE std_rod (
        id   INTEGER PRIMARY KEY AUTOINCREMENT,
        dia  REAL NOT NULL,
        note TEXT DEFAULT ''
    )""")
    rods = [4, 5, 6, 8, 10, 12, 14, 16, 18, 20, 22, 25, 28,
            32, 36, 40, 45, 50, 56, 63, 70, 80, 90, 100,
            110, 125, 140, 160, 180, 200, 220, 250, 280]
    for d in rods:
        c.execute("INSERT INTO std_rod (dia) VALUES (?)", (d,))

    # ── HSG系列工程液压缸 ──
    c.execute("DROP TABLE IF EXISTS hsg_series")
    c.execute("""CREATE TABLE hsg_series (
        id                INTEGER PRIMARY KEY AUTOINCREMENT,
        series            TEXT NOT NULL,
        model             TEXT NOT NULL,
        bore_dia_D        REAL NOT NULL,
        rod_dia_d         REAL NOT NULL,
        max_stroke        REAL NOT NULL,
        max_pressure      REAL NOT NULL,
        push_force_16MPa  REAL,
        pull_force_16MPa  REAL,
        min_install_length REAL,
        mass_per_100mm    REAL
    )""")
    # HSG01系列常用数据(16MPa, η=0.9)
    hsg_data = [
        ("HSG01", "HSGK01-40/22",  40,  22,  500, 16, 20.1, 14.0, 230, 2.2),
        ("HSG01", "HSGK01-50/28",  50,  28,  600, 16, 31.4, 21.5, 255, 3.0),
        ("HSG01", "HSGK01-63/36",  63,  36,  800, 16, 49.9, 33.6, 285, 4.5),
        ("HSG01", "HSGK01-80/45",  80,  45, 1000, 16, 80.4, 54.8, 320, 7.0),
        ("HSG01", "HSGK01-100/56", 100, 56, 1200, 16, 125.7, 86.2, 370, 10.5),
        ("HSG01", "HSGK01-125/70", 125, 70, 1500, 16, 196.3, 134.7, 430, 16.0),
        ("HSG01", "HSGK01-140/80", 140, 80, 1600, 16, 246.3, 166.0, 470, 20.0),
        ("HSG01", "HSGK01-160/90", 160, 90, 1800, 16, 321.7, 219.9, 520, 26.0),
        ("HSG01", "HSGK01-180/100", 180, 100, 2000, 16, 407.2, 281.5, 570, 33.0),
        ("HSG01", "HSGK01-200/110", 200, 110, 2000, 16, 502.7, 350.6, 620, 42.0),
    ]
    for row in hsg_data:
        c.execute("""INSERT INTO hsg_series
            (series, model, bore_dia_D, rod_dia_d, max_stroke, max_pressure,
             push_force_16MPa, pull_force_16MPa, min_install_length, mass_per_100mm)
            VALUES (?,?,?,?,?,?,?,?,?,?)""", row)

    # ── 密封件选型表 ──
    c.execute("DROP TABLE IF EXISTS seal_spec")
    c.execute("""CREATE TABLE seal_spec (
        id          INTEGER PRIMARY KEY AUTOINCREMENT,
        seal_type   TEXT NOT NULL,
        bore_d_min  REAL,
        bore_d_max  REAL,
        rod_d_min   REAL,
        rod_d_max   REAL,
        material    TEXT,
        pressure_max REAL,
        temp_range  TEXT
    )""")
    seal_data = [
        ("O型密封圈",   6,  500,  4, 280, "丁腈橡胶NBR",   35, "-40~+100"),
        ("Y型密封圈",  20,  500, 10, 280, "聚氨酯PU",      32, "-30~+100"),
        ("Yx型密封圈", 20,  500, 10, 280, "聚氨酯PU",      40, "-30~+100"),
        ("格莱圈",     25,  500, 14, 280, "PTFE+丁腈橡胶", 50, "-40~+200"),
        ("斯特封",     25,  500, 14, 280, "PTFE+丁腈橡胶", 60, "-40~+200"),
        ("组合密封",   40,  500, 22, 280, "PTFE+NBR",      70, "-40~+200"),
    ]
    for row in seal_data:
        c.execute("""INSERT INTO seal_spec
            (seal_type, bore_d_min, bore_d_max, rod_d_min, rod_d_max,
             material, pressure_max, temp_range)
            VALUES (?,?,?,?,?,?,?,?)""", row)

    conn.commit()
    conn.close()
    print(f"[OK] 数据库初始化完成: {DB_PATH}")


if __name__ == "__main__":
    init_database()
