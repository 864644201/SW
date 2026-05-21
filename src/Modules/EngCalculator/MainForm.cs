using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EngCalculator
{
    public partial class MainForm : Form
    {
        private static readonly Dictionary<string, string> CategoryNames = new Dictionary<string, string>
        {
            { "01", "常用公式计算" },
            { "02", "工程力学/材料力学" },
            { "03", "数学计算" },
            { "04", "其他计算" },
            { "05", "机械设计计算" },
            { "07", "其他" },
            { "08", "单位换算" },
            { "09", "其他计算" },
            { "10", "其他" },
            { "11", "其他计算" },
            { "12", "其他" },
            { "13", "其他计算" },
        };

        private static readonly Dictionary<string, Dictionary<string, string>> SubNames =
            new Dictionary<string, Dictionary<string, string>>
        {
            { "01", new Dictionary<string, string>
                {
                    { "01", "三角形" }, { "02", "四边形" }, { "03", "圆" }, { "04", "梯形" },
                    { "05", "正多边形" }, { "06", "圆弧/弓形" }, { "07", "椭圆" }, { "08", "扇形" },
                    { "09", "菱形" }, { "10", "平行四边形" }, { "11", "五边形" }, { "12", "六边形" },
                    { "13", "不规则图形" }, { "14", "抛物线" }, { "15", "双曲线" }, { "16", "其他" },
                }
            },
            { "02", new Dictionary<string, string>
                {
                    { "01", "截面力学特性" }, { "06", "梁的弯曲" }, { "07", "扭转" },
                    { "08", "压杆稳定" }, { "09", "应力集中" }, { "10", "组合变形" },
                }
            },
            { "05", new Dictionary<string, string>
                {
                    { "01", "螺纹连接" }, { "02", "齿轮设计" }, { "03", "蜗杆传动" },
                    { "04", "带传动" }, { "05", "链传动" }, { "06", "弹簧设计" },
                }
            },
        };

        private string _formulaDir;

        public MainForm()
        {
            InitializeComponent();
            _formulaDir = FindFormulaDir();
            if (_formulaDir != null)
                BuildFormulaTree();
            txtExpression.Focus();
        }

        private string FindFormulaDir()
        {
            // Try output directory first
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "formulas");
            if (Directory.Exists(path)) return path;

            // Try source directory
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"..\..\..\src\Modules\EngCalculator\formulas");
            if (Directory.Exists(path)) return Path.GetFullPath(path);

            return null;
        }

        private void BuildFormulaTree()
        {
            treeFormulas.BeginUpdate();
            try
            {
                var files = Directory.GetFiles(_formulaDir, "*.bmp");
                if (files.Length == 0) return;

                var tree = new SortedDictionary<string, SortedDictionary<string, List<string>>>();

                foreach (var file in files)
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    var parts = name.Split('.');
                    if (parts.Length < 2) continue;

                    string cat1 = parts[0];
                    string cat2 = parts.Length >= 2 ? parts[1] : "";

                    if (!tree.ContainsKey(cat1))
                        tree[cat1] = new SortedDictionary<string, List<string>>();
                    if (!tree[cat1].ContainsKey(cat2))
                        tree[cat1][cat2] = new List<string>();
                    tree[cat1][cat2].Add(name);
                }

                foreach (var cat1 in tree)
                {
                    string cat1Name = CategoryNames.ContainsKey(cat1.Key)
                        ? cat1.Key + " " + CategoryNames[cat1.Key]
                        : cat1.Key;
                    var node1 = new TreeNode(cat1Name);

                    foreach (var cat2 in cat1.Value)
                    {
                        string label = cat1.Key + "." + cat2.Key;
                        var subs = SubNames.ContainsKey(cat1.Key) ? SubNames[cat1.Key] : null;
                        if (subs != null && subs.ContainsKey(cat2.Key))
                            label += " " + subs[cat2.Key];

                        var node2 = new TreeNode(label);
                        foreach (var imgName in cat2.Value)
                        {
                            node2.Nodes.Add(new TreeNode(imgName) { Tag = imgName + ".bmp" });
                        }
                        node1.Nodes.Add(node2);
                    }

                    treeFormulas.Nodes.Add(node1);
                }
            }
            finally
            {
                treeFormulas.EndUpdate();
            }
        }

        private void TreeFormulas_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null)
            {
                picFormula.Image = null;
                return;
            }

            string fileName = e.Node.Tag.ToString();
            string filePath = Path.Combine(_formulaDir, fileName);
            if (File.Exists(filePath))
            {
                try
                {
                    if (picFormula.Image != null)
                    {
                        var old = picFormula.Image;
                        picFormula.Image = null;
                        old.Dispose();
                    }
                    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        picFormula.Image = new Bitmap(stream);
                    }
                }
                catch
                {
                    picFormula.Image = null;
                }
            }
        }

        private void BtnCalc_Click(object sender, EventArgs e)
        {
            DoCalc();
        }

        private void TxtExpression_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoCalc();
                e.SuppressKeyPress = true;
            }
        }

        private void DoCalc()
        {
            string expr = txtExpression.Text.Trim();
            if (string.IsNullOrEmpty(expr))
            {
                txtResult.Text = "请输入表达式";
                return;
            }

            try
            {
                var variables = ParseVariables(txtVarInput.Text);
                double result = ExpressionEvaluator.Evaluate(expr, variables);
                string resultStr = FormatResult(result);
                txtResult.Text = expr + " = " + resultStr;
            }
            catch (Exception ex)
            {
                txtResult.Text = "错误: " + ex.Message;
            }
        }

        private Dictionary<string, double> ParseVariables(string input)
        {
            var vars = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
            if (string.IsNullOrWhiteSpace(input) || input.StartsWith("如:"))
                return vars;

            var matches = Regex.Matches(input, @"([a-zA-Z_]\w*)\s*=\s*([+-]?\d*\.?\d+)");
            foreach (Match m in matches)
            {
                string name = m.Groups[1].Value;
                double val = double.Parse(m.Groups[2].Value,
                    System.Globalization.CultureInfo.InvariantCulture);
                vars[name] = val;
            }
            return vars;
        }

        private string FormatResult(double value)
        {
            if (double.IsNaN(value)) return "NaN";
            if (double.IsInfinity(value)) return value > 0 ? "∞" : "-∞";

            if (Math.Abs(value) < 1e-12) return "0";
            if (Math.Abs(value) >= 1e9 || (Math.Abs(value) < 1e-3 && value != 0))
                return value.ToString("G8", System.Globalization.CultureInfo.InvariantCulture);
            return value.ToString("G10", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
