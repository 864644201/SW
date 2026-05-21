using System;
using System.Text;
using System.Windows.Forms;

namespace FrictionWheel
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static double ParseDouble(TextBox tb, double defaultVal = 0)
        {
            double v;
            if (double.TryParse(tb.Text.Trim(), out v)) return v;
            return defaultVal;
        }

        // ====== 圆柱摩擦轮 ======
        private void BtnCylCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double P1 = ParseDouble(txtCylP1);
                double n1 = ParseDouble(txtCyln1);
                double n2 = ParseDouble(txtCyln2);
                double mu = ParseDouble(txtCylMu, 0.1);
                double sigmaH = ParseDouble(txtCylSigmaH);
                double Ka = ParseDouble(txtCylKa, 1.0);
                double epsilon = ParseDouble(txtCylEpsilon, 1.0) / 100.0;
                double delta = ParseDouble(txtCylDelta, 2.0);
                double psi = ParseDouble(txtCylPsi, 0.3);
                bool external = cboCylContact.SelectedIndex == 0;

                if (P1 <= 0 || n1 <= 0) { txtCylResult.Text = "请输入功率和转速"; return; }

                // 传动比
                double i = n2 > 0 ? n1 / n2 : ParseDouble(txtCyli);
                if (i <= 0) { txtCylResult.Text = "请输入传动比或从动轴转速"; return; }
                if (n2 <= 0) n2 = n1 / i;

                // 主动轮扭矩
                double T1 = 9550.0 * P1 / n1; // N·m

                // 计算主动轮直径 D1 (按接触强度)
                // 圆柱摩擦轮: D1 = sqrt(2*Ka*T1*(i+1) / (pi*psi*mu*[sigmaH]^2*(1-eps)*i))
                // 注意: 公式源自接触力学简化
                double D1;
                if (sigmaH > 0)
                {
                    double num = 2.0 * Ka * T1 * (i + (external ? 1 : -1));
                    double den = Math.PI * psi * mu * sigmaH * sigmaH * (1 - epsilon) * i;
                    D1 = Math.Pow(Math.Abs(num / den), 1.0 / 3.0) * 1000.0; // m -> mm
                }
                else
                {
                    D1 = 0;
                }

                // 从动轮直径
                double D2 = i * D1 * (1 - epsilon);

                // 中心距
                double a;
                if (external)
                    a = (D1 + D2) / 2.0 + delta;
                else
                    a = (D1 - D2) / 2.0 + delta;

                // 轮宽
                double b = psi * D1;

                // 压紧力
                double Fn = Ka * T1 / (mu * D1 / 2000.0); // D1 in mm, convert to m
                double Q = Fn; // 圆柱: Q = Fn

                // 接触应力校核
                double sigmaH_calc = 0;
                if (D1 > 0 && b > 0)
                {
                    double r1 = D1 / 2000.0;
                    double r2 = D2 / 2000.0;
                    double rho = external ? r1 * r2 / (r1 + r2) : r1 * r2 / (r2 - r1);
                    sigmaH_calc = Math.Sqrt(Fn / (Math.PI * b / 1000.0 * rho)) / 1e6;
                }

                var sb = new StringBuilder();
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine("  圆柱摩擦轮传动设计计算结果");
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine();
                sb.AppendLine("【输入参数】");
                sb.AppendLine("  传动功率 P1     = " + F(P1) + " kW");
                sb.AppendLine("  主动轴转速 n1   = " + F(n1) + " r/min");
                sb.AppendLine("  从动轴转速 n2   = " + F(n2) + " r/min");
                sb.AppendLine("  传动比 i        = " + F(i));
                sb.AppendLine("  接触形式        = " + (external ? "外接触" : "内接触"));
                sb.AppendLine("  摩擦系数 μ      = " + F(mu));
                sb.AppendLine("  工况系数 Ka     = " + F(Ka));
                sb.AppendLine();
                sb.AppendLine("【计算结果】");
                sb.AppendLine("  主动轮扭矩 T1   = " + F(T1) + " N·m");
                sb.AppendLine("  主动轮直径 D1   = " + F(D1) + " mm");
                sb.AppendLine("  从动轮直径 D2   = " + F(D2) + " mm");
                sb.AppendLine("  中心距 a        = " + F(a) + " mm");
                sb.AppendLine("  轮宽 b          = " + F(b) + " mm");
                sb.AppendLine("  压紧力 Q        = " + F(Q) + " N");
                if (sigmaH > 0)
                    sb.AppendLine("  计算接触应力 σH = " + F(sigmaH_calc) + " MPa (许用: " + F(sigmaH) + ")");

                txtCylResult.Text = sb.ToString();
            }
            catch (Exception ex) { txtCylResult.Text = "计算错误: " + ex.Message; }
        }

        // ====== 槽形摩擦轮 ======
        private void BtnGroCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double P1 = ParseDouble(txtGroP1);
                double n1 = ParseDouble(txtGron1);
                double n2 = ParseDouble(txtGron2);
                double mu = ParseDouble(txtGroMu, 0.1);
                double sigmaH = ParseDouble(txtGroSigmaH);
                double Ka = ParseDouble(txtGroKa, 1.0);
                double epsilon = ParseDouble(txtGroEpsilon, 1.0) / 100.0;
                double delta = ParseDouble(txtGroDelta, 2.0);
                double z = ParseDouble(txtGroZ, 5);
                double beta = ParseDouble(txtGroBeta, 15.0);
                bool external = cboGroContact.SelectedIndex == 0;

                if (P1 <= 0 || n1 <= 0) { txtGroResult.Text = "请输入功率和转速"; return; }

                double i = n2 > 0 ? n1 / n2 : ParseDouble(txtGroi);
                if (i <= 0) { txtGroResult.Text = "请输入传动比或从动轴转速"; return; }
                if (n2 <= 0) n2 = n1 / i;

                double T1 = 9550.0 * P1 / n1;
                double betaRad = beta * Math.PI / 180.0;

                // 槽形: D1 按接触强度
                double D1;
                if (sigmaH > 0)
                {
                    double num = 2.0 * Ka * T1 * (i + (external ? 1 : -1));
                    double den = Math.PI * z * mu * sigmaH * sigmaH * (1 - epsilon) * i * Math.Sin(betaRad / 2.0);
                    D1 = Math.Pow(Math.Abs(num / den), 1.0 / 3.0) * 1000.0;
                }
                else
                {
                    D1 = 0;
                }

                double D2 = i * D1 * (1 - epsilon);
                double a = external ? (D1 + D2) / 2.0 + delta : (D1 - D2) / 2.0 + delta;

                // 每槽法向力
                double Fn = Ka * T1 / (z * mu * D1 / 2000.0 * Math.Sin(betaRad / 2.0));
                // 压紧力
                double Q = 2.0 * z * Fn * Math.Sin(betaRad / 2.0);

                var sb = new StringBuilder();
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine("  槽形摩擦轮传动设计计算结果");
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine();
                sb.AppendLine("【输入参数】");
                sb.AppendLine("  传动功率 P1     = " + F(P1) + " kW");
                sb.AppendLine("  主动轴转速 n1   = " + F(n1) + " r/min");
                sb.AppendLine("  从动轴转速 n2   = " + F(n2) + " r/min");
                sb.AppendLine("  传动比 i        = " + F(i));
                sb.AppendLine("  接触形式        = " + (external ? "外接触" : "内接触"));
                sb.AppendLine("  摩擦系数 μ      = " + F(mu));
                sb.AppendLine("  沟槽数 z        = " + F0(z));
                sb.AppendLine("  楔角 β          = " + F(beta) + "°");
                sb.AppendLine();
                sb.AppendLine("【计算结果】");
                sb.AppendLine("  主动轮扭矩 T1   = " + F(T1) + " N·m");
                sb.AppendLine("  主动轮直径 D1   = " + F(D1) + " mm");
                sb.AppendLine("  从动轮直径 D2   = " + F(D2) + " mm");
                sb.AppendLine("  中心距 a        = " + F(a) + " mm");
                sb.AppendLine("  每槽法向力 Fn   = " + F(Fn) + " N");
                sb.AppendLine("  压紧力 Q        = " + F(Q) + " N");

                txtGroResult.Text = sb.ToString();
            }
            catch (Exception ex) { txtGroResult.Text = "计算错误: " + ex.Message; }
        }

        // ====== 端面摩擦轮 ======
        private void BtnEfCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double P1 = ParseDouble(txtEfP1);
                double n1 = ParseDouble(txtEfn1);
                double n2 = ParseDouble(txtEfn2);
                double mu = ParseDouble(txtEfMu, 0.1);
                double sigmaH = ParseDouble(txtEfSigmaH);
                double Ka = ParseDouble(txtEfKa, 1.0);
                double epsilon = ParseDouble(txtEfEpsilon, 1.0) / 100.0;
                double psi = ParseDouble(txtEfPsi, 0.3);

                if (P1 <= 0 || n1 <= 0) { txtEfResult.Text = "请输入功率和转速"; return; }

                double i = n2 > 0 ? n1 / n2 : ParseDouble(txtEfi);
                if (i <= 0) { txtEfResult.Text = "请输入传动比或从动轴转速"; return; }
                if (n2 <= 0) n2 = n1 / i;

                double T1 = 9550.0 * P1 / n1;

                // 端面: D1 按接触强度
                double D1;
                if (sigmaH > 0)
                {
                    double num = 4.0 * Ka * T1;
                    double den = Math.PI * psi * mu * sigmaH * sigmaH * (1 - epsilon) * i;
                    D1 = Math.Pow(Math.Abs(num / den), 1.0 / 3.0) * 1000.0;
                }
                else
                {
                    D1 = 0;
                }

                double D2 = i * D1 * (1 - epsilon);
                double a = (D1 + D2) / 2.0; // 端面: 无间隙
                double b = psi * D1;

                double Fn = Ka * T1 / (mu * D1 / 2000.0);
                double Q = Fn;

                var sb = new StringBuilder();
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine("  端面摩擦轮传动设计计算结果");
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine();
                sb.AppendLine("【输入参数】");
                sb.AppendLine("  传动功率 P1     = " + F(P1) + " kW");
                sb.AppendLine("  主动轴转速 n1   = " + F(n1) + " r/min");
                sb.AppendLine("  从动轴转速 n2   = " + F(n2) + " r/min");
                sb.AppendLine("  传动比 i        = " + F(i));
                sb.AppendLine("  摩擦系数 μ      = " + F(mu));
                sb.AppendLine();
                sb.AppendLine("【计算结果】");
                sb.AppendLine("  主动轮扭矩 T1   = " + F(T1) + " N·m");
                sb.AppendLine("  主动轮直径 D1   = " + F(D1) + " mm");
                sb.AppendLine("  从动轮直径 D2   = " + F(D2) + " mm");
                sb.AppendLine("  中心距 a        = " + F(a) + " mm");
                sb.AppendLine("  接触宽度 b      = " + F(b) + " mm");
                sb.AppendLine("  法向力 Fn       = " + F(Fn) + " N");
                sb.AppendLine("  压紧力 Q        = " + F(Q) + " N");

                txtEfResult.Text = sb.ToString();
            }
            catch (Exception ex) { txtEfResult.Text = "计算错误: " + ex.Message; }
        }

        // ====== 圆锥摩擦轮 ======
        private void BtnConCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double P1 = ParseDouble(txtConP1);
                double n1 = ParseDouble(txtConn1);
                double n2 = ParseDouble(txtConn2);
                double mu = ParseDouble(txtConMu, 0.1);
                double sigmaH = ParseDouble(txtConSigmaH);
                double Ka = ParseDouble(txtConKa, 1.0);
                double epsilon = ParseDouble(txtConEpsilon, 1.0) / 100.0;
                double psi = ParseDouble(txtConPsi, 0.3);
                double delta1 = ParseDouble(txtConDelta1, 45.0);

                if (P1 <= 0 || n1 <= 0) { txtConResult.Text = "请输入功率和转速"; return; }

                double i = n2 > 0 ? n1 / n2 : ParseDouble(txtConi);
                if (i <= 0) { txtConResult.Text = "请输入传动比或从动轴转速"; return; }
                if (n2 <= 0) n2 = n1 / i;

                double T1 = 9550.0 * P1 / n1;
                double delta1Rad = delta1 * Math.PI / 180.0;
                double delta2 = 90.0 - delta1; // 假设 Σ = 90°
                double delta2Rad = delta2 * Math.PI / 180.0;

                // 圆锥: D1 按接触强度
                double D1;
                if (sigmaH > 0)
                {
                    double sinD1 = Math.Sin(delta1Rad);
                    double num = 4.0 * Ka * T1 * Math.Sqrt(1 + i * i);
                    double den = Math.PI * psi * mu * sigmaH * sigmaH * (1 - epsilon) * sinD1 * sinD1 * i;
                    D1 = Math.Pow(Math.Abs(num / den), 1.0 / 3.0) * 1000.0;
                }
                else
                {
                    D1 = 0;
                }

                double D2 = i * D1 * (1 - epsilon);

                // 锥距
                double L1 = D1 / (2.0 * Math.Sin(delta1Rad));
                double L2 = D2 / (2.0 * Math.Sin(delta2Rad));
                double b = psi * L1;

                // 平均半径
                double Rm1 = (D1 / 2.0 - b * Math.Sin(delta1Rad) / 2.0) / 1000.0;
                double Rm2 = (D2 / 2.0 - b * Math.Sin(delta2Rad) / 2.0) / 1000.0;

                // 法向力
                double Fn = Ka * T1 / (mu * Rm1);
                // 压紧力
                double Q = Fn * Math.Sin(delta2Rad);

                var sb = new StringBuilder();
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine("  圆锥摩擦轮传动设计计算结果");
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine();
                sb.AppendLine("【输入参数】");
                sb.AppendLine("  传动功率 P1     = " + F(P1) + " kW");
                sb.AppendLine("  主动轴转速 n1   = " + F(n1) + " r/min");
                sb.AppendLine("  从动轴转速 n2   = " + F(n2) + " r/min");
                sb.AppendLine("  传动比 i        = " + F(i));
                sb.AppendLine("  摩擦系数 μ      = " + F(mu));
                sb.AppendLine("  主动轮锥角 δ1   = " + F(delta1) + "°");
                sb.AppendLine();
                sb.AppendLine("【计算结果】");
                sb.AppendLine("  主动轮扭矩 T1   = " + F(T1) + " N·m");
                sb.AppendLine("  主动轮大径 D1   = " + F(D1) + " mm");
                sb.AppendLine("  从动轮大径 D2   = " + F(D2) + " mm");
                sb.AppendLine("  从动轮锥角 δ2   = " + F(delta2) + "°");
                sb.AppendLine("  锥距 L1         = " + F(L1) + " mm");
                sb.AppendLine("  接触宽度 b      = " + F(b) + " mm");
                sb.AppendLine("  法向力 Fn       = " + F(Fn) + " N");
                sb.AppendLine("  压紧力 Q        = " + F(Q) + " N");

                txtConResult.Text = sb.ToString();
            }
            catch (Exception ex) { txtConResult.Text = "计算错误: " + ex.Message; }
        }

        private static string F(double v) { return v.ToString("G6", System.Globalization.CultureInfo.InvariantCulture); }
        private static string F0(double v) { return ((int)v).ToString(); }
    }
}
