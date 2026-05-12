using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace InquireTolerance
{
    public partial class MainForm : Form
    {
        private int _precision = 3; // 小数位数

        public MainForm()
        {
            InitializeComponent();
            InitializeComboBoxes();
            RefreshTable();
        }

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        private void InitializeComboBoxes()
        {
            // 孔偏差代号
            string[] holeDeviations = {
                "A", "B", "C", "CD", "D", "E", "EF", "F", "FG", "G",
                "H", "JS", "J", "K", "M", "N", "P", "R", "S", "T", "U"
            };

            // 轴偏差代号
            string[] shaftDeviations = {
                "a", "b", "c", "cd", "d", "e", "ef", "f", "fg", "g",
                "h", "js", "j", "k", "m", "n", "p", "r", "s", "t", "u"
            };

            cmbDeviationCode.Items.Clear();
            cmbDeviationCode.Items.AddRange(radHole.Checked ? holeDeviations : shaftDeviations);
            cmbDeviationCode.SelectedIndex = radHole.Checked ? 10 : 10; // 默认 H 或 h

            // 公差等级
            cmbITGrade.Items.Clear();
            for (int i = 1; i <= 18; i++)
            {
                cmbITGrade.Items.Add("IT" + i);
            }
            cmbITGrade.SelectedIndex = 6; // 默认 IT7

            // 表格公差等级
            cmbTableGrade.Items.Clear();
            for (int i = 5; i <= 13; i++)
            {
                cmbTableGrade.Items.Add("IT" + i);
            }
            cmbTableGrade.SelectedIndex = 2; // 默认 IT7
        }

        /// <summary>
        /// 切换孔/轴时更新偏差代号列表
        /// </summary>
        private void RadType_CheckedChanged(object sender, EventArgs e)
        {
            InitializeComboBoxes();
            UpdateLabels();
        }

        /// <summary>
        /// 更新标签文字
        /// </summary>
        private void UpdateLabels()
        {
            if (radHole.Checked)
            {
                lblESCaption.Text = "上偏差 (ES):";
                lblEICaption.Text = "下偏差 (EI):";
            }
            else
            {
                lblESCaption.Text = "上偏差 (es):";
                lblEICaption.Text = "下偏差 (ei):";
            }
        }

        /// <summary>
        /// 查询按钮点击
        /// </summary>
        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(txtNominalSize.Text, out double nominalSize) || nominalSize <= 0)
                {
                    MessageBox.Show("请输入有效的基本尺寸 (正数)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNominalSize.Focus();
                    return;
                }

                if (nominalSize > 500)
                {
                    MessageBox.Show("基本尺寸超出范围 (0.5-500mm)", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string deviationCode = cmbDeviationCode.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(deviationCode))
                {
                    MessageBox.Show("请选择偏差代号", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int itGrade = cmbITGrade.SelectedIndex + 1; // IT1-IT18

                double tolerance = ToleranceCalculator.CalculateStandardTolerance(nominalSize, itGrade);
                double es, ei;

                if (radHole.Checked)
                {
                    es = ToleranceCalculator.CalculateHoleUpperDeviation(nominalSize, deviationCode, itGrade);
                    ei = ToleranceCalculator.CalculateHoleLowerDeviation(nominalSize, deviationCode, itGrade);
                }
                else
                {
                    es = ToleranceCalculator.CalculateShaftUpperDeviation(nominalSize, deviationCode, itGrade);
                    ei = ToleranceCalculator.CalculateShaftLowerDeviation(nominalSize, deviationCode, itGrade);
                }

                // 显示结果 (转换为 mm)
                txtES.Text = ToleranceCalculator.FormatDeviation(es, _precision);
                txtEI.Text = ToleranceCalculator.FormatDeviation(ei, _precision);
                txtTolerance.Text = Math.Round(tolerance / 1000.0, _precision).ToString("F" + _precision);

                // 更新配合计算输入
                if (radHole.Checked)
                {
                    txtFitHoleCode.Text = deviationCode;
                    txtFitHoleGrade.Text = itGrade.ToString();
                }
                else
                {
                    txtFitShaftCode.Text = deviationCode;
                    txtFitShaftGrade.Text = itGrade.ToString();
                }

                lblStatus.Text = $"查询完成: {deviationCode}{itGrade}, 基本尺寸 {nominalSize}mm, 公差值 {Math.Round(tolerance / 1000.0, _precision)}mm";
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 配合计算按钮点击
        /// </summary>
        private void BtnFitCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(txtNominalSize.Text, out double nominalSize) || nominalSize <= 0)
                {
                    MessageBox.Show("请输入有效的基本尺寸", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string holeCode = txtFitHoleCode.Text.Trim();
                string shaftCode = txtFitShaftCode.Text.Trim();

                if (string.IsNullOrEmpty(holeCode) || string.IsNullOrEmpty(shaftCode))
                {
                    MessageBox.Show("请输入孔和轴的偏差代号", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtFitHoleGrade.Text, out int holeGrade) || holeGrade < 1 || holeGrade > 18)
                {
                    MessageBox.Show("孔的公差等级应为 1-18", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtFitShaftGrade.Text, out int shaftGrade) || shaftGrade < 1 || shaftGrade > 18)
                {
                    MessageBox.Show("轴的公差等级应为 1-18", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                FitResult result = FitCalculator.CalculateFit(nominalSize, holeCode, holeGrade, shaftCode, shaftGrade);
                txtFitResult.Text = FitCalculator.FormatFitResult(result, _precision);

                lblStatus.Text = $"配合计算完成: {result.FitCode}, {result.TypeDescription}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("配合计算出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 刷新公差表格
        /// </summary>
        private void BtnShowTable_Click(object sender, EventArgs e)
        {
            RefreshTable();
        }

        /// <summary>
        /// 刷新公差速查表
        /// </summary>
        private void RefreshTable()
        {
            try
            {
                int itGrade = cmbTableGrade.SelectedIndex + 5; // IT5-IT13
                if (itGrade < 5) itGrade = 7;

                DataTable dt = new DataTable();
                dt.Columns.Add("尺寸范围 (mm)", typeof(string));
                dt.Columns.Add("公差值 (mm)", typeof(string));
                dt.Columns.Add("H 偏差 (mm)", typeof(string));
                dt.Columns.Add("h 偏差 (mm)", typeof(string));
                dt.Columns.Add("g 偏差 (mm)", typeof(string));
                dt.Columns.Add("f 偏差 (mm)", typeof(string));
                dt.Columns.Add("k 偏差 (mm)", typeof(string));
                dt.Columns.Add("n 偏差 (mm)", typeof(string));
                dt.Columns.Add("p 偏差 (mm)", typeof(string));

                // 常用尺寸
                double[] sizes = {
                    3, 6, 10, 18, 30, 50, 80, 120, 180, 250, 315, 400, 500
                };
                string[] rangeNames = {
                    "0.5-3", "3-6", "6-10", "10-18", "18-30", "30-50",
                    "50-80", "80-120", "120-180", "180-250", "250-315",
                    "315-400", "400-500"
                };

                for (int i = 0; i < sizes.Length; i++)
                {
                    double d = sizes[i];
                    double tol = ToleranceCalculator.CalculateStandardTolerance(d, itGrade);

                    // H 孔: EI=0, ES=IT
                    double hES = tol;
                    double hEI = 0;

                    // h 轴: es=0, ei=-IT
                    double hes = 0;
                    double hei = -tol;

                    // g 轴
                    double ges = ToleranceCalculator.CalculateShaftUpperDeviation(d, "g", itGrade);
                    double gei = ToleranceCalculator.CalculateShaftLowerDeviation(d, "g", itGrade);

                    // f 轴
                    double fes = ToleranceCalculator.CalculateShaftUpperDeviation(d, "f", itGrade);
                    double fei = ToleranceCalculator.CalculateShaftLowerDeviation(d, "f", itGrade);

                    // k 轴
                    double kes = ToleranceCalculator.CalculateShaftUpperDeviation(d, "k", itGrade);
                    double kei = ToleranceCalculator.CalculateShaftLowerDeviation(d, "k", itGrade);

                    // n 轴
                    double nes = ToleranceCalculator.CalculateShaftUpperDeviation(d, "n", itGrade);
                    double nei = ToleranceCalculator.CalculateShaftLowerDeviation(d, "n", itGrade);

                    // p 轴
                    double pes = ToleranceCalculator.CalculateShaftUpperDeviation(d, "p", itGrade);
                    double pei = ToleranceCalculator.CalculateShaftLowerDeviation(d, "p", itGrade);

                    DataRow row = dt.NewRow();
                    row[0] = rangeNames[i];
                    row[1] = FormatRange(tol);
                    row[2] = FormatRange(hES, hEI);
                    row[3] = FormatRange(hes, hei);
                    row[4] = FormatRange(ges, gei);
                    row[5] = FormatRange(fes, fei);
                    row[6] = FormatRange(kes, kei);
                    row[7] = FormatRange(nes, nei);
                    row[8] = FormatRange(pes, pei);
                    dt.Rows.Add(row);
                }

                dgvToleranceTable.DataSource = dt;
                lblStatus.Text = $"公差速查表已刷新 (IT{itGrade})";
            }
            catch (Exception ex)
            {
                MessageBox.Show("表格刷新出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 格式化公差值为 "ES/EI" 格式 (微米转mm)
        /// </summary>
        private string FormatRange(double es, double ei)
        {
            string s = ToleranceCalculator.FormatDeviation(es, _precision);
            string i = ToleranceCalculator.FormatDeviation(ei, _precision);
            return $"{s} / {i}";
        }

        /// <summary>
        /// 格式化单一公差值
        /// </summary>
        private string FormatRange(double tolerance)
        {
            return Math.Round(tolerance / 1000.0, _precision).ToString("F" + _precision);
        }
    }
}
