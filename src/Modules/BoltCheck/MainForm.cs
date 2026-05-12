using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BoltCheck
{
    public partial class MainForm : Form
    {
        private List<BoltDimension> _dimensions;
        private List<BoltGrade> _grades;

        public MainForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _dimensions = BoltTable.GetDimensions();
            _grades = BoltTable.GetGrades();

            foreach (var dim in _dimensions)
            {
                cboSpec.Items.Add(dim.DisplayName);
            }
            cboSpec.SelectedIndex = 0;

            foreach (var grade in _grades)
            {
                cboGrade.Items.Add(grade.Name);
            }
            cboGrade.SelectedIndex = 4; // default 8.8
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse inputs
                if (cboSpec.SelectedIndex < 0 || cboGrade.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择螺栓规格和材料等级", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dim = _dimensions[cboSpec.SelectedIndex];
                var grade = _grades[cboGrade.SelectedIndex];

                if (!double.TryParse(txtF.Text, out double F) || F <= 0)
                {
                    MessageBox.Show("请输入有效的轴向力", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int n = (int)numN.Value;

                if (!double.TryParse(txtK.Text, out double K) || K < 1.0 || K > 3.0)
                {
                    MessageBox.Show("预紧力系数 K 请输入 1.0~3.0 之间的值", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(txtK2.Text, out double K2) || K2 < 0)
                {
                    MessageBox.Show("请输入有效的残余预紧力系数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double K1 = cboK1.SelectedIndex == 0 ? 0.2 : 0.15;

                if (!double.TryParse(txtCbRatio.Text, out double CbRatio) || CbRatio <= 0 || CbRatio >= 1)
                {
                    MessageBox.Show("刚度比请输入 0~1 之间的值", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Run calculation
                var result = BoltCalculator.Check(dim, grade, F, n, K, K2, K1, CbRatio);

                // Display results
                dgvResult.Rows.Clear();
                AddRow("螺栓规格", result.SpecName, "-", "-");
                AddRow("材料等级", result.GradeName, "-", "-");
                AddRow("公称直径 d", result.D.ToString("F1"), "mm", "-");
                AddRow("螺距 P", result.P.ToString("F2"), "mm", "-");
                AddRow("小径 d1", result.D1.ToString("F3"), "mm", "-");
                AddRow("中径 d2", result.D2.ToString("F3"), "mm", "-");
                AddRow("应力截面积 As", result.As.ToString("F1"), "mm^2", "-");
                AddRow("抗拉强度 σb", result.SigmaB.ToString("F0"), "MPa", "-");
                AddRow("屈服强度 σs", result.SigmaS.ToString("F0"), "MPa", "-");
                AddRow("----------", "--------", "----", "----");
                AddRow("单螺栓载荷 F/n", result.FPerBolt.ToString("F3"), "kN", "-");
                AddRow("拉伸应力 σ", result.Sigma.ToString("F2"), "MPa", "σ = F/(n·As)");
                AddRow("预紧力 F0", result.F0.ToString("F3"), "kN", $"F0 = K·F/n (K={K})");
                AddRow("残余预紧力 F0'", result.F0Residual.ToString("F3"), "kN", $"F0' = K2·F/n");
                AddRow("总拉力 F2", result.F2.ToString("F3"), "kN", "F2 = F0 + Cb/(Cb+Cm)·F/n");
                AddRow("总拉伸应力 σ_total", result.SigmaTotal.ToString("F2"), "MPa", "σ = F2/As");
                AddRow("拧紧力矩 T", result.T.ToString("F2"), "N·m", $"T = K1·F0·d (K1={K1})");
                AddRow("----------", "--------", "----", "----");
                AddRow("安全系数 S", result.S.ToString("F2"), "-", "S = σs·As/(F/n)");
                AddRow("应力幅 σa", result.SigmaA.ToString("F2"), "MPa", "σa = Cb/(Cb+Cm)·Fa/As");
                AddRow("疲劳安全系数 Sf", result.SFatigue.ToString("F2"), "-", "Sf = σ(-1)/σa");

                // Update status
                if (result.IsPassed)
                {
                    lblStatus.Text = "强度校核: 通过";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblStatus.Text = "强度校核: 不通过";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }

                if (result.IsFatiguePassed)
                {
                    lblFatigueStatus.Text = "疲劳校核: 通过";
                    lblFatigueStatus.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblFatigueStatus.Text = "疲劳校核: 不通过";
                    lblFatigueStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddRow(string item, string value, string unit, string note)
        {
            dgvResult.Rows.Add(item, value, unit, note);
        }
    }
}
