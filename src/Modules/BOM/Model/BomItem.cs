using System;
using System.Collections.Generic;

namespace McBom.Model
{
    /// <summary>
    /// BOM明细表数据模型 - 表示装配体中的一个零部件
    /// </summary>
    public class BomItem
    {
        /// <summary>序号</summary>
        public int ID { get; set; }

        /// <summary>零件号</summary>
        public string PartNo { get; set; }

        /// <summary>名称</summary>
        public string Name { get; set; }

        /// <summary>材料</summary>
        public string Material { get; set; }

        /// <summary>数量</summary>
        public int Quantity { get; set; } = 1;

        /// <summary>单重 (kg)</summary>
        public double UnitWeight { get; set; }

        /// <summary>总重 (kg) = 数量 * 单重</summary>
        public double TotalWeight => Quantity * UnitWeight;

        /// <summary>备注</summary>
        public string Remark { get; set; }

        /// <summary>装配层级 (0=顶层)</summary>
        public int Level { get; set; }

        /// <summary>父级ID</summary>
        public int ParentID { get; set; } = -1;

        /// <summary>是否为装配体</summary>
        public bool IsAssembly { get; set; }

        /// <summary>是否被隐藏</summary>
        public bool IsHidden { get; set; }

        /// <summary>SolidWorks模型ID (Component ID)</summary>
        public int ModelId { get; set; }

        /// <summary>文件完整路径</summary>
        public string FilePath { get; set; }

        /// <summary>配置名称</summary>
        public string Configuration { get; set; }

        /// <summary>子项集合</summary>
        public List<BomItem> Children { get; set; } = new List<BomItem>();

        /// <summary>自定义属性字典</summary>
        public Dictionary<string, string> CustomProperties { get; set; } = new Dictionary<string, string>();

        public override string ToString()
        {
            return $"{PartNo} - {Name} x{Quantity}";
        }
    }
}
