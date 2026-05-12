namespace MaidouBao.Plugin
{
    /// <summary>
    /// 插件元数据信息，从 Plugin.xml 文件中解析。
    /// </summary>
    public class PluginInfo
    {
        /// <summary>
        /// 模块名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 插件类的完整名称（命名空间.类名）。
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 插件分类标识，如 2090_2219_2223。
        /// </summary>
        public string ClassId { get; set; }

        /// <summary>
        /// 插件版本号。
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 软件密钥，用于区分插件类别，如 0401。
        /// </summary>
        public string SoftKey { get; set; }

        /// <summary>
        /// 所属用户名或产品名。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户标识。
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 启动的可执行文件名。
        /// </summary>
        public string StartExe { get; set; }

        /// <summary>
        /// 是否为麦迪设计宝模块。
        /// </summary>
        public bool IsMd { get; set; }

        /// <summary>
        /// 模块说明。
        /// </summary>
        public string Explain { get; set; }

        /// <summary>
        /// 模块所在目录的完整路径。
        /// </summary>
        public string DirPath { get; set; }
    }
}
