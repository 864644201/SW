using System;

namespace SpringDesign
{
    /// <summary>
    /// 压缩/拉伸弹簧计算结果
    /// </summary>
    public class CompressionExtensionResult
    {
        // 输入参数回显
        public double d;          // 丝径 (mm)
        public double D;          // 中径 (mm)
        public int n;             // 有效圈数
        public double t;          // 节距 (mm)
        public double G;          // 切变模量 (MPa)
        public double tauAllow;   // 许用切应力 (MPa)

        // 计算结果
        public double C;          // 旋绕比
        public double K;          // 曲度系数 (Wahl)
        public double k;          // 弹簧刚度 (N/mm)
        public double F_max;      // 最大工作载荷 (N)
        public double tau_max;    // 最大切应力 (MPa)
        public double H0;         // 自由高度 (mm)
        public double Hs;         // 压并高度 (mm)
        public double F1;         // 最小工作载荷 (N)
        public double Fn;         // 最大工作载荷 (N)
        public double f1;         // 最小工作载荷变形量 (mm)
        public double fn;         // 最大工作载荷变形量 (mm)
        public double fj;         // 工作极限载荷变形量 (mm)
        public double Fj;         // 工作极限载荷 (N)
        public double tau_j;      // 工作极限切应力 (MPa)
        public double L_wire;     // 展开长度 (mm)
        public double n_total;    // 总圈数
        public double stabilityRatio; // 高径比 H0/D
        public bool stabilityOk;  // 稳定性校核
        public double tau_m;      // 平均切应力 (MPa)
        public double tau_a;      // 切应力幅 (MPa)
        public double tau_max_fatigue; // 最大切应力 (疲劳) (MPa)
        public double D1;         // 内径 (mm)
        public double D2;         // 外径 (mm)
        public double ratio_d2;   // 最大外径比
        public double ratio_d1;   // 最小内径比
        public string endType;    // 端部类型

        // 载荷类别
        public string loadCategoryDesc;
    }

    /// <summary>
    /// 扭转弹簧计算结果
    /// </summary>
    public class TorsionResult
    {
        // 输入参数回显
        public double d;          // 丝径 (mm)
        public double D;          // 中径 (mm)
        public int n;             // 有效圈数
        public double E;          // 弹性模量 (MPa)
        public double sigmaAllow; // 许用弯曲应力 (MPa)

        // 计算结果
        public double C;          // 旋绕比
        public double Ki;         // 曲度系数
        public double k;          // 扭转刚度 (N*mm/deg)
        public double k_rad;      // 扭转刚度 (N*mm/rad)
        public double M_max;      // 最大扭矩 (N*mm)
        public double sigma_max;  // 最大弯曲应力 (MPa)
        public double D1;         // 内径 (mm)
        public double D2;         // 外径 (mm)
        public double L_wire;     // 钢丝展开长度 (mm)
        public double alpha_max;  // 最大扭转角 (deg)
        public double L0;         // 自由长度 (mm)
    }

    /// <summary>
    /// 弹簧计算器 - 核心工程计算
    /// 包含圆柱螺旋压缩弹簧、拉伸弹簧和扭转弹簧的设计计算
    /// </summary>
    public static class SpringCalculator
    {
        #region 压缩弹簧计算

        /// <summary>
        /// 压缩弹簧完整设计计算
        /// </summary>
        /// <param name="d">丝径 (mm)</param>
        /// <param name="D">中径 (mm)</param>
        /// <param name="n">有效圈数</param>
        /// <param name="G">切变模量 (MPa)</param>
        /// <param name="tauAllow">许用切应力 (MPa)</param>
        /// <param name="F1">最小工作载荷 (N)，可选</param>
        /// <param name="Fn">最大工作载荷 (N)，可选</param>
        /// <param name="endType">端部类型: "yI"(两端圈并紧磨平), "yII"(两端圈并紧不磨平)</param>
        public static CompressionExtensionResult CalcCompression(
            double d, double D, int n, double G, double tauAllow,
            double F1 = 0, double Fn = 0, string endType = "yI")
        {
            if (d <= 0) throw new ArgumentException("丝径 d 必须大于零");
            if (D <= 0) throw new ArgumentException("中径 D 必须大于零");
            if (n <= 0) throw new ArgumentException("有效圈数 n 必须大于零");
            if (D <= d) throw new ArgumentException("中径 D 必须大于丝径 d");

            var r = new CompressionExtensionResult();
            r.d = d;
            r.D = D;
            r.n = n;
            r.G = G;
            r.tauAllow = tauAllow;
            r.F1 = F1;
            r.Fn = Fn;
            r.endType = endType;

            // 1. 旋绕比 C = D/d
            r.C = D / d;

            // 2. 曲度系数 (Wahl 修正系数)
            // K = (4C - 1) / (4C - 4) + 0.615 / C
            r.K = (4.0 * r.C - 1.0) / (4.0 * r.C - 4.0) + 0.615 / r.C;

            // 3. 弹簧刚度 k = G*d^4 / (8*D^3*n)
            r.k = G * Math.Pow(d, 4) / (8.0 * Math.Pow(D, 3) * n);

            // 4. 最大工作载荷 F_max = tau_allow * pi * d^3 / (8 * K * D)
            r.F_max = tauAllow * Math.PI * Math.Pow(d, 3) / (8.0 * r.K * D);

            // 5. 节距 t (假设常用范围)
            // 对于压缩弹簧，推荐 C=4~16，节距 t 一般在 (0.3~0.5)*D
            r.t = d + r.F_max / (r.k * n);  // 由最大变形量反推节距

            // 6. 自由高度 H0 (两端圈并紧磨平 - squared and ground ends)
            // H0 = n*t + 2*d (压缩弹簧，两端并紧磨平)
            if (endType == "yI")
            {
                // 两端圈并紧磨平 (YI型)
                r.n_total = n + 2.0;  // 总圈数 = 有效圈数 + 2支承圈
                r.H0 = n * r.t + 2.0 * d;
            }
            else
            {
                // 两端圈并紧不磨平 (YII型)
                r.n_total = n + 2.5;
                r.H0 = n * r.t + 3.0 * d;
            }

            // 7. 压并高度 Hs
            // 两端并紧磨平: Hs = (n_total - 0.5) * d ≈ (n + 1.5) * d
            if (endType == "yI")
            {
                r.Hs = (n + 1.5) * d;
            }
            else
            {
                r.Hs = (n + 2.0) * d;
            }

            // 8. 内径、外径
            r.D1 = D - d;   // 内径
            r.D2 = D + d;   // 外径

            // 9. 变形量计算
            if (F1 > 0)
            {
                r.f1 = F1 / r.k;
            }
            if (Fn > 0)
            {
                r.fn = Fn / r.k;
            }

            // 工作极限载荷 (取 tau_j = 0.56 * sigma_b，近似III类上限)
            // 一般工作极限载荷取最大工作载荷的 1.25 倍
            r.Fj = r.F_max * 1.25;
            r.fj = r.Fj / r.k;

            // 工作极限切应力
            r.tau_j = 8.0 * r.K * r.Fj * D / (Math.PI * Math.Pow(d, 3));

            // 10. 最大切应力 (在最大工作载荷下)
            if (Fn > 0)
            {
                r.tau_max = 8.0 * r.K * Fn * D / (Math.PI * Math.Pow(d, 3));
            }
            else
            {
                r.tau_max = tauAllow;
            }

            // 11. 稳定性校核
            // 两端固定: H0/D <= 5.3
            // 一端固定一端铰支: H0/D <= 3.7
            // 两端铰支: H0/D <= 2.6
            r.stabilityRatio = r.H0 / D;
            r.stabilityOk = r.stabilityRatio <= 5.3; // 两端固定

            // 12. 展开长度 L = pi * D * n_total / cos(alpha)
            // alpha = arctan(t / (pi*D))
            double alpha = Math.Atan(r.t / (Math.PI * D));
            r.L_wire = Math.PI * D * r.n_total / Math.Cos(alpha);

            // 13. 最大外径比 (与安装空间相关)
            r.ratio_d2 = r.D2 / D;
            r.ratio_d1 = r.D1 / D;

            return r;
        }

        /// <summary>
        /// 根据已知参数校核压缩弹簧
        /// </summary>
        public static CompressionExtensionResult CheckCompression(
            double d, double D, int n, double G, double tauAllow,
            double F_min, double F_max, string endType = "yI")
        {
            var r = CalcCompression(d, D, n, G, tauAllow, F_min, F_max, endType);

            // 疲劳校核 (Goodman 图)
            // tau_m = tau_min + tau_max) / 2
            // tau_a = (tau_max - tau_min) / 2
            double tau_min = (F_min > 0) ? 8.0 * r.K * F_min * D / (Math.PI * Math.Pow(d, 3)) : 0;
            double tau_max = 8.0 * r.K * F_max * D / (Math.PI * Math.Pow(d, 3));

            r.tau_m = (tau_min + tau_max) / 2.0;
            r.tau_a = (tau_max - tau_min) / 2.0;
            r.tau_max_fatigue = tau_max;

            return r;
        }

        #endregion

        #region 拉伸弹簧计算

        /// <summary>
        /// 拉伸弹簧完整设计计算
        /// 拉伸弹簧的计算与压缩弹簧类似，但没有压并高度和稳定性问题
        /// </summary>
        /// <param name="d">丝径 (mm)</param>
        /// <param name="D">中径 (mm)</param>
        /// <param name="n">有效圈数</param>
        /// <param name="G">切变模量 (MPa)</param>
        /// <param name="tauAllow">许用切应力 (MPa)，拉伸弹簧取压缩的 0.8 倍</param>
        /// <param name="F1">最小工作载荷 (N)</param>
        /// <param name="Fn">最大工作载荷 (N)</param>
        /// <param name="initialTension">初拉力 (N)，可选</param>
        public static CompressionExtensionResult CalcExtension(
            double d, double D, int n, double G, double tauAllow,
            double F1 = 0, double Fn = 0, double initialTension = 0)
        {
            if (d <= 0) throw new ArgumentException("丝径 d 必须大于零");
            if (D <= 0) throw new ArgumentException("中径 D 必须大于零");
            if (n <= 0) throw new ArgumentException("有效圈数 n 必须大于零");
            if (D <= d) throw new ArgumentException("中径 D 必须大于丝径 d");

            var r = new CompressionExtensionResult();
            r.d = d;
            r.D = D;
            r.n = n;
            r.G = G;
            r.tauAllow = tauAllow;
            r.F1 = F1;
            r.Fn = Fn;
            r.endType = "拉伸弹簧";

            // 1. 旋绕比 C = D/d
            r.C = D / d;

            // 2. 曲度系数 (Wahl 修正系数)
            // 拉伸弹簧同样使用 Wahl 系数
            r.K = (4.0 * r.C - 1.0) / (4.0 * r.C - 4.0) + 0.615 / r.C;

            // 3. 弹簧刚度 k = G*d^4 / (8*D^3*n)
            // 拉伸弹簧由于各圈紧密接触，有效圈数即为总圈数
            r.k = G * Math.Pow(d, 4) / (8.0 * Math.Pow(D, 3) * n);

            // 4. 最大工作载荷 F_max = tau_allow * pi * d^3 / (8 * K * D)
            r.F_max = tauAllow * Math.PI * Math.Pow(d, 3) / (8.0 * r.K * D);

            // 5. 拉伸弹簧没有节距和压并高度概念
            r.t = 0;
            r.Hs = 0;

            // 6. 自由长度 (拉伸弹簧)
            // H0 = (n+1)*d + 2*hook_length
            // 钩环长度一般取 1~2 圈，简化为:
            r.n_total = n + 2.0; // 包含两端钩环
            r.H0 = n * d + 2.0 * d; // 简化计算，实际取决于钩型

            // 7. 内径、外径
            r.D1 = D - d;
            r.D2 = D + d;

            // 8. 变形量计算 (含初拉力)
            if (F1 > 0)
            {
                r.f1 = (F1 - initialTension) / r.k;
                if (r.f1 < 0) r.f1 = 0;
            }
            if (Fn > 0)
            {
                r.fn = (Fn - initialTension) / r.k;
            }

            // 9. 工作极限载荷
            r.Fj = r.F_max * 1.25;
            r.fj = (r.Fj - initialTension) / r.k;

            // 10. 最大切应力
            if (Fn > 0)
            {
                r.tau_max = 8.0 * r.K * Fn * D / (Math.PI * Math.Pow(d, 3));
            }
            else
            {
                r.tau_max = tauAllow;
            }

            // 11. 拉伸弹簧无稳定性问题
            r.stabilityRatio = 0;
            r.stabilityOk = true;

            // 12. 展开长度 (拉伸弹簧，含钩环)
            // L = pi * D * n + 钩环展开长度
            r.L_wire = Math.PI * D * n + 2.0 * Math.PI * D; // 简化

            // 13. 工作极限切应力
            r.tau_j = 8.0 * r.K * r.Fj * D / (Math.PI * Math.Pow(d, 3));

            // 14. 最大外径比
            r.ratio_d2 = r.D2 / D;
            r.ratio_d1 = r.D1 / D;

            return r;
        }

        /// <summary>
        /// 拉伸弹簧疲劳校核
        /// </summary>
        public static CompressionExtensionResult CheckExtension(
            double d, double D, int n, double G, double tauAllow,
            double F_min, double F_max, double initialTension = 0)
        {
            var r = CalcExtension(d, D, n, G, tauAllow, F_min, F_max, initialTension);

            // 疲劳校核
            double tau_min = (F_min > 0) ? 8.0 * r.K * F_min * D / (Math.PI * Math.Pow(d, 3)) : 0;
            double tau_max = 8.0 * r.K * F_max * D / (Math.PI * Math.Pow(d, 3));

            r.tau_m = (tau_min + tau_max) / 2.0;
            r.tau_a = (tau_max - tau_min) / 2.0;
            r.tau_max_fatigue = tau_max;

            return r;
        }

        #endregion

        #region 扭转弹簧计算

        /// <summary>
        /// 扭转弹簧完整设计计算
        /// </summary>
        /// <param name="d">丝径 (mm)</param>
        /// <param name="D">中径 (mm)</param>
        /// <param name="n">有效圈数</param>
        /// <param name="E">弹性模量 (MPa)</param>
        /// <param name="sigmaAllow">许用弯曲应力 (MPa)</param>
        /// <param name="M_work">工作扭矩 (N*mm)，可选</param>
        public static TorsionResult CalcTorsion(
            double d, double D, int n, double E, double sigmaAllow,
            double M_work = 0)
        {
            if (d <= 0) throw new ArgumentException("丝径 d 必须大于零");
            if (D <= 0) throw new ArgumentException("中径 D 必须大于零");
            if (n <= 0) throw new ArgumentException("有效圈数 n 必须大于零");
            if (D <= d) throw new ArgumentException("中径 D 必须大于丝径 d");

            var r = new TorsionResult();
            r.d = d;
            r.D = D;
            r.n = n;
            r.E = E;
            r.sigmaAllow = sigmaAllow;

            // 1. 旋绕比 C = D/d
            r.C = D / d;

            // 2. 曲度系数 Ki (扭转弹簧)
            // Ki = (4*C^2 - C - 1) / (4*C*(C - 1))
            r.Ki = (4.0 * r.C * r.C - r.C - 1.0) / (4.0 * r.C * (r.C - 1.0));

            // 3. 扭转刚度
            // k = E*d^4 / (10.186 * D * n)   (N*mm/deg)
            // 或 k = E*d^4 / (64 * D * n * pi)  (N*mm/rad)
            // 两种表达式等价: 10.186 ≈ 64/pi/2 ≈ 32/pi ... 实际上:
            // k (N*mm/rad) = E*d^4 / (64 * D * n)   -- 这是正确的公式
            // k (N*mm/deg) = E*d^4 / (64 * D * n) * (pi/180)
            r.k_rad = E * Math.Pow(d, 4) / (64.0 * D * n);
            r.k = r.k_rad * Math.PI / 180.0; // 转换为每度

            // 4. 最大扭矩 M_max = sigma_allow * pi * d^3 / (32 * Ki)
            r.M_max = sigmaAllow * Math.PI * Math.Pow(d, 3) / (32.0 * r.Ki);

            // 5. 最大弯曲应力 (在工作扭矩下)
            if (M_work > 0)
            {
                r.sigma_max = 32.0 * M_work * r.Ki / (Math.PI * Math.Pow(d, 3));
            }
            else
            {
                r.sigma_max = sigmaAllow;
            }

            // 6. 内径、外径
            r.D1 = D - d;
            r.D2 = D + d;

            // 7. 最大扭转角 (度)
            r.alpha_max = r.M_max / r.k;

            // 8. 钢丝展开长度
            // L = pi * D * n + 两端臂长 (简化为 2*pi*D)
            r.L_wire = Math.PI * D * n + 2.0 * Math.PI * D * 0.5;

            // 9. 自由长度 (扭转弹簧轴向长度)
            // L0 ≈ n*(d + 间隙) + 两端臂厚
            // 紧密缠绕时 L0 ≈ n*d + 2*d
            r.L0 = n * d + 2.0 * d;

            return r;
        }

        /// <summary>
        /// 扭转弹簧校核计算
        /// </summary>
        public static TorsionResult CheckTorsion(
            double d, double D, int n, double E, double sigmaAllow,
            double M_min, double M_max)
        {
            var r = CalcTorsion(d, D, n, E, sigmaAllow, M_max);

            // 疲劳校核用弯曲应力
            double sigma_min = 32.0 * M_min * r.Ki / (Math.PI * Math.Pow(d, 3));
            double sigma_max = 32.0 * M_max * r.Ki / (Math.PI * Math.Pow(d, 3));

            // 可用于 Goodman 图疲劳校核
            // sigma_m = (sigma_min + sigma_max) / 2
            // sigma_a = (sigma_max - sigma_min) / 2

            return r;
        }

        #endregion

        #region 辅助计算方法

        /// <summary>
        /// 计算旋绕比 C = D/d
        /// </summary>
        public static double CalcSpringIndex(double D, double d)
        {
            return D / d;
        }

        /// <summary>
        /// 计算 Wahl 曲度系数 K = (4C-1)/(4C-4) + 0.615/C
        /// </summary>
        public static double CalcWahlFactor(double C)
        {
            if (C <= 1.0) throw new ArgumentException("旋绕比 C 必须大于 1");
            return (4.0 * C - 1.0) / (4.0 * C - 4.0) + 0.615 / C;
        }

        /// <summary>
        /// 计算压缩/拉伸弹簧刚度 k = G*d^4/(8*D^3*n)
        /// </summary>
        public static double CalcStiffness(double G, double d, double D, int n)
        {
            return G * Math.Pow(d, 4) / (8.0 * Math.Pow(D, 3) * n);
        }

        /// <summary>
        /// 计算扭转弹簧刚度 (N*mm/deg) = E*d^4/(10.186*D*n)
        /// </summary>
        public static double CalcTorsionStiffness(double E, double d, double D, int n)
        {
            return E * Math.Pow(d, 4) / (10.186 * D * n);
        }

        /// <summary>
        /// 计算切应力 tau = 8*K*F*D/(pi*d^3)
        /// </summary>
        public static double CalcShearStress(double K, double F, double D, double d)
        {
            return 8.0 * K * F * D / (Math.PI * Math.Pow(d, 3));
        }

        /// <summary>
        /// 计算最大工作载荷 F_max = tau_allow*pi*d^3/(8*K*D)
        /// </summary>
        public static double CalcMaxLoad(double tauAllow, double d, double D, double K)
        {
            return tauAllow * Math.PI * Math.Pow(d, 3) / (8.0 * K * D);
        }

        /// <summary>
        /// 计算自由高度 H0 = n*t + 2*d (压缩弹簧，两端并紧磨平)
        /// </summary>
        public static double CalcFreeHeight_Compression(int n, double t, double d, bool ground = true)
        {
            return ground ? n * t + 2.0 * d : n * t + 3.0 * d;
        }

        /// <summary>
        /// 计算压并高度 Hs = (n+1.5)*d (压缩弹簧，两端并紧磨平)
        /// </summary>
        public static double CalcSolidHeight(int n, double d, bool ground = true)
        {
            return ground ? (n + 1.5) * d : (n + 2.0) * d;
        }

        /// <summary>
        /// 稳定性校核: b = H0/D <= 5.3 (两端固定)
        /// </summary>
        /// <returns>true 表示满足稳定性要求</returns>
        public static bool CheckStability(double H0, double D, double limitRatio = 5.3)
        {
            return (H0 / D) <= limitRatio;
        }

        /// <summary>
        /// 旋绕比范围校核: 推荐 C = 4 ~ 16
        /// </summary>
        public static bool CheckSpringIndex(double C, double minC = 4.0, double maxC = 16.0)
        {
            return C >= minC && C <= maxC;
        }

        /// <summary>
        /// 计算弯曲应力 (扭转弹簧) sigma = 32*M*Ki/(pi*d^3)
        /// </summary>
        public static double CalcBendingStress_Torsion(double M, double Ki, double d)
        {
            return 32.0 * M * Ki / (Math.PI * Math.Pow(d, 3));
        }

        /// <summary>
        /// 计算扭转弹簧最大扭矩 M_max = sigma_allow*pi*d^3/(32*Ki)
        /// </summary>
        public static double CalcMaxTorque(double sigmaAllow, double d, double Ki)
        {
            return sigmaAllow * Math.PI * Math.Pow(d, 3) / (32.0 * Ki);
        }

        /// <summary>
        /// 计算扭转弹簧曲度系数 Ki = (4C^2-C-1)/(4C*(C-1))
        /// </summary>
        public static double CalcTorsionFactor(double C)
        {
            if (C <= 1.0) throw new ArgumentException("旋绕比 C 必须大于 1");
            return (4.0 * C * C - C - 1.0) / (4.0 * C * (C - 1.0));
        }

        /// <summary>
        /// 疲劳校核 - Goodman 图法
        /// </summary>
        /// <param name="tau_m">平均切应力 (MPa)</param>
        /// <param name="tau_a">切应力幅 (MPa)</param>
        /// <param name="tau_0">脉动循环疲劳极限 (MPa)</param>
        /// <param name="tau_b">对称循环疲劳极限 (MPa)</param>
        /// <returns>安全系数</returns>
        public static double CalcFatigueSafetyFactor_Goodman(
            double tau_m, double tau_a, double tau_0, double tau_b)
        {
            // Goodman 线: tau_a / tau_b + tau_m / tau_0 <= 1
            // 安全系数 n = 1 / (tau_a/tau_b + tau_m/tau_0)
            double ratio = tau_a / tau_b + tau_m / tau_0;
            if (ratio <= 0) return double.MaxValue;
            return 1.0 / ratio;
        }

        /// <summary>
        /// 疲劳校核 - 静强度安全系数
        /// </summary>
        /// <param name="tauAllow">许用切应力 (MPa)</param>
        /// <param name="tauMax">最大切应力 (MPa)</param>
        /// <returns>安全系数</returns>
        public static double CalcStaticSafetyFactor(double tauAllow, double tauMax)
        {
            if (tauMax <= 0) return double.MaxValue;
            return tauAllow / tauMax;
        }

        /// <summary>
        /// 计算推荐丝径 (根据载荷和中径反推)
        /// d = (8*K*F*D / (pi*tau_allow))^(1/3)
        /// </summary>
        public static double CalcRecommendedWireDia(double F, double D, double tauAllow, double K)
        {
            return Math.Pow(8.0 * K * F * D / (Math.PI * tauAllow), 1.0 / 3.0);
        }

        #endregion
    }
}
