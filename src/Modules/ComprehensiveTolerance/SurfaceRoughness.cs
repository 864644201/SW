using System;
using System.Collections.Generic;

namespace ComprehensiveTolerance
{
    /// <summary>
    /// 表面粗糙度计算器
    /// 基于 GB/T 1031-2009 / ISO 468
    /// </summary>
    public static class SurfaceRoughnessCalculator
    {
        /// <summary>
        /// 表面粗糙度计算结果
        /// </summary>
        public class RoughnessResult
        {
            public double Ra { get; set; }
            public double Rz { get; set; }
            public double Rq { get; set; }
            public string Grade { get; set; }
            public string ManufacturingMethod { get; set; }
            public string Application { get; set; }
            public string Description { get; set; }
        }

        /// <summary>
        /// 标准 Ra 系列值 (μm) - GB/T 1031-2009 第1系列
        /// </summary>
        public static readonly double[] StandardRaValues = new double[]
        {
            0.008, 0.010, 0.012, 0.016, 0.020, 0.025, 0.032, 0.040, 0.050,
            0.063, 0.080, 0.100, 0.125, 0.160, 0.20, 0.25, 0.32, 0.40, 0.50,
            0.63, 0.80, 1.00, 1.25, 1.60, 2.0, 2.5, 3.2, 4.0, 5.0,
            6.3, 8.0, 10.0, 12.5, 16.0, 20, 25, 32, 40, 50,
            63, 80, 100
        };

        /// <summary>
        /// Ra 粗糙度等级对照表
        /// </summary>
        private struct RaGradeInfo
        {
            public double RaMax;
            public string Grade;
            public string ManufacturingMethod;
            public string Application;
            public string Description;
        }

        private static readonly RaGradeInfo[] RaGradeTable = new RaGradeInfo[]
        {
            new RaGradeInfo
            {
                RaMax = 0.008,
                Grade = "N1 (▽14)",
                ManufacturingMethod = "超精密加工: 镜面磨削、超精密研磨、离子束加工",
                Application = "精密量块、精密轴承滚道、光学镜面",
                Description = "最高级别表面粗糙度,仅有极少加工方法能达到"
            },
            new RaGradeInfo
            {
                RaMax = 0.012,
                Grade = "N1 (▽14)",
                ManufacturingMethod = "超精密加工: 镜面磨削、超精密研磨",
                Application = "精密量具、精密轴承、光学元件",
                Description = "极高精度表面,用于最关键配合面"
            },
            new RaGradeInfo
            {
                RaMax = 0.025,
                Grade = "N2 (▽13)",
                ManufacturingMethod = "超精密磨削、精密研磨、精密抛光",
                Application = "精密量具工作面、高精度轴承、精密液压阀",
                Description = "极高精度表面,用于精密配合"
            },
            new RaGradeInfo
            {
                RaMax = 0.05,
                Grade = "N3 (▽12)",
                ManufacturingMethod = "精密磨削、精密研磨",
                Application = "精密量具、精密轴承配合面、精密齿轮齿面",
                Description = "很高精度表面"
            },
            new RaGradeInfo
            {
                RaMax = 0.1,
                Grade = "N4 (▽11)",
                ManufacturingMethod = "精密磨削、精密珩磨、精密研磨",
                Application = "液压缸内表面、精密滑动面、精密密封面",
                Description = "高精度表面,用于重要配合"
            },
            new RaGradeInfo
            {
                RaMax = 0.2,
                Grade = "N5 (▽10)",
                ManufacturingMethod = "精密磨削、精密车削、珩磨",
                Application = "精密轴颈、精密导轨面、精密齿轮",
                Description = "较高精度表面"
            },
            new RaGradeInfo
            {
                RaMax = 0.4,
                Grade = "N6 (▽9)",
                ManufacturingMethod = "精磨、精车、精铣、精密镗削",
                Application = "一般轴承配合面、齿轮齿面、滑动导轨",
                Description = "中高精度表面,常见精密配合要求"
            },
            new RaGradeInfo
            {
                RaMax = 0.8,
                Grade = "N7 (▽8)",
                ManufacturingMethod = "磨削、精车、精铣、铰削",
                Application = "一般配合面、轴颈、齿轮、键槽侧面",
                Description = "中等精度表面,最常用的一般配合粗糙度"
            },
            new RaGradeInfo
            {
                RaMax = 1.6,
                Grade = "N8 (▽7)",
                ManufacturingMethod = "车削、铣削、镗削、拉削",
                Application = "一般配合面、非关键密封面、一般轴类零件",
                Description = "中等精度表面,用于一般配合"
            },
            new RaGradeInfo
            {
                RaMax = 3.2,
                Grade = "N9 (▽6)",
                ManufacturingMethod = "车削、铣削、钻削、镗削",
                Application = "非配合面、一般结构件表面、螺栓孔",
                Description = "较低精度表面,用于非精密配合"
            },
            new RaGradeInfo
            {
                RaMax = 6.3,
                Grade = "N10 (▽5)",
                ManufacturingMethod = "粗车、粗铣、粗镗、砂型铸造",
                Application = "非配合面、箱体底面、支架表面、退刀槽",
                Description = "低精度表面,用于非配合外观面"
            },
            new RaGradeInfo
            {
                RaMax = 12.5,
                Grade = "N11 (▽4)",
                ManufacturingMethod = "粗车、粗铣、粗刨、铸造、锻造",
                Application = "非配合面、不重要接触面、螺栓头部底面",
                Description = "粗糙表面,用于不重要的非配合面"
            },
            new RaGradeInfo
            {
                RaMax = 25,
                Grade = "N12 (▽3)",
                ManufacturingMethod = "粗加工、铸造、锻造、气割",
                Application = "非配合自由表面、机座底面、焊接件表面",
                Description = "很粗糙表面"
            },
            new RaGradeInfo
            {
                RaMax = 50,
                Grade = "N13 (▽2)",
                ManufacturingMethod = "粗加工、铸造、锻造、气割、砂轮清理",
                Application = "非配合自由表面、铸件毛坯面",
                Description = "极粗糙表面"
            },
            new RaGradeInfo
            {
                RaMax = 100,
                Grade = "N14 (▽1)",
                ManufacturingMethod = "铸造、锻造、气割、砂轮清理",
                Application = "毛坯面、非加工面",
                Description = "最粗糙等级,几乎无加工痕迹要求"
            }
        };

        /// <summary>
        /// Rz 标准系列值 (μm) - GB/T 1031-2009
        /// </summary>
        public static readonly double[] StandardRzValues = new double[]
        {
            0.025, 0.05, 0.1, 0.2, 0.4, 0.8, 1.6, 3.2, 6.3, 12.5,
            25, 50, 100, 200, 400, 800, 1600
        };

        /// <summary>
        /// 根据 Ra 值计算粗糙度信息
        /// </summary>
        public static RoughnessResult Calculate(double ra)
        {
            var result = new RoughnessResult();
            result.Ra = ra;
            result.Rz = RaToRz(ra);
            result.Rq = RaToRq(ra);

            // 查找对应的等级信息
            for (int i = 0; i < RaGradeTable.Length; i++)
            {
                if (ra <= RaGradeTable[i].RaMax)
                {
                    result.Grade = RaGradeTable[i].Grade;
                    result.ManufacturingMethod = RaGradeTable[i].ManufacturingMethod;
                    result.Application = RaGradeTable[i].Application;
                    result.Description = RaGradeTable[i].Description;
                    return result;
                }
            }

            // 超出范围
            result.Grade = "超出标准范围";
            result.ManufacturingMethod = "特殊加工";
            result.Application = "特殊用途";
            result.Description = "超出 GB/T 1031-2009 标准范围";
            return result;
        }

        /// <summary>
        /// Ra 转 Rz (经验公式)
        /// Rz ≈ Ra × K, 其中 K 取决于加工方法
        /// 一般情况 K ≈ 4~7, 取经验值 4.5
        /// GB/T 1031-2009 中 Rz ≈ 4~6 倍 Ra
        /// </summary>
        public static double RaToRz(double ra)
        {
            // 经验系数随 Ra 变化
            double k;
            if (ra <= 0.1)
                k = 5.0;      // 超精密加工
            else if (ra <= 0.8)
                k = 4.5;      // 精密加工
            else if (ra <= 6.3)
                k = 4.0;      // 一般加工
            else if (ra <= 25)
                k = 3.5;      // 粗加工
            else
                k = 3.0;      // 粗糙加工

            return ra * k;
        }

        /// <summary>
        /// Ra 转 Rq (均方根粗糙度)
        /// Rq ≈ Ra × 1.1 ~ 1.25
        /// 对于高斯分布的表面, Rq ≈ Ra × 1.11
        /// </summary>
        public static double RaToRq(double ra)
        {
            return ra * 1.11;
        }

        /// <summary>
        /// Rz 转 Ra
        /// </summary>
        public static double RzToRa(double rz)
        {
            return rz / 4.5;
        }

        /// <summary>
        /// Rq 转 Ra
        /// </summary>
        public static double RqToRa(double rq)
        {
            return rq / 1.11;
        }

        /// <summary>
        /// 根据 Ra 值获取推荐的加工方法
        /// </summary>
        public static string GetManufacturingMethod(double ra)
        {
            if (ra <= 0.025) return "超精密研磨、镜面磨削、离子束加工";
            if (ra <= 0.05) return "精密研磨、超精密磨削";
            if (ra <= 0.1) return "精密磨削、精密珩磨";
            if (ra <= 0.2) return "精密磨削、精密车削";
            if (ra <= 0.4) return "精磨、精车、精铣";
            if (ra <= 0.8) return "磨削、精车、精铣、铰削";
            if (ra <= 1.6) return "车削、铣削、镗削";
            if (ra <= 3.2) return "车削、铣削、钻削";
            if (ra <= 6.3) return "粗车、粗铣、粗镗";
            if (ra <= 12.5) return "粗车、粗铣、粗刨";
            if (ra <= 25) return "粗加工、铸造、锻造";
            if (ra <= 50) return "粗加工、铸造、气割";
            return "铸造、锻造、毛坯";
        }

        /// <summary>
        /// 根据 Ra 值获取典型应用
        /// </summary>
        public static string GetTypicalApplication(double ra)
        {
            if (ra <= 0.025) return "精密量块、光学镜面";
            if (ra <= 0.05) return "精密量具、高精度轴承";
            if (ra <= 0.1) return "液压缸内表面、精密密封面";
            if (ra <= 0.2) return "精密轴颈、精密导轨";
            if (ra <= 0.4) return "一般轴承配合面、齿轮齿面";
            if (ra <= 0.8) return "一般配合面、轴颈、键槽";
            if (ra <= 1.6) return "一般配合面、非关键密封面";
            if (ra <= 3.2) return "非配合面、一般结构件";
            if (ra <= 6.3) return "非配合面、箱体底面";
            if (ra <= 12.5) return "非配合自由表面";
            if (ra <= 25) return "机座底面、焊接件表面";
            if (ra <= 50) return "铸件毛坯面";
            return "毛坯面、非加工面";
        }

        /// <summary>
        /// 获取表面粗糙度符号说明
        /// 基本符号: √ (任何方法获得的表面)
        /// 去除材料: √̲ (去除材料方法获得的表面,如车、铣、磨等)
        /// 不去除材料: √̅ (不去除材料方法获得的表面,如铸、锻、轧等)
        /// </summary>
        public static string GetSymbolDescription(int symbolType)
        {
            switch (symbolType)
            {
                case 0:
                    return "基本符号 (√): 表示用任何方法获得的表面,单独使用时无精度要求";
                case 1:
                    return "去除材料符号 (√̲): 表示用去除材料方法获得的表面,如车、铣、磨、钻、电火花加工等";
                case 2:
                    return "不去除材料符号 (√̅): 表示用不去除材料方法获得的表面,如铸、锻、轧、冲压、粉末冶金等";
                default:
                    return "未知符号类型";
            }
        }

        /// <summary>
        /// 获取完整的表面粗糙度标注说明
        /// 格式: 符号 + Ra值 + 加工方法
        /// 例如: √̲ Ra 0.8 (磨削)
        /// </summary>
        public static string GetFullDesignation(double ra, int symbolType)
        {
            string symbol;
            switch (symbolType)
            {
                case 1: symbol = "去除材料"; break;
                case 2: symbol = "不去除材料"; break;
                default: symbol = "任何方法"; break;
            }

            string method = GetManufacturingMethod(ra);
            return $"{symbol} Ra {ra} μm ({method})";
        }

        /// <summary>
        /// 计算轮廓算术平均偏差 Ra
        /// Ra = (1/L) * ∫|y(x)|dx
        /// 离散近似: Ra = (1/n) * Σ|yi|
        /// </summary>
        /// <param name="profileData">轮廓数据 (偏差值数组)</param>
        /// <returns>Ra 值</returns>
        public static double CalculateRaFromProfile(double[] profileData)
        {
            if (profileData == null || profileData.Length == 0) return 0;

            double sum = 0;
            for (int i = 0; i < profileData.Length; i++)
            {
                sum += Math.Abs(profileData[i]);
            }

            return sum / profileData.Length;
        }

        /// <summary>
        /// 计算轮廓最大高度 Rz
        /// Rz = 最大峰高 + 最大谷深 (在一个取样长度内)
        /// </summary>
        /// <param name="profileData">轮廓数据</param>
        /// <returns>Rz 值</returns>
        public static double CalculateRzFromProfile(double[] profileData)
        {
            if (profileData == null || profileData.Length == 0) return 0;

            double maxPeak = 0;
            double maxValley = 0;

            for (int i = 0; i < profileData.Length; i++)
            {
                if (profileData[i] > maxPeak) maxPeak = profileData[i];
                if (profileData[i] < maxValley) maxValley = profileData[i];
            }

            return maxPeak - maxValley;
        }

        /// <summary>
        /// 计算均方根粗糙度 Rq
        /// Rq = sqrt((1/n) * Σyi²)
        /// </summary>
        /// <param name="profileData">轮廓数据</param>
        /// <returns>Rq 值</returns>
        public static double CalculateRqFromProfile(double[] profileData)
        {
            if (profileData == null || profileData.Length == 0) return 0;

            double sumSq = 0;
            for (int i = 0; i < profileData.Length; i++)
            {
                sumSq += profileData[i] * profileData[i];
            }

            return Math.Sqrt(sumSq / profileData.Length);
        }
    }
}
