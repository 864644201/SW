namespace MaidouBao.Plugin
{
    /// <summary>
    /// 插件分类枚举，定义了麦豆宝系统中的主要插件类别。
    /// </summary>
    public enum PluginCategory
    {
        /// <summary>
        /// 设计工具，分类标识 2090_2219_2221，软键前缀 02xx。
        /// </summary>
        DesignTools = 1,

        /// <summary>
        /// 二维工具，分类标识 2090_2219_2222，软键前缀 03xx。
        /// </summary>
        DraftingTools = 2,

        /// <summary>
        /// 常用工具，分类标识 2090_2219_2223，软键前缀 04xx。
        /// </summary>
        CommonTools = 3,

        /// <summary>
        /// 表格工具大全，分类标识 2090_2219_2224。
        /// </summary>
        TableTools = 4
    }
}
