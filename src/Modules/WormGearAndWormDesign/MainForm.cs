using System;
using System.Windows.Forms;

namespace WormGearAndWormDesign
{
    internal partial class MainForm : Form
    {
        private WormGearCalculator calculator;

        public MainForm()
        {
            InitializeComponent();
            calculator = new WormGearCalculator();
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            // 传递功率默认值
            txtP.Text = "5.5";
            // 蜗杆转速默认值
            txtN1.Text = "1440";
            // 传动比默认值
            txtI.Text = "20";
            // 蜗杆头数默认值
            cmbZ1.SelectedIndex = 0; // z1=1
            // 蜗轮齿数 (自动计算)
            txtZ2.Text = "";
            // 模数默认值
            cmbM.SelectedIndex = 3; // m=4
            // 蜗杆直径系数默认值
            cmbQ.SelectedIndex = 2; // q=10
            // 蜗杆材料
            cmbWormMaterial.SelectedIndex = 0;
            // 蜗轮材料
            cmbWormGearMaterial.SelectedIndex = 0;

            // 高级参数默认值
            txtKa.Text = "1.0";
            txtZe.Text = "155";
            txtSigmaH.Text = "200";
            txtSigmaF.Text = "56";
            txtYFa2.Text = "2.4";
            txtYBeta.Text = "1.0";
            txtK.Text = "1.0";
            txtYAllow.Text = "0.05";

            // 热平衡参数
            txtKHeat.Text = "12";
            txtAHeat.Text = "0.5";
            txtT1Oil.Text = "90";
            txtT0Env.Text = "20";

            // 材料参数
            txtE.Text = "206000";
            txtNu.Text = "0.3";
            txtF.Text = "0.04";
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // 读取输入参数
                if (!double.TryParse(txtP.Text, out double P) || P <= 0)
                {
                    MessageBox.Show("请输入有效的传递功率 P (kW)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtP.Focus();
                    return;
                }

                if (!double.TryParse(txtN1.Text, out double N1) || N1 <= 0)
                {
                    MessageBox.Show("请输入有效的蜗杆转速 n1 (rpm)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtN1.Focus();
                    return;
                }

                if (!double.TryParse(txtI.Text, out double I) || I <= 0)
                {
                    MessageBox.Show("请输入有效的传动比 i", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtI.Focus();
                    return;
                }

                if (!int.TryParse(cmbZ1.Text, out int Z1) || Z1 < 1 || Z1 > 4)
                {
                    MessageBox.Show("蜗杆头数 z1 应为 1~4", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbZ1.Focus();
                    return;
                }

                // 蜗轮齿数: 可自动计算或手动输入
                int Z2 = 0;
                if (!string.IsNullOrEmpty(txtZ2.Text))
                {
                    if (!int.TryParse(txtZ2.Text, out Z2) || Z2 <= 0)
                    {
                        MessageBox.Show("请输入有效的蜗轮齿数 z2", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtZ2.Focus();
                        return;
                    }
                }

                if (!double.TryParse(cmbM.Text, out double M) || M <= 0)
                {
                    MessageBox.Show("请输入有效的模数 m (mm)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbM.Focus();
                    return;
                }

                if (!double.TryParse(cmbQ.Text, out double Q) || Q <= 0)
                {
                    MessageBox.Show("请输入有效的蜗杆直径系数 q", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbQ.Focus();
                    return;
                }

                // 读取高级参数
                if (!double.TryParse(txtKa.Text, out double Ka)) Ka = 1.0;
                if (!double.TryParse(txtZe.Text, out double Ze)) Ze = 155;
                if (!double.TryParse(txtSigmaH.Text, out double SigmaH)) SigmaH = 200;
                if (!double.TryParse(txtSigmaF.Text, out double SigmaF)) SigmaF = 56;
                if (!double.TryParse(txtYFa2.Text, out double YFa2)) YFa2 = 2.4;
                if (!double.TryParse(txtYBeta.Text, out double YBeta)) YBeta = 1.0;
                if (!double.TryParse(txtK.Text, out double K_Load)) K_Load = 1.0;
                if (!double.TryParse(txtYAllow.Text, out double YAllow)) YAllow = 0.05;
                if (!double.TryParse(txtKHeat.Text, out double KHeat)) KHeat = 12;
                if (!double.TryParse(txtAHeat.Text, out double AHeat)) AHeat = 0.5;
                if (!double.TryParse(txtT1Oil.Text, out double T1Oil)) T1Oil = 90;
                if (!double.TryParse(txtT0Env.Text, out double T0Env)) T0Env = 20;
                if (!double.TryParse(txtE.Text, out double E)) E = 206000;
                if (!double.TryParse(txtNu.Text, out double Nu)) Nu = 0.3;
                if (!double.TryParse(txtF.Text, out double F)) F = 0.04;

                // 设置计算器参数
                calculator.P = P;
                calculator.N1 = N1;
                calculator.I = I;
                calculator.Z1 = Z1;
                calculator.Z2 = Z2;
                calculator.M = M;
                calculator.Q = Q;
                calculator.Ka = Ka;
                calculator.Ze = Ze;
                calculator.SigmaH_Allow = SigmaH;
                calculator.SigmaF_Allow = SigmaF;
                calculator.YFa2 = YFa2;
                calculator.YBeta = YBeta;
                calculator.K_Load = K_Load;
                calculator.Y_Allow = YAllow;
                calculator.K_Heat = KHeat;
                calculator.A_Heat = AHeat;
                calculator.T1_Oil = T1Oil;
                calculator.T0_Env = T0Env;
                calculator.E = E;
                calculator.Nu = Nu;
                calculator.F = F;

                // 执行计算
                calculator.Calculate();

                // 显示结果
                txtResult.Text = calculator.GetResultSummary();

                // 自动填写蜗轮齿数
                if (Z2 <= 0)
                    txtZ2.Text = calculator.Z2.ToString();

                // 更新结果显示
                UpdateResultFields();

                // 更新校核状态
                UpdateCheckStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算过程发生错误：\n" + ex.Message, "计算错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateResultFields()
        {
            // 几何参数
            txtD1.Text = calculator.D1.ToString("F4");
            txtD2.Text = calculator.D2.ToString("F4");
            txtA.Text = calculator.A.ToString("F4");
            txtGamma.Text = calculator.GammaDeg.ToString("F4");
            txtPx.Text = calculator.Px.ToString("F4");
            txtL.Text = calculator.L.ToString("F4");
            txtDa1.Text = calculator.Da1.ToString("F4");
            txtDf1.Text = calculator.Df1.ToString("F4");
            txtDa2.Text = calculator.Da2.ToString("F4");
            txtDf2.Text = calculator.Df2.ToString("F4");
            txtB1.Text = calculator.B1.ToString("F4");
            txtB2.Text = calculator.B2.ToString("F4");

            // 运动学参数
            txtN2.Text = calculator.N2.ToString("F4");
            txtV1.Text = calculator.V1.ToString("F4");
            txtVs.Text = calculator.Vs.ToString("F4");
            txtV2.Text = calculator.V2.ToString("F4");
            txtEta.Text = calculator.Eta.ToString("F4");

            // 力的分析
            txtT1.Text = calculator.T1.ToString("F4");
            txtT2.Text = calculator.T2.ToString("F4");
            txtFt1.Text = calculator.Ft1.ToString("F4");
            txtFa1.Text = calculator.Fa1.ToString("F4");
            txtFr1.Text = calculator.Fr1.ToString("F4");
            txtFt2.Text = calculator.Ft2.ToString("F4");
            txtFa2.Text = calculator.Fa2.ToString("F4");
            txtFr2.Text = calculator.Fr2.ToString("F4");

            // 强度校核
            txtSigmaH_Calc.Text = calculator.SigmaH.ToString("F4");
            txtSigmaF_Calc.Text = calculator.SigmaF.ToString("F4");

            // 热平衡
            txtQr.Text = calculator.Qr.ToString("F4");
            txtQc.Text = calculator.Qc.ToString("F4");

            // 刚度
            txtDeflection.Text = calculator.Deflection.ToString("F6");
        }

        private void UpdateCheckStatus()
        {
            // 接触强度
            lblContactStatus.Text = calculator.ContactOK ? "满足" : "不满足";
            lblContactStatus.ForeColor = calculator.ContactOK ? System.Drawing.Color.Green : System.Drawing.Color.Red;

            // 弯曲强度
            lblBendingStatus.Text = calculator.BendingOK ? "满足" : "不满足";
            lblBendingStatus.ForeColor = calculator.BendingOK ? System.Drawing.Color.Green : System.Drawing.Color.Red;

            // 热平衡
            lblHeatStatus.Text = calculator.HeatBalanceOK ? "满足" : "不满足";
            lblHeatStatus.ForeColor = calculator.HeatBalanceOK ? System.Drawing.Color.Green : System.Drawing.Color.Red;

            // 刚度
            lblStiffnessStatus.Text = calculator.StiffnessOK ? "满足" : "不满足";
            lblStiffnessStatus.ForeColor = calculator.StiffnessOK ? System.Drawing.Color.Green : System.Drawing.Color.Red;

            // 总体判定
            bool allOK = calculator.ContactOK && calculator.BendingOK &&
                         calculator.HeatBalanceOK && calculator.StiffnessOK;
            lblOverall.Text = allOK ? "全部校核通过" : "存在不满足项，请调整参数";
            lblOverall.ForeColor = allOK ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            txtP.Focus();
        }

        private void cmbZ1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 蜗杆头数改变时，自动计算蜗轮齿数
            if (int.TryParse(cmbZ1.Text, out int Z1) && double.TryParse(txtI.Text, out double I))
            {
                txtZ2.Text = ((int)Math.Round(I * Z1)).ToString();
            }
        }

        private void txtI_TextChanged(object sender, EventArgs e)
        {
            // 传动比改变时，自动计算蜗轮齿数
            if (int.TryParse(cmbZ1.Text, out int Z1) && double.TryParse(txtI.Text, out double I))
            {
                txtZ2.Text = ((int)Math.Round(I * Z1)).ToString();
            }
        }
    }
}
