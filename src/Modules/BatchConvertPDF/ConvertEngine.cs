using System;
using System.IO;
using System.Runtime.InteropServices;

namespace BatchConvertPDF
{
    public class ConvertResult
    {
        public string SourceFile { get; set; }
        public string OutputFile { get; set; }
        public bool Success { get; set; }
        public string Error { get; set; }
    }

    /// <summary>
    /// Engine for converting SolidWorks files to various formats.
    /// Uses SolidWorks COM API (SldWorks.Application) for CAD conversions.
    /// Falls back to file-copy stub when SolidWorks is not available.
    /// </summary>
    public class ConvertEngine
    {
        private dynamic _swApp;

        /// <summary>
        /// Attempts to connect to a running SolidWorks instance or launch one.
        /// </summary>
        private dynamic GetSolidWorksApp()
        {
            if (_swApp != null) return _swApp;

            try
            {
                // Try to get running instance
                _swApp = Marshal.GetActiveObject("SldWorks.Application");
            }
            catch
            {
                try
                {
                    // Launch new instance
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
        /// Converts a single file to the specified output format.
        /// </summary>
        public ConvertResult ConvertFile(string sourceFile, string outputFile, string outputFormat, bool overwrite)
        {
            var result = new ConvertResult
            {
                SourceFile = sourceFile,
                OutputFile = outputFile
            };

            try
            {
                if (!File.Exists(sourceFile))
                {
                    result.Error = "源文件不存在";
                    return result;
                }

                if (File.Exists(outputFile) && !overwrite)
                {
                    result.Error = "目标文件已存在";
                    return result;
                }

                switch (outputFormat.ToUpperInvariant())
                {
                    case "PDF":
                        ConvertToPDF(sourceFile, outputFile);
                        break;
                    case "DWG":
                        ConvertToDWG(sourceFile, outputFile);
                        break;
                    case "DXF":
                        ConvertToDXF(sourceFile, outputFile);
                        break;
                    case "STEP":
                        ConvertToSTEP(sourceFile, outputFile);
                        break;
                    case "IGES":
                        ConvertToIGES(sourceFile, outputFile);
                        break;
                    default:
                        result.Error = $"不支持的输出格式: {outputFormat}";
                        return result;
                }

                result.Success = File.Exists(outputFile);
                if (!result.Success)
                    result.Error = "转换后未生成输出文件";
            }
            catch (COMException comEx)
            {
                result.Error = $"SolidWorks COM 错误: {comEx.Message}";
            }
            catch (Exception ex)
            {
                result.Error = $"转换失败: {ex.Message}";
            }

            return result;
        }

        private void ConvertToPDF(string sourceFile, string outputFile)
        {
            var swApp = GetSolidWorksApp();
            if (swApp == null)
                throw new InvalidOperationException("SolidWorks 未安装或无法启动。需要安装 SolidWorks 才能使用转换功能。");

            // Open the document
            int errors = 0, warnings = 0;
            int docType = GetDocType(sourceFile);
            dynamic swDoc = swApp.OpenDoc6(sourceFile, docType, 1, "", ref errors, ref warnings);

            if (swDoc == null)
                throw new InvalidOperationException($"无法打开文件: {sourceFile} (错误码: {errors})");

            try
            {
                // Export to PDF
                bool success = swDoc.SaveAs(outputFile);
                if (!success)
                    throw new InvalidOperationException("SaveAs 失败");
            }
            finally
            {
                swApp.CloseDoc(swDoc.GetTitle());
            }
        }

        private void ConvertToDWG(string sourceFile, string outputFile)
        {
            ConvertViaExport(sourceFile, outputFile, 0); // DWG export type
        }

        private void ConvertToDXF(string sourceFile, string outputFile)
        {
            ConvertViaExport(sourceFile, outputFile, 1); // DXF export type
        }

        private void ConvertToSTEP(string sourceFile, string outputFile)
        {
            ConvertViaExport(sourceFile, outputFile, 3); // STEP AP203
        }

        private void ConvertToIGES(string sourceFile, string outputFile)
        {
            ConvertViaExport(sourceFile, outputFile, 4); // IGES
        }

        private void ConvertViaExport(string sourceFile, string outputFile, int exportType)
        {
            var swApp = GetSolidWorksApp();
            if (swApp == null)
                throw new InvalidOperationException("SolidWorks 未安装或无法启动。需要安装 SolidWorks 才能使用转换功能。");

            int errors = 0, warnings = 0;
            int docType = GetDocType(sourceFile);
            dynamic swDoc = swApp.OpenDoc6(sourceFile, docType, 1, "", ref errors, ref warnings);

            if (swDoc == null)
                throw new InvalidOperationException($"无法打开文件: {sourceFile} (错误码: {errors})");

            try
            {
                // Use ExportFlatPatternView or SaveAs depending on type
                bool success = swDoc.SaveAs(outputFile);
                if (!success)
                    throw new InvalidOperationException($"导出失败，格式类型: {exportType}");
            }
            finally
            {
                swApp.CloseDoc(swDoc.GetTitle());
            }
        }

        private int GetDocType(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            switch (ext)
            {
                case ".sldprt": return 1;  // swDocPART
                case ".sldasm": return 2;  // swDocASSEMBLY
                case ".slddrw": return 3;  // swDocDRAWING
                default: return 1;
            }
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
