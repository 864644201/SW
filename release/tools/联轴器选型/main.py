# -*- coding: utf-8 -*-
"""
联轴器选型工具 V1.0
入口文件 — 基于 tkinter 的图形界面
"""

import os
import sys
import tkinter as tk
from tkinter import ttk, messagebox, filedialog

# 确保可导入同目录模块
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))

from coupling_db import get_all_series_names, SERIES_STANDARD
from coupling_calc import full_selection, LOAD_FACTOR, TEMP_FACTOR, START_FACTOR


class CouplingApp(tk.Tk):
    """主窗口"""

    def __init__(self):
        super().__init__()
        self.title("联轴器选型工具 V1.0")
        self.geometry("1100x720")
        self.minsize(960, 640)
        self.resizable(True, True)

        # 系统托盘图标等可后续扩展
        self._build_ui()

    # ================================================================
    # UI 构建
    # ================================================================
    def _build_ui(self):
        # 整体使用 PanedWindow 分左右
        paned = ttk.PanedWindow(self, orient=tk.HORIZONTAL)
        paned.pack(fill=tk.BOTH, expand=True, padx=6, pady=6)

        # ---------- 左侧: 输入参数 ----------
        left_frame = ttk.Frame(paned, width=380)
        paned.add(left_frame, weight=0)
        self._build_left_panel(left_frame)

        # ---------- 右侧: 选型结果 ----------
        right_frame = ttk.Frame(paned)
        paned.add(right_frame, weight=1)
        self._build_right_panel(right_frame)

    # ----- 左侧面板 -----
    def _build_left_panel(self, parent):
        canvas = tk.Canvas(parent, highlightthickness=0)
        scrollbar = ttk.Scrollbar(parent, orient=tk.VERTICAL, command=canvas.yview)
        scroll_frame = ttk.Frame(canvas)

        scroll_frame.bind(
            "<Configure>",
            lambda e: canvas.configure(scrollregion=canvas.bbox("all")),
        )
        canvas.create_window((0, 0), window=scroll_frame, anchor="nw")
        canvas.configure(yscrollcommand=scrollbar.set)

        canvas.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)
        scrollbar.pack(side=tk.RIGHT, fill=tk.Y)

        # 鼠标滚轮支持
        def _on_mousewheel(event):
            canvas.yview_scroll(int(-1 * (event.delta / 120)), "units")
        canvas.bind_all("<MouseWheel>", _on_mousewheel)

        frame = ttk.LabelFrame(scroll_frame, text="输入参数", padding=10)
        frame.pack(fill=tk.X, padx=5, pady=5)

        row = 0

        # ---- 输入方式 ----
        ttk.Label(frame, text="输入方式:").grid(row=row, column=0, sticky="e", padx=4, pady=3)
        self.input_mode_var = tk.StringVar(value="按功率计算")
        mode_combo = ttk.Combobox(frame, textvariable=self.input_mode_var,
                                  values=["按功率计算", "按扭矩计算"], state="readonly", width=18)
        mode_combo.grid(row=row, column=1, sticky="w", padx=4, pady=3)
        mode_combo.bind("<<ComboboxSelected>>", self._on_mode_change)
        row += 1

        # ---- 功率 P ----
        ttk.Label(frame, text="功率 P (kW):").grid(row=row, column=0, sticky="e", padx=4, pady=3)
        self.power_var = tk.StringVar(value="5.5")
        self.power_entry = ttk.Entry(frame, textvariable=self.power_var, width=20)
        self.power_entry.grid(row=row, column=1, sticky="w", padx=4, pady=3)
        row += 1

        # ---- 转速 n ----
        ttk.Label(frame, text="转速 n (RPM):").grid(row=row, column=0, sticky="e", padx=4, pady=3)
        self.speed_var = tk.StringVar(value="1450")
        self.speed_entry = ttk.Entry(frame, textvariable=self.speed_var, width=20)
        self.speed_entry.grid(row=row, column=1, sticky="w", padx=4, pady=3)
        row += 1

        # ---- 工作扭矩 T ----
        ttk.Label(frame, text="工作扭矩 T (N·m):").grid(row=row, column=0, sticky="e", padx=4, pady=3)
        self.torque_var = tk.StringVar(value="")
        self.torque_entry = ttk.Entry(frame, textvariable=self.torque_var, width=20, state="disabled")
        self.torque_entry.grid(row=row, column=1, sticky="w", padx=4, pady=3)
        row += 1

        # ---- 载荷性质 ----
        ttk.Label(frame, text="载荷性质:").grid(row=row, column=0, sticky="e", padx=4, pady=3)
        self.load_var = tk.StringVar(value="平稳载荷")
        ttk.Combobox(frame, textvariable=self.load_var,
                     values=list(LOAD_FACTOR.keys()), state="readonly", width=18).grid(
            row=row, column=1, sticky="w", padx=4, pady=3)
        row += 1

        # ---- 工作温度 ----
        ttk.Label(frame, text="工作温度:").grid(row=row, column=0, sticky="e", padx=4, pady=3)
        self.temp_var = tk.StringVar(value="≤100°C")
        ttk.Combobox(frame, textvariable=self.temp_var,
                     values=list(TEMP_FACTOR.keys()), state="readonly", width=18).grid(
            row=row, column=1, sticky="w", padx=4, pady=3)
        row += 1

        # ---- 启动工况 ----
        ttk.Label(frame, text="启动工况:").grid(row=row, column=0, sticky="e", padx=4, pady=3)
        self.start_var = tk.StringVar(value="启动不频繁")
        ttk.Combobox(frame, textvariable=self.start_var,
                     values=list(START_FACTOR.keys()), state="readonly", width=18).grid(
            row=row, column=1, sticky="w", padx=4, pady=3)
        row += 1

        # ---- 分隔线 ----
        ttk.Separator(frame, orient=tk.HORIZONTAL).grid(
            row=row, column=0, columnspan=2, sticky="ew", pady=6)
        row += 1

        # ---- 主动端轴径 ----
        ttk.Label(frame, text="主动端轴径 d1 (mm):").grid(row=row, column=0, sticky="e", padx=4, pady=3)
        self.d1_var = tk.StringVar(value="30")
        ttk.Entry(frame, textvariable=self.d1_var, width=20).grid(
            row=row, column=1, sticky="w", padx=4, pady=3)
        row += 1

        # ---- 从动端轴径 ----
        ttk.Label(frame, text="从动端轴径 d2 (mm):").grid(row=row, column=0, sticky="e", padx=4, pady=3)
        self.d2_var = tk.StringVar(value="30")
        ttk.Entry(frame, textvariable=self.d2_var, width=20).grid(
            row=row, column=1, sticky="w", padx=4, pady=3)
        row += 1

        # ---- 联轴器类型 ----
        ttk.Label(frame, text="联轴器类型:").grid(row=row, column=0, sticky="e", padx=4, pady=3)
        series_names = get_all_series_names()
        self.series_var = tk.StringVar(value=series_names[0])
        ttk.Combobox(frame, textvariable=self.series_var,
                     values=series_names, state="readonly", width=26).grid(
            row=row, column=1, sticky="w", padx=4, pady=3)
        row += 1

        # ---- 补偿量需求 (可选) ----
        ttk.Separator(frame, orient=tk.HORIZONTAL).grid(
            row=row, column=0, columnspan=2, sticky="ew", pady=6)
        row += 1

        ttk.Label(frame, text="角向补偿需求(°):").grid(row=row, column=0, sticky="e", padx=4, pady=3)
        self.ang_need_var = tk.StringVar(value="0")
        ttk.Entry(frame, textvariable=self.ang_need_var, width=20).grid(
            row=row, column=1, sticky="w", padx=4, pady=3)
        row += 1

        ttk.Label(frame, text="径向补偿需求(mm):").grid(row=row, column=0, sticky="e", padx=4, pady=3)
        self.rad_need_var = tk.StringVar(value="0")
        ttk.Entry(frame, textvariable=self.rad_need_var, width=20).grid(
            row=row, column=1, sticky="w", padx=4, pady=3)
        row += 1

        ttk.Label(frame, text="轴向补偿需求(mm):").grid(row=row, column=0, sticky="e", padx=4, pady=3)
        self.ax_need_var = tk.StringVar(value="0")
        ttk.Entry(frame, textvariable=self.ax_need_var, width=20).grid(
            row=row, column=1, sticky="w", padx=4, pady=3)
        row += 1

        # ---- 按钮区 ----
        btn_frame = ttk.Frame(frame)
        btn_frame.grid(row=row, column=0, columnspan=2, pady=12)

        ttk.Button(btn_frame, text="选型计算", command=self._on_calculate).pack(
            side=tk.LEFT, padx=8, ipadx=12)
        ttk.Button(btn_frame, text="清空", command=self._on_clear).pack(
            side=tk.LEFT, padx=8, ipadx=12)
        ttk.Button(btn_frame, text="导出结果", command=self._on_export).pack(
            side=tk.LEFT, padx=8, ipadx=12)

    # ----- 右侧面板 -----
    def _build_right_panel(self, parent):
        # ---- 计算参数汇总 ----
        info_frame = ttk.LabelFrame(parent, text="计算结果", padding=8)
        info_frame.pack(fill=tk.X, padx=5, pady=5)

        self.info_text = tk.Text(info_frame, height=5, wrap=tk.WORD, font=("Consolas", 10))
        self.info_text.pack(fill=tk.X)
        self.info_text.config(state=tk.DISABLED)

        # ---- 选型结果表格 ----
        result_frame = ttk.LabelFrame(parent, text="推荐联轴器型号", padding=8)
        result_frame.pack(fill=tk.BOTH, expand=True, padx=5, pady=5)

        columns = [
            "型号", "标准", "公称扭矩\n(N·m)", "许用转速\n(RPM)",
            "轴孔范围\n(mm)", "外径D\n(mm)", "长度L\n(mm)",
            "质量\n(kg)", "角向\n(°)", "径向\n(mm)", "轴向\n(mm)",
            "校核结果",
        ]
        self.tree = ttk.Treeview(result_frame, columns=columns, show="headings", height=14)

        col_widths = [55, 80, 70, 70, 80, 60, 60, 55, 45, 50, 50, 70]
        for col, w in zip(columns, col_widths):
            self.tree.heading(col, text=col)
            self.tree.column(col, width=w, anchor="center")

        tree_scroll_y = ttk.Scrollbar(result_frame, orient=tk.VERTICAL, command=self.tree.yview)
        tree_scroll_x = ttk.Scrollbar(result_frame, orient=tk.HORIZONTAL, command=self.tree.xview)
        self.tree.configure(yscrollcommand=tree_scroll_y.set, xscrollcommand=tree_scroll_x.set)

        self.tree.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)
        tree_scroll_y.pack(side=tk.RIGHT, fill=tk.Y)

        # 行标签颜色
        self.tree.tag_configure("pass", background="#d4edda")
        self.tree.tag_configure("fail", background="#f8d7da")

        # ---- 校核明细 ----
        detail_frame = ttk.LabelFrame(parent, text="校核明细", padding=8)
        detail_frame.pack(fill=tk.X, padx=5, pady=5)

        self.detail_text = tk.Text(detail_frame, height=6, wrap=tk.WORD, font=("Consolas", 10))
        self.detail_text.pack(fill=tk.X)
        self.detail_text.config(state=tk.DISABLED)

        # 点击行显示明细
        self.tree.bind("<<TreeviewSelect>>", self._on_row_select)
        self._last_results = []

    # ================================================================
    # 事件处理
    # ================================================================
    def _on_mode_change(self, _event=None):
        mode = self.input_mode_var.get()
        if mode == "按功率计算":
            self.power_entry.config(state="normal")
            self.speed_entry.config(state="normal")
            self.torque_entry.config(state="disabled")
        else:
            self.power_entry.config(state="disabled")
            self.speed_entry.config(state="disabled")
            self.torque_entry.config(state="normal")

    def _on_calculate(self):
        try:
            input_mode = "power" if self.input_mode_var.get() == "按功率计算" else "torque"
            power_kw = float(self.power_var.get()) if self.power_var.get() else 0
            speed_rpm = float(self.speed_var.get()) if self.speed_var.get() else 0
            torque_nm = float(self.torque_var.get()) if self.torque_var.get() else 0
            d1 = float(self.d1_var.get())
            d2 = float(self.d2_var.get())
            ang_need = float(self.ang_need_var.get()) if self.ang_need_var.get() else 0
            rad_need = float(self.rad_need_var.get()) if self.rad_need_var.get() else 0
            ax_need = float(self.ax_need_var.get()) if self.ax_need_var.get() else 0
        except ValueError:
            messagebox.showerror("输入错误", "请检查数值输入是否正确")
            return

        load_type = self.load_var.get()
        temp_type = self.temp_var.get()
        start_type = self.start_var.get()
        series_name = self.series_var.get()

        if input_mode == "power" and (power_kw <= 0 or speed_rpm <= 0):
            messagebox.showwarning("输入提示", "功率和转速必须大于 0")
            return
        if input_mode == "torque" and torque_nm <= 0:
            messagebox.showwarning("输入提示", "扭矩必须大于 0")
            return
        if d1 <= 0 or d2 <= 0:
            messagebox.showwarning("输入提示", "轴径必须大于 0")
            return

        result = full_selection(
            input_mode, power_kw, speed_rpm, torque_nm,
            load_type, temp_type, start_type,
            d1, d2, series_name,
            ang_need, rad_need, ax_need,
        )
        self._display_results(result)

    def _display_results(self, result):
        # ---- 更新计算汇总 ----
        self.info_text.config(state=tk.NORMAL)
        self.info_text.delete("1.0", tk.END)

        info = (
            f"工作扭矩 T = {result['T']:.2f} N·m    "
            f"工况系数 KA = {result['KA']:.2f}    "
            f"温度系数 K = {result['K']:.1f}    "
            f"启动系数 Kw = {result['Kw']:.2f}\n"
            f"计算扭矩 Tca = KA × K × Kw × T = "
            f"{result['KA']:.2f} × {result['K']:.1f} × {result['Kw']:.2f} × {result['T']:.2f} "
            f"= {result['Tca']:.2f} N·m\n"
            f"标准: {result['standard']}    "
            f"联轴器类型: {result['series_name']}"
        )
        self.info_text.insert(tk.END, info)
        self.info_text.config(state=tk.DISABLED)

        # ---- 清空表格 ----
        for item in self.tree.get_children():
            self.tree.delete(item)

        self._last_results = result["results"]
        standard = result["standard"]

        if not result["results"]:
            messagebox.showinfo("选型结果", "未找到满足条件的联轴器，请调整参数后重试。")
            return

        for row_data, checks, all_ok in result["results"]:
            model, Tn, n_max, d_min, d_max = row_data[0], row_data[1], row_data[2], row_data[3], row_data[4]
            D, L, mass = row_data[5], row_data[6], row_data[8]
            ang, rad, ax = row_data[9], row_data[10], row_data[11]

            tag = "pass" if all_ok else "fail"
            status = "全部通过" if all_ok else "不满足"
            bore_range = f"{d_min:.0f}~{d_max:.0f}"

            self.tree.insert("", tk.END, values=(
                model, standard, f"{Tn:.0f}", f"{n_max:.0f}",
                bore_range, f"{D:.0f}", f"{L:.0f}",
                f"{mass:.2f}", f"{ang}", f"{rad}", f"{ax}",
                status,
            ), tags=(tag,))

    def _on_row_select(self, _event=None):
        sel = self.tree.selection()
        if not sel or not self._last_results:
            return

        idx = self.tree.index(sel[0])
        if idx >= len(self._last_results):
            return

        row_data, checks, all_ok = self._last_results[idx]

        self.detail_text.config(state=tk.NORMAL)
        self.detail_text.delete("1.0", tk.END)

        model = row_data[0]
        self.detail_text.insert(tk.END, f"型号: {model}\n")
        for name, (ok, desc) in checks.items():
            mark = "PASS" if ok else "FAIL"
            self.detail_text.insert(tk.END, f"  [{mark}] {name}: {desc}\n")

        self.detail_text.config(state=tk.DISABLED)

    def _on_clear(self):
        self.power_var.set("5.5")
        self.speed_var.set("1450")
        self.torque_var.set("")
        self.d1_var.set("30")
        self.d2_var.set("30")
        self.ang_need_var.set("0")
        self.rad_need_var.set("0")
        self.ax_need_var.set("0")
        self.load_var.set("平稳载荷")
        self.temp_var.set("≤100°C")
        self.start_var.set("启动不频繁")
        self.input_mode_var.set("按功率计算")
        self._on_mode_change()

        self.info_text.config(state=tk.NORMAL)
        self.info_text.delete("1.0", tk.END)
        self.info_text.config(state=tk.DISABLED)

        self.detail_text.config(state=tk.NORMAL)
        self.detail_text.delete("1.0", tk.END)
        self.detail_text.config(state=tk.DISABLED)

        for item in self.tree.get_children():
            self.tree.delete(item)
        self._last_results = []

    def _on_export(self):
        if not self._last_results:
            messagebox.showinfo("导出", "请先执行选型计算")
            return

        filepath = filedialog.asksaveasfilename(
            defaultextension=".txt",
            filetypes=[("文本文件", "*.txt"), ("所有文件", "*.*")],
            title="导出选型结果",
        )
        if not filepath:
            return

        try:
            with open(filepath, "w", encoding="utf-8") as f:
                f.write("=" * 60 + "\n")
                f.write("联轴器选型结果\n")
                f.write("=" * 60 + "\n\n")

                # 计算参数
                info = self.info_text.get("1.0", tk.END).strip()
                f.write("【计算参数】\n")
                f.write(info + "\n\n")

                # 结果表
                f.write("【推荐型号】\n")
                f.write(f"{'型号':<8} {'公称扭矩':>10} {'许用转速':>10} "
                        f"{'轴孔范围':<12} {'外径D':>6} {'长度L':>6} {'质量':>8} 校核\n")
                f.write("-" * 80 + "\n")

                for row_data, checks, all_ok in self._last_results:
                    model = row_data[0]
                    Tn = row_data[1]
                    n_max = row_data[2]
                    d_min, d_max = row_data[3], row_data[4]
                    D, L, mass = row_data[5], row_data[6], row_data[8]
                    status = "通过" if all_ok else "不满足"

                    f.write(
                        f"{model:<8} {Tn:>10.0f} {n_max:>10.0f} "
                        f"{d_min:.0f}~{d_max:.0f}mm{'':<4} "
                        f"{D:>6.0f} {L:>6.0f} {mass:>8.2f} {status}\n"
                    )

                f.write("\n")
                # 校核明细 (合格型号)
                f.write("【校核明细】\n")
                for row_data, checks, all_ok in self._last_results:
                    if all_ok:
                        f.write(f"\n{row_data[0]}:\n")
                        for name, (ok, desc) in checks.items():
                            mark = "PASS" if ok else "FAIL"
                            f.write(f"  [{mark}] {name}: {desc}\n")

            messagebox.showinfo("导出成功", f"结果已保存至:\n{filepath}")
        except Exception as e:
            messagebox.showerror("导出失败", str(e))


# ================================================================
# 入口
# ================================================================
if __name__ == "__main__":
    app = CouplingApp()
    app.mainloop()
