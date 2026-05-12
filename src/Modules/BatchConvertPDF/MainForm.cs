using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BatchConvertPDF
{
    public partial class MainForm : Form
    {
        private readonly ConvertEngine _engine = new ConvertEngine();
        private BackgroundWorker _worker;
        private bool _cancelRequested;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "选择包含 CAD 文件的源文件夹";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtSource.Text = dlg.SelectedPath;
                    if (string.IsNullOrEmpty(txtOutput.Text))
                        txtOutput.Text = Path.Combine(dlg.SelectedPath, "Output");
                    LoadFiles();
                }
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "选择输出文件夹";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtOutput.Text = dlg.SelectedPath;
                }
            }
        }

        private void LoadFiles()
        {
            lvFiles.Items.Clear();

            if (string.IsNullOrEmpty(txtSource.Text) || !Directory.Exists(txtSource.Text))
                return;

            var extensions = GetSelectedExtensions();
            var searchOption = chkIncludeSubfolders.Checked
                ? SearchOption.AllDirectories
                : SearchOption.TopDirectoryOnly;

            var files = new List<FileInfo>();
            foreach (var ext in extensions)
            {
                files.AddRange(new DirectoryInfo(txtSource.Text).GetFiles("*" + ext, searchOption));
            }

            files = files.OrderBy(f => f.Name).ToList();

            foreach (var fi in files)
            {
                var item = new ListViewItem(fi.Name);
                item.SubItems.Add(FormatFileSize(fi.Length));
                item.SubItems.Add("等待转换");
                item.Tag = fi.FullName;
                lvFiles.Items.Add(item);
            }

            lblFileCount.Text = $"共 {files.Count} 个文件";
        }

        private List<string> GetSelectedExtensions()
        {
            var extensions = new List<string>();
            if (chkSldprt.Checked) extensions.Add(".sldprt");
            if (chkSldasm.Checked) extensions.Add(".sldasm");
            if (chkSlddrw.Checked) extensions.Add(".slddrw");
            return extensions;
        }

        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes} B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024.0:F1} KB";
            return $"{bytes / (1024.0 * 1024.0):F1} MB";
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (lvFiles.Items.Count == 0)
            {
                MessageBox.Show("没有可转换的文件。请先选择源文件夹。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtOutput.Text))
            {
                MessageBox.Show("请选择输出目录。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ensure output directory exists
            if (!Directory.Exists(txtOutput.Text))
                Directory.CreateDirectory(txtOutput.Text);

            var filePaths = new List<string>();
            foreach (ListViewItem item in lvFiles.Items)
            {
                if (item.Tag is string path)
                    filePaths.Add(path);
            }

            var outputFormat = cboFormat.SelectedItem.ToString();
            var overwrite = chkOverwrite.Checked;
            var sourceRoot = txtSource.Text;
            var outputRoot = txtOutput.Text;

            _cancelRequested = false;
            btnConvert.Enabled = false;
            btnCancel.Enabled = true;
            progressBar.Maximum = filePaths.Count;
            progressBar.Value = 0;
            txtLog.Clear();
            AppendLog($"开始转换 {filePaths.Count} 个文件 -> {outputFormat}");

            _worker = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            _worker.DoWork += (s, ev) =>
            {
                var args = (ConvertJobArgs)ev.Argument;
                var results = new List<ConvertResult>();
                for (int i = 0; i < args.FilePaths.Count; i++)
                {
                    if (_cancelRequested)
                    {
                        ev.Cancel = true;
                        break;
                    }

                    string srcFile = args.FilePaths[i];
                    string relPath = srcFile.Substring(args.SourceRoot.Length).TrimStart(Path.DirectorySeparatorChar);
                    string outFile = Path.ChangeExtension(
                        Path.Combine(args.OutputRoot, relPath), "." + args.OutputFormat.ToLower());

                    // Ensure subdirectory exists in output
                    string outDir = Path.GetDirectoryName(outFile);
                    if (!Directory.Exists(outDir))
                        Directory.CreateDirectory(outDir);

                    var result = _engine.ConvertFile(srcFile, outFile, args.OutputFormat, args.Overwrite);
                    results.Add(result);
                    _worker.ReportProgress(i + 1, new ProgressInfo { FileName = Path.GetFileName(srcFile), Result = result });
                }
                ev.Result = results;
            };
            _worker.ProgressChanged += (s, ev) =>
            {
                var info = (ProgressInfo)ev.UserState;
                progressBar.Value = ev.ProgressPercentage;
                lblProgress.Text = $"{ev.ProgressPercentage}/{progressBar.Maximum}";
                lvFiles.Items[ev.ProgressPercentage - 1].SubItems[2].Text = info.Result.Success ? "完成" : $"失败: {info.Result.Error}";
                AppendLog($"[{ev.ProgressPercentage}] {info.FileName}: {(info.Result.Success ? "成功" : info.Result.Error)}");
            };
            _worker.RunWorkerCompleted += (s, ev) =>
            {
                btnConvert.Enabled = true;
                btnCancel.Enabled = false;
                if (ev.Cancelled)
                    AppendLog("转换已取消。");
                else
                {
                    var results = (List<ConvertResult>)ev.Result;
                    int ok = results.Count(r => r.Success);
                    AppendLog($"转换完成。成功: {ok}/{results.Count}");
                    MessageBox.Show($"转换完成！成功: {ok}/{results.Count}", "完成",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            _worker.RunWorkerAsync(new ConvertJobArgs
            {
                FilePaths = filePaths,
                OutputFormat = outputFormat,
                Overwrite = overwrite,
                SourceRoot = sourceRoot,
                OutputRoot = outputRoot
            });
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancelRequested = true;
            btnCancel.Enabled = false;
        }

        private void AppendLog(string message)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
        }
    }

    internal class ProgressInfo
    {
        public string FileName { get; set; }
        public ConvertResult Result { get; set; }
    }

    internal class ConvertJobArgs
    {
        public List<string> FilePaths { get; set; }
        public string OutputFormat { get; set; }
        public bool Overwrite { get; set; }
        public string SourceRoot { get; set; }
        public string OutputRoot { get; set; }
    }
}
