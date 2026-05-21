using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

            var specNames = new List<string>();
            foreach (var dim in _dimensions)
                specNames.Add(dim.DisplayName);

            // Populate all spec combos
            foreach (var cbo in new[] { cboTensionSpec, cboShearSpec, cboCombSpec, cboEccSpec, cboFlanSpec })
            {
                cbo.Items.Clear();
                foreach (var s in specNames) cbo.Items.Add(s);
                cbo.SelectedIndex = 8; // M16
            }

            var gradeNames = new List<string>();
            foreach (var g in _grades)
                gradeNames.Add(g.Name);

            // Populate all grade combos
            foreach (var cbo in new[] { cboTensionGrade, cboShearGrade, cboCombGrade, cboEccGrade, cboFlanGrade, cboRecGrade })
            {
                cbo.Items.Clear();
                foreach (var g in gradeNames) cbo.Items.Add(g);
                cbo.SelectedIndex = 4; // 8.8
            }

            // Default selections
            cboTensionConn.SelectedIndex = 0;
            cboShearConn.SelectedIndex = 0;
            cboCombConn.SelectedIndex = 0;
            cboTensionK1.SelectedIndex = 0;
            cboTensionCond.SelectedIndex = 0;
        }

        // ============================
        // Tab1: 轴向拉伸
        // ============================
        private void btnTensionCalc_Click(object sender, EventArgs e)
        {
            try
            {
                var dim = GetSelectedDim(cboTensionSpec);
                var grade = GetSelectedGrade(cboTensionGrade);
                if (dim == null || grade == null) return;

                if (!ParseDouble(txtTensionF, "轴向力", out double F)) return;
                int n = (int)numTensionN.Value;
                if (!ParseDouble(txtTensionK, "预紧力系数", out double K, 1.0, 3.0)) return;
                if (!ParseDouble(txtTensionK2, "残余预紧力系数", out double K2, 0)) return;
                double K1 = cboTensionK1.SelectedIndex == 0 ? 0.2 : 0.15;
                if (!ParseDouble(txtTensionCb, "刚度比", out double Cb, 0.001, 0.999)) return;
                var connType = (ConnectionType)cboTensionConn.SelectedIndex;
                var loadCond = (LoadCondition)cboTensionCond.SelectedIndex;
                if (!ParseDouble(txtTensionSF, "安全系数", out double sf, 0.1)) return;

                var result = BoltCalculator.CheckTension(dim, grade, F, n, K, K2, K1, Cb, connType, loadCond, sf);
                DisplayTensionResult(result);
            }
            catch (Exception ex) { MessageBox.Show("计算出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DisplayTensionResult(BoltCheckResult r)
        {
            dgvResult.Rows.Clear();
            AddRow("场景", r.ScenarioName, "-", "-");
            AddRow("连接形式", r.ConnectionType, "-", "-");
            AddRow("螺栓规格", r.SpecName, "-", "-");
            AddRow("材料等级", r.GradeName, "-", "-");
            AddRow("公称直径 d", r.D.ToString("F1"), "mm", "-");
            AddRow("螺距 P", r.P.ToString("F2"), "mm", "-");
            AddRow("小径 d1", r.D1.ToString("F3"), "mm", "-");
            AddRow("中径 d2", r.D2.ToString("F3"), "mm", "-");
            AddRow("应力截面积 As", r.As.ToString("F1"), "mm²", "-");
            AddRow("抗拉强度 σb", r.SigmaB.ToString("F0"), "MPa", "-");
            AddRow("屈服强度 σs", r.SigmaS.ToString("F0"), "MPa", "-");
            AddSepRow();
            AddRow("单螺栓载荷 F/n", r.FPerBolt.ToString("F3"), "kN", "-");
            AddRow("拉伸应力 σ", r.Sigma.ToString("F2"), "MPa", "σ = F/(n·As)");
            AddRow("预紧力 F0", r.F0.ToString("F3"), "kN", $"F0 = K·F/n (K={r.K})");
            AddRow("残余预紧力 F0'", r.F0Residual.ToString("F3"), "kN", "F0' = K2·F/n");
            AddRow("总拉力 F2", r.F2.ToString("F3"), "kN", "F2 = F0 + Cb/(Cb+Cm)·F/n");
            AddRow("总拉伸应力 σt", r.SigmaTotal.ToString("F2"), "MPa", "σt = F2/As");
            AddRow("拧紧力矩 T", r.T.ToString("F2"), "N·m", $"T = K1·F0·d (K1={r.K1})");
            AddSepRow();
            AddRow("安全系数 S", r.S.ToString("F2"), "-", $"S = σs·As/(F/n)  ≥{r.SafetyFactor}");
            AddRow("应力幅 σa", r.SigmaA.ToString("F2"), "MPa", "σa = Cb/(Cb+Cm)·Fa/As");
            AddRow("疲劳安全系数 Sf", r.SFatigue.ToString("F2"), "-", $"Sf = σ(-1)/σa  ≥{r.SafetyFactor}");

            UpdateStatus(r.IsPassed, r.IsFatiguePassed, r.S >= r.SafetyFactor, r.SFatigue >= r.SafetyFactor);
        }

        // ============================
        // Tab2: 横向剪切
        // ============================
        private void btnShearCalc_Click(object sender, EventArgs e)
        {
            try
            {
                var dim = GetSelectedDim(cboShearSpec);
                var grade = GetSelectedGrade(cboShearGrade);
                if (dim == null || grade == null) return;

                if (!ParseDouble(txtShearFs, "横向力", out double Fs)) return;
                int n = (int)numShearN.Value;
                if (!ParseDouble(txtShearMu, "摩擦系数", out double mu, 0.01)) return;
                int m = (int)numShearM.Value;
                if (!ParseDouble(txtShearSF, "安全系数", out double sf, 0.1)) return;

                var connType = cboShearConn.SelectedIndex == 0 ? ConnectionType.普通螺栓 : ConnectionType.铰制孔螺栓;
                var result = BoltCalculator.CheckShear(dim, grade, Fs, n, mu, m, connType, sf);
                DisplayShearResult(result);
            }
            catch (Exception ex) { MessageBox.Show("计算出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DisplayShearResult(BoltCheckResult r)
        {
            dgvResult.Rows.Clear();
            AddRow("场景", r.ScenarioName, "-", "-");
            AddRow("连接形式", r.ConnectionType, "-", "-");
            AddRow("螺栓规格", r.SpecName, "-", "-");
            AddRow("材料等级", r.GradeName, "-", "-");
            AddRow("公称直径 d", r.D.ToString("F1"), "mm", "-");
            AddRow("应力截面积 As", r.As.ToString("F1"), "mm²", "-");
            AddSepRow();
            AddRow("横向力 Fs", r.Fs.ToString("F3"), "kN", "-");
            AddRow("单螺栓载荷 Fs/n", r.FPerBolt.ToString("F3"), "kN", "-");

            if (r.ConnectionType == "铰制孔螺栓")
            {
                AddRow("剪切面积", (Math.PI * r.D * r.D / 4.0).ToString("F1"), "mm²", "πd²/4");
                AddRow("剪切应力 τ", r.Tau.ToString("F2"), "MPa", "τ = Fs/(n·πd²/4)");
                AddRow("许用剪切应力 [τ]", (0.6 * r.SigmaS).ToString("F0"), "MPa", "[τ] = 0.6·σs");
                AddSepRow();
                AddRow("剪切安全系数 Sτ", r.SShear.ToString("F2"), "-", $"[τ]/τ  ≥{r.SafetyFactor}");
            }
            else
            {
                AddRow("所需预紧力 F0'", r.F0Min.ToString("F3"), "kN", "F0 = Fs/(n·m·μ)");
                AddRow("预紧应力", (r.F0Min * 1000.0 / r.As).ToString("F2"), "MPa", "σ0 = F0/As");
                AddSepRow();
                AddRow("螺栓能力 F0_max", (r.SigmaS * r.As / 1000.0).ToString("F3"), "kN", "σs·As");
                AddRow("强度安全系数 S", r.S.ToString("F2"), "-", $"σs·As/F0  ≥{r.SafetyFactor}");
                AddRow("裕度安全系数", r.SShear.ToString("F2"), "-", $"F0_max/F0'  ≥{r.SafetyFactor}");
            }

            UpdateStatus(r.IsShearPassed, true, r.SShear >= r.SafetyFactor, true);
        }

        // ============================
        // Tab3: 拉剪组合
        // ============================
        private void btnCombCalc_Click(object sender, EventArgs e)
        {
            try
            {
                var dim = GetSelectedDim(cboCombSpec);
                var grade = GetSelectedGrade(cboCombGrade);
                if (dim == null || grade == null) return;

                if (!ParseDouble(txtCombF, "轴向力", out double F)) return;
                if (!ParseDouble(txtCombFs, "横向力", out double Fs)) return;
                int n = (int)numCombN.Value;
                if (!ParseDouble(txtCombK, "预紧力系数", out double K, 1.0, 3.0)) return;
                if (!ParseDouble(txtCombCb, "刚度比", out double Cb, 0.001, 0.999)) return;
                if (!ParseDouble(txtCombMu, "摩擦系数", out double mu, 0.01)) return;
                int m = (int)numCombM.Value;
                if (!ParseDouble(txtCombSF, "安全系数", out double sf, 0.1)) return;

                var connType = cboCombConn.SelectedIndex == 0 ? ConnectionType.普通螺栓 : ConnectionType.铰制孔螺栓;
                var result = BoltCalculator.CheckCombined(dim, grade, F, Fs, n, K, 1.0, 0.2, Cb, mu, m, connType, sf);
                DisplayCombinedResult(result);
            }
            catch (Exception ex) { MessageBox.Show("计算出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DisplayCombinedResult(BoltCheckResult r)
        {
            dgvResult.Rows.Clear();
            AddRow("场景", r.ScenarioName, "-", "-");
            AddRow("连接形式", r.ConnectionType, "-", "-");
            AddRow("螺栓规格", r.SpecName, "-", "-");
            AddRow("材料等级", r.GradeName, "-", "-");
            AddSepRow();
            AddRow("轴向力 F", r.F.ToString("F3"), "kN", "-");
            AddRow("横向力 Fs", r.Fs.ToString("F3"), "kN", "-");
            AddRow("预紧力 F0", r.F0.ToString("F3"), "kN", "-");
            AddRow("总拉力 F2", r.F2.ToString("F3"), "kN", "F2 = F0 + Cb/(Cb+Cm)·F/n");
            AddSepRow();
            AddRow("拉伸应力 σ", r.Sigma.ToString("F2"), "MPa", "σ = F2/As");
            AddRow("剪切应力 τ", r.Tau.ToString("F2"), "MPa", r.Tau > 0 ? "τ = Fs/(n·A_shear)" : "摩擦型不直接承受剪切");
            AddRow("等效应力 σeq", r.SigmaEq.ToString("F2"), "MPa", "σeq = √(σ² + 3τ²)");
            AddSepRow();
            AddRow("拉伸安全系数 S", r.S.ToString("F2"), "-", $"σs·As/F2  ≥{r.SafetyFactor}");
            AddRow("剪切安全系数 Sτ", r.SShear.ToString("F2"), "-", r.Tau > 0 ? "0.6·σs/τ" : "∞(摩擦型)");
            AddRow("组合安全系数 Sc", r.SCombined.ToString("F2"), "-", $"σs/σeq  ≥{r.SafetyFactor}");
            AddRow("疲劳安全系数 Sf", r.SFatigue.ToString("F2"), "-", "σ(-1)/σa");

            UpdateStatus(r.IsCombinedPassed, r.IsFatiguePassed, r.SCombined >= r.SafetyFactor, r.SFatigue >= r.SafetyFactor);
        }

        // ============================
        // Tab4: 偏心载荷
        // ============================
        private void btnEccCalc_Click(object sender, EventArgs e)
        {
            try
            {
                var dim = GetSelectedDim(cboEccSpec);
                var grade = GetSelectedGrade(cboEccGrade);
                if (dim == null || grade == null) return;

                if (!ParseDouble(txtEccF, "载荷", out double F)) return;
                if (!ParseDouble(txtEccE, "偏心距", out double e_mm, 0.1)) return;
                int n = (int)numEccN.Value;
                if (!ParseDouble(txtEccSpacing, "螺栓间距", out double spacing, 1.0)) return;
                if (!ParseDouble(txtEccK, "预紧力系数", out double K, 1.0, 3.0)) return;
                if (!ParseDouble(txtEccCb, "刚度比", out double Cb, 0.001, 0.999)) return;
                if (!ParseDouble(txtEccSF, "安全系数", out double sf, 0.1)) return;

                var result = BoltCalculator.CheckEccentric(dim, grade, F, e_mm, n, K, Cb, spacing, ConnectionType.普通螺栓, sf);
                DisplayEccentricResult(result);
            }
            catch (Exception ex) { MessageBox.Show("计算出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DisplayEccentricResult(BoltCheckResult r)
        {
            dgvResult.Rows.Clear();
            AddRow("场景", r.ScenarioName, "-", "-");
            AddRow("螺栓规格", r.SpecName, "-", "-");
            AddRow("材料等级", r.GradeName, "-", "-");
            AddSepRow();
            AddRow("外载荷 F", r.F.ToString("F3"), "kN", "-");
            AddRow("单螺栓平均力 F/n", (r.F / r.N).ToString("F3"), "kN", "-");
            AddRow("最大螺栓力 Fmax", r.FPerBolt.ToString("F3"), "kN", "F/n + M·ymax/Σyi²");
            AddRow("拉伸应力 σ", r.Sigma.ToString("F2"), "MPa", "σ = Fmax/As");
            AddRow("预紧力 F0", r.F0.ToString("F3"), "kN", "F0 = K·Fmax");
            AddRow("总拉力 F2", r.F2.ToString("F3"), "kN", "F2 = F0 + Cb·Fmax");
            AddRow("总拉伸应力 σt", r.SigmaTotal.ToString("F2"), "MPa", "σt = F2/As");
            AddSepRow();
            AddRow("安全系数 S", r.S.ToString("F2"), "-", $"σs·As/(F2)  ≥{r.SafetyFactor}");

            UpdateStatus(r.IsPassed, true, r.S >= r.SafetyFactor, true);
        }

        // ============================
        // Tab5: 法兰连接
        // ============================
        private void btnFlanCalc_Click(object sender, EventArgs e)
        {
            try
            {
                var dim = GetSelectedDim(cboFlanSpec);
                var grade = GetSelectedGrade(cboFlanGrade);
                if (dim == null || grade == null) return;

                if (!ParseDouble(txtFlanP, "设计压力", out double P, 0.001)) return;
                if (!ParseDouble(txtFlanDf, "法兰内径", out double Df, 1.0)) return;
                if (!ParseDouble(txtFlanDb, "螺栓分布圆", out double Db, 1.0)) return;
                int n = (int)numFlanN.Value;
                if (!ParseDouble(txtFlanK, "预紧力系数", out double K, 1.0, 3.0)) return;
                if (!ParseDouble(txtFlanCb, "刚度比", out double Cb, 0.001, 0.999)) return;
                if (!ParseDouble(txtFlanGasket, "垫片系数", out double gasket, 0.1)) return;
                if (!ParseDouble(txtFlanSF, "安全系数", out double sf, 0.1)) return;

                var result = BoltCalculator.CheckFlange(dim, grade, P, Df, Db, n, K, Cb, gasket, sf);
                DisplayFlangeResult(result);
            }
            catch (Exception ex) { MessageBox.Show("计算出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DisplayFlangeResult(BoltCheckResult r)
        {
            dgvResult.Rows.Clear();
            AddRow("场景", r.ScenarioName, "-", "-");
            AddRow("螺栓规格", r.SpecName, "-", "-");
            AddRow("材料等级", r.GradeName, "-", "-");
            AddSepRow();
            AddRow("法兰总载荷", r.F.ToString("F3"), "kN", "F = P·πDf²/4 + 垫片力");
            AddRow("单螺栓载荷 F/n", r.FPerBolt.ToString("F3"), "kN", "-");
            AddRow("预紧力 F0", r.F0.ToString("F3"), "kN", "F0 = K·F/n");
            AddRow("总拉力 F2", r.F2.ToString("F3"), "kN", "F2 = F0 + Cb·F/n");
            AddRow("拉伸应力 σ", r.Sigma.ToString("F2"), "MPa", "σ = F/n/As");
            AddRow("总拉伸应力 σt", r.SigmaTotal.ToString("F2"), "MPa", "σt = F2/As");
            AddSepRow();
            AddRow("安全系数 S", r.S.ToString("F2"), "-", $"σs·As/(F2)  ≥{r.SafetyFactor}");
            AddRow("疲劳安全系数 Sf", r.SFatigue.ToString("F2"), "-", "σ(-1)/σa");

            UpdateStatus(r.IsPassed, r.IsFatiguePassed, r.S >= r.SafetyFactor, r.SFatigue >= r.SafetyFactor);
        }

        // ============================
        // Tab6: 推荐直径
        // ============================
        private void btnRecCalc_Click(object sender, EventArgs e)
        {
            try
            {
                var grade = GetSelectedGrade(cboRecGrade);
                if (grade == null) return;

                if (!ParseDouble(txtRecF, "轴向力", out double F)) return;
                int n = (int)numRecN.Value;
                if (!ParseDouble(txtRecSF, "安全系数", out double sf, 0.1)) return;
                if (!ParseDouble(txtRecK, "预紧力系数", out double K, 1.0, 3.0)) return;
                if (!ParseDouble(txtRecCb, "刚度比", out double Cb, 0.001, 0.999)) return;
                bool fine = chkRecFine.Checked;

                var result = BoltCalculator.RecommendDiameter(grade, F, n, sf, K, Cb, fine);
                DisplayRecommendResult(result);
            }
            catch (Exception ex) { MessageBox.Show("计算出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DisplayRecommendResult(BoltCheckResult r)
        {
            dgvResult.Rows.Clear();
            AddRow("场景", "推荐最小直径", "-", "-");
            AddRow("材料等级", r.GradeName, "-", "-");
            AddRow("轴向力 F", r.F.ToString("F3"), "kN", "-");
            AddRow("螺栓数量 n", r.N.ToString(), "-", "-");
            AddRow("安全系数要求", r.SafetyFactor.ToString("F1"), "-", "-");
            AddSepRow();

            if (r.RecommendDim != null)
            {
                AddRow("★ 推荐螺栓规格", r.SpecName, "-", "满足强度的最小规格");
                AddRow("公称直径 d", r.D.ToString("F1"), "mm", "-");
                AddRow("螺距 P", r.P.ToString("F2"), "mm", "-");
                AddRow("应力截面积 As", r.As.ToString("F1"), "mm²", "-");
                AddRow("单螺栓载荷", r.FPerBolt.ToString("F3"), "kN", "-");
                AddRow("预紧力 F0", r.F0.ToString("F3"), "kN", "-");
                AddRow("总拉力 F2", r.F2.ToString("F3"), "kN", "-");
                AddRow("总拉伸应力", r.SigmaTotal.ToString("F2"), "MPa", "-");
                AddRow("实际安全系数 S", r.S.ToString("F2"), "-", $"要求 ≥{r.SafetyFactor}");
                AddRow("建议拧紧力矩", r.T.ToString("F2"), "N·m", "T = 0.2·F0·d");
            }
            else
            {
                AddRow("结果", "无满足条件的规格", "-", "请增加螺栓数量或选用更高等级");
            }

            UpdateStatus(r.RecommendDim != null, true, r.RecommendDim != null, true);
        }

        // ============================
        // HTML 导出
        // ============================
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvResult.Rows.Count == 0)
            {
                MessageBox.Show("请先计算再导出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "HTML 文件|*.html";
                dlg.FileName = $"螺栓校核_{DateTime.Now:yyyyMMdd_HHmmss}.html";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExportHtml(dlg.FileName);
                    MessageBox.Show("导出成功: " + dlg.FileName, "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ExportHtml(string path)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html><html><head><meta charset='utf-8'>");
            sb.AppendLine("<title>螺栓校核计算报告</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body{font-family:'Microsoft YaHei',sans-serif;margin:20px;background:#f8f9fa}");
            sb.AppendLine("h1{color:#2d5f8a;border-bottom:2px solid #2d5f8a;padding-bottom:8px}");
            sb.AppendLine("table{border-collapse:collapse;width:100%;margin:16px 0;background:white;box-shadow:0 1px 3px rgba(0,0,0,0.1)}");
            sb.AppendLine("th,td{border:1px solid #dee2e6;padding:8px 12px;text-align:center}");
            sb.AppendLine("th{background:#2d5f8a;color:white;font-weight:bold}");
            sb.AppendLine("tr:nth-child(even){background:#f8f9fa}");
            sb.AppendLine(".pass{color:#28a745;font-weight:bold}.fail{color:#dc3545;font-weight:bold}");
            sb.AppendLine(".sep{background:#e9ecef;height:2px}");
            sb.AppendLine(".footer{margin-top:24px;color:#6c757d;font-size:12px}");
            sb.AppendLine("</style></head><body>");
            sb.AppendLine("<h1>螺栓校核计算报告</h1>");
            sb.AppendLine($"<p>生成时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>");
            sb.AppendLine("<table><tr><th>项目</th><th>数值</th><th>单位</th><th>说明/公式</th></tr>");

            foreach (DataGridViewRow row in dgvResult.Rows)
            {
                string item = row.Cells[0].Value?.ToString() ?? "";
                string val = row.Cells[1].Value?.ToString() ?? "";
                string unit = row.Cells[2].Value?.ToString() ?? "";
                string note = row.Cells[3].Value?.ToString() ?? "";

                if (item.StartsWith("---"))
                {
                    sb.AppendLine("<tr class='sep'><td colspan='4'></td></tr>");
                }
                else
                {
                    string cls = "";
                    if (item.Contains("安全系数") && double.TryParse(val, out double sv))
                        cls = sv >= 1.5 ? " class='pass'" : " class='fail'";
                    sb.AppendLine($"<tr{cls}><td>{item}</td><td>{val}</td><td>{unit}</td><td>{note}</td></tr>");
                }
            }

            sb.AppendLine("</table>");
            sb.AppendLine($"<p class='footer'>校核结论: {lblStatus.Text} | {lblFatigueStatus.Text}</p>");
            sb.AppendLine("<p class='footer'>参考标准: GB/T 196-2003 | GB/T 3098.1 | GB 50017-2017 | GB/T 1228~1231</p>");
            sb.AppendLine("</body></html>");

            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        }

        // ============================
        // Helper methods
        // ============================
        private BoltDimension GetSelectedDim(ComboBox cbo)
        {
            if (cbo.SelectedIndex < 0) { MessageBox.Show("请选择螺栓规格", "提示"); return null; }
            return _dimensions[cbo.SelectedIndex];
        }

        private BoltGrade GetSelectedGrade(ComboBox cbo)
        {
            if (cbo.SelectedIndex < 0) { MessageBox.Show("请选择材料等级", "提示"); return null; }
            return _grades[cbo.SelectedIndex];
        }

        private bool ParseDouble(TextBox txt, string name, out double val, double min = 0, double max = double.MaxValue)
        {
            if (!double.TryParse(txt.Text, out val) || val < min || val > max)
            {
                MessageBox.Show($"请输入有效的{name} ({min}~{max})", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                val = 0;
                return false;
            }
            return true;
        }

        private void AddRow(string item, string value, string unit, string note)
        {
            dgvResult.Rows.Add(item, value, unit, note);
        }

        private void AddSepRow()
        {
            dgvResult.Rows.Add("----------", "--------", "----", "----");
        }

        private void UpdateStatus(bool strength, bool fatigue, bool strengthBool, bool fatigueBool)
        {
            lblStatus.Text = strength ? "强度校核: 通过" : "强度校核: 不通过";
            lblStatus.ForeColor = strength ? System.Drawing.Color.Green : System.Drawing.Color.Red;

            if (fatigue)
            {
                lblFatigueStatus.Text = fatigueBool ? "疲劳校核: 通过" : "疲劳校核: 不通过";
                lblFatigueStatus.ForeColor = fatigueBool ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            }
            else
            {
                lblFatigueStatus.Text = "";
            }
        }
    }
}
