using System;

namespace InquireTolerance
{
    /// <summary>
    /// ISO 286 公差计算器
    /// 实现标准公差等级 IT01-IT18 和基本偏差计算
    /// </summary>
    public static class ToleranceCalculator
    {
        // 标准尺寸分段 (mm): 上限值
        private static readonly double[] SizeRanges = {
            3, 6, 10, 18, 30, 50, 80, 120, 180, 250, 315, 400, 500
        };

        // 各尺寸分段的几何平均值 D (mm)
        private static readonly double[] GeometricMeans = {
            Math.Sqrt(0.5 * 3),     // 0.5~3:    D = 1.2247
            Math.Sqrt(3 * 6),       // 3~6:      D = 4.2426
            Math.Sqrt(6 * 10),      // 6~10:     D = 7.7460
            Math.Sqrt(10 * 18),     // 10~18:    D = 13.4164
            Math.Sqrt(18 * 30),     // 18~30:    D = 23.2379
            Math.Sqrt(30 * 50),     // 30~50:    D = 38.7298
            Math.Sqrt(50 * 80),     // 50~80:    D = 63.2456
            Math.Sqrt(80 * 120),    // 80~120:   D = 97.9796
            Math.Sqrt(120 * 180),   // 120~180:  D = 146.9694
            Math.Sqrt(180 * 250),   // 180~250:  D = 212.1320
            Math.Sqrt(250 * 315),   // 250~315:  D = 280.6243
            Math.Sqrt(315 * 400),   // 315~400:  D = 354.9648
            Math.Sqrt(400 * 500)    // 400~500:  D = 447.2136
        };

        // 标准公差系数 (IT等级 -> 系数, 以 i 为单位)
        // IT1-IT4 使用插值公式
        // IT5=7i, IT6=10i, IT7=16i, IT8=25i, IT9=40i, IT10=64i
        // IT11=100i, IT12=160i, IT13=250i, IT14=400i, IT15=640i, IT16=1000i, IT17=1600i, IT18=2500i
        private static readonly double[] ITFactors = {
            0,     // IT0 (placeholder)
            0,     // IT01 (placeholder)
            0.8,   // IT1 - special formula
            1.0,   // IT2
            1.6,   // IT3 (interpolated)
            2.5,   // IT4 (interpolated)
            7,     // IT5
            10,    // IT6
            16,    // IT7
            25,    // IT8
            40,    // IT9
            64,    // IT10
            100,   // IT11
            160,   // IT12
            250,   // IT13
            400,   // IT14
            640,   // IT15
            1000,  // IT16
            1600,  // IT17
            2500   // IT18
        };

        /// <summary>
        /// 获取尺寸分段索引 (0-based)
        /// </summary>
        public static int GetSizeRangeIndex(double nominalSize)
        {
            if (nominalSize <= 0) return -1;
            if (nominalSize <= 0.5) return 0; // 特殊小尺寸

            for (int i = 0; i < SizeRanges.Length; i++)
            {
                if (nominalSize <= SizeRanges[i])
                    return i;
            }
            return SizeRanges.Length - 1; // 400-500
        }

        /// <summary>
        /// 计算标准公差单位 i (微米)
        /// i = 0.45 * cbrt(D) + 0.001 * D
        /// D 为尺寸分段的几何平均值 (mm)
        /// </summary>
        public static double CalculateToleranceUnit(double nominalSize)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;

            double D = GeometricMeans[idx];
            return 0.45 * Math.Pow(D, 1.0 / 3.0) + 0.001 * D;
        }

        /// <summary>
        /// 计算标准公差值 (微米)
        /// </summary>
        /// <param name="nominalSize">名义尺寸 (mm)</param>
        /// <param name="itGrade">公差等级 (1-18)</param>
        /// <returns>公差值 (微米)</returns>
        public static double CalculateStandardTolerance(double nominalSize, int itGrade)
        {
            if (itGrade < 1 || itGrade > 18) return 0;

            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;

            double D = GeometricMeans[idx];

            // IT01, IT0 特殊处理
            if (itGrade == 0) // IT01
            {
                return 0.3 + 0.008 * D;
            }

            // IT1 = 0.8 + 0.020*D
            if (itGrade == 1)
            {
                return 0.8 + 0.020 * D;
            }

            // IT2 = IT1 * q (q ≈ 1.6)
            if (itGrade == 2)
            {
                double it1 = 0.8 + 0.020 * D;
                return it1 * 1.6;
            }

            // IT3 = IT2 * q ≈ IT1 * q^2
            if (itGrade == 3)
            {
                double it1 = 0.8 + 0.020 * D;
                return it1 * 1.6 * 1.6;
            }

            // IT4 = IT3 * q ≈ IT1 * q^3
            if (itGrade == 4)
            {
                double it1 = 0.8 + 0.020 * D;
                return it1 * 1.6 * 1.6 * 1.6;
            }

            // IT5-IT18: 使用标准公差单位 i
            double i = 0.45 * Math.Pow(D, 1.0 / 3.0) + 0.001 * D;

            // 公差系数表 (IT等级 -> 系数)
            double factor;
            switch (itGrade)
            {
                case 5:  factor = 7; break;
                case 6:  factor = 10; break;
                case 7:  factor = 16; break;
                case 8:  factor = 25; break;
                case 9:  factor = 40; break;
                case 10: factor = 64; break;
                case 11: factor = 100; break;
                case 12: factor = 160; break;
                case 13: factor = 250; break;
                case 14: factor = 400; break;
                case 15: factor = 640; break;
                case 16: factor = 1000; break;
                case 17: factor = 1600; break;
                case 18: factor = 2500; break;
                default: factor = 0; break;
            }

            return factor * i;
        }

        /// <summary>
        /// 计算基本偏差值 (微米)
        /// </summary>
        /// <param name="nominalSize">名义尺寸 (mm)</param>
        /// <param name="deviationCode">偏差代号 (如 "H", "h", "g", "f" 等)</param>
        /// <param name="itGrade">公差等级</param>
        /// <returns>基本偏差值 (微米)。对于孔: EI; 对于轴: es</returns>
        public static double CalculateFundamentalDeviation(double nominalSize, string deviationCode, int itGrade)
        {
            if (string.IsNullOrEmpty(deviationCode)) return 0;

            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;

            double D = GeometricMeans[idx];
            bool isHole = char.IsUpper(deviationCode[0]);
            string code = deviationCode.ToUpper();

            // ===== 孔的基本偏差 =====
            if (isHole)
            {
                switch (code)
                {
                    case "A":
                        return GetADeviation(nominalSize, true);
                    case "B":
                        return GetBDeviation(nominalSize, true);
                    case "C":
                        return GetCDeviation(nominalSize, true);
                    case "CD":
                        return GetCDDeviation(nominalSize, true);
                    case "D":
                        return GetDDeviation(nominalSize, true);
                    case "E":
                        return GetEDeviation(nominalSize, true);
                    case "EF":
                        return GetEFDeviation(nominalSize, true);
                    case "F":
                        return GetFDeviation(nominalSize, true);
                    case "FG":
                        return GetFGDeviation(nominalSize, true);
                    case "G":
                        return GetGDeviation(nominalSize, true);
                    case "H":
                        return 0; // EI = 0
                    case "JS":
                        return -CalculateStandardTolerance(nominalSize, itGrade) / 2;
                    case "J":
                        return GetJHoleDeviation(nominalSize, itGrade);
                    case "K":
                        return GetKHoleDeviation(nominalSize, itGrade);
                    case "M":
                        return GetMHoleDeviation(nominalSize, itGrade);
                    case "N":
                        return GetNHoleDeviation(nominalSize, itGrade);
                    case "P":
                        return GetPHoleDeviation(nominalSize);
                    case "R":
                        return GetRHoleDeviation(nominalSize);
                    case "S":
                        return GetSHoleDeviation(nominalSize);
                    case "T":
                        return GetTHoleDeviation(nominalSize);
                    case "U":
                        return GetUHoleDeviation(nominalSize);
                    default:
                        return 0;
                }
            }
            else // 轴的基本偏差
            {
                switch (code)
                {
                    case "A":
                        return GetADeviation(nominalSize, false);
                    case "B":
                        return GetBDeviation(nominalSize, false);
                    case "C":
                        return GetCDeviation(nominalSize, false);
                    case "CD":
                        return GetCDDeviation(nominalSize, false);
                    case "D":
                        return GetDDeviation(nominalSize, false);
                    case "E":
                        return GetEDeviation(nominalSize, false);
                    case "EF":
                        return GetEFDeviation(nominalSize, false);
                    case "F":
                        return GetFDeviation(nominalSize, false);
                    case "FG":
                        return GetFGDeviation(nominalSize, false);
                    case "G":
                        return GetGDeviation(nominalSize, false);
                    case "H":
                        return 0; // es = 0
                    case "JS":
                        return -CalculateStandardTolerance(nominalSize, itGrade) / 2;
                    case "J":
                        return GetJShaftDeviation(nominalSize, itGrade);
                    case "K":
                        return GetKShaftDeviation(nominalSize, itGrade);
                    case "M":
                        return GetMShaftDeviation(nominalSize, itGrade);
                    case "N":
                        return GetNShaftDeviation(nominalSize, itGrade);
                    case "P":
                        return GetPShaftDeviation(nominalSize);
                    case "R":
                        return GetRShaftDeviation(nominalSize);
                    case "S":
                        return GetSShaftDeviation(nominalSize);
                    case "T":
                        return GetTShaftDeviation(nominalSize);
                    case "U":
                        return GetUShaftDeviation(nominalSize);
                    default:
                        return 0;
                }
            }
        }

        /// <summary>
        /// 计算轴的上偏差 es (微米)
        /// </summary>
        public static double CalculateShaftUpperDeviation(double nominalSize, string deviationCode, int itGrade)
        {
            if (string.IsNullOrEmpty(deviationCode)) return 0;

            string code = deviationCode.ToUpper();
            double fundamental = CalculateFundamentalDeviation(nominalSize, deviationCode, itGrade);

            // 对于 a-h 轴偏差: 基本偏差是 es (上偏差)
            if (code[0] == 'A' || code[0] == 'B' || code[0] == 'C' || code[0] == 'D' ||
                code[0] == 'E' || code[0] == 'F' || code[0] == 'G' || code[0] == 'H')
            {
                // a-h: es 是基本偏差 (负值或零)
                return fundamental;
            }

            // 对于 js 轴: es = +IT/2
            if (code == "JS")
            {
                return CalculateStandardTolerance(nominalSize, itGrade) / 2;
            }

            // 对于 j-zc 轴: 基本偏差是 ei (下偏差), es = ei + IT
            double it = CalculateStandardTolerance(nominalSize, itGrade);
            return fundamental + it;
        }

        /// <summary>
        /// 计算轴的下偏差 ei (微米)
        /// </summary>
        public static double CalculateShaftLowerDeviation(double nominalSize, string deviationCode, int itGrade)
        {
            if (string.IsNullOrEmpty(deviationCode)) return 0;

            string code = deviationCode.ToUpper();
            double fundamental = CalculateFundamentalDeviation(nominalSize, deviationCode, itGrade);
            double it = CalculateStandardTolerance(nominalSize, itGrade);

            // 对于 a-h 轴偏差: ei = es - IT
            if (code[0] == 'A' || code[0] == 'B' || code[0] == 'C' || code[0] == 'D' ||
                code[0] == 'E' || code[0] == 'F' || code[0] == 'G' || code[0] == 'H')
            {
                return fundamental - it;
            }

            // 对于 js 轴: ei = -IT/2
            if (code == "JS")
            {
                return -CalculateStandardTolerance(nominalSize, itGrade) / 2;
            }

            // 对于 j-zc 轴: ei 是基本偏差
            return fundamental;
        }

        /// <summary>
        /// 计算孔的上偏差 ES (微米)
        /// </summary>
        public static double CalculateHoleUpperDeviation(double nominalSize, string deviationCode, int itGrade)
        {
            if (string.IsNullOrEmpty(deviationCode)) return 0;

            string code = deviationCode.ToUpper();
            double fundamental = CalculateFundamentalDeviation(nominalSize, deviationCode, itGrade);
            double it = CalculateStandardTolerance(nominalSize, itGrade);

            // 对于 A-H 孔偏差: ES = EI + IT, 基本偏差是 EI
            if (code[0] == 'A' || code[0] == 'B' || code[0] == 'C' || code[0] == 'D' ||
                code[0] == 'E' || code[0] == 'F' || code[0] == 'G' || code[0] == 'H')
            {
                return fundamental + it;
            }

            // 对于 JS 孔: ES = +IT/2
            if (code == "JS")
            {
                return it / 2;
            }

            // 对于 J-ZC 孔: 基本偏差是 ES
            return fundamental;
        }

        /// <summary>
        /// 计算孔的下偏差 EI (微米)
        /// </summary>
        public static double CalculateHoleLowerDeviation(double nominalSize, string deviationCode, int itGrade)
        {
            if (string.IsNullOrEmpty(deviationCode)) return 0;

            string code = deviationCode.ToUpper();
            double fundamental = CalculateFundamentalDeviation(nominalSize, deviationCode, itGrade);

            // 对于 A-H 孔偏差: EI 是基本偏差
            if (code[0] == 'A' || code[0] == 'B' || code[0] == 'C' || code[0] == 'D' ||
                code[0] == 'E' || code[0] == 'F' || code[0] == 'G' || code[0] == 'H')
            {
                return fundamental;
            }

            // 对于 JS 孔: EI = -IT/2
            if (code == "JS")
            {
                return -CalculateStandardTolerance(nominalSize, itGrade) / 2;
            }

            // 对于 J-ZC 孔: EI = ES - IT
            double it = CalculateStandardTolerance(nominalSize, itGrade);
            return fundamental - it;
        }

        #region 轴的基本偏差计算 (es 值, 微米)

        // a 偏差: es = -(265 + 1.3*D) for D<=120, -(3.5*D) for D>120
        private static double GetADeviation(double nominalSize, bool isHole)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            double D = GeometricMeans[idx];
            double val;
            if (D <= 120)
                val = -(265 + 1.3 * D);
            else
                val = -(3.5 * D);
            return isHole ? -val : val; // 孔偏差符号相反
        }

        // b 偏差: es = -(140 + 0.85*D) for D<=160, -(1.8*D) for D>160
        private static double GetBDeviation(double nominalSize, bool isHole)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            double D = GeometricMeans[idx];
            double val;
            if (D <= 160)
                val = -(140 + 0.85 * D);
            else
                val = -(1.8 * D);
            return isHole ? -val : val;
        }

        // c 偏差: es = -(52 + D^0.2) for D<=40, -95*D^0.2 for D>40
        // 简化公式: c ≈ -52*D^0.2 (D<=40), -95*D^0.2 (D>40)
        private static readonly double[] CShaftEs = {
            -60, -70, -80, -95, -110, -120, -130, -140, -150, -170, -180, -190, -200
        };
        private static double GetCDeviation(double nominalSize, bool isHole)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            double val = CShaftEs[idx];
            return isHole ? -val : val;
        }

        // cd 偏差
        private static readonly double[] CDShaftEs = {
            -34, -46, -56, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
        private static double GetCDDeviation(double nominalSize, bool isHole)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            double val = CDShaftEs[idx];
            return isHole ? -val : val;
        }

        // d 偏差: es ≈ -16*D^0.44
        private static readonly double[] DShaftEs = {
            -20, -30, -40, -50, -65, -80, -100, -120, -145, -170, -190, -210, -230
        };
        private static double GetDDeviation(double nominalSize, bool isHole)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            double val = DShaftEs[idx];
            return isHole ? -val : val;
        }

        // e 偏差: es ≈ -11*D^0.41
        private static readonly double[] EShaftEs = {
            -14, -20, -25, -32, -40, -50, -60, -72, -85, -100, -110, -125, -135
        };
        private static double GetEDeviation(double nominalSize, bool isHole)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            double val = EShaftEs[idx];
            return isHole ? -val : val;
        }

        // ef 偏差
        private static readonly double[] EFShaftEs = {
            -10, -14, -18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
        private static double GetEFDeviation(double nominalSize, bool isHole)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            double val = EFShaftEs[idx];
            return isHole ? -val : val;
        }

        // f 偏差: es ≈ -5.5*D^0.41
        private static readonly double[] FShaftEs = {
            -6, -10, -13, -16, -20, -25, -30, -36, -43, -50, -56, -62, -68
        };
        private static double GetFDeviation(double nominalSize, bool isHole)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            double val = FShaftEs[idx];
            return isHole ? -val : val;
        }

        // fg 偏差
        private static readonly double[] FGShaftEs = {
            -4, -6, -8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
        private static double GetFGDeviation(double nominalSize, bool isHole)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            double val = FGShaftEs[idx];
            return isHole ? -val : val;
        }

        // g 偏差: es ≈ -2.5*D^0.34
        private static readonly double[] GShaftEs = {
            -2, -4, -5, -6, -7, -9, -10, -12, -14, -15, -17, -18, -20
        };
        private static double GetGDeviation(double nominalSize, bool isHole)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            double val = GShaftEs[idx];
            return isHole ? -val : val;
        }

        // j 偏差 (特殊, 依赖等级)
        private static double GetJShaftDeviation(double nominalSize, int itGrade)
        {
            // j 仅用于 IT5-IT8
            double it = CalculateStandardTolerance(nominalSize, itGrade);
            switch (itGrade)
            {
                case 5:
                case 6:
                    return -it / 2; // 无偏差, 对称
                case 7:
                    return -it / 2;
                case 8:
                    return -it / 2;
                default:
                    return 0;
            }
        }

        private static double GetJHoleDeviation(double nominalSize, int itGrade)
        {
            return -GetJShaftDeviation(nominalSize, itGrade);
        }

        // k 偏差 (轴)
        private static readonly double[] KShaftIT567 = {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
        private static double GetKShaftDeviation(double nominalSize, int itGrade)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;

            // k 轴: IT1-IT4: ei=0; IT5-IT7: ei=+1~+Δ (取决于尺寸); IT8+: ei=0
            if (itGrade <= 4 || itGrade >= 8) return 0;

            // IT5-IT7: Δ 值 (微米)
            double[] delta = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            // 简化: k 对于 IT5-IT7, ei = +Δ
            double it = CalculateStandardTolerance(nominalSize, itGrade);
            if (itGrade == 5)
                return Math.Max(0, Math.Ceiling(it * 0.1));
            if (itGrade == 6)
                return Math.Max(0, Math.Ceiling(it * 0.08));
            if (itGrade == 7)
                return Math.Max(0, Math.Ceiling(it * 0.05));
            return 0;
        }

        private static double GetKHoleDeviation(double nominalSize, int itGrade)
        {
            // K 孔: ES = -ei_shaft + Δ (对于IT<=8)
            double shaftEi = GetKShaftDeviation(nominalSize, itGrade);
            return -shaftEi;
        }

        // m 偏差 (轴)
        private static readonly double[] MShaftEs = {
            2, 4, 6, 7, 8, 10, 12, 14, 16, 17, 18, 20, 21
        };
        private static double GetMShaftDeviation(double nominalSize, int itGrade)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            // m 轴: ei 基本偏差
            return MShaftEs[idx];
        }

        private static double GetMHoleDeviation(double nominalSize, int itGrade)
        {
            double shaftEi = GetMShaftDeviation(nominalSize, itGrade);
            double it = CalculateStandardTolerance(nominalSize, itGrade);
            // M 孔: ES = -ei_shaft + Δ (IT<=8)
            if (itGrade <= 8)
            {
                double delta = Math.Max(0, Math.Ceiling(it * 0.05));
                return -shaftEi + delta;
            }
            return -shaftEi;
        }

        // n 偏差 (轴)
        private static readonly double[] NShaftEs = {
            4, 8, 10, 12, 15, 17, 20, 23, 27, 31, 34, 37, 40
        };
        private static double GetNShaftDeviation(double nominalSize, int itGrade)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            return NShaftEs[idx];
        }

        private static double GetNHoleDeviation(double nominalSize, int itGrade)
        {
            double shaftEi = GetNShaftDeviation(nominalSize, itGrade);
            double it = CalculateStandardTolerance(nominalSize, itGrade);
            if (itGrade <= 8)
            {
                double delta = Math.Max(0, Math.Ceiling(it * 0.05));
                return -shaftEi + delta;
            }
            return -shaftEi;
        }

        // p 偏差 (轴)
        private static readonly double[] PShaftEs = {
            6, 12, 15, 18, 22, 26, 32, 37, 43, 50, 56, 62, 68
        };
        private static double GetPShaftDeviation(double nominalSize)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            return PShaftEs[idx];
        }

        private static double GetPHoleDeviation(double nominalSize)
        {
            return -GetPShaftDeviation(nominalSize);
        }

        // r 偏差 (轴)
        private static readonly double[] RShaftEs = {
            10, 15, 19, 23, 28, 34, 41, 43, 51, 54, 63, 65, 68
        };
        private static double GetRShaftDeviation(double nominalSize)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            return RShaftEs[idx];
        }

        private static double GetRHoleDeviation(double nominalSize)
        {
            return -GetRShaftDeviation(nominalSize);
        }

        // s 偏差 (轴)
        private static readonly double[] SShaftEs = {
            14, 19, 23, 28, 35, 43, 53, 59, 71, 79, 80, 92, 100
        };
        private static double GetSShaftDeviation(double nominalSize)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            return SShaftEs[idx];
        }

        private static double GetSHoleDeviation(double nominalSize)
        {
            return -GetSShaftDeviation(nominalSize);
        }

        // t 偏差 (轴)
        private static readonly double[] TShaftEs = {
            0, 0, 0, 0, 0, 48, 54, 66, 75, 85, 93, 103, 109
        };
        private static double GetTShaftDeviation(double nominalSize)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            return TShaftEs[idx];
        }

        private static double GetTHoleDeviation(double nominalSize)
        {
            return -GetTShaftDeviation(nominalSize);
        }

        // u 偏差 (轴)
        private static readonly double[] UShaftEs = {
            18, 23, 28, 33, 41, 48, 60, 70, 87, 100, 108, 114, 126
        };
        private static double GetUShaftDeviation(double nominalSize)
        {
            int idx = GetSizeRangeIndex(nominalSize);
            if (idx < 0) return 0;
            return UShaftEs[idx];
        }

        private static double GetUHoleDeviation(double nominalSize)
        {
            return -GetUShaftDeviation(nominalSize);
        }

        #endregion

        /// <summary>
        /// 格式化偏差值为带符号的字符串 (mm)
        /// </summary>
        public static string FormatDeviation(double deviationMicrons, int precision)
        {
            double mm = deviationMicrons / 1000.0;
            mm = Math.Round(mm, precision);

            if (mm > 0)
                return "+" + mm.ToString("F" + precision);
            else if (mm < 0)
                return mm.ToString("F" + precision);
            else
                return "0";
        }

        /// <summary>
        /// 获取公差等级名称
        /// </summary>
        public static string GetITGradeName(int itGrade)
        {
            if (itGrade == 0) return "IT01";
            return "IT" + itGrade;
        }
    }
}
