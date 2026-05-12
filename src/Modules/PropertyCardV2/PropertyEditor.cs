using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace PropertyCardV2
{
    /// <summary>
    /// SolidWorks 自定义属性读写器，通过 COM Interop 操作 SolidWorks 文件属性。
    /// </summary>
    public class PropertyEditor
    {
        private static readonly Dictionary<string, int> SwFileTypes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { ".sldprt", 1 },  // swDocPART
            { ".sldasm", 2 },  // swDocASSEMBLY
            { ".slddrw", 3 },  // swDocDRAWING
        };

        /// <summary>
        /// 读取 SolidWorks 文件的自定义属性。
        /// </summary>
        public Dictionary<string, string> ReadProperties(string filePath)
        {
            var result = new Dictionary<string, string>();
            if (!File.Exists(filePath))
                throw new FileNotFoundException("文件不存在", filePath);

            dynamic swApp = null;
            dynamic swModel = null;
            try
            {
                swApp = GetSolidWorksApp();
                if (swApp == null)
                    throw new InvalidOperationException("无法连接到 SolidWorks 应用程序。请确保 SolidWorks 已启动。");

                int fileError = 0;
                int fileWarning = 0;
                swModel = swApp.OpenDoc6(filePath, GetFileType(filePath), 0, "", ref fileError, ref fileWarning);
                if (swModel == null)
                    throw new InvalidOperationException($"无法打开文件: {filePath}");

                dynamic customPropMgr = swModel.Extension.CustomPropertyManager[""];
                if (customPropMgr != null)
                {
                    string[] propNames = (string[])customPropMgr.GetNames();
                    if (propNames != null)
                    {
                        foreach (string name in propNames)
                        {
                            string val = "";
                            string resolvedVal = "";
                            customPropMgr.Get2(name, out val, out resolvedVal);
                            result[name] = resolvedVal ?? val ?? "";
                        }
                    }
                }

                swApp.CloseDoc(filePath);
            }
            catch (COMException ex)
            {
                throw new InvalidOperationException($"COM 调用失败: {ex.Message}", ex);
            }
            finally
            {
                if (swModel != null) Marshal.ReleaseComObject(swModel);
                if (swApp != null) Marshal.ReleaseComObject(swApp);
            }
            return result;
        }

        /// <summary>
        /// 写入 SolidWorks 文件的自定义属性。
        /// </summary>
        public void WriteProperties(string filePath, Dictionary<string, string> properties)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("文件不存在", filePath);

            dynamic swApp = null;
            dynamic swModel = null;
            try
            {
                swApp = GetSolidWorksApp();
                if (swApp == null)
                    throw new InvalidOperationException("无法连接到 SolidWorks 应用程序。请确保 SolidWorks 已启动。");

                int fileError = 0;
                int fileWarning = 0;
                swModel = swApp.OpenDoc6(filePath, GetFileType(filePath), 0, "", ref fileError, ref fileWarning);
                if (swModel == null)
                    throw new InvalidOperationException($"无法打开文件: {filePath}");

                dynamic customPropMgr = swModel.Extension.CustomPropertyManager[""];
                if (customPropMgr != null)
                {
                    // 30 = swCustomPropertyReplaceAndOverwriteExisting
                    // 33 = swCustomPropertyDeleteAndAdd
                    foreach (var kv in properties)
                    {
                        customPropMgr.Set3(kv.Key, kv.Value, 30);
                    }
                }

                swModel.Save();
                swApp.CloseDoc(filePath);
            }
            catch (COMException ex)
            {
                throw new InvalidOperationException($"COM 调用失败: {ex.Message}", ex);
            }
            finally
            {
                if (swModel != null) Marshal.ReleaseComObject(swModel);
                if (swApp != null) Marshal.ReleaseComObject(swApp);
            }
        }

        private dynamic GetSolidWorksApp()
        {
            try
            {
                return Marshal.GetActiveObject("SldWorks.Application");
            }
            catch (COMException)
            {
                return null;
            }
        }

        private int GetFileType(string filePath)
        {
            string ext = Path.GetExtension(filePath);
            if (SwFileTypes.TryGetValue(ext, out int type))
                return type;
            return 1; // 默认为零件
        }
    }
}
