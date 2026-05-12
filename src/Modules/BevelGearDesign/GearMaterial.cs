using System;
using System.Collections.Generic;

namespace BevelGearDesign
{
    /// <summary>
    /// 齿轮材料属性
    /// </summary>
    public class GearMaterial
    {
        /// <summary>
        /// 材料名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 热处理方式
        /// </summary>
        public string HeatTreatment { get; set; }

        /// <summary>
        /// 齿面硬度
        /// </summary>
        public string Hardness { get; set; }

        /// <summary>
        /// 弹性系数 ZE (MPa^0.5)
        /// </summary>
        public double ZE { get; set; }

        /// <summary>
        /// 许用接触疲劳强度 sigma_HP (MPa)
        /// </summary>
        public double SigmaHP { get; set; }

        /// <summary>
        /// 许用弯曲疲劳强度 sigma_FP (MPa)
        /// </summary>
        public double SigmaFP { get; set; }

        /// <summary>
        /// 材料牌号/代号
        /// </summary>
        public string Grade { get; set; }

        public override string ToString()
        {
            return $"{Name} ({HeatTreatment}) - {Hardness}";
        }

        /// <summary>
        /// 获取常用齿轮材料列表
        /// </summary>
        public static List<GearMaterial> GetCommonMaterials()
        {
            return new List<GearMaterial>
            {
                // 调质钢
                new GearMaterial
                {
                    Name = "45钢",
                    Grade = "45",
                    HeatTreatment = "调质",
                    Hardness = "217~255 HBS",
                    ZE = 189.8,
                    SigmaHP = 550,
                    SigmaFP = 220
                },
                new GearMaterial
                {
                    Name = "45钢",
                    Grade = "45",
                    HeatTreatment = "表面淬火",
                    Hardness = "40~50 HRC",
                    ZE = 189.8,
                    SigmaHP = 1100,
                    SigmaFP = 320
                },
                new GearMaterial
                {
                    Name = "40Cr",
                    Grade = "40Cr",
                    HeatTreatment = "调质",
                    Hardness = "241~286 HBS",
                    ZE = 189.8,
                    SigmaHP = 600,
                    SigmaFP = 250
                },
                new GearMaterial
                {
                    Name = "40Cr",
                    Grade = "40Cr",
                    HeatTreatment = "表面淬火",
                    Hardness = "48~55 HRC",
                    ZE = 189.8,
                    SigmaHP = 1150,
                    SigmaFP = 350
                },
                new GearMaterial
                {
                    Name = "35SiMn",
                    Grade = "35SiMn",
                    HeatTreatment = "调质",
                    Hardness = "229~286 HBS",
                    ZE = 189.8,
                    SigmaHP = 580,
                    SigmaFP = 240
                },
                new GearMaterial
                {
                    Name = "42SiMn",
                    Grade = "42SiMn",
                    HeatTreatment = "表面淬火",
                    Hardness = "45~55 HRC",
                    ZE = 189.8,
                    SigmaHP = 1100,
                    SigmaFP = 330
                },
                new GearMaterial
                {
                    Name = "20Cr",
                    Grade = "20Cr",
                    HeatTreatment = "渗碳淬火",
                    Hardness = "56~62 HRC",
                    ZE = 189.8,
                    SigmaHP = 1350,
                    SigmaFP = 400
                },
                new GearMaterial
                {
                    Name = "20CrMnTi",
                    Grade = "20CrMnTi",
                    HeatTreatment = "渗碳淬火",
                    Hardness = "56~62 HRC",
                    ZE = 189.8,
                    SigmaHP = 1400,
                    SigmaFP = 420
                },
                new GearMaterial
                {
                    Name = "20CrMnMo",
                    Grade = "20CrMnMo",
                    HeatTreatment = "渗碳淬火",
                    Hardness = "56~62 HRC",
                    ZE = 189.8,
                    SigmaHP = 1400,
                    SigmaFP = 420
                },
                new GearMaterial
                {
                    Name = "38CrMoAlA",
                    Grade = "38CrMoAlA",
                    HeatTreatment = "氮化",
                    Hardness = "850~1000 HV",
                    ZE = 189.8,
                    SigmaHP = 1000,
                    SigmaFP = 300
                },
                // 铸钢
                new GearMaterial
                {
                    Name = "ZG310-570",
                    Grade = "ZG310-570",
                    HeatTreatment = "正火",
                    Hardness = "156~217 HBS",
                    ZE = 189.8,
                    SigmaHP = 400,
                    SigmaFP = 160
                },
                new GearMaterial
                {
                    Name = "ZG340-640",
                    Grade = "ZG340-640",
                    HeatTreatment = "正火",
                    Hardness = "169~229 HBS",
                    ZE = 189.8,
                    SigmaHP = 450,
                    SigmaFP = 180
                },
                // 铸铁
                new GearMaterial
                {
                    Name = "HT250",
                    Grade = "HT250",
                    HeatTreatment = "去应力退火",
                    Hardness = "170~241 HBS",
                    ZE = 143.7,
                    SigmaHP = 290,
                    SigmaFP = 100
                },
                new GearMaterial
                {
                    Name = "HT300",
                    Grade = "HT300",
                    HeatTreatment = "去应力退火",
                    Hardness = "187~255 HBS",
                    ZE = 143.7,
                    SigmaHP = 310,
                    SigmaFP = 110
                },
                new GearMaterial
                {
                    Name = "QT500-7",
                    Grade = "QT500-7",
                    HeatTreatment = "正火",
                    Hardness = "170~230 HBS",
                    ZE = 181.4,
                    SigmaHP = 420,
                    SigmaFP = 150
                },
                new GearMaterial
                {
                    Name = "QT600-3",
                    Grade = "QT600-3",
                    HeatTreatment = "正火",
                    Hardness = "190~270 HBS",
                    ZE = 181.4,
                    SigmaHP = 470,
                    SigmaFP = 170
                },
                // 塑料
                new GearMaterial
                {
                    Name = "尼龙PA",
                    Grade = "PA66",
                    HeatTreatment = "-",
                    Hardness = "-",
                    ZE = 56.4,
                    SigmaHP = 40,
                    SigmaFP = 45
                },
                new GearMaterial
                {
                    Name = "聚甲醛POM",
                    Grade = "POM",
                    HeatTreatment = "-",
                    Hardness = "-",
                    ZE = 56.4,
                    SigmaHP = 35,
                    SigmaFP = 40
                }
            };
        }

        /// <summary>
        /// 钢-钢配对时的弹性系数
        /// </summary>
        public const double ZE_SteelSteel = 189.8;

        /// <summary>
        /// 钢-铸铁配对时的弹性系数
        /// </summary>
        public const double ZE_SteelCastIron = 165.4;

        /// <summary>
        /// 铸铁-铸铁配对时的弹性系数
        /// </summary>
        public const double ZE_CastIronCastIron = 143.7;

        /// <summary>
        /// 钢-青铜配对时的弹性系数
        /// </summary>
        public const double ZE_SteelBronze = 159.8;
    }
}
