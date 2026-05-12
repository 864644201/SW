using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MD_SW_ConnectSW
{
    /// <summary>
    /// SolidWorks 连接桥接类，用于获取和管理 SolidWorks 应用程序实例
    /// </summary>
    public class ConnectSW
    {
        private static dynamic iswapp;
        private static int zzziversion;
        private static int iswlanguagename;

        /// <summary>
        /// 获取或设置当前连接的 SolidWorks 应用程序实例
        /// </summary>
        public dynamic iSwApp
        {
            get { return iswapp; }
            set { iswapp = value; }
        }

        /// <summary>
        /// 获取当前连接的 SolidWorks 版本号
        /// </summary>
        public int iSWVersion
        {
            get { return zzziversion; }
        }

        /// <summary>
        /// 获取当前连接的 SolidWorks 语言标识
        /// </summary>
        public int iSWLanguage
        {
            get { return iswlanguagename; }
        }

        /// <summary>
        /// 尝试获取正在运行的 SolidWorks 实例。
        /// 依次尝试以下 ProgID：
        ///   1. SldWorks.Application
        ///   2. SldWorks.SldWorks
        ///   3. SldWorks.Application.18 ~ SldWorks.Application.30
        /// </summary>
        /// <returns>成功获取返回 true，否则返回 false</returns>
        public static bool GetActiveSldWorks()
        {
            // 方法1：尝试通用 ProgID
            iswapp = GetRunningComObject("SldWorks.Application");
            if (iswapp != null)
            {
                zzziversion = 0;
                return true;
            }

            // 方法2：尝试备用 ProgID
            iswapp = GetRunningComObject("SldWorks.SldWorks");
            if (iswapp != null)
            {
                zzziversion = 0;
                return true;
            }

            // 方法3：依次尝试版本号 18-30
            for (int ver = 30; ver >= 18; ver--)
            {
                string progID = $"SldWorks.Application.{ver}";
                iswapp = GetRunningComObject(progID);
                if (iswapp != null)
                {
                    zzziversion = ver;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 通过 ProgID 创建新的 SolidWorks 实例
        /// </summary>
        /// <param name="progID">COM ProgID，例如 "SldWorks.Application.30"</param>
        /// <returns>创建的 SolidWorks 实例，失败返回 null</returns>
        public static dynamic GetSldWorksByProgID(string progID)
        {
            try
            {
                Type swType = Type.GetTypeFromProgID(progID);
                if (swType == null)
                    return null;

                dynamic instance = Activator.CreateInstance(swType);
                if (instance != null)
                {
                    iswapp = instance;
                    // 从 ProgID 中提取版本号
                    string[] parts = progID.Split('.');
                    if (parts.Length >= 3 && int.TryParse(parts[parts.Length - 1], out int ver))
                    {
                        zzziversion = ver;
                    }
                }
                return instance;
            }
            catch (COMException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 从注册表读取已安装的 SolidWorks 版本信息列表
        /// </summary>
        /// <returns>已安装的 SolidWorks 版本信息列表</returns>
        private static List<SwVersionInfo> GetSwVersionInfoByRegistry()
        {
            var result = new List<SwVersionInfo>();

            try
            {
                // 在 HKLM\SOFTWARE\SolidWorks 下查找已安装的版本
                using (RegistryKey swKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\SolidWorks"))
                {
                    if (swKey == null)
                        return result;

                    foreach (string subKeyName in swKey.GetSubKeyNames())
                    {
                        // 子键名称通常为版本号，如 "SOLIDWORKS 2022"
                        try
                        {
                            using (RegistryKey versionKey = swKey.OpenSubKey(subKeyName))
                            {
                                if (versionKey == null)
                                    continue;

                                string installPath = versionKey.GetValue("InstallDir") as string
                                                  ?? versionKey.GetValue("InstallPath") as string
                                                  ?? string.Empty;

                                string langValue = versionKey.GetValue("Language") as string
                                                ?? versionKey.GetValue("SWLanguage") as string
                                                ?? string.Empty;

                                // 尝试从子键名称中解析版本号
                                int version = 0;
                                string[] nameParts = subKeyName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string part in nameParts)
                                {
                                    if (int.TryParse(part, out int parsedYear) && parsedYear >= 2000)
                                    {
                                        // 年份转版本号：2018 -> 26, 2019 -> 27, 2020 -> 28, 2021 -> 29, 2022 -> 30
                                        version = parsedYear - 1992;
                                        break;
                                    }
                                }

                                if (version > 0)
                                {
                                    result.Add(new SwVersionInfo(version, installPath, ResolveLanguageName(langValue)));
                                }
                            }
                        }
                        catch
                        {
                            // 跳过无法读取的子键
                            continue;
                        }
                    }
                }

                // 备用方案：从 HKCR 检查 SldWorks.Application.XX 键
                if (result.Count == 0)
                {
                    using (RegistryKey classesRoot = Registry.ClassesRoot)
                    {
                        for (int ver = 18; ver <= 30; ver++)
                        {
                            string progIdKey = $"SldWorks.Application.{ver}";
                            using (RegistryKey key = classesRoot.OpenSubKey(progIdKey))
                            {
                                if (key != null)
                                {
                                    string clsid = null;
                                    using (RegistryKey clsidKey = key.OpenSubKey("CLSID"))
                                    {
                                        clsid = clsidKey?.GetValue(null) as string;
                                    }

                                    string installPath = string.Empty;
                                    if (!string.IsNullOrEmpty(clsid))
                                    {
                                        string clsidPath = $@"CLSID\{clsid}\LocalServer32";
                                        using (RegistryKey serverKey = Registry.ClassesRoot.OpenSubKey(clsidPath))
                                        {
                                            string serverPath = serverKey?.GetValue(null) as string;
                                            if (!string.IsNullOrEmpty(serverPath))
                                            {
                                                installPath = System.IO.Path.GetDirectoryName(serverPath.Trim('"'));
                                            }
                                        }
                                    }

                                    result.Add(new SwVersionInfo(ver, installPath, ResolveLanguageName(string.Empty)));
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                // 注册表读取失败，返回已收集的结果
            }

            return result;
        }

        /// <summary>
        /// 获取正在运行的 COM 对象，失败返回 null
        /// </summary>
        private static dynamic GetRunningComObject(string progID)
        {
            try
            {
                return Marshal.GetActiveObject(progID);
            }
            catch (COMException)
            {
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将语言代码/标识转换为可读的语言名称
        /// </summary>
        private static string ResolveLanguageName(string languageValue)
        {
            if (string.IsNullOrEmpty(languageValue))
                return "English";

            // 常见语言映射
            switch (languageValue.Trim().ToLowerInvariant())
            {
                case "chinese":
                case "chs":
                case "2052":
                case "zh-cn":
                    return "Chinese (Simplified)";
                case "cht":
                case "zh-tw":
                case "1028":
                    return "Chinese (Traditional)";
                case "english":
                case "en":
                case "1033":
                    return "English";
                case "japanese":
                case "ja":
                case "1041":
                    return "Japanese";
                case "german":
                case "de":
                case "1031":
                    return "German";
                case "french":
                case "fr":
                case "1036":
                    return "French";
                case "korean":
                case "ko":
                case "1042":
                    return "Korean";
                default:
                    return languageValue;
            }
        }
    }
}
