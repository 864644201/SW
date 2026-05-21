using System;

namespace BoltCheck
{
    public class BoltCheckResult
    {
        public double F { get; set; }
        public double Fs { get; set; }
        public int N { get; set; }
        public double FPerBolt { get; set; }
        public double As { get; set; }
        public double D1 { get; set; }
        public double D2 { get; set; }
        public double D { get; set; }
        public double P { get; set; }
        public double Sigma { get; set; }
        public double Tau { get; set; }
        public double SigmaEq { get; set; }
        public double F0 { get; set; }
        public double F0Min { get; set; }
        public double K { get; set; }
        public double K2 { get; set; }
        public double F0Residual { get; set; }
        public double F2 { get; set; }
        public double SigmaTotal { get; set; }
        public double T { get; set; }
        public double S { get; set; }
        public double SShear { get; set; }
        public double SCombined { get; set; }
        public double SigmaS { get; set; }
        public double SigmaB { get; set; }
        public double K1 { get; set; }
        public double SigmaA { get; set; }
        public double SFatigue { get; set; }
        public double StiffnessRatio { get; set; }
        public double SafetyFactor { get; set; }
        public bool IsPassed => S >= SafetyFactor;
        public bool IsShearPassed => SShear >= SafetyFactor;
        public bool IsCombinedPassed => SCombined >= SafetyFactor;
        public bool IsFatiguePassed => SFatigue >= SafetyFactor;
        public string GradeName { get; set; }
        public string SpecName { get; set; }
        public string ScenarioName { get; set; }
        public string ConnectionType { get; set; }
        public string LoadCondition { get; set; }
        public BoltDimension RecommendDim { get; set; }
    }

    public static class BoltCalculator
    {
        /// <summary>
        /// 轴向拉伸校核（增强版，支持连接形式和工况选择）
        /// </summary>
        public static BoltCheckResult CheckTension(
            BoltDimension dim, BoltGrade grade,
            double F_kN, int n, double K, double K2, double K1,
            double CbRatio, ConnectionType connType, LoadCondition loadCond,
            double safetyFactor = 1.5)
        {
            if (n <= 0) throw new ArgumentException("螺栓数量必须大于0");

            var result = new BoltCheckResult
            {
                F = F_kN, N = n, D = dim.D, D1 = dim.D1, D2 = dim.D2,
                P = dim.P, As = dim.As, K = K, K2 = K2, K1 = K1,
                SigmaS = grade.SigmaS, SigmaB = grade.SigmaB,
                StiffnessRatio = CbRatio, GradeName = grade.Name,
                SpecName = dim.DisplayName, SafetyFactor = safetyFactor,
                ScenarioName = "轴向拉伸",
                ConnectionType = connType.ToString(),
                LoadCondition = loadCond.ToString(),
            };

            // 铰制孔螺栓用光杆截面积
            double effectiveAs = (connType == ConnectionType.铰制孔螺栓)
                ? Math.PI * dim.D * dim.D / 4.0
                : dim.As;

            result.FPerBolt = F_kN / n;
            result.Sigma = (F_kN * 1000.0) / (n * effectiveAs);
            result.F0 = K * F_kN / n;
            result.F0Residual = K2 * F_kN / n;
            result.F2 = result.F0 + CbRatio * result.FPerBolt;
            result.SigmaTotal = (result.F2 * 1000.0) / effectiveAs;
            result.T = K1 * result.F0 * dim.D;
            result.S = grade.SigmaS * effectiveAs / (result.FPerBolt * 1000.0);

            // 疲劳校核 — 根据工况调整应力幅
            double Fa_N = result.FPerBolt * 1000.0;
            switch (loadCond)
            {
                case LoadCondition.脉动循环:
                    Fa_N /= 2.0;
                    break;
                case LoadCondition.对称循环:
                    // Fa_N = full amplitude
                    break;
                case LoadCondition.静载:
                default:
                    Fa_N = 0;
                    break;
            }

            if (Fa_N > 0)
            {
                result.SigmaA = CbRatio * Fa_N / effectiveAs;
                result.SFatigue = result.SigmaA > 0 ? grade.SigmaEndurance / result.SigmaA : double.PositiveInfinity;
            }
            else
            {
                result.SigmaA = 0;
                result.SFatigue = double.PositiveInfinity;
            }

            return result;
        }

        /// <summary>
        /// 横向剪切校核
        /// </summary>
        public static BoltCheckResult CheckShear(
            BoltDimension dim, BoltGrade grade,
            double Fs_kN, int n, double mu, int jointFaces,
            ConnectionType connType, double safetyFactor = 1.5)
        {
            if (n <= 0) throw new ArgumentException("螺栓数量必须大于0");
            if (jointFaces <= 0) throw new ArgumentException("接合面数必须大于0");

            var result = new BoltCheckResult
            {
                Fs = Fs_kN, N = n, D = dim.D, D1 = dim.D1, D2 = dim.D2,
                P = dim.P, As = dim.As, SigmaS = grade.SigmaS, SigmaB = grade.SigmaB,
                GradeName = grade.Name, SpecName = dim.DisplayName,
                SafetyFactor = safetyFactor, ScenarioName = "横向剪切",
                ConnectionType = connType.ToString(),
            };

            if (connType == ConnectionType.铰制孔螺栓)
            {
                // 铰制孔螺栓：直接剪切，剪切面按光杆截面积
                double shearArea = Math.PI * dim.D * dim.D / 4.0;
                result.FPerBolt = Fs_kN / n;
                result.Tau = (Fs_kN * 1000.0) / (n * shearArea);
                // 许用剪切应力 [τ] = 0.6 * σs
                double tauAllow = 0.6 * grade.SigmaS;
                result.SShear = tauAllow / result.Tau;
                result.F0 = 0;
                result.S = double.PositiveInfinity;
                result.SFatigue = double.PositiveInfinity;
            }
            else
            {
                // 摩擦型：靠预紧力产生的摩擦力抵抗横向载荷
                // F0_req = Fs / (n * m * μ)
                double F0_req = (Fs_kN * 1000.0) / (n * jointFaces * mu);
                result.F0Min = F0_req / 1000.0; // kN
                result.FPerBolt = Fs_kN / n;
                result.Tau = 0;

                // 检查预紧力是否超过螺栓能力
                double F0_max = grade.Sp * dim.As / 1000.0; // kN, 保证应力下的最大预紧力
                result.F0 = F0_req / 1000.0;
                result.S = grade.SigmaS * dim.As / F0_req;
                result.SShear = F0_max / result.F0;
                result.SFatigue = double.PositiveInfinity;
            }

            return result;
        }

        /// <summary>
        /// 拉剪组合校核
        /// </summary>
        public static BoltCheckResult CheckCombined(
            BoltDimension dim, BoltGrade grade,
            double F_kN, double Fs_kN, int n, double K, double K2, double K1,
            double CbRatio, double mu, int jointFaces,
            ConnectionType connType, double safetyFactor = 1.5)
        {
            if (n <= 0) throw new ArgumentException("螺栓数量必须大于0");

            var result = new BoltCheckResult
            {
                F = F_kN, Fs = Fs_kN, N = n, D = dim.D, D1 = dim.D1, D2 = dim.D2,
                P = dim.P, As = dim.As, K = K, K2 = K2, K1 = K1,
                SigmaS = grade.SigmaS, SigmaB = grade.SigmaB,
                StiffnessRatio = CbRatio, GradeName = grade.Name,
                SpecName = dim.DisplayName, SafetyFactor = safetyFactor,
                ScenarioName = "拉剪组合", ConnectionType = connType.ToString(),
            };

            // 拉伸部分
            result.FPerBolt = (F_kN + Fs_kN) / n;
            result.F0 = K * (F_kN + Fs_kN) / n;
            result.F0Residual = K2 * (F_kN + Fs_kN) / n;
            result.F2 = result.F0 + CbRatio * F_kN / n;
            result.SigmaTotal = (result.F2 * 1000.0) / dim.As;
            result.Sigma = result.SigmaTotal;
            result.T = K1 * result.F0 * dim.D;

            // 剪切部分
            if (connType == ConnectionType.铰制孔螺栓)
            {
                double shearArea = Math.PI * dim.D * dim.D / 4.0;
                result.Tau = (Fs_kN * 1000.0) / (n * shearArea);
            }
            else
            {
                // 摩擦型：剪切力由摩擦力承担，螺栓本身不直接承受剪切
                result.Tau = 0;
            }

            // 等效应力（第四强度理论）
            result.SigmaEq = Math.Sqrt(result.SigmaTotal * result.SigmaTotal + 3.0 * result.Tau * result.Tau);
            result.SCombined = grade.SigmaS / result.SigmaEq;

            // 单独拉伸安全系数
            result.S = grade.SigmaS * dim.As / (result.F2 * 1000.0);
            result.SShear = result.Tau > 0 ? (0.6 * grade.SigmaS) / result.Tau : double.PositiveInfinity;

            // 疲劳
            double Fa_N = F_kN * 1000.0 / (2.0 * n);
            result.SigmaA = CbRatio * Fa_N / dim.As;
            result.SFatigue = result.SigmaA > 0 ? grade.SigmaEndurance / result.SigmaA : double.PositiveInfinity;

            return result;
        }

        /// <summary>
        /// 偏心载荷校核
        /// </summary>
        public static BoltCheckResult CheckEccentric(
            BoltDimension dim, BoltGrade grade,
            double F_kN, double ecc_mm, int n, double K, double CbRatio,
            double boltSpacing_mm, ConnectionType connType,
            double safetyFactor = 1.5)
        {
            if (n <= 0) throw new ArgumentException("螺栓数量必须大于0");
            if (n < 2) throw new ArgumentException("偏心载荷至少需要2个螺栓");

            var result = new BoltCheckResult
            {
                F = F_kN, N = n, D = dim.D, D1 = dim.D1, D2 = dim.D2,
                P = dim.P, As = dim.As, K = K,
                SigmaS = grade.SigmaS, SigmaB = grade.SigmaB,
                StiffnessRatio = CbRatio, GradeName = grade.Name,
                SpecName = dim.DisplayName, SafetyFactor = safetyFactor,
                ScenarioName = "偏心载荷", ConnectionType = connType.ToString(),
            };

            // 翻转力矩 M = F * e
            double M_kNm = F_kN * ecc_mm / 1000.0; // kN·m

            // 螺栓组对称排列，受拉侧最大螺栓力
            // 假设螺栓均匀分布在 boltSpacing 的范围内
            // 对于双排螺栓：F_max = F/n + M * y_max / Σ(y_i²)
            // 简化为对称排列：Σ(y_i²) = n/2 * (boltSpacing/2)² * 2 = n * (boltSpacing/2)²
            // 但更通用的公式用螺栓组惯性矩
            double halfSpan = (n - 1) * boltSpacing_mm / 2.0;
            double sumY2 = 0;
            for (int i = 0; i < n; i++)
            {
                double yi = -halfSpan + i * boltSpacing_mm;
                sumY2 += yi * yi;
            }

            double yMax = halfSpan;
            double M_Nmm = F_kN * 1000.0 * ecc_mm; // N·mm
            double F_bolt_extra = sumY2 > 0 ? M_Nmm * yMax / sumY2 : 0; // N

            double F_bolt_total = F_kN * 1000.0 / n + F_bolt_extra; // N

            result.FPerBolt = F_bolt_total / 1000.0; // kN
            result.Sigma = F_bolt_total / dim.As;
            result.F0 = K * result.FPerBolt;
            result.F2 = result.F0 + CbRatio * result.FPerBolt;
            result.SigmaTotal = (result.F2 * 1000.0) / dim.As;
            result.S = grade.SigmaS * dim.As / (result.F2 * 1000.0);
            result.SCombined = result.S;
            result.SShear = double.PositiveInfinity;
            result.SFatigue = double.PositiveInfinity;

            return result;
        }

        /// <summary>
        /// 法兰连接校核
        /// </summary>
        public static BoltCheckResult CheckFlange(
            BoltDimension dim, BoltGrade grade,
            double pressure_MPa, double flangeDia_mm, double boltCircleDia_mm,
            int n, double K, double CbRatio, double gasketFactor,
            double safetyFactor = 1.5)
        {
            if (n <= 0) throw new ArgumentException("螺栓数量必须大于0");

            var result = new BoltCheckResult
            {
                N = n, D = dim.D, D1 = dim.D1, D2 = dim.D2,
                P = dim.P, As = dim.As, K = K,
                SigmaS = grade.SigmaS, SigmaB = grade.SigmaB,
                StiffnessRatio = CbRatio, GradeName = grade.Name,
                SpecName = dim.DisplayName, SafetyFactor = safetyFactor,
                ScenarioName = "法兰连接", ConnectionType = "法兰螺栓",
            };

            // 轴向力 = 内压 × 法兰密封面积
            double sealArea = Math.PI * flangeDia_mm * flangeDia_mm / 4.0; // mm²
            double F_pressure = pressure_MPa * sealArea / 1000.0; // kN

            // 垫片密封力
            double F_gasket = gasketFactor * pressure_MPa * Math.PI * flangeDia_mm / 1000.0; // kN (简化)

            // 螺栓总载荷
            double F_bolts = F_pressure + F_gasket;

            result.F = F_bolts;
            result.FPerBolt = F_bolts / n;

            // 预紧力
            result.F0 = K * F_bolts / n;
            result.F2 = result.F0 + CbRatio * result.FPerBolt;
            result.Sigma = (result.FPerBolt * 1000.0) / dim.As;
            result.SigmaTotal = (result.F2 * 1000.0) / dim.As;
            result.S = grade.SigmaS * dim.As / (result.F2 * 1000.0);
            result.SCombined = result.S;
            result.SShear = double.PositiveInfinity;

            // 疲劳：压力波动
            double Fa_N = result.FPerBolt * 1000.0 / 2.0;
            result.SigmaA = CbRatio * Fa_N / dim.As;
            result.SFatigue = result.SigmaA > 0 ? grade.SigmaEndurance / result.SigmaA : double.PositiveInfinity;

            return result;
        }

        /// <summary>
        /// 推荐最小螺栓直径
        /// </summary>
        public static BoltCheckResult RecommendDiameter(
            BoltGrade grade, double F_kN, int n, double safetyFactor,
            double K, double CbRatio, bool includeFinePitch)
        {
            var dims = BoltTable.GetDimensions();
            BoltDimension best = null;

            foreach (var dim in dims)
            {
                if (!includeFinePitch && dim.IsFinePitch) continue;

                double F0 = K * F_kN / n;
                double F2 = F0 + CbRatio * F_kN / n;
                double sigmaTotal = (F2 * 1000.0) / dim.As;
                double S = grade.SigmaS * dim.As / (F2 * 1000.0);

                if (S >= safetyFactor)
                {
                    best = dim;
                    break;
                }
            }

            var result = new BoltCheckResult
            {
                F = F_kN, N = n, SafetyFactor = safetyFactor,
                SigmaS = grade.SigmaS, SigmaB = grade.SigmaB,
                GradeName = grade.Name, ScenarioName = "推荐直径",
                ConnectionType = "普通螺栓",
            };

            if (best != null)
            {
                result.SpecName = best.DisplayName;
                result.D = best.D; result.P = best.P;
                result.D1 = best.D1; result.D2 = best.D2; result.As = best.As;
                result.FPerBolt = F_kN / n;
                result.F0 = K * F_kN / n;
                result.F2 = result.F0 + CbRatio * result.FPerBolt;
                result.SigmaTotal = (result.F2 * 1000.0) / best.As;
                result.S = grade.SigmaS * best.As / (result.F2 * 1000.0);
                result.T = 0.2 * result.F0 * best.D;
                result.RecommendDim = best;
            }
            else
            {
                result.SpecName = "无满足条件的规格";
                result.S = 0;
            }

            return result;
        }
    }
}
