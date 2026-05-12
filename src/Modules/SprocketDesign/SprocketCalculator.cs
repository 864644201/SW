using System;

namespace SprocketDesign
{
    /// <summary>
    /// 链号枚举 (对应GB/T 1243标准)
    /// </summary>
    public enum ChainNumber
    {
        _08A,   // 08A: p=12.70mm
        _10A,   // 10A: p=15.875mm
        _12A,   // 12A: p=19.05mm
        _16A,   // 16A: p=25.40mm
        _20A,   // 20A: p=31.75mm
        _24A    // 24A: p=38.10mm
    }

    /// <summary>
    /// 润滑方式
    /// </summary>
    public enum LubricationType
    {
        Manual,         // 人工定期润滑
        Drip,           // 滴油润滑
        OilBath,        // 油浴润滑
        OilStream       // 喷油润滑
    }

    /// <summary>
    /// 链条参数表
    /// </summary>
    public class ChainParams
    {
        public double Pitch { get; set; }           // 链节距 p (mm)
        public double RowPitch { get; set; }        // 排距 pt (mm), 单排为0
        public double RollerDia { get; set; }       // 滚子直径 d1 (mm)
        public double InnerWidth { get; set; }      // 内链节内宽 b1 (mm)
        public double BreakingLoad { get; set; }    // 单排极限拉伸载荷 Q (kN)
        public double MassPerMeter { get; set; }    // 每米质量 q (kg/m)
        /// <summary>
        /// 额定功率曲线系数 (简化模型)
        /// P0 = K1 * n1^0.8 * p^1.5 (kW)
        /// </summary>
        public double K1 { get; set; }
    }

    /// <summary>
    /// 链传动设计计算结果
    /// </summary>
    public class SprocketResult
    {
        // 基本参数
        public double Pitch { get; set; }             // 链节距 mm
        public int Z1 { get; set; }                   // 小链轮齿数
        public int Z2 { get; set; }                   // 大链轮齿数
        public double ChainSpeed { get; set; }        // 链速 m/s
        public double InitialCenterDist { get; set; } // 初估中心距 mm
        public double ChainLinks { get; set; }        // 链节数 Lp
        public int ChainLinksRounded { get; set; }    // 圆整链节数 (取偶数)
        public double ActualCenterDist { get; set; }  // 实际中心距 mm
        public double RatedPower { get; set; }        // 额定功率 kW
        public double SafetyFactor { get; set; }      // 安全系数
        public LubricationType Lubrication { get; set; } // 推荐润滑方式

        // 校核结果
        public bool Z1Check { get; set; }             // 小链轮齿数校核
        public bool Z2Check { get; set; }             // 大链轮齿数校核
        public bool SpeedCheck { get; set; }          // 链速校核
        public bool WearCheck { get; set; }           // 磨损校核
        public string Remarks { get; set; }           // 备注
    }

    /// <summary>
    /// 链传动设计计算器
    /// 参考标准: GB/T 1243, GB/T 18150
    /// </summary>
    public class SprocketCalculator
    {
        /// <summary>
        /// 链条参数表 (基于GB/T 1243)
        /// </summary>
        private static readonly ChainParams[] ChainTable = new ChainParams[]
        {
            // 08A: p=12.70
            new ChainParams { Pitch = 12.700, RowPitch = 14.38, RollerDia = 7.95,
                              InnerWidth = 7.85, BreakingLoad = 13.9, MassPerMeter = 0.60,
                              K1 = 0.0038 },
            // 10A: p=15.875
            new ChainParams { Pitch = 15.875, RowPitch = 18.11, RollerDia = 10.16,
                              InnerWidth = 9.40, BreakingLoad = 21.8, MassPerMeter = 1.00,
                              K1 = 0.0062 },
            // 12A: p=19.05
            new ChainParams { Pitch = 19.050, RowPitch = 22.78, RollerDia = 11.91,
                              InnerWidth = 12.57, BreakingLoad = 31.3, MassPerMeter = 1.50,
                              K1 = 0.0095 },
            // 16A: p=25.40
            new ChainParams { Pitch = 25.400, RowPitch = 29.29, RollerDia = 15.88,
                              InnerWidth = 15.75, BreakingLoad = 55.6, MassPerMeter = 2.60,
                              K1 = 0.0180 },
            // 20A: p=31.75
            new ChainParams { Pitch = 31.750, RowPitch = 35.76, RollerDia = 19.05,
                              InnerWidth = 18.90, BreakingLoad = 87.0, MassPerMeter = 3.80,
                              K1 = 0.0290 },
            // 24A: p=38.10
            new ChainParams { Pitch = 38.100, RowPitch = 45.44, RollerDia = 22.23,
                              InnerWidth = 25.22, BreakingLoad = 125.0, MassPerMeter = 5.60,
                              K1 = 0.0430 }
        };

        /// <summary>
        /// 获取链条参数
        /// </summary>
        public static ChainParams GetChainParams(ChainNumber chainNumber)
        {
            int index = (int)chainNumber;
            if (index >= 0 && index < ChainTable.Length)
                return ChainTable[index];
            return ChainTable[0];
        }

        /// <summary>
        /// 执行链传动设计计算
        /// </summary>
        /// <param name="power">传递功率 P (kW)</param>
        /// <param name="n1">小链轮转速 (rpm)</param>
        /// <param name="ratio">传动比 i</param>
        /// <param name="chainNumber">链号</param>
        /// <param name="z1">小链轮齿数, 0表示自动推荐</param>
        /// <param name="centerDist">中心距 mm, 0表示自动估算</param>
        /// <returns>计算结果</returns>
        public static SprocketResult Calculate(double power, double n1, double ratio,
                                                ChainNumber chainNumber, int z1 = 0,
                                                double centerDist = 0)
        {
            var result = new SprocketResult();
            var remarks = new System.Text.StringBuilder();

            // 1. 获取链条参数
            ChainParams chain = GetChainParams(chainNumber);
            result.Pitch = chain.Pitch;

            // 2. 小链轮齿数确定
            if (z1 <= 0)
            {
                // 推荐齿数: 根据链速选取
                // 低速(v<0.6): Z1>=13, 中速: Z1>=17, 高速(v>8): Z1>=21
                double estSpeed = Math.PI * 50 * n1 * 0.001 / 60.0; // 粗估
                if (estSpeed < 0.6) z1 = 15;
                else if (estSpeed < 3) z1 = 17;
                else if (estSpeed < 8) z1 = 19;
                else z1 = 23;
            }
            result.Z1 = z1;
            result.Z1Check = z1 >= 17;
            if (!result.Z1Check)
            {
                remarks.AppendLine($"[注意] 小链轮齿数 Z1={z1} < 17，链速不均匀性增大");
            }

            // 3. 大链轮齿数: Z2 = i * Z1
            result.Z2 = (int)Math.Round(ratio * z1);
            result.Z2Check = result.Z2 <= 120;
            if (!result.Z2Check)
            {
                remarks.AppendLine($"[警告] 大链轮齿数 Z2={result.Z2} > 120，建议减小传动比或采用多级传动");
            }

            // 4. 链速: v = Z1 * p * n1 / 60000 (m/s)
            result.ChainSpeed = z1 * chain.Pitch * n1 / 60000.0;
            result.SpeedCheck = result.ChainSpeed <= 15.0; // 一般链速限制
            if (!result.SpeedCheck)
            {
                remarks.AppendLine($"[警告] 链速 {result.ChainSpeed:F2} m/s 过高，建议使用V带传动");
            }

            // 5. 初估中心距
            if (centerDist <= 0)
            {
                // 推荐: a0 = (30~50)*p, 一般取 a0 = 40*p
                // 传动比大时取小值
                double aFactor = 40.0 - Math.Min(ratio, 6) * 2.0;
                if (aFactor < 30) aFactor = 30;
                result.InitialCenterDist = aFactor * chain.Pitch;
            }
            else
            {
                result.InitialCenterDist = centerDist;
            }

            // 6. 链节数: Lp = 2*a0/p + (Z1+Z2)/2 + ((Z2-Z1)/(2*pi))^2 * p/a0
            double a0 = result.InitialCenterDist;
            double p = chain.Pitch;
            double zAvg = (z1 + result.Z2) / 2.0;
            double zDiff = result.Z2 - z1;
            double zDiffSqTerm = Math.Pow(zDiff / (2.0 * Math.PI), 2) * p / a0;

            result.ChainLinks = 2.0 * a0 / p + zAvg + zDiffSqTerm;

            // 链节数圆整为偶数
            result.ChainLinksRounded = (int)Math.Ceiling(result.ChainLinks);
            if (result.ChainLinksRounded % 2 != 0)
                result.ChainLinksRounded += 1;

            // 7. 实际中心距: a = p * (Lp - (Z1+Z2)/2) / 2 * Ka
            // Ka为修正系数，简化取值
            double Lp = result.ChainLinksRounded;
            double KaCenterDist = 1.0; // 简化
            result.ActualCenterDist = p * (Lp - zAvg) / 2.0 * KaCenterDist;

            // 8. 额定功率计算
            // P0 = Kz * Km * Ka * Pr
            // Kz: 小链轮齿数系数
            double Kz = Math.Pow(z1 / 19.0, 1.5);
            // Km: 多排链系数 (单排=1.0)
            double Km = 1.0;
            // Ka: 工况系数 (取1.2中等冲击)
            double Ka = 1.2;
            // Pr: 特定条件下单排链额定功率
            double Pr = chain.K1 * Math.Pow(n1, 0.8) * Math.Pow(p, 1.5);
            result.RatedPower = Kz * Km * Ka * Pr;

            // 安全系数
            result.SafetyFactor = result.RatedPower / power;
            if (result.SafetyFactor < 1.0)
            {
                remarks.AppendLine($"[警告] 安全系数 {result.SafetyFactor:F2} < 1.0，链条承载能力不足");
                remarks.AppendLine("建议: 选用更大链号或增加排数");
            }

            // 9. 磨损校核 (基于铰链比压)
            // 链条磨损寿命与铰链比压密切相关
            // 有效圆周力 Ft = 1000 * P / v
            double Ft = 1000.0 * power / result.ChainSpeed;
            // 许用比压校核 (简化)
            double contactArea = chain.RollerDia * chain.InnerWidth; // mm^2
            double specificPressure = Ft / contactArea; // MPa
            double allowablePressure = 25.0; // 许用比压 (MPa, 简化值)
            result.WearCheck = specificPressure <= allowablePressure;
            if (!result.WearCheck)
            {
                remarks.AppendLine($"[警告] 铰链比压 {specificPressure:F1} MPa 超过许用值 {allowablePressure} MPa");
            }
            remarks.AppendLine($"铰链比压 = {specificPressure:F2} MPa (许用 {allowablePressure} MPa)");

            // 10. 润滑方式选择 (基于链速)
            result.Lubrication = SelectLubrication(result.ChainSpeed, chain.Pitch);
            remarks.AppendLine($"推荐润滑方式: {GetLubricationName(result.Lubrication)}");

            // 11. 压轴力估算 (简化: Ft * 1.2~1.3)
            double shaftForce = Ft * 1.25;
            remarks.AppendLine($"估算压轴力 Fr = {shaftForce:F1} N");

            result.Remarks = remarks.ToString();
            return result;
        }

        /// <summary>
        /// 根据链速和节距选择润滑方式
        /// </summary>
        private static LubricationType SelectLubrication(double speed, double pitch)
        {
            // 基于GB/T 18150润滑区域图简化
            if (speed <= 1.5)
                return LubricationType.Manual;
            else if (speed <= 4.0 && pitch <= 19.05)
                return LubricationType.Drip;
            else if (speed <= 8.0)
                return LubricationType.OilBath;
            else
                return LubricationType.OilStream;
        }

        /// <summary>
        /// 获取润滑方式名称
        /// </summary>
        private static string GetLubricationName(LubricationType type)
        {
            switch (type)
            {
                case LubricationType.Manual: return "人工定期润滑";
                case LubricationType.Drip: return "滴油润滑";
                case LubricationType.OilBath: return "油浴润滑";
                case LubricationType.OilStream: return "喷油润滑";
                default: return "未知";
            }
        }

        /// <summary>
        /// 根据功率和转速推荐链号
        /// </summary>
        public static ChainNumber RecommendChain(double power, double n1)
        {
            // 基于功率-转速曲线简化推荐
            double pv = power * n1; // 功率*转速积
            if (pv <= 500) return ChainNumber._08A;
            if (pv <= 2000) return ChainNumber._10A;
            if (pv <= 6000) return ChainNumber._12A;
            if (pv <= 20000) return ChainNumber._16A;
            if (pv <= 50000) return ChainNumber._20A;
            return ChainNumber._24A;
        }
    }
}
