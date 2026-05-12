using System;
using System.Collections.Generic;

namespace InquireTolerance
{
    /// <summary>
    /// 预计算公差表 - ISO 286 标准尺寸和公差等级
    /// </summary>
    public static class ToleranceTable
    {
        /// <summary>
        /// 常用名义尺寸列表 (mm)
        /// </summary>
        public static readonly double[] CommonSizes = {
            1, 1.5, 2, 2.5, 3,
            4, 5, 6,
            8, 10,
            12, 14, 15, 16, 18,
            20, 22, 24, 25, 26, 28, 30,
            32, 35, 38, 40, 42, 45, 48, 50,
            55, 60, 65, 70, 75, 80,
            85, 90, 95, 100, 105, 110, 120,
            130, 140, 150, 160, 170, 180,
            190, 200, 220, 240, 250,
            260, 280, 300, 315,
            320, 340, 360, 380, 400,
            420, 440, 450, 460, 480, 500
        };

        /// <summary>
        /// 常用公差等级
        /// </summary>
        public static readonly int[] CommonITGrades = { 6, 7, 8, 9, 10, 11, 12, 13 };

        /// <summary>
        /// 常用轴偏差代号
        /// </summary>
        public static readonly string[] CommonShaftDeviations = {
            "a", "b", "c", "d", "e", "f", "g", "h", "js", "j", "k", "m", "n", "p", "r", "s", "t", "u"
        };

        /// <summary>
        /// 常用孔偏差代号
        /// </summary>
        public static readonly string[] CommonHoleDeviations = {
            "A", "B", "C", "D", "E", "F", "G", "H", "JS", "J", "K", "M", "N", "P", "R", "S", "T", "U"
        };

        /// <summary>
        /// 常用配合定义: (孔代号, 孔等级, 轴代号, 轴等级, 配合类型说明)
        /// </summary>
        public static readonly FitDefinition[] CommonFits = {
            // 基孔制常用配合
            new FitDefinition("H", 7, "g", 6, "间隙配合 - 滑动配合"),
            new FitDefinition("H", 7, "h", 6, "间隙配合 - 推入配合"),
            new FitDefinition("H", 7, "k", 6, "过渡配合 - 轻压配合"),
            new FitDefinition("H", 7, "n", 6, "过渡配合 - 压入配合"),
            new FitDefinition("H", 7, "p", 6, "过盈配合 - 压配合"),
            new FitDefinition("H", 7, "s", 6, "过盈配合 - 重压配合"),
            new FitDefinition("H", 7, "u", 6, "过盈配合 - 热压配合"),

            new FitDefinition("H", 8, "f", 7, "间隙配合 - 松动配合"),
            new FitDefinition("H", 8, "h", 7, "间隙配合 - 推入配合"),
            new FitDefinition("H", 8, "k", 7, "过渡配合 - 轻压配合"),
            new FitDefinition("H", 8, "n", 7, "过渡配合 - 压入配合"),

            new FitDefinition("H", 9, "d", 9, "间隙配合 - 松动配合"),
            new FitDefinition("H", 9, "e", 9, "间隙配合 - 转动配合"),
            new FitDefinition("H", 9, "f", 9, "间隙配合 - 滑动配合"),
            new FitDefinition("H", 9, "h", 9, "间隙配合 - 推入配合"),

            new FitDefinition("H", 11, "h", 11, "间隙配合 - 粗糙配合"),
            new FitDefinition("H", 11, "c", 11, "间隙配合 - 松动配合"),
            new FitDefinition("H", 11, "d", 11, "间隙配合 - 转动配合"),

            new FitDefinition("H", 12, "h", 12, "间隙配合 - 粗糙配合"),

            // 基轴制常用配合
            new FitDefinition("G", 7, "h", 6, "间隙配合 - 滑动配合"),
            new FitDefinition("F", 7, "h", 6, "间隙配合 - 转动配合"),
            new FitDefinition("H", 7, "h", 6, "间隙配合 - 推入配合"),
            new FitDefinition("K", 7, "h", 6, "过渡配合 - 轻压配合"),
            new FitDefinition("N", 7, "h", 6, "过渡配合 - 压入配合"),
            new FitDefinition("P", 7, "h", 6, "过盈配合 - 压配合"),

            new FitDefinition("F", 8, "h", 7, "间隙配合 - 松动配合"),
            new FitDefinition("H", 8, "h", 7, "间隙配合 - 推入配合"),
        };

        /// <summary>
        /// 配合定义结构
        /// </summary>
        public struct FitDefinition
        {
            public string HoleCode;
            public int HoleGrade;
            public string ShaftCode;
            public int ShaftGrade;
            public string Description;

            public FitDefinition(string holeCode, int holeGrade, string shaftCode, int shaftGrade, string description)
            {
                HoleCode = holeCode;
                HoleGrade = holeGrade;
                ShaftCode = shaftCode;
                ShaftGrade = shaftGrade;
                Description = description;
            }

            public override string ToString()
            {
                return $"{HoleCode}{HoleGrade}/{ShaftCode}{ShaftGrade} - {Description}";
            }
        }

        /// <summary>
        /// 获取常用配合列表 (基孔制)
        /// </summary>
        public static List<FitDefinition> GetHoleBasedFits()
        {
            var fits = new List<FitDefinition>();
            foreach (var fit in CommonFits)
            {
                if (fit.HoleCode == "H")
                    fits.Add(fit);
            }
            return fits;
        }

        /// <summary>
        /// 获取常用配合列表 (基轴制)
        /// </summary>
        public static List<FitDefinition> GetShaftBasedFits()
        {
            var fits = new List<FitDefinition>();
            foreach (var fit in CommonFits)
            {
                if (fit.ShaftCode == "h" && fit.HoleCode != "H")
                    fits.Add(fit);
            }
            return fits;
        }

        /// <summary>
        /// 计算指定配合的结果
        /// </summary>
        public static FitResult CalculateFit(double nominalSize, FitDefinition fit)
        {
            return FitCalculator.CalculateFit(
                nominalSize,
                fit.HoleCode, fit.HoleGrade,
                fit.ShaftCode, fit.ShaftGrade
            );
        }
    }
}
