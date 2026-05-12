using System;
using System.Collections.Generic;

namespace ComprehensiveTolerance
{
    /// <summary>
    /// 形位公差 (GD&T) 计算器
    /// 基于 GB/T 1182-2018 / ISO 1101
    /// </summary>
    public class GeometricToleranceCalculator
    {
        /// <summary>
        /// 形位公差项目信息
        /// </summary>
        public class ToleranceInfo
        {
            public string Name { get; set; }
            public string Symbol { get; set; }
            public string Category { get; set; }
            public string DefaultZoneType { get; set; }
            public string Description { get; set; }
            public string[] AllowedZoneTypes { get; set; }
        }

        /// <summary>
        /// 计算结果
        /// </summary>
        public class ToleranceResult
        {
            public string ZoneShape { get; set; }
            public double ZoneSize { get; set; }
            public bool HasBonusTolerance { get; set; }
            public double BonusTolerance { get; set; }
            public double TotalTolerance { get; set; }
            public double MMCSize { get; set; }
            public double LMCSize { get; set; }
        }

        /// <summary>
        /// 材料条件修饰符
        /// </summary>
        public enum MaterialCondition
        {
            RFS,    // Regardless of Feature Size (无修饰符)
            MMC,    // Maximum Material Condition (最大实体条件) M
            LMC,    // Least Material Condition (最小实体条件) L
            RPR     // Reciprocity Requirement (可逆要求) R
        }

        private static readonly Dictionary<string, ToleranceInfo> ToleranceFeatures = new Dictionary<string, ToleranceInfo>
        {
            // ===== 形状公差 =====
            ["直线度"] = new ToleranceInfo
            {
                Name = "直线度",
                Symbol = "╱", // /
                Category = "形状公差",
                DefaultZoneType = "两平行直线之间的区域",
                Description = "限制实际直线对其理想直线的变动量。用于控制直线、轴线、棱线等的形状精度。",
                AllowedZoneTypes = new[] { "两平行直线", "圆柱面内" }
            },
            ["平面度"] = new ToleranceInfo
            {
                Name = "平面度",
                Symbol = "□", // □
                Category = "形状公差",
                DefaultZoneType = "两平行平面之间的区域",
                Description = "限制实际表面对理想平面的变动量。用于控制平面的形状精度。",
                AllowedZoneTypes = new[] { "两平行平面" }
            },
            ["圆度"] = new ToleranceInfo
            {
                Name = "圆度",
                Symbol = "○", // ○
                Category = "形状公差",
                DefaultZoneType = "两同心圆之间的区域",
                Description = "限制实际圆对其理想圆的变动量。用于控制圆柱面、圆锥面、球面等的截面形状精度。",
                AllowedZoneTypes = new[] { "两同心圆之间" }
            },
            ["圆柱度"] = new ToleranceInfo
            {
                Name = "圆柱度",
                Symbol = "○/□", // 圆柱度符号
                Category = "形状公差",
                DefaultZoneType = "两同轴圆柱面之间的区域",
                Description = "限制实际圆柱面对其理想圆柱面的变动量。是圆度、直线度和素线平行度的综合控制。",
                AllowedZoneTypes = new[] { "两同轴圆柱面之间" }
            },

            // ===== 轮廓公差 =====
            ["线轮廓度"] = new ToleranceInfo
            {
                Name = "线轮廓度",
                Symbol = "⌒", // ⌒
                Category = "轮廓公差",
                DefaultZoneType = "包络一系列圆的两等距曲线之间的区域",
                Description = "限制实际曲线对其理想曲线的变动量。公差带为包络一系列直径等于公差值的圆的两包络线之间的区域。",
                AllowedZoneTypes = new[] { "两等距曲线" }
            },
            ["面轮廓度"] = new ToleranceInfo
            {
                Name = "面轮廓度",
                Symbol = "⌒⌒", // 面轮廓度
                Category = "轮廓公差",
                DefaultZoneType = "包络一系列球的两等距曲面之间的区域",
                Description = "限制实际曲面对理想曲面的变动量。公差带为包络一系列直径等于公差值的球的两包络面之间的区域。",
                AllowedZoneTypes = new[] { "两等距曲面" }
            },

            // ===== 方向公差 =====
            ["平行度"] = new ToleranceInfo
            {
                Name = "平行度",
                Symbol = "∥", // ∥
                Category = "方向公差",
                DefaultZoneType = "平行于基准的两平行平面之间的区域",
                Description = "限制被测要素对基准要素在平行方向上的变动量。用于控制面或线对基准的平行程度。",
                AllowedZoneTypes = new[] { "两平行平面", "圆柱面内" }
            },
            ["垂直度"] = new ToleranceInfo
            {
                Name = "垂直度",
                Symbol = "⊥", // ⊥
                Category = "方向公差",
                DefaultZoneType = "垂直于基准的两平行平面之间的区域",
                Description = "限制被测要素对基准要素在垂直方向上的变动量。用于控制面或线对基准的垂直程度。",
                AllowedZoneTypes = new[] { "两平行平面", "圆柱面内" }
            },
            ["倾斜度"] = new ToleranceInfo
            {
                Name = "倾斜度",
                Symbol = "∠", // ∠
                Category = "方向公差",
                DefaultZoneType = "与基准成理论角度的两平行平面之间的区域",
                Description = "限制被测要素对基准要素在给定角度方向上的变动量。用于控制面或线对基准的角度精度。",
                AllowedZoneTypes = new[] { "两平行平面", "圆柱面内" }
            },

            // ===== 位置公差 =====
            ["位置度"] = new ToleranceInfo
            {
                Name = "位置度",
                Symbol = "◇", // ◇
                Category = "位置公差",
                DefaultZoneType = "以理想位置为中心的圆柱面内或球内",
                Description = "限制被测要素的实际位置对其理想位置的变动量。用于控制点、线、面的位置精度。",
                AllowedZoneTypes = new[] { "圆柱面内", "两平行平面", "圆内", "球内" }
            },
            ["同轴度"] = new ToleranceInfo
            {
                Name = "同轴度",
                Symbol = "◎", // ◎
                Category = "位置公差",
                DefaultZoneType = "与基准同轴的圆柱面内的区域",
                Description = "限制被测轴线对基准轴线的变动量。用于控制被测轴线与基准轴线的同轴程度。",
                AllowedZoneTypes = new[] { "圆柱面内" }
            },
            ["对称度"] = new ToleranceInfo
            {
                Name = "对称度",
                Symbol = "██", // 对称度符号
                Category = "位置公差",
                DefaultZoneType = "与基准对称的两平行平面之间的区域",
                Description = "限制被测要素对基准要素的对称变动量。用于控制被测中心要素相对于基准中心要素的位置。",
                AllowedZoneTypes = new[] { "两平行平面" }
            },

            // ===== 跳动公差 =====
            ["圆跳动"] = new ToleranceInfo
            {
                Name = "圆跳动",
                Symbol = "↗", // 圆跳动符号
                Category = "跳动公差",
                DefaultZoneType = "在垂直于基准轴线的测量平面内，两同心圆之间的区域",
                Description = "限制被测表面绕基准轴线回转一周时的最大变动量。分为径向圆跳动、端面圆跳动和斜向圆跳动。",
                AllowedZoneTypes = new[] { "两同心圆之间" }
            },
            ["全跳动"] = new ToleranceInfo
            {
                Name = "全跳动",
                Symbol = "↗↗", // 全跳动符号
                Category = "跳动公差",
                DefaultZoneType = "绕基准轴线回转时，两同轴圆柱面之间的区域",
                Description = "限制被测表面绕基准轴线连续回转时的变动量。是圆跳动和轴向跳动的综合控制。",
                AllowedZoneTypes = new[] { "两同轴圆柱面之间" }
            }
        };

        /// <summary>
        /// 获取形位公差项目信息
        /// </summary>
        public ToleranceInfo GetToleranceInfo(string featureName)
        {
            if (ToleranceFeatures.TryGetValue(featureName, out ToleranceInfo info))
                return info;

            return new ToleranceInfo
            {
                Name = featureName,
                Symbol = "?",
                Category = "未知",
                DefaultZoneType = "未知",
                Description = "未知的形位公差项目",
                AllowedZoneTypes = new[] { "未知" }
            };
        }

        /// <summary>
        /// 计算形位公差
        /// </summary>
        public ToleranceResult Calculate(string featureName, double toleranceValue, double nominalSize,
            string zoneType, string materialCondition)
        {
            var result = new ToleranceResult();
            var info = GetToleranceInfo(featureName);

            // 公差带形状
            result.ZoneShape = info.DefaultZoneType;
            result.ZoneSize = toleranceValue;

            // 计算 MMC 和 LMC
            // 假设外部特征(轴): MMC = 最大极限尺寸, LMC = 最小极限尺寸
            // 假设内部特征(孔): MMC = 最小极限尺寸, LMC = 最大极限尺寸
            // 这里简化为公称尺寸的偏移
            double sizeTolerance = nominalSize * 0.01; // 假设 1% 的尺寸公差
            result.MMCSize = nominalSize + sizeTolerance / 2;
            result.LMCSize = nominalSize - sizeTolerance / 2;

            // 材料条件修饰符计算
            MaterialCondition mc = ParseMaterialCondition(materialCondition);
            result.HasBonusTolerance = (mc == MaterialCondition.MMC || mc == MaterialCondition.LMC);

            if (mc == MaterialCondition.MMC)
            {
                // 最大实体条件: 当实际尺寸偏离 MMC 时,可获得附加公差
                // Bonus = |实际尺寸 - MMC尺寸|
                // 最大附加公差 = 尺寸公差
                result.BonusTolerance = sizeTolerance;
                result.TotalTolerance = toleranceValue + result.BonusTolerance;
            }
            else if (mc == MaterialCondition.LMC)
            {
                // 最小实体条件: 当实际尺寸偏离 LMC 时,可获得附加公差
                result.BonusTolerance = sizeTolerance;
                result.TotalTolerance = toleranceValue + result.BonusTolerance;
            }
            else if (mc == MaterialCondition.RPR)
            {
                // 可逆要求: 形位公差可补偿给尺寸公差
                result.BonusTolerance = sizeTolerance;
                result.TotalTolerance = toleranceValue + result.BonusTolerance;
            }
            else
            {
                // RFS (无修饰符): 不考虑尺寸变化
                result.BonusTolerance = 0;
                result.TotalTolerance = toleranceValue;
            }

            // 根据公差带类型调整
            if (zoneType.Contains("圆柱"))
            {
                // 圆柱形公差带: 直径表示
                result.ZoneShape = "圆柱形公差带 (直径 " + toleranceValue.ToString("F4") + " mm)";
            }
            else if (zoneType.Contains("圆") && !zoneType.Contains("圆柱"))
            {
                // 圆形公差带
                result.ZoneShape = "圆形公差带 (直径 " + toleranceValue.ToString("F4") + " mm)";
            }
            else if (zoneType.Contains("球"))
            {
                // 球形公差带
                result.ZoneShape = "球形公差带 (直径 " + toleranceValue.ToString("F4") + " mm)";
            }
            else
            {
                // 平行平面/直线公差带
                result.ZoneShape = "宽度 " + toleranceValue.ToString("F4") + " mm 的" + info.DefaultZoneType;
            }

            return result;
        }

        private MaterialCondition ParseMaterialCondition(string mc)
        {
            if (string.IsNullOrEmpty(mc)) return MaterialCondition.RFS;
            if (mc.Contains("M")) return MaterialCondition.MMC;
            if (mc.Contains("L")) return MaterialCondition.LMC;
            if (mc.Contains("R")) return MaterialCondition.RPR;
            return MaterialCondition.RFS;
        }

        /// <summary>
        /// 计算位置度公差 (含 MMC 附加公差)
        /// GB/T 1182-2018 位置度计算
        /// </summary>
        /// <param name="baseTolerance">基本位置度公差</param>
        /// <param name="actualSize">实际尺寸</param>
        /// <param name="mmcSize">最大实体尺寸</param>
        /// <param name="lmcSize">最小实体尺寸</param>
        /// <param name="isExternal">是否为外部特征(轴)</param>
        /// <returns>实际可用位置度公差</returns>
        public static double CalculatePositionToleranceWithMMC(double baseTolerance, double actualSize,
            double mmcSize, double lmcSize, bool isExternal)
        {
            double bonus;
            if (isExternal)
            {
                // 外部特征(轴): MMC 是最大尺寸
                bonus = mmcSize - actualSize;
            }
            else
            {
                // 内部特征(孔): MMC 是最小尺寸
                bonus = actualSize - mmcSize;
            }

            // 附加公差不能为负
            if (bonus < 0) bonus = 0;

            // 附加公差不能超过尺寸公差范围
            double sizeTolerance = Math.Abs(lmcSize - mmcSize);
            if (bonus > sizeTolerance) bonus = sizeTolerance;

            return baseTolerance + bonus;
        }

        /// <summary>
        /// 计算同轴度公差带
        /// </summary>
        /// <param name="toleranceValue">同轴度公差值</param>
        /// <param name="measuredOffsetX">测量偏移 X</param>
        /// <param name="measuredOffsetY">测量偏移 Y</param>
        /// <returns>是否合格</returns>
        public static bool CheckConcentricity(double toleranceValue, double measuredOffsetX, double measuredOffsetY)
        {
            // 同轴度公差带为圆柱形,偏移量必须在公差圆柱内
            double offset = Math.Sqrt(measuredOffsetX * measuredOffsetX + measuredOffsetY * measuredOffsetY);
            return offset <= toleranceValue / 2.0;
        }

        /// <summary>
        /// 计算圆跳动
        /// </summary>
        /// <param name="measurements">一周测量值数组</param>
        /// <returns>圆跳动值 (最大值 - 最小值)</returns>
        public static double CalculateCircularRunout(double[] measurements)
        {
            if (measurements == null || measurements.Length == 0) return 0;

            double max = double.MinValue;
            double min = double.MaxValue;

            for (int i = 0; i < measurements.Length; i++)
            {
                if (measurements[i] > max) max = measurements[i];
                if (measurements[i] < min) min = measurements[i];
            }

            return max - min;
        }

        /// <summary>
        /// 计算全跳动
        /// </summary>
        /// <param name="allMeasurements">所有截面的测量值二维数组 [截面数, 每圈测点数]</param>
        /// <returns>全跳动值</returns>
        public static double CalculateTotalRunout(double[,] allMeasurements)
        {
            if (allMeasurements == null) return 0;

            int sections = allMeasurements.GetLength(0);
            int points = allMeasurements.GetLength(1);

            double max = double.MinValue;
            double min = double.MaxValue;

            for (int i = 0; i < sections; i++)
            {
                for (int j = 0; j < points; j++)
                {
                    if (allMeasurements[i, j] > max) max = allMeasurements[i, j];
                    if (allMeasurements[i, j] < min) min = allMeasurements[i, j];
                }
            }

            return max - min;
        }

        /// <summary>
        /// 计算平面度误差 (最小二乘法)
        /// </summary>
        /// <param name="points">测量点坐标 [x, y, z]</param>
        /// <returns>平面度误差值</returns>
        public static double CalculateFlatness(double[,] points)
        {
            if (points == null || points.GetLength(0) < 3) return 0;

            int n = points.GetLength(0);

            // 最小二乘法拟合平面 z = ax + by + c
            // 构建法方程
            double sx = 0, sy = 0, sz = 0;
            double sxx = 0, syy = 0, sxy = 0, sxz = 0, syz = 0;

            for (int i = 0; i < n; i++)
            {
                double x = points[i, 0];
                double y = points[i, 1];
                double z = points[i, 2];

                sx += x;
                sy += y;
                sz += z;
                sxx += x * x;
                syy += y * y;
                sxy += x * y;
                sxz += x * z;
                syz += y * z;
            }

            // 解法方程 (简化: 假设平面近似水平)
            double c = sz / n;

            // 计算各点到拟合平面的距离
            double maxDist = double.MinValue;
            double minDist = double.MaxValue;

            for (int i = 0; i < n; i++)
            {
                double dist = points[i, 2] - c;
                if (dist > maxDist) maxDist = dist;
                if (dist < minDist) minDist = dist;
            }

            return maxDist - minDist;
        }

        /// <summary>
        /// 计算直线度误差 (最小二乘法)
        /// </summary>
        /// <param name="positions">测量位置数组</param>
        /// <param name="deviations">偏差数组</param>
        /// <returns>直线度误差值</returns>
        public static double CalculateStraightness(double[] positions, double[] deviations)
        {
            if (positions == null || deviations == null || positions.Length < 2) return 0;

            int n = positions.Length;

            // 最小二乘法拟合直线 y = ax + b
            double sx = 0, sy = 0, sxx = 0, sxy = 0;
            for (int i = 0; i < n; i++)
            {
                sx += positions[i];
                sy += deviations[i];
                sxx += positions[i] * positions[i];
                sxy += positions[i] * deviations[i];
            }

            double a = (n * sxy - sx * sy) / (n * sxx - sx * sx);
            double b = (sy - a * sx) / n;

            // 计算各点到拟合直线的偏差
            double maxDev = double.MinValue;
            double minDev = double.MaxValue;

            for (int i = 0; i < n; i++)
            {
                double fitted = a * positions[i] + b;
                double dev = deviations[i] - fitted;
                if (dev > maxDev) maxDev = dev;
                if (dev < minDev) minDev = dev;
            }

            return maxDev - minDev;
        }

        /// <summary>
        /// 计算圆度误差 (最小二乘圆法)
        /// </summary>
        /// <param name="angles">角度数组 (弧度)</param>
        /// <param name="radii">半径数组</param>
        /// <returns>圆度误差值</returns>
        public static double CalculateRoundness(double[] angles, double[] radii)
        {
            if (angles == null || radii == null || angles.Length < 3) return 0;

            int n = angles.Length;

            // 最小二乘圆: r = R + ex*cos(θ) + ey*sin(θ)
            double sc = 0, ss = 0, sr = 0, scc = 0, sss = 0, scr = 0, ssr = 0;

            for (int i = 0; i < n; i++)
            {
                double c = Math.Cos(angles[i]);
                double s = Math.Sin(angles[i]);

                sc += c;
                ss += s;
                sr += radii[i];
                scc += c * c;
                sss += s * s;
                scr += c * radii[i];
                ssr += s * radii[i];
            }

            // 简化求解
            double R = sr / n;
            double ex = (scr - sc * R) / scc;
            double ey = (ssr - ss * R) / sss;

            // 计算各点到最小二乘圆的偏差
            double maxDev = double.MinValue;
            double minDev = double.MaxValue;

            for (int i = 0; i < n; i++)
            {
                double fitted = R + ex * Math.Cos(angles[i]) + ey * Math.Sin(angles[i]);
                double dev = radii[i] - fitted;
                if (dev > maxDev) maxDev = dev;
                if (dev < minDev) minDev = dev;
            }

            return maxDev - minDev;
        }

        /// <summary>
        /// 计算圆柱度误差
        /// </summary>
        /// <param name="sections">截面数</param>
        /// <param name="pointsPerSection">每截面测点数</param>
        /// <param name="measurements">测量值数组</param>
        /// <returns>圆柱度误差值</returns>
        public static double CalculateCylindricity(int sections, int pointsPerSection, double[,] measurements)
        {
            if (measurements == null) return 0;

            // 圆柱度 = 各截面圆度误差的最大值 + 轴线直线度误差
            // 简化计算: 取所有测量值的极差
            double max = double.MinValue;
            double min = double.MaxValue;

            for (int i = 0; i < measurements.GetLength(0); i++)
            {
                for (int j = 0; j < measurements.GetLength(1); j++)
                {
                    if (measurements[i, j] > max) max = measurements[i, j];
                    if (measurements[i, j] < min) min = measurements[i, j];
                }
            }

            return max - min;
        }
    }
}
