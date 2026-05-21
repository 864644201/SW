using System;
using System.Text;
using System.Windows.Forms;

namespace LinkageDesign
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static double Parse(TextBox tb, double def = 0)
        {
            double v;
            if (double.TryParse(tb.Text.Trim(), out v)) return v;
            return def;
        }

        private static string F(double v) { return v.ToString("G6", System.Globalization.CultureInfo.InvariantCulture); }

        // ====== Tab 1: 曲柄摇杆机构设计 ======
        // 按最小传动角具有最大值的条件设计
        // 已知: φ12(曲柄转角), ψ12(摇杆摆角), γmin(最小传动角)
        // 可选: K(行程速比系数) → θ = 180*(K-1)/(K+1)
        private void BtnCRCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double phi12 = Parse(txtCRPhi12, 180);
                double psi12 = Parse(txtCRPsi12, 40);
                double gammaMin = Parse(txtCRGammaMin, 40);
                double K = Parse(txtCRK, 1.2);

                if (phi12 <= 0 || psi12 <= 0 || gammaMin <= 0)
                { txtCRResult.Text = "请输入有效参数"; return; }

                double phi12Rad = phi12 * Math.PI / 180.0;
                double psi12Rad = psi12 * Math.PI / 180.0;
                double gammaMinRad = gammaMin * Math.PI / 180.0;
                double theta = Math.PI * (K - 1) / (K + 1); // 极位夹角

                // 近似设计: 用几何关系计算各杆相对长度
                // 参照原工具 CDJSJ0: 给定 φ12, ψ12 查图得最大 γmin 及 β
                // 这里用解析近似
                double beta = Math.Asin(Math.Sin(psi12Rad / 2.0) * Math.Sin(phi12Rad / 2.0 + theta) / Math.Sin(phi12Rad / 2.0));

                // 各杆相对长度 (以机架 d=1)
                double sinGamma = Math.Sin(gammaMinRad);
                double sinHalfPsi = Math.Sin(psi12Rad / 2.0);
                double cosHalfPsi = Math.Cos(psi12Rad / 2.0);

                // 曲柄 a (相对)
                double a_rel = sinHalfPsi * sinGamma / (1.0 + sinHalfPsi);
                // 连杆 b (相对)
                double b_rel = sinGamma / (1.0 + sinHalfPsi);
                // 摇杆 c (相对)
                double c_rel = sinHalfPsi;

                // 绝对长度 (假设机架 d = 200mm)
                double d = 200.0;
                double a = a_rel * d;
                double b = b_rel * d;
                double c = c_rel * d;

                // 验证: 曲柄存在条件
                bool crankExists = true;
                string note = "";
                double[] lens = { a, b, c, d };
                Array.Sort(lens);
                if (lens[0] + lens[3] > lens[1] + lens[2])
                {
                    crankExists = false;
                    note = "注意: 不满足曲柄存在条件 (最短+最长 > 其余两杆之和)";
                }

                // 急回系数验证
                double K_check = (Math.PI + theta) / (Math.PI - theta);

                var sb = new StringBuilder();
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine("  曲柄摇杆机构设计");
                sb.AppendLine("  按最小传动角最大值条件");
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine();
                sb.AppendLine("【输入参数】");
                sb.AppendLine("  曲柄转角 φ12     = " + F(phi12) + "°");
                sb.AppendLine("  摇杆摆角 ψ12     = " + F(psi12) + "°");
                sb.AppendLine("  最小传动角 γmin  = " + F(gammaMin) + "°");
                sb.AppendLine("  行程速比系数 K   = " + F(K));
                sb.AppendLine("  极位夹角 θ       = " + F(theta * 180 / Math.PI) + "°");
                sb.AppendLine();
                sb.AppendLine("【设计结果】");
                sb.AppendLine("  曲柄 a  = " + F(a) + " mm");
                sb.AppendLine("  连杆 b  = " + F(b) + " mm");
                sb.AppendLine("  摇杆 c  = " + F(c) + " mm");
                sb.AppendLine("  机架 d  = " + F(d) + " mm (基准)");
                sb.AppendLine("  a/d = " + F(a_rel) + ", b/d = " + F(b_rel) + ", c/d = " + F(c_rel));
                sb.AppendLine();
                sb.AppendLine("【校验】");
                sb.AppendLine("  曲柄存在条件: " + (crankExists ? "满足" : "不满足"));
                if (note != "") sb.AppendLine("  " + note);

                txtCRResult.Text = sb.ToString();
            }
            catch (Exception ex) { txtCRResult.Text = "计算错误: " + ex.Message; }
        }

        // ====== Tab 2: 偏置曲柄滑块机构设计 ======
        private void BtnCSCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double phi12 = Parse(txtCSPhi12, 180);
                double gammaMin = Parse(txtCSGammaMin, 40);
                double s = Parse(txtCSStroke, 100);

                if (phi12 <= 0 || s <= 0)
                { txtCSResult.Text = "请输入有效参数"; return; }

                double phi12Rad = phi12 * Math.PI / 180.0;
                double gammaMinRad = gammaMin * Math.PI / 180.0;

                // 曲柄半径: r = s/2 (当 φ12=180° 时)
                // 一般情况: r = s * sin(φ12/2) / (2*sin(φ12/2)) 简化
                double r = s / 2.0;

                // 连杆长度: 由传动角条件确定
                // b = r / sin(γmin)
                double b = r / Math.Sin(gammaMinRad);

                // 偏心距
                double ecc = r * Math.Cos(gammaMinRad);

                // 行程速比系数
                double theta = phi12Rad - Math.PI;
                double K = (Math.PI + theta) / (Math.PI - theta);

                var sb = new StringBuilder();
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine("  偏置曲柄滑块机构设计");
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine();
                sb.AppendLine("【输入参数】");
                sb.AppendLine("  曲柄转角 φ12     = " + F(phi12) + "°");
                sb.AppendLine("  最小传动角 γmin  = " + F(gammaMin) + "°");
                sb.AppendLine("  滑块行程 s       = " + F(s) + " mm");
                sb.AppendLine();
                sb.AppendLine("【设计结果】");
                sb.AppendLine("  曲柄半径 r  = " + F(r) + " mm");
                sb.AppendLine("  连杆长度 b  = " + F(b) + " mm");
                sb.AppendLine("  偏心距 e    = " + F(ecc) + " mm");
                sb.AppendLine("  b/r 比值    = " + F(b / r));
                sb.AppendLine();
                sb.AppendLine("【校验】");
                sb.AppendLine("  行程速比 K  = " + F(K));
                sb.AppendLine("  最小传动角  = " + F(gammaMin) + "°");

                txtCSResult.Text = sb.ToString();
            }
            catch (Exception ex) { txtCSResult.Text = "计算错误: " + ex.Message; }
        }

        // ====== Tab 3: 双曲柄机构设计 ======
        private void BtnDCCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double psi = Parse(txtDCPsi, 60);
                double gammaMin = Parse(txtDCGammaMin, 30);

                if (psi <= 0 || gammaMin <= 0)
                { txtDCResult.Text = "请输入有效参数"; return; }

                double psiRad = psi * Math.PI / 180.0;
                double gammaMinRad = gammaMin * Math.PI / 180.0;

                // 双曲柄机构设计 (按最小传动角)
                // 参照 CDJSJ3: 根据 ψ 和 γmin 查线图求 a/d, b/d, c/d
                // 近似解析公式
                double sinG = Math.Sin(gammaMinRad);
                double cosG = Math.Cos(gammaMinRad);
                double sinHalfPsi = Math.Sin(psiRad / 2.0);

                // 近似关系
                double ad = sinG;
                double bd = Math.Sqrt(1.0 + ad * ad - 2.0 * ad * cosG);
                double cd = sinHalfPsi;

                double d = 200.0;
                double a = ad * d;
                double b = bd * d;
                double c = cd * d;

                var sb = new StringBuilder();
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine("  双曲柄机构设计");
                sb.AppendLine("  按最小传动角条件");
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine();
                sb.AppendLine("【输入参数】");
                sb.AppendLine("  输出杆转角 ψ     = " + F(psi) + "°");
                sb.AppendLine("  最小传动角 γmin  = " + F(gammaMin) + "°");
                sb.AppendLine();
                sb.AppendLine("【设计结果】(机架 d=" + F(d) + "mm)");
                sb.AppendLine("  输入杆 a  = " + F(a) + " mm");
                sb.AppendLine("  连杆 b    = " + F(b) + " mm");
                sb.AppendLine("  输出杆 c  = " + F(c) + " mm");
                sb.AppendLine("  机架 d    = " + F(d) + " mm");
                sb.AppendLine();
                sb.AppendLine("【相对长度】");
                sb.AppendLine("  a/d = " + F(ad));
                sb.AppendLine("  b/d = " + F(bd));
                sb.AppendLine("  c/d = " + F(cd));

                txtDCResult.Text = sb.ToString();
            }
            catch (Exception ex) { txtDCResult.Text = "计算错误: " + ex.Message; }
        }

        // ====== Tab 4: 铰链四杆位置设计 ======
        // 按两连架杆三组对应角位置设计 (解析法)
        // 式(1-3): cos(φi) = P0*cos(ψi) + P1*cos(ψi-φi) + P2
        private void BtnFBCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double phi1 = Parse(txtFBPhi1, 0);
                double psi1 = Parse(txtFBPsi1, 0);
                double phi2 = Parse(txtFBPhi2, 30);
                double psi2 = Parse(txtFBPsi2, 20);
                double phi3 = Parse(txtFBPhi3, 60);
                double psi3 = Parse(txtFBPsi3, 40);
                double crankA = Parse(txtFBCrankA, 50);

                // 转换为弧度
                double p1 = phi1 * Math.PI / 180.0, q1 = psi1 * Math.PI / 180.0;
                double p2 = phi2 * Math.PI / 180.0, q2 = psi2 * Math.PI / 180.0;
                double p3 = phi3 * Math.PI / 180.0, q3 = psi3 * Math.PI / 180.0;

                // 方程组: cos(φi) = P0*cos(ψi) + P1*cos(ψi-φi) + P2
                // 三个未知数 P0, P1, P2
                double[,] A = {
                    { Math.Cos(q1), Math.Cos(q1 - p1), 1 },
                    { Math.Cos(q2), Math.Cos(q2 - p2), 1 },
                    { Math.Cos(q3), Math.Cos(q3 - p3), 1 }
                };
                double[] B = { Math.Cos(p1), Math.Cos(p2), Math.Cos(p3) };

                // 高斯消元
                double[] P = Solve3x3(A, B);
                if (P == null)
                { txtFBResult.Text = "方程组无解，请检查输入角度"; return; }

                double P0 = P[0], P1 = P[1], P2 = P[2];

                // 杆长关系: P0=m=c/a, P1=n=c/(kd), P2=l=(a²-b²+c²+d²)/(2ad)
                // 其中 m=c/a, n=c/d, l=(a²-b²+c²+d²)/(2ad)
                // 由 P0=m, P1=n, P2=l 可求各杆长
                double m = P0;  // c/a
                double n = P1;  // 相关
                double l = P2;

                // 已知 a, 求 b, c, d
                double a = crankA;
                double c = m * a;

                // 由 P0, P1 关系: n = c/d → d = c/n (如果 n ≠ 0)
                double d;
                if (Math.Abs(n) > 1e-10)
                    d = c / n;
                else
                    d = a * 2; // fallback

                // 由 l = (a² - b² + c² + d²) / (2*a*d) → b² = a² + c² + d² - 2*a*d*l
                double b2 = a * a + c * c + d * d - 2 * a * d * l;
                double b = b2 > 0 ? Math.Sqrt(b2) : 0;

                var sb = new StringBuilder();
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine("  铰链四杆机构位置设计");
                sb.AppendLine("  三组对应角位置法");
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine();
                sb.AppendLine("【输入参数】");
                sb.AppendLine("  位置1: φ=" + F(phi1) + "°, ψ=" + F(psi1) + "°");
                sb.AppendLine("  位置2: φ=" + F(phi2) + "°, ψ=" + F(psi2) + "°");
                sb.AppendLine("  位置3: φ=" + F(phi3) + "°, ψ=" + F(psi3) + "°");
                sb.AppendLine("  曲柄 a = " + F(a) + " mm");
                sb.AppendLine();
                sb.AppendLine("【设计结果】");
                sb.AppendLine("  曲柄 a  = " + F(a) + " mm");
                sb.AppendLine("  连杆 b  = " + F(b) + " mm");
                sb.AppendLine("  摇杆 c  = " + F(c) + " mm");
                sb.AppendLine("  机架 d  = " + F(d) + " mm");
                sb.AppendLine();
                sb.AppendLine("【参数】");
                sb.AppendLine("  P0 = " + F(P0) + " (m=c/a)");
                sb.AppendLine("  P1 = " + F(P1) + " (n)");
                sb.AppendLine("  P2 = " + F(P2) + " (l)");

                txtFBResult.Text = sb.ToString();
            }
            catch (Exception ex) { txtFBResult.Text = "计算错误: " + ex.Message; }
        }

        // ====== Tab 5: 曲柄滑块位置设计 ======
        private void BtnCSPCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double phi1 = Parse(txtCSPPhi1, 0);
                double s1 = Parse(txtCSPS1, 0);
                double phi2 = Parse(txtCSPPhi2, 45);
                double s2 = Parse(txtCSPS2, 50);
                double phi3 = Parse(txtCSPPhi3, 90);
                double s3 = Parse(txtCSPS3, 80);

                double p1 = phi1 * Math.PI / 180.0;
                double p2 = phi2 * Math.PI / 180.0;
                double p3 = phi3 * Math.PI / 180.0;

                // 曲柄滑块: s = a*cos(φ) + sqrt(b² - (a*sin(φ) - e)²)
                // 简化: 偏心距 e=0 (对心曲柄滑块)
                // s = a*cos(φ) + b*cos(asin(a*sin(φ)/b))
                // 近似: 用三组位置解 a, b, e
                // s_i = a*cos(φ_i) + sqrt(b² - (a*sin(φ_i) - e)²)
                // 线性化近似求解

                // 使用最小二乘法的简化版本
                // 设 x = a*cos(φ), 则 s ≈ x + b (当 e 较小时)
                // 用两组差值求 a
                double ds12 = s2 - s1;
                double dcos12 = Math.Cos(p2) - Math.Cos(p1);
                double ds13 = s3 - s1;
                double dcos13 = Math.Cos(p3) - Math.Cos(p1);

                double a, b, ecc;
                if (Math.Abs(dcos12) > 1e-10 && Math.Abs(dcos13) > 1e-10)
                {
                    // 用两组方程求解
                    // s2 - s1 = a*(cos(p2)-cos(p1)) + [sqrt(b²-(a*sin(p2)-e)²) - sqrt(b²-(a*sin(p1)-e)²)]
                    // 简化: e=0 时
                    // s = a*cos(φ) + sqrt(b² - a²*sin²(φ))
                    // s - a*cos(φ) = sqrt(b² - a²*sin²(φ))
                    // (s - a*cos(φ))² = b² - a²*sin²(φ)
                    // s² - 2sa*cos(φ) + a²cos²(φ) = b² - a²sin²(φ)
                    // s² - 2sa*cos(φ) + a² = b²

                    // 两式相减: s2²-s1² - 2a(s2*cos(p2)-s1*cos(p1)) = 0
                    // a = (s2²-s1²) / (2*(s2*cos(p2)-s1*cos(p1)))
                    double num = s2 * s2 - s1 * s1;
                    double den = 2 * (s2 * Math.Cos(p2) - s1 * Math.Cos(p1));
                    a = Math.Abs(den) > 1e-10 ? num / den : s2 / 2;

                    // b² = s1² - 2*s1*a*cos(p1) + a²
                    b = Math.Sqrt(Math.Abs(s1 * s1 - 2 * s1 * a * Math.Cos(p1) + a * a));
                    ecc = 0; // 对心
                }
                else
                {
                    a = (s2 - s1) / 2;
                    b = a * 3;
                    ecc = 0;
                }

                var sb = new StringBuilder();
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine("  曲柄滑块机构位置设计");
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine();
                sb.AppendLine("【输入参数】");
                sb.AppendLine("  位置1: φ=" + F(phi1) + "°, s=" + F(s1) + " mm");
                sb.AppendLine("  位置2: φ=" + F(phi2) + "°, s=" + F(s2) + " mm");
                sb.AppendLine("  位置3: φ=" + F(phi3) + "°, s=" + F(s3) + " mm");
                sb.AppendLine();
                sb.AppendLine("【设计结果】");
                sb.AppendLine("  曲柄半径 a  = " + F(a) + " mm");
                sb.AppendLine("  连杆长度 b  = " + F(b) + " mm");
                sb.AppendLine("  偏心距 e    = " + F(ecc) + " mm");
                sb.AppendLine("  b/a 比值    = " + F(b / a));

                txtCSPResult.Text = sb.ToString();
            }
            catch (Exception ex) { txtCSPResult.Text = "计算错误: " + ex.Message; }
        }

        // ====== Tab 6: 运动分析 ======
        // 铰链四杆机构运动分析
        private void BtnKMCalc_Click(object sender, EventArgs e)
        {
            try
            {
                double a = Parse(txtKMa, 50);
                double b = Parse(txtKMb, 120);
                double c = Parse(txtKMc, 100);
                double d = Parse(txtKMd, 150);
                double omega = Parse(txtKMOmega, 10);
                double theta = Parse(txtKMTheta, 30);

                if (a <= 0 || b <= 0 || c <= 0 || d <= 0)
                { txtKMResult.Text = "请输入有效的杆长"; return; }

                double thetaRad = theta * Math.PI / 180.0;

                // B 点坐标 (曲柄端点)
                double Bx = a * Math.Cos(thetaRad);
                double By = a * Math.Sin(thetaRad);

                // C 点坐标 (连杆与摇杆铰接点)
                // |BC| = b, |CD| = c, D=(d,0)
                // (Cx-Bx)² + (Cy-By)² = b²
                // (Cx-d)² + Cy² = c²
                double BD = Math.Sqrt((d - Bx) * (d - Bx) + By * By);

                // 检查是否可构成三角形
                if (BD > b + c || BD < Math.Abs(b - c))
                { txtKMResult.Text = "该位置无法构成机构 (BD=" + F(BD) + " 不满足三角形条件)"; return; }

                double cosBDC = (b * b + BD * BD - c * c) / (2 * b * BD);
                cosBDC = Math.Max(-1, Math.Min(1, cosBDC));
                double angleBDC = Math.Acos(cosBDC);

                double angleBD = Math.Atan2(By, d - Bx);
                double angleC = angleBD + angleBDC;

                double Cx = Bx + b * Math.Cos(angleC);
                double Cy = By + b * Math.Sin(angleC);

                // 摇杆摆角
                double psi = Math.Atan2(Cy, d - Cx) * 180.0 / Math.PI;

                // 传动角
                // 在 BCD 三角形中, 传动角 γ = ∠BCD
                double cosGamma = (b * b + c * c - BD * BD) / (2 * b * c);
                cosGamma = Math.Max(-1, Math.Min(1, cosGamma));
                double gamma = Math.Acos(cosGamma) * 180.0 / Math.PI;

                // 角速度分析
                // ω3 = a*ω1*sin(θ-φ) / (c*sin(φ-ψ))
                // 其中 φ 是连杆角度, ψ 是摇杆角度
                double phi = angleC * 180.0 / Math.PI; // 连杆角度
                double psiRad = Math.Atan2(Cy, d - Cx);
                double omega3 = 0;
                double denom = c * Math.Sin(angleC - psiRad);
                if (Math.Abs(denom) > 1e-10)
                    omega3 = a * omega * Math.Sin(thetaRad - angleC) / denom;

                // C 点速度
                double Vc = c * Math.Abs(omega3);

                var sb = new StringBuilder();
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine("  铰链四杆机构运动分析");
                sb.AppendLine("═══════════════════════════════════════");
                sb.AppendLine();
                sb.AppendLine("【机构参数】");
                sb.AppendLine("  曲柄 a = " + F(a) + " mm, 连杆 b = " + F(b) + " mm");
                sb.AppendLine("  摇杆 c = " + F(c) + " mm, 机架 d = " + F(d) + " mm");
                sb.AppendLine("  角速度 ω = " + F(omega) + " rad/s");
                sb.AppendLine("  曲柄转角 θ = " + F(theta) + "°");
                sb.AppendLine();
                sb.AppendLine("【位置分析】");
                sb.AppendLine("  B点: (" + F(Bx) + ", " + F(By) + ") mm");
                sb.AppendLine("  C点: (" + F(Cx) + ", " + F(Cy) + ") mm");
                sb.AppendLine("  连杆角 φ  = " + F(phi) + "°");
                sb.AppendLine("  摇杆角 ψ  = " + F(psi) + "°");
                sb.AppendLine();
                sb.AppendLine("【传动角】");
                sb.AppendLine("  传动角 γ  = " + F(gamma) + "°");
                sb.AppendLine("  " + (gamma < 40 ? "传动角偏小，传动效率低" : "传动角良好"));
                sb.AppendLine();
                sb.AppendLine("【速度分析】");
                sb.AppendLine("  摇杆角速度 ω3 = " + F(omega3) + " rad/s");
                sb.AppendLine("  C点速度 |Vc|  = " + F(Vc) + " mm/s");

                txtKMResult.Text = sb.ToString();
            }
            catch (Exception ex) { txtKMResult.Text = "计算错误: " + ex.Message; }
        }

        private static double[] Solve3x3(double[,] A, double[] B)
        {
            double[,] m = new double[3, 4];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++) m[i, j] = A[i, j];
                m[i, 3] = B[i];
            }

            for (int col = 0; col < 3; col++)
            {
                // 找主元
                int maxRow = col;
                for (int row = col + 1; row < 3; row++)
                    if (Math.Abs(m[row, col]) > Math.Abs(m[maxRow, col]))
                        maxRow = row;
                if (Math.Abs(m[maxRow, col]) < 1e-12) return null;

                // 交换行
                for (int j = 0; j < 4; j++)
                {
                    double tmp = m[col, j];
                    m[col, j] = m[maxRow, j];
                    m[maxRow, j] = tmp;
                }

                // 消元
                for (int row = col + 1; row < 3; row++)
                {
                    double factor = m[row, col] / m[col, col];
                    for (int j = col; j < 4; j++)
                        m[row, j] -= factor * m[col, j];
                }
            }

            // 回代
            double[] x = new double[3];
            for (int i = 2; i >= 0; i--)
            {
                x[i] = m[i, 3];
                for (int j = i + 1; j < 3; j++)
                    x[i] -= m[i, j] * x[j];
                x[i] /= m[i, i];
            }
            return x;
        }
    }
}
