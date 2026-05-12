using System;
using System.Collections.Generic;

namespace SpringDesign
{
    /// <summary>
    /// 弹簧材料信息
    /// </summary>
    public class SpringMaterial
    {
        /// <summary>材料名称</summary>
        public string Name { get; set; }

        /// <summary>切变模量 G (MPa)</summary>
        public double G { get; set; }

        /// <summary>弹性模量 E (MPa)</summary>
        public double E { get; set; }

        /// <summary>II类载荷许用切应力 tau_II (MPa)</summary>
        public double TauII { get; set; }

        /// <summary>III类载荷许用切应力 tau_III (MPa)</summary>
        public double TauIII { get; set; }

        /// <summary>I类载荷许用切应力 tau_I (MPa)，通常为 tau_II 的 0.5~0.6 倍</summary>
        public double TauI { get; set; }

        /// <summary>许用弯曲应力 sigma_b (MPa)，用于扭转弹簧</summary>
        public double SigmaB { get; set; }

        /// <summary>抗拉强度 sigma_b (MPa)</summary>
        public double SigmaUlt { get; set; }

        /// <summary>推荐丝径范围最小值 (mm)</summary>
        public double WireDiaMin { get; set; }

        /// <summary>推荐丝径范围最大值 (mm)</summary>
        public double WireDiaMax { get; set; }

        /// <summary>材料描述</summary>
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// 弹簧材料数据库
    /// </summary>
    public static class SpringMaterialDatabase
    {
        private static readonly List<SpringMaterial> _materials = new List<SpringMaterial>
        {
            // 碳素弹簧钢丝 (琴钢丝)
            new SpringMaterial
            {
                Name = "琴钢丝 (Grade I)",
                G = 79000,
                E = 206000,
                TauI = 0.30 * 2000,
                TauII = 0.40 * 2000,
                TauIII = 0.50 * 2000,
                SigmaB = 0.60 * 2000,
                SigmaUlt = 2000,
                WireDiaMin = 0.1,
                WireDiaMax = 6.0,
                Description = "碳素弹簧钢丝 I 组，sigma_b=2000MPa (d=0.1~0.3mm)"
            },
            new SpringMaterial
            {
                Name = "琴钢丝 (Grade II)",
                G = 79000,
                E = 206000,
                TauI = 0.30 * 1700,
                TauII = 0.40 * 1700,
                TauIII = 0.50 * 1700,
                SigmaB = 0.60 * 1700,
                SigmaUlt = 1700,
                WireDiaMin = 0.1,
                WireDiaMax = 6.0,
                Description = "碳素弹簧钢丝 II 组，sigma_b=1700MPa (d=0.3~1.0mm)"
            },
            new SpringMaterial
            {
                Name = "琴钢丝 (Grade III)",
                G = 79000,
                E = 206000,
                TauI = 0.30 * 1400,
                TauII = 0.40 * 1400,
                TauIII = 0.50 * 1400,
                SigmaB = 0.60 * 1400,
                SigmaUlt = 1400,
                WireDiaMin = 0.1,
                WireDiaMax = 6.0,
                Description = "碳素弹簧钢丝 III 组，sigma_b=1400MPa (d=1.0~3.0mm)"
            },
            new SpringMaterial
            {
                Name = "琴钢丝 (Grade IV)",
                G = 79000,
                E = 206000,
                TauI = 0.30 * 1100,
                TauII = 0.40 * 1100,
                TauIII = 0.50 * 1100,
                SigmaB = 0.60 * 1100,
                SigmaUlt = 1100,
                WireDiaMin = 0.1,
                WireDiaMax = 6.0,
                Description = "碳素弹簧钢丝 IV 组，sigma_b=1100MPa (d=3.0~6.0mm)"
            },

            // 65Mn 弹簧钢
            new SpringMaterial
            {
                Name = "65Mn",
                G = 79000,
                E = 206000,
                TauI = 340,
                TauII = 590,
                TauIII = 785,
                SigmaB = 980,
                SigmaUlt = 980,
                WireDiaMin = 1.0,
                WireDiaMax = 16.0,
                Description = "65Mn弹簧钢，淬火回火后使用，适用于一般弹簧"
            },

            // 60Si2Mn 弹簧钢
            new SpringMaterial
            {
                Name = "60Si2Mn",
                G = 79000,
                E = 206000,
                TauI = 445,
                TauII = 740,
                TauIII = 980,
                SigmaB = 1225,
                SigmaUlt = 1225,
                WireDiaMin = 1.0,
                WireDiaMax = 25.0,
                Description = "60Si2Mn弹簧钢，高应力弹簧，适用于汽车悬挂弹簧等"
            },

            // 50CrVA 弹簧钢
            new SpringMaterial
            {
                Name = "50CrVA",
                G = 79000,
                E = 206000,
                TauI = 445,
                TauII = 590,
                TauIII = 785,
                SigmaB = 980,
                SigmaUlt = 1275,
                WireDiaMin = 1.0,
                WireDiaMax = 50.0,
                Description = "50CrVA弹簧钢，高疲劳性能，适用于气门弹簧等"
            },

            // 不锈钢丝
            new SpringMaterial
            {
                Name = "不锈钢丝 (1Cr18Ni9)",
                G = 71500,
                E = 193000,
                TauI = 265,
                TauII = 440,
                TauIII = 590,
                SigmaB = 735,
                SigmaUlt = 735,
                WireDiaMin = 0.1,
                WireDiaMax = 12.0,
                Description = "不锈钢弹簧丝，耐腐蚀，适用于化工、食品等环境"
            },

            // 硅锰弹簧钢丝
            new SpringMaterial
            {
                Name = "60Si2MnA 钢丝",
                G = 79000,
                E = 206000,
                TauI = 445,
                TauII = 740,
                TauIII = 980,
                SigmaB = 1225,
                SigmaUlt = 1570,
                WireDiaMin = 1.0,
                WireDiaMax = 14.0,
                Description = "60Si2MnA弹簧钢丝，油淬火回火钢丝"
            },

            // 铬钒弹簧钢丝
            new SpringMaterial
            {
                Name = "50CrVA 钢丝",
                G = 79000,
                E = 206000,
                TauI = 445,
                TauII = 590,
                TauIII = 785,
                SigmaB = 980,
                SigmaUlt = 1470,
                WireDiaMin = 0.5,
                WireDiaMax = 14.0,
                Description = "50CrVA弹簧钢丝，油淬火回火钢丝，高疲劳寿命"
            },

            // 磷青铜丝
            new SpringMaterial
            {
                Name = "磷青铜丝 (QSn4-3)",
                G = 40200,
                E = 93000,
                TauI = 185,
                TauII = 310,
                TauIII = 410,
                SigmaB = 510,
                SigmaUlt = 510,
                WireDiaMin = 0.1,
                WireDiaMax = 6.0,
                Description = "磷青铜弹簧丝，导电性好，适用于电器弹簧"
            },

            // 铍青铜丝
            new SpringMaterial
            {
                Name = "铍青铜丝 (QBe2)",
                G = 46000,
                E = 110000,
                TauI = 240,
                TauII = 400,
                TauIII = 530,
                SigmaB = 660,
                SigmaUlt = 660,
                WireDiaMin = 0.1,
                WireDiaMax = 6.0,
                Description = "铍青铜弹簧丝，高强度、高弹性，适用于精密仪器"
            }
        };

        /// <summary>
        /// 获取所有材料
        /// </summary>
        public static List<SpringMaterial> GetAllMaterials()
        {
            return new List<SpringMaterial>(_materials);
        }

        /// <summary>
        /// 根据名称获取材料
        /// </summary>
        public static SpringMaterial GetMaterial(string name)
        {
            return _materials.Find(m => m.Name == name);
        }

        /// <summary>
        /// 根据载荷类别获取许用切应力
        /// </summary>
        /// <param name="material">材料</param>
        /// <param name="loadCategory">载荷类别: 1=I类, 2=II类, 3=III类</param>
        public static double GetAllowableShearStress(SpringMaterial material, int loadCategory)
        {
            switch (loadCategory)
            {
                case 1: return material.TauI;
                case 2: return material.TauII;
                case 3: return material.TauIII;
                default: throw new ArgumentException("载荷类别必须为 1, 2 或 3");
            }
        }

        /// <summary>
        /// 根据载荷类别获取扭转弹簧许用弯曲应力
        /// </summary>
        /// <param name="material">材料</param>
        /// <param name="loadCategory">载荷类别: 1=I类, 2=II类, 3=III类</param>
        public static double GetAllowableBendingStress(SpringMaterial material, int loadCategory)
        {
            // 弯曲许用应力约为拉伸强度的 0.5~0.75 倍，按载荷类别递增
            switch (loadCategory)
            {
                case 1: return material.SigmaB * 0.5;
                case 2: return material.SigmaB * 0.65;
                case 3: return material.SigmaB * 0.80;
                default: throw new ArgumentException("载荷类别必须为 1, 2 或 3");
            }
        }
    }
}
