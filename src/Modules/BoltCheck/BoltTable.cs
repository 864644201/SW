using System;
using System.Collections.Generic;

namespace BoltCheck
{
    /// <summary>
    /// 连接形式
    /// </summary>
    public enum ConnectionType
    {
        普通螺栓,
        铰制孔螺栓,
        双头螺柱,
        螺钉
    }

    /// <summary>
    /// 工况类型
    /// </summary>
    public enum LoadCondition
    {
        静载,
        脉动循环,
        对称循环
    }

    public class BoltDimension
    {
        public double D { get; set; }
        public double P { get; set; }
        public double D1 { get; set; }
        public double D2 { get; set; }
        public double As { get; set; }
        public bool IsFinePitch { get; set; }

        public string DisplayName => IsFinePitch ? $"M{D}×{P}" : $"M{D}";
    }

    public class BoltGrade
    {
        public string Name { get; set; }
        public double SigmaB { get; set; }
        public double SigmaS { get; set; }
        public double Sp { get; set; }
        public double SigmaEndurance { get; set; }
        public string Material { get; set; }
    }

    public static class BoltTable
    {
        /// <summary>
        /// 螺栓尺寸表 (GB/T 196-2003) — 粗牙+细牙
        /// </summary>
        public static List<BoltDimension> GetDimensions()
        {
            return new List<BoltDimension>
            {
                // 粗牙系列
                new BoltDimension { D = 3,  P = 0.5,  D1 = 2.459,  D2 = 2.675,  As = 5.03  },
                new BoltDimension { D = 4,  P = 0.7,  D1 = 3.242,  D2 = 3.545,  As = 8.78  },
                new BoltDimension { D = 5,  P = 0.8,  D1 = 4.134,  D2 = 4.480,  As = 14.2  },
                new BoltDimension { D = 6,  P = 1.0,  D1 = 4.917,  D2 = 5.350,  As = 20.1  },
                new BoltDimension { D = 8,  P = 1.25, D1 = 6.647,  D2 = 7.188,  As = 36.6  },
                new BoltDimension { D = 10, P = 1.5,  D1 = 8.376,  D2 = 9.026,  As = 58.0  },
                new BoltDimension { D = 12, P = 1.75, D1 = 10.106, D2 = 10.863, As = 84.3  },
                new BoltDimension { D = 16, P = 2.0,  D1 = 13.835, D2 = 14.701, As = 157.0 },
                new BoltDimension { D = 20, P = 2.5,  D1 = 17.294, D2 = 18.376, As = 245.0 },
                new BoltDimension { D = 24, P = 3.0,  D1 = 20.752, D2 = 22.051, As = 353.0 },
                new BoltDimension { D = 30, P = 3.5,  D1 = 25.211, D2 = 27.727, As = 561.0 },
                new BoltDimension { D = 36, P = 4.0,  D1 = 31.670, D2 = 34.026, As = 817.0 },
                new BoltDimension { D = 42, P = 4.5,  D1 = 37.129, D2 = 40.051, As = 1120.0 },
                new BoltDimension { D = 48, P = 5.0,  D1 = 42.587, D2 = 46.051, As = 1470.0 },
                new BoltDimension { D = 56, P = 5.5,  D1 = 50.046, D2 = 53.717, As = 2030.0 },
                new BoltDimension { D = 64, P = 6.0,  D1 = 57.505, D2 = 61.384, As = 2680.0 },
                // 细牙系列
                new BoltDimension { D = 8,  P = 1.0,  D1 = 6.917,  D2 = 7.350,  As = 39.2,  IsFinePitch = true },
                new BoltDimension { D = 10, P = 1.25, D1 = 8.647,  D2 = 9.188,  As = 61.2,  IsFinePitch = true },
                new BoltDimension { D = 12, P = 1.5,  D1 = 10.376, D2 = 11.026, As = 88.1,  IsFinePitch = true },
                new BoltDimension { D = 16, P = 1.5,  D1 = 14.376, D2 = 15.026, As = 167.0, IsFinePitch = true },
                new BoltDimension { D = 20, P = 2.0,  D1 = 17.835, D2 = 18.701, As = 258.0, IsFinePitch = true },
                new BoltDimension { D = 24, P = 2.0,  D1 = 21.835, D2 = 22.701, As = 375.0, IsFinePitch = true },
                new BoltDimension { D = 30, P = 2.0,  D1 = 27.835, D2 = 28.701, As = 596.0, IsFinePitch = true },
                new BoltDimension { D = 36, P = 3.0,  D1 = 32.752, D2 = 34.051, As = 865.0, IsFinePitch = true },
            };
        }

        /// <summary>
        /// 螺栓材料等级力学性能表 (GB/T 3098.1 + 不锈钢 GB/T 3098.6)
        /// </summary>
        public static List<BoltGrade> GetGrades()
        {
            return new List<BoltGrade>
            {
                new BoltGrade { Name = "4.6",   SigmaB = 400,  SigmaS = 240,  Sp = 225,  SigmaEndurance = 120, Material = "碳钢" },
                new BoltGrade { Name = "4.8",   SigmaB = 400,  SigmaS = 320,  Sp = 310,  SigmaEndurance = 130, Material = "碳钢" },
                new BoltGrade { Name = "5.8",   SigmaB = 500,  SigmaS = 400,  Sp = 380,  SigmaEndurance = 160, Material = "碳钢" },
                new BoltGrade { Name = "6.8",   SigmaB = 600,  SigmaS = 480,  Sp = 440,  SigmaEndurance = 190, Material = "碳钢" },
                new BoltGrade { Name = "8.8",   SigmaB = 800,  SigmaS = 640,  Sp = 580,  SigmaEndurance = 260, Material = "合金钢" },
                new BoltGrade { Name = "10.9",  SigmaB = 1000, SigmaS = 900,  Sp = 830,  SigmaEndurance = 320, Material = "合金钢" },
                new BoltGrade { Name = "12.9",  SigmaB = 1200, SigmaS = 1080, Sp = 970,  SigmaEndurance = 380, Material = "合金钢" },
                new BoltGrade { Name = "A2-70", SigmaB = 700,  SigmaS = 450,  Sp = 420,  SigmaEndurance = 180, Material = "不锈钢" },
                new BoltGrade { Name = "A4-80", SigmaB = 800,  SigmaS = 600,  Sp = 560,  SigmaEndurance = 230, Material = "不锈钢" },
            };
        }
    }
}
