using System;
using System.Windows.Forms;

namespace SprocketDesign
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 推荐链号按钮
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
                MessageBox.Show("请输入有效的小链轮转速", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChainNumber recommended = SprocketCalculator.RecommendChain(power, n1);
            cboChainNumber.SelectedIndex = (int)recommended;

            ChainParams p = SprocketCalculator.GetChainParams(recommended);
            MessageBox.Show($"推荐使用 {GetChainName(recommended)} 链条\n" +
                            $"链节距: {p.Pitch} mm\n" +
                            $"滚子直径: {p.RollerDia} mm\n" +
                            $"极限拉伸载荷: {p.BreakingLoad} kN\n" +
                            $"每米质量: {p.MassPerMeter} kg/m",
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
                MessageBox.Show("请输入有效的小链轮转速 (rpm)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtN1.Focus();
                return;
            }
            if (!double.TryParse(txtRatio.Text, out double ratio) || ratio < 1)
            {
                MessageBox.Show("传动比必须大于等于1", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRatio.Focus();
                return;
            }
            if (!int.TryParse(txtZ1.Text, out int z1))
            {
                z1 = 0;
            }
            if (!double.TryParse(txtCenterDist.Text, out double centerDist))
            {
                centerDist = 0;
            }

            ChainNumber chainNumber = (ChainNumber)cboChainNumber.SelectedIndex;

            // 执行计算
            SprocketResult result = SprocketCalculator.Calculate(power, n1, ratio, chainNumber, z1, centerDist);

            // 显示结果
            DisplayResult(result);
        }

        /// <summary>
        /// 显示计算结果
        /// </summary>
        private void DisplayResult(SprocketResult result)
        {
            dgvResult.Rows.Clear();

            AddRow("链节距 p", result.Pitch.ToString("F3"), "mm", "");
            AddRow("小链轮齿数 Z1", result.Z1.ToString(), "",
                   result.Z1Check ? "合格" : "偏少!");
            AddRow("大链轮齿数 Z2", result.Z2.ToString(), "",
                   result.Z2Check ? "合格" : "超标!");
            AddRow("链速 v", result.ChainSpeed.ToString("F3"), "m/s",
                   result.SpeedCheck ? "合格" : "超标!");
            AddRow("初估中心距 a0", result.InitialCenterDist.ToString("F1"), "mm", "");
            AddRow("计算链节数 Lp", result.ChainLinks.ToString("F2"), "节", "");
            AddRow("圆整链节数 Lp", result.ChainLinksRounded.ToString(), "节 (偶数)", "");
            AddRow("实际中心距 a", result.ActualCenterDist.ToString("F1"), "mm", "");
            AddRow("额定功率 P0", result.RatedPower.ToString("F3"), "kW", "");
            AddRow("安全系数 S", result.SafetyFactor.ToString("F2"), "",
                   result.SafetyFactor >= 1.0 ? "安全" : "不足!");
            AddRow("磨损校核", result.WearCheck ? "通过" : "不通过", "",
                   result.WearCheck ? "合格" : "超标!");
            AddRow("推荐润滑", GetLubricationName(result.Lubrication), "", "");

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
        /// 获取链号名称
        /// </summary>
        private string GetChainName(ChainNumber chainNumber)
        {
            switch (chainNumber)
            {
                case ChainNumber._08A: return "08A";
                case ChainNumber._10A: return "10A";
                case ChainNumber._12A: return "12A";
                case ChainNumber._16A: return "16A";
                case ChainNumber._20A: return "20A";
                case ChainNumber._24A: return "24A";
                default: return "未知";
            }
        }

        /// <summary>
        /// 获取润滑方式名称
        /// </summary>
        private string GetLubricationName(LubricationType type)
        {
            switch (type)
            {
                case LubricationType.Manual: return "人工定期润滑";
                case LubricationType.Drip: return "滴油润滑";
                case LubricationType.OilBath: return "油浴润滑";
                case LubricationType.OilStream: return "喷油润滑";
                default: return "未知";
            }
        }
    }
}
