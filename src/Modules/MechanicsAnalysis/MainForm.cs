using System;
using System.Windows.Forms;

namespace MDSolids
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadMaterialList();
        }

        private void LoadMaterialList()
        {
            cmbMaterial.Items.Clear();
            foreach (var mat in MaterialDatabase.Materials)
                cmbMaterial.Items.Add(mat);
            if (cmbMaterial.Items.Count > 0)
                cmbMaterial.SelectedIndex = 0;
        }

        private void cmbMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            var mat = cmbMaterial.SelectedItem as MaterialProperty;
            if (mat == null) return;
            txtSigmaYield.Text = mat.SigmaS.ToString("F0");
            txtSigmaB.Text = mat.SigmaB.ToString("F0");
            txtElasticModulus.Text = mat.E.ToString("F0");
        }

        #region 弯曲应力计算

        private void btnCalcBending_Click(object sender, EventArgs e)
        {
            try
            {
                double M = double.Parse(txtBendingMoment.Text);
                double I, c;

                // 根据截面类型自动计算惯性矩
                if (rbRectSection.Checked)
                {
                    double b = double.Parse(txtRectB.Text);
                    double h = double.Parse(txtRectH.Text);
                    I = StressCalculator.CalcMomentOfInertia_Rect(b, h);
                    c = h / 2.0;
                    txtSectionInertia.Text = I.ToString("F2");
                }
                else // 圆形截面
                {
                    double d = double.Parse(txtCircleD.Text);
                    I = StressCalculator.CalcMomentOfInertia_Circle(d);
                    c = d / 2.0;
                    txtSectionInertia.Text = I.ToString("F2");
                }

                double sigma = StressCalculator.CalcBendingStress(M, I, c);
                txtBendingResult.Text = sigma.ToString("F4");
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入参数错误: " + ex.Message, "计算错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void rbRectSection_CheckedChanged(object sender, EventArgs e)
        {
            pnlRect.Enabled = rbRectSection.Checked;
            pnlCircle.Enabled = !rbRectSection.Checked;
        }

        private void rbCircleSection_CheckedChanged(object sender, EventArgs e)
        {
            pnlRect.Enabled = !rbCircleSection.Checked;
            pnlCircle.Enabled = rbCircleSection.Checked;
        }

        #endregion

        #region 扭转应力计算

        private void btnCalcTorsion_Click(object sender, EventArgs e)
        {
            try
            {
                double T = double.Parse(txtTorque.Text);
                double d = double.Parse(txtTorsionD.Text);
                double r = d / 2.0;
                double J = StressCalculator.CalcPolarMoment_Circle(d);

                txtPolarInertia.Text = J.ToString("F2");
                double tau = StressCalculator.CalcTorsionStress(T, J, r);
                txtTorsionResult.Text = tau.ToString("F4");
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入参数错误: " + ex.Message, "计算错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region 组合应力 (Von Mises)

        private void btnCalcVonMises_Click(object sender, EventArgs e)
        {
            try
            {
                double sigma = double.Parse(txtNormalStress.Text);
                double tau = double.Parse(txtShearStress.Text);
                double sigma_vm = StressCalculator.CalcVonMises(sigma, tau);
                txtVonMisesResult.Text = sigma_vm.ToString("F4");

                // 同时计算安全系数
                var mat = cmbMaterial.SelectedItem as MaterialProperty;
                if (mat != null && mat.SigmaS > 0)
                {
                    double n = StressCalculator.CalcSafetyFactor(mat.SigmaS, sigma_vm);
                    txtSafetyFactor.Text = n.ToString("F4");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入参数错误: " + ex.Message, "计算错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region 挠度计算

        private void btnCalcDeflection_Click(object sender, EventArgs e)
        {
            try
            {
                double E = double.Parse(txtDeflectionE.Text);
                double I = double.Parse(txtDeflectionI.Text);
                double delta;

                if (rbCantilever.Checked)
                {
                    double F = double.Parse(txtDeflectionF.Text);
                    double L = double.Parse(txtDeflectionL.Text);
                    delta = StressCalculator.CalcDeflectionCantilever(F, L, E, I);
                }
                else // 简支梁均布载荷
                {
                    double w = double.Parse(txtDistributedLoad.Text);
                    double L = double.Parse(txtDeflectionL.Text);
                    delta = StressCalculator.CalcDeflectionSimplySupported(w, L, E, I);
                }

                txtDeflectionResult.Text = delta.ToString("F6");
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入参数错误: " + ex.Message, "计算错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void rbCantilever_CheckedChanged(object sender, EventArgs e)
        {
            pnlPointLoad.Enabled = rbCantilever.Checked;
            pnlDistLoad.Enabled = !rbCantilever.Checked;
        }

        private void rbDistributed_CheckedChanged(object sender, EventArgs e)
        {
            pnlPointLoad.Enabled = !rbDistributed.Checked;
            pnlDistLoad.Enabled = rbDistributed.Checked;
        }

        #endregion

        #region 安全系数计算

        private void btnCalcSafety_Click(object sender, EventArgs e)
        {
            try
            {
                double sigma_yield = double.Parse(txtSafetyYield.Text);
                double sigma_vm = double.Parse(txtSafetyVM.Text);
                double n = StressCalculator.CalcSafetyFactor(sigma_yield, sigma_vm);
                txtSafetyResult.Text = n.ToString("F4");

                // 判定结果
                if (n >= 2.0)
                    lblSafetyJudgment.Text = "安全 (n >= 2.0)";
                else if (n >= 1.5)
                    lblSafetyJudgment.Text = "注意 (1.5 <= n < 2.0)";
                else if (n >= 1.0)
                    lblSafetyJudgment.Text = "警告 (1.0 <= n < 1.5)";
                else
                    lblSafetyJudgment.Text = "危险! (n < 1.0)";

                lblSafetyJudgment.ForeColor = n >= 1.5 ? System.Drawing.Color.Green :
                    (n >= 1.0 ? System.Drawing.Color.Orange : System.Drawing.Color.Red);
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入参数错误: " + ex.Message, "计算错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion
    }
}
