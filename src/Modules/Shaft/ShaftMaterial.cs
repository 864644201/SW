using System;
using System.Collections.Generic;

namespace ShaftDesign
{
    /// <summary>
    /// 轴材料属性定义
    /// </summary>
    public class ShaftMaterial
    {
        /// <summary>材料名称</summary>
        public string Name { get; set; }

        /// <summary>屈服强度 sigma_s (MPa)</summary>
        public double SigmaS { get; set; }

        /// <summary>对称循环弯曲疲劳极限 sigma_-1 (MPa)</summary>
        public double SigmaMinus1 { get; set; }

        /// <summary>对称循环扭转疲劳极限 tau_-1 (MPa)</summary>
        public double TauMinus1 { get; set; }

        /// <summary>抗拉强度 sigma_b (MPa)</summary>
        public double SigmaB { get; set; }

        /// <summary>脉动循环弯曲疲劳极限 sigma_0 (MPa)</summary>
        public double Sigma0 { get; set; }

        /// <summary>脉动循环扭转疲劳极限 tau_0 (MPa)</summary>
        public double Tau0 { get; set; }

        /// <summary>弹性模量 E (MPa)</summary>
        public double E { get; set; }

        /// <summary>切变模量 G (MPa)</summary>
        public double G { get; set; }

        /// <summary>
        /// 初估轴径系数 A
        /// d >= A * (P/n)^(1/3)
        /// </summary>
        public double CoefficientA { get; set; }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// 获取默认材料列表
        /// </summary>
        public static List<ShaftMaterial> GetDefaultMaterials()
        {
            return new List<ShaftMaterial>
            {
                new ShaftMaterial
                {
                    Name = "Q235",
                    SigmaS = 235,
                    SigmaMinus1 = 170,
                    TauMinus1 = 100,
                    SigmaB = 400,
                    Sigma0 = 260,
                    Tau0 = 155,
                    E = 2.06e5,
                    G = 7.94e4,
                    CoefficientA = 148
                },
                new ShaftMaterial
                {
                    Name = "45钢正火",
                    SigmaS = 355,
                    SigmaMinus1 = 270,
                    TauMinus1 = 155,
                    SigmaB = 600,
                    Sigma0 = 410,
                    Tau0 = 240,
                    E = 2.06e5,
                    G = 7.94e4,
                    CoefficientA = 126
                },
                new ShaftMaterial
                {
                    Name = "45钢调质",
                    SigmaS = 530,
                    SigmaMinus1 = 350,
                    TauMinus1 = 210,
                    SigmaB = 700,
                    Sigma0 = 530,
                    Tau0 = 320,
                    E = 2.06e5,
                    G = 7.94e4,
                    CoefficientA = 118
                },
                new ShaftMaterial
                {
                    Name = "40Cr调质",
                    SigmaS = 785,
                    SigmaMinus1 = 485,
                    TauMinus1 = 280,
                    SigmaB = 980,
                    Sigma0 = 740,
                    Tau0 = 430,
                    E = 2.06e5,
                    G = 7.94e4,
                    CoefficientA = 103
                },
                new ShaftMaterial
                {
                    Name = "42CrMo调质",
                    SigmaS = 930,
                    SigmaMinus1 = 530,
                    TauMinus1 = 310,
                    SigmaB = 1080,
                    Sigma0 = 810,
                    Tau0 = 470,
                    E = 2.06e5,
                    G = 7.94e4,
                    CoefficientA = 100
                }
            };
        }
    }
}
