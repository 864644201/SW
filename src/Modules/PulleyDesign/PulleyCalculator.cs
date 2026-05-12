using System;

namespace PulleyDesign
{
    /// <summary>
    /// 带类型枚举
    /// </summary>
    public enum BeltType
    {
        V_A,    // A型V带
        V_B,    // B型V带
        V_C,    // C型V带
        V_D,    // D型V带
        V_E,    // E型V带
        Flat,   // 平带
        Timing  // 同步带
    }

    /// <summary>
    /// V带基本额定功率表参数 (P0, 单根V带在特定条件下的额定功率)
    /// 基于GB/T 13575.1标准，简化模型
    /// </summary>
    public class VBeltParams
    {
        public double TopWidth { get; set; }     // 顶宽 mm
        public double Height { get; set; }        // 高度 mm
        public double MinPulleyDia { get; set; }  // 最小带轮直径 mm
        public double UnitMass { get; set; }      // 单位长度质量 q (kg/m)
        public double MaxSpeed { get; set; }      // 最大带速 m/s
        /// <summary>
        /// 额定功率经验系数 (用于简化P0计算)
        /// P0 = (K1 - K2/d1 - K3*v^2) * v
        /// </summary>
        public double K1 { get; set; }
        public double K2 { get; set; }
        public double K3 { get; set; }
    }

    /// <summary>
    /// 带传动设计计算结果
    /// </summary>
    public class PulleyResult
    {
        // 基本参数
        public double D1 { get; set; }           // 小带轮直径 mm
        public double D2 { get; set; }           // 大带轮直径 mm
        public double BeltSpeed { get; set; }    // 带速 m/s
        public double InitialCenterDist { get; set; } // 初估中心距 mm
        public double BeltLengthL0 { get; set; } // 初算带长 mm
        public double StandardBeltLength { get; set; } // 标准带长 mm
        public double ActualCenterDist { get; set; }   // 实际中心距 mm
        public double WrapAngle1 { get; set; }   // 小带轮包角 deg
        public double WrapAngle2 { get; set; }   // 大带轮包角 deg
        public int BeltCount { get; set; }       // V带根数
        public double PreTension { get; set; }   // 单根预紧力 N
        public double ShaftForce { get; set; }   // 轴压力 N

        // 校核结果
        public bool SpeedCheck { get; set; }     // 带速校核
        public bool WrapAngleCheck { get; set; } // 包角校核
        public string Remarks { get; set; }      // 备注
    }

    /// <summary>
    /// 带传动设计计算器
    /// 参考标准: GB/T 13575.1, GB/T 11544
    /// </summary>
    public class PulleyCalculator
    {
        // 滑动率 (弹性滑动率)
        private const double SlipRate = 0.02;

        /// <summary>
        /// V带基本参数表
        /// </summary>
        private static readonly VBeltParams[] VBeltTable = new VBeltParams[]
        {
            // A型: 顶宽11, 高6, 最小直径75
            new VBeltParams { TopWidth = 11, Height = 6, MinPulleyDia = 75,
                             UnitMass = 0.10, MaxSpeed = 25, K1 = 0.45, K2 = 14.0, K3 = 0.00048 },
            // B型: 顶宽14, 高8, 最小直径125
            new VBeltParams { TopWidth = 14, Height = 8, MinPulleyDia = 125,
                             UnitMass = 0.17, MaxSpeed = 25, K1 = 0.79, K2 = 28.0, K3 = 0.00077 },
            // C型: 顶宽19, 高11, 最小直径200
            new VBeltParams { TopWidth = 19, Height = 11, MinPulleyDia = 200,
                             UnitMass = 0.30, MaxSpeed = 25, K1 = 1.42, K2 = 56.0, K3 = 0.0014 },
            // D型: 顶宽27, 高14, 最小直径355
            new VBeltParams { TopWidth = 27, Height = 14, MinPulleyDia = 355,
                             UnitMass = 0.62, MaxSpeed = 25, K1 = 2.65, K2 = 120.0, K3 = 0.0028 },
            // E型: 顶宽32, 高19, 最小直径500
            new VBeltParams { TopWidth = 32, Height = 19, MinPulleyDia = 500,
                             UnitMass = 0.90, MaxSpeed = 25, K1 = 4.10, K2 = 220.0, K3 = 0.0045 }
        };

        /// <summary>
        /// 标准带长系列 (mm), 简化选取
        /// </summary>
        private static readonly double[] StandardLengths = new double[]
        {
            400, 450, 500, 560, 630, 710, 800, 900, 1000, 1120, 1250, 1400,
            1600, 1800, 2000, 2240, 2500, 2800, 3150, 3550, 4000, 4500, 5000,
            5600, 6300, 7100, 8000, 9000, 10000, 11200, 12500
        };

        /// <summary>
        /// 获取V带参数
        /// </summary>
        public static VBeltParams GetVBeltParams(BeltType type)
        {
            int index = (int)type;
            if (index >= 0 && index < VBeltTable.Length)
                return VBeltTable[index];
            return VBeltTable[0]; // 默认A型
        }

        /// <summary>
        /// 执行带传动设计计算
        /// </summary>
        /// <param name="power">传递功率 P (kW)</param>
        /// <param name="n1">小带轮转速 (rpm)</param>
        /// <param name="ratio">传动比 i</param>
        /// <param name="d1">小带轮直径 (mm)</param>
        /// <param name="beltType">带类型</param>
        /// <param name="centerDist">中心距 (mm), 0表示自动估算</param>
        /// <returns>计算结果</returns>
        public static PulleyResult Calculate(double power, double n1, double ratio,
                                              double d1, BeltType beltType, double centerDist = 0)
        {
            var result = new PulleyResult();
            var remarks = new System.Text.StringBuilder();

            // 1. 获取带参数
            VBeltParams beltParams = null;
            bool isVBelt = beltType != BeltType.Flat && beltType != BeltType.Timing;
            if (isVBelt)
            {
                beltParams = GetVBeltParams(beltType);
            }

            // 2. 大带轮直径: d2 = i * d1 * (1 - epsilon)
            double effectiveRatio = ratio * (1 - SlipRate);
            result.D2 = effectiveRatio * d1;
            // 圆整到标准值 (按5mm圆整)
            result.D2 = Math.Round(result.D2 / 5) * 5;
            result.D1 = d1;

            // 3. 带速: v = pi * d1 * n1 / 60000 (m/s)
            result.BeltSpeed = Math.PI * d1 * n1 / 60000.0;

            // 带速校核
            double maxSpeed = isVBelt ? beltParams.MaxSpeed : 35.0; // 平带最大35m/s
            result.SpeedCheck = result.BeltSpeed <= maxSpeed;
            if (!result.SpeedCheck)
            {
                remarks.AppendLine($"[警告] 带速 {result.BeltSpeed:F2} m/s 超过最大允许值 {maxSpeed} m/s");
                remarks.AppendLine("建议: 增大小带轮转速或减小小带轮直径");
            }

            // V带最小直径校核
            if (isVBelt && d1 < beltParams.MinPulleyDia)
            {
                remarks.AppendLine($"[警告] 小带轮直径 {d1}mm 小于该型号最小允许直径 {beltParams.MinPulleyDia}mm");
            }

            // 4. 中心距确定
            if (centerDist <= 0)
            {
                // 初估中心距: a0 = 0.7*(d1+d2) ~ 2*(d1+d2), 取中间偏大值
                result.InitialCenterDist = 0.7 * (d1 + result.D2) + 0.65 * (2.0 - 0.7) * (d1 + result.D2);
            }
            else
            {
                result.InitialCenterDist = centerDist;
            }

            // 中心距范围校核
            double aMin = 0.7 * (d1 + result.D2);
            double aMax = 2.0 * (d1 + result.D2);
            if (result.InitialCenterDist < aMin || result.InitialCenterDist > aMax)
            {
                remarks.AppendLine($"[警告] 中心距超出推荐范围 [{aMin:F1}, {aMax:F1}] mm");
            }

            // 5. 带长计算: L0 = 2*a0 + pi*(d1+d2)/2 + (d2-d1)^2/(4*a0)
            result.BeltLengthL0 = 2 * result.InitialCenterDist
                + Math.PI * (d1 + result.D2) / 2.0
                + Math.Pow(result.D2 - d1, 2) / (4.0 * result.InitialCenterDist);

            // 选取标准带长
            result.StandardBeltLength = SelectStandardLength(result.BeltLengthL0);

            // 6. 实际中心距: a = a0 + (Ld - L0) / 2
            result.ActualCenterDist = result.InitialCenterDist
                + (result.StandardBeltLength - result.BeltLengthL0) / 2.0;

            // 7. 包角计算: alpha1 = 180 - (d2-d1)*57.3/a
            result.WrapAngle1 = 180.0 - (result.D2 - d1) * 57.3 / result.ActualCenterDist;
            result.WrapAngle2 = 360.0 - result.WrapAngle1;

            // 包角校核 (V带 >= 120度, 平带 >= 150度)
            double minWrapAngle = isVBelt ? 120.0 : 150.0;
            result.WrapAngleCheck = result.WrapAngle1 >= minWrapAngle;
            if (!result.WrapAngleCheck)
            {
                remarks.AppendLine($"[警告] 小带轮包角 {result.WrapAngle1:F1} 度 小于最小允许值 {minWrapAngle} 度");
                remarks.AppendLine("建议: 增大中心距或减小传动比");
            }

            // 8. V带根数计算
            if (isVBelt && beltParams != null)
            {
                // 单根V带额定功率 P0
                double v = result.BeltSpeed;
                double P0 = (beltParams.K1 - beltParams.K2 / d1 - beltParams.K3 * v * v) * v;

                // 包角修正系数 K_alpha
                double Kalpha = WrapAngleFactor(result.WrapAngle1);

                // 带长修正系数 K_L (简化公式)
                double Ld_ref = GetReferenceLength(beltType);
                double Kl = 1.0 + 0.05 * Math.Log(result.StandardBeltLength / Ld_ref);

                // 工况系数 Ka (取1.2作为中等冲击)
                double Ka = 1.2;

                // 根数: z >= P / (P0 * Kalpha * Kl * Ka)
                if (P0 > 0)
                {
                    double zCalc = power / (P0 * Kalpha * Kl * Ka);
                    result.BeltCount = (int)Math.Ceiling(zCalc);
                    if (result.BeltCount < 1) result.BeltCount = 1;

                    remarks.AppendLine($"单根V带额定功率 P0 = {P0:F3} kW");
                    remarks.AppendLine($"包角修正系数 K_alpha = {Kalpha:F3}");
                    remarks.AppendLine($"带长修正系数 K_L = {Kl:F3}");
                    remarks.AppendLine($"工况系数 K_a = {Ka:F1}");

                    // 限制根数 (一般不超过8-10根)
                    if (result.BeltCount > 8)
                    {
                        remarks.AppendLine($"[警告] V带根数 {result.BeltCount} 过多，建议更换带型或增大直径");
                    }

                    // 9. 预紧力: F0 = 500*P/(z*v)*(2.5/Kalpha - 1) + q*v^2
                    double q = beltParams.UnitMass;
                    result.PreTension = 500.0 * power / (result.BeltCount * v) * (2.5 / Kalpha - 1)
                                       + q * v * v;

                    // 10. 轴压力: Fr = 2*z*F0*sin(alpha1/2)
                    double alphaRad = result.WrapAngle1 * Math.PI / 180.0;
                    result.ShaftForce = 2 * result.BeltCount * result.PreTension * Math.Sin(alphaRad / 2.0);
                }
                else
                {
                    result.BeltCount = 1;
                    result.PreTension = 0;
                    result.ShaftForce = 0;
                    remarks.AppendLine("[错误] P0计算值异常，请检查参数");
                }
            }
            else
            {
                // 平带 / 同步带 简化处理
                result.BeltCount = 1;
                // 平带功率估算: P = F*v/1000
                double beltWidth = power * 1000 / (result.BeltSpeed * 3.0); // 假设有效拉力3MPa*宽
                result.PreTension = power * 1000 / result.BeltSpeed * 1.5;  // 简化
                double alphaRad = result.WrapAngle1 * Math.PI / 180.0;
                result.ShaftForce = 2 * result.PreTension * Math.Sin(alphaRad / 2.0);
                remarks.AppendLine($"平带/同步带计算为简化模型");
                remarks.AppendLine($"建议带宽 >= {beltWidth:F1} mm");
            }

            result.Remarks = remarks.ToString();
            return result;
        }

        /// <summary>
        /// 包角修正系数 K_alpha
        /// 基于经验公式: K_alpha = 1.25*(1 - 5^(-alpha1/180))
        /// </summary>
        private static double WrapAngleFactor(double alphaDeg)
        {
            // 简化的包角修正系数
            if (alphaDeg >= 180) return 1.0;
            if (alphaDeg >= 170) return 0.98;
            if (alphaDeg >= 160) return 0.95;
            if (alphaDeg >= 150) return 0.92;
            if (alphaDeg >= 140) return 0.89;
            if (alphaDeg >= 130) return 0.86;
            if (alphaDeg >= 120) return 0.82;
            if (alphaDeg >= 110) return 0.78;
            if (alphaDeg >= 100) return 0.74;
            return 0.70;
        }

        /// <summary>
        /// 获取带型参考长度
        /// </summary>
        private static double GetReferenceLength(BeltType type)
        {
            switch (type)
            {
                case BeltType.V_A: return 1250;
                case BeltType.V_B: return 1600;
                case BeltType.V_C: return 2240;
                case BeltType.V_D: return 3550;
                case BeltType.V_E: return 5000;
                default: return 2000;
            }
        }

        /// <summary>
        /// 选取最接近的标准带长
        /// </summary>
        private static double SelectStandardLength(double L0)
        {
            double closest = StandardLengths[0];
            double minDiff = Math.Abs(L0 - closest);
            foreach (double L in StandardLengths)
            {
                double diff = Math.Abs(L0 - L);
                if (diff < minDiff)
                {
                    minDiff = diff;
                    closest = L;
                }
            }
            return closest;
        }

        /// <summary>
        /// 根据功率和转速推荐V带型号
        /// </summary>
        public static BeltType RecommendBeltType(double power, double n1)
        {
            if (power <= 1.0) return BeltType.V_A;
            if (power <= 3.0) return BeltType.V_B;
            if (power <= 7.5) return BeltType.V_C;
            if (power <= 20.0) return BeltType.V_D;
            return BeltType.V_E;
        }
    }
}
