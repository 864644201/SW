"""
轴承计算工具 V1.0 - 主界面
基于 tkinter 的轴承寿命计算与选型工具
"""

import os
import sys
import tkinter as tk
from tkinter import ttk, messagebox, scrolledtext

# 添加公共库路径
sys.path.insert(0, os.path.join(os.path.dirname(__file__), '..', '公共库'))

import bearing_calc
import bearing_db


class BearingCalculatorApp:
    """轴承计算工具主窗口"""

    def __init__(self, root):
        self.root = root
        self.root.title("轴承计算工具 V1.0")
        self.root.geometry("960x680")
        self.root.minsize(900, 600)

        # 初始化数据库
        bearing_db.init_check()

        self._build_ui()

    def _build_ui(self):
        """构建界面"""
        # 主框架
        main_frame = ttk.Frame(self.root, padding=10)
        main_frame.pack(fill=tk.BOTH, expand=True)

        # ===== 左侧输入面板 =====
        left_frame = ttk.LabelFrame(main_frame, text="输入参数", padding=10)
        left_frame.pack(side=tk.LEFT, fill=tk.Y, padx=(0, 5))

        # 轴承类型
        ttk.Label(left_frame, text="轴承类型:").grid(row=0, column=0, sticky=tk.W, pady=4)
        self.var_bearing_type = tk.StringVar(value="深沟球轴承")
        self.cmb_bearing_type = ttk.Combobox(
            left_frame, textvariable=self.var_bearing_type,
            values=["深沟球轴承", "角接触球轴承", "圆锥滚子轴承"],
            state="readonly", width=18
        )
        self.cmb_bearing_type.grid(row=0, column=1, sticky=tk.W, pady=4, padx=(4, 0))

        # 径向载荷 Fr
        ttk.Label(left_frame, text="径向载荷 Fr (kN):").grid(row=1, column=0, sticky=tk.W, pady=4)
        self.var_Fr = tk.StringVar(value="5.0")
        ttk.Entry(left_frame, textvariable=self.var_Fr, width=20).grid(
            row=1, column=1, sticky=tk.W, pady=4, padx=(4, 0))

        # 轴向载荷 Fa
        ttk.Label(left_frame, text="轴向载荷 Fa (kN):").grid(row=2, column=0, sticky=tk.W, pady=4)
        self.var_Fa = tk.StringVar(value="2.0")
        ttk.Entry(left_frame, textvariable=self.var_Fa, width=20).grid(
            row=2, column=1, sticky=tk.W, pady=4, padx=(4, 0))

        # 转速 n
        ttk.Label(left_frame, text="转速 n (RPM):").grid(row=3, column=0, sticky=tk.W, pady=4)
        self.var_n = tk.StringVar(value="1500")
        ttk.Entry(left_frame, textvariable=self.var_n, width=20).grid(
            row=3, column=1, sticky=tk.W, pady=4, padx=(4, 0))

        # 要求寿命
        ttk.Label(left_frame, text="要求寿命 Lh (h):").grid(row=4, column=0, sticky=tk.W, pady=4)
        self.var_Lh_req = tk.StringVar(value="10000")
        ttk.Entry(left_frame, textvariable=self.var_Lh_req, width=20).grid(
            row=4, column=1, sticky=tk.W, pady=4, padx=(4, 0))

        # 可靠度
        ttk.Label(left_frame, text="可靠度:").grid(row=5, column=0, sticky=tk.W, pady=4)
        self.var_reliability = tk.StringVar(value="0.90")
        self.cmb_reliability = ttk.Combobox(
            left_frame, textvariable=self.var_reliability,
            values=["0.90", "0.95", "0.99"],
            state="readonly", width=18
        )
        self.cmb_reliability.grid(row=5, column=1, sticky=tk.W, pady=4, padx=(4, 0))

        # 温度系数 ft
        ttk.Label(left_frame, text="温度系数 ft:").grid(row=6, column=0, sticky=tk.W, pady=4)
        self.var_ft = tk.StringVar(value="1.0")
        ttk.Entry(left_frame, textvariable=self.var_ft, width=20).grid(
            row=6, column=1, sticky=tk.W, pady=4, padx=(4, 0))

        # 选型用的轴承型号（可选）
        ttk.Label(left_frame, text="指定型号 (可选):").grid(row=7, column=0, sticky=tk.W, pady=4)
        self.var_code = tk.StringVar()
        self.cmb_code = ttk.Combobox(
            left_frame, textvariable=self.var_code,
            values=[], state="normal", width=18
        )
        self.cmb_code.grid(row=7, column=1, sticky=tk.W, pady=4, padx=(4, 0))

        # 轴承类型变更时更新型号列表
        self.cmb_bearing_type.bind("<<ComboboxSelected>>", self._on_type_changed)
        self._on_type_changed()

        # 分隔线
        ttk.Separator(left_frame, orient=tk.HORIZONTAL).grid(
            row=8, column=0, columnspan=2, sticky=tk.EW, pady=10)

        # 按钮区
        btn_frame = ttk.Frame(left_frame)
        btn_frame.grid(row=9, column=0, columnspan=2, pady=5)

        ttk.Button(btn_frame, text="计算寿命", command=self._on_calc_life, width=12).pack(pady=3)
        ttk.Button(btn_frame, text="自动选型", command=self._on_auto_select, width=12).pack(pady=3)
        ttk.Button(btn_frame, text="清空", command=self._on_clear, width=12).pack(pady=3)

        # ===== 右侧结果面板 =====
        right_frame = ttk.LabelFrame(main_frame, text="计算结果", padding=5)
        right_frame.pack(side=tk.LEFT, fill=tk.BOTH, expand=True, padx=(5, 0))

        self.txt_result = scrolledtext.ScrolledText(
            right_frame, wrap=tk.WORD, font=("Consolas", 10), state=tk.NORMAL
        )
        self.txt_result.pack(fill=tk.BOTH, expand=True)

        # ===== 底部状态栏 =====
        self.var_status = tk.StringVar(value="就绪 - 请填写参数后点击计算")
        status_bar = ttk.Label(self.root, textvariable=self.var_status, relief=tk.SUNKEN, anchor=tk.W)
        status_bar.pack(fill=tk.X, side=tk.BOTTOM, padx=5, pady=2)

    def _on_type_changed(self, event=None):
        """轴承类型变更，更新型号下拉列表"""
        btype = self.var_bearing_type.get()
        bearings = bearing_db.get_bearings_by_type(btype)
        codes = [b["bearing_code"] for b in bearings]
        self.cmb_code["values"] = ["(自动选择)"] + codes
        self.var_code.set("(自动选择)")

    def _read_inputs(self):
        """读取并校验输入参数"""
        try:
            Fr = float(self.var_Fr.get())
            Fa = float(self.var_Fa.get())
            n = float(self.var_n.get())
            Lh_req = float(self.var_Lh_req.get())
            reliability = float(self.var_reliability.get())
            ft = float(self.var_ft.get())
        except ValueError:
            messagebox.showerror("输入错误", "请确保所有数值参数填写正确！")
            return None

        if Fr < 0 or Fa < 0:
            messagebox.showerror("输入错误", "载荷不能为负数！")
            return None
        if n <= 0:
            messagebox.showerror("输入错误", "转速必须大于 0！")
            return None
        if ft <= 0:
            messagebox.showerror("输入错误", "温度系数必须大于 0！")
            return None

        return {
            "bearing_type": self.var_bearing_type.get(),
            "Fr": Fr,
            "Fa": Fa,
            "n": n,
            "Lh_required": Lh_req,
            "reliability": reliability,
            "ft": ft,
            "code": self.var_code.get(),
        }

    def _show_result(self, text):
        """在结果区域显示文本"""
        self.txt_result.delete("1.0", tk.END)
        self.txt_result.insert(tk.END, text)

    def _on_calc_life(self):
        """计算寿命按钮事件"""
        params = self._read_inputs()
        if params is None:
            return

        self.var_status.set("正在计算寿命...")

        code = params["code"]
        btype = params["bearing_type"]

        if code and code != "(自动选择)":
            bearing = bearing_db.get_bearing_by_code(code)
            if not bearing:
                messagebox.showerror("错误", f"未找到型号: {code}")
                self.var_status.set("就绪")
                return
        else:
            # 选择该类型第一个轴承作为示例
            bearings = bearing_db.get_bearings_by_type(btype)
            if not bearings:
                messagebox.showerror("错误", f"数据库中没有 {btype} 数据")
                self.var_status.set("就绪")
                return
            bearing = bearings[0]
            code = bearing["bearing_code"]

        try:
            result = bearing_calc.calculate_bearing_life(
                bearing, params["Fr"], params["Fa"], params["n"],
                params["reliability"], params["ft"]
            )

            header = self._format_input_summary(params)
            calc_text = bearing_calc.format_result(result)

            self._show_result(header + "\n" + calc_text)
            self.var_status.set(f"计算完成 - 型号: {code}, L10 = {result['Lh']:,.0f} 小时")
        except Exception as e:
            messagebox.showerror("计算错误", str(e))
            self.var_status.set("计算出错")

    def _on_auto_select(self):
        """自动选型按钮事件"""
        params = self._read_inputs()
        if params is None:
            return

        self.var_status.set("正在自动选型...")

        try:
            candidates = bearing_calc.auto_select_bearing(
                params["bearing_type"], params["Fr"], params["Fa"],
                params["n"], params["Lh_required"],
                params["reliability"], params["ft"]
            )

            if not candidates:
                messagebox.showinfo("选型结果", "未找到匹配的轴承，请调整参数。")
                self.var_status.set("选型完成 - 无匹配结果")
                return

            header = self._format_input_summary(params)
            table = bearing_calc.format_selection_table(candidates)

            self._show_result(header + "\n" + table)

            met = sum(1 for c in candidates if c["meets_requirement"])
            self.var_status.set(f"选型完成 - {len(candidates)} 个候选, {met} 个满足要求")
        except Exception as e:
            messagebox.showerror("选型错误", str(e))
            self.var_status.set("选型出错")

    def _on_clear(self):
        """清空按钮事件"""
        self.var_Fr.set("5.0")
        self.var_Fa.set("2.0")
        self.var_n.set("1500")
        self.var_Lh_req.set("10000")
        self.var_reliability.set("0.90")
        self.var_ft.set("1.0")
        self.var_bearing_type.set("深沟球轴承")
        self._on_type_changed()
        self.txt_result.delete("1.0", tk.END)
        self.var_status.set("已清空 - 就绪")

    def _format_input_summary(self, params):
        """格式化输入参数摘要"""
        lines = []
        lines.append("=" * 56)
        lines.append("  输入参数摘要")
        lines.append("=" * 56)
        lines.append(f"  轴承类型    : {params['bearing_type']}")
        lines.append(f"  径向载荷 Fr : {params['Fr']:.3f} kN")
        lines.append(f"  轴向载荷 Fa : {params['Fa']:.3f} kN")
        lines.append(f"  转速 n      : {params['n']:.0f} RPM")
        lines.append(f"  要求寿命    : {params['Lh_required']:,.0f} 小时")
        lines.append(f"  可靠度      : {params['reliability']:.0%}")
        lines.append(f"  温度系数 ft : {params['ft']:.2f}")
        if params["code"] and params["code"] != "(自动选择)":
            lines.append(f"  指定型号    : {params['code']}")
        lines.append("")
        return "\n".join(lines)


def main():
    root = tk.Tk()
    app = BearingCalculatorApp(root)
    root.mainloop()


if __name__ == "__main__":
    main()
