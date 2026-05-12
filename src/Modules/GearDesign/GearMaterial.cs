using System;
using System.Collections.Generic;

namespace GearDesign
{
    /// <summary>
    /// 齿轮材料定义 - 包含接触疲劳极限、弯曲疲劳极限、弹性模量等参数
    /// </summary>
    public class GearMaterial
    {
        /// <summary>材料名称</summary>
        public string Name { get; set; }

        /// <summary>热处理方式</summary>
        public string HeatTreatment { get; set; }

        /// <summary>接触疲劳极限 sigma_Hlim (MPa)</summary>
        public double SigmaHlim { get; set; }

        /// <summary>弯曲疲劳极限 sigma_Flim (MPa)</summary>
        public double SigmaFlim { get; set; }

        /// <summary>弹性模量 E (MPa)</summary>
        public double ElasticModulus { get; set; }

        /// <summary>泊松比 mu</summary>
        public double PoissonRatio { get; set; }

        /// <summary>硬度值 (HBS 或 HRC)</summary>
        public string Hardness { get; set; }

        /// <summary>硬度数值 (用于寿命系数查表)</summary>
        public double HardnessValue { get; set; }

        /// <summary>硬度类型: HBS 或 HRC</summary>
        public string HardnessType { get; set; }

        public override string ToString()
        {
            return $"{Name} ({HeatTreatment})";
        }

        /// <summary>
        /// 获取预定义的齿轮材料列表
        /// 依据 GB/T 3480-1997 / ISO 6336 标准
        /// </summary>
        public static List<GearMaterial> GetPredefinedMaterials()
        {
            return new List<GearMaterial>
            {
                // 调质钢
                new GearMaterial
                {
                    Name = "45钢", HeatTreatment = "调质",
                    SigmaHlim = 550, SigmaFlim = 220,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "210 HBS", HardnessValue = 210, HardnessType = "HBS"
                },
                new GearMaterial
                {
                    Name = "45钢", HeatTreatment = "调质+表面淬火",
                    SigmaHlim = 1100, SigmaFlim = 350,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "48 HRC", HardnessValue = 48, HardnessType = "HRC"
                },
                new GearMaterial
                {
                    Name = "40Cr", HeatTreatment = "调质",
                    SigmaHlim = 650, SigmaFlim = 270,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "260 HBS", HardnessValue = 260, HardnessType = "HBS"
                },
                new GearMaterial
                {
                    Name = "40Cr", HeatTreatment = "调质+表面淬火",
                    SigmaHlim = 1200, SigmaFlim = 380,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "52 HRC", HardnessValue = 52, HardnessType = "HRC"
                },
                new GearMaterial
                {
                    Name = "35SiMn", HeatTreatment = "调质",
                    SigmaHlim = 620, SigmaFlim = 260,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "250 HBS", HardnessValue = 250, HardnessType = "HBS"
                },
                new GearMaterial
                {
                    Name = "42CrMo", HeatTreatment = "调质",
                    SigmaHlim = 700, SigmaFlim = 300,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "280 HBS", HardnessValue = 280, HardnessType = "HBS"
                },
                new GearMaterial
                {
                    Name = "42CrMo", HeatTreatment = "调质+表面淬火",
                    SigmaHlim = 1250, SigmaFlim = 400,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "54 HRC", HardnessValue = 54, HardnessType = "HRC"
                },

                // 渗碳钢
                new GearMaterial
                {
                    Name = "20Cr", HeatTreatment = "渗碳淬火",
                    SigmaHlim = 1350, SigmaFlim = 420,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "58 HRC", HardnessValue = 58, HardnessType = "HRC"
                },
                new GearMaterial
                {
                    Name = "20CrMnTi", HeatTreatment = "渗碳淬火",
                    SigmaHlim = 1500, SigmaFlim = 460,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "60 HRC", HardnessValue = 60, HardnessType = "HRC"
                },
                new GearMaterial
                {
                    Name = "20CrMnMo", HeatTreatment = "渗碳淬火",
                    SigmaHlim = 1500, SigmaFlim = 460,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "60 HRC", HardnessValue = 60, HardnessType = "HRC"
                },
                new GearMaterial
                {
                    Name = "18Cr2Ni4WA", HeatTreatment = "渗碳淬火",
                    SigmaHlim = 1550, SigmaFlim = 480,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "60 HRC", HardnessValue = 60, HardnessType = "HRC"
                },
                new GearMaterial
                {
                    Name = "20CrNiMo", HeatTreatment = "渗碳淬火",
                    SigmaHlim = 1400, SigmaFlim = 440,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "58 HRC", HardnessValue = 58, HardnessType = "HRC"
                },

                // 氮化钢
                new GearMaterial
                {
                    Name = "38CrMoAlA", HeatTreatment = "氮化",
                    SigmaHlim = 1000, SigmaFlim = 340,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "850 HV", HardnessValue = 850, HardnessType = "HV"
                },
                new GearMaterial
                {
                    Name = "30CrMoSiA", HeatTreatment = "氮化",
                    SigmaHlim = 950, SigmaFlim = 320,
                    ElasticModulus = 206000, PoissonRatio = 0.3,
                    Hardness = "800 HV", HardnessValue = 800, HardnessType = "HV"
                },

                // 铸钢
                new GearMaterial
                {
                    Name = "ZG310-570", HeatTreatment = "正火",
                    SigmaHlim = 450, SigmaFlim = 180,
                    ElasticModulus = 202000, PoissonRatio = 0.3,
                    Hardness = "160 HBS", HardnessValue = 160, HardnessType = "HBS"
                },
                new GearMaterial
                {
                    Name = "ZG340-640", HeatTreatment = "正火",
                    SigmaHlim = 500, SigmaFlim = 200,
                    ElasticModulus = 202000, PoissonRatio = 0.3,
                    Hardness = "180 HBS", HardnessValue = 180, HardnessType = "HBS"
                },

                // 铸铁
                new GearMaterial
                {
                    Name = "HT250", HeatTreatment = "退火",
                    SigmaHlim = 320, SigmaFlim = 120,
                    ElasticModulus = 118000, PoissonRatio = 0.3,
                    Hardness = "180 HBS", HardnessValue = 180, HardnessType = "HBS"
                },
                new GearMaterial
                {
                    Name = "HT300", HeatTreatment = "退火",
                    SigmaHlim = 350, SigmaFlim = 130,
                    ElasticModulus = 118000, PoissonRatio = 0.3,
                    Hardness = "200 HBS", HardnessValue = 200, HardnessType = "HBS"
                },
                new GearMaterial
                {
                    Name = "QT500-7", HeatTreatment = "退火",
                    SigmaHlim = 420, SigmaFlim = 160,
                    ElasticModulus = 173000, PoissonRatio = 0.3,
                    Hardness = "180 HBS", HardnessValue = 180, HardnessType = "HBS"
                },
                new GearMaterial
                {
                    Name = "QT600-3", HeatTreatment = "正火",
                    SigmaHlim = 480, SigmaFlim = 190,
                    ElasticModulus = 173000, PoissonRatio = 0.3,
                    Hardness = "220 HBS", HardnessValue = 220, HardnessType = "HBS"
                },

                // 塑料/非金属
                new GearMaterial
                {
                    Name = "尼龙 (PA)", HeatTreatment = "-",
                    SigmaHlim = 80, SigmaFlim = 40,
                    ElasticModulus = 2800, PoissonRatio = 0.4,
                    Hardness = "-", HardnessValue = 0, HardnessType = "-"
                },
                new GearMaterial
                {
                    Name = "聚甲醛 (POM)", HeatTreatment = "-",
                    SigmaHlim = 70, SigmaFlim = 35,
                    ElasticModulus = 2600, PoissonRatio = 0.4,
                    Hardness = "-", HardnessValue = 0, HardnessType = "-"
                },
                new GearMaterial
                {
                    Name = "夹布胶木", HeatTreatment = "-",
                    SigmaHlim = 100, SigmaFlim = 50,
                    ElasticModulus = 7800, PoissonRatio = 0.35,
                    Hardness = "-", HardnessValue = 0, HardnessType = "-"
                }
            };
        }
    }
}
