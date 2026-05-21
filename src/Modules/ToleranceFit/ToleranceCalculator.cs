using System;

namespace ToleranceFit
{
    public class ToleranceResult
    {
        public double BasicSize { get; set; }
        public string HoleSpec { get; set; }
        public string ShaftSpec { get; set; }
        public double IT { get; set; }
        public double ES_Hole { get; set; }
        public double EI_Hole { get; set; }
        public double ES_Shaft { get; set; }
        public double EI_Shaft { get; set; }
        public double HoleMax { get; set; }
        public double HoleMin { get; set; }
        public double ShaftMax { get; set; }
        public double ShaftMin { get; set; }
        public string FitType { get; set; }
        public double MaxClearance { get; set; }
        public double MinClearance { get; set; }
        public double MaxInterference { get; set; }
        public double MinInterference { get; set; }
    }

    public static class ToleranceCalculator
    {
        /// <summary>
        /// 在尺寸段表中查找给定基本尺寸对应的行索引
        /// </summary>
        public static int FindSizeRow(double basicSize, double[] sizeRanges)
        {
            for (int i = 0; i < sizeRanges.Length; i++)
            {
                if (basicSize <= sizeRanges[i])
                    return i;
            }
            return sizeRanges.Length - 1;
        }

        /// <summary>
        /// 获取标准公差值（毫米）
        /// </summary>
        public static double GetIT(double basicSize, int grade)
        {
            int row = FindSizeRow(basicSize, ToleranceData.ITSizeRanges);
            if (grade < 1 || grade > 18) return 0;
            return ToleranceData.ITValues[row][grade - 1] / 1000.0;
        }

        /// <summary>
        /// 获取孔的偏差值（毫米）
        /// 返回 (ES, EI)
        /// </summary>
        public static (double ES, double EI) GetHoleDeviation(double basicSize, string letter, int grade)
        {
            double IT = GetIT(basicSize, grade);
            int row = FindSizeRow(basicSize, ToleranceData.ITSizeRanges);

            // Js 特殊处理
            if (letter == "Js" || letter == "JS")
            {
                double ITum = IT * 1000;
                if (grade >= 7 && grade <= 11 && ((int)Math.Round(ITum) % 2 == 1))
                {
                    double ES = (ITum - 1) / 2000.0;
                    double EI = -(ITum - 1) / 2000.0;
                    return (ES, EI);
                }
                else
                {
                    return (IT / 2.0, -IT / 2.0);
                }
            }

            // J 特殊处理
            if (letter == "J")
            {
                string col;
                if (grade <= 6) col = "J6";
                else if (grade == 7) col = "J7";
                else col = "J8";
                double ES = ToleranceData.HoleDeviations[col][row] / 1000.0;
                return (ES, ES - IT);
            }

            // A~H: EI查表, ES = EI + IT
            if (string.Compare(letter, "A", StringComparison.Ordinal) >= 0 &&
                string.Compare(letter, "H", StringComparison.Ordinal) <= 0)
            {
                double EI = ToleranceData.HoleDeviations[letter][row] / 1000.0;
                return (EI + IT, EI);
            }

            // ZB, ZC: 直接查表
            if (letter == "ZB" || letter == "ZC")
            {
                double ES = ToleranceData.HoleDeviations[letter][row] / 1000.0;
                return (ES, ES - IT);
            }

            // ZA 特殊处理
            if (letter == "ZA")
            {
                string col;
                if (grade >= 8 && grade <= 11) col = "ZA";
                else if (grade == 6) col = "ZA6";
                else if (grade == 7) col = "ZA7";
                else col = "ZA";
                double ES = ToleranceData.HoleDeviations[col][row] / 1000.0;
                return (ES, ES - IT);
            }

            // K~N: 等级映射
            if (string.Compare(letter, "K", StringComparison.Ordinal) >= 0 &&
                string.Compare(letter, "N", StringComparison.Ordinal) <= 0)
            {
                int mappedGrade;
                if (grade <= 3) mappedGrade = 3;
                else if (grade <= 8) mappedGrade = grade;
                else mappedGrade = 9;

                // N特殊: 基本尺寸>3mm且等级>8时 ES=0, EI=-IT
                if (letter == "N" && basicSize > 3 && grade > 8)
                {
                    return (0, -IT);
                }

                string col = letter + mappedGrade;
                if (ToleranceData.HoleDeviations.ContainsKey(col))
                {
                    double ES = ToleranceData.HoleDeviations[col][row] / 1000.0;
                    return (ES, ES - IT);
                }
            }

            // P~Z: 等级映射
            if (string.Compare(letter, "P", StringComparison.Ordinal) >= 0 &&
                string.Compare(letter, "Z", StringComparison.Ordinal) <= 0)
            {
                int mappedGrade;
                if (grade <= 3) mappedGrade = 3;
                else if (grade <= 7) mappedGrade = grade;
                else mappedGrade = 8;

                string col = letter + mappedGrade;
                if (ToleranceData.HoleDeviations.ContainsKey(col))
                {
                    double ES = ToleranceData.HoleDeviations[col][row] / 1000.0;
                    return (ES, ES - IT);
                }
            }

            // 默认尝试直接查表
            if (ToleranceData.HoleDeviations.ContainsKey(letter))
            {
                double val = ToleranceData.HoleDeviations[letter][row] / 1000.0;
                return (val + IT, val);
            }

            return (0, 0);
        }

        /// <summary>
        /// 获取轴的偏差值（毫米）
        /// 返回 (es, ei)
        /// </summary>
        public static (double es, double ei) GetShaftDeviation(double basicSize, string letter, int grade)
        {
            double IT = GetIT(basicSize, grade);
            int row = FindSizeRow(basicSize, ToleranceData.ShaftSizeRanges);

            // js 特殊处理
            if (letter == "js")
            {
                return (IT / 2.0, -IT / 2.0);
            }

            // a~h: es查表, ei = es - IT
            if (string.Compare(letter, "a", StringComparison.Ordinal) >= 0 &&
                string.Compare(letter, "h", StringComparison.Ordinal) <= 0)
            {
                if (ToleranceData.ShaftDeviations.ContainsKey(letter))
                {
                    double es = ToleranceData.ShaftDeviations[letter][row] / 1000.0;
                    return (es, es - IT);
                }
            }

            // j: 按等级查
            if (letter == "j")
            {
                string col;
                if (grade <= 5) col = "j5";
                else if (grade == 6) col = "j6";
                else if (grade == 7) col = "j7";
                else col = "j8";

                if (ToleranceData.ShaftDeviations.ContainsKey(col))
                {
                    double ei = ToleranceData.ShaftDeviations[col][row] / 1000.0;
                    return (ei + IT, ei);
                }
            }

            // k: 按等级查
            if (letter == "k")
            {
                string col;
                if (grade >= 4 && grade <= 7) col = "k47";
                else col = "k38";

                if (ToleranceData.ShaftDeviations.ContainsKey(col))
                {
                    double ei = ToleranceData.ShaftDeviations[col][row] / 1000.0;
                    return (ei + IT, ei);
                }
            }

            // m~zc: 直接查表
            if (ToleranceData.ShaftDeviations.ContainsKey(letter))
            {
                double ei = ToleranceData.ShaftDeviations[letter][row] / 1000.0;
                return (ei + IT, ei);
            }

            return (0, 0);
        }

        /// <summary>
        /// 计算配合结果
        /// </summary>
        public static ToleranceResult Calculate(
            double basicSize, string holeLetter, int holeGrade,
            string shaftLetter, int shaftGrade)
        {
            var result = new ToleranceResult
            {
                BasicSize = basicSize,
                HoleSpec = holeLetter + holeGrade,
                ShaftSpec = shaftLetter + shaftGrade,
                IT = GetIT(basicSize, holeGrade)
            };

            var (ES, EI) = GetHoleDeviation(basicSize, holeLetter, holeGrade);
            result.ES_Hole = ES;
            result.EI_Hole = EI;
            result.HoleMax = basicSize + ES;
            result.HoleMin = basicSize + EI;

            var (es, ei) = GetShaftDeviation(basicSize, shaftLetter, shaftGrade);
            result.ES_Shaft = es;
            result.EI_Shaft = ei;
            result.ShaftMax = basicSize + es;
            result.ShaftMin = basicSize + ei;

            // 配合类型判定
            double minClearance = EI - es;  // 最小间隙（或最大过盈）
            double maxClearance = ES - ei;  // 最大间隙（或最小过盈）

            // 特殊覆盖规则
            bool isSpecialTransition = false;
            if (basicSize <= 3.0 && holeLetter == "H" && holeGrade == 6 &&
                shaftLetter == "n" && shaftGrade == 5)
                isSpecialTransition = true;
            if (basicSize <= 3.0 && holeLetter == "H" && holeGrade == 7 &&
                shaftLetter == "p" && shaftGrade == 6)
                isSpecialTransition = true;
            if (basicSize <= 100.0 && holeLetter == "H" && holeGrade == 8 &&
                shaftLetter == "r" && shaftGrade == 7)
                isSpecialTransition = true;

            if (minClearance >= 0)
            {
                result.FitType = "间隙配合";
                result.MaxClearance = maxClearance;
                result.MinClearance = minClearance;
            }
            else if (maxClearance < 0)
            {
                if (isSpecialTransition)
                {
                    result.FitType = "过渡配合";
                    result.MaxClearance = maxClearance;
                    result.MinClearance = minClearance;
                    result.MaxInterference = -minClearance;
                    result.MinInterference = -maxClearance;
                }
                else
                {
                    result.FitType = "过盈配合";
                    result.MaxInterference = -minClearance;
                    result.MinInterference = -maxClearance;
                }
            }
            else
            {
                if (isSpecialTransition)
                    result.FitType = "过渡配合";
                else
                    result.FitType = "过渡配合";
                result.MaxClearance = maxClearance;
                result.MinClearance = minClearance;
                result.MaxInterference = -minClearance;
                result.MinInterference = -maxClearance;
            }

            return result;
        }
    }
}
