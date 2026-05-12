using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace MD_SW_ConnectSW
{
    /// <summary>
    /// SolidWorks 版本信息
    /// </summary>
    public class SwVersionInfo
    {
        /// <summary>
        /// SolidWorks 主版本号（如 30 表示 SW2022）
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// SolidWorks 安装路径
        /// </summary>
        public string InstallPath { get; set; }

        /// <summary>
        /// SolidWorks 语言名称
        /// </summary>
        public string LanguageName { get; set; }

        /// <summary>
        /// ProgID，例如 "SldWorks.Application.30"
        /// </summary>
        public string ProgID => $"SldWorks.Application.{Version}";

        public SwVersionInfo()
        {
        }

        public SwVersionInfo(int version, string installPath, string languageName)
        {
            Version = version;
            InstallPath = installPath;
            LanguageName = languageName;
        }

        public override string ToString()
        {
            return $"SolidWorks {Version} ({LanguageName}) - {InstallPath}";
        }
    }
}
