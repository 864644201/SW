using System;

namespace WormGearAndWormDesign
{
    /// <summary>
    /// 蜗轮蜗杆传动设计计算器
    /// 实现蜗轮蜗杆几何参数计算、强度校核、热平衡校核、刚度校核
    /// </summary>
    internal class WormGearCalculator
    {
        // ==================== 输入参数 ====================
        /// <summary>传递功率 P (kW)</summary>
        public double P { get; set; }
        /// <summary>蜗杆转速 n1 (rpm)</summary>
        public double N1 { get; set; }
        /// <summary>传动比 i</summary>
        public double I { get; set; }
        /// <summary>蜗杆头数 z1</summary>
        public int Z1 { get; set; }
        /// <summary>蜗轮齿数 z2</summary>
        public int Z2 { get; set; }
        /// <summary>模数 m (mm)</summary>
        public double M { get; set; }
        /// <summary>蜗杆直径系数 q</summary>
        public double Q { get; set; }
        /// <summary>蜗杆材料弹性模量 E (MPa)</summary>
        public double E { get; set; }
        /// <summary>蜗杆材料泊松比</summary>
        public double Nu { get; set; }
        /// <summary>摩擦系数 f</summary>
        public double F { get; set; }
        /// <summary>使用系数 Ka</summary>
        public double Ka { get; set; }
        /// <summary>弹性影响系数 Ze (MPa^0.5)</summary>
        public double Ze { get; set; }
        /// <summary>蜗轮许用接触应力 [sigma_H] (MPa)</summary>
        public double SigmaH_Allow { get; set; }
        /// <summary>蜗轮许用弯曲应力 [sigma_F] (MPa)</summary>
        public double SigmaF_Allow { get; set; }
        /// <summary>蜗杆许用挠度 [y] (mm)</summary>
        public double Y_Allow { get; set; }
        /// <summary>散热系数 K (W/(m2·C))</summary>
        public double K_Heat { get; set; }
        /// <summary>散热面积 A (m2)</summary>
        public double A_Heat { get; set; }
        /// <summary>允许油温 t1 (C)</summary>
        public double T1_Oil { get; set; }
        /// <summary>环境温度 t0 (C)</summary>
        public double T0_Env { get; set; }
        /// <summary>蜗杆齿形系数 Y_Fa2</summary>
        public double YFa2 { get; set; }
        /// <summary>螺旋角系数 Y_beta</summary>
        public double YBeta { get; set; }
        /// <summary>载荷系数 K</summary>
        public double K_Load { get; set; }

        // ==================== 计算结果 ====================
        /// <summary>蜗杆分度圆直径 d1 (mm)</summary>
        public double D1 { get; private set; }
        /// <summary>蜗轮分度圆直径 d2 (mm)</summary>
        public double D2 { get; private set; }
        /// <summary>中心距 a (mm)</summary>
        public double A { get; private set; }
        /// <summary>蜗杆导程角 gamma (rad)</summary>
        public double Gamma { get; private set; }
        /// <summary>蜗杆导程角 gamma (deg)</summary>
        public double GammaDeg { get; private set; }
        /// <summary>蜗杆轴向齿距 px (mm)</summary>
        public double Px { get; private set; }
        /// <summary>蜗杆导程 L (mm)</summary>
        public double L { get; private set; }
        /// <summary>蜗轮转速 n2 (rpm)</summary>
        public double N2 { get; private set; }
        /// <summary>蜗杆圆周速度 v1 (m/s)</summary>
        public double V1 { get; private set; }
        /// <summary>滑动速度 vs (m/s)</summary>
        public double Vs { get; private set; }
        /// <summary>蜗轮圆周速度 v2 (m/s)</summary>
        public double V2 { get; private set; }
        /// <summary>蜗杆效率 eta</summary>
        public double Eta { get; private set; }
        /// <summary>摩擦角 rho (rad)</summary>
        public double Rho { get; private set; }
        /// <summary>蜗轮转矩 T2 (N·mm)</summary>
        public double T2 { get; private set; }
        /// <summary>蜗杆转矩 T1 (N·mm)</summary>
        public double T1 { get; private set; }
        /// <summary>齿面接触应力 sigma_H (MPa)</summary>
        public double SigmaH { get; private set; }
        /// <summary>齿根弯曲应力 sigma_F (MPa)</summary>
        public double SigmaF { get; private set; }
        /// <summary>发热量 Q_r (W)</summary>
        public double Qr { get; private set; }
        /// <summary>散热量 Q_c (W)</summary>
        public double Qc { get; private set; }
        /// <summary>热平衡是否满足</summary>
        public bool HeatBalanceOK { get; private set; }
        /// <summary>蜗杆挠度 y (mm)</summary>
        public double Deflection { get; private set; }
        /// <summary>蜗杆刚度是否满足</summary>
        public bool StiffnessOK { get; private set; }
        /// <summary>接触强度是否满足</summary>
        public bool ContactOK { get; private set; }
        /// <summary>弯曲强度是否满足</summary>
        public bool BendingOK { get; private set; }
        /// <summary>蜗杆齿顶圆直径 da1 (mm)</summary>
        public double Da1 { get; private set; }
        /// <summary>蜗杆齿根圆直径 df1 (mm)</summary>
        public double Df1 { get; private set; }
        /// <summary>蜗轮齿顶圆直径 da2 (mm)</summary>
        public double Da2 { get; private set; }
        /// <summary>蜗轮齿根圆直径 df2 (mm)</summary>
        public double Df2 { get; private set; }
        /// <summary>蜗杆齿宽 b1 (mm)</summary>
        public double B1 { get; private set; }
        /// <summary>蜗轮齿宽 b2 (mm)</summary>
        public double B2 { get; private set; }
        /// <summary>蜗杆轴向力 Fa1 (N)</summary>
        public double Fa1 { get; private set; }
        /// <summary>蜗杆圆周力 Ft1 (N)</summary>
        public double Ft1 { get; private set; }
        /// <summary>蜗杆径向力 Fr1 (N)</summary>
        public double Fr1 { get; private set; }
        /// <summary>蜗轮轴向力 Fa2 (N)</summary>
        public double Fa2 { get; private set; }
        /// <summary>蜗轮圆周力 Ft2 (N)</summary>
        public double Ft2 { get; private set; }
        /// <summary>蜗轮径向力 Fr2 (N)</summary>
        public double Fr2 { get; private set; }
        /// <summary>蜗杆截面惯性矩 I (mm^4)</summary>
        public double MomentOfInertia { get; private set; }
        /// <summary>蜗杆危险截面直径 (mm)</summary>
        public double Df1_Equiv { get; private set; }

        /// <summary>
        /// 执行全部计算
        /// </summary>
        public void Calculate()
        {
            CalculateGeometry();
            CalculateKinematics();
            CalculateForces();
            CalculateEfficiency();
            CalculateStrength();
            CalculateHeatBalance();
            CalculateStiffness();
        }

        /// <summary>
        /// 几何参数计算
        /// </summary>
        private void CalculateGeometry()
        {
            // 蜗轮齿数: z2 = i * z1 (若未指定则自动计算)
            if (Z2 <= 0)
                Z2 = (int)Math.Round(I * Z1);

            // 蜗杆分度圆直径: d1 = m * q
            D1 = M * Q;

            // 蜗轮分度圆直径: d2 = m * z2
            D2 = M * Z2;

            // 中心距: a = m*(q + z2)/2
            A = M * (Q + Z2) / 2.0;

            // 蜗杆导程角: gamma = arctan(z1/q)
            Gamma = Math.Atan((double)Z1 / Q);
            GammaDeg = Gamma * 180.0 / Math.PI;

            // 蜗杆轴向齿距: px = pi * m
            Px = Math.PI * M;

            // 蜗杆导程: L = z1 * px
            L = Z1 * Px;

            // 齿顶高系数 ha* = 1, 顶隙系数 c* = 0.2
            double ha = M;        // 齿顶高
            double c = 0.2 * M;   // 顶隙

            // 蜗杆齿顶圆直径: da1 = d1 + 2*ha*m = m*(q+2)
            Da1 = M * (Q + 2);

            // 蜗杆齿根圆直径: df1 = d1 - 2*(ha+c)*m = m*(q-2.4)
            Df1 = M * (Q - 2.4);

            // 蜗轮齿顶圆直径: da2 = d2 + 2*ha*m = m*(z2+2)
            Da2 = M * (Z2 + 2);

            // 蜗轮齿根圆直径: df2 = d2 - 2*(ha+c)*m = m*(z2-2.4)
            Df2 = M * (Z2 - 2.4);

            // 蜗杆齿宽 (经验公式): b1 ≈ 2*m*sqrt(z2)
            B1 = 2 * M * Math.Sqrt(Z2);

            // 蜗轮齿宽: b2 ≈ 2*m*(0.5 + sqrt(q-1))
            if (Q > 1)
                B2 = 2 * M * (0.5 + Math.Sqrt(Q - 1));
            else
                B2 = 2 * M;

            // 蜗杆危险截面直径 (齿根圆)
            Df1_Equiv = Df1;

            // 截面惯性矩: I = pi*d^4/64
            MomentOfInertia = Math.PI * Math.Pow(Df1_Equiv, 4) / 64.0;
        }

        /// <summary>
        /// 运动学计算
        /// </summary>
        private void CalculateKinematics()
        {
            // 蜗轮转速: n2 = n1 / i
            N2 = N1 / I;

            // 蜗杆圆周速度: v1 = pi*d1*n1/60000 (m/s)
            V1 = Math.PI * D1 * N1 / 60000.0;

            // 滑动速度: vs = v1/cos(gamma)
            Vs = V1 / Math.Cos(Gamma);

            // 蜗轮圆周速度: v2 = pi*d2*n2/60000 (m/s)
            V2 = Math.PI * D2 * N2 / 60000.0;
        }

        /// <summary>
        /// 力的计算
        /// </summary>
        private void CalculateForces()
        {
            // 蜗杆转矩: T1 = P*10^6/(2*pi*n1/60) = P*10^6*60/(2*pi*n1) (N·mm)
            T1 = P * 1e6 * 60.0 / (2.0 * Math.PI * N1);

            // 蜗轮转矩: T2 = T1 * i * eta (先用 T1*i 估算，效率后续更新)
            // 这里先计算不考虑效率的转矩，效率计算后再修正
            T2 = T1 * I * 0.85; // 初始估算

            // 蜗杆圆周力: Ft1 = 2*T1/d1 (N)
            Ft1 = 2 * T1 / D1;

            // 蜗杆轴向力: Fa1 = Ft2 = 2*T2/d2 (N) -- 蜗杆轴向力等于蜗轮圆周力
            Ft2 = 2 * T2 / D2;
            Fa1 = Ft2;

            // 蜗杆径向力: Fr1 = Ft1*tan(alpha_n) ≈ Ft1*tan(20°)/cos(gamma)
            double alpha = 20.0 * Math.PI / 180.0; // 压力角
            Fr1 = Ft1 * Math.Tan(alpha) / Math.Cos(Gamma);

            // 蜗轮轴向力: Fa2 = Ft1 (蜗轮轴向力等于蜗杆圆周力)
            Fa2 = Ft1;

            // 蜗轮径向力: Fr2 = Fr1
            Fr2 = Fr1;
        }

        /// <summary>
        /// 效率计算
        /// </summary>
        private void CalculateEfficiency()
        {
            // 摩擦角: rho = arctan(f)
            Rho = Math.Atan(F);

            // 蜗杆效率: eta = tan(gamma)/tan(gamma + rho)
            Eta = Math.Tan(Gamma) / Math.Tan(Gamma + Rho);

            // 修正蜗轮转矩: T2 = T1 * i * eta
            T2 = T1 * I * Eta;

            // 修正力的计算
            Ft2 = 2 * T2 / D2;
            Fa1 = Ft2;
        }

        /// <summary>
        /// 强度计算
        /// </summary>
        private void CalculateStrength()
        {
            // 齿面接触强度: sigma_H = Z_E * sqrt(9*K*T2/(d1*d2^2))
            double d2_squared = D2 * D2;
            SigmaH = Ze * Math.Sqrt(9.0 * Ka * T2 / (D1 * d2_squared));

            // 接触强度判定
            ContactOK = SigmaH <= SigmaH_Allow;

            // 蜗轮齿根弯曲强度: sigma_F = (2*K*T2*Y_Fa2*Y_beta)/(d1*d2*m)
            SigmaF = (2.0 * K_Load * T2 * YFa2 * YBeta) / (D1 * D2 * M);

            // 弯曲强度判定
            BendingOK = SigmaF <= SigmaF_Allow;
        }

        /// <summary>
        /// 热平衡计算
        /// </summary>
        private void CalculateHeatBalance()
        {
            // 发热量: Q_r = P*(1-eta)*1000 (W)
            Qr = P * (1.0 - Eta) * 1000.0;

            // 散热量: Q_c = K*A*(t1-t0) (W)
            Qc = K_Heat * A_Heat * (T1_Oil - T0_Env);

            // 热平衡判定: Q_c >= Q_r
            HeatBalanceOK = Qc >= Qr;
        }

        /// <summary>
        /// 蜗杆刚度校核
        /// </summary>
        private void CalculateStiffness()
        {
            // 蜗杆圆周力 Ft1 作用下的挠度
            // y = F_t1 * L^3 / (48 * E * I)
            // 其中 L 为蜗杆支撑跨距，近似取为蜗杆齿宽 b1
            double span = B1; // 支撑跨距近似为齿宽
            Deflection = Ft1 * Math.Pow(span, 3) / (48.0 * E * MomentOfInertia);

            // 刚度判定: y <= [y]
            StiffnessOK = Deflection <= Y_Allow;
        }

        /// <summary>
        /// 获取计算结果摘要
        /// </summary>
        public string GetResultSummary()
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("========================================");
            sb.AppendLine("        蜗轮蜗杆设计计算结果");
            sb.AppendLine("========================================");
            sb.AppendLine();

            sb.AppendLine("--- 基本几何参数 ---");
            sb.AppendFormat("  蜗杆头数 z1 = {0}\n", Z1);
            sb.AppendFormat("  蜗轮齿数 z2 = {0}\n", Z2);
            sb.AppendFormat("  模数 m = {0:F4} mm\n", M);
            sb.AppendFormat("  蜗杆直径系数 q = {0:F4}\n", Q);
            sb.AppendFormat("  蜗杆分度圆直径 d1 = {0:F4} mm\n", D1);
            sb.AppendFormat("  蜗轮分度圆直径 d2 = {0:F4} mm\n", D2);
            sb.AppendFormat("  中心距 a = {0:F4} mm\n", A);
            sb.AppendFormat("  蜗杆导程角 γ = {0:F4}° ({1:F6} rad)\n", GammaDeg, Gamma);
            sb.AppendFormat("  蜗杆轴向齿距 px = {0:F4} mm\n", Px);
            sb.AppendFormat("  蜗杆导程 L = {0:F4} mm\n", L);
            sb.AppendLine();

            sb.AppendLine("--- 蜗杆几何尺寸 ---");
            sb.AppendFormat("  齿顶圆直径 da1 = {0:F4} mm\n", Da1);
            sb.AppendFormat("  齿根圆直径 df1 = {0:F4} mm\n", Df1);
            sb.AppendFormat("  齿宽 b1 = {0:F4} mm\n", B1);
            sb.AppendLine();

            sb.AppendLine("--- 蜗轮几何尺寸 ---");
            sb.AppendFormat("  齿顶圆直径 da2 = {0:F4} mm\n", Da2);
            sb.AppendFormat("  齿根圆直径 df2 = {0:F4} mm\n", Df2);
            sb.AppendFormat("  齿宽 b2 = {0:F4} mm\n", B2);
            sb.AppendLine();

            sb.AppendLine("--- 运动学参数 ---");
            sb.AppendFormat("  蜗轮转速 n2 = {0:F4} rpm\n", N2);
            sb.AppendFormat("  蜗杆圆周速度 v1 = {0:F4} m/s\n", V1);
            sb.AppendFormat("  滑动速度 vs = {0:F4} m/s\n", Vs);
            sb.AppendFormat("  蜗轮圆周速度 v2 = {0:F4} m/s\n", V2);
            sb.AppendFormat("  传动效率 η = {0:F4} ({1:P2})\n", Eta, Eta);
            sb.AppendLine();

            sb.AppendLine("--- 力的分析 ---");
            sb.AppendFormat("  蜗杆转矩 T1 = {0:F4} N·mm\n", T1);
            sb.AppendFormat("  蜗轮转矩 T2 = {0:F4} N·mm\n", T2);
            sb.AppendFormat("  蜗杆圆周力 Ft1 = {0:F4} N\n", Ft1);
            sb.AppendFormat("  蜗杆轴向力 Fa1 = {0:F4} N\n", Fa1);
            sb.AppendFormat("  蜗杆径向力 Fr1 = {0:F4} N\n", Fr1);
            sb.AppendFormat("  蜗轮圆周力 Ft2 = {0:F4} N\n", Ft2);
            sb.AppendFormat("  蜗轮轴向力 Fa2 = {0:F4} N\n", Fa2);
            sb.AppendFormat("  蜗轮径向力 Fr2 = {0:F4} N\n", Fr2);
            sb.AppendLine();

            sb.AppendLine("--- 强度校核 ---");
            sb.AppendFormat("  齿面接触应力 σ_H = {0:F4} MPa\n", SigmaH);
            sb.AppendFormat("  许用接触应力 [σ_H] = {0:F4} MPa\n", SigmaH_Allow);
            sb.AppendFormat("  接触强度: {0}\n", ContactOK ? "满足" : "不满足");
            sb.AppendFormat("  齿根弯曲应力 σ_F = {0:F4} MPa\n", SigmaF);
            sb.AppendFormat("  许用弯曲应力 [σ_F] = {0:F4} MPa\n", SigmaF_Allow);
            sb.AppendFormat("  弯曲强度: {0}\n", BendingOK ? "满足" : "不满足");
            sb.AppendLine();

            sb.AppendLine("--- 热平衡校核 ---");
            sb.AppendFormat("  发热量 Q_r = {0:F4} W\n", Qr);
            sb.AppendFormat("  散热量 Q_c = {0:F4} W\n", Qc);
            sb.AppendFormat("  热平衡: {0} (Q_c {1} Q_r)\n",
                HeatBalanceOK ? "满足" : "不满足",
                HeatBalanceOK ? ">=" : "<");
            sb.AppendLine();

            sb.AppendLine("--- 蜗杆刚度校核 ---");
            sb.AppendFormat("  蜗杆挠度 y = {0:F6} mm\n", Deflection);
            sb.AppendFormat("  许用挠度 [y] = {0:F6} mm\n", Y_Allow);
            sb.AppendFormat("  刚度: {0}\n", StiffnessOK ? "满足" : "不满足");

            return sb.ToString();
        }
    }
}
