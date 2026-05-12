using System;

namespace InquireTolerance
{
    /// <summary>
    /// 配合类型枚举
    /// </summary>
    public enum FitType
    {
        /// <summary>间隙配合</summary>
        Clearance,
        /// <summary>过渡配合</summary>
        Transition,
        /// <summary>过盈配合</summary>
        Interference
    }

    /// <summary>
    /// 配合计算结果
    /// </summary>
    public class FitResult
    {
        /// <summary>配合类型</summary>
        public FitType Type { get; set; }

        /// <summary>配合类型描述</summary>
        public string TypeDescription { get; set; }

        /// <summary>孔的上偏差 ES (微米)</summary>
        public double HoleES { get; set; }

        /// <summary>孔的下偏差 EI (微米)</summary>
        public double HoleEI { get; set; }

        /// <summary>孔的公差值 (微米)</summary>
        public double HoleTolerance { get; set; }

        /// <summary>轴的上偏差 es (微米)</summary>
        public double ShaftES { get; set; }

        /// <summary>轴的下偏差 ei (微米)</summary>
        public double ShaftEI { get; set; }

        /// <summary>轴的公差值 (微米)</summary>
        public double ShaftTolerance { get; set; }

        /// <summary>最大间隙 (微米), 仅间隙配合和过渡配合</summary>
        public double MaxClearance { get; set; }

        /// <summary>最小间隙 (微米), 仅间隙配合</summary>
        public double MinClearance { get; set; }

        /// <summary>最大过盈 (微米), 仅过盈配合和过渡配合</summary>
        public double MaxInterference { get; set; }

        /// <summary>最小过盈 (微米), 仅过盈配合</summary>
        public double MinInterference { get; set; }

        /// <summary>配合代号</summary>
        public string FitCode { get; set; }

        /// <summary>名义尺寸 (mm)</summary>
        public double NominalSize { get; set; }
    }

    /// <summary>
    /// ISO 286 配合计算器
    /// </summary>
    public static class FitCalculator
    {
        /// <summary>
        /// 计算配合结果
        /// </summary>
        /// <param name="nominalSize">名义尺寸 (mm)</param>
        /// <param name="holeCode">孔的偏差代号</param>
        /// <param name="holeGrade">孔的公差等级</param>
        /// <param name="shaftCode">轴的偏差代号</param>
        /// <param name="shaftGrade">轴的公差等级</param>
        /// <returns>配合计算结果</returns>
        public static FitResult CalculateFit(double nominalSize, string holeCode, int holeGrade, string shaftCode, int shaftGrade)
        {
            var result = new FitResult
            {
                NominalSize = nominalSize,
                FitCode = $"{holeCode}{holeGrade}/{shaftCode}{shaftGrade}"
            };

            // 计算孔的偏差
            result.HoleES = ToleranceCalculator.CalculateHoleUpperDeviation(nominalSize, holeCode, holeGrade);
            result.HoleEI = ToleranceCalculator.CalculateHoleLowerDeviation(nominalSize, holeCode, holeGrade);
            result.HoleTolerance = result.HoleES - result.HoleEI;

            // 计算轴的偏差
            result.ShaftES = ToleranceCalculator.CalculateShaftUpperDeviation(nominalSize, shaftCode, shaftGrade);
            result.ShaftEI = ToleranceCalculator.CalculateShaftLowerDeviation(nominalSize, shaftCode, shaftGrade);
            result.ShaftTolerance = result.ShaftES - result.ShaftEI;

            // 计算配合特性
            // 最大间隙 = ES(孔) - ei(轴)
            // 最小间隙 = EI(孔) - es(轴)
            // 最大过盈 = ei(孔) - es(轴)
            // 最小过盈 = ES(孔) - ei(轴)
            double maxClearance = result.HoleES - result.ShaftEI;
            double minClearance = result.HoleEI - result.ShaftES;

            if (minClearance >= 0)
            {
                // 间隙配合: 最小间隙 >= 0
                result.Type = FitType.Clearance;
                result.TypeDescription = "间隙配合";
                result.MaxClearance = maxClearance;
                result.MinClearance = minClearance;
            }
            else if (maxClearance <= 0)
            {
                // 过盈配合: 最大间隙 <= 0 (即最大过盈 >= 0)
                result.Type = FitType.Interference;
                result.TypeDescription = "过盈配合";
                // 过盈值取绝对值显示
                result.MaxInterference = Math.Abs(minClearance); // 最大过盈 = |最小间隙|
                result.MinInterference = Math.Abs(maxClearance); // 最小过盈 = |最大间隙|
            }
            else
            {
                // 过渡配合: 可能有间隙也可能有过盈
                result.Type = FitType.Transition;
                result.TypeDescription = "过渡配合";
                result.MaxClearance = maxClearance;
                result.MaxInterference = Math.Abs(minClearance);
            }

            return result;
        }

        /// <summary>
        /// 判断配合类型
        /// </summary>
        public static FitType DetermineFitType(double holeES, double holeEI, double shaftES, double shaftEI)
        {
            double maxClearance = holeES - shaftEI;
            double minClearance = holeEI - shaftES;

            if (minClearance >= 0)
                return FitType.Clearance;
            else if (maxClearance <= 0)
                return FitType.Interference;
            else
                return FitType.Transition;
        }

        /// <summary>
        /// 格式化配合结果为显示字符串
        /// </summary>
        public static string FormatFitResult(FitResult result, int precision)
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine($"配合代号: {result.FitCode}");
            sb.AppendLine($"名义尺寸: {result.NominalSize} mm");
            sb.AppendLine();
            sb.AppendLine($"孔 ({result.FitCode.Split('/')[0]}):");
            sb.AppendLine($"  上偏差 ES = {ToleranceCalculator.FormatDeviation(result.HoleES, precision)} mm");
            sb.AppendLine($"  下偏差 EI = {ToleranceCalculator.FormatDeviation(result.HoleEI, precision)} mm");
            sb.AppendLine($"  公差值 = {Math.Round(result.HoleTolerance / 1000.0, precision)} mm");
            sb.AppendLine();
            sb.AppendLine($"轴 ({result.FitCode.Split('/')[1]}):");
            sb.AppendLine($"  上偏差 es = {ToleranceCalculator.FormatDeviation(result.ShaftES, precision)} mm");
            sb.AppendLine($"  下偏差 ei = {ToleranceCalculator.FormatDeviation(result.ShaftEI, precision)} mm");
            sb.AppendLine($"  公差值 = {Math.Round(result.ShaftTolerance / 1000.0, precision)} mm");
            sb.AppendLine();
            sb.AppendLine($"配合类型: {result.TypeDescription}");

            switch (result.Type)
            {
                case FitType.Clearance:
                    sb.AppendLine($"最大间隙 = {ToleranceCalculator.FormatDeviation(result.MaxClearance, precision)} mm");
                    sb.AppendLine($"最小间隙 = {ToleranceCalculator.FormatDeviation(result.MinClearance, precision)} mm");
                    break;
                case FitType.Transition:
                    sb.AppendLine($"最大间隙 = {ToleranceCalculator.FormatDeviation(result.MaxClearance, precision)} mm");
                    sb.AppendLine($"最大过盈 = {ToleranceCalculator.FormatDeviation(-result.MaxInterference, precision)} mm");
                    break;
                case FitType.Interference:
                    sb.AppendLine($"最大过盈 = {ToleranceCalculator.FormatDeviation(-result.MaxInterference, precision)} mm");
                    sb.AppendLine($"最小过盈 = {ToleranceCalculator.FormatDeviation(-result.MinInterference, precision)} mm");
                    break;
            }

            return sb.ToString();
        }
    }
}
