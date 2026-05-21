using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ToleranceFit
{
    public partial class MainForm : Form
    {
        private ToleranceResult lastResult;

        // 孔偏差字母：A~H, J, Js, K~N, P~Z, ZA, ZB, ZC
        private static readonly string[] HoleLetters = {
            "A","B","C","CD","D","E","EF","F","FG","G","H",
            "J","Js","K","M","N","P","R","S","T","U","V","X","Y","Z","ZA","ZB","ZC"
        };

        // 轴偏差字母：a~h, j, js, k, m~z, za, zb, zc
        private static readonly string[] ShaftLetters = {
            "a","b","c","cd","d","e","ef","f","fg","g","h",
            "j","js","k","m","n","p","r","s","t","u","v","x","y","z","za","zb","zc"
        };

        // 间隙配合可选轴（基孔制）
        private static readonly string[] ClearanceShafts = {"a","b","c","cd","d","e","ef","f","fg","g","h"};
        // 过渡配合可选轴（基孔制）
        private static readonly string[] TransitionShafts = {"js","k","m","n"};
        // 过盈配合可选轴（基孔制）
        private static readonly string[] InterferenceShafts = {"p","r","s","t","u","v","x","y","z","za","zb","zc"};

        // 间隙配合可选孔（基轴制）
        private static readonly string[] ClearanceHoles = {"A","B","C","CD","D","E","EF","F","FG","G","H"};
        // 过渡配合可选孔（基轴制）
        private static readonly string[] TransitionHoles = {"Js","K","M","N"};
        // 过盈配合可选孔（基轴制）
        private static readonly string[] InterferenceHoles = {"P","R","S","T","U","V","X","Y","Z","ZA","ZB","ZC"};

        // 基孔制推荐配合
        private static readonly string[][] HoleBasisRecommend = {
            // 间隙配合
            new string[] {"H6/f5", "H6/g5", "H6/h5",
             "H7/f6", "H7/g6", "H7/h6",
             "H8/d7", "H8/e7", "H8/f7", "H8/g7", "H8/h7",
             "H9/c8", "H9/d8", "H9/e8", "H9/f8", "H9/h8",
             "H10/c9", "H10/d9", "H10/h9",
             "H11/a11", "H11/b11", "H11/c11", "H11/d11", "H11/h11",
             "H12/b12", "H12/h12"},
            // 过渡配合
            new string[] {"H6/js5", "H6/k5", "H6/m5",
             "H7/js6", "H7/k6", "H7/m6", "H7/n6",
             "H8/js7", "H8/k7", "H8/m7", "H8/n7", "H8/p7"},
            // 过盈配合
            new string[] {"H6/n5", "H6/p5", "H6/r5", "H6/s5", "H6/t5",
             "H7/p6", "H7/r6", "H7/s6", "H7/t6", "H7/u6", "H7/v6", "H7/x6", "H7/y6", "H7/z6",
             "H8/r7", "H8/s7", "H8/t7", "H8/u7"}
        };

        public MainForm()
        {
            InitializeComponent();
            PopulateComboBoxes();
            UpdateRecommend();
        }

        private void PopulateComboBoxes()
        {
            cmbHoleLetter.Items.Clear();
            cmbHoleLetter.Items.AddRange(HoleLetters);
            cmbHoleLetter.SelectedIndex = 11; // H

            cmbHoleGrade.Items.Clear();
            for (int i = 1; i <= 18; i++)
                cmbHoleGrade.Items.Add(i.ToString());
            cmbHoleGrade.SelectedIndex = 6; // IT7

            cmbShaftLetter.Items.Clear();
            cmbShaftLetter.Items.AddRange(ShaftLetters);
            cmbShaftLetter.SelectedIndex = 21; // f

            cmbShaftGrade.Items.Clear();
            for (int i = 1; i <= 18; i++)
                cmbShaftGrade.Items.Add(i.ToString());
            cmbShaftGrade.SelectedIndex = 5; // IT6

            // 默认基孔制 + 间隙配合
            radHoleBasis.Checked = true;
            radClearance.Checked = true;
            UpdateLetterOptions();
        }

        private void UpdateLetterOptions()
        {
            bool isHoleBasis = radHoleBasis.Checked;

            if (isHoleBasis)
            {
                // 基孔制：孔固定为H，轴按配合类型变化
                cmbHoleLetter.Items.Clear();
                cmbHoleLetter.Items.Add("H");
                cmbHoleLetter.SelectedIndex = 0;
                cmbHoleLetter.Enabled = false;

                string[] shafts;
                if (radClearance.Checked) shafts = ClearanceShafts;
                else if (radTransition.Checked) shafts = TransitionShafts;
                else shafts = InterferenceShafts;

                string prevShaft = cmbShaftLetter.SelectedItem?.ToString();
                cmbShaftLetter.Items.Clear();
                cmbShaftLetter.Items.AddRange(shafts);
                if (prevShaft != null && cmbShaftLetter.Items.Contains(prevShaft))
                    cmbShaftLetter.SelectedItem = prevShaft;
                else
                    cmbShaftLetter.SelectedIndex = 0;
            }
            else
            {
                // 基轴制：轴固定为h，孔按配合类型变化
                cmbShaftLetter.Items.Clear();
                cmbShaftLetter.Items.Add("h");
                cmbShaftLetter.SelectedIndex = 0;
                cmbShaftLetter.Enabled = false;

                string[] holes;
                if (radClearance.Checked) holes = ClearanceHoles;
                else if (radTransition.Checked) holes = TransitionHoles;
                else holes = InterferenceHoles;

                string prevHole = cmbHoleLetter.SelectedItem?.ToString();
                cmbHoleLetter.Items.Clear();
                cmbHoleLetter.Items.AddRange(holes);
                if (prevHole != null && cmbHoleLetter.Items.Contains(prevHole))
                    cmbHoleLetter.SelectedItem = prevHole;
                else
                    cmbHoleLetter.SelectedIndex = 0;
            }

            UpdateRecommend();
        }

        private void UpdateRecommend()
        {
            int fitIdx = radClearance.Checked ? 0 : (radTransition.Checked ? 1 : 2);
            if (radHoleBasis.Checked)
            {
                var sb = new StringBuilder();
                for (int i = 0; i < HoleBasisRecommend[fitIdx].Length; i++)
                {
                    string val = HoleBasisRecommend[fitIdx][i];
                    if (string.IsNullOrEmpty(val)) break;
                    if (sb.Length > 0) sb.Append(", ");
                    sb.Append(val);
                }
                txtRecommend.Text = sb.ToString();
            }
            else
            {
                txtRecommend.Text = "基轴制配合请手动选择孔公差带";
            }
        }

        private void OnBasisChanged(object sender, EventArgs e)
        {
            UpdateLetterOptions();
        }

        private void OnFitTypeChanged(object sender, EventArgs e)
        {
            UpdateLetterOptions();
        }

        private void OnHoleLetterChanged(object sender, EventArgs e)
        {
            // 联动更新推荐等级
        }

        private void OnShaftLetterChanged(object sender, EventArgs e)
        {
            // 联动更新推荐等级
        }

        private void OnQuery(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            double basicSize = double.Parse(txtBasicSize.Text);
            string holeLetter = cmbHoleLetter.SelectedItem.ToString();
            int holeGrade = int.Parse(cmbHoleGrade.SelectedItem.ToString());
            string shaftLetter = cmbShaftLetter.SelectedItem.ToString();
            int shaftGrade = int.Parse(cmbShaftGrade.SelectedItem.ToString());

            lastResult = ToleranceCalculator.Calculate(
                basicSize, holeLetter, holeGrade, shaftLetter, shaftGrade);

            DisplayResult(lastResult);
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtBasicSize.Text))
            {
                MessageBox.Show("请输入基本尺寸", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBasicSize.Focus();
                return false;
            }

            double size;
            if (!double.TryParse(txtBasicSize.Text, out size) || size <= 0 || size > 3150)
            {
                MessageBox.Show("基本尺寸必须在 0~3150 mm 之间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBasicSize.Focus();
                txtBasicSize.SelectAll();
                return false;
            }

            if (cmbHoleLetter.SelectedItem == null || cmbHoleGrade.SelectedItem == null ||
                cmbShaftLetter.SelectedItem == null || cmbShaftGrade.SelectedItem == null)
            {
                MessageBox.Show("请选择完整的孔和轴公差带", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void DisplayResult(ToleranceResult r)
        {
            var sb = new StringBuilder();
            sb.AppendLine("═══════════════════════════════════════════════════════════");
            sb.AppendLine("  公差与配合查询结果");
            sb.AppendLine("═══════════════════════════════════════════════════════════");
            sb.AppendLine();
            sb.AppendFormat("  基本尺寸:  Φ{0}", r.BasicSize);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendFormat("  孔:  {0}", r.HoleSpec);
            sb.AppendLine();
            sb.AppendFormat("    ES = {0} mm", FormatDeviation(r.ES_Hole));
            sb.AppendLine();
            sb.AppendFormat("    EI = {0} mm", FormatDeviation(r.EI_Hole));
            sb.AppendLine();
            sb.AppendFormat("    最大极限尺寸: Φ{0:F3}", r.HoleMax);
            sb.AppendLine();
            sb.AppendFormat("    最小极限尺寸: Φ{0:F3}", r.HoleMin);
            sb.AppendLine();
            sb.AppendFormat("    公差: {0:F3} mm", r.ES_Hole - r.EI_Hole);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendFormat("  轴:  {0}", r.ShaftSpec);
            sb.AppendLine();
            sb.AppendFormat("    es = {0} mm", FormatDeviation(r.ES_Shaft));
            sb.AppendLine();
            sb.AppendFormat("    ei = {0} mm", FormatDeviation(r.EI_Shaft));
            sb.AppendLine();
            sb.AppendFormat("    最大极限尺寸: Φ{0:F3}", r.ShaftMax);
            sb.AppendLine();
            sb.AppendFormat("    最小极限尺寸: Φ{0:F3}", r.ShaftMin);
            sb.AppendLine();
            sb.AppendFormat("    公差: {0:F3} mm", r.ES_Shaft - r.EI_Shaft);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("───────────────────────────────────────────────────────────");
            sb.AppendFormat("  配合类型:  {0}", r.FitType);
            sb.AppendLine();
            sb.AppendLine();

            if (r.FitType == "间隙配合")
            {
                sb.AppendFormat("    最大间隙: {0} mm", FormatDeviation(r.MaxClearance));
                sb.AppendLine();
                sb.AppendFormat("    最小间隙: {0} mm", FormatDeviation(r.MinClearance));
                sb.AppendLine();
            }
            else if (r.FitType == "过盈配合")
            {
                sb.AppendFormat("    最大过盈: {0} mm", FormatDeviation(r.MaxInterference));
                sb.AppendLine();
                sb.AppendFormat("    最小过盈: {0} mm", FormatDeviation(r.MinInterference));
                sb.AppendLine();
            }
            else
            {
                sb.AppendFormat("    最大间隙: {0} mm", FormatDeviation(r.MaxClearance));
                sb.AppendLine();
                sb.AppendFormat("    最小过盈: {0} mm", FormatDeviation(r.MaxInterference));
                sb.AppendLine();
            }

            sb.AppendLine();
            sb.AppendLine("───────────────────────────────────────────────────────────");
            sb.AppendLine("  公差带示意:");
            sb.AppendLine();
            sb.AppendFormat("    孔: |← EI={0} →|← IT{1}={2:F3} →|",
                FormatDeviation(r.EI_Hole), cmbHoleGrade.SelectedItem, r.ES_Hole - r.EI_Hole);
            sb.AppendLine();
            sb.AppendFormat("    轴: |← es={0} →|← IT{1}={2:F3} →|",
                FormatDeviation(r.ES_Shaft), cmbShaftGrade.SelectedItem, r.ES_Shaft - r.EI_Shaft);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("───────────────────────────────────────────────────────────");
            sb.AppendLine("  参考标准:");
            sb.AppendLine("    GB/T 1800.1-2009 产品几何技术规范 极限与配合");
            sb.AppendLine("    GB/T 1800.2-2009 标准公差数值和基本偏差数值表");
            sb.AppendLine("    GB/T 1801-2009 公差带和配合的选择");
            sb.AppendLine("═══════════════════════════════════════════════════════════");

            txtResult.Text = sb.ToString();
        }

        private string FormatDeviation(double val)
        {
            if (val > 0) return "+" + val.ToString("F3");
            return val.ToString("F3");
        }

        private void OnExportHtml(object sender, EventArgs e)
        {
            if (lastResult == null)
            {
                MessageBox.Show("请先进行查询", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dlg = new SaveFileDialog();
            dlg.Filter = "HTML文件|*.html";
            dlg.FileName = string.Format("公差配合_{0}_{1}_{2}.html",
                lastResult.BasicSize, lastResult.HoleSpec, lastResult.ShaftSpec);

            if (dlg.ShowDialog() != DialogResult.OK) return;

            string html = GenerateHtml(lastResult);
            File.WriteAllText(dlg.FileName, html, Encoding.UTF8);
            MessageBox.Show("导出成功: " + dlg.FileName, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GenerateHtml(ToleranceResult r)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html><head><meta charset='utf-8'>");
            sb.AppendLine("<title>公差与配合查询报告</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body{font-family:'Microsoft YaHei',sans-serif;margin:20px;background:#f5f5f5;}");
            sb.AppendLine(".container{max-width:800px;margin:0 auto;background:#fff;padding:30px;border-radius:8px;box-shadow:0 2px 8px rgba(0,0,0,0.1);}");
            sb.AppendLine("h1{text-align:center;color:#1a5276;border-bottom:3px solid #2980b9;padding-bottom:10px;}");
            sb.AppendLine("h2{color:#2980b9;margin-top:25px;border-left:4px solid #2980b9;padding-left:10px;}");
            sb.AppendLine("table{width:100%;border-collapse:collapse;margin:15px 0;}");
            sb.AppendLine("th,td{border:1px solid #bdc3c7;padding:8px 12px;text-align:center;}");
            sb.AppendLine("th{background:#2980b9;color:#fff;font-weight:bold;}");
            sb.AppendLine("tr:nth-child(even){background:#ecf0f1;}");
            sb.AppendLine(".highlight{background:#d5f5e3;font-weight:bold;}");
            sb.AppendLine(".warn{background:#fdebd0;}");
            sb.AppendLine(".fit-type{font-size:18px;font-weight:bold;color:#e74c3c;text-align:center;padding:10px;}");
            sb.AppendLine(".footer{margin-top:30px;text-align:center;color:#7f8c8d;font-size:12px;border-top:1px solid #eee;padding-top:10px;}");
            sb.AppendLine("</style></head><body>");
            sb.AppendLine("<div class='container'>");
            sb.AppendLine("<h1>公差与配合查询报告</h1>");

            // 基本信息
            sb.AppendLine("<h2>基本参数</h2>");
            sb.AppendFormat("<p>基本尺寸: <strong>Φ{0}</strong> mm</p>", r.BasicSize);
            sb.AppendLine();

            // 孔信息
            sb.AppendLine("<h2>孔公差带</h2>");
            sb.AppendLine("<table>");
            sb.AppendFormat("<tr><th>公差带</th><th>ES (上偏差)</th><th>EI (下偏差)</th><th>公差</th><th>最大极限尺寸</th><th>最小极限尺寸</th></tr>");
            sb.AppendFormat("<tr class='highlight'><td>{0}</td><td>{1} mm</td><td>{2} mm</td><td>{3:F3} mm</td><td>Φ{4:F3}</td><td>Φ{5:F3}</td></tr>",
                r.HoleSpec, FormatDeviation(r.ES_Hole), FormatDeviation(r.EI_Hole),
                r.ES_Hole - r.EI_Hole, r.HoleMax, r.HoleMin);
            sb.AppendLine("</table>");

            // 轴信息
            sb.AppendLine("<h2>轴公差带</h2>");
            sb.AppendLine("<table>");
            sb.AppendFormat("<tr><th>公差带</th><th>es (上偏差)</th><th>ei (下偏差)</th><th>公差</th><th>最大极限尺寸</th><th>最小极限尺寸</th></tr>");
            sb.AppendFormat("<tr class='highlight'><td>{0}</td><td>{1} mm</td><td>{2} mm</td><td>{3:F3} mm</td><td>Φ{4:F3}</td><td>Φ{5:F3}</td></tr>",
                r.ShaftSpec, FormatDeviation(r.ES_Shaft), FormatDeviation(r.EI_Shaft),
                r.ES_Shaft - r.EI_Shaft, r.ShaftMax, r.ShaftMin);
            sb.AppendLine("</table>");

            // 配合信息
            sb.AppendLine("<h2>配合分析</h2>");
            sb.AppendFormat("<p class='fit-type'>{0}</p>", r.FitType);
            sb.AppendLine("<table>");
            if (r.FitType == "间隙配合")
            {
                sb.AppendFormat("<tr><th>参数</th><th>数值</th></tr>");
                sb.AppendFormat("<tr><td>最大间隙</td><td>{0} mm</td></tr>", FormatDeviation(r.MaxClearance));
                sb.AppendFormat("<tr><td>最小间隙</td><td>{0} mm</td></tr>", FormatDeviation(r.MinClearance));
            }
            else if (r.FitType == "过盈配合")
            {
                sb.AppendFormat("<tr><th>参数</th><th>数值</th></tr>");
                sb.AppendFormat("<tr><td>最大过盈</td><td>{0} mm</td></tr>", FormatDeviation(r.MaxInterference));
                sb.AppendFormat("<tr><td>最小过盈</td><td>{0} mm</td></tr>", FormatDeviation(r.MinInterference));
            }
            else
            {
                sb.AppendFormat("<tr><th>参数</th><th>数值</th></tr>");
                sb.AppendFormat("<tr><td>最大间隙</td><td>{0} mm</td></tr>", FormatDeviation(r.MaxClearance));
                sb.AppendFormat("<tr><td>最小过盈</td><td>{0} mm</td></tr>", FormatDeviation(r.MaxInterference));
            }
            sb.AppendLine("</table>");

            // 公差带示意图（文字）
            sb.AppendLine("<h2>公差带示意</h2>");
            sb.AppendLine("<pre style='font-family:Consolas,monospace;font-size:14px;background:#f8f9fa;padding:15px;border-radius:4px;'>");
            sb.AppendFormat("  孔 {0}: |← EI={1} →|← IT={2:F3} →|", r.HoleSpec, FormatDeviation(r.EI_Hole), r.ES_Hole - r.EI_Hole);
            sb.AppendLine();
            sb.AppendFormat("  轴 {0}: |← es={1} →|← IT={2:F3} →|", r.ShaftSpec, FormatDeviation(r.ES_Shaft), r.ES_Shaft - r.EI_Shaft);
            sb.AppendLine();
            sb.AppendLine("</pre>");

            // 参考标准
            sb.AppendLine("<h2>参考标准</h2>");
            sb.AppendLine("<ul>");
            sb.AppendLine("<li>GB/T 1800.1-2009 产品几何技术规范 极限与配合</li>");
            sb.AppendLine("<li>GB/T 1800.2-2009 标准公差数值和基本偏差数值表</li>");
            sb.AppendLine("<li>GB/T 1801-2009 公差带和配合的选择</li>");
            sb.AppendLine("</ul>");

            sb.AppendFormat("<div class='footer'>麦豆宝工具集 - 公差与配合查询模块 | 生成时间: {0}</div>", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendLine("</div></body></html>");

            return sb.ToString();
        }
    }
}
