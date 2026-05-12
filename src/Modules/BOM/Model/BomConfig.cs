using System.Collections.Generic;

namespace McBom.Model
{
    /// <summary>
    /// BOM配置
    /// </summary>
    public class BomConfig
    {
        /// <summary>是否隐藏标记为IsHidden的项目</summary>
        public bool HideHiddenItems { get; set; } = true;

        /// <summary>是否自动计算重量</summary>
        public bool AutoCalculateWeight { get; set; } = true;

        /// <summary>是否包含子装配体中的零件</summary>
        public bool IncludeSubAssemblies { get; set; } = true;

        /// <summary>默认材料密度 (kg/m3), 钢=7850, 铝=2700</summary>
        public double DefaultDensity { get; set; } = 7850;

        /// <summary>序号前缀</summary>
        public string ItemPrefix { get; set; } = "";

        /// <summary>Excel导出模板路径</summary>
        public string ExcelTemplatePath { get; set; }

        /// <summary>PDF导出模板路径</summary>
        public string PdfTemplatePath { get; set; }

        /// <summary>要提取的自定义属性名列表</summary>
        public List<string> CustomPropertyNames { get; set; } = new List<string>
        {
            "零件号", "名称", "材料", "重量", "备注"
        };

        /// <summary>SolidWorks属性名到BOM字段的映射</summary>
        public Dictionary<string, string> PropertyMapping { get; set; } = new Dictionary<string, string>
        {
            { "SW-零件号", "PartNo" },
            { "零件号", "PartNo" },
            { "PartNo", "PartNo" },
            { "SW-文件名称", "Name" },
            { "名称", "Name" },
            { "Material", "Material" },
            { "材料", "Material" },
            { "Weight", "UnitWeight" },
            { "重量", "UnitWeight" },
            { "备注", "Remark" },
            { "Remark", "Remark" }
        };

        /// <summary>材料密度表 (材料名 -> 密度 kg/m3)</summary>
        public Dictionary<string, double> MaterialDensityTable { get; set; } = new Dictionary<string, double>
        {
            { "碳钢", 7850 },
            { "Q235", 7850 },
            { "Q345", 7850 },
            { "45钢", 7850 },
            { "不锈钢", 7930 },
            { "304不锈钢", 7930 },
            { "316不锈钢", 7980 },
            { "铝合金", 2700 },
            { "6061铝合金", 2700 },
            { "7075铝合金", 2810 },
            { "铜", 8900 },
            { "黄铜", 8500 },
            { "铸铁", 7200 },
            { "塑料", 1200 },
            { "ABS", 1050 },
            { "尼龙", 1140 }
        };
    }
}
