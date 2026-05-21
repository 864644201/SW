using System;
using System.Text;
using System.Windows.Forms;

namespace ConnDesign
{
    public partial class MainForm : Form
    {
        private string currentType = "平键静连接";

        public MainForm()
        {
            InitializeComponent();
            treeNav.SelectedNode = treeNav.Nodes[0].Nodes[0];
        }

        private void OnNodeSelected(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null) return;
            currentType = e.Node.Text;
            lblTitle.Text = currentType;
            txtResult.Clear();
        }

        private void OnCalculate(object sender, EventArgs e)
        {
            double T, d, L;
            if (!double.TryParse(txtTorque.Text, out T) || T <= 0) { MessageBox.Show("请输入有效转矩"); return; }
            if (!double.TryParse(txtShaftDia.Text, out d) || d <= 0) { MessageBox.Show("请输入有效轴径"); return; }
            if (!double.TryParse(txtKeyLength.Text, out L) || L <= 0) { MessageBox.Show("请输入有效键长"); return; }

            int matIdx = cmbMaterial.SelectedIndex; // 0=钢, 1=铸铁
            int loadIdx = cmbLoadType.SelectedIndex; // 0=静, 1=轻微冲击, 2=冲击

            if (currentType.Contains("平键"))
                CalcFlatKey(T, d, L, matIdx, loadIdx, currentType.Contains("动"));
            else if (currentType.Contains("半圆"))
                CalcWoodruff(T, d, L, matIdx, loadIdx);
            else if (currentType.Contains("楔键"))
                CalcGibKey(T, d, L, matIdx, loadIdx);
            else if (currentType.Contains("切向"))
                CalcTangential(T, d, L, matIdx, loadIdx);
            else if (currentType.Contains("花键"))
                CalcSpline(T, d, L, matIdx, loadIdx, currentType.Contains("渐开线"));
            else
                txtResult.Text = "该功能开发中...";
        }

        private void CalcFlatKey(double T, double d, double L, int matIdx, int loadIdx, bool isDynamic)
        {
            double b, h;
            ConnDesignData.FindKeySection(d, out b, out h);
            lblSection.Text = string.Format("查表得截面尺寸: b={0}mm, h={1}mm", b, h);

            // 键类型影响有效长度
            int keyTypeIdx = cmbKeyType.SelectedIndex;
            double L0;
            if (keyTypeIdx == 1) L0 = L;           // B型: L0 = L
            else if (keyTypeIdx == 2) L0 = L - b / 2.0; // C型: L0 = L - b/2
            else L0 = L - b;                       // A型: L0 = L - b

            double k = h / 2.0; // 接触高度

            if (isDynamic)
            {
                double p = 2.0 * T / (d * k * L0);
                double allowP = ConnDesignData.AllowPressure[matIdx, loadIdx];
                bool ok = p <= allowP;
                var sb = new StringBuilder();
                sb.AppendLine("=== 平键动连接校核 ===");
                sb.AppendFormat("截面尺寸: b={0}mm, h={1}mm\n", b, h);
                sb.AppendFormat("有效接触长度 L0={0:F1}mm\n", L0);
                sb.AppendFormat("接触高度 k={0:F2}mm\n", k);
                sb.AppendLine();
                sb.AppendFormat("压强 p = 2T/(d·k·L0)\n");
                sb.AppendFormat("      = 2×{0:F0}/({1:F1}×{2:F2}×{3:F1})\n", T, d, k, L0);
                sb.AppendFormat("      = {0:F2} MPa\n", p);
                sb.AppendLine();
                sb.AppendFormat("许用压强 [p] = {0} MPa\n", allowP);
                sb.AppendFormat("结论: p {0} [p], {1}\n", ok ? "<=" : ">", ok ? "满足" : "不满足");
                txtResult.Text = sb.ToString();
            }
            else
            {
                double sigma = 2.0 * T / (d * k * L0);
                double allowSigma = ConnDesignData.AllowBearingStress[matIdx, loadIdx];
                bool ok = sigma <= allowSigma;
                var sb = new StringBuilder();
                sb.AppendLine("=== 平键静连接校核 ===");
                sb.AppendFormat("截面尺寸: b={0}mm, h={1}mm\n", b, h);
                sb.AppendFormat("有效接触长度 L0={0:F1}mm\n", L0);
                sb.AppendFormat("接触高度 k={0:F2}mm\n", k);
                sb.AppendLine();
                sb.AppendFormat("挤压应力 σ = 2T/(d·k·L0)\n");
                sb.AppendFormat("           = 2×{0:F0}/({1:F1}×{2:F2}×{3:F1})\n", T, d, k, L0);
                sb.AppendFormat("           = {0:F2} MPa\n", sigma);
                sb.AppendLine();
                sb.AppendFormat("许用挤压应力 [σ] = {0} MPa\n", allowSigma);
                sb.AppendFormat("结论: σ {0} [σ], {1}\n", ok ? "<=" : ">", ok ? "安全" : "不安全");
                txtResult.Text = sb.ToString();
            }
        }

        private void CalcWoodruff(double T, double d, double L, int matIdx, int loadIdx)
        {
            double b, h;
            ConnDesignData.FindKeySection(d, out b, out h);
            lblSection.Text = string.Format("查表得截面尺寸: b={0}mm, h={1}mm (半圆键)", b, h);
            double k = h / 2.0;
            double sigma = 2.0 * T / (d * k * L);
            double allowSigma = ConnDesignData.AllowBearingStress[matIdx, loadIdx];
            bool ok = sigma <= allowSigma;
            var sb = new StringBuilder();
            sb.AppendLine("=== 半圆键连接校核 ===");
            sb.AppendFormat("接触高度 k={0:F2}mm\n", k);
            sb.AppendFormat("挤压应力 σ = 2T/(d·k·L) = {0:F2} MPa\n", sigma);
            sb.AppendFormat("许用挤压应力 [σ] = {0} MPa\n", allowSigma);
            sb.AppendFormat("结论: {0}\n", ok ? "安全" : "不安全");
            txtResult.Text = sb.ToString();
        }

        private void CalcGibKey(double T, double d, double L, int matIdx, int loadIdx)
        {
            double b, h;
            ConnDesignData.FindKeySection(d, out b, out h);
            lblSection.Text = string.Format("查表得截面尺寸: b={0}mm, h={1}mm (楔键)", b, h);
            double mu = 0.14; // 摩擦系数
            double L0 = L;
            double sigma = 12.0 * T / (b * L0 * (b + 6 * mu * d));
            double allowSigma = ConnDesignData.AllowBearingStress[matIdx, loadIdx];
            bool ok = sigma <= allowSigma;
            var sb = new StringBuilder();
            sb.AppendLine("=== 楔键连接校核 ===");
            sb.AppendFormat("摩擦系数 μ=0.14\n");
            sb.AppendFormat("挤压应力 σ = 12T/(b·L·(b+6μd)) = {0:F2} MPa\n", sigma);
            sb.AppendFormat("许用挤压应力 [σ] = {0} MPa\n", allowSigma);
            sb.AppendFormat("结论: {0}\n", ok ? "安全" : "不安全");
            txtResult.Text = sb.ToString();
        }

        private void CalcTangential(double T, double d, double L, int matIdx, int loadIdx)
        {
            lblSection.Text = "切向键: 按轴径查表确定键参数";
            double allowSigma = ConnDesignData.AllowBearingStress[matIdx, loadIdx];
            var sb = new StringBuilder();
            sb.AppendLine("=== 切向键连接校核 ===");
            sb.AppendFormat("轴径 d={0:F1}mm\n", d);
            sb.AppendFormat("转矩 T={0:F0} N·mm\n", T);
            sb.AppendFormat("许用挤压应力 [σ] = {0} MPa\n", allowSigma);
            sb.AppendLine("切向键详细参数请参照 GB/T 1974");
            txtResult.Text = sb.ToString();
        }

        private void CalcSpline(double T, double d, double L, int matIdx, int loadIdx, bool isInvolute)
        {
            lblSection.Text = isInvolute ? "渐开线花键: 按 GB/T 3478" : "矩形花键: 按 GB/T 1144";
            double allowSigma = ConnDesignData.AllowBearingStress[matIdx, loadIdx];
            var sb = new StringBuilder();
            sb.AppendLine(isInvolute ? "=== 渐开线花键连接校核 ===" : "=== 矩形花键连接校核 ===");
            sb.AppendFormat("转矩 T={0:F0} N·mm\n", T);
            sb.AppendFormat("许用挤压应力 [σ] = {0} MPa\n", allowSigma);
            sb.AppendLine(isInvolute
                ? "渐开线花键参数请参照 GB/T 3478 选择"
                : "矩形花键参数请参照 GB/T 1144 选择");
            txtResult.Text = sb.ToString();
        }
    }
}
