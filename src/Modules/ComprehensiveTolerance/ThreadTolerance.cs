using System;
using System.Collections.Generic;

namespace ComprehensiveTolerance
{
    /// <summary>
    /// 螺纹公差计算器
    /// 基于 GB/T 197-2003 (普通螺纹 公差)
    /// 基于 GB/T 196-2003 (普通螺纹 基本尺寸)
    /// </summary>
    public static class ThreadToleranceCalculator
    {
        /// <summary>
        /// 螺纹基本尺寸信息
        /// </summary>
        public class ThreadInfo
        {
            public string Spec { get; set; }
            public double MajorDiameter { get; set; }  // 大径 d/D
            public double Pitch { get; set; }           // 螺距 P
            public double PitchDiameter { get; set; }   // 中径 d2/D2
            public double MinorDiameter { get; set; }   // 小径 d1/D1
            public double ThreadHeight { get; set; }    // 原始三角形高度 H
            public bool IsCoarse { get; set; }
        }

        /// <summary>
        /// 螺纹公差计算结果
        /// </summary>
        public class ThreadToleranceResult
        {
            public string ToleranceGrade { get; set; }
            public double MajorDiaTolerance { get; set; }
            public double PitchDiaTolerance { get; set; }
            public double MinorDiaTolerance { get; set; }
            public double MajorDiaMax { get; set; }
            public double MajorDiaMin { get; set; }
            public double PitchDiaMax { get; set; }
            public double PitchDiaMin { get; set; }
            public double MinorDiaMax { get; set; }
            public double MinorDiaMin { get; set; }
            public string FitDescription { get; set; }
        }

        /// <summary>
        /// 螺纹基本尺寸数据 (公称直径 -> 粗牙/细牙数据)
        /// 数据来源: GB/T 196-2003
        /// </summary>
        private static readonly Dictionary<string, CoarseFineData> ThreadData = new Dictionary<string, CoarseFineData>
        {
            ["M1"] = new CoarseFineData(1.0, 0.25, null),
            ["M1.2"] = new CoarseFineData(1.2, 0.25, null),
            ["M1.4"] = new CoarseFineData(1.4, 0.3, null),
            ["M1.6"] = new CoarseFineData(1.6, 0.35, null),
            ["M2"] = new CoarseFineData(2.0, 0.4, null),
            ["M2.5"] = new CoarseFineData(2.5, 0.45, null),
            ["M3"] = new CoarseFineData(3.0, 0.5, new double[] { 0.35 }),
            ["M4"] = new CoarseFineData(4.0, 0.7, new double[] { 0.5 }),
            ["M5"] = new CoarseFineData(5.0, 0.8, new double[] { 0.5 }),
            ["M6"] = new CoarseFineData(6.0, 1.0, new double[] { 0.75 }),
            ["M8"] = new CoarseFineData(8.0, 1.25, new double[] { 1.0, 0.75 }),
            ["M10"] = new CoarseFineData(10.0, 1.5, new double[] { 1.25, 1.0, 0.75 }),
            ["M12"] = new CoarseFineData(12.0, 1.75, new double[] { 1.5, 1.25, 1.0 }),
            ["M14"] = new CoarseFineData(14.0, 2.0, new double[] { 1.5, 1.25, 1.0 }),
            ["M16"] = new CoarseFineData(16.0, 2.0, new double[] { 1.5, 1.0 }),
            ["M18"] = new CoarseFineData(18.0, 2.5, new double[] { 2.0, 1.5, 1.0 }),
            ["M20"] = new CoarseFineData(20.0, 2.5, new double[] { 2.0, 1.5, 1.0 }),
            ["M22"] = new CoarseFineData(22.0, 2.5, new double[] { 2.0, 1.5, 1.0 }),
            ["M24"] = new CoarseFineData(24.0, 3.0, new double[] { 2.0, 1.5, 1.0 }),
            ["M27"] = new CoarseFineData(27.0, 3.0, new double[] { 2.0, 1.5, 1.0 }),
            ["M30"] = new CoarseFineData(30.0, 3.5, new double[] { 2.0, 1.5, 1.0 }),
            ["M33"] = new CoarseFineData(33.0, 3.5, new double[] { 2.0, 1.5 }),
            ["M36"] = new CoarseFineData(36.0, 4.0, new double[] { 3.0, 2.0, 1.5 }),
            ["M39"] = new CoarseFineData(39.0, 4.0, new double[] { 3.0, 2.0, 1.5 }),
            ["M42"] = new CoarseFineData(42.0, 4.5, new double[] { 3.0, 2.0, 1.5 }),
            ["M45"] = new CoarseFineData(45.0, 4.5, new double[] { 3.0, 2.0, 1.5 }),
            ["M48"] = new CoarseFineData(48.0, 5.0, new double[] { 3.0, 2.0, 1.5 }),
            ["M52"] = new CoarseFineData(52.0, 5.0, new double[] { 3.0, 2.0, 1.5 }),
            ["M56"] = new CoarseFineData(56.0, 5.5, new double[] { 4.0, 2.0, 1.5 }),
            ["M60"] = new CoarseFineData(60.0, 5.5, new double[] { 4.0, 2.0, 1.5 }),
            ["M64"] = new CoarseFineData(64.0, 6.0, new double[] { 4.0, 2.0, 1.5 }),
            ["M68"] = new CoarseFineData(68.0, 6.0, new double[] { 4.0, 2.0, 1.5 }),
        };

        private class CoarseFineData
        {
            public double NominalDiameter;
            public double CoarsePitch;
            public double[] FinePitches;

            public CoarseFineData(double dia, double coarsePitch, double[] finePitches)
            {
                NominalDiameter = dia;
                CoarsePitch = coarsePitch;
                FinePitches = finePitches;
            }
        }

        /// <summary>
        /// 获取螺纹基本尺寸
        /// </summary>
        /// <param name="spec">螺纹规格,如 "M10"</param>
        /// <param name="isCoarse">是否为粗牙</param>
        /// <returns>螺纹基本尺寸信息</returns>
        public static ThreadInfo GetThreadInfo(string spec, bool isCoarse)
        {
            if (!ThreadData.TryGetValue(spec, out CoarseFineData data))
                return null;

            double pitch;
            if (isCoarse)
            {
                pitch = data.CoarsePitch;
            }
            else
            {
                // 细牙: 取第一个细牙螺距
                if (data.FinePitches == null || data.FinePitches.Length == 0)
                    pitch = data.CoarsePitch;
                else
                    pitch = data.FinePitches[0];
            }

            return CalculateThreadDimensions(data.NominalDiameter, pitch, spec, isCoarse);
        }

        /// <summary>
        /// 计算螺纹基本尺寸
        /// 公式来源: GB/T 196-2003
        /// H = (√3/2) × P = 0.866025 × P
        /// d2 = d - 2 × (3H/8) = d - 0.6495 × P
        /// d1 = d - 2 × (5H/8) = d - 1.0825 × P
        /// </summary>
        private static ThreadInfo CalculateThreadDimensions(double d, double pitch, string spec, bool isCoarse)
        {
            double H = 0.866025 * pitch;  // 原始三角形高度
            double d2 = d - 0.6495 * pitch; // 中径
            double d1 = d - 1.0825 * pitch; // 小径

            return new ThreadInfo
            {
                Spec = spec,
                MajorDiameter = d,
                Pitch = pitch,
                PitchDiameter = d2,
                MinorDiameter = d1,
                ThreadHeight = H,
                IsCoarse = isCoarse
            };
        }

        /// <summary>
        /// 计算螺纹公差
        /// 数据来源: GB/T 197-2003
        /// </summary>
        /// <param name="spec">螺纹规格</param>
        /// <param name="toleranceGrade">公差带代号,如 "6H", "6g"</param>
        /// <param name="isCoarse">是否为粗牙</param>
        /// <returns>公差计算结果</returns>
        public static ThreadToleranceResult CalculateTolerance(string spec, string toleranceGrade, bool isCoarse)
        {
            var info = GetThreadInfo(spec, isCoarse);
            if (info == null) return null;

            var result = new ThreadToleranceResult();
            result.ToleranceGrade = toleranceGrade;

            double d = info.MajorDiameter;
            double pitch = info.Pitch;

            // 根据公差等级和螺距计算公差值
            // GB/T 197-2003 公差值表 (简化计算)
            // 公差值取决于螺距和公称直径

            bool isInternal = toleranceGrade.Contains("H") || toleranceGrade.Contains("G");
            bool isExternal = toleranceGrade.Contains("h") || toleranceGrade.Contains("g") ||
                              toleranceGrade.Contains("e") || toleranceGrade.Contains("f");

            if (isInternal)
            {
                // 内螺纹 (母螺纹)
                CalculateInternalThreadTolerance(result, info, toleranceGrade);
            }
            else if (isExternal)
            {
                // 外螺纹 (公螺纹)
                CalculateExternalThreadTolerance(result, info, toleranceGrade);
            }

            // 配合描述
            result.FitDescription = GetFitDescription(toleranceGrade);

            return result;
        }

        /// <summary>
        /// 计算内螺纹公差 (GB/T 197-2003)
        /// 内螺纹: 大径 D, 中径 D2, 小径 D1
        /// 下偏差 EI = 0 (H级) 或 正值 (G级)
        /// 上偏差 ES = EI + T (T为公差值)
        /// </summary>
        private static void CalculateInternalThreadTolerance(ThreadToleranceResult result, ThreadInfo info, string grade)
        {
            double pitch = info.Pitch;
            double d = info.MajorDiameter;

            // 公差值计算 (基于 GB/T 197-2003 公差表)
            // 小径公差 TD1
            double TD1 = GetInternalMinorDiaTolerance(pitch, grade);
            // 中径公差 TD2
            double TD2 = GetInternalPitchDiaTolerance(pitch, grade);
            // 大径公差 (内螺纹大径不规定公差,由刀具保证)
            double TD = 0;

            // 基本偏差 (下偏差 EI)
            double EI;
            if (grade.Contains("H"))
                EI = 0;
            else if (grade.Contains("G"))
                EI = GetGDeviation(pitch);
            else
                EI = 0;

            // 小径
            result.MinorDiaTolerance = TD1;
            result.MinorDiaMin = info.MinorDiameter + EI;
            result.MinorDiaMax = result.MinorDiaMin + TD1;

            // 中径
            result.PitchDiaTolerance = TD2;
            result.PitchDiaMin = info.PitchDiameter + EI;
            result.PitchDiaMax = result.PitchDiaMin + TD2;

            // 大径
            result.MajorDiaTolerance = TD;
            result.MajorDiaMax = info.MajorDiameter;
            result.MajorDiaMin = info.MajorDiameter;
        }

        /// <summary>
        /// 计算外螺纹公差 (GB/T 197-2003)
        /// 外螺纹: 大径 d, 中径 d2, 小径 d1
        /// 上偏差 es = 0 (h级) 或 负值 (g, f, e级)
        /// 下偏差 ei = es - T
        /// </summary>
        private static void CalculateExternalThreadTolerance(ThreadToleranceResult result, ThreadInfo info, string grade)
        {
            double pitch = info.Pitch;
            double d = info.MajorDiameter;

            // 公差值计算
            // 大径公差 Td
            double Td = GetExternalMajorDiaTolerance(pitch, grade);
            // 中径公差 Td2
            double Td2 = GetExternalPitchDiaTolerance(pitch, grade);

            // 基本偏差 (上偏差 es)
            double es;
            if (grade.Contains("h"))
                es = 0;
            else if (grade.Contains("g"))
                es = -GetgDeviation(pitch);
            else if (grade.Contains("f"))
                es = -GetfDeviation(pitch);
            else if (grade.Contains("e"))
                es = -GeteDeviation(pitch);
            else
                es = 0;

            // 大径
            result.MajorDiaTolerance = Td;
            result.MajorDiaMax = info.MajorDiameter + es;
            result.MajorDiaMin = result.MajorDiaMax - Td;

            // 中径
            result.PitchDiaTolerance = Td2;
            result.PitchDiaMax = info.PitchDiameter + es;
            result.PitchDiaMin = result.PitchDiaMax - Td2;

            // 小径 (外螺纹小径由刀具保证,不规定公差)
            result.MinorDiaTolerance = 0;
            result.MinorDiaMax = info.MinorDiameter;
            result.MinorDiaMin = info.MinorDiameter;
        }

        /// <summary>
        /// 内螺纹小径公差 TD1 (μm) - GB/T 197-2003
        /// </summary>
        private static double GetInternalMinorDiaTolerance(double pitch, string grade)
        {
            // 公差等级系数
            int level = GetToleranceLevel(grade);
            double baseFactor = GetSmallDiaBaseTolerance(pitch);

            return baseFactor * GetLevelMultiplier(level) / 1000.0; // 转换为 mm
        }

        /// <summary>
        /// 内螺纹中径公差 TD2 (μm) - GB/T 197-2003
        /// </summary>
        private static double GetInternalPitchDiaTolerance(double pitch, string grade)
        {
            int level = GetToleranceLevel(grade);
            double baseFactor = GetPitchDiaBaseTolerance(pitch);

            return baseFactor * GetLevelMultiplier(level) / 1000.0;
        }

        /// <summary>
        /// 外螺纹大径公差 Td (μm) - GB/T 197-2003
        /// </summary>
        private static double GetExternalMajorDiaTolerance(double pitch, string grade)
        {
            int level = GetToleranceLevel(grade);
            double baseFactor = GetMajorDiaBaseTolerance(pitch);

            return baseFactor * GetLevelMultiplier(level) / 1000.0;
        }

        /// <summary>
        /// 外螺纹中径公差 Td2 (μm) - GB/T 197-2003
        /// </summary>
        private static double GetExternalPitchDiaTolerance(double pitch, string grade)
        {
            int level = GetToleranceLevel(grade);
            double baseFactor = GetPitchDiaBaseTolerance(pitch);

            return baseFactor * GetLevelMultiplier(level) / 1000.0;
        }

        /// <summary>
        /// 从公差带代号提取公差等级数字
        /// </summary>
        private static int GetToleranceLevel(string grade)
        {
            for (int i = 0; i < grade.Length; i++)
            {
                if (char.IsDigit(grade[i]))
                    return grade[i] - '0';
            }
            return 6; // 默认6级
        }

        /// <summary>
        /// 公差等级倍率因子 (IT等级)
        /// </summary>
        private static double GetLevelMultiplier(int level)
        {
            switch (level)
            {
                case 3: return 0.50;
                case 4: return 0.63;
                case 5: return 0.80;
                case 6: return 1.00;
                case 7: return 1.25;
                case 8: return 1.60;
                case 9: return 2.00;
                default: return 1.00;
            }
        }

        /// <summary>
        /// 小径基本公差 (基于螺距, μm)
        /// </summary>
        private static double GetSmallDiaBaseTolerance(double pitch)
        {
            // GB/T 197-2003 6级公差参考值
            if (pitch <= 0.25) return 38;
            if (pitch <= 0.35) return 45;
            if (pitch <= 0.5) return 56;
            if (pitch <= 0.7) return 71;
            if (pitch <= 0.75) return 75;
            if (pitch <= 0.8) return 80;
            if (pitch <= 1.0) return 90;
            if (pitch <= 1.25) return 112;
            if (pitch <= 1.5) return 132;
            if (pitch <= 1.75) return 150;
            if (pitch <= 2.0) return 170;
            if (pitch <= 2.5) return 212;
            if (pitch <= 3.0) return 265;
            if (pitch <= 3.5) return 300;
            if (pitch <= 4.0) return 335;
            if (pitch <= 4.5) return 375;
            if (pitch <= 5.0) return 425;
            if (pitch <= 5.5) return 450;
            if (pitch <= 6.0) return 500;
            return 500;
        }

        /// <summary>
        /// 中径基本公差 (基于螺距, μm)
        /// </summary>
        private static double GetPitchDiaBaseTolerance(double pitch)
        {
            // GB/T 197-2003 6级公差参考值
            if (pitch <= 0.25) return 28;
            if (pitch <= 0.35) return 34;
            if (pitch <= 0.5) return 42;
            if (pitch <= 0.7) return 50;
            if (pitch <= 0.75) return 53;
            if (pitch <= 0.8) return 56;
            if (pitch <= 1.0) return 63;
            if (pitch <= 1.25) return 75;
            if (pitch <= 1.5) return 85;
            if (pitch <= 1.75) return 95;
            if (pitch <= 2.0) return 106;
            if (pitch <= 2.5) return 125;
            if (pitch <= 3.0) return 140;
            if (pitch <= 3.5) return 160;
            if (pitch <= 4.0) return 180;
            if (pitch <= 4.5) return 190;
            if (pitch <= 5.0) return 200;
            if (pitch <= 5.5) return 212;
            if (pitch <= 6.0) return 224;
            return 224;
        }

        /// <summary>
        /// 大径基本公差 (基于螺距, μm)
        /// </summary>
        private static double GetMajorDiaBaseTolerance(double pitch)
        {
            // GB/T 197-2003 6级公差参考值
            if (pitch <= 0.25) return 36;
            if (pitch <= 0.35) return 42;
            if (pitch <= 0.5) return 50;
            if (pitch <= 0.7) return 60;
            if (pitch <= 0.75) return 63;
            if (pitch <= 0.8) return 67;
            if (pitch <= 1.0) return 75;
            if (pitch <= 1.25) return 90;
            if (pitch <= 1.5) return 106;
            if (pitch <= 1.75) return 118;
            if (pitch <= 2.0) return 132;
            if (pitch <= 2.5) return 160;
            if (pitch <= 3.0) return 180;
            if (pitch <= 3.5) return 200;
            if (pitch <= 4.0) return 224;
            if (pitch <= 4.5) return 236;
            if (pitch <= 5.0) return 250;
            if (pitch <= 5.5) return 265;
            if (pitch <= 6.0) return 280;
            return 280;
        }

        /// <summary>
        /// G 级内螺纹下偏差 EI (μm)
        /// </summary>
        private static double GetGDeviation(double pitch)
        {
            if (pitch <= 0.25) return 14;
            if (pitch <= 0.35) return 16;
            if (pitch <= 0.5) return 18;
            if (pitch <= 0.7) return 20;
            if (pitch <= 0.75) return 22;
            if (pitch <= 0.8) return 22;
            if (pitch <= 1.0) return 24;
            if (pitch <= 1.25) return 28;
            if (pitch <= 1.5) return 32;
            if (pitch <= 1.75) return 34;
            if (pitch <= 2.0) return 38;
            if (pitch <= 2.5) return 42;
            if (pitch <= 3.0) return 48;
            if (pitch <= 3.5) return 53;
            if (pitch <= 4.0) return 56;
            if (pitch <= 4.5) return 60;
            if (pitch <= 5.0) return 63;
            if (pitch <= 5.5) return 67;
            if (pitch <= 6.0) return 71;
            return 71;
        }

        /// <summary>
        /// g 级外螺纹上偏差 es (μm, 绝对值)
        /// </summary>
        private static double GetgDeviation(double pitch)
        {
            if (pitch <= 0.25) return 14;
            if (pitch <= 0.35) return 16;
            if (pitch <= 0.5) return 18;
            if (pitch <= 0.7) return 20;
            if (pitch <= 0.75) return 22;
            if (pitch <= 0.8) return 22;
            if (pitch <= 1.0) return 24;
            if (pitch <= 1.25) return 28;
            if (pitch <= 1.5) return 32;
            if (pitch <= 1.75) return 34;
            if (pitch <= 2.0) return 38;
            if (pitch <= 2.5) return 42;
            if (pitch <= 3.0) return 48;
            if (pitch <= 3.5) return 53;
            if (pitch <= 4.0) return 56;
            if (pitch <= 4.5) return 60;
            if (pitch <= 5.0) return 63;
            if (pitch <= 5.5) return 67;
            if (pitch <= 6.0) return 71;
            return 71;
        }

        /// <summary>
        /// f 级外螺纹上偏差 es (μm, 绝对值)
        /// </summary>
        private static double GetfDeviation(double pitch)
        {
            if (pitch <= 0.5) return 26;
            if (pitch <= 0.7) return 30;
            if (pitch <= 0.75) return 32;
            if (pitch <= 0.8) return 34;
            if (pitch <= 1.0) return 36;
            if (pitch <= 1.25) return 42;
            if (pitch <= 1.5) return 48;
            if (pitch <= 1.75) return 53;
            if (pitch <= 2.0) return 58;
            if (pitch <= 2.5) return 63;
            if (pitch <= 3.0) return 71;
            if (pitch <= 3.5) return 75;
            if (pitch <= 4.0) return 80;
            if (pitch <= 4.5) return 85;
            if (pitch <= 5.0) return 90;
            if (pitch <= 5.5) return 95;
            if (pitch <= 6.0) return 100;
            return 100;
        }

        /// <summary>
        /// e 级外螺纹上偏差 es (μm, 绝对值)
        /// </summary>
        private static double GeteDeviation(double pitch)
        {
            if (pitch <= 0.5) return 38;
            if (pitch <= 0.7) return 44;
            if (pitch <= 0.75) return 45;
            if (pitch <= 0.8) return 48;
            if (pitch <= 1.0) return 50;
            if (pitch <= 1.25) return 56;
            if (pitch <= 1.5) return 60;
            if (pitch <= 1.75) return 67;
            if (pitch <= 2.0) return 71;
            if (pitch <= 2.5) return 80;
            if (pitch <= 3.0) return 85;
            if (pitch <= 3.5) return 90;
            if (pitch <= 4.0) return 95;
            if (pitch <= 4.5) return 100;
            if (pitch <= 5.0) return 106;
            if (pitch <= 5.5) return 112;
            if (pitch <= 6.0) return 118;
            return 118;
        }

        /// <summary>
        /// 获取螺纹配合描述
        /// </summary>
        private static string GetFitDescription(string toleranceGrade)
        {
            switch (toleranceGrade)
            {
                case "4h":
                    return "精密配合: 用于精密机械,要求螺纹旋合性好,间隙极小。适用于精密仪器、航天零件。";
                case "4H":
                    return "精密配合 (内螺纹): 用于精密机械的内螺纹,配合间隙极小。";
                case "5g6g":
                    return "中等配合: 外螺纹,中径5级、大径6级公差带。用于一般机械,有镀层要求时推荐。";
                case "5H":
                    return "中等配合 (内螺纹): 用于一般精度要求的内螺纹。";
                case "5H6H":
                    return "中等配合 (内螺纹): 中径5级、小径6级公差带。用于一般精度内螺纹。";
                case "6g":
                    return "一般配合: 外螺纹最常用公差带。用于一般机械连接,是最常用的外螺纹公差。";
                case "6e":
                    return "一般配合 (大间隙): 外螺纹,有较大间隙,用于需要镀层或涂层的场合。";
                case "6f":
                    return "一般配合 (中间间隙): 外螺纹,间隙介于6g和6e之间,用于薄镀层场合。";
                case "6h":
                    return "一般配合 (零间隙): 外螺纹,上偏差为零。用于不需要间隙的场合。";
                case "6H":
                    return "一般配合 (内螺纹): 内螺纹最常用公差带。用于一般机械连接,是最常用的内螺纹公差。";
                case "6G":
                    return "一般配合 (内螺纹,大间隙): 内螺纹有较大间隙,用于需要镀层的场合。";
                case "7g6g":
                    return "粗糙配合: 外螺纹,大径7级、中径6级公差带。用于精度要求不高的场合。";
                case "7H":
                    return "粗糙配合 (内螺纹): 用于精度要求不高的内螺纹。";
                case "8g":
                    return "粗糙配合: 外螺纹,大间隙。用于精度要求低、需要大间隙的场合,如热镀锌螺纹。";
                default:
                    return "标准配合";
            }
        }

        /// <summary>
        /// 计算螺纹配合间隙
        /// 用于判断 6H/6g 等配合的实际间隙
        /// </summary>
        /// <param name="internalSpec">内螺纹公差带</param>
        /// <param name="externalSpec">外螺纹公差带</param>
        /// <param name="info">螺纹基本尺寸</param>
        /// <returns>配合间隙信息</returns>
        public static string CalculateFitClearance(string internalSpec, string externalSpec, ThreadInfo info)
        {
            var internalResult = CalculateTolerance(info.Spec, internalSpec, info.IsCoarse);
            var externalResult = CalculateTolerance(info.Spec, externalSpec, info.IsCoarse);

            if (internalResult == null || externalResult == null)
                return "无法计算配合间隙";

            // 中径配合间隙
            double minClearance = externalResult.PitchDiaMin - internalResult.PitchDiaMax;
            double maxClearance = externalResult.PitchDiaMax - internalResult.PitchDiaMin;

            return $"中径配合间隙: 最小 {minClearance:F3} mm, 最大 {maxClearance:F3} mm";
        }

        /// <summary>
        /// 常用螺纹配合推荐 (GB/T 197-2003)
        /// </summary>
        public static string GetRecommendedFit(string application)
        {
            switch (application)
            {
                case "精密":
                    return "推荐配合: 4H/4h 或 5H/5h\n适用: 精密仪器、航天零件、高精度连接";
                case "一般":
                    return "推荐配合: 6H/6g\n适用: 一般机械连接、最常用配合";
                case "粗糙":
                    return "推荐配合: 7H/8g\n适用: 精度要求低、大间隙场合";
                case "镀层":
                    return "推荐配合: 6H/6e 或 6G/6e\n适用: 需要电镀、热镀锌等表面处理";
                case "高温":
                    return "推荐配合: 6H/6g 或 6G/6g\n适用: 高温环境,考虑热膨胀";
                default:
                    return "推荐配合: 6H/6g (通用)";
            }
        }
    }
}
