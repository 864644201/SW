using System.Drawing;

namespace MaidouBao.Plugin
{
    /// <summary>
    /// 麦豆宝插件接口，所有插件必须实现此接口。
    /// </summary>
    public interface IMdbPlugin
    {
        /// <summary>
        /// 插件描述信息。
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 插件的唯一标识 GUID。
        /// </summary>
        string GUID { get; }

        /// <summary>
        /// 插件图标。
        /// </summary>
        Image Icon { get; }

        /// <summary>
        /// 插件名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 插件版本号。
        /// </summary>
        string Version { get; }

        /// <summary>
        /// 加载插件时调用，用于初始化资源。
        /// </summary>
        void Load();

        /// <summary>
        /// 运行插件的主要功能。
        /// </summary>
        void Run();

        /// <summary>
        /// 卸载插件时调用，用于释放资源。
        /// </summary>
        void UnLoad();
    }
}
