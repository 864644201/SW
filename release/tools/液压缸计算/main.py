# -*- coding: utf-8 -*-
"""
液压缸计算工具 V1.0
main.py - 基于tkinter的液压缸设计计算工具
"""
import sys
import os
import tkinter as tk
from tkinter import ttk, messagebox

sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))

import hydraulic_calc as hc
import hydraulic_db as db


class HydraulicCylinderApp:
    """液压缸计算工具主窗口"""

    def __init__(self, root):
        self.root = root
        self.root.title("液压缸计算工具 V1.0")
        self.root.geometry("920x700")
        self.root.minsize(820, 600)

        # 初始化数据库
        try:
            db.ensure_db()
        except Exception:
            pass

        style = ttk.Style()
        style.configure("Title.TLabel", font=("微软雅黑", 11, "bold"))
        style.configure("Result.TLabel", font=("Consolas", 10))

        self._build_ui()

    # ══════════════════════════════════════════════════════
    #  UI 构建
    # ══════════════════════════════════════════════════════

    def _build_ui(self):
        notebook = ttk.Notebook(self.root)
        notebook.pack(fill="both", expand=True, padx=5, pady=5)

        # Tab 1: 参数计算
        self.tab1 = ttk.Frame(notebook)
        notebook.add(self.tab1, text="  参数计算  ")
        self._build_tab1()

        # Tab 2: 缸筒设计
        self.tab2 = ttk.Frame(notebook)
        notebook.add(self.tab2, text="  缸筒设计  ")
        self._build_tab2()

        # Tab 3: 选型参考
        self.tab3 = ttk.Frame(notebook)
        notebook.add(self.tab3, text="  选型参考  ")
        self._build_tab3()

    # ─── Tab 1: 参数计算 ──────────────────────────────────
    def _build_tab1(self):
        # 输入区
        inp = ttk.LabelFrame(self.tab1, text="输入参数", padding=10)
        inp.pack(fill="x", padx=10, pady=5)

        self.entries = {}
        params = [
            ("所需推力 F (kN):",   "F",    "50"),
            ("工作压力 p (MPa):",  "p",    "16"),
            ("速比 φ:",            "phi",  "1.46"),
            ("流量 Q (L/min):",    "Q",    "20"),
            ("安装长度 L (mm):",   "L",    "1000"),
            ("机械效率 η_m:",      "eta",  "0.90"),
        ]

        for i, (label, key, default) in enumerate(params):
            r, c = divmod(i, 3)
            ttk.Label(inp, text=label).grid(row=r, column=c*2, sticky="e", padx=3, pady=4)
            e = ttk.Entry(inp, width=12)
            e.insert(0, default)
            e.grid(row=r, column=c*2+1, padx=3, pady=4)
            self.entries[key] = e

        # 速比下拉
        ttk.Label(inp, text="速比 φ:").grid(row=0, column=4, sticky="e", padx=3)
        self.phi_combo = ttk.Combobox(inp, values=[str(v) for v in hc.SPEED_RATIOS],
                                       width=10, state="readonly")
        self.phi_combo.set("1.46")
        self.phi_combo.grid(row=0, column=5, padx=3)
        # 隐藏原来的速比输入框
        self.entries["phi"].grid_forget()
        ttk.Label(inp, text="").grid(row=0, column=4, sticky="e")  # placeholder clear

        # 安装形式
        ttk.Label(inp, text="安装形式:").grid(row=1, column=4, sticky="e", padx=3)
        self.mount_combo = ttk.Combobox(inp, values=list(hc.MOUNT_MU.keys()),
                                         width=10, state="readonly")
        self.mount_combo.set("铰支-铰支")
        self.mount_combo.grid(row=1, column=5, padx=3)

        # 按钮
        btn_frame = ttk.Frame(inp)
        btn_frame.grid(row=2, column=4, columnspan=2, pady=5)
        ttk.Button(btn_frame, text="计  算", command=self._calc_tab1).pack(side="left", padx=5)
        ttk.Button(btn_frame, text="清  空", command=self._clear_tab1).pack(side="left", padx=5)

        # 结果区
        res = ttk.LabelFrame(self.tab1, text="计算结果", padding=10)
        res.pack(fill="both", expand=True, padx=10, pady=5)

        self.result_text = tk.Text(res, height=20, font=("Consolas", 10), wrap="word")
        scroll = ttk.Scrollbar(res, command=self.result_text.yview)
        self.result_text.configure(yscrollcommand=scroll.set)
        scroll.pack(side="right", fill="y")
        self.result_text.pack(fill="both", expand=True)

    def _calc_tab1(self):
        try:
            F = float(self.entries["F"].get())
            p = float(self.entries["p"].get())
            phi = float(self.phi_combo.get())
            Q = float(self.entries["Q"].get())
            L = float(self.entries["L"].get())
            eta = float(self.entries["eta"].get())
            mount = self.mount_combo.get()
        except ValueError:
            messagebox.showerror("输入错误", "请输入有效的数值")
            return

        r = hc.full_calculation(F, p, phi, Q, mount, L, eta)

        lines = [
            "=" * 52,
            "         液 压 缸 计 算 结 果",
            "=" * 52,
            "",
            "【缸筒内径 D】",
            f"  计算值: {r['D_calc']:.2f} mm",
            f"  标准值: {r['D_std']:.0f} mm  (GB/T 2348)",
            "",
            "【活塞杆直径 d】",
            f"  计算值: {r['d_calc']:.2f} mm",
            f"  标准值: {r['d_std']:.0f} mm  (GB/T 2348)",
            "",
            "【推力/拉力】",
            f"  推力 F1(无杆腔): {r['F1']:.2f} kN  ({r['F1']*1000:.1f} N)",
            f"  拉力 F2(有杆腔): {r['F2']:.2f} kN  ({r['F2']*1000:.1f} N)",
            f"  实际速比: {r['actual_phi']:.3f}",
            "",
            "【速度】",
            f"  前进速度 v1: {r['v1']:.2f} mm/s  ({r['v1']*60/1000:.3f} m/min)",
            f"  返回速度 v2: {r['v2']:.2f} mm/s  ({r['v2']*60/1000:.3f} m/min)",
            "",
            "【功率】",
            f"  液压功率 Ph: {r['Ph']:.3f} kW",
            "",
            "【缸筒壁厚】(45#钢 σ={:.0f} MPa)".format(r['sigma']),
            f"  Lamé公式: {r['wall_lame']:.2f} mm",
            f"  简化公式: {r['wall_simple']:.2f} mm",
            "",
            "【稳定性校核】({})".format(r['mount_type']),
            f"  临界力 F_cr: {r['F_cr']:.2f} kN",
            f"  安全系数: {r['sf']:.2f}",
            f"  结论: {'通过 ✓' if r['buckling_ok'] else '不通过 ✗ (需加大杆径或缩短行程)'}",
            "",
            "=" * 52,
        ]
        self.result_text.delete("1.0", "end")
        self.result_text.insert("1.0", "\n".join(lines))

    def _clear_tab1(self):
        for key in ("F", "p", "Q", "L", "eta"):
            self.entries[key].delete(0, "end")
        self.phi_combo.set("1.46")
        self.mount_combo.set("铰支-铰支")
        self.result_text.delete("1.0", "end")

    # ─── Tab 2: 缸筒设计 ──────────────────────────────────
    def _build_tab2(self):
        inp = ttk.LabelFrame(self.tab2, text="缸筒壁厚计算", padding=10)
        inp.pack(fill="x", padx=10, pady=5)

        self.wall_entries = {}
        wall_params = [
            ("缸筒内径 D (mm):",    "D",     "80"),
            ("工作压力 p (MPa):",   "p_w",   "16"),
            ("许用应力 σ (MPa):",   "sigma", "120"),
            ("安全系数:",            "sf",    "1.5"),
        ]
        for i, (label, key, default) in enumerate(wall_params):
            ttk.Label(inp, text=label).grid(row=i, column=0, sticky="e", padx=5, pady=4)
            e = ttk.Entry(inp, width=15)
            e.insert(0, default)
            e.grid(row=i, column=1, padx=5, pady=4)
            self.wall_entries[key] = e

        ttk.Button(inp, text="计算壁厚", command=self._calc_wall).grid(
            row=0, column=2, rowspan=2, padx=10)
        ttk.Button(inp, text="清空", command=self._clear_wall).grid(
            row=2, column=2, rowspan=2, padx=10)

        # 标准内径参考
        ttk.Label(inp, text="标准内径参考:").grid(row=4, column=0, sticky="ne", padx=5, pady=4)
        bore_list = ", ".join(str(int(b)) for b in hc.STD_BORES)
        bore_lbl = ttk.Label(inp, text=bore_list, wraplength=500, font=("Consolas", 9))
        bore_lbl.grid(row=4, column=1, columnspan=2, sticky="w", padx=5)

        # 结果
        res = ttk.LabelFrame(self.tab2, text="计算结果", padding=10)
        res.pack(fill="both", expand=True, padx=10, pady=5)
        self.wall_result = tk.Text(res, height=12, font=("Consolas", 10), wrap="word")
        self.wall_result.pack(fill="both", expand=True)

    def _calc_wall(self):
        try:
            D = float(self.wall_entries["D"].get())
            p = float(self.wall_entries["p_w"].get())
            sigma = float(self.wall_entries["sigma"].get())
            sf = float(self.wall_entries["sf"].get())
        except ValueError:
            messagebox.showerror("输入错误", "请输入有效的数值")
            return

        lame = hc.calc_wall_lame(D, p, sigma)
        simple = hc.calc_wall_simplified(D, p, sigma, sf)

        # 标准壁厚推荐
        std_walls = [2, 2.5, 3, 3.5, 4, 4.5, 5, 6, 7, 8, 10, 12, 14, 16, 18, 20, 25]
        std_lame = next((w for w in std_walls if w >= lame), std_walls[-1])
        std_simple = next((w for w in std_walls if w >= simple), std_walls[-1])

        lines = [
            "=" * 45,
            "         缸 筒 壁 厚 计 算 结 果",
            "=" * 45,
            f"  缸筒内径 D = {D:.0f} mm",
            f"  工作压力 p = {p:.1f} MPa",
            f"  许用应力 σ = {sigma:.0f} MPa",
            "",
            "【Lamé厚壁公式】",
            f"  δ = D/2 × ((σ+0.4p)/(σ-1.3p) - 1)",
            f"  δ = {lame:.2f} mm",
            f"  推荐标准壁厚: {std_lame} mm",
            "",
            "【简化公式】",
            f"  δ ≥ p×D/(2σ-p) × {sf}",
            f"  δ ≥ {simple:.2f} mm",
            f"  推荐标准壁厚: {std_simple} mm",
            "",
            f"  综合推荐壁厚: {max(std_lame, std_simple)} mm",
            "=" * 45,
        ]
        self.wall_result.delete("1.0", "end")
        self.wall_result.insert("1.0", "\n".join(lines))

    def _clear_wall(self):
        for e in self.wall_entries.values():
            e.delete(0, "end")
        self.wall_result.delete("1.0", "end")

    # ─── Tab 3: 选型参考 ──────────────────────────────────
    def _build_tab3(self):
        inp = ttk.LabelFrame(self.tab3, text="HSG系列选型", padding=10)
        inp.pack(fill="x", padx=10, pady=5)

        ttk.Label(inp, text="所需推力 (kN):").grid(row=0, column=0, sticky="e", padx=5)
        self.sel_force = ttk.Entry(inp, width=12)
        self.sel_force.insert(0, "50")
        self.sel_force.grid(row=0, column=1, padx=5)

        ttk.Label(inp, text="工作压力 (MPa):").grid(row=0, column=2, sticky="e", padx=5)
        self.sel_pressure = ttk.Entry(inp, width=12)
        self.sel_pressure.insert(0, "16")
        self.sel_pressure.grid(row=0, column=3, padx=5)

        ttk.Button(inp, text="查询", command=self._query_hsg).grid(row=0, column=4, padx=10)
        ttk.Button(inp, text="显示全部", command=self._show_all_hsg).grid(row=0, column=5, padx=5)

        # 结果表格
        res = ttk.LabelFrame(self.tab3, text="HSG系列液压缸", padding=5)
        res.pack(fill="both", expand=True, padx=10, pady=5)

        cols = ("型号", "缸径D", "杆径d", "最大行程", "最大压力",
                "推力@16MPa", "拉力@16MPa", "最小安装长", "每100mm质量")
        self.tree = ttk.Treeview(res, columns=cols, show="headings", height=12)
        widths = [130, 55, 55, 70, 65, 80, 80, 80, 85]
        for c, w in zip(cols, widths):
            self.tree.heading(c, text=c)
            self.tree.column(c, width=w, anchor="center")

        vsb = ttk.Scrollbar(res, orient="vertical", command=self.tree.yview)
        self.tree.configure(yscrollcommand=vsb.set)
        vsb.pack(side="right", fill="y")
        self.tree.pack(fill="both", expand=True)

        # 密封件查询区
        seal_frame = ttk.LabelFrame(self.tab3, text="密封件推荐", padding=5)
        seal_frame.pack(fill="x", padx=10, pady=5)

        ttk.Label(seal_frame, text="缸径 D (mm):").pack(side="left", padx=5)
        self.seal_bore = ttk.Entry(seal_frame, width=8)
        self.seal_bore.pack(side="left", padx=3)

        ttk.Label(seal_frame, text="杆径 d (mm):").pack(side="left", padx=5)
        self.seal_rod = ttk.Entry(seal_frame, width=8)
        self.seal_rod.pack(side="left", padx=3)

        ttk.Button(seal_frame, text="查询密封件", command=self._query_seals).pack(side="left", padx=10)

        self.seal_result = tk.Text(seal_frame, height=4, font=("Consolas", 9), wrap="word")
        self.seal_result.pack(fill="x", pady=5)

    def _query_hsg(self):
        try:
            force = float(self.sel_force.get())
            pressure = float(self.sel_pressure.get())
        except ValueError:
            messagebox.showerror("输入错误", "请输入有效的数值")
            return
        rows = db.query_hsg(pressure_mpa=pressure, force_kn=force)
        self._fill_hsg_table(rows)

    def _show_all_hsg(self):
        rows = db.get_all_hsg()
        self._fill_hsg_table(rows)

    def _fill_hsg_table(self, rows):
        for item in self.tree.get_children():
            self.tree.delete(item)
        if not rows:
            self.tree.insert("", "end", values=("无匹配型号", "", "", "", "", "", "", "", ""))
            return
        for r in rows:
            self.tree.insert("", "end", values=(
                r["model"],
                f"{r['bore_dia_D']:.0f}",
                f"{r['rod_dia_d']:.0f}",
                f"{r['max_stroke']:.0f}",
                f"{r['max_pressure']:.0f}",
                f"{r['push_force_16MPa']:.1f}",
                f"{r['pull_force_16MPa']:.1f}",
                f"{r['min_install_length']:.0f}",
                f"{r['mass_per_100mm']:.1f}",
            ))

    def _query_seals(self):
        try:
            bore = float(self.seal_bore.get())
            rod = float(self.seal_rod.get())
        except ValueError:
            messagebox.showerror("输入错误", "请输入有效的缸径和杆径")
            return

        rows = db.query_seals(bore, rod)
        self.seal_result.delete("1.0", "end")
        if not rows:
            self.seal_result.insert("1.0", "未找到匹配的密封件")
            return
        lines = ["推荐密封件:"]
        for r in rows:
            lines.append(f"  {r['seal_type']} | {r['material']} | "
                         f"耐压≤{r['pressure_max']}MPa | 温度{r['temp_range']}")
        self.seal_result.insert("1.0", "\n".join(lines))


def main():
    root = tk.Tk()
    app = HydraulicCylinderApp(root)
    root.mainloop()


if __name__ == "__main__":
    main()
