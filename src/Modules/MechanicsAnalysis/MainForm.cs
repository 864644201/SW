using System;
using System.Drawing;
using System.Windows.Forms;

namespace MDSolids
{
    public partial class MainForm : Form
    {
        private TextBox _bendingInsightBox;
        private TextBox _torsionInsightBox;
        private TextBox _combinedInsightBox;
        private TextBox _deflectionInsightBox;
        private TextBox _safetyInsightBox;

        public MainForm()
        {
            InitializeComponent();
            LoadMaterialList();
            InitializeEngineerAssistUi();
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
                double sigmaS = ParseMaterialStrength(txtSigmaYield.Text);
                double factor = sigma > 0 ? sigmaS / sigma : 0;
                UpdateInsight(_bendingInsightBox,
                    $"弯曲结论：截面弯曲应力为 {sigma:F2} MPa，材料屈服强度 {sigmaS:F0} MPa，静强度安全系数约 {factor:F2}。" + Environment.NewLine +
                    (factor >= 2.0
                        ? "当前弯曲裕量较充足，适合常规结构件。"
                        : factor >= 1.2
                            ? "当前能工作，但安全裕量偏紧，建议复核冲击和应力集中。"
                            : "弯曲裕量不足，建议优先增大截面模量或降低弯矩。"));
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
                double sigmaS = ParseMaterialStrength(txtSigmaYield.Text);
                double allowableTau = sigmaS * 0.58;
                UpdateInsight(_torsionInsightBox,
                    $"扭转结论：切应力为 {tau:F2} MPa，按屈服估算许用切应力约 {allowableTau:F1} MPa。" + Environment.NewLine +
                    (tau <= allowableTau
                        ? "当前扭转强度基本可接受，后续重点关注键槽和花键等局部削弱。"
                        : "扭转应力已偏高，建议加粗轴径或改用更高强度材料。"));
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
                    UpdateInsight(_combinedInsightBox,
                        $"组合应力结论：Von Mises 等效应力 {sigma_vm:F2} MPa，材料屈服强度 {mat.SigmaS:F0} MPa，安全系数 n={n:F2}。" + Environment.NewLine +
                        (n >= 2.0
                            ? "适合一般连续工况。"
                            : n >= 1.5
                                ? "具备一定可用性，但建议核查装配偏载与冲击附加载荷。"
                                : "安全系数偏低，建议尽快优化载荷路径或截面。"));
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
                double length = double.Parse(txtDeflectionL.Text);
                double allowable = rbCantilever.Checked ? length / 150.0 : length / 250.0;
                UpdateInsight(_deflectionInsightBox,
                    $"挠度结论：最大挠度 {delta:F3} mm，按经验限值建议控制在 {allowable:F3} mm 以内。" + Environment.NewLine +
                    (delta <= allowable
                        ? "刚度满足常规机械结构要求。"
                        : "刚度偏弱，建议优先增大惯性矩、缩短跨距或提高支撑刚度。"));
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
                UpdateInsight(_safetyInsightBox,
                    $"安全系数结论：n = {n:F2}。" + Environment.NewLine +
                    (n >= 2.0
                        ? "裕量充足，适合一般稳态工况。"
                        : n >= 1.5
                            ? "属于工程上常见可接受区间，但不宜忽视冲击、焊缝和缺口效应。"
                            : n >= 1.0
                                ? "安全系数偏紧，仅建议在充分校核边界条件后使用。"
                                : "存在明显失效风险，应立即调整设计。"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入参数错误: " + ex.Message, "计算错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        private void InitializeEngineerAssistUi()
        {
            var copyButton = new Button
            {
                Text = "复制结论",
                Size = new Size(64, 28),
                Location = new Point(590, 8),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            copyButton.Click += (sender, args) =>
            {
                string text = GetActiveInsightText();
                if (string.IsNullOrWhiteSpace(text))
                {
                    return;
                }

                try
                {
                    Clipboard.SetText(text);
                    toolStripStatusLabel.Text = "当前工程结论已复制";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("复制失败: " + ex.Message, "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            pnlMaterial.Controls.Add(copyButton);

            _bendingInsightBox = CreateInsightBox(new Rectangle(15, 318, 605, 60));
            _torsionInsightBox = CreateInsightBox(new Rectangle(15, 245, 605, 60));
            _combinedInsightBox = CreateInsightBox(new Rectangle(15, 245, 605, 60));
            _deflectionInsightBox = CreateInsightBox(new Rectangle(15, 318, 605, 60));
            _safetyInsightBox = CreateInsightBox(new Rectangle(15, 250, 605, 70));

            tabBending.Controls.Add(_bendingInsightBox);
            tabTorsion.Controls.Add(_torsionInsightBox);
            tabCombined.Controls.Add(_combinedInsightBox);
            tabDeflection.Controls.Add(_deflectionInsightBox);
            tabSafety.Controls.Add(_safetyInsightBox);
        }

        private static TextBox CreateInsightBox(Rectangle bounds)
        {
            return new TextBox
            {
                Location = bounds.Location,
                Size = bounds.Size,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.FromArgb(248, 250, 252),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Microsoft YaHei UI", 9F)
            };
        }

        private void UpdateInsight(TextBox target, string text)
        {
            target.Text = text;
            toolStripStatusLabel.Text = text.Split(new[] { Environment.NewLine }, StringSplitOptions.None)[0];
        }

        private string GetActiveInsightText()
        {
            if (tabControl.SelectedTab == tabBending) return _bendingInsightBox.Text;
            if (tabControl.SelectedTab == tabTorsion) return _torsionInsightBox.Text;
            if (tabControl.SelectedTab == tabCombined) return _combinedInsightBox.Text;
            if (tabControl.SelectedTab == tabDeflection) return _deflectionInsightBox.Text;
            if (tabControl.SelectedTab == tabSafety) return _safetyInsightBox.Text;
            return string.Empty;
        }

        private static double ParseMaterialStrength(string text)
        {
            double result;
            if (double.TryParse(text, out result))
            {
                return result;
            }

            return 235.0;
        }
    }
}
