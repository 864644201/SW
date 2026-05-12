# -*- coding: utf-8 -*-
"""
减速器计算工具 V1.0
主界面模块
"""
import sys
import os
import tkinter as tk
from tkinter import ttk, messagebox

# 确保模块路径
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))

from reducer_calc import (
    calc_transmission, calc_gear_params, full_strength_check,
    estimate_form_factor, estimate_stress_correction_factor,
    get_efficiency, EFFICIENCY_TABLE, LOAD_FACTOR_TABLE,
    calc_torque_from_power,
)
from reducer_db import (
    get_material_names, get_material_by_name, select_reducer,
    get_first_series_modules, get_std_ratios,
)

# 确保数据库存在
from init_db import init_database, DB_PATH
if not os.path.exists(DB_PATH):
    init_database()


class ReducerApp(tk.Tk):
    """减速器计算工具主窗口"""

    def __init__(self):
        super().__init__()
        self.title("减速器计算工具 V1.0")
        self.geometry("960x680")
        self.minsize(900, 620)
        self.configure(bg="#f0f0f0")

        self._build_ui()

    def _build_ui(self):
        """构建主界面"""
        # 标题
        title_frame = tk.Frame(self, bg="#2c3e50", height=50)
        title_frame.pack(fill=tk.X)
        title_frame.pack_propagate(False)
        tk.Label(
            title_frame, text="减速器计算工具 V1.0",
            font=("Microsoft YaHei", 16, "bold"),
            fg="white", bg="#2c3e50"
        ).pack(expand=True)

        # 选项卡
        self.notebook = ttk.Notebook(self)
        self.notebook.pack(fill=tk.BOTH, expand=True, padx=8, pady=8)

        self._build_tab_transmission()
        self._build_tab_gear_params()
        self._build_tab_strength()
        self._build_tab_selection()

    # ═══════════════════════════════════════════════════════════
    # Tab 1: 传动计算
    # ═══════════════════════════════════════════════════════════

    def _build_tab_transmission(self):
        tab = ttk.Frame(self.notebook)
        self.notebook.add(tab, text="  传动计算  ")

        # 输入区
        input_frame = ttk.LabelFrame(tab, text="输入参数", padding=10)
        input_frame.pack(fill=tk.X, padx=10, pady=5)

        labels = ["输入功率 P (kW):", "输入转速 n₁ (RPM):",
                  "传动比 i:", "传动类型:", "工作机载荷:"]
        self.trans_entries = {}

        for idx, label in enumerate(labels):
            tk.Label(input_frame, text=label, font=("Microsoft YaHei", 10)).grid(
                row=idx, column=0, sticky=tk.W, padx=5, pady=4)

        self.trans_entries["power"] = tk.Entry(input_frame, width=20, font=("Microsoft YaHei", 10))
        self.trans_entries["power"].grid(row=0, column=1, padx=5, pady=4)
        self.trans_entries["power"].insert(0, "10")

        self.trans_entries["speed"] = tk.Entry(input_frame, width=20, font=("Microsoft YaHei", 10))
        self.trans_entries["speed"].grid(row=1, column=1, padx=5, pady=4)
        self.trans_entries["speed"].insert(0, "1450")

        self.trans_entries["ratio"] = tk.Entry(input_frame, width=20, font=("Microsoft YaHei", 10))
        self.trans_entries["ratio"].grid(row=2, column=1, padx=5, pady=4)
        self.trans_entries["ratio"].insert(0, "4.0")

        trans_types = ["单级圆柱齿轮", "两级圆柱齿轮", "三级圆柱齿轮", "行星齿轮", "蜗轮蜗杆"]
        self.trans_type_var = tk.StringVar(value=trans_types[0])
        ttk.Combobox(input_frame, textvariable=self.trans_type_var,
                     values=trans_types, state="readonly", width=18,
                     font=("Microsoft YaHei", 10)).grid(row=3, column=1, padx=5, pady=4)

        load_types = list(LOAD_FACTOR_TABLE.keys())
        self.load_type_var = tk.StringVar(value=load_types[0])
        ttk.Combobox(input_frame, textvariable=self.load_type_var,
                     values=load_types, state="readonly", width=18,
                     font=("Microsoft YaHei", 10)).grid(row=4, column=1, padx=5, pady=4)

        # 按钮
        btn_frame = tk.Frame(input_frame)
        btn_frame.grid(row=5, column=0, columnspan=2, pady=10)
        tk.Button(btn_frame, text="计  算", font=("Microsoft YaHei", 11, "bold"),
                  bg="#3498db", fg="white", width=12, command=self._calc_transmission
                  ).pack(side=tk.LEFT, padx=10)
        tk.Button(btn_frame, text="清  空", font=("Microsoft YaHei", 11),
                  width=12, command=self._clear_transmission
                  ).pack(side=tk.LEFT, padx=10)

        # 结果区
        result_frame = ttk.LabelFrame(tab, text="计算结果", padding=10)
        result_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=5)

        self.trans_result_text = tk.Text(result_frame, height=15, font=("Consolas", 11),
                                         state=tk.DISABLED, bg="#fafafa")
        scrollbar = ttk.Scrollbar(result_frame, command=self.trans_result_text.yview)
        self.trans_result_text.configure(yscrollcommand=scrollbar.set)
        self.trans_result_text.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)
        scrollbar.pack(side=tk.RIGHT, fill=tk.Y)

    def _calc_transmission(self):
        try:
            p = float(self.trans_entries["power"].get())
            n = float(self.trans_entries["speed"].get())
            i = float(self.trans_entries["ratio"].get())
            tt = self.trans_type_var.get()
            lt = self.load_type_var.get()

            if p <= 0 or n <= 0 or i <= 0:
                messagebox.showerror("错误", "参数必须为正数")
                return

            result = calc_transmission(p, n, i, tt, lt)

            text = (
                "┌─────────────────────────────────────────┐\n"
                "│           传 动 计 算 结 果              │\n"
                "├─────────────────────────────────────────┤\n"
                f"│ 输入功率 P₁      = {result['p_in']:>10.3f} kW       │\n"
                f"│ 输入转速 n₁      = {result['n_in']:>10.2f} RPM      │\n"
                f"│ 传动比 i         = {result['ratio']:>10.4f}            │\n"
                f"│ 传动类型         = {result['trans_type']:<12s}       │\n"
                f"│ 载荷类型         = {result['load_type']:<12s}       │\n"
                f"│ 载荷系数 K       = {result['load_factor']:>10.2f}            │\n"
                f"│ 效率 η           = {result['efficiency']:>10.4f}            │\n"
                "├─────────────────────────────────────────┤\n"
                f"│ 输出转速 n₂      = {result['n_out']:>10.2f} RPM      │\n"
                f"│ 输入扭矩 T₁      = {result['t_in']:>10.2f} N·m      │\n"
                f"│ 输出扭矩 T₂      = {result['t_out']:>10.2f} N·m      │\n"
                f"│ 输出功率 P₂      = {result['p_out']:>10.3f} kW       │\n"
                f"│ 功率损耗 ΔP      = {result['p_loss']:>10.3f} kW       │\n"
                "└─────────────────────────────────────────┘\n"
            )
            self._set_text(self.trans_result_text, text)
        except ValueError as e:
            messagebox.showerror("输入错误", f"请检查输入参数: {e}")

    def _clear_transmission(self):
        for e in self.trans_entries.values():
            if isinstance(e, tk.Entry):
                e.delete(0, tk.END)
        self._set_text(self.trans_result_text, "")

    # ═══════════════════════════════════════════════════════════
    # Tab 2: 齿轮参数
    # ═══════════════════════════════════════════════════════════

    def _build_tab_gear_params(self):
        tab = ttk.Frame(self.notebook)
        self.notebook.add(tab, text="  齿轮参数  ")

        input_frame = ttk.LabelFrame(tab, text="输入参数", padding=10)
        input_frame.pack(fill=tk.X, padx=10, pady=5)

        labels = [
            ("小齿轮齿数 z₁:", "20"),
            ("大齿轮齿数 z₂:", "80"),
            ("模数 m (mm):", "3"),
            ("压力角 α (°):", "20"),
            ("齿宽系数 φ_d:", "1.0"),
        ]
        self.gear_entries = {}

        for idx, (label, default) in enumerate(labels):
            tk.Label(input_frame, text=label, font=("Microsoft YaHei", 10)).grid(
                row=idx, column=0, sticky=tk.W, padx=5, pady=4)
            entry = tk.Entry(input_frame, width=20, font=("Microsoft YaHei", 10))
            entry.grid(row=idx, column=1, padx=5, pady=4)
            entry.insert(0, default)
            self.gear_entries[label] = entry

        # 模数选择辅助
        tk.Label(input_frame, text="常用模数:", font=("Microsoft YaHei", 10)).grid(
            row=5, column=0, sticky=tk.W, padx=5, pady=4)
        modules = get_first_series_modules()
        self.module_var = tk.StringVar(value="3")
        mod_combo = ttk.Combobox(input_frame, textvariable=self.module_var,
                                 values=[str(m) for m in modules], width=18,
                                 font=("Microsoft YaHei", 10))
        mod_combo.grid(row=5, column=1, padx=5, pady=4)
        mod_combo.bind("<<ComboboxSelected>>",
                       lambda e: self.gear_entries["模数 m (mm):"].delete(0, tk.END)
                       or self.gear_entries["模数 m (mm):"].insert(0, self.module_var.get()))

        btn_frame = tk.Frame(input_frame)
        btn_frame.grid(row=6, column=0, columnspan=2, pady=10)
        tk.Button(btn_frame, text="计算齿轮参数", font=("Microsoft YaHei", 11, "bold"),
                  bg="#27ae60", fg="white", width=14, command=self._calc_gear
                  ).pack(side=tk.LEFT, padx=10)
        tk.Button(btn_frame, text="清  空", font=("Microsoft YaHei", 11),
                  width=12, command=self._clear_gear
                  ).pack(side=tk.LEFT, padx=10)

        # 结果区
        result_frame = ttk.LabelFrame(tab, text="齿轮几何参数", padding=10)
        result_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=5)

        self.gear_result_text = tk.Text(result_frame, height=20, font=("Consolas", 11),
                                        state=tk.DISABLED, bg="#fafafa")
        scrollbar = ttk.Scrollbar(result_frame, command=self.gear_result_text.yview)
        self.gear_result_text.configure(yscrollcommand=scrollbar.set)
        self.gear_result_text.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)
        scrollbar.pack(side=tk.RIGHT, fill=tk.Y)

    def _calc_gear(self):
        try:
            z1 = int(self.gear_entries["小齿轮齿数 z₁:"].get())
            z2 = int(self.gear_entries["大齿轮齿数 z₂:"].get())
            m = float(self.gear_entries["模数 m (mm):"].get())
            alpha = float(self.gear_entries["压力角 α (°):"].get())
            phi_d = float(self.gear_entries["齿宽系数 φ_d:"].get())

            if z1 < 6 or z2 < 6:
                messagebox.showerror("错误", "齿数不能少于6")
                return
            if m <= 0:
                messagebox.showerror("错误", "模数必须为正数")
                return

            gp = calc_gear_params(z1, z2, m, alpha, phi_d)

            text = (
                "┌─────────────────────────────────────────┐\n"
                "│           齿 轮 几 何 参 数              │\n"
                "├─────────────────────────────────────────┤\n"
                f"│ 齿数 z₁          = {gp['z1']:>8d}              │\n"
                f"│ 齿数 z₂          = {gp['z2']:>8d}              │\n"
                f"│ 模数 m           = {gp['module']:>8.2f} mm          │\n"
                f"│ 压力角 α         = {gp['pressure_angle']:>8.2f} °           │\n"
                f"│ 齿数比 u         = {gp['u']:>8.4f}              │\n"
                f"│ 齿宽系数 φ_d     = {gp['phi_d']:>8.3f}              │\n"
                "├─────────────────────────────────────────┤\n"
                f"│ 分度圆直径 d₁    = {gp['d1']:>8.2f} mm          │\n"
                f"│ 分度圆直径 d₂    = {gp['d2']:>8.2f} mm          │\n"
                f"│ 中心距 a         = {gp['a']:>8.2f} mm          │\n"
                "├─────────────────────────────────────────┤\n"
                f"│ 齿顶高 hₐ        = {gp['ha']:>8.2f} mm          │\n"
                f"│ 齿根高 h_f       = {gp['hf']:>8.2f} mm          │\n"
                f"│ 齿全高 h         = {gp['h']:>8.2f} mm          │\n"
                f"│ 齿顶圆直径 dₐ₁   = {gp['da1']:>8.2f} mm          │\n"
                f"│ 齿顶圆直径 dₐ₂   = {gp['da2']:>8.2f} mm          │\n"
                f"│ 齿根圆直径 d_f₁  = {gp['df1']:>8.2f} mm          │\n"
                f"│ 齿根圆直径 d_f₂  = {gp['df2']:>8.2f} mm          │\n"
                f"│ 基圆直径 d_b₁    = {gp['db1']:>8.2f} mm          │\n"
                f"│ 基圆直径 d_b₂    = {gp['db2']:>8.2f} mm          │\n"
                f"│ 齿距 p           = {gp['p']:>8.3f} mm          │\n"
                f"│ 齿宽 b           = {gp['b']:>8.1f} mm          │\n"
                f"│ 重合度 ε_α       = {gp['epsilon_alpha']:>8.3f}              │\n"
                "└─────────────────────────────────────────┘\n"
            )
            self._set_text(self.gear_result_text, text)
        except ValueError as e:
            messagebox.showerror("输入错误", f"请检查输入参数: {e}")

    def _clear_gear(self):
        for e in self.gear_entries.values():
            e.delete(0, tk.END)
        self._set_text(self.gear_result_text, "")

    # ═══════════════════════════════════════════════════════════
    # Tab 3: 强度校核
    # ═══════════════════════════════════════════════════════════

    def _build_tab_strength(self):
        tab = ttk.Frame(self.notebook)
        self.notebook.add(tab, text="  强度校核  ")

        input_frame = ttk.LabelFrame(tab, text="输入参数", padding=10)
        input_frame.pack(fill=tk.X, padx=10, pady=5)

        labels_defaults = [
            ("小齿轮扭矩 T₁ (N·m):", "65.5"),
            ("模数 m (mm):", "3"),
            ("小齿轮齿数 z₁:", "20"),
            ("大齿轮齿数 z₂:", "80"),
            ("齿宽 b (mm):", "60"),
            ("载荷系数 K:", "1.4"),
            ("接触安全系数 S_H:", "1.2"),
            ("弯曲安全系数 S_F:", "1.4"),
        ]
        self.strength_entries = {}

        for idx, (label, default) in enumerate(labels_defaults):
            row = idx // 2
            col = (idx % 2) * 2
            tk.Label(input_frame, text=label, font=("Microsoft YaHei", 10)).grid(
                row=row, column=col, sticky=tk.W, padx=5, pady=4)
            entry = tk.Entry(input_frame, width=16, font=("Microsoft YaHei", 10))
            entry.grid(row=row, column=col + 1, padx=5, pady=4)
            entry.insert(0, default)
            self.strength_entries[label] = entry

        # 材料选择
        row_base = len(labels_defaults) // 2
        tk.Label(input_frame, text="齿轮材料:", font=("Microsoft YaHei", 10)).grid(
            row=row_base, column=0, sticky=tk.W, padx=5, pady=4)
        material_names = get_material_names()
        self.material_var = tk.StringVar(value=material_names[0] if material_names else "")
        ttk.Combobox(input_frame, textvariable=self.material_var,
                     values=material_names, state="readonly", width=28,
                     font=("Microsoft YaHei", 9)).grid(
            row=row_base, column=1, columnspan=3, padx=5, pady=4, sticky=tk.W)

        btn_frame = tk.Frame(input_frame)
        btn_frame.grid(row=row_base + 1, column=0, columnspan=4, pady=10)
        tk.Button(btn_frame, text="强度校核", font=("Microsoft YaHei", 11, "bold"),
                  bg="#e74c3c", fg="white", width=14, command=self._calc_strength
                  ).pack(side=tk.LEFT, padx=10)
        tk.Button(btn_frame, text="清  空", font=("Microsoft YaHei", 11),
                  width=12, command=self._clear_strength
                  ).pack(side=tk.LEFT, padx=10)

        # 结果区
        result_frame = ttk.LabelFrame(tab, text="校核结果", padding=10)
        result_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=5)

        self.strength_result_text = tk.Text(result_frame, height=18, font=("Consolas", 11),
                                            state=tk.DISABLED, bg="#fafafa")
        scrollbar = ttk.Scrollbar(result_frame, command=self.strength_result_text.yview)
        self.strength_result_text.configure(yscrollcommand=scrollbar.set)
        self.strength_result_text.pack(side=tk.LEFT, fill=tk.BOTH, expand=True)
        scrollbar.pack(side=tk.RIGHT, fill=tk.Y)

    def _calc_strength(self):
        try:
            t1 = float(self.strength_entries["小齿轮扭矩 T₁ (N·m):"].get())
            m = float(self.strength_entries["模数 m (mm):"].get())
            z1 = int(self.strength_entries["小齿轮齿数 z₁:"].get())
            z2 = int(self.strength_entries["大齿轮齿数 z₂:"].get())
            b = float(self.strength_entries["齿宽 b (mm):"].get())
            K = float(self.strength_entries["载荷系数 K:"].get())
            S_H = float(self.strength_entries["接触安全系数 S_H:"].get())
            S_F = float(self.strength_entries["弯曲安全系数 S_F:"].get())

            mat_name = self.material_var.get()
            mat = get_material_by_name(mat_name)
            if not mat:
                messagebox.showerror("错误", "请选择材料")
                return

            Y_F = estimate_form_factor(z1)
            Y_S = estimate_stress_correction_factor(z1)

            result = full_strength_check(
                t1, m, z1, z2, b, mat, K=K,
                S_H=S_H, S_F=S_F, Y_F=Y_F, Y_S=Y_S
            )

            hc = result["contact"]
            hb = result["bending"]
            overall = "合格 ✓" if result["overall_passed"] else "不合格 ✗"

            text = (
                "┌──────────────────────────────────────────────┐\n"
                "│             强 度 校 核 结 果                │\n"
                "├──────────────────────────────────────────────┤\n"
                f"│ 齿轮材料          = {mat_name:<22s}   │\n"
                f"│ σ_Hlim            = {mat['sigma_hlim']:>8.0f} MPa             │\n"
                f"│ σ_Flim            = {mat['sigma_flim']:>8.0f} MPa             │\n"
                f"│ 硬度              = {mat['hardness']:<18s}       │\n"
                "├──────────────────────────────────────────────┤\n"
                f"│ 齿形系数 Y_F      = {Y_F:>8.3f}                 │\n"
                f"│ 应力修正系数 Y_S  = {Y_S:>8.3f}                 │\n"
                "├──────────────────────────────────────────────┤\n"
                "│ 【齿面接触强度】                             │\n"
                f"│ 计算应力 σ_H      = {hc['sigma_H']:>8.2f} MPa             │\n"
                f"│ 许用应力 σ_Hp     = {hc['sigma_allow']:>8.2f} MPa             │\n"
                f"│ 安全系数 S_H      = {hc['safety_factor']:>8.2f}                 │\n"
                f"│ 实际安全系数      = {hc['actual_safety']:>8.3f}                 │\n"
                f"│ 校核结论          = {hc['conclusion']:<12s}             │\n"
                "├──────────────────────────────────────────────┤\n"
                "│ 【齿根弯曲强度】                             │\n"
                f"│ 计算应力 σ_F      = {hb['sigma_F']:>8.2f} MPa             │\n"
                f"│ 许用应力 σ_Fp     = {hb['sigma_allow']:>8.2f} MPa             │\n"
                f"│ 安全系数 S_F      = {hb['safety_factor']:>8.2f}                 │\n"
                f"│ 实际安全系数      = {hb['actual_safety']:>8.3f}                 │\n"
                f"│ 校核结论          = {hb['conclusion']:<12s}             │\n"
                "├──────────────────────────────────────────────┤\n"
                f"│ 综合结论          = {overall:<12s}             │\n"
                "└──────────────────────────────────────────────┘\n"
            )
            self._set_text(self.strength_result_text, text)
        except ValueError as e:
            messagebox.showerror("输入错误", f"请检查输入参数: {e}")

    def _clear_strength(self):
        for e in self.strength_entries.values():
            e.delete(0, tk.END)
        self._set_text(self.strength_result_text, "")

    # ═══════════════════════════════════════════════════════════
    # Tab 4: 减速器选型
    # ═══════════════════════════════════════════════════════════

    def _build_tab_selection(self):
        tab = ttk.Frame(self.notebook)
        self.notebook.add(tab, text="  减速器选型  ")

        input_frame = ttk.LabelFrame(tab, text="选型参数", padding=10)
        input_frame.pack(fill=tk.X, padx=10, pady=5)

        labels_defaults = [
            ("输入功率 P (kW):", "15"),
            ("输入转速 n₁ (RPM):", "1450"),
            ("传动比 i:", "8.0"),
        ]
        self.sel_entries = {}

        for idx, (label, default) in enumerate(labels_defaults):
            tk.Label(input_frame, text=label, font=("Microsoft YaHei", 10)).grid(
                row=idx, column=0, sticky=tk.W, padx=5, pady=4)
            entry = tk.Entry(input_frame, width=20, font=("Microsoft YaHei", 10))
            entry.grid(row=idx, column=1, padx=5, pady=4)
            entry.insert(0, default)
            self.sel_entries[label] = entry

        tk.Label(input_frame, text="减速器类型:", font=("Microsoft YaHei", 10)).grid(
            row=3, column=0, sticky=tk.W, padx=5, pady=4)
        reducer_types = ["自动选型", "单级圆柱齿轮 (ZDY)", "两级圆柱齿轮 (ZLY)",
                         "三级圆柱齿轮 (ZSY)", "行星齿轮 (NGW)", "蜗轮蜗杆 (CWU)"]
        self.reducer_type_var = tk.StringVar(value=reducer_types[0])
        ttk.Combobox(input_frame, textvariable=self.reducer_type_var,
                     values=reducer_types, state="readonly", width=28,
                     font=("Microsoft YaHei", 10)).grid(row=3, column=1, padx=5, pady=4)

        btn_frame = tk.Frame(input_frame)
        btn_frame.grid(row=4, column=0, columnspan=2, pady=10)
        tk.Button(btn_frame, text="选  型", font=("Microsoft YaHei", 11, "bold"),
                  bg="#8e44ad", fg="white", width=14, command=self._calc_selection
                  ).pack(side=tk.LEFT, padx=10)
        tk.Button(btn_frame, text="清  空", font=("Microsoft YaHei", 11),
                  width=12, command=self._clear_selection
                  ).pack(side=tk.LEFT, padx=10)

        # 结果区 - 使用 Treeview 表格
        result_frame = ttk.LabelFrame(tab, text="推荐型号", padding=10)
        result_frame.pack(fill=tk.BOTH, expand=True, padx=10, pady=5)

        columns = ("类型", "型号", "传动比", "额定功率(kW)", "额定扭矩(N·m)",
                   "许用转速(RPM)", "效率", "质量(kg)", "外形尺寸(mm)", "功率裕度(%)")
        self.sel_tree = ttk.Treeview(result_frame, columns=columns,
                                     show="headings", height=12)
        for col in columns:
            self.sel_tree.heading(col, text=col)
            self.sel_tree.column(col, width=90, anchor=tk.CENTER)

        tree_scroll_y = ttk.Scrollbar(result_frame, orient=tk.VERTICAL,
                                      command=self.sel_tree.yview)
        tree_scroll_x = ttk.Scrollbar(result_frame, orient=tk.HORIZONTAL,
                                      command=self.sel_tree.xview)
        self.sel_tree.configure(yscrollcommand=tree_scroll_y.set,
                                xscrollcommand=tree_scroll_x.set)

        self.sel_tree.pack(side=tk.TOP, fill=tk.BOTH, expand=True)
        tree_scroll_y.pack(side=tk.RIGHT, fill=tk.Y)
        tree_scroll_x.pack(side=tk.BOTTOM, fill=tk.X)

    def _calc_selection(self):
        try:
            p = float(self.sel_entries["输入功率 P (kW):"].get())
            n = float(self.sel_entries["输入转速 n₁ (RPM):"].get())
            i = float(self.sel_entries["传动比 i:"].get())

            if p <= 0 or n <= 0 or i <= 0:
                messagebox.showerror("错误", "参数必须为正数")
                return

            type_map = {
                "自动选型": "auto",
                "单级圆柱齿轮 (ZDY)": "zdy",
                "两级圆柱齿轮 (ZLY)": "zly",
                "三级圆柱齿轮 (ZSY)": "zsy",
                "行星齿轮 (NGW)": "ngw",
                "蜗轮蜗杆 (CWU)": "cwu",
            }
            rtype = type_map.get(self.reducer_type_var.get(), "auto")

            results = select_reducer(p, n, i, rtype)

            # 清空表格
            for item in self.sel_tree.get_children():
                self.sel_tree.delete(item)

            if not results:
                self.sel_tree.insert("", tk.END, values=(
                    "-", "未找到匹配型号", "-", "-", "-", "-", "-", "-", "-", "-"))
                return

            for r in results[:30]:  # 最多显示30条
                self.sel_tree.insert("", tk.END, values=(
                    r["type_name"],
                    r["model"],
                    f'{r["ratio"]:.1f}',
                    f'{r["rated_power"]:.2f}',
                    f'{r["rated_torque"]:.0f}',
                    r["max_input_speed"],
                    f'{r["efficiency"]:.4f}',
                    f'{r["mass_kg"]:.1f}',
                    r["dimensions"],
                    f'{r["power_margin"]:.1f}',
                ))
        except ValueError as e:
            messagebox.showerror("输入错误", f"请检查输入参数: {e}")

    def _clear_selection(self):
        for e in self.sel_entries.values():
            e.delete(0, tk.END)
        for item in self.sel_tree.get_children():
            self.sel_tree.delete(item)

    # ═══════════════════════════════════════════════════════════
    # 通用辅助
    # ═══════════════════════════════════════════════════════════

    @staticmethod
    def _set_text(widget, text):
        widget.configure(state=tk.NORMAL)
        widget.delete("1.0", tk.END)
        widget.insert(tk.END, text)
        widget.configure(state=tk.DISABLED)


if __name__ == "__main__":
    app = ReducerApp()
    app.mainloop()
