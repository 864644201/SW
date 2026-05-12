using System;
using System.Windows.Forms;

namespace Leadscrew
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UpdateCaVisibility();
        }

        private void cboScrewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCaVisibility();
        }

        private void UpdateCaVisibility()
        {
            bool isBallScrew = cboScrewType.SelectedIndex == 2;
            lblCa.Visible = isBallScrew;
            txtCa.Visible = isBallScrew;
            lblCaUnit.Visible = isBallScrew;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                // Parse inputs
                ScrewType screwType;
                switch (cboScrewType.SelectedIndex)
                {
                    case 0: screwType = ScrewType.Trapezoidal; break;
                    case 1: screwType = ScrewType.Buttress; break;
                    case 2: screwType = ScrewType.BallScrew; break;
                    default:
                        MessageBox.Show("请选择丝杠类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                SupportType supportType;
                switch (cboSupport.SelectedIndex)
                {
                    case 0: supportType = SupportType.FixedFree; break;
                    case 1: supportType = SupportType.FixedPinned; break;
                    case 2: supportType = SupportType.PinnedPinned; break;
                    case 3: supportType = SupportType.FixedFixed; break;
                    default:
                        MessageBox.Show("请选择支撑方式", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                if (!double.TryParse(txtD.Text, out double d) || d <= 0)
                {
                    MessageBox.Show("请输入有效的公称直径", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(txtPh.Text, out double Ph) || Ph <= 0)
                {
                    MessageBox.Show("请输入有效的导程", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(txtF.Text, out double F) || F <= 0)
                {
                    MessageBox.Show("请输入有效的轴向载荷", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(txtRPM.Text, out double rpm) || rpm <= 0)
                {
                    MessageBox.Show("请输入有效的转速", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!double.TryParse(txtL.Text, out double L) || L <= 0)
                {
                    MessageBox.Show("请输入有效的丝杠长度", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double Ca = 0;
                if (screwType == ScrewType.BallScrew)
                {
                    if (!double.TryParse(txtCa.Text, out Ca) || Ca <= 0)
                    {
                        MessageBox.Show("请输入有效的额定动载荷", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Run calculation
                var result = LeadscrewCalculator.Calculate(screwType, supportType, d, Ph, F, rpm, L, Ca);

                // Display results
                dgvResult.Rows.Clear();

                string typeName;
                switch (result.ScrewType)
                {
                    case ScrewType.Trapezoidal: typeName = "梯形螺纹"; break;
                    case ScrewType.Buttress:    typeName = "锯齿形螺纹"; break;
                    case ScrewType.BallScrew:   typeName = "滚珠丝杠"; break;
                    default: typeName = "-"; break;
                }

                AddRow("丝杠类型", typeName, "-", "-");
                AddRow("公称直径 d", result.D.ToString("F1"), "mm", "-");
                AddRow("导程 Ph", result.Ph.ToString("F1"), "mm", "-");
                AddRow("中径 d2", result.D2.ToString("F3"), "mm", "-");
                AddRow("小径 d3", result.D3.ToString("F3"), "mm", "-");
                AddRow("截面积 As", result.As.ToString("F1", System.Globalization.CultureInfo.InvariantCulture), "mm^2", "-");
                AddRow("惯性矩 I", result.I.ToString("F1", System.Globalization.CultureInfo.InvariantCulture), "mm^4", "-");
                AddRow("--------", "--------", "----", "----");
                AddRow("导程角 lambda", result.Lambda.ToString("F3"), "deg", "arctan(Ph/(pi*d2))");
                AddRow("当量摩擦角 rho", result.Rho.ToString("F3"), "deg", "-");
                AddRow("传动效率 eta", result.Eta.ToString("F4"), "-", "tan(lam)/tan(lam+rho)");
                AddRow("驱动扭矩 T", result.T.ToString("F2"), "N.mm", "F*Ph/(2*pi*eta)");
                AddRow("--------", "--------", "----", "----");
                AddRow("拉伸应力 sigma", result.Sigma.ToString("F2"), "MPa", "F/As");
                AddRow("扭转剪应力 tau", result.Tau.ToString("F2"), "MPa", "16*T/(pi*d3^3)");
                AddRow("复合应力 sigma_v", result.SigmaV.ToString("F2"), "MPa", "sqrt(s^2+3*t^2)");
                AddRow("强度安全系数", result.SStrength.ToString("F2"), "-", "sigma_s/sigma_v");
                AddRow("--------", "--------", "----", "----");
                AddRow("临界转速 nc", result.Nc.ToString("F0"), "rpm", "-");
                AddRow("压杆临界载荷 Fcr", result.Fcr.ToString("F0"), "N", "pi^2*E*I/(mu*L)^2");
                AddRow("稳定性安全系数", result.SStability.ToString("F2"), "-", "Fcr/F");

                if (result.ScrewType == ScrewType.BallScrew)
                {
                    AddRow("--------", "--------", "----", "----");
                    AddRow("额定动载荷 Ca", result.Ca.ToString("F0"), "N", "-");
                    AddRow("额定寿命 L10", result.L10.ToString("F2"), "x10^6 rev", "(Ca/Fa)^3");
                    AddRow("寿命 L10h", result.L10h.ToString("F0"), "h", "-");
                }

                // Update status labels
                SetStatus(lblStrength, "强度", result.IsStrengthOk);
                SetStatus(lblStability, "稳定性", result.IsStable);
                SetStatus(lblSpeed, "转速", result.IsSpeedOk);

                if (result.ScrewType == ScrewType.BallScrew)
                {
                    SetStatus(lblLife, "寿命", result.IsLifeOk);
                }
                else
                {
                    lblLife.Text = "寿命: 滚珠丝杠专用";
                    lblLife.ForeColor = System.Drawing.Color.Gray;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetStatus(Label lbl, string prefix, bool ok)
        {
            if (ok)
            {
                lbl.Text = prefix + ": 通过";
                lbl.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lbl.Text = prefix + ": 不通过";
                lbl.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void AddRow(string item, string value, string unit, string note)
        {
            dgvResult.Rows.Add(item, value, unit, note);
        }
    }
}
