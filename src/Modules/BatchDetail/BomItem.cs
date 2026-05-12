namespace BatchDetail
{
    /// <summary>
    /// Represents a single row in a Bill of Materials extracted from a SolidWorks assembly.
    /// </summary>
    public class BomItem
    {
        /// <summary>零件号</summary>
        public string PartNo { get; set; } = string.Empty;

        /// <summary>名称</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>材料</summary>
        public string Material { get; set; } = string.Empty;

        /// <summary>数量</summary>
        public int Quantity { get; set; } = 1;

        /// <summary>重量 (kg)</summary>
        public double Weight { get; set; }

        /// <summary>文件名</summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>文件完整路径</summary>
        public string FilePath { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{PartNo} - {Name} x{Quantity}";
        }
    }
}
