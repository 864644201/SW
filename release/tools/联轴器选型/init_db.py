# -*- coding: utf-8 -*-
"""
初始化联轴器数据库 (SQLite)
运行此脚本可生成 coupling.db 文件
"""

import os
import sqlite3
from coupling_db import SERIES_MAP, SERIES_STANDARD, COLUMNS

DB_PATH = os.path.join(os.path.dirname(os.path.abspath(__file__)), "coupling.db")


def init_database(db_path=DB_PATH):
    """创建并填充联轴器数据库"""
    if os.path.exists(db_path):
        os.remove(db_path)

    conn = sqlite3.connect(db_path)
    cur = conn.cursor()

    # 创建系列信息表
    cur.execute("""
        CREATE TABLE IF NOT EXISTS series_info (
            series_name TEXT PRIMARY KEY,
            standard    TEXT NOT NULL
        )
    """)

    # 创建联轴器规格表
    cur.execute("""
        CREATE TABLE IF NOT EXISTS couplings (
            id          INTEGER PRIMARY KEY AUTOINCREMENT,
            series_name TEXT    NOT NULL,
            model       TEXT    NOT NULL,
            Tn          REAL    NOT NULL,
            n_max       REAL    NOT NULL,
            d_min       REAL    NOT NULL,
            d_max       REAL    NOT NULL,
            D           REAL    NOT NULL,
            L           REAL    NOT NULL,
            I           REAL    NOT NULL,
            mass        REAL    NOT NULL,
            angular_comp  REAL,
            radial_comp   REAL,
            axial_comp    REAL,
            FOREIGN KEY (series_name) REFERENCES series_info(series_name)
        )
    """)

    cur.execute("CREATE INDEX idx_series ON couplings(series_name)")
    cur.execute("CREATE INDEX idx_Tn ON couplings(Tn)")
    cur.execute("CREATE INDEX idx_model ON couplings(model)")

    # 插入数据
    for series_name, standard in SERIES_STANDARD.items():
        cur.execute(
            "INSERT INTO series_info (series_name, standard) VALUES (?, ?)",
            (series_name, standard),
        )

    for series_name, rows in SERIES_MAP.items():
        for row in rows:
            cur.execute(
                """INSERT INTO couplings
                   (series_name, model, Tn, n_max, d_min, d_max, D, L, I, mass,
                    angular_comp, radial_comp, axial_comp)
                   VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)""",
                (series_name, *row),
            )

    conn.commit()
    conn.close()
    print(f"[完成] 数据库已生成: {db_path}")
    return db_path


if __name__ == "__main__":
    init_database()
