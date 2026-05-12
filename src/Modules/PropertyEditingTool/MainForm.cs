using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PropertyEditingTool
{
    public partial class MainForm : Form
    {
        private readonly List<FilePropertyData> _files = new List<FilePropertyData>();
        private bool _suppressEvents = false;

        public MainForm()
        {
            InitializeComponent();
            SetupDragDrop();
        }

        private void SetupDragDrop()
        {
            lstFiles.AllowDrop = true;
            lstFiles.DragEnter += (s, e) =>
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
            };
            lstFiles.DragDrop += (s, e) =>
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                AddFiles(files);
            };
        }

        private void AddFiles(string[] files)
        {
            foreach (var file in files)
            {
                string ext = Path.GetExtension(file).ToLower();
                if (ext == ".sldprt" || ext == ".sldasm" || ext == ".slddrw")
                {
                    if (_files.Any(f => f.FilePath == file)) continue;
                    var data = new FilePropertyData { FilePath = file };
                    _files.Add(data);
                    lstFiles.Items.Add(Path.GetFileName(file));
                }
            }
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "SolidWorks 文件 (*.sldprt;*.sldasm;*.slddrw)|*.sldprt;*.sldasm;*.slddrw|所有文件 (*.*)|*.*";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                    AddFiles(ofd.FileNames);
            }
        }

        private void btnClearFiles_Click(object sender, EventArgs e)
        {
            _files.Clear();
            lstFiles.Items.Clear();
            dgvBatch.Rows.Clear();
            dgvFileProps.Rows.Clear();
        }

        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            if (_files.Count == 0)
            {
                MessageBox.Show("请先添加文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var swApp = GetSwApp();
            if (swApp == null)
            {
                MessageBox.Show("无法连接 SolidWorks。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 收集所有属性名
            var allPropNames = new HashSet<string>();
            foreach (var file in _files)
            {
                try
                {
                    file.Properties = ReadPropsFromSw(swApp, file.FilePath);
                    foreach (var key in file.Properties.Keys)
                        allPropNames.Add(key);
                }
                catch { }
            }

            // 填充批量编辑网格
            dgvBatch.Rows.Clear();
            foreach (var name in allPropNames.OrderBy(x => x))
            {
                dgvBatch.Rows.Add(name, "", false);
            }

            // 刷新当前选中文件的属性
            RefreshFileProps();
            MessageBox.Show($"已加载 {_files.Count} 个文件的属性。", "完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFileProps();
        }

        private void RefreshFileProps()
        {
            dgvFileProps.Rows.Clear();
            if (lstFiles.SelectedIndex < 0 || lstFiles.SelectedIndex >= _files.Count) return;
            var data = _files[lstFiles.SelectedIndex];
            foreach (var kv in data.Properties)
            {
                dgvFileProps.Rows.Add(kv.Key, kv.Value);
            }
        }

        private void btnFindReplace_Click(object sender, EventArgs e)
        {
            string findText = txtFind.Text;
            string replaceText = txtReplace.Text;
            if (string.IsNullOrEmpty(findText)) return;

            int count = 0;
            foreach (DataGridViewRow row in dgvBatch.Rows)
            {
                if (row.IsNewRow) continue;
                bool isChecked = Convert.ToBoolean(row.Cells[2].Value ?? false);
                if (!isChecked) continue;
                string val = row.Cells[1].Value?.ToString() ?? "";
                if (val.Contains(findText))
                {
                    row.Cells[1].Value = val.Replace(findText, replaceText);
                    count++;
                }
            }

            // 同时检查文件属性网格
            foreach (DataGridViewRow row in dgvFileProps.Rows)
            {
                if (row.IsNewRow) continue;
                string val = row.Cells[1].Value?.ToString() ?? "";
                if (val.Contains(findText))
                {
                    row.Cells[1].Value = val.Replace(findText, replaceText);
                    count++;
                }
            }

            MessageBox.Show($"已替换 {count} 处。", "查找替换", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnApplyBatch_Click(object sender, EventArgs e)
        {
            if (_files.Count == 0)
            {
                MessageBox.Show("请先添加文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 收集要批量写入的属性
            var batchProps = new Dictionary<string, string>();
            foreach (DataGridViewRow row in dgvBatch.Rows)
            {
                if (row.IsNewRow) continue;
                bool isChecked = Convert.ToBoolean(row.Cells[2].Value ?? false);
                if (!isChecked) continue;
                string name = row.Cells[0].Value?.ToString()?.Trim();
                string value = row.Cells[1].Value?.ToString()?.Trim();
                if (!string.IsNullOrEmpty(name))
                    batchProps[name] = value ?? "";
            }

            if (batchProps.Count == 0)
            {
                MessageBox.Show("请勾选要批量写入的属性。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var swApp = GetSwApp();
            if (swApp == null)
            {
                MessageBox.Show("无法连接 SolidWorks。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int success = 0, fail = 0;
            foreach (var file in _files)
            {
                try
                {
                    WritePropsToSw(swApp, file.FilePath, batchProps);
                    foreach (var kv in batchProps)
                        file.Properties[kv.Key] = kv.Value;
                    success++;
                }
                catch
                {
                    fail++;
                }
            }

            RefreshFileProps();
            MessageBox.Show($"批量写入完成。\n成功: {success}\n失败: {fail}", "结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (_files.Count == 0)
            {
                MessageBox.Show("没有数据可导出。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV 文件 (*.csv)|*.csv";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                // 收集所有属性名
                var allNames = new HashSet<string>();
                foreach (var f in _files)
                    foreach (var k in f.Properties.Keys)
                        allNames.Add(k);
                var nameList = allNames.OrderBy(x => x).ToList();

                using (var writer = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                {
                    // 表头
                    writer.Write("文件名");
                    foreach (var n in nameList)
                        writer.Write("," + n);
                    writer.WriteLine();

                    // 数据行
                    foreach (var file in _files)
                    {
                        writer.Write(Path.GetFileName(file.FilePath));
                        foreach (var n in nameList)
                        {
                            string val = file.Properties.ContainsKey(n) ? file.Properties[n] : "";
                            writer.Write("," + EscapeCsv(val));
                        }
                        writer.WriteLine();
                    }
                }
                MessageBox.Show("导出完成。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV 文件 (*.csv)|*.csv";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                var lines = File.ReadAllLines(ofd.FileName);
                if (lines.Length < 2)
                {
                    MessageBox.Show("CSV 文件为空或只有表头。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var headers = ParseCsvLine(lines[0]);
                if (headers.Length < 2)
                {
                    MessageBox.Show("CSV 格式不正确。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 第一列是文件名，其余是属性
                var propNames = new List<string>();
                for (int i = 1; i < headers.Length; i++)
                    propNames.Add(headers[i]);

                // 清空现有数据
                _files.Clear();
                lstFiles.Items.Clear();
                dgvBatch.Rows.Clear();
                dgvFileProps.Rows.Clear();

                for (int r = 1; r < lines.Length; r++)
                {
                    var cells = ParseCsvLine(lines[r]);
                    if (cells.Length < 1) continue;

                    var data = new FilePropertyData { FilePath = cells[0] };
                    for (int c = 0; c < propNames.Count && c + 1 < cells.Length; c++)
                    {
                        data.Properties[propNames[c]] = cells[c + 1];
                    }
                    _files.Add(data);
                    lstFiles.Items.Add(Path.GetFileName(data.FilePath));
                }

                // 更新批量编辑网格
                foreach (var name in propNames)
                    dgvBatch.Rows.Add(name, "", false);

                if (_files.Count > 0)
                {
                    lstFiles.SelectedIndex = 0;
                    RefreshFileProps();
                }

                MessageBox.Show($"已导入 {_files.Count} 个文件的数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string EscapeCsv(string val)
        {
            if (val.Contains(",") || val.Contains("\"") || val.Contains("\n"))
                return "\"" + val.Replace("\"", "\"\"") + "\"";
            return val;
        }

        private string[] ParseCsvLine(string line)
        {
            var result = new List<string>();
            bool inQuotes = false;
            string current = "";
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (inQuotes)
                {
                    if (c == '"' && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current += '"';
                        i++;
                    }
                    else if (c == '"')
                    {
                        inQuotes = false;
                    }
                    else
                    {
                        current += c;
                    }
                }
                else
                {
                    if (c == '"')
                    {
                        inQuotes = true;
                    }
                    else if (c == ',')
                    {
                        result.Add(current);
                        current = "";
                    }
                    else
                    {
                        current += c;
                    }
                }
            }
            result.Add(current);
            return result.ToArray();
        }

        private dynamic GetSwApp()
        {
            try { return Marshal.GetActiveObject("SldWorks.Application"); }
            catch { return null; }
        }

        private int GetFileType(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            switch (ext)
            {
                case ".sldprt": return 1;
                case ".sldasm": return 2;
                case ".slddrw": return 3;
                default: return 1;
            }
        }

        private Dictionary<string, string> ReadPropsFromSw(dynamic swApp, string filePath)
        {
            var result = new Dictionary<string, string>();
            int err = 0, warn = 0;
            dynamic model = swApp.OpenDoc6(filePath, GetFileType(filePath), 0, "", ref err, ref warn);
            if (model == null) return result;

            dynamic propMgr = model.Extension.CustomPropertyManager[""];
            if (propMgr != null)
            {
                string[] names = (string[])propMgr.GetNames();
                if (names != null)
                {
                    foreach (string name in names)
                    {
                        string val, resolved;
                        propMgr.Get2(name, out val, out resolved);
                        result[name] = resolved ?? val ?? "";
                    }
                }
            }
            swApp.CloseDoc(filePath);
            return result;
        }

        private void WritePropsToSw(dynamic swApp, string filePath, Dictionary<string, string> props)
        {
            int err = 0, warn = 0;
            dynamic model = swApp.OpenDoc6(filePath, GetFileType(filePath), 0, "", ref err, ref warn);
            if (model == null) throw new InvalidOperationException("无法打开文件");

            dynamic propMgr = model.Extension.CustomPropertyManager[""];
            if (propMgr != null)
            {
                foreach (var kv in props)
                    propMgr.Set3(kv.Key, kv.Value, 30);
            }
            model.Save();
            swApp.CloseDoc(filePath);
        }
    }

    public class FilePropertyData
    {
        public string FilePath { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}
