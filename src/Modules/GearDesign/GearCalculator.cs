using System;

namespace GearDesign
{
    /// <summary>
    /// 渐开线直齿圆柱齿轮设计计算器
    /// 依据 GB/T 3480-1997 / ISO 6336 标准
    /// </summary>
    public class GearCalculator
    {
        #region 输入参数

        /// <summary>传递功率 P (kW)</summary>
        public double Power { get; set; }

        /// <summary>小齿轮转速 n1 (rpm)</summary>
        public double SpeedN1 { get; set; }

        /// <summary>传动比 i</summary>
        public double RatioI { get; set; }

        /// <summary>小齿轮齿数 Z1</summary>
        public int Z1 { get; set; }

        /// <summary>大齿轮齿数 Z2</summary>
        public int Z2 { get; set; }

        /// <summary>模数 m (mm)</summary>
        public double ModuleM { get; set; }

        /// <summary>压力角 alpha (度), 标准值20°</summary>
        public double Alpha { get; set; } = 20.0;

        /// <summary>齿宽系数 phi_d</summary>
        public double PhiD { get; set; }

        /// <summary>小齿轮材料</summary>
        public GearMaterial Material1 { get; set; }

        /// <summary>大齿轮材料</summary>
        public GearMaterial Material2 { get; set; }

        /// <summary>精度等级 (6-10)</summary>
        public int PrecisionGrade { get; set; } = 7;

        /// <summary>齿顶高系数 ha* (标准值1.0)</summary>
        public double HaCoeff { get; set; } = 1.0;

        /// <summary>顶隙系数 c* (标准值0.25)</summary>
        public double CcCoeff { get; set; } = 0.25;

        /// <summary>小齿轮转矩 T1 (N·mm) - 用于自定义</summary>
        public double? CustomT1 { get; set; }

        #endregion

        #region 几何参数结果

        /// <summary>小齿轮分度圆直径 d1 (mm)</summary>
        public double D1 { get; private set; }

        /// <summary>大齿轮分度圆直径 d2 (mm)</summary>
        public double D2 { get; private set; }

        /// <summary>小齿轮齿顶圆直径 da1 (mm)</summary>
        public double Da1 { get; private set; }

        /// <summary>大齿轮齿顶圆直径 da2 (mm)</summary>
        public double Da2 { get; private set; }

        /// <summary>小齿轮齿根圆直径 df1 (mm)</summary>
        public double Df1 { get; private set; }

        /// <summary>大齿轮齿根圆直径 df2 (mm)</summary>
        public double Df2 { get; private set; }

        /// <summary>中心距 a (mm)</summary>
        public double CenterDist { get; private set; }

        /// <summary>齿宽 b (mm)</summary>
        public double FaceWidth { get; private set; }

        /// <summary>小齿轮齿顶高 ha (mm)</summary>
        public double Ha { get; private set; }

        /// <summary>小齿轮齿根高 hf (mm)</summary>
        public double Hf { get; private set; }

        /// <summary>全齿高 h (mm)</summary>
        public double TotalHeight { get; private set; }

        /// <summary>齿距 p (mm)</summary>
        public double Pitch { get; private set; }

        /// <summary>基圆直径 db1 (mm)</summary>
        public double Db1 { get; private set; }

        /// <summary>基圆直径 db2 (mm)</summary>
        public double Db2 { get; private set; }

        #endregion

        #region 载荷参数结果

        /// <summary>小齿轮转矩 T1 (N·mm)</summary>
        public double T1 { get; private set; }

        /// <summary>大齿轮转速 n2 (rpm)</summary>
        public double SpeedN2 { get; private set; }

        /// <summary>圆周力 Ft (N)</summary>
        public double Ft { get; private set; }

        /// <summary>径向力 Fr (N)</summary>
        public double Fr { get; private set; }

        /// <summary>法向力 Fn (N)</summary>
        public double Fn { get; private set; }

        /// <summary>圆周速度 v (m/s)</summary>
        public double Velocity { get; private set; }

        #endregion

        #region 载荷系数

        /// <summary>使用系数 KA (可由外部设置)</summary>
        public double KA { get; set; }

        /// <summary>动载系数 Kv</summary>
        public double Kv { get; private set; }

        /// <summary>齿向载荷分布系数 KHbeta</summary>
        public double KHbeta { get; private set; }

        /// <summary>齿间载荷分配系数 KHalpha</summary>
        public double KHalpha { get; private set; }

        /// <summary>载荷系数 K = KA * Kv * KHbeta * KHalpha</summary>
        public double K { get; private set; }

        #endregion

        #region 强度校核结果

        /// <summary>接触应力 sigma_H (MPa)</summary>
        public double SigmaH { get; private set; }

        /// <summary>许用接触应力 sigma_HP (MPa)</summary>
        public double SigmaHP { get; private set; }

        /// <summary>接触强度安全系数 SH</summary>
        public double SH { get; private set; }

        /// <summary>弯曲应力 sigma_F1 (MPa) - 小齿轮</summary>
        public double SigmaF1 { get; private set; }

        /// <summary>弯曲应力 sigma_F2 (MPa) - 大齿轮</summary>
        public double SigmaF2 { get; private set; }

        /// <summary>许用弯曲应力 sigma_FP1 (MPa)</summary>
        public double SigmaFP1 { get; private set; }

        /// <summary>许用弯曲应力 sigma_FP2 (MPa)</summary>
        public double SigmaFP2 { get; private set; }

        /// <summary>弯曲强度安全系数 SF1 - 小齿轮</summary>
        public double SF1 { get; private set; }

        /// <summary>弯曲强度安全系数 SF2 - 大齿轮</summary>
        public double SF2 { get; private set; }

        /// <summary>接触疲劳强度校核是否通过</summary>
        public bool ContactStrengthOK { get; private set; }

        /// <summary>弯曲疲劳强度校核是否通过</summary>
        public bool BendingStrengthOK { get; private set; }

        #endregion

        #region 寿命与尺寸系数

        /// <summary>接触强度寿命系数 ZN1</summary>
        public double ZN1 { get; private set; }

        /// <summary>接触强度寿命系数 ZN2</summary>
        public double ZN2 { get; private set; }

        /// <summary>弯曲强度寿命系数 YN1</summary>
        public double YN1 { get; private set; }

        /// <summary>弯曲强度寿命系数 YN2</summary>
        public double YN2 { get; private set; }

        /// <summary>接触强度尺寸系数 Zx</summary>
        public double Zx { get; private set; }

        /// <summary>弯曲强度尺寸系数 Yx</summary>
        public double Yx { get; private set; }

        /// <summary>寿命系数使用说明</summary>
        public string LifeCoeffNote { get; private set; }

        #endregion

        #region 区域系数与弹性系数

        /// <summary>节点区域系数 ZH</summary>
        public double ZH { get; private set; }

        /// <summary>弹性系数 ZE (sqrt(MPa))</summary>
        public double ZE { get; private set; }

        /// <summary>重合度系数 Zeps</summary>
        public double Zeps { get; private set; }

        /// <summary>重合度系数 Yeps</summary>
        public double Yeps { get; private set; }

        #endregion

        #region 重合度参数

        /// <summary>端面重合度 epsilon_alpha</summary>
        public double EpsilonAlpha { get; private set; }

        #endregion

        /// <summary>
        /// 执行完整的齿轮设计计算
        /// </summary>
        public void Calculate()
        {
            ValidateInputs();
            CalculateGeometry();
            CalculateLoad();
            CalculateLoadCoefficients();
            CalculateStrengthCoefficients();
            CalculateContactStrength();
            CalculateBendingStrength();
        }

        private void ValidateInputs()
        {
            if (Power <= 0) throw new ArgumentException("传递功率必须大于0");
            if (SpeedN1 <= 0) throw new ArgumentException("小齿轮转速必须大于0");
            if (RatioI <= 0) throw new ArgumentException("传动比必须大于0");
            if (Z1 < 17) throw new ArgumentException("小齿轮齿数不应少于17（避免根切）");
            if (Z2 < 17) throw new ArgumentException("大齿轮齿数不应少于17（避免根切）");
            if (ModuleM <= 0) throw new ArgumentException("模数必须大于0");
            if (Alpha <= 0 || Alpha >= 45) throw new ArgumentException("压力角应在0°~45°之间");
            if (PhiD <= 0 || PhiD > 2.0) throw new ArgumentException("齿宽系数应在0~2.0之间");
            if (Material1 == null) throw new ArgumentException("请选择小齿轮材料");
            if (Material2 == null) throw new ArgumentException("请选择大齿轮材料");
            if (PrecisionGrade < 6 || PrecisionGrade > 10) throw new ArgumentException("精度等级应在6~10之间");
        }

        /// <summary>
        /// 几何参数计算
        /// </summary>
        private void CalculateGeometry()
        {
            double alphaRad = Alpha * Math.PI / 180.0;

            // 分度圆直径
            D1 = ModuleM * Z1;
            D2 = ModuleM * Z2;

            // 齿顶高、齿根高、全齿高
            Ha = HaCoeff * ModuleM;
            Hf = (HaCoeff + CcCoeff) * ModuleM;
            TotalHeight = Ha + Hf;

            // 齿顶圆直径
            Da1 = D1 + 2.0 * Ha;
            Da2 = D2 + 2.0 * Ha;

            // 齿根圆直径
            Df1 = D1 - 2.0 * Hf;
            Df2 = D2 - 2.0 * Hf;

            // 基圆直径
            Db1 = D1 * Math.Cos(alphaRad);
            Db2 = D2 * Math.Cos(alphaRad);

            // 中心距
            CenterDist = (D1 + D2) / 2.0;

            // 齿宽 (取整到0.5mm)
            FaceWidth = Math.Round(PhiD * D1 * 2, MidpointRounding.AwayFromZero) / 2.0;
            if (FaceWidth < 8) FaceWidth = 8; // 最小齿宽

            // 齿距
            Pitch = Math.PI * ModuleM;

            // 端面重合度 (近似公式, 适用于标准直齿轮)
            // epsilon_alpha = [sqrt(da1^2 - db1^2) + sqrt(da2^2 - db2^2) - 2*a*sin(alpha)] / (pi*m*cos(alpha))
            double term1 = Math.Sqrt(Da1 * Da1 - Db1 * Db1);
            double term2 = Math.Sqrt(Da2 * Da2 - Db2 * Db2);
            double term3 = 2.0 * CenterDist * Math.Sin(alphaRad);
            EpsilonAlpha = (term1 + term2 - term3) / (Math.PI * ModuleM * Math.Cos(alphaRad));
        }

        /// <summary>
        /// 载荷计算
        /// </summary>
        private void CalculateLoad()
        {
            // 大齿轮转速
            SpeedN2 = SpeedN1 / RatioI;

            // 小齿轮转矩 T1 = 9550 * P / n1 (N·m) -> 转换为 N·mm
            if (CustomT1.HasValue)
            {
                T1 = CustomT1.Value;
            }
            else
            {
                T1 = 9550.0 * Power / SpeedN1 * 1000.0; // N·mm
            }

            // 圆周力 Ft = 2000 * T1 / d1 (N) (T1 in N·mm, d1 in mm -> Ft in N)
            Ft = 2.0 * T1 / D1;

            double alphaRad = Alpha * Math.PI / 180.0;
            // 径向力 Fr = Ft * tan(alpha)
            Fr = Ft * Math.Tan(alphaRad);

            // 法向力 Fn = Ft / cos(alpha)
            Fn = Ft / Math.Cos(alphaRad);

            // 圆周速度 v = pi * d1 * n1 / (60 * 1000) (m/s)
            Velocity = Math.PI * D1 * SpeedN1 / (60.0 * 1000.0);
        }

        /// <summary>
        /// 载荷系数计算
        /// 依据 GB/T 3480 / ISO 6336
        /// </summary>
        private void CalculateLoadCoefficients()
        {
            // 使用系数 KA - 由外部设置 (ReadInputs中已赋值)
            // 均匀平稳: 1.0; 轻微冲击: 1.25; 中等冲击: 1.5; 严重冲击: 1.75
            // 若未设置则默认1.0
            if (KA <= 0) KA = 1.0;

            // 动载系数 Kv
            // 根据齿轮精度等级和圆周速度确定
            Kv = CalculateKv();

            // 齿向载荷分布系数 KHbeta
            // 与齿宽、支撑刚度、精度等级相关
            KHbeta = CalculateKHbeta();

            // 齿间载荷分配系数 KHalpha
            KHalpha = CalculateKHalpha();

            // 总载荷系数
            K = KA * Kv * KHbeta * KHalpha;
        }

        /// <summary>
        /// 动载系数 Kv 计算
        /// 依据 GB/T 3480-1997 方法B
        /// </summary>
        private double CalculateKv()
        {
            // 根据精度等级和速度查表 (简化计算)
            // Kv = 1 + K1 * v / (K2 + v) 的近似形式
            double k1, k2;

            switch (PrecisionGrade)
            {
                case 6:
                    k1 = 0.06; k2 = 8.0;
                    break;
                case 7:
                    k1 = 0.10; k2 = 6.0;
                    break;
                case 8:
                    k1 = 0.16; k2 = 4.0;
                    break;
                case 9:
                    k1 = 0.25; k2 = 2.5;
                    break;
                case 10:
                    k1 = 0.40; k2 = 1.5;
                    break;
                default:
                    k1 = 0.10; k2 = 6.0;
                    break;
            }

            double kv = 1.0 + k1 * Velocity / (k2 + Velocity);
            return Math.Round(kv, 4);
        }

        /// <summary>
        /// 齿向载荷分布系数 KHbeta
        /// 依据经验公式
        /// </summary>
        private double CalculateKHbeta()
{
            // 简化公式: KHbeta = 1.0 + Cbeta * (b/d1)^2
            // Cbeta 与支撑刚度和精度相关
            double cbeta;
            switch (PrecisionGrade)
            {
                case 6: cbeta = 0.005; break;
                case 7: cbeta = 0.008; break;
                case 8: cbeta = 0.012; break;
                case 9: cbeta = 0.018; break;
                case 10: cbeta = 0.025; break;
                default: cbeta = 0.008; break;
            }

            double bd1Ratio = FaceWidth / D1;
            double khbeta = 1.0 + cbeta * bd1Ratio * bd1Ratio * 100.0;

            // 限值
            if (khbeta < 1.0) khbeta = 1.0;
            if (khbeta > 2.0) khbeta = 2.0;

            return Math.Round(khbeta, 4);
        }

        /// <summary>
        /// 齿间载荷分配系数 KHalpha
        /// </summary>
        private double CalculateKHalpha()
        {
            // 与重合度相关
            // KHalpha ≈ 1 / (0.45 * epsilon_alpha + 0.55) 的简化形式
            // 但受精度等级影响
            double khalpha;

            if (EpsilonAlpha > 1.0)
            {
                khalpha = 0.9 + 0.4 * (EpsilonAlpha - 1.0) / (PrecisionGrade - 4.0);
            }
            else
            {
                khalpha = 1.0;
            }

            // 限值: 1.0 <= KHalpha <= epsilon_alpha
            if (khalpha < 1.0) khalpha = 1.0;
            if (khalpha > EpsilonAlpha) khalpha = EpsilonAlpha;

            return Math.Round(khalpha, 4);
        }

        /// <summary>
        /// 强度系数计算 (区域系数、弹性系数、重合度系数)
        /// </summary>
        private void CalculateStrengthCoefficients()
        {
            double alphaRad = Alpha * Math.PI / 180.0;

            // 节点区域系数 ZH
            // ZH = sqrt(2 * cos(beta_b) / (sin(alpha_t) * cos(alpha_t) * tan(alpha_wt)))
            // 对于标准直齿轮 (beta=0, alpha_t=alpha, alpha_wt=alpha):
            // ZH = sqrt(2 / (sin(alpha) * cos(alpha) * tan(alpha))) = sqrt(2 / (sin(alpha) * sin(alpha)))
            // ZH = sqrt(2) / sin(alpha)
            // alpha=20°: ZH = sqrt(2) / sin(20°) = 1.4142 / 0.3420 ≈ 2.490
            ZH = Math.Sqrt(2.0) / Math.Sin(alphaRad);
            ZH = Math.Round(ZH, 4);

            // 弹性系数 ZE (sqrt(MPa))
            // 钢-钢: 189.8; 钢-铸钢: 188.9; 钢-球墨铸铁: 181.4; 铸铁-铸铁: 143.7
            ZE = CalculateZE();

            // 重合度系数 Zeps (接触强度)
            // Zeps = sqrt((4 - epsilon_alpha) / 3)
            Zeps = Math.Sqrt((4.0 - EpsilonAlpha) / 3.0);
            if (Zeps < 0.85) Zeps = 0.85;
            if (Zeps > 1.0) Zeps = 1.0;
            Zeps = Math.Round(Zeps, 4);

            // 重合度系数 Yeps (弯曲强度)
            // Yeps = 0.25 + 0.75 / epsilon_alpha
            Yeps = 0.25 + 0.75 / EpsilonAlpha;
            Yeps = Math.Round(Yeps, 4);

            // 寿命系数
            CalculateLifeCoefficients();

            // 尺寸系数
            CalculateSizeCoefficients();
        }

        /// <summary>
        /// 根据材料组合计算弹性系数 ZE
        /// </summary>
        private double CalculateZE()
        {
            double E1 = Material1.ElasticModulus;
            double E2 = Material2.ElasticModulus;
            double mu1 = Material1.PoissonRatio;
            double mu2 = Material2.PoissonRatio;

            // ZE = sqrt(1 / (pi * ((1-mu1^2)/E1 + (1-mu2^2)/E2)))
            // 注意: E的单位是MPa, ZE的单位是sqrt(MPa)
            double term = (1.0 - mu1 * mu1) / E1 + (1.0 - mu2 * mu2) / E2;
            double ze = Math.Sqrt(1.0 / (Math.PI * term));

            return Math.Round(ze, 1);
        }

        /// <summary>
        /// 寿命系数计算
        /// 假设无限寿命 (N > N0), 此时 ZN=1.0, YN=1.0
        /// 用户可调整
        /// </summary>
        private void CalculateLifeCoefficients()
        {
            // 默认按无限寿命计算
            // 接触强度寿命系数 ZN (N0 = 10^7 for 调质钢, 10^9 for 渗碳淬火)
            // 简化: 当循环次数 >= N0 时, ZN = 1.0
            ZN1 = 1.0;
            ZN2 = 1.0;

            // 弯曲强度寿命系数 YN (N0 = 3 * 10^6)
            YN1 = 1.0;
            YN2 = 1.0;

            LifeCoeffNote = "按无限寿命计算（循环次数 >= N0）";
        }

        /// <summary>
        /// 寿命系数计算 (有限寿命)
        /// </summary>
        /// <param name="cycles1">小齿轮应力循环次数</param>
        /// <param name="cycles2">大齿轮应力循环次数</param>
        public void CalculateLifeCoefficients(double cycles1, double cycles2)
        {
            // 接触强度寿命系数 ZN
            // 对于调质钢: N0 = 10^7
            //   N <= 10^5: ZN = 1.6
            //   10^5 < N < 10^7: ZN = (10^7/N)^(1/6)
            //   N >= 10^7: ZN = 1.0
            ZN1 = CalculateZN(cycles1, Material1);
            ZN2 = CalculateZN(cycles2, Material2);

            // 弯曲强度寿命系数 YN
            // N0 = 3 * 10^6
            //   N <= 10^4: YN = 2.5
            //   10^4 < N < 3*10^6: YN = (3*10^6/N)^(1/8)
            //   N >= 3*10^6: YN = 1.0
            YN1 = CalculateYN(cycles1);
            YN2 = CalculateYN(cycles2);

            LifeCoeffNote = $"有限寿命计算: ZN1={ZN1:F3}, ZN2={ZN2:F3}, YN1={YN1:F3}, YN2={YN2:F3}";
        }

        private double CalculateZN(double cycles, GearMaterial material)
        {
            double N0;
            if (material.HardnessType == "HRC")
                N0 = 1e9; // 表面硬化
            else
                N0 = 1e7; // 调质/正火

            if (cycles >= N0) return 1.0;
            if (cycles <= 1e5) return 1.6;

            // ZN = (N0/N)^(1/6)
            return Math.Pow(N0 / cycles, 1.0 / 6.0);
        }

        private double CalculateYN(double cycles)
        {
            double N0 = 3e6;

            if (cycles >= N0) return 1.0;
            if (cycles <= 1e4) return 2.5;

            // YN = (N0/N)^(1/8)
            return Math.Pow(N0 / cycles, 1.0 / 8.0);
        }

        /// <summary>
        /// 尺寸系数计算
        /// </summary>
        private void CalculateSizeCoefficients()
        {
            // 接触强度尺寸系数 Zx
            // 对于大多数情况, Zx = 1.0
            // 当模数 > 10mm 时需考虑
            if (ModuleM <= 10)
                Zx = 1.0;
            else
                Zx = Math.Pow(10.0 / ModuleM, 0.05);

            // 弯曲强度尺寸系数 Yx
            // 对于 m <= 5: Yx = 1.0
            // 对于 5 < m <= 30: Yx = 1.05 - 0.01 * m
            if (ModuleM <= 5)
                Yx = 1.0;
            else if (ModuleM <= 30)
                Yx = 1.05 - 0.01 * ModuleM;
            else
                Yx = 0.75;

            Yx = Math.Round(Yx, 4);
        }

        /// <summary>
        /// 接触疲劳强度校核
        /// sigma_H = ZH * ZE * Zeps * sqrt(2*K*T1 / (b*d1^2 * u/(u+1)))
        /// </summary>
        private void CalculateContactStrength()
        {
            // 齿数比 u (= i for 减速传动)
            double u = (double)Z2 / Z1;

            // 计算接触应力
            // sigma_H = ZH * ZE * Zeps * sqrt(2*K*T1 / (b*d1^2 * u/(u+1)))
            double innerTerm = 2.0 * K * T1 / (FaceWidth * D1 * D1 * u / (u + 1.0));
            SigmaH = ZH * ZE * Zeps * Math.Sqrt(innerTerm);
            SigmaH = Math.Round(SigmaH, 2);

            // 许用接触应力
            // sigma_HP = sigma_Hlim * ZN * Zx / SH_min
            // 取小齿轮和大齿轮中较小的许用应力
            double SHmin = 1.0; // 最小安全系数

            double sigmaHP1 = Material1.SigmaHlim * ZN1 * Zx / SHmin;
            double sigmaHP2 = Material2.SigmaHlim * ZN2 * Zx / SHmin;
            SigmaHP = Math.Min(sigmaHP1, sigmaHP2);
            SigmaHP = Math.Round(SigmaHP, 2);

            // 安全系数
            SH = SigmaHP / SigmaH;
            SH = Math.Round(SH, 3);

            // 校核结果
            ContactStrengthOK = SH >= 1.0;
        }

        /// <summary>
        /// 弯曲疲劳强度校核
        /// sigma_F = 2*K*T1*YFa*YSa*Yeps / (b*d1*m)
        /// </summary>
        private void CalculateBendingStrength()
        {
            // 齿形系数 YFa (根据齿数查表)
            double YFa1 = GetYFa(Z1);
            double YFa2 = GetYFa(Z2);

            // 应力修正系数 YSa
            double YSa1 = GetYSa(Z1);
            double YSa2 = GetYSa(Z2);

            // 小齿轮弯曲应力
            SigmaF1 = 2.0 * K * T1 * YFa1 * YSa1 * Yeps / (FaceWidth * D1 * ModuleM);
            SigmaF1 = Math.Round(SigmaF1, 2);

            // 大齿轮弯曲应力
            SigmaF2 = 2.0 * K * T1 * YFa2 * YSa2 * Yeps / (FaceWidth * D1 * ModuleM);
            SigmaF2 = Math.Round(SigmaF2, 2);

            // 许用弯曲应力
            // sigma_FP = sigma_Flim * YN * Yx / SF_min
            double SFmin = 1.4; // 弯曲强度最小安全系数

            SigmaFP1 = Material1.SigmaFlim * YN1 * Yx / SFmin;
            SigmaFP2 = Material2.SigmaFlim * YN2 * Yx / SFmin;
            SigmaFP1 = Math.Round(SigmaFP1, 2);
            SigmaFP2 = Math.Round(SigmaFP2, 2);

            // 安全系数
            SF1 = SigmaFP1 / SigmaF1;
            SF2 = SigmaFP2 / SigmaF2;
            SF1 = Math.Round(SF1, 3);
            SF2 = Math.Round(SF2, 3);

            // 校核结果
            BendingStrengthOK = (SF1 >= 1.0) && (SF2 >= 1.0);
        }

        /// <summary>
        /// 齿形系数 YFa 查表
        /// 标准直齿轮 (alpha=20°, ha*=1.0)
        /// 依据 GB/T 3480
        /// </summary>
        private double GetYFa(int z)
        {
            // 近似公式: YFa = 2.0 * (1 + 0.15 * (z - 17) / 100) / (1 + (z - 17) / 100)
            // 更精确的查表值:
            if (z <= 17) return 2.97;
            if (z <= 18) return 2.91;
            if (z <= 19) return 2.85;
            if (z <= 20) return 2.80;
            if (z <= 22) return 2.72;
            if (z <= 25) return 2.62;
            if (z <= 28) return 2.53;
            if (z <= 30) return 2.48;
            if (z <= 35) return 2.39;
            if (z <= 40) return 2.32;
            if (z <= 45) return 2.27;
            if (z <= 50) return 2.22;
            if (z <= 60) return 2.14;
            if (z <= 70) return 2.09;
            if (z <= 80) return 2.04;
            if (z <= 100) return 1.97;
            if (z <= 150) return 1.88;
            if (z <= 200) return 1.82;
            if (z <= 300) return 1.76;
            return 1.70; // z > 300
        }

        /// <summary>
        /// 应力修正系数 YSa 查表
        /// 标准直齿轮 (alpha=20°, ha*=1.0)
        /// </summary>
        private double GetYSa(int z)
        {
            // 与 YFa 配套的 YSa 值
            if (z <= 17) return 1.52;
            if (z <= 18) return 1.53;
            if (z <= 19) return 1.54;
            if (z <= 20) return 1.55;
            if (z <= 22) return 1.56;
            if (z <= 25) return 1.58;
            if (z <= 28) return 1.60;
            if (z <= 30) return 1.61;
            if (z <= 35) return 1.65;
            if (z <= 40) return 1.69;
            if (z <= 45) return 1.72;
            if (z <= 50) return 1.75;
            if (z <= 60) return 1.80;
            if (z <= 70) return 1.85;
            if (z <= 80) return 1.89;
            if (z <= 100) return 1.97;
            if (z <= 150) return 2.07;
            if (z <= 200) return 2.14;
            if (z <= 300) return 2.22;
            return 2.30; // z > 300
        }

        /// <summary>
        /// 计算推荐模数
        /// </summary>
        /// <param name="targetD1">目标小齿轮分度圆直径 (mm)</param>
        /// <returns>推荐模数 (mm)</returns>
        public double GetRecommendedModule(double targetD1)
        {
            // 标准模数系列 (GB/T 1357)
            double[] standardModules = {
                1, 1.25, 1.5, 2, 2.5, 3, 4, 5, 6, 8, 10, 12, 16, 20, 25, 32, 40, 50
            };

            double rawModule = targetD1 / Z1;

            // 找到最接近的标准模数
            double best = standardModules[0];
            double minDiff = Math.Abs(rawModule - best);

            foreach (double m in standardModules)
            {
                double diff = Math.Abs(rawModule - m);
                if (diff < minDiff)
                {
                    minDiff = diff;
                    best = m;
                }
            }

            return best;
        }

        /// <summary>
        /// 根据接触强度估算最小分度圆直径
        /// </summary>
        /// <returns>最小分度圆直径 d1 (mm)</returns>
        public double EstimateMinD1()
        {
            double u = (double)Z2 / Z1;
            double SHmin = 1.0;

            // 取较小的许用应力
            double sigmaHP = Math.Min(Material1.SigmaHlim, Material2.SigmaHlim) / SHmin;

            // d1 >= (2*K*T1/(b/d1) * (u+1)/u * (ZH*ZE*Zeps/sigmaHP)^2)^(1/3)
            // 令 b = phi_d * d1, 则 b/d1 = phi_d
            double t = 2.0 * K * T1 / PhiD * (u + 1.0) / u;
            double coeff = ZH * ZE * Zeps / sigmaHP;
            double d1min = Math.Pow(t * coeff * coeff, 1.0 / 3.0);

            return Math.Round(d1min, 2);
        }
    }
}
