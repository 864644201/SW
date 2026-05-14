using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GearDesign
{
    public partial class MainForm : Form
    {
        private readonly GearCalculator _calculator;
        private readonly List<GearMaterial> _materials;
        private TextBox _engineeringReportBox;
        private string _lastEngineeringReport = string.Empty;

        public MainForm()
        {
            InitializeComponent();
            _calculator = new GearCalculator();
            _materials = GearMaterial.GetPredefinedMaterials();
            LoadMaterials();
            InitializeEngineeringPanel();
        }

        /// <summary>
        /// 加载材料列表到下拉框
        /// </summary>
        private void LoadMaterials()
        {
            cmbMat1.Items.Clear();
            cmbMat2.Items.Clear();

            foreach (var mat in _materials)
            {
                cmbMat1.Items.Add(mat);
                cmbMat2.Items.Add(mat);
            }

            // 默认选择: 小齿轮 40Cr调质+表面淬火, 大齿轮 45钢调质
            cmbMat1.SelectedIndex = FindMaterialIndex("40Cr", "调质+表面淬火");
            cmbMat2.SelectedIndex = FindMaterialIndex("45钢", "调质");

            cmbMat1.SelectedIndexChanged += cmbMat1_SelectedIndexChanged;
            cmbMat2.SelectedIndexChanged += cmbMat2_SelectedIndexChanged;
            UpdateMaterialInfo();
        }

        private int FindMaterialIndex(string name, string heatTreatment)
        {
            for (int i = 0; i < _materials.Count; i++)
            {
                if (_materials[i].Name == name && _materials[i].HeatTreatment == heatTreatment)
                    return i;
            }
            return 0;
        }

        private void cmbMat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMaterialInfo();
        }

        private void cmbMat2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMaterialInfo();
        }

        private void UpdateMaterialInfo()
        {
            if (cmbMat1.SelectedIndex >= 0)
            {
                var mat = _materials[cmbMat1.SelectedIndex];
                labelMat1Info.Text = $"sigma_Hlim={mat.SigmaHlim}MPa  sigma_Flim={mat.SigmaFlim}MPa  硬度={mat.Hardness}";
            }
            if (cmbMat2.SelectedIndex >= 0)
            {
                var mat = _materials[cmbMat2.SelectedIndex];
                labelMat2Info.Text = $"sigma_Hlim={mat.SigmaHlim}MPa  sigma_Flim={mat.SigmaFlim}MPa  硬度={mat.Hardness}";
            }
        }

        /// <summary>
        /// 计算按钮点击事件
        /// </summary>
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // 读取输入参数
                ReadInputs();

                // 执行计算
                _calculator.Calculate();

                // 显示结果
                DisplayGeometryResults();
                DisplayLoadResults();
                DisplayStrengthResults();
                DisplayConclusion();
                UpdateEngineeringReport();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("计算出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 读取所有输入参数
        /// </summary>
        private void ReadInputs()
        {
            _calculator.Power = ParseDouble(txtPower, "传递功率");
            _calculator.SpeedN1 = ParseDouble(txtSpeed, "小齿轮转速");
            _calculator.RatioI = ParseDouble(txtRatio, "传动比");
            _calculator.Z1 = ParseInt(txtZ1, "小齿轮齿数");
            _calculator.Z2 = ParseInt(txtZ2, "大齿轮齿数");
            _calculator.ModuleM = ParseDouble(txtModule, "模数");
            _calculator.Alpha = ParseDouble(txtAlpha, "压力角");
            _calculator.PhiD = ParseDouble(txtPhiD, "齿宽系数");
            _calculator.PrecisionGrade = int.Parse(cmbPrecision.SelectedItem.ToString());
            _calculator.Material1 = _materials[cmbMat1.SelectedIndex];
            _calculator.Material2 = _materials[cmbMat2.SelectedIndex];

            // 使用系数 KA
            switch (cmbKA.SelectedIndex)
            {
                case 0: _calculator.KA = 1.0; break;
                case 1: _calculator.KA = 1.25; break;
                case 2: _calculator.KA = 1.50; break;
                case 3: _calculator.KA = 1.75; break;
            }
        }

        private double ParseDouble(TextBox txt, string name)
        {
            if (double.TryParse(txt.Text, out double val))
                return val;
            throw new ArgumentException(name + " 必须是有效的数字");
        }

        private int ParseInt(TextBox txt, string name)
        {
            if (int.TryParse(txt.Text, out int val))
                return val;
            throw new ArgumentException(name + " 必须是有效的整数");
        }

        /// <summary>
        /// 显示几何参数结果
        /// </summary>
        private void DisplayGeometryResults()
        {
            dgvGeometry.Rows.Clear();

            AddGeometryRow("分度圆直径 d (mm)", _calculator.D1, _calculator.D2);
            AddGeometryRow("齿顶圆直径 da (mm)", _calculator.Da1, _calculator.Da2);
            AddGeometryRow("齿根圆直径 df (mm)", _calculator.Df1, _calculator.Df2);
            AddGeometryRow("基圆直径 db (mm)", _calculator.Db1, _calculator.Db2);
            AddGeometryRow("齿距 p (mm)", _calculator.Pitch, _calculator.Pitch);

            // 其他参数 (不分大小齿轮)
            dgvGeometry.Rows.Add("齿顶高 ha (mm)", _calculator.Ha.ToString("F3"), _calculator.Ha.ToString("F3"));
            dgvGeometry.Rows.Add("齿根高 hf (mm)", _calculator.Hf.ToString("F3"), _calculator.Hf.ToString("F3"));
            dgvGeometry.Rows.Add("全齿高 h (mm)", _calculator.TotalHeight.ToString("F3"), _calculator.TotalHeight.ToString("F3"));
            dgvGeometry.Rows.Add("中心距 a (mm)", _calculator.CenterDist.ToString("F3"), "-");
            dgvGeometry.Rows.Add("齿宽 b (mm)", _calculator.FaceWidth.ToString("F1"), _calculator.FaceWidth.ToString("F1"));
            dgvGeometry.Rows.Add("端面重合度 eps_a", _calculator.EpsilonAlpha.ToString("F4"), "-");

            // 齿轮2的齿宽 (通常小齿轮比大齿轮宽5~10mm)
            double b2 = _calculator.FaceWidth;
            dgvGeometry.Rows[dgvGeometry.Rows.Count - 3].Cells[2].Value = b2.ToString("F1");
        }

        private void AddGeometryRow(string name, double val1, double val2)
        {
            dgvGeometry.Rows.Add(name, val1.ToString("F3"), val2.ToString("F3"));
        }

        /// <summary>
        /// 显示载荷参数结果
        /// </summary>
        private void DisplayLoadResults()
        {
            txtLoadResult.Text =
                $"小齿轮转矩  T1 = {_calculator.T1:F2} N·mm ({_calculator.T1 / 1000.0:F2} N·m)" + Environment.NewLine +
                $"大齿轮转速  n2 = {_calculator.SpeedN2:F2} rpm" + Environment.NewLine +
                $"圆周力      Ft = {_calculator.Ft:F2} N" + Environment.NewLine +
                $"径向力      Fr = {_calculator.Fr:F2} N" + Environment.NewLine +
                $"法向力      Fn = {_calculator.Fn:F2} N" + Environment.NewLine +
                $"圆周速度    v  = {_calculator.Velocity:F3} m/s" + Environment.NewLine +
                $"────────────────────────────────" + Environment.NewLine +
                $"使用系数    KA = {_calculator.KA:F2}" + Environment.NewLine +
                $"动载系数    Kv = {_calculator.Kv:F4}" + Environment.NewLine +
                $"齿向载荷系数 KHbeta = {_calculator.KHbeta:F4}" + Environment.NewLine +
                $"齿间载荷系数 KHalpha = {_calculator.KHalpha:F4}" + Environment.NewLine +
                $"总载荷系数  K  = {_calculator.K:F4}";
        }

        /// <summary>
        /// 显示强度校核结果
        /// </summary>
        private void DisplayStrengthResults()
        {
            dgvStrength.Rows.Clear();

            // 接触疲劳强度
            string contactResult = _calculator.ContactStrengthOK ? "通过" : "不通过";
            Color contactColor = _calculator.ContactStrengthOK ? Color.Green : Color.Red;
            dgvStrength.Rows.Add(
                "接触疲劳强度",
                _calculator.SigmaH.ToString("F2"),
                _calculator.SigmaHP.ToString("F2"),
                _calculator.SH.ToString("F3"),
                contactResult
            );
            dgvStrength.Rows[0].DefaultCellStyle.ForeColor = contactColor;

            // 小齿轮弯曲疲劳强度
            string bend1Result = _calculator.SF1 >= 1.0 ? "通过" : "不通过";
            Color bend1Color = _calculator.SF1 >= 1.0 ? Color.Green : Color.Red;
            dgvStrength.Rows.Add(
                "弯曲疲劳强度(小齿轮)",
                _calculator.SigmaF1.ToString("F2"),
                _calculator.SigmaFP1.ToString("F2"),
                _calculator.SF1.ToString("F3"),
                bend1Result
            );
            dgvStrength.Rows[1].DefaultCellStyle.ForeColor = bend1Color;

            // 大齿轮弯曲疲劳强度
            string bend2Result = _calculator.SF2 >= 1.0 ? "通过" : "不通过";
            Color bend2Color = _calculator.SF2 >= 1.0 ? Color.Green : Color.Red;
            dgvStrength.Rows.Add(
                "弯曲疲劳强度(大齿轮)",
                _calculator.SigmaF2.ToString("F2"),
                _calculator.SigmaFP2.ToString("F2"),
                _calculator.SF2.ToString("F3"),
                bend2Result
            );
            dgvStrength.Rows[2].DefaultCellStyle.ForeColor = bend2Color;
        }

        /// <summary>
        /// 显示校核结论
        /// </summary>
        private void DisplayConclusion()
        {
            bool allOK = _calculator.ContactStrengthOK && _calculator.BendingStrengthOK;
            string controllingItem = GetControllingItem();

            if (allOK)
            {
                labelConclusion.Text = $"校核结论: 全部通过，控制项目为 {controllingItem}";
                labelConclusion.ForeColor = Color.Green;
            }
            else
            {
                string msg = "校核结论: ";
                if (!_calculator.ContactStrengthOK)
                    msg += "接触强度不足 ";
                if (!_calculator.BendingStrengthOK)
                    msg += "弯曲强度不足";
                msg += $" -- 控制项目为 {controllingItem}，请调整齿轮参数或材料";

                labelConclusion.Text = msg;
                labelConclusion.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 重置按钮
        /// </summary>
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtPower.Text = "10";
            txtSpeed.Text = "1450";
            txtRatio.Text = "3.5";
            txtZ1.Text = "20";
            txtZ2.Text = "70";
            txtModule.Text = "3";
            txtAlpha.Text = "20";
            txtPhiD.Text = "1.0";
            cmbPrecision.SelectedIndex = 1;
            cmbKA.SelectedIndex = 0;
            cmbMat1.SelectedIndex = FindMaterialIndex("40Cr", "调质+表面淬火");
            cmbMat2.SelectedIndex = FindMaterialIndex("45钢", "调质");

            dgvGeometry.Rows.Clear();
            dgvStrength.Rows.Clear();
            txtLoadResult.Text = "";
            labelConclusion.Text = "";
            _engineeringReportBox.Text = "";
            _lastEngineeringReport = string.Empty;
        }

        private void InitializeEngineeringPanel()
        {
            var groupBoxEngineer = new GroupBox
            {
                Text = "工程建议与报告",
                Dock = DockStyle.Top,
                Height = 190,
                Padding = new Padding(8)
            };

            var toolbar = new Panel { Dock = DockStyle.Top, Height = 34 };
            var btnSave = CreateToolbarButton("导出报告");
            var btnCopy = CreateToolbarButton("复制报告");
            btnSave.Dock = DockStyle.Right;
            btnCopy.Dock = DockStyle.Right;
            btnCopy.Click += (sender, args) => CopyEngineeringReport();
            btnSave.Click += (sender, args) => SaveEngineeringReport();
            toolbar.Controls.Add(btnSave);
            toolbar.Controls.Add(btnCopy);

            _engineeringReportBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = Color.White,
                Font = new Font("Microsoft YaHei UI", 9F)
            };

            groupBoxEngineer.Controls.Add(_engineeringReportBox);
            groupBoxEngineer.Controls.Add(toolbar);
            panelRight.Controls.Add(groupBoxEngineer);
        }

        private static Button CreateToolbarButton(string text)
        {
            return new Button
            {
                Text = text,
                Width = 92,
                Height = 28,
                Margin = new Padding(0, 0, 8, 0)
            };
        }

        private void UpdateEngineeringReport()
        {
            _lastEngineeringReport = BuildEngineeringReport();
            _engineeringReportBox.Text = _lastEngineeringReport;
        }

        private string BuildEngineeringReport()
        {
            var builder = new StringBuilder();
            double actualRatio = _calculator.Z1 == 0 ? 0 : (double)_calculator.Z2 / _calculator.Z1;
            double ratioDeviation = Math.Abs(actualRatio - _calculator.RatioI);
            string controllingItem = GetControllingItem();
            double faceWidthRatio = _calculator.D1 <= 0 ? 0 : _calculator.FaceWidth / _calculator.D1;

            builder.AppendLine("工程结论");
            builder.AppendLine($"- 总体判定: {(_calculator.ContactStrengthOK && _calculator.BendingStrengthOK ? "校核通过，可进入结构与工艺细化阶段。" : "校核未完全通过，建议先调整参数再下发图纸。")}");
            builder.AppendLine($"- 控制项目: {controllingItem}");
            builder.AppendLine($"- 实际传动比: {actualRatio:F3}，与目标传动比偏差 {ratioDeviation:F3}");
            builder.AppendLine($"- 圆周速度: {_calculator.Velocity:F2} m/s，当前精度等级 { _calculator.PrecisionGrade } 级");
            builder.AppendLine($"- 齿宽比 b/d1: {faceWidthRatio:F3}，端面重合度 eps_a: {_calculator.EpsilonAlpha:F3}");
            builder.AppendLine();
            builder.AppendLine("资深工程师建议");

            if (!_calculator.ContactStrengthOK)
            {
                builder.AppendLine($"- 接触安全系数 SH={_calculator.SH:F2} 偏低，优先增大模数、齿宽，或提高齿面硬度。");
            }
            else
            {
                builder.AppendLine($"- 接触安全系数 SH={_calculator.SH:F2}，接触疲劳裕量可接受。");
            }

            if (_calculator.SF1 < 1.0 || _calculator.SF2 < 1.0)
            {
                builder.AppendLine($"- 弯曲安全系数偏低，小齿轮 SF1={_calculator.SF1:F2}，大齿轮 SF2={_calculator.SF2:F2}，建议优先增大模数或优化齿根强度。");
            }
            else
            {
                builder.AppendLine($"- 弯曲强度通过，最小弯曲安全系数为 {Math.Min(_calculator.SF1, _calculator.SF2):F2}。");
            }

            if (_calculator.EpsilonAlpha < 1.20)
            {
                builder.AppendLine("- 端面重合度偏低，传动平稳性和噪声表现可能一般，可考虑增大齿数或修正中心距方案。");
            }
            else
            {
                builder.AppendLine("- 端面重合度处于较合理区间，啮合连续性较好。");
            }

            if (_calculator.Velocity > 8.0 && _calculator.PrecisionGrade >= 8)
            {
                builder.AppendLine("- 当前圆周速度较高，建议把精度等级控制在 7 级或更高，并关注齿面修形与安装误差。");
            }

            if (ratioDeviation > 0.05)
            {
                builder.AppendLine("- 齿数组合与目标传动比偏差较明显，若系统对同步性敏感，建议重选 Z1/Z2。");
            }

            if (_calculator.Z1 < 20)
            {
                builder.AppendLine("- 小齿轮齿数偏少，虽未必根切，但加工与噪声裕量偏紧，建议优先评估 20 齿以上方案。");
            }

            builder.AppendLine();
            builder.AppendLine("关键结果");
            builder.AppendLine($"- 接触应力/许用值: {_calculator.SigmaH:F1} / {_calculator.SigmaHP:F1} MPa");
            builder.AppendLine($"- 小齿轮弯曲应力/许用值: {_calculator.SigmaF1:F1} / {_calculator.SigmaFP1:F1} MPa");
            builder.AppendLine($"- 大齿轮弯曲应力/许用值: {_calculator.SigmaF2:F1} / {_calculator.SigmaFP2:F1} MPa");
            builder.AppendLine($"- 总载荷系数 K: {_calculator.K:F3}");

            return builder.ToString();
        }

        private string GetControllingItem()
        {
            double minFactor = _calculator.SH;
            string item = "接触疲劳强度";

            if (_calculator.SF1 < minFactor)
            {
                minFactor = _calculator.SF1;
                item = "小齿轮弯曲疲劳强度";
            }

            if (_calculator.SF2 < minFactor)
            {
                item = "大齿轮弯曲疲劳强度";
            }

            return item;
        }

        private void CopyEngineeringReport()
        {
            if (string.IsNullOrWhiteSpace(_lastEngineeringReport))
            {
                return;
            }

            try
            {
                Clipboard.SetText(_lastEngineeringReport);
                MessageBox.Show("齿轮设计报告已复制。", "已复制", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("复制失败: " + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SaveEngineeringReport()
        {
            if (string.IsNullOrWhiteSpace(_lastEngineeringReport))
            {
                return;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "文本报告 (*.txt)|*.txt";
                dialog.FileName = "齿轮设计工程报告.txt";
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    System.IO.File.WriteAllText(dialog.FileName, _lastEngineeringReport, Encoding.UTF8);
                    MessageBox.Show("齿轮设计报告已导出。", "导出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出失败: " + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
