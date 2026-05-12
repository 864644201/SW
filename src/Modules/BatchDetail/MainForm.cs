using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BatchDetail
{
    public partial class MainForm : Form
    {
        private readonly List<string> _assemblyFiles = new List<string>();
        private List<BomItem> _allBomItems = new List<BomItem>();
        private DetailExtractor _extractor;

        public MainForm()
        {
            InitializeComponent();
        }

        #region 文件管理

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "SolidWorks 装配体|*.sldasm|所有文件|*.*";
                dlg.Multiselect = true;
                dlg.Title = "选择装配体文件";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (var f in dlg.FileNames)
                    {
                        if (!_assemblyFiles.Contains(f))
                            _assemblyFiles.Add(f);
                    }
                    RefreshFileList();
                }
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "选择包含装配体的文件夹";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var files = Directory.GetFiles(dlg.SelectedPath, "*.sldasm",
                        chkSubdirs.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
                    foreach (var f in files)
                    {
                        if (!_assemblyFiles.Contains(f))
                            _assemblyFiles.Add(f);
                    }
                    RefreshFileList();
                }
            }
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (lstFiles.SelectedIndex >= 0 && lstFiles.SelectedIndex < _assemblyFiles.Count)
            {
                _assemblyFiles.RemoveAt(lstFiles.SelectedIndex);
                RefreshFileList();
            }
        }

        private void btnClearFiles_Click(object sender, EventArgs e)
        {
            _assemblyFiles.Clear();
            RefreshFileList();
        }

        private void RefreshFileList()
        {
            lstFiles.Items.Clear();
            foreach (var f in _assemblyFiles)
                lstFiles.Items.Add(Path.GetFileName(f));
            lblFileCount.Text = "共 " + _assemblyFiles.Count + " 个装配体";
        }

        #endregion

        #region BOM 提取

        private void btnExtract_Click(object sender, EventArgs e)
        {
            if (_assemblyFiles.Count == 0)
            {
                MessageBox.Show("请先添加装配体文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _allBomItems.Clear();
            dgvBom.Rows.Clear();

            progressBar.Maximum = _assemblyFiles.Count;
            progressBar.Value = 0;

            int success = 0;
            int failed = 0;
            var sw = Stopwatch.StartNew();

            try
            {
                _extractor = new DetailExtractor();

                foreach (var filePath in _assemblyFiles)
                {
                    try
                    {
                        lblStatus.Text = "正在处理: " + Path.GetFileName(filePath);
                        Application.DoEvents();

                        var items = _extractor.ExtractBomFromAssembly(filePath);
                        _allBomItems.AddRange(items);

                        foreach (var item in items)
                        {
                            dgvBom.Rows.Add(
                                item.PartNo,
                                item.Name,
                                item.Material,
                                item.Quantity,
                                item.Weight.ToString("F4"),
                                item.FileName
                            );
                        }

                        success++;
                    }
                    catch (Exception ex)
                    {
                        dgvBom.Rows.Add(Path.GetFileNameWithoutExtension(filePath), "错误", "", 0, "", Path.GetFileName(filePath));
                        lstLog.Items.Add("[错误] " + Path.GetFileName(filePath) + ": " + ex.Message);
                        failed++;
                    }

                    progressBar.Value++;
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_extractor != null) { _extractor.Dispose(); _extractor = null; }
                sw.Stop();
            }

            lblStatus.Text = "提取完成: 成功 " + success + ", 失败 " + failed +
                ", 共 " + _allBomItems.Count + " 条, 耗时 " + sw.Elapsed.TotalSeconds.ToString("F1") + "秒";
            lstLog.Items.Add("提取完成: 成功 " + success + ", 失败 " + failed +
                ", 共 " + _allBomItems.Count + " 条记录");
        }

        #endregion

        #region 导出

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (_allBomItems.Count == 0)
            {
                MessageBox.Show("请先提取 BOM 数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Excel 文件|*.xlsx|CSV 文件|*.csv";
                dlg.DefaultExt = "xlsx";
                dlg.FileName = "BOM_明细_" + DateTime.Now.ToString("yyyyMMdd");
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(dlg.FileName).ToLower();
                        if (ext == ".csv")
                            DetailExtractor.ExportToCsv(_allBomItems, dlg.FileName);
                        else
                            DetailExtractor.ExportToExcel(_allBomItems, dlg.FileName);

                        lblStatus.Text = "导出完成: " + dlg.FileName;
                        lstLog.Items.Add("已导出: " + dlg.FileName);

                        if (chkOpenAfterExport.Checked)
                            Process.Start(dlg.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (_allBomItems.Count == 0)
            {
                MessageBox.Show("请先提取 BOM 数据。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "CSV 文件|*.csv";
                dlg.DefaultExt = "csv";
                dlg.FileName = "BOM_明细_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DetailExtractor.ExportToCsv(_allBomItems, dlg.FileName);
                        lblStatus.Text = "导出完成: " + dlg.FileName;
                        lstLog.Items.Add("已导出 CSV: " + dlg.FileName);

                        if (chkOpenAfterExport.Checked)
                            Process.Start(dlg.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion
    }
}
