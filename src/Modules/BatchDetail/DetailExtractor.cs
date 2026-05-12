using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BatchDetail
{
    /// <summary>
    /// Extracts BOM data from SolidWorks assemblies and exports to Excel/CSV.
    /// </summary>
    public class DetailExtractor : IDisposable
    {
        private dynamic _swApp;
        private bool _ownApp;

        /// <summary>
        /// Creates an extractor using an existing SolidWorks instance, or starts a new one.
        /// </summary>
        public DetailExtractor(dynamic existingApp = null)
        {
            if (existingApp != null)
            {
                _swApp = existingApp;
                _ownApp = false;
            }
            else
            {
                var swType = Type.GetTypeFromProgID("SldWorks.Application");
                if (swType == null)
                    throw new InvalidOperationException("未找到 SolidWorks。请确认已安装 SolidWorks。");
                _swApp = Activator.CreateInstance(swType);
                _swApp.Visible = false;
                _ownApp = true;
            }
        }

        /// <summary>
        /// Extracts BOM data from a SolidWorks assembly file.
        /// </summary>
        public List<BomItem> ExtractBomFromAssembly(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("装配体文件不存在。", filePath);

            string ext = Path.GetExtension(filePath).ToLower();
            if (ext != ".sldasm")
                throw new ArgumentException("仅支持 .sldasm 装配体文件。");

            int errors = 0, warnings = 0;
            dynamic doc = _swApp.OpenDoc6(filePath,
                2, // swDocASSEMBLY
                1, // swOpenDocOptions_Silent
                "", ref errors, ref warnings);

            if (doc == null)
                throw new InvalidOperationException($"无法打开文件: {filePath} (错误码: {errors})");

            try
            {
                dynamic assembly = doc;
                return ExtractBomFromDoc(doc, assembly, filePath);
            }
            finally
            {
                _swApp.CloseDoc(doc.GetTitle());
            }
        }

        private List<BomItem> ExtractBomFromDoc(dynamic doc, dynamic assembly, string filePath)
        {
            var result = new List<BomItem>();
            var components = (object[])assembly.GetComponents(false);
            if (components == null) return result;

            // Group components by filename to aggregate quantities
            var grouped = new Dictionary<string, BomInfo>(StringComparer.OrdinalIgnoreCase);

            foreach (dynamic comp in components)
            {
                string compPath = comp.GetPathName();
                if (string.IsNullOrEmpty(compPath)) continue;

                string compName = Path.GetFileNameWithoutExtension(compPath);
                string compExt = Path.GetExtension(compPath).ToLower();

                // Skip virtual components and sub-assemblies in flat BOM
                if (comp.IsVirtual()) continue;

                int qty = 1;
                if (comp.GetSuppression() != 0) // swComponentSuppressed = 0
                {
                    qty = 1; // Each instance counts as 1; we group below
                }

                if (grouped.ContainsKey(compPath))
                {
                    grouped[compPath].Quantity += qty;
                }
                else
                {
                    string material = GetMaterialProperty(comp);
                    double weight = GetMassProperty(comp, doc);

                    grouped[compPath] = new BomInfo
                    {
                        PartNo = compName,
                        Name = comp.Name2 ?? compName,
                        Material = material,
                        Quantity = qty,
                        Weight = weight,
                        FileName = Path.GetFileName(compPath),
                        FilePath = compPath
                    };
                }
            }

            // Also include the top-level assembly itself
            string topPath = filePath;
            string topName = Path.GetFileNameWithoutExtension(topPath);

            // Build final list
            result.Add(new BomItem
            {
                PartNo = topName,
                Name = doc.GetTitle(),
                Material = "",
                Quantity = 1,
                Weight = 0,
                FileName = Path.GetFileName(topPath),
                FilePath = topPath
            });

            result.AddRange(grouped.Values.Select(b => new BomItem
            {
                PartNo = b.PartNo,
                Name = b.Name,
                Material = b.Material,
                Quantity = b.Quantity,
                Weight = b.Weight,
                FileName = b.FileName,
                FilePath = b.FilePath
            }));

            return result;
        }

        private string GetMaterialProperty(dynamic comp)
        {
            try
            {
                dynamic compDoc = comp.GetModelDoc2();
                if (compDoc == null) return "";

                dynamic swFeat = compDoc.FirstFeature();
                while (swFeat != null)
                {
                    if (swFeat.GetTypeName2() == "MaterialFolder")
                    {
                        return swFeat.Name ?? "";
                    }
                    swFeat = swFeat.GetNextFeature();
                }
            }
            catch { }
            return "";
        }

        private double GetMassProperty(dynamic comp, dynamic topDoc)
        {
            try
            {
                dynamic swMass = topDoc.Extension.CreateMassProperty();
                dynamic compDoc = comp.GetModelDoc2();
                if (compDoc == null) return 0;

                dynamic compMass = compDoc.Extension.CreateMassProperty();
                return compMass?.Mass ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Exports BOM data to an Excel (.xlsx) file using COM automation.
        /// </summary>
        public static void ExportToExcel(List<BomItem> items, string outputPath)
        {
            var excelType = Type.GetTypeFromProgID("Excel.Application");
            if (excelType == null)
            {
                // Fallback: export as CSV with .xlsx extension
                ExportToCsv(items, outputPath);
                return;
            }

            dynamic excel = Activator.CreateInstance(excelType);
            try
            {
                excel.Visible = false;
                excel.DisplayAlerts = false;

                dynamic workbook = excel.Workbooks.Add();
                dynamic sheet = workbook.Sheets[1];
                sheet.Name = "BOM";

                // Headers
                string[] headers = { "序号", "零件号", "名称", "材料", "数量", "重量(kg)", "文件名", "文件路径" };
                for (int i = 0; i < headers.Length; i++)
                {
                    sheet.Cells[1, i + 1] = headers[i];
                    sheet.Cells[1, i + 1].Font.Bold = true;
                }

                // Data rows
                for (int row = 0; row < items.Count; row++)
                {
                    var item = items[row];
                    sheet.Cells[row + 2, 1] = row + 1;
                    sheet.Cells[row + 2, 2] = item.PartNo;
                    sheet.Cells[row + 2, 3] = item.Name;
                    sheet.Cells[row + 2, 4] = item.Material;
                    sheet.Cells[row + 2, 5] = item.Quantity;
                    sheet.Cells[row + 2, 6] = item.Weight;
                    sheet.Cells[row + 2, 7] = item.FileName;
                    sheet.Cells[row + 2, 8] = item.FilePath;
                }

                // Auto-fit columns
                sheet.Columns.AutoFit();

                // Save
                string absPath = Path.GetFullPath(outputPath);
                workbook.SaveAs(absPath, 51); // 51 = xlsx format
                workbook.Close(false);
            }
            finally
            {
                excel.Quit();
                Marshal.ReleaseComObject(excel);
            }
        }

        /// <summary>
        /// Exports BOM data to a CSV file (UTF-8 with BOM).
        /// </summary>
        public static void ExportToCsv(List<BomItem> items, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath, false, new UTF8Encoding(true)))
            {
                // Header
                writer.WriteLine("序号,零件号,名称,材料,数量,重量(kg),文件名,文件路径");

                // Rows
                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    writer.WriteLine($"{i + 1},{CsvEscape(item.PartNo)},{CsvEscape(item.Name)}," +
                                     $"{CsvEscape(item.Material)},{item.Quantity},{item.Weight:F4}," +
                                     $"{CsvEscape(item.FileName)},{CsvEscape(item.FilePath)}");
                }
            }
        }

        private static string CsvEscape(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }
            return value;
        }

        public void Dispose()
        {
            if (_ownApp && _swApp != null)
            {
                try { _swApp.ExitApp(); } catch { }
                Marshal.ReleaseComObject(_swApp);
                _swApp = null;
            }
        }

        /// <summary>Internal grouping helper</summary>
        private class BomInfo
        {
            public string PartNo;
            public string Name;
            public string Material;
            public int Quantity;
            public double Weight;
            public string FileName;
            public string FilePath;
        }
    }
}
