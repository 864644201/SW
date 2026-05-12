using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace MaidouBao.Plugin
{
    /// <summary>
    /// 插件管理器，负责扫描、加载和实例化插件。
    /// </summary>
    public class PluginManager
    {
        /// <summary>
        /// 扫描指定目录下的所有子目录，查找 Plugin.xml 文件并解析为 PluginInfo 列表。
        /// </summary>
        /// <param name="dataDir">插件根目录路径。</param>
        /// <returns>解析得到的插件信息列表。</returns>
        public static List<PluginInfo> LoadPlugins(string dataDir)
        {
            var plugins = new List<PluginInfo>();

            if (string.IsNullOrEmpty(dataDir) || !Directory.Exists(dataDir))
            {
                return plugins;
            }

            foreach (string subDir in Directory.GetDirectories(dataDir))
            {
                string pluginXmlPath = Path.Combine(subDir, "Plugin.xml");

                if (!File.Exists(pluginXmlPath))
                {
                    continue;
                }

                try
                {
                    PluginInfo info = ParsePluginXml(pluginXmlPath);
                    if (info != null)
                    {
                        info.DirPath = subDir;
                        plugins.Add(info);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"解析插件配置文件失败: {pluginXmlPath}, 错误: {ex.Message}");
                }
            }

            return plugins;
        }

        /// <summary>
        /// 根据插件元数据加载程序集并创建插件实例。
        /// </summary>
        /// <param name="info">插件元数据信息。</param>
        /// <returns>创建的插件实例。</returns>
        public IMdbPlugin CreatePlugin(PluginInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (string.IsNullOrEmpty(info.ClassName))
            {
                throw new ArgumentException("插件类名不能为空。", nameof(info));
            }

            // 从 ClassName 中提取程序集名称（取命名空间部分）
            string assemblyName = info.ClassName;
            int dotIndex = info.ClassName.LastIndexOf('.');
            if (dotIndex > 0)
            {
                assemblyName = info.ClassName.Substring(0, dotIndex);
            }

            Assembly assembly = Assembly.Load(assemblyName);
            Type pluginType = assembly.GetType(info.ClassName);

            if (pluginType == null)
            {
                throw new TypeLoadException(
                    $"无法在程序集 '{assemblyName}' 中找到类型 '{info.ClassName}'。");
            }

            object instance = Activator.CreateInstance(pluginType);

            if (instance is IMdbPlugin plugin)
            {
                return plugin;
            }

            throw new InvalidCastException(
                $"类型 '{info.ClassName}' 未实现 IMdbPlugin 接口。");
        }

        /// <summary>
        /// 解析 Plugin.xml 文件为 PluginInfo 对象。
        /// </summary>
        private static PluginInfo ParsePluginXml(string xmlPath)
        {
            var doc = new XmlDocument();
            doc.Load(xmlPath);

            XmlNode rootNode = doc.SelectSingleNode("Plugin");
            if (rootNode == null)
            {
                return null;
            }

            var info = new PluginInfo
            {
                Name = GetNodeText(rootNode, "name"),
                ClassName = GetNodeText(rootNode, "classname"),
                ClassId = GetNodeText(rootNode, "classid"),
                Version = GetNodeText(rootNode, "version"),
                SoftKey = GetNodeText(rootNode, "softkey"),
                UserName = GetNodeText(rootNode, "username"),
                UserId = GetNodeText(rootNode, "userid"),
                StartExe = GetNodeText(rootNode, "startexe"),
                Explain = GetNodeText(rootNode, "explain")
            };

            string isMdText = GetNodeText(rootNode, "isMd");
            if (bool.TryParse(isMdText, out bool isMd))
            {
                info.IsMd = isMd;
            }

            return info;
        }

        /// <summary>
        /// 安全获取 XML 节点的文本内容。
        /// </summary>
        private static string GetNodeText(XmlNode parent, string childName)
        {
            XmlNode node = parent.SelectSingleNode(childName);
            return node?.InnerText?.Trim() ?? string.Empty;
        }
    }
}
