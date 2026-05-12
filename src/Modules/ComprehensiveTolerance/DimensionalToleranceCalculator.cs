using System;

namespace ComprehensiveTolerance
{
    /// <summary>
    /// 尺寸公差计算器
    /// 基于 GB/T 1800.1-2009 (极限与配合)
    /// IT 公差等级计算: IT01~IT18
    /// </summary>
    public static class DimensionalToleranceCalculator
    {
        /// <summary>
        /// 计算标准公差值 (IT等级)
        /// 公式来源: GB/T 1800.1-2009
        /// IT01~IT1 使用特殊公式
        /// IT2~IT4 使用几何插值
        /// IT5~IT18 使用 i = 0.45*D^(1/3) + 0.001*D 公式
        /// </summary>
        /// <param name="nominalSize">公称尺寸 (mm)</param>
        /// <param name="grade">公差等级索引 (0=IT01, 1=IT0, 2=IT1, ..., 7=IT7, ...)</param>
        /// <returns>公差值 (mm)</returns>
        public static double CalculateTolerance(double nominalSize, int grade)
        {
            // 标准公差等级: IT01, IT0, IT1, IT2, ..., IT18
            // grade 索引:     0,    1,   2,   3,  ..., 20

            if (grade < 0 || grade > 20) return 0;
            if (nominalSize <= 0) return 0;

            // 标准尺寸段的几何平均值 D (mm)
            double D = GetGeometricMean(nominalSize);

            if (grade == 0)
            {
                // IT01: 0.3 + 0.008*D (μm)
                return (0.3 + 0.008 * D) / 1000.0;
            }
            else if (grade == 1)
            {
                // IT0: 0.5 + 0.012*D (μm)
                return (0.5 + 0.012 * D) / 1000.0;
            }
            else if (grade == 2)
            {
                // IT1: 0.8 + 0.020*D (μm)
                return (0.8 + 0.020 * D) / 1000.0;
            }
            else if (grade >= 3 && grade <= 5)
            {
                // IT2~IT4: 在 IT1 和 IT5 之间几何插值
                double it1 = 0.8 + 0.020 * D;
                double it5 = GetIT5Value(D);
                double ratio = (grade - 2) / 3.0; // 1/3, 2/3, 3/3
                return it1 * Math.Pow(it5 / it1, ratio) / 1000.0;
            }
            else
            {
                // IT5~IT18: 使用标准公式
                // 公差单位 i = 0.45 * D^(1/3) + 0.001 * D (μm)
                double i = 0.45 * Math.Pow(D, 1.0 / 3.0) + 0.001 * D;

                // 标准公差因子
                double factor = GetStandardToleranceFactor(grade);
                return factor * i / 1000.0; // 转换为 mm
            }
        }

        /// <summary>
        /// 获取 IT5 的公差值 (μm)
        /// IT5 = 7i
        /// </summary>
        private static double GetIT5Value(double D)
        {
            double i = 0.45 * Math.Pow(D, 1.0 / 3.0) + 0.001 * D;
            return 7 * i;
        }

        /// <summary>
        /// 标准公差因子 (IT等级倍率)
        /// 来源: GB/T 1800.1-2009
        /// </summary>
        private static double GetStandardToleranceFactor(int grade)
        {
            switch (grade)
            {
                case 5: return 7;
                case 6: return 10;
                case 7: return 16;
                case 8: return 25;
                case 9: return 40;
                case 10: return 64;
                case 11: return 100;
                case 12: return 160;
                case 13: return 250;
                case 14: return 400;
                case 15: return 640;
                case 16: return 1000;
                case 17: return 1600;
                case 18: return 2500;
                default: return 7;
            }
        }

        /// <summary>
        /// 获取公称尺寸段的几何平均值 D (mm)
        /// GB/T 1800.1-2009 尺寸分段
        /// </summary>
        private static double GetGeometricMean(double nominalSize)
        {
            // GB/T 1800.1-2009 标准尺寸段 (mm)
            // >0~3, >3~6, >6~10, >10~18, >18~30, >30~50, >50~80,
            // >80~120, >120~180, >180~250, >250~315, >315~400, >400~500

            double[][] ranges = new double[][]
            {
                new double[] { 0, 3, 1.732 },       // sqrt(0*3) = 0, 用 sqrt(1*3)
                new double[] { 3, 6, 4.243 },        // sqrt(3*6)
                new double[] { 6, 10, 7.746 },       // sqrt(6*10)
                new double[] { 10, 18, 13.416 },     // sqrt(10*18)
                new double[] { 18, 30, 23.238 },     // sqrt(18*30)
                new double[] { 30, 50, 38.730 },     // sqrt(30*50)
                new double[] { 50, 80, 63.246 },     // sqrt(50*80)
                new double[] { 80, 120, 97.980 },    // sqrt(80*120)
                new double[] { 120, 180, 146.969 },  // sqrt(120*180)
                new double[] { 180, 250, 212.132 },  // sqrt(180*250)
                new double[] { 250, 315, 280.624 },  // sqrt(250*315)
                new double[] { 315, 400, 354.965 },  // sqrt(315*400)
                new double[] { 400, 500, 447.214 },  // sqrt(400*500)
            };

            for (int i = 0; i < ranges.Length; i++)
            {
                if (nominalSize > ranges[i][0] && nominalSize <= ranges[i][1])
                    return ranges[i][2];
            }

            // 超出500mm范围,直接计算
            return Math.Sqrt(nominalSize);
        }

        /// <summary>
        /// 计算基本偏差 (轴)
        /// GB/T 1800.1-2009 基本偏差表
        /// </summary>
        /// <param name="nominalSize">公称尺寸 (mm)</param>
        /// <param name="pitch">螺距 (mm)</param>
        /// <param name="deviationType">偏差类型: "a"~"zc" (轴), "A"~"ZC" (孔)</param>
        /// <returns>基本偏差值 (mm), 正值表示上偏差,负值表示下偏差</returns>
        public static double CalculateBasicDeviation(double nominalSize, string deviationType)
        {
            double D = GetGeometricMean(nominalSize);

            switch (deviationType.ToLower())
            {
                case "a":
                    // 轴 a: es = -(265 + 1.3*D) (μm), 用于所有等级
                    return -(265 + 1.3 * D) / 1000.0;
                case "b":
                    // 轴 b: es = -(140 + 0.85*D) (μm)
                    return -(140 + 0.85 * D) / 1000.0;
                case "c":
                    // 轴 c: es = -(52 + 0.8*D) (μm) for D>16
                    if (D > 16)
                        return -(52 + 0.8 * D) / 1000.0;
                    else
                        return -(95 + 0.8 * D) / 1000.0;
                case "cd":
                    // 轴 cd: es = -(c*d) 的几何平均
                    return -(34 + 0.5 * D) / 1000.0;
                case "d":
                    // 轴 d: es = -16*D^0.44 (μm)
                    return -16 * Math.Pow(D, 0.44) / 1000.0;
                case "e":
                    // 轴 e: es = -11*D^0.41 (μm)
                    return -11 * Math.Pow(D, 0.41) / 1000.0;
                case "ef":
                    // 轴 ef
                    return -8 * Math.Pow(D, 0.41) / 1000.0;
                case "f":
                    // 轴 f: es = -5.5*D^0.41 (μm)
                    return -5.5 * Math.Pow(D, 0.41) / 1000.0;
                case "fg":
                    // 轴 fg
                    return -3.5 * Math.Pow(D, 0.41) / 1000.0;
                case "g":
                    // 轴 g: es = -2.5*D^0.34 (μm)
                    return -2.5 * Math.Pow(D, 0.34) / 1000.0;
                case "h":
                    // 轴 h: es = 0
                    return 0;
                case "js":
                    // 轴 js: 偏差 = ±IT/2, 这里返回 0 (对称偏差)
                    return 0;
                case "j":
                    // 轴 j: 无基本偏差 (IT5~IT8 有特殊规定)
                    return 0;
                case "k":
                    // 轴 k: ei = 0 (IT4~IT7), ei > 0 (IT3~IT4)
                    return 0;
                case "m":
                    // 轴 m: ei = +(IT7-IT6) (近似)
                    return GetITApprox(D, 7) - GetITApprox(D, 6);
                case "n":
                    // 轴 n: ei = +IT7 (近似)
                    return GetITApprox(D, 7);
                case "p":
                    // 轴 p: ei = +IT7 + 0~5 (μm)
                    return (GetITApprox(D, 7) * 1000 + 2.5 * D) / 1000.0;
                case "r":
                    // 轴 r: ei = p + 0~3 (μm)
                    return (GetITApprox(D, 7) * 1000 + 4.0 * D) / 1000.0;
                case "s":
                    // 轴 s: ei = +IT7 + D^0.5*常数
                    return (GetITApprox(D, 7) * 1000 + Math.Sqrt(D) * 14) / 1000.0;
                case "t":
                    // 轴 t: ei = +IT7 + D^0.5*常数
                    return (GetITApprox(D, 7) * 1000 + Math.Sqrt(D) * 18) / 1000.0;
                case "u":
                    // 轴 u: ei = +IT7 + D^0.5*常数
                    return (GetITApprox(D, 7) * 1000 + Math.Sqrt(D) * 22) / 1000.0;
                case "v":
                    return (GetITApprox(D, 7) * 1000 + Math.Sqrt(D) * 26) / 1000.0;
                case "x":
                    return (GetITApprox(D, 7) * 1000 + Math.Sqrt(D) * 30) / 1000.0;
                case "y":
                    return (GetITApprox(D, 7) * 1000 + Math.Sqrt(D) * 35) / 1000.0;
                case "z":
                    return (GetITApprox(D, 7) * 1000 + Math.Sqrt(D) * 40) / 1000.0;
                case "za":
                    return (GetITApprox(D, 7) * 1000 + Math.Sqrt(D) * 50) / 1000.0;
                case "zb":
                    return (GetITApprox(D, 7) * 1000 + Math.Sqrt(D) * 65) / 1000.0;
                case "zc":
                    return (GetITApprox(D, 7) * 1000 + Math.Sqrt(D) * 90) / 1000.0;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// IT 等级近似值 (μm)
        /// </summary>
        private static double GetITApprox(double D, int itGrade)
        {
            double i = 0.45 * Math.Pow(D, 1.0 / 3.0) + 0.001 * D;
            return GetStandardToleranceFactor(itGrade) * i / 1000.0; // 返回 mm
        }

        /// <summary>
        /// 计算配合的极限间隙或过盈
        /// </summary>
        /// <param name="holeES">孔的上偏差</param>
        /// <param name="holeEI">孔的下偏差</param>
        /// <param name="shaftes">轴的上偏差</param>
        /// <param name="shaftei">轴的下偏差</param>
        /// <returns>最大间隙, 最小间隙(或最大过盈)</returns>
        public static (double maxClearance, double minClearance) CalculateFit(
            double holeES, double holeEI, double shaftes, double shaftei)
        {
            // 最大间隙 = 孔上偏差 - 轴下偏差
            double maxClearance = holeES - shaftei;
            // 最小间隙 = 孔下偏差 - 轴上偏差
            double minClearance = holeEI - shaftes;

            return (maxClearance, minClearance);
        }
    }
}
