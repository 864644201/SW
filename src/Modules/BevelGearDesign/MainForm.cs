using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BevelGearDesign
{
    public partial class MainForm : Form
    {
        private List<GearMaterial> materialList;

        public MainForm()
        {
            InitializeComponent();
            InitializeCustom();
        }

        private void InitializeCustom()
        {
            materialList = GearMaterial.GetCommonMaterials();

            // 填充材料下拉框
            foreach (var mat in materialList)
            {
                cmbPinionMaterial.Items.Add(mat.ToString());
                cmbWheelMaterial.Items.Add(mat.ToString());
            }

            // 默认选择常用材料: 45钢调质
            cmbPinionMaterial.SelectedIndex = 0;
            cmbWheelMaterial.SelectedIndex = 2; // 40Cr调质

            // 设置默认值
            txtPower.Text = "5.5";
            txtSpeed.Text = "960";
            txtGearRatio.Text = "3";
            txtZ1.Text = "20";
            txtZ2.Text = "60";
            txtModule.Text = "3";
            txtPressureAngle.Text = "20";
            txtFaceWidthCoeff.Text = "0.3";
            txtShaftAngle.Text = "90";
            txtKA.Text = "1.0";
            txtKv.Text = "1.0";
            txtKbeta.Text = "1.0";
            txtKalpha.Text = "1.0";

            // 齿数比变化时自动更新Z2
            txtGearRatio.Leave += TxtGearRatio_Leave;
            txtZ1.Leave += TxtZ1_Leave;
        }

        private void TxtGearRatio_Leave(object sender, EventArgs e)
        {
            UpdateZ2();
        }

        private void TxtZ1_Leave(object sender, EventArgs e)
        {
            UpdateZ2();
        }

        private void UpdateZ2()
        {
            if (double.TryParse(txtGearRatio.Text, out double u) && int.TryParse(txtZ1.Text, out int z1))
            {
                int z2 = (int)Math.Round(z1 * u);
                txtZ2.Text = z2.ToString();
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // 验证输入
                if (!ValidateInputs())
                    return;

                var calc = new BevelGearCalculator
                {
                    InputPower = double.Parse(txtPower.Text),
                    InputSpeed = double.Parse(txtSpeed.Text),
                    GearRatio = double.Parse(txtGearRatio.Text),
                    Z1 = int.Parse(txtZ1.Text),
                    Z2 = int.Parse(txtZ2.Text),
                    Module = double.Parse(txtModule.Text),
                    PressureAngle = double.Parse(txtPressureAngle.Text),
                    FaceWidthCoeff = double.Parse(txtFaceWidthCoeff.Text),
                    ShaftAngle = double.Parse(txtShaftAngle.Text),
                    KA = double.Parse(txtKA.Text),
                    Kv = double.Parse(txtKv.Text),
                    Kbeta = double.Parse(txtKbeta.Text),
                    Kalpha = double.Parse(txtKalpha.Text),
                    PinionMaterial = materialList[cmbPinionMaterial.SelectedIndex],
                    WheelMaterial = materialList[cmbWheelMaterial.SelectedIndex]
                };

                // 验证齿数比一致性
                double actualRatio = (double)calc.Z2 / calc.Z1;
                double inputRatio = calc.GearRatio;
                if (Math.Abs(actualRatio - inputRatio) / inputRatio > 0.1)
                {
                    MessageBox.Show(
                        $"齿数比不一致: 输入齿数比={inputRatio:F3}, 实际齿数比={actualRatio:F3}\n请检查齿数设置。",
                        "参数警告",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                calc.Calculate();

                txtResult.Text = calc.GetResultText();

                // 高亮结果状态
                lblContactResult.Text = calc.ContactStrengthOk ? "接触强度: 合格" : "接触强度: 不合格";
                lblContactResult.ForeColor = calc.ContactStrengthOk ? System.Drawing.Color.Green : System.Drawing.Color.Red;

                lblBendingResult.Text = calc.BendingStrengthOk ? "弯曲强度: 合格" : "弯曲强度: 不合格";
                lblBendingResult.ForeColor = calc.BendingStrengthOk ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"计算出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (!double.TryParse(txtPower.Text, out double p) || p <= 0)
            {
                MessageBox.Show("请输入有效的输入功率 (大于0)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPower.Focus();
                return false;
            }
            if (!double.TryParse(txtSpeed.Text, out double n) || n <= 0)
            {
                MessageBox.Show("请输入有效的输入转速 (大于0)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSpeed.Focus();
                return false;
            }
            if (!int.TryParse(txtZ1.Text, out int z1) || z1 < 12)
            {
                MessageBox.Show("小齿轮齿数不能小于12 (避免根切)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtZ1.Focus();
                return false;
            }
            if (!int.TryParse(txtZ2.Text, out int z2) || z2 < z1)
            {
                MessageBox.Show("大齿轮齿数应大于小齿轮齿数", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtZ2.Focus();
                return false;
            }
            if (!double.TryParse(txtModule.Text, out double m) || m <= 0)
            {
                MessageBox.Show("请输入有效的模数 (大于0)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtModule.Focus();
                return false;
            }
            if (!double.TryParse(txtPressureAngle.Text, out double alpha) || alpha <= 0 || alpha >= 45)
            {
                MessageBox.Show("压力角应在0~45度之间", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPressureAngle.Focus();
                return false;
            }
            if (!double.TryParse(txtFaceWidthCoeff.Text, out double phi) || phi < 0.1 || phi > 0.5)
            {
                MessageBox.Show("齿宽系数应在0.1~0.5之间 (推荐0.25~0.35)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFaceWidthCoeff.Focus();
                return false;
            }
            if (cmbPinionMaterial.SelectedIndex < 0)
            {
                MessageBox.Show("请选择小齿轮材料", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPinionMaterial.Focus();
                return false;
            }
            if (cmbWheelMaterial.SelectedIndex < 0)
            {
                MessageBox.Show("请选择大齿轮材料", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbWheelMaterial.Focus();
                return false;
            }
            return true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtResult.Clear();
            lblContactResult.Text = "";
            lblBendingResult.Text = "";
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            txtPower.Text = "5.5";
            txtSpeed.Text = "960";
            txtGearRatio.Text = "3";
            txtZ1.Text = "20";
            txtZ2.Text = "60";
            txtModule.Text = "3";
            txtPressureAngle.Text = "20";
            txtFaceWidthCoeff.Text = "0.3";
            txtShaftAngle.Text = "90";
            txtKA.Text = "1.0";
            txtKv.Text = "1.0";
            txtKbeta.Text = "1.0";
            txtKalpha.Text = "1.0";
            cmbPinionMaterial.SelectedIndex = 0;
            cmbWheelMaterial.SelectedIndex = 2;
        }
    }
}
