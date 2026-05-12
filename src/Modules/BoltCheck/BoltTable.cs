using System;
using System.Collections.Generic;

namespace BoltCheck
{
    /// <summary>
    /// 螺栓标准尺寸参数表 (GB/T 196-2003)
    /// </summary>
    public class BoltDimension
    {
        /// <summary>公称直径 d (mm)</summary>
        public double D { get; set; }
        /// <summary>螺距 P (mm)</summary>
        public double P { get; set; }
        /// <summary>小径 d1 (mm)</summary>
        public double D1 { get; set; }
        /// <summary>中径 d2 (mm)</summary>
        public double D2 { get; set; }
        /// <summary>应力截面积 As (mm^2)</summary>
        public double As { get; set; }

        public string DisplayName => $"M{D}";
    }

    /// <summary>
    /// 螺栓材料等级及力学性能
    /// </summary>
    public class BoltGrade
    {
        /// <summary>等级标识</summary>
        public string Name { get; set; }
        /// <summary>抗拉强度 sigma_b (MPa)</summary>
        public double SigmaB { get; set; }
        /// <summary>屈服强度 sigma_s (MPa)</summary>
        public double SigmaS { get; set; }
        /// <summary>保证应力 Sp (MPa)</summary>
        public double Sp { get; set; }
        /// <summary>疲劳极限 sigma_(-1) (MPa)，对称循环</summary>
        public double SigmaEndurance { get; set; }
    }

    /// <summary>
    /// 螺栓标准参数查询表
    /// </summary>
    public static class BoltTable
    {
        /// <summary>
        /// 螺栓尺寸表 (GB/T 196-2003 普通螺纹基本尺寸)
        /// </summary>
        public static List<BoltDimension> GetDimensions()
        {
            return new List<BoltDimension>
            {
                new BoltDimension { D = 6,  P = 1.0,  D1 = 4.917,  D2 = 5.350,  As = 20.1  },
                new BoltDimension { D = 8,  P = 1.25, D1 = 6.647,  D2 = 7.188,  As = 36.6  },
                new BoltDimension { D = 10, P = 1.5,  D1 = 8.376,  D2 = 9.026,  As = 58.0  },
                new BoltDimension { D = 12, P = 1.75, D1 = 10.106, D2 = 10.863, As = 84.3  },
                new BoltDimension { D = 16, P = 2.0,  D1 = 13.835, D2 = 14.701, As = 157.0 },
                new BoltDimension { D = 20, P = 2.5,  D1 = 17.294, D2 = 18.376, As = 245.0 },
                new BoltDimension { D = 24, P = 3.0,  D1 = 20.752, D2 = 22.051, As = 353.0 },
                new BoltDimension { D = 30, P = 3.5,  D1 = 25.211, D2 = 27.727, As = 561.0 },
                new BoltDimension { D = 36, P = 4.0,  D1 = 31.670, D2 = 34.026, As = 817.0 },
            };
        }

        /// <summary>
        /// 螺栓材料等级力学性能表 (GB/T 3098.1)
        /// </summary>
        public static List<BoltGrade> GetGrades()
        {
            return new List<BoltGrade>
            {
                new BoltGrade { Name = "4.6",  SigmaB = 400,  SigmaS = 240,  Sp = 225,  SigmaEndurance = 120  },
                new BoltGrade { Name = "4.8",  SigmaB = 400,  SigmaS = 320,  Sp = 310,  SigmaEndurance = 130  },
                new BoltGrade { Name = "5.8",  SigmaB = 500,  SigmaS = 400,  Sp = 380,  SigmaEndurance = 160  },
                new BoltGrade { Name = "6.8",  SigmaB = 600,  SigmaS = 480,  Sp = 440,  SigmaEndurance = 190  },
                new BoltGrade { Name = "8.8",  SigmaB = 800,  SigmaS = 640,  Sp = 580,  SigmaEndurance = 260  },
                new BoltGrade { Name = "10.9", SigmaB = 1000, SigmaS = 900,  Sp = 830,  SigmaEndurance = 320  },
                new BoltGrade { Name = "12.9", SigmaB = 1200, SigmaS = 1080, Sp = 970,  SigmaEndurance = 380  },
            };
        }
    }
}
