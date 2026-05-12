using System;

namespace BoltCheck
{
    /// <summary>
    /// 螺栓校核计算结果
    /// </summary>
    public class BoltCheckResult
    {
        /// <summary>轴向工作载荷 F (kN)</summary>
        public double F { get; set; }
        /// <summary>螺栓数量 n</summary>
        public int N { get; set; }
        /// <summary>单个螺栓工作载荷 (kN)</summary>
        public double FPerBolt { get; set; }
        /// <summary>应力截面积 As (mm^2)</summary>
        public double As { get; set; }
        /// <summary>螺栓小径 d1 (mm)</summary>
        public double D1 { get; set; }
        /// <summary>螺栓中径 d2 (mm)</summary>
        public double D2 { get; set; }
        /// <summary>公称直径 d (mm)</summary>
        public double D { get; set; }
        /// <summary>螺距 P (mm)</summary>
        public double P { get; set; }
        /// <summary>拉伸应力 sigma (MPa)</summary>
        public double Sigma { get; set; }
        /// <summary>预紧力 F0 (kN)</summary>
        public double F0 { get; set; }
        /// <summary>预紧力系数 K</summary>
        public double K { get; set; }
        /// <summary>残余预紧力系数 K2</summary>
        public double K2 { get; set; }
        /// <summary>残余预紧力 F0' (kN)</summary>
        public double F0Residual { get; set; }
        /// <summary>总拉力 F2 (kN)</summary>
        public double F2 { get; set; }
        /// <summary>总拉伸应力 sigma_total (MPa)</summary>
        public double SigmaTotal { get; set; }
        /// <summary>拧紧力矩 T (N.m)</summary>
        public double T { get; set; }
        /// <summary>安全系数 S</summary>
        public double S { get; set; }
        /// <summary>屈服强度 sigma_s (MPa)</summary>
        public double SigmaS { get; set; }
        /// <summary>抗拉强度 sigma_b (MPa)</summary>
        public double SigmaB { get; set; }
        /// <summary>拧紧力矩系数 K1 (0.2 dry, 0.15 oiled)</summary>
        public double K1 { get; set; }
        /// <summary>疲劳应力幅 sigma_a (MPa)</summary>
        public double SigmaA { get; set; }
        /// <summary>疲劳安全系数 S_f</summary>
        public double SFatigue { get; set; }
        /// <summary>螺栓刚度比 C_b/(C_b+C_m)</summary>
        public double StiffnessRatio { get; set; }
        /// <summary>是否通过安全校核</summary>
        public bool IsPassed => S >= 1.5;
        /// <summary>是否通过疲劳校核</summary>
        public bool IsFatiguePassed => SFatigue >= 1.5;
        /// <summary>等级名称</summary>
        public string GradeName { get; set; }
        /// <summary>螺栓规格名称</summary>
        public string SpecName { get; set; }
    }

    /// <summary>
    /// 螺栓校核计算器
    /// </summary>
    public static class BoltCalculator
    {
        /// <summary>
        /// 执行螺栓强度校核计算
        /// </summary>
        /// <param name="dim">螺栓尺寸参数</param>
        /// <param name="grade">材料等级</param>
        /// <param name="F_kN">轴向工作载荷 (kN)</param>
        /// <param name="n">螺栓数量</param>
        /// <param name="K">预紧力系数 (1.5~2.5)</param>
        /// <param name="K2">残余预紧力系数</param>
        /// <param name="K1">拧紧力矩系数 (0.2 dry, 0.15 oiled)</param>
        /// <param name="CbRatio">螺栓刚度比 C_b/(C_b+C_m)，通常0.2~0.4</param>
        /// <returns>校核结果</returns>
        public static BoltCheckResult Check(
            BoltDimension dim,
            BoltGrade grade,
            double F_kN,
            int n,
            double K,
            double K2,
            double K1,
            double CbRatio)
        {
            if (n <= 0) throw new ArgumentException("螺栓数量必须大于0");

            var result = new BoltCheckResult
            {
                F = F_kN,
                N = n,
                D = dim.D,
                D1 = dim.D1,
                D2 = dim.D2,
                P = dim.P,
                As = dim.As,
                K = K,
                K2 = K2,
                K1 = K1,
                SigmaS = grade.SigmaS,
                SigmaB = grade.SigmaB,
                StiffnessRatio = CbRatio,
                GradeName = grade.Name,
                SpecName = dim.DisplayName,
            };

            // 1. 单个螺栓工作载荷 (kN)
            result.FPerBolt = F_kN / n;

            // 2. 拉伸应力 sigma = F / (n * As) [MPa, 注意 F 转换为 N]
            result.Sigma = (F_kN * 1000.0) / (n * dim.As);

            // 3. 预紧力 F0 = K * F / n (kN)
            result.F0 = K * F_kN / n;

            // 4. 残余预紧力 F0' = K2 * F / n (kN)
            result.F0Residual = K2 * F_kN / n;

            // 5. 总拉力 F2 = F0 + C_b/(C_b+C_m) * F/n (kN)
            //    考虑螺栓刚度比的影响
            result.F2 = result.F0 + CbRatio * result.FPerBolt;

            // 6. 总拉伸应力 sigma_total = F2 / As [MPa]
            result.SigmaTotal = (result.F2 * 1000.0) / dim.As;

            // 7. 拧紧力矩 T = K1 * F0 * d (N.m)
            //    F0 in kN, d in mm => T = K1 * F0 * 1000 * d / 1000 = K1 * F0 * d (N.m)
            result.T = K1 * result.F0 * dim.D;

            // 8. 安全系数 S = sigma_s * As / F (单螺栓, F in N)
            result.S = grade.SigmaS * dim.As / (result.FPerBolt * 1000.0);

            // 9. 疲劳校核
            //    应力幅 sigma_a = (C_b/(C_b+C_m)) * F_a / As
            //    假设载荷脉动循环，F_a = F_per_bolt / 2
            double Fa_N = result.FPerBolt * 1000.0 / 2.0;
            result.SigmaA = CbRatio * Fa_N / dim.As;

            //    疲劳安全系数 S_f = sigma_(-1) / sigma_a
            if (result.SigmaA > 0)
            {
                result.SFatigue = grade.SigmaEndurance / result.SigmaA;
            }
            else
            {
                result.SFatigue = double.PositiveInfinity;
            }

            return result;
        }

        /// <summary>
        /// 由公称直径 d 计算应力截面积 (经验公式)
        /// As = pi/4 * ((d2+d3)/2)^2, 其中 d3 = d1 - H/6, H = sqrt(3)/2 * P
        /// </summary>
        public static double CalcStressArea(double d1, double d2, double P)
        {
            double H = Math.Sqrt(3.0) / 2.0 * P;
            double d3 = d1 - H / 6.0;
            double davg = (d2 + d3) / 2.0;
            return Math.PI / 4.0 * davg * davg;
        }
    }
}
