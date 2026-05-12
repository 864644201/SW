using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ReplaceDrawingTemplate
{
    public class TemplateInfo
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Version { get; set; }
        public string SheetSize { get; set; }
    }

    /// <summary>
    /// Handles replacing the drawing template (sheet format) in SolidWorks drawing files.
    /// Uses SolidWorks COM API to open drawings and swap their template.
    /// </summary>
    public class TemplateReplacer
    {
        private dynamic _swApp;

        /// <summary>
        /// Connects to or launches SolidWorks.
        /// </summary>
        private dynamic GetSolidWorksApp()
        {
            if (_swApp != null) return _swApp;

            try
            {
                _swApp = Marshal.GetActiveObject("SldWorks.Application");
            }
            catch
            {
                try
                {
                    var type = Type.GetTypeFromProgID("SldWorks.Application");
                    if (type != null)
                    {
                        _swApp = Activator.CreateInstance(type);
                        _swApp.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Cannot create SolidWorks instance: {ex.Message}");
                }
            }

            return _swApp;
        }

        /// <summary>
        /// Gets basic template information (version, sheet size) from a template file.
        /// </summary>
        public TemplateInfo GetTemplateInfo(string templatePath)
        {
            if (!File.Exists(templatePath)) return null;

            var info = new TemplateInfo
            {
                FilePath = templatePath,
                FileName = Path.GetFileName(templatePath)
            };

            try
            {
                var swApp = GetSolidWorksApp();
                if (swApp != null)
                {
                    int errors = 0, warnings = 0;
                    int docType = templatePath.EndsWith(".drwdot", StringComparison.OrdinalIgnoreCase) ? 3 : 3;
                    dynamic swDoc = swApp.OpenDoc6(templatePath, docType, 1, "", ref errors, ref warnings);

                    if (swDoc != null)
                    {
                        try
                        {
                            info.Version = swDoc.Version.ToString();
                            dynamic sheet = swDoc.GetCurrentSheet();
                            if (sheet != null)
                            {
                                double[] props = sheet.GetProperties();
                                if (props != null && props.Length >= 2)
                                {
                                    double width = props[0] * 1000;
                                    double height = props[1] * 1000;
                                    info.SheetSize = $"{width:F0} x {height:F0} mm";
                                }
                            }
                        }
                        finally
                        {
                            swApp.CloseDoc(swDoc.GetTitle());
                        }
                    }
                }
            }
            catch
            {
                // SolidWorks not available - return basic info from file
                info.Version = "未知";
                var fi = new FileInfo(templatePath);
                info.SheetSize = $"文件大小: {fi.Length / 1024.0:F1} KB";
            }

            return info;
        }

        /// <summary>
        /// Detects what template a drawing file is currently using.
        /// Returns the template filename, or null if detection fails.
        /// </summary>
        public string DetectCurrentTemplate(string drawingPath)
        {
            try
            {
                var swApp = GetSolidWorksApp();
                if (swApp != null)
                {
                    int errors = 0, warnings = 0;
                    dynamic swDoc = swApp.OpenDoc6(drawingPath, 3, 1, "", ref errors, ref warnings);

                    if (swDoc != null)
                    {
                        try
                        {
                            dynamic sheet = swDoc.GetCurrentSheet();
                            if (sheet != null)
                            {
                                string templatePath = sheet.GetTemplateName();
                                if (!string.IsNullOrEmpty(templatePath))
                                    return Path.GetFileName(templatePath);
                            }
                        }
                        finally
                        {
                            swApp.CloseDoc(swDoc.GetTitle());
                        }
                    }
                }
            }
            catch
            {
                // SolidWorks not available
            }

            return null;
        }

        /// <summary>
        /// Replaces the drawing template in the specified file.
        /// Returns true if the replacement was performed, false if the old template was not found/matched.
        /// </summary>
        public bool ReplaceTemplate(string drawingPath, string oldTemplatePath, string newTemplatePath)
        {
            if (!File.Exists(drawingPath))
                throw new FileNotFoundException("工程图文件不存在", drawingPath);

            if (!File.Exists(newTemplatePath))
                throw new FileNotFoundException("新模板文件不存在", newTemplatePath);

            var swApp = GetSolidWorksApp();
            if (swApp == null)
                throw new InvalidOperationException("SolidWorks 未安装或无法启动。需要安装 SolidWorks 才能使用此功能。");

            int errors = 0, warnings = 0;
            dynamic swDoc = swApp.OpenDoc6(drawingPath, 3, 1, "", ref errors, ref warnings);

            if (swDoc == null)
                throw new InvalidOperationException($"无法打开文件: {drawingPath} (错误码: {errors})");

            try
            {
                dynamic sheet = swDoc.GetCurrentSheet();
                if (sheet == null)
                    throw new InvalidOperationException("无法获取当前图纸页");

                // Check if current template matches the old template (if specified)
                if (!string.IsNullOrEmpty(oldTemplatePath))
                {
                    string currentTemplate = sheet.GetTemplateName();
                    if (string.IsNullOrEmpty(currentTemplate))
                        return false;

                    string currentName = Path.GetFileNameWithoutExtension(currentTemplate);
                    string oldName = Path.GetFileNameWithoutExtension(oldTemplatePath);

                    if (!string.Equals(currentName, oldName, StringComparison.OrdinalIgnoreCase))
                        return false; // Current template doesn't match the old template
                }

                // Get current sheet properties to preserve them
                double[] props = sheet.GetProperties();

                // Apply the new template
                bool success = sheet.SetTemplateName(newTemplatePath);
                if (!success)
                    throw new InvalidOperationException("设置新模板失败");

                // Reload the sheet format with the new template
                swDoc.ForceRebuild3(true);

                // Save the document
                swDoc.Save();

                return true;
            }
            finally
            {
                swApp.CloseDoc(swDoc.GetTitle());
            }
        }

        /// <summary>
        /// Replaces templates on all sheets in a multi-sheet drawing.
        /// </summary>
        public int ReplaceAllSheets(string drawingPath, string oldTemplatePath, string newTemplatePath)
        {
            if (!File.Exists(drawingPath))
                throw new FileNotFoundException("工程图文件不存在", drawingPath);

            var swApp = GetSolidWorksApp();
            if (swApp == null)
                throw new InvalidOperationException("SolidWorks 未安装或无法启动。");

            int errors = 0, warnings = 0;
            dynamic swDoc = swApp.OpenDoc6(drawingPath, 3, 1, "", ref errors, ref warnings);

            if (swDoc == null)
                throw new InvalidOperationException($"无法打开文件: {drawingPath} (错误码: {errors})");

            int replaced = 0;
            try
            {
                object[] sheets = swDoc.GetSheets();
                if (sheets == null) return 0;

                foreach (dynamic s in sheets)
                {
                    try
                    {
                        // Activate each sheet
                        swDoc.ActivateSheet(s.GetName());

                        string currentTemplate = s.GetTemplateName();
                        if (string.IsNullOrEmpty(currentTemplate)) continue;

                        // Check if matches old template
                        if (!string.IsNullOrEmpty(oldTemplatePath))
                        {
                            string currentName = Path.GetFileNameWithoutExtension(currentTemplate);
                            string oldName = Path.GetFileNameWithoutExtension(oldTemplatePath);
                            if (!string.Equals(currentName, oldName, StringComparison.OrdinalIgnoreCase))
                                continue;
                        }

                        s.SetTemplateName(newTemplatePath);
                        replaced++;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Sheet replace error: {ex.Message}");
                    }
                }

                if (replaced > 0)
                {
                    swDoc.ForceRebuild3(true);
                    swDoc.Save();
                }
            }
            finally
            {
                swApp.CloseDoc(swDoc.GetTitle());
            }

            return replaced;
        }

        /// <summary>
        /// Releases the SolidWorks COM object.
        /// </summary>
        public void Release()
        {
            if (_swApp != null)
            {
                try
                {
                    _swApp.ExitApp();
                    Marshal.ReleaseComObject(_swApp);
                }
                catch { }
                _swApp = null;
            }
        }
    }
}
