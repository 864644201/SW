using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using McBom.Controller;
using McBom.Model;

namespace McBom.ViewModel
{
    /// <summary>
    /// BOM ViewModel - 管理BOM数据的加载、过滤、排序、分组和导出
    /// </summary>
    public class BomViewModel
    {
        private readonly BomController _controller;
        private string _currentFilePath;
        private string _searchText;
        private string _currentFilter;
        private string _currentGroup;

        public ObservableCollection<BomItem> BomItems { get; private set; }
        public List<BomItem> FilteredItems { get; private set; }
        public BomConfig Config { get; set; }
        public BomItem SelectedItem { get; private set; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                ApplyFilterAndSearch();
            }
        }

        public BomViewModel()
        {
            BomItems = new ObservableCollection<BomItem>();
            FilteredItems = new List<BomItem>();
            Config = new BomConfig();
            _controller = new BomController();
        }

        /// <summary>
        /// 从SolidWorks装配体加载BOM数据
        /// </summary>
        public void LoadBomFromAssembly(string filePath)
        {
            _currentFilePath = filePath;
            BomItems.Clear();

            var items = _controller.ExtractBomFromAssembly(filePath, Config);

            int seq = 1;
            foreach (var item in items)
            {
                if (Config.HideHiddenItems && item.IsHidden)
                    continue;

                item.ID = seq++;
                BomItems.Add(item);
            }

            ApplyFilterAndSearch();
        }

        /// <summary>
        /// 刷新当前BOM数据
        /// </summary>
        public void RefreshBom()
        {
            if (!string.IsNullOrEmpty(_currentFilePath))
            {
                LoadBomFromAssembly(_currentFilePath);
            }
        }

        /// <summary>
        /// 应用过滤条件
        /// </summary>
        public void ApplyFilter(string filterType)
        {
            _currentFilter = filterType;
            ApplyFilterAndSearch();
        }

        /// <summary>
        /// 应用分组
        /// </summary>
        public void ApplyGrouping(string groupType)
        {
            _currentGroup = groupType;
            ApplyFilterAndSearch();
        }

        /// <summary>
        /// 选中指定项目
        /// </summary>
        public void SelectItem(BomItem item)
        {
            SelectedItem = item;
        }

        /// <summary>
        /// 获取指定父ID的子项
        /// </summary>
        public List<BomItem> GetChildren(int parentId)
        {
            return BomItems.Where(i => i.ParentID == parentId).ToList();
        }

        /// <summary>
        /// 导出为Excel
        /// </summary>
        public void ExportToExcel(string filePath)
        {
            var items = FilteredItems;
            if (items.Count == 0)
                throw new InvalidOperationException("没有可导出的BOM数据");

            var sb = new StringBuilder();

            // HTML table format for Excel compatibility
            sb.AppendLine("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\"");
            sb.AppendLine("xmlns:x=\"urn:schemas-microsoft-com:office:excel\"");
            sb.AppendLine("xmlns=\"http://www.w3.org/TR/REC-html40\">");
            sb.AppendLine("<head><meta charset=\"utf-8\">");
            sb.AppendLine("<style>td{mso-number-format:\\@;}.num{mso-number-format:0.000;}</style>");
            sb.AppendLine("</head><body>");
            sb.AppendLine("<table border=\"1\" cellpadding=\"3\">");

            // Header
            sb.AppendLine("<tr style=\"background:#4472C4;color:white;font-weight:bold\">");
            sb.AppendLine("<td>序号</td><td>零件号</td><td>名称</td><td>材料</td>");
            sb.AppendLine("<td>数量</td><td>单重(kg)</td><td>总重(kg)</td><td>备注</td>");
            sb.AppendLine("</tr>");

            // Data rows
            foreach (var item in items)
            {
                var bgColor = item.IsAssembly ? "background:#D6E4F0;" : "";
                sb.AppendLine($"<tr style=\"{bgColor}\">");
                sb.AppendLine($"<td>{item.ID}</td>");
                sb.AppendLine($"<td>{EscapeHtml(item.PartNo)}</td>");
                sb.AppendLine($"<td>{EscapeHtml(item.Name)}</td>");
                sb.AppendLine($"<td>{EscapeHtml(item.Material)}</td>");
                sb.AppendLine($"<td>{item.Quantity}</td>");
                sb.AppendLine($"<td class=\"num\">{item.UnitWeight:F3}</td>");
                sb.AppendLine($"<td class=\"num\">{item.TotalWeight:F3}</td>");
                sb.AppendLine($"<td>{EscapeHtml(item.Remark)}</td>");
                sb.AppendLine("</tr>");
            }

            // Summary row
            var totalWeight = items.Sum(i => i.TotalWeight);
            sb.AppendLine("<tr style=\"background:#E2EFDA;font-weight:bold\">");
            sb.AppendLine("<td colspan=\"4\">合计</td>");
            sb.AppendLine($"<td>{items.Sum(i => i.Quantity)}</td>");
            sb.AppendLine("<td></td>");
            sb.AppendLine($"<td class=\"num\">{totalWeight:F3}</td>");
            sb.AppendLine("<td></td></tr>");

            sb.AppendLine("</table></body></html>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// 导出为PDF (通过HTML中间格式)
        /// </summary>
        public void ExportToPdf(string filePath)
        {
            var items = FilteredItems;
            if (items.Count == 0)
                throw new InvalidOperationException("没有可导出的BOM数据");

            // Generate HTML content, then save as .html for browser-based PDF printing
            var htmlPath = Path.ChangeExtension(filePath, ".html");
            ExportToHtml(htmlPath, items);

            // Open in default browser for PDF printing
            Process.Start(new ProcessStartInfo(htmlPath) { UseShellExecute = true });
        }

        private void ExportToHtml(string filePath, List<BomItem> items)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html><html><head><meta charset=\"utf-8\">");
            sb.AppendLine("<title>BOM明细表</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body{font-family:SimSun,serif;margin:20px;}");
            sb.AppendLine("h1{text-align:center;font-size:18px;}");
            sb.AppendLine("table{width:100%;border-collapse:collapse;margin-top:15px;}");
            sb.AppendLine("th,td{border:1px solid #333;padding:5px 8px;font-size:12px;}");
            sb.AppendLine("th{background:#4472C4;color:white;text-align:center;}");
            sb.AppendLine("td.center{text-align:center;}");
            sb.AppendLine("td.num{text-align:right;}");
            sb.AppendLine("tr:nth-child(even){background:#f5f5f5;}");
            sb.AppendLine(".summary{background:#E2EFDA;font-weight:bold;}");
            sb.AppendLine("@media print{body{margin:10mm;}}");
            sb.AppendLine("</style></head><body>");
            sb.AppendLine($"<h1>BOM明细表 - {Path.GetFileNameWithoutExtension(_currentFilePath ?? "")}</h1>");
            sb.AppendLine($"<p>生成日期: {DateTime.Now:yyyy-MM-dd HH:mm}</p>");
            sb.AppendLine("<table><thead><tr>");
            sb.AppendLine("<th>序号</th><th>零件号</th><th>名称</th><th>材料</th>");
            sb.AppendLine("<th>数量</th><th>单重(kg)</th><th>总重(kg)</th><th>备注</th>");
            sb.AppendLine("</tr></thead><tbody>");

            foreach (var item in items)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td class=\"center\">{item.ID}</td>");
                sb.AppendLine($"<td>{EscapeHtml(item.PartNo)}</td>");
                sb.AppendLine($"<td>{EscapeHtml(item.Name)}</td>");
                sb.AppendLine($"<td>{EscapeHtml(item.Material)}</td>");
                sb.AppendLine($"<td class=\"center\">{item.Quantity}</td>");
                sb.AppendLine($"<td class=\"num\">{item.UnitWeight:F3}</td>");
                sb.AppendLine($"<td class=\"num\">{item.TotalWeight:F3}</td>");
                sb.AppendLine($"<td>{EscapeHtml(item.Remark)}</td>");
                sb.AppendLine("</tr>");
            }

            var totalWeight = items.Sum(i => i.TotalWeight);
            sb.AppendLine("<tr class=\"summary\">");
            sb.AppendLine("<td colspan=\"4\">合计</td>");
            sb.AppendLine($"<td class=\"center\">{items.Sum(i => i.Quantity)}</td>");
            sb.AppendLine("<td></td>");
            sb.AppendLine($"<td class=\"num\">{totalWeight:F3}</td>");
            sb.AppendLine("<td></td></tr>");

            sb.AppendLine("</tbody></table></body></html>");

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private void ApplyFilterAndSearch()
        {
            var items = BomItems.AsEnumerable();

            // Apply search
            if (!string.IsNullOrWhiteSpace(_searchText))
            {
                var search = _searchText.Trim().ToLower();
                items = items.Where(i =>
                    (i.PartNo != null && i.PartNo.ToLower().Contains(search)) ||
                    (i.Name != null && i.Name.ToLower().Contains(search)) ||
                    (i.Material != null && i.Material.ToLower().Contains(search)) ||
                    (i.Remark != null && i.Remark.ToLower().Contains(search)));
            }

            // Apply filter
            switch (_currentFilter)
            {
                case "仅零件":
                    items = items.Where(i => !i.IsAssembly);
                    break;
                case "仅装配体":
                    items = items.Where(i => i.IsAssembly);
                    break;
                case "仅隐藏项":
                    items = items.Where(i => i.IsHidden);
                    break;
            }

            // Apply grouping/sorting
            switch (_currentGroup)
            {
                case "按材料":
                    items = items.OrderBy(i => i.Material ?? "").ThenBy(i => i.PartNo);
                    break;
                case "按层级":
                    items = items.OrderBy(i => i.Level).ThenBy(i => i.PartNo);
                    break;
                default:
                    items = items.OrderBy(i => i.ID);
                    break;
            }

            FilteredItems = items.ToList();
        }

        private static string EscapeHtml(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return text.Replace("&", "&amp;")
                       .Replace("<", "&lt;")
                       .Replace(">", "&gt;")
                       .Replace("\"", "&quot;");
        }
    }
}
