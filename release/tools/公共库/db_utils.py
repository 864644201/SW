"""数据库工具模块 - SQLite 数据库操作封装"""
import sqlite3
import os

class StandardDB:
    """标准件数据库管理"""

    def __init__(self, db_path):
        self.db_path = db_path
        self._ensure_db()

    def _ensure_db(self):
        os.makedirs(os.path.dirname(self.db_path), exist_ok=True) if os.path.dirname(self.db_path) else None

    def connect(self):
        return sqlite3.connect(self.db_path)

    def query(self, sql, params=None):
        conn = self.connect()
        try:
            cur = conn.cursor()
            cur.execute(sql, params or [])
            columns = [d[0] for d in cur.description]
            return [dict(zip(columns, row)) for row in cur.fetchall()]
        finally:
            conn.close()

    def execute(self, sql, params=None):
        conn = self.connect()
        try:
            conn.execute(sql, params or [])
            conn.commit()
        finally:
            conn.close()

    def execute_many(self, sql, data):
        conn = self.connect()
        try:
            conn.executemany(sql, data)
            conn.commit()
        finally:
            conn.close()

    def table_exists(self, table_name):
        result = self.query(
            "SELECT name FROM sqlite_master WHERE type='table' AND name=?",
            [table_name]
        )
        return len(result) > 0
