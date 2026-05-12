using System;
using System.Windows.Forms;

namespace PulleyDesign
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 推荐带型按钮
        /// </summary>
        private void btnRecommend_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(txtPower.Text, out double power) || power <= 0)
            {
                MessageBox.Show("请输入有效的传递功率", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!double.TryParse(txtN1.Text, out double n1) || n1 <= 0)
            {
                MessageBox.Show("请输入有效的小带轮转速", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BeltType recommended = PulleyCalculator.RecommendBeltType(power, n1);
            cboBeltType.SelectedIndex = (int)recommended;

            // 同时推荐小带轮直径
            VBeltParams p = PulleyCalculator.GetVBeltParams(recommended);
            txtD1.Text = p.MinPulleyDia.ToString("F0");

            MessageBox.Show($"推荐使用 {GetBeltTypeName(recommended)} 型V带\n" +
                            $"最小带轮直径: {p.MinPulleyDia} mm\n" +
                            $"单位质量: {p.UnitMass} kg/m",
                            "推荐结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 计算按钮
        /// </summary>
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // 输入验证
            if (!double.TryParse(txtPower.Text, out double power) || power <= 0)
            {
                MessageBox.Show("请输入有效的传递功率 (kW)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPower.Focus();
                return;
            }
            if (!double.TryParse(txtN1.Text, out double n1) || n1 <= 0)
            {
                MessageBox.Show("请输入有效的小带轮转速 (rpm)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtN1.Focus();
                return;
            }
            if (!double.TryParse(txtRatio.Text, out double ratio) || ratio <= 1)
            {
                MessageBox.Show("传动比必须大于1", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRatio.Focus();
                return;
            }
            if (!double.TryParse(txtD1.Text, out double d1) || d1 <= 0)
            {
                MessageBox.Show("请输入有效的小带轮直径 (mm)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtD1.Focus();
                return;
            }
            if (!double.TryParse(txtCenterDist.Text, out double centerDist))
            {
                centerDist = 0;
            }

            BeltType beltType = (BeltType)cboBeltType.SelectedIndex;

            // 执行计算
            PulleyResult result = PulleyCalculator.Calculate(power, n1, ratio, d1, beltType, centerDist);

            // 显示结果
            DisplayResult(result);
        }

        /// <summary>
        /// 显示计算结果
        /// </summary>
        private void DisplayResult(PulleyResult result)
        {
            dgvResult.Rows.Clear();

            AddRow("大带轮直径 d2", result.D2.ToString("F2"), "mm", "");
            AddRow("带速 v", result.BeltSpeed.ToString("F3"), "m/s",
                   result.SpeedCheck ? "合格" : "超标!");
            AddRow("初估中心距 a0", result.InitialCenterDist.ToString("F1"), "mm", "");
            AddRow("初算带长 L0", result.BeltLengthL0.ToString("F1"), "mm", "");
            AddRow("标准带长 Ld", result.StandardBeltLength.ToString("F0"), "mm", "");
            AddRow("实际中心距 a", result.ActualCenterDist.ToString("F1"), "mm", "");
            AddRow("小带轮包角 alpha1", result.WrapAngle1.ToString("F2"), "deg",
                   result.WrapAngleCheck ? "合格" : "偏小!");
            AddRow("大带轮包角 alpha2", result.WrapAngle2.ToString("F2"), "deg", "");
            AddRow("V带根数 z", result.BeltCount.ToString(), "根", "");
            AddRow("预紧力 F0", result.PreTension.ToString("F2"), "N", "");
            AddRow("轴压力 Fr", result.ShaftForce.ToString("F2"), "N", "");

            txtRemarks.Text = result.Remarks;
        }

        /// <summary>
        /// 添加结果行
        /// </summary>
        private void AddRow(string param, string value, string unit, string check)
        {
            dgvResult.Rows.Add(param, value, unit, check);
        }

        /// <summary>
        /// 获取带类型名称
        /// </summary>
        private string GetBeltTypeName(BeltType type)
        {
            switch (type)
            {
                case BeltType.V_A: return "A";
                case BeltType.V_B: return "B";
                case BeltType.V_C: return "C";
                case BeltType.V_D: return "D";
                case BeltType.V_E: return "E";
                case BeltType.Flat: return "平带";
                case BeltType.Timing: return "同步带";
                default: return "未知";
            }
        }
    }
}
