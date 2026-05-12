using System;

namespace BevelGearDesign
{
    /// <summary>
    /// 直齿圆锥齿轮设计计算器
    /// 参考标准: GB/T 12369-1990, GB/T 12370-1990
    /// </summary>
    public class BevelGearCalculator
    {
        // ========== 输入参数 ==========

        /// <summary>
        /// 输入功率 P (kW)
        /// </summary>
        public double InputPower { get; set; }

        /// <summary>
        /// 小齿轮转速 n1 (rpm)
        /// </summary>
        public double InputSpeed { get; set; }

        /// <summary>
        /// 齿数比 u = Z2/Z1
        /// </summary>
        public double GearRatio { get; set; }

        /// <summary>
        /// 小齿轮齿数 Z1
        /// </summary>
        public int Z1 { get; set; }

        /// <summary>
        /// 大齿轮齿数 Z2
        /// </summary>
        public int Z2 { get; set; }

        /// <summary>
        /// 大端模数 m (mm)
        /// </summary>
        public double Module { get; set; }

        /// <summary>
        /// 压力角 alpha (度), 通常为20度
        /// </summary>
        public double PressureAngle { get; set; } = 20.0;

        /// <summary>
        /// 齿宽系数 phi_R = b/R, 通常0.25~0.35
        /// </summary>
        public double FaceWidthCoeff { get; set; } = 0.3;

        /// <summary>
        /// 轴交角 Sigma (度), 通常为90度
        /// </summary>
        public double ShaftAngle { get; set; } = 90.0;

        /// <summary>
        /// 使用系数 KA
        /// </summary>
        public double KA { get; set; } = 1.0;

        /// <summary>
        /// 动载系数 Kv
        /// </summary>
        public double Kv { get; set; } = 1.0;

        /// <summary>
        /// 齿向载荷分布系数 Kbeta
        /// </summary>
        public double Kbeta { get; set; } = 1.0;

        /// <summary>
        /// 齿间载荷分配系数 Kalpha
        /// </summary>
        public double Kalpha { get; set; } = 1.0;

        /// <summary>
        /// 小齿轮材料
        /// </summary>
        public GearMaterial PinionMaterial { get; set; }

        /// <summary>
        /// 大齿轮材料
        /// </summary>
        public GearMaterial WheelMaterial { get; set; }

        // ========== 计算结果 ==========

        /// <summary>
        /// 小齿轮分度圆锥角 delta1 (度)
        /// </summary>
        public double Delta1 { get; private set; }

        /// <summary>
        /// 大齿轮分度圆锥角 delta2 (度)
        /// </summary>
        public double Delta2 { get; private set; }

        /// <summary>
        /// 锥距 R (mm)
        /// </summary>
        public double ConeDistance { get; private set; }

        /// <summary>
        /// 齿宽 b (mm)
        /// </summary>
        public double FaceWidth { get; private set; }

        /// <summary>
        /// 小齿轮当量齿数 Zv1
        /// </summary>
        public double Zv1 { get; private set; }

        /// <summary>
        /// 大齿轮当量齿数 Zv2
        /// </summary>
        public double Zv2 { get; private set; }

        /// <summary>
        /// 小齿轮分度圆直径 d1 (mm)
        /// </summary>
        public double d1 { get; private set; }

        /// <summary>
        /// 大齿轮分度圆直径 d2 (mm)
        /// </summary>
        public double d2 { get; private set; }

        /// <summary>
        /// 小齿轮齿顶圆直径 da1 (mm)
        /// </summary>
        public double da1 { get; private set; }

        /// <summary>
        /// 大齿轮齿顶圆直径 da2 (mm)
        /// </summary>
        public double da2 { get; private set; }

        /// <summary>
        /// 小齿轮齿根圆直径 df1 (mm)
        /// </summary>
        public double df1 { get; private set; }

        /// <summary>
        /// 大齿轮齿根圆直径 df2 (mm)
        /// </summary>
        public double df2 { get; private set; }

        /// <summary>
        /// 小齿轮齿顶角 theta_a1 (度)
        /// </summary>
        public double ThetaA1 { get; private set; }

        /// <summary>
        /// 大齿轮齿顶角 theta_a2 (度)
        /// </summary>
        public double ThetaA2 { get; private set; }

        /// <summary>
        /// 小齿轮齿根角 theta_f1 (度)
        /// </summary>
        public double ThetaF1 { get; private set; }

        /// <summary>
        /// 大齿轮齿根角 theta_f2 (度)
        /// </summary>
        public double ThetaF2 { get; private set; }

        /// <summary>
        /// 小齿轮顶锥角 delta_a1 (度)
        /// </summary>
        public double DeltaA1 { get; private set; }

        /// <summary>
        /// 大齿轮顶锥角 delta_a2 (度)
        /// </summary>
        public double DeltaA2 { get; private set; }

        /// <summary>
        /// 小齿轮根锥角 delta_f1 (度)
        /// </summary>
        public double DeltaF1 { get; private set; }

        /// <summary>
        /// 大齿轮根锥角 delta_f2 (度)
        /// </summary>
        public double DeltaF2 { get; private set; }

        /// <summary>
        /// 大端齿高 h (mm)
        /// </summary>
        public double ToothHeight { get; private set; }

        /// <summary>
        /// 大端齿顶高 ha (mm)
        /// </summary>
        public double Addendum { get; private set; }

        /// <summary>
        /// 大端齿根高 hf (mm)
        /// </summary>
        public double Dedendum { get; private set; }

        /// <summary>
        /// 小齿轮转矩 T1 (N·mm)
        /// </summary>
        public double T1 { get; private set; }

        /// <summary>
        /// 圆周力 Ft (N)
        /// </summary>
        public double Ft { get; private set; }

        /// <summary>
        /// 径向力 Fr (N)
        /// </summary>
        public double Fr { get; private set; }

        /// <summary>
        /// 轴向力 Fa (N)
        /// </summary>
        public double Fa { get; private set; }

        /// <summary>
        /// 节圆处圆周速度 v (m/s)
        /// </summary>
        public double PitchLineVelocity { get; private set; }

        /// <summary>
        /// 接触应力 sigma_H (MPa)
        /// </summary>
        public double SigmaH { get; private set; }

        /// <summary>
        /// 弯曲应力 sigma_F1 (MPa) - 小齿轮
        /// </summary>
        public double SigmaF1 { get; private set; }

        /// <summary>
        /// 弯曲应力 sigma_F2 (MPa) - 大齿轮
        /// </summary>
        public double SigmaF2 { get; private set; }

        /// <summary>
        /// 接触疲劳强度安全系数 SH
        /// </summary>
        public double SH { get; private set; }

        /// <summary>
        /// 弯曲疲劳强度安全系数 SF1 (小齿轮)
        /// </summary>
        public double SF1 { get; private set; }

        /// <summary>
        /// 弯曲疲劳强度安全系数 SF2 (大齿轮)
        /// </summary>
        public double SF2 { get; private set; }

        /// <summary>
        /// 综合载荷系数 K
        /// </summary>
        public double K { get; private set; }

        /// <summary>
        /// 节点区域系数 ZH
        /// </summary>
        public double ZH { get; private set; }

        /// <summary>
        /// 重合度系数 Z_eps
        /// </summary>
        public double Zeps { get; private set; }

        /// <summary>
        /// 弹性系数 ZE (MPa^0.5)
        /// </summary>
        public double ZE { get; private set; }

        /// <summary>
        /// 齿形系数 YFa1 (小齿轮)
        /// </summary>
        public double YFa1 { get; private set; }

        /// <summary>
        /// 齿形系数 YFa2 (大齿轮)
        /// </summary>
        public double YFa2 { get; private set; }

        /// <summary>
        /// 应力修正系数 YSa1 (小齿轮)
        /// </summary>
        public double YSa1 { get; private set; }

        /// <summary>
        /// 应力修正系数 YSa2 (大齿轮)
        /// </summary>
        public double YSa2 { get; private set; }

        /// <summary>
        /// 弯曲强度重合度系数 Yeps
        /// </summary>
        public double Yeps { get; private set; }

        /// <summary>
        /// 端面重合度 epsilon_alpha
        /// </summary>
        public double EpsilonAlpha { get; private set; }

        /// <summary>
        /// 接触强度是否合格
        /// </summary>
        public bool ContactStrengthOk { get; private set; }

        /// <summary>
        /// 弯曲强度是否合格
        /// </summary>
        public bool BendingStrengthOk { get; private set; }

        /// <summary>
        /// 执行全部计算
        /// </summary>
        public void Calculate()
        {
            // 1. 基本几何参数计算
            CalculateGeometry();

            // 2. 运动学与载荷计算
            CalculateKinematics();

            // 3. 强度校核
            CalculateStrength();
        }

        /// <summary>
        /// 几何参数计算
        /// </summary>
        private void CalculateGeometry()
        {
            double alpha = PressureAngle * Math.PI / 180.0;
            double sigma = ShaftAngle * Math.PI / 180.0;

            // 分度圆锥角 (对于轴交角90度的直齿圆锥齿轮)
            // tan(delta1) = Z1/Z2
            Delta1 = Math.Atan((double)Z1 / Z2) * 180.0 / Math.PI;
            Delta2 = ShaftAngle - Delta1;

            double delta1Rad = Delta1 * Math.PI / 180.0;
            double delta2Rad = Delta2 * Math.PI / 180.0;

            // 分度圆直径
            d1 = Module * Z1;
            d2 = Module * Z2;

            // 齿顶高、齿根高
            Addendum = Module;                    // ha = m (正常齿制 ha* = 1)
            double hfCoeff = 1.25;                // hf* = 1.25 (正常齿制)
            Dedendum = hfCoeff * Module;          // hf = 1.25m
            ToothHeight = Addendum + Dedendum;    // h = ha + hf = 2.25m

            // 齿顶圆直径
            da1 = d1 + 2.0 * Module * Math.Cos(delta1Rad);
            da2 = d2 + 2.0 * Module * Math.Cos(delta2Rad);

            // 齿根圆直径
            df1 = d1 - 2.0 * hfCoeff * Module * Math.Cos(delta1Rad);
            df2 = d2 - 2.0 * hfCoeff * Module * Math.Cos(delta2Rad);

            // 锥距 (外锥距)
            ConeDistance = Module * Z1 / (2.0 * Math.Sin(delta1Rad));

            // 齿宽
            FaceWidth = FaceWidthCoeff * ConeDistance;
            // 齿宽限制: b <= 10m 且 b <= R/3
            double bMax1 = 10.0 * Module;
            double bMax2 = ConeDistance / 3.0;
            if (FaceWidth > bMax1) FaceWidth = bMax1;
            if (FaceWidth > bMax2) FaceWidth = bMax2;

            // 当量齿数 (虚拟圆柱齿轮齿数)
            Zv1 = Z1 / Math.Cos(delta1Rad);
            Zv2 = Z2 / Math.Cos(delta2Rad);

            // 齿顶角 theta_a = arctan(ha/R)
            ThetaA1 = Math.Atan(Addendum / ConeDistance) * 180.0 / Math.PI;
            ThetaA2 = Math.Atan(Addendum / ConeDistance) * 180.0 / Math.PI;

            // 齿根角 theta_f = arctan(hf/R)
            ThetaF1 = Math.Atan(Dedendum / ConeDistance) * 180.0 / Math.PI;
            ThetaF2 = Math.Atan(Dedendum / ConeDistance) * 180.0 / Math.PI;

            // 顶锥角 delta_a = delta + theta_a
            DeltaA1 = Delta1 + ThetaA1;
            DeltaA2 = Delta2 + ThetaA2;

            // 根锥角 delta_f = delta - theta_f
            DeltaF1 = Delta1 - ThetaF1;
            DeltaF2 = Delta2 - ThetaF2;
        }

        /// <summary>
        /// 运动学与载荷计算
        /// </summary>
        private void CalculateKinematics()
        {
            // 小齿轮转矩 T1 = 9550 * P / n1 (N·m) -> T1 = 9.55e6 * P / n1 (N·mm)
            T1 = 9.55e6 * InputPower / InputSpeed;

            // 节圆处圆周速度 v = pi*d1*n1 / (60*1000) (m/s)
            PitchLineVelocity = Math.PI * d1 * InputSpeed / (60.0 * 1000.0);

            // 圆周力 Ft = 2*T1 / d1 (N)
            Ft = 2.0 * T1 / d1;

            // 对于直齿圆锥齿轮(轴交角90度)
            double delta1Rad = Delta1 * Math.PI / 180.0;
            double delta2Rad = Delta2 * Math.PI / 180.0;
            double alpha = PressureAngle * Math.PI / 180.0;

            // 径向力 Fr = Ft * tan(alpha) * cos(delta)
            Fr = Ft * Math.Tan(alpha) * Math.Cos(delta1Rad);

            // 轴向力 Fa = Ft * tan(alpha) * sin(delta)
            Fa = Ft * Math.Tan(alpha) * Math.Sin(delta1Rad);

            // 综合载荷系数 K = KA * Kv * Kbeta * Kalpha
            K = KA * Kv * Kbeta * Kalpha;
        }

        /// <summary>
        /// 接触疲劳强度与弯曲疲劳强度校核
        /// </summary>
        private void CalculateStrength()
        {
            double alpha = PressureAngle * Math.PI / 180.0;
            double delta1Rad = Delta1 * Math.PI / 180.0;

            // 节点区域系数 ZH (直齿轮, 标准压力角)
            // ZH = 2*cos(beta_b)/(sin(2*alpha_t)*cos(alpha_t))
            // 对于直齿: ZH = sqrt(2*cos(beta_b)/sin(2*alpha))
            // 简化: ZH = 2.495 (alpha=20°)
            ZH = 2.0 * Math.Cos(0) / Math.Sqrt(Math.Sin(2.0 * alpha) * Math.Cos(alpha));

            // 弹性系数 ZE
            if (PinionMaterial != null && WheelMaterial != null)
            {
                // 取配对材料的平均弹性系数
                ZE = Math.Sqrt(PinionMaterial.ZE * WheelMaterial.ZE);
            }
            else
            {
                ZE = GearMaterial.ZE_SteelSteel; // 默认钢-钢
            }

            // 端面重合度 epsilon_alpha (近似公式)
            // 对于直齿圆锥齿轮, 使用当量齿轮的重合度
            // epsilon_alpha = [Zv1*(tan(alpha_va1)-tan(alpha')) + Zv2*(tan(alpha_va2)-tan(alpha'))] / (2*pi)
            // 简化近似: epsilon_alpha ≈ 1.88 - 3.2*(1/Zv1 + 1/Zv2)
            EpsilonAlpha = 1.88 - 3.2 * (1.0 / Zv1 + 1.0 / Zv2);

            // 重合度系数 Z_eps (接触强度)
            // Z_eps = sqrt((4-epsilon_alpha)/3)
            if (EpsilonAlpha < 1.0) EpsilonAlpha = 1.0;
            Zeps = Math.Sqrt((4.0 - EpsilonAlpha) / 3.0);

            // 弯曲强度重合度系数 Y_eps
            // Y_eps = 0.25 + 0.75/epsilon_alpha
            Yeps = 0.25 + 0.75 / EpsilonAlpha;

            // 齿形系数 YFa 和应力修正系数 YSa (基于当量齿数)
            YFa1 = GetYFa(Zv1);
            YFa2 = GetYFa(Zv2);
            YSa1 = GetYSa(Zv1);
            YSa2 = GetYSa(Zv2);

            // ========== 接触疲劳强度校核 ==========
            // sigma_H = ZH * ZE * Zeps * sqrt(2*K*T1 / (b*d1^2 * u/(u+1)))
            double u = GearRatio;
            double b = FaceWidth;
            double innerH = 2.0 * K * T1 / (b * d1 * d1 * u / (u + 1.0));
            if (innerH < 0) innerH = 0;
            SigmaH = ZH * ZE * Zeps * Math.Sqrt(innerH);

            // 许用接触应力 (取小齿轮和大齿轮中较小值)
            double sigmaHP = double.MaxValue;
            if (PinionMaterial != null)
                sigmaHP = Math.Min(sigmaHP, PinionMaterial.SigmaHP);
            if (WheelMaterial != null)
                sigmaHP = Math.Min(sigmaHP, WheelMaterial.SigmaHP);
            if (sigmaHP == double.MaxValue)
                sigmaHP = 550; // 默认

            SH = sigmaHP / SigmaH;
            ContactStrengthOk = SH >= 1.0;

            // ========== 弯曲疲劳强度校核 ==========
            // sigma_F = 2*K*T1*YFa*YSa*Yeps / (b*d1*m)
            double baseF = 2.0 * K * T1 / (b * d1 * Module);
            SigmaF1 = baseF * YFa1 * YSa1 * Yeps;
            SigmaF2 = baseF * YFa2 * YSa2 * Yeps;

            // 许用弯曲应力
            double sigmaFP1 = double.MaxValue;
            double sigmaFP2 = double.MaxValue;
            if (PinionMaterial != null)
                sigmaFP1 = PinionMaterial.SigmaFP;
            if (WheelMaterial != null)
                sigmaFP2 = WheelMaterial.SigmaFP;
            if (sigmaFP1 == double.MaxValue) sigmaFP1 = 220;
            if (sigmaFP2 == double.MaxValue) sigmaFP2 = 220;

            SF1 = sigmaFP1 / SigmaF1;
            SF2 = sigmaFP2 / SigmaF2;
            BendingStrengthOk = (SF1 >= 1.0) && (SF2 >= 1.0);
        }

        /// <summary>
        /// 根据当量齿数获取齿形系数 YFa
        /// 近似公式: YFa = 2.06 + (-0.194+0.0006*Zv)*Zv (-0.00004*Zv^2)
        /// 基于GB/T 3480-1997标准图表拟合
        /// </summary>
        private double GetYFa(double zv)
        {
            // 3次多项式拟合标准图表
            if (zv < 12) zv = 12;
            if (zv > 400) zv = 400;

            // 基于渐开线齿廓的齿形系数拟合
            // 标准齿 ha*=1, rho_f*=0.38
            double yFa = 2.9834
                - 0.03223 * zv
                + 0.000353 * zv * zv
                - 0.00000163 * zv * zv * zv;
            return yFa;
        }

        /// <summary>
        /// 根据当量齿数获取应力修正系数 YSa
        /// 基于GB/T 3480-1997标准图表拟合
        /// </summary>
        private double GetYSa(double zv)
        {
            if (zv < 12) zv = 12;
            if (zv > 400) zv = 400;

            // 应力修正系数拟合
            double ySa = 1.4456
                + 0.004882 * zv
                - 0.0000487 * zv * zv
                + 0.000000208 * zv * zv * zv;
            return ySa;
        }

        /// <summary>
        /// 生成计算结果文本
        /// </summary>
        public string GetResultText()
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("═══════════════════════════════════════════");
            sb.AppendLine("          直齿圆锥齿轮设计计算结果");
            sb.AppendLine("═══════════════════════════════════════════");
            sb.AppendLine();

            sb.AppendLine("【一、基本参数】");
            sb.AppendLine($"  输入功率 P = {InputPower:F3} kW");
            sb.AppendLine($"  小齿轮转速 n1 = {InputSpeed:F1} rpm");
            sb.AppendLine($"  齿数比 u = {GearRatio:F3}");
            sb.AppendLine($"  小齿轮齿数 Z1 = {Z1}");
            sb.AppendLine($"  大齿轮齿数 Z2 = {Z2}");
            sb.AppendLine($"  大端模数 m = {Module:F3} mm");
            sb.AppendLine($"  压力角 alpha = {PressureAngle:F1} deg");
            sb.AppendLine($"  齿宽系数 phi_R = {FaceWidthCoeff:F3}");
            sb.AppendLine($"  轴交角 Sigma = {ShaftAngle:F1} deg");
            sb.AppendLine();

            sb.AppendLine("【二、几何参数】");
            sb.AppendLine($"  分度圆锥角 delta1 = {Delta1:F4} deg");
            sb.AppendLine($"  分度圆锥角 delta2 = {Delta2:F4} deg");
            sb.AppendLine($"  分度圆直径 d1 = {d1:F3} mm");
            sb.AppendLine($"  分度圆直径 d2 = {d2:F3} mm");
            sb.AppendLine($"  齿顶圆直径 da1 = {da1:F3} mm");
            sb.AppendLine($"  齿顶圆直径 da2 = {da2:F3} mm");
            sb.AppendLine($"  齿根圆直径 df1 = {df1:F3} mm");
            sb.AppendLine($"  齿根圆直径 df2 = {df2:F3} mm");
            sb.AppendLine($"  锥距 R = {ConeDistance:F3} mm");
            sb.AppendLine($"  齿宽 b = {FaceWidth:F3} mm");
            sb.AppendLine($"  齿顶高 ha = {Addendum:F3} mm");
            sb.AppendLine($"  齿根高 hf = {Dedendum:F3} mm");
            sb.AppendLine($"  齿高 h = {ToothHeight:F3} mm");
            sb.AppendLine($"  当量齿数 Zv1 = {Zv1:F2}");
            sb.AppendLine($"  当量齿数 Zv2 = {Zv2:F2}");
            sb.AppendLine();

            sb.AppendLine("【三、锥角参数】");
            sb.AppendLine($"  齿顶角 theta_a1 = {ThetaA1:F4} deg");
            sb.AppendLine($"  齿顶角 theta_a2 = {ThetaA2:F4} deg");
            sb.AppendLine($"  齿根角 theta_f1 = {ThetaF1:F4} deg");
            sb.AppendLine($"  齿根角 theta_f2 = {ThetaF2:F4} deg");
            sb.AppendLine($"  顶锥角 delta_a1 = {DeltaA1:F4} deg");
            sb.AppendLine($"  顶锥角 delta_a2 = {DeltaA2:F4} deg");
            sb.AppendLine($"  根锥角 delta_f1 = {DeltaF1:F4} deg");
            sb.AppendLine($"  根锥角 delta_f2 = {DeltaF2:F4} deg");
            sb.AppendLine();

            sb.AppendLine("【四、载荷分析】");
            sb.AppendLine($"  小齿轮转矩 T1 = {T1:F2} N*mm");
            sb.AppendLine($"  圆周速度 v = {PitchLineVelocity:F3} m/s");
            sb.AppendLine($"  圆周力 Ft = {Ft:F2} N");
            sb.AppendLine($"  径向力 Fr = {Fr:F2} N");
            sb.AppendLine($"  轴向力 Fa = {Fa:F2} N");
            sb.AppendLine($"  综合载荷系数 K = {K:F3}");
            sb.AppendLine();

            sb.AppendLine("【五、强度校核】");
            sb.AppendLine($"  节点区域系数 ZH = {ZH:F4}");
            sb.AppendLine($"  弹性系数 ZE = {ZE:F2} MPa^0.5");
            sb.AppendLine($"  端面重合度 epsilon_a = {EpsilonAlpha:F4}");
            sb.AppendLine($"  接触强度重合度系数 Zeps = {Zeps:F4}");
            sb.AppendLine($"  弯曲强度重合度系数 Yeps = {Yeps:F4}");
            sb.AppendLine();
            sb.AppendLine($"  齿形系数 YFa1 = {YFa1:F4}");
            sb.AppendLine($"  齿形系数 YFa2 = {YFa2:F4}");
            sb.AppendLine($"  应力修正系数 YSa1 = {YSa1:F4}");
            sb.AppendLine($"  应力修正系数 YSa2 = {YSa2:F4}");
            sb.AppendLine();

            sb.AppendLine($"  接触应力 sigma_H = {SigmaH:F2} MPa");
            double sigmaHP = double.MaxValue;
            if (PinionMaterial != null) sigmaHP = Math.Min(sigmaHP, PinionMaterial.SigmaHP);
            if (WheelMaterial != null) sigmaHP = Math.Min(sigmaHP, WheelMaterial.SigmaHP);
            if (sigmaHP == double.MaxValue) sigmaHP = 550;
            sb.AppendLine($"  许用接触应力 sigma_HP = {sigmaHP:F2} MPa");
            sb.AppendLine($"  接触强度安全系数 SH = {SH:F4}  {(ContactStrengthOk ? "[合格]" : "[不合格]")}");
            sb.AppendLine();

            sb.AppendLine($"  小齿轮弯曲应力 sigma_F1 = {SigmaF1:F2} MPa");
            double sigmaFP1 = PinionMaterial?.SigmaFP ?? 220;
            sb.AppendLine($"  小齿轮许用弯曲应力 sigma_FP1 = {sigmaFP1:F2} MPa");
            sb.AppendLine($"  小齿轮弯曲安全系数 SF1 = {SF1:F4}  {(SF1 >= 1.0 ? "[合格]" : "[不合格]")}");
            sb.AppendLine();
            sb.AppendLine($"  大齿轮弯曲应力 sigma_F2 = {SigmaF2:F2} MPa");
            double sigmaFP2 = WheelMaterial?.SigmaFP ?? 220;
            sb.AppendLine($"  大齿轮许用弯曲应力 sigma_FP2 = {sigmaFP2:F2} MPa");
            sb.AppendLine($"  大齿轮弯曲安全系数 SF2 = {SF2:F4}  {(SF2 >= 1.0 ? "[合格]" : "[不合格]")}");
            sb.AppendLine();

            sb.AppendLine("═══════════════════════════════════════════");
            if (ContactStrengthOk && BendingStrengthOk)
                sb.AppendLine("  结论: 接触强度和弯曲强度均满足要求");
            else if (!ContactStrengthOk && !BendingStrengthOk)
                sb.AppendLine("  结论: 接触强度和弯曲强度均不满足要求，需增大模数或齿宽");
            else if (!ContactStrengthOk)
                sb.AppendLine("  结论: 接触强度不满足要求，需增大齿轮尺寸");
            else
                sb.AppendLine("  结论: 弯曲强度不满足要求，需增大模数");
            sb.AppendLine("═══════════════════════════════════════════");

            return sb.ToString();
        }
    }
}
