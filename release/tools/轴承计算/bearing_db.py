"""
轴承数据库模块 - Bearing Database Module
提供标准轴承数据的 SQLite 数据库访问接口
"""

import sqlite3
import os

DB_PATH = os.path.join(os.path.dirname(os.path.abspath(__file__)), "bearings.db")


def get_connection():
    """获取数据库连接"""
    conn = sqlite3.connect(DB_PATH)
    conn.row_factory = sqlite3.Row
    return conn


def init_check():
    """检查数据库是否存在，不存在则初始化"""
    if not os.path.exists(DB_PATH):
        from init_db import init_database
        init_database()


def get_bearings_by_type(bearing_type):
    """按类型查询轴承列表
    bearing_type: '深沟球轴承' / '角接触球轴承' / '圆锥滚子轴承'
    """
    init_check()
    conn = get_connection()
    try:
        cursor = conn.execute(
            "SELECT * FROM bearings WHERE type = ? ORDER BY bearing_code",
            (bearing_type,)
        )
        return [dict(row) for row in cursor.fetchall()]
    finally:
        conn.close()


def get_bearing_by_code(bearing_code):
    """按型号查询单个轴承"""
    init_check()
    conn = get_connection()
    try:
        cursor = conn.execute(
            "SELECT * FROM bearings WHERE bearing_code = ?",
            (bearing_code,)
        )
        row = cursor.fetchone()
        return dict(row) if row else None
    finally:
        conn.close()


def get_all_bearings():
    """查询所有轴承"""
    init_check()
    conn = get_connection()
    try:
        cursor = conn.execute("SELECT * FROM bearings ORDER BY type, bearing_code")
        return [dict(row) for row in cursor.fetchall()]
    finally:
        conn.close()


def search_bearings(bearing_type=None, min_C=None, min_C0=None, min_bore=None, max_bore=None):
    """多条件搜索轴承"""
    init_check()
    conditions = []
    params = []

    if bearing_type:
        conditions.append("type = ?")
        params.append(bearing_type)
    if min_C is not None:
        conditions.append("dynamic_load_C >= ?")
        params.append(min_C)
    if min_C0 is not None:
        conditions.append("static_load_C0 >= ?")
        params.append(min_C0)
    if min_bore is not None:
        conditions.append("inner_dia >= ?")
        params.append(min_bore)
    if max_bore is not None:
        conditions.append("inner_dia <= ?")
        params.append(max_bore)

    sql = "SELECT * FROM bearings"
    if conditions:
        sql += " WHERE " + " AND ".join(conditions)
    sql += " ORDER BY dynamic_load_C"

    conn = get_connection()
    try:
        cursor = conn.execute(sql, params)
        return [dict(row) for row in cursor.fetchall()]
    finally:
        conn.close()


def get_bearing_types():
    """获取所有轴承类型"""
    init_check()
    conn = get_connection()
    try:
        cursor = conn.execute("SELECT DISTINCT type FROM bearings ORDER BY type")
        return [row[0] for row in cursor.fetchall()]
    finally:
        conn.close()
