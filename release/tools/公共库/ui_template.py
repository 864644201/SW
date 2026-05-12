"""UI 模板模块 - 标准化界面组件"""
import tkinter as tk
from tkinter import ttk, messagebox
import math


class CalcWindow(tk.Tk):
    """计算模块通用窗口模板"""

    def __init__(self, title, width=900, height=650):
        super().__init__()
        self.title(title)
        self.geometry(f"{width}x{height}")
        self.resizable(True, True)
        self.minsize(700, 500)

        # 样式
        style = ttk.Style()
        style.theme_use('clam')

        # 主框架
        self.main_frame = ttk.Frame(self, padding=10)
        self.main_frame.pack(fill=tk.BOTH, expand=True)

        # 输入区
        self.input_frame = ttk.LabelFrame(self.main_frame, text="输入参数", padding=10)
        self.input_frame.pack(fill=tk.X, pady=(0, 5))

        # 按钮区
        self.btn_frame = ttk.Frame(self.main_frame)
        self.btn_frame.pack(fill=tk.X, pady=5)

        # 结果区
        self.result_frame = ttk.LabelFrame(self.main_frame, text="计算结果", padding=10)
        self.result_frame.pack(fill=tk.BOTH, expand=True, pady=(5, 0))

    def add_input_row(self, label, var, unit="", row=0, col=0):
        """添加输入行"""
        ttk.Label(self.input_frame, text=label).grid(row=row, column=col, sticky=tk.W, padx=5, pady=3)
        entry = ttk.Entry(self.input_frame, textvariable=var, width=15)
        entry.grid(row=row, column=col + 1, padx=5, pady=3)
        if unit:
            ttk.Label(self.input_frame, text=unit).grid(row=row, column=col + 2, sticky=tk.W, padx=2, pady=3)
        return entry

    def add_combobox_row(self, label, var, values, row=0, col=0):
        """添加下拉选择行"""
        ttk.Label(self.input_frame, text=label).grid(row=row, column=col, sticky=tk.W, padx=5, pady=3)
        combo = ttk.Combobox(self.input_frame, textvariable=var, values=values, width=13, state="readonly")
        combo.grid(row=row, column=col + 1, padx=5, pady=3)
        if values:
            combo.current(0)
        return combo

    def add_result_text(self):
        """添加结果文本框"""
        text = tk.Text(self.result_frame, height=15, font=("Consolas", 10), wrap=tk.WORD)
        scrollbar = ttk.Scrollbar(self.result_frame, command=text.yview)
        text.configure(yscrollcommand=scrollbar.set)
        scrollbar.pack(side=tk.RIGHT, fill=tk.Y)
        text.pack(fill=tk.BOTH, expand=True)
        return text

    def add_calc_button(self, text="计算", command=None):
        """添加计算按钮"""
        btn = ttk.Button(self.btn_frame, text=text, command=command)
        btn.pack(side=tk.LEFT, padx=5)
        return btn

    def add_clear_button(self, text="清空", command=None):
        """添加清空按钮"""
        btn = ttk.Button(self.btn_frame, text=text, command=command)
        btn.pack(side=tk.LEFT, padx=5)
        return btn


def show_result(result_text, lines):
    """显示计算结果到文本框"""
    result_text.delete("1.0", tk.END)
    for line in lines:
        result_text.insert(tk.END, line + "\n")
    result_text.see("1.0")


def format_table(headers, rows, col_widths=None):
    """格式化表格为文本"""
    if not col_widths:
        col_widths = []
        for i, h in enumerate(headers):
            max_w = len(str(h))
            for row in rows:
                if i < len(row):
                    max_w = max(max_w, len(str(row[i])))
            col_widths.append(max_w + 2)

    lines = []
    header_line = "".join(str(h).ljust(w) for h, w in zip(headers, col_widths))
    lines.append(header_line)
    lines.append("-" * len(header_line))
    for row in rows:
        lines.append("".join(str(v).ljust(w) for v, w in zip(row, col_widths)))
    return "\n".join(lines)
