using System;
using System.Collections.Generic;

namespace MDSolids
{
    /// <summary>
    /// 材料属性
    /// </summary>
    public class MaterialProperty
    {
        /// <summary>材料名称</summary>
        public string Name { get; set; }

        /// <summary>屈服强度 sigma_s (MPa)，铸铁等无屈服强度的材料为0</summary>
        public double SigmaS { get; set; }

        /// <summary>抗拉强度 sigma_b (MPa)</summary>
        public double SigmaB { get; set; }

        /// <summary>弹性模量 E (MPa)</summary>
        public double E { get; set; }

        /// <summary>泊松比 mu</summary>
        public double Mu { get; set; }

        /// <summary>密度 (kg/mm^3)</summary>
        public double Density { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// 材料属性数据库 - 包含常用工程材料的力学性能参数
    /// </summary>
    public static class MaterialDatabase
    {
        private static List<MaterialProperty> _materials;

        public static List<MaterialProperty> Materials
        {
            get
            {
                if (_materials == null)
                    _materials = CreateMaterials();
                return _materials;
            }
        }

        private static List<MaterialProperty> CreateMaterials()
        {
            return new List<MaterialProperty>
            {
                new MaterialProperty
                {
                    Name = "碳素钢 Q235",
                    SigmaS = 235,
                    SigmaB = 375,
                    E = 206000,
                    Mu = 0.3,
                    Density = 7.85e-6
                },
                new MaterialProperty
                {
                    Name = "碳素钢 Q275",
                    SigmaS = 275,
                    SigmaB = 410,
                    E = 206000,
                    Mu = 0.3,
                    Density = 7.85e-6
                },
                new MaterialProperty
                {
                    Name = "碳素钢 45#",
                    SigmaS = 355,
                    SigmaB = 600,
                    E = 206000,
                    Mu = 0.3,
                    Density = 7.85e-6
                },
                new MaterialProperty
                {
                    Name = "合金钢 40Cr",
                    SigmaS = 785,
                    SigmaB = 980,
                    E = 206000,
                    Mu = 0.3,
                    Density = 7.85e-6
                },
                new MaterialProperty
                {
                    Name = "合金钢 42CrMo",
                    SigmaS = 930,
                    SigmaB = 1080,
                    E = 206000,
                    Mu = 0.3,
                    Density = 7.85e-6
                },
                new MaterialProperty
                {
                    Name = "不锈钢 1Cr18Ni9Ti",
                    SigmaS = 205,
                    SigmaB = 520,
                    E = 193000,
                    Mu = 0.29,
                    Density = 7.93e-6
                },
                new MaterialProperty
                {
                    Name = "铸铁 HT200",
                    SigmaS = 0,
                    SigmaB = 200,
                    E = 130000,
                    Mu = 0.26,
                    Density = 7.2e-6
                },
                new MaterialProperty
                {
                    Name = "铝合金 6061",
                    SigmaS = 276,
                    SigmaB = 310,
                    E = 68900,
                    Mu = 0.33,
                    Density = 2.7e-6
                },
                new MaterialProperty
                {
                    Name = "铜合金 H62",
                    SigmaS = 160,
                    SigmaB = 330,
                    E = 100000,
                    Mu = 0.34,
                    Density = 8.5e-6
                }
            };
        }

        /// <summary>
        /// 根据名称查找材料
        /// </summary>
        public static MaterialProperty FindByName(string name)
        {
            return Materials.Find(m => m.Name == name);
        }
    }
}
