using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileEncryption
{
    public partial class MainForm : Form
    {
        private static readonly HashSet<string> SolidWorksExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".sldprt", ".sldasm", ".slddrw"
        };

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "选择文件";
                dialog.Filter = "SolidWorks 文件|*.sldprt;*.sldasm;*.slddrw|所有文件|*.*";
                dialog.Multiselect = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = string.Join(";", dialog.FileNames);
                    UpdateFileCount();
                }
            }
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "选择包含 SolidWorks 文件的文件夹";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = dialog.SelectedPath;
                    UpdateFileCount();
                }
            }
        }

        private void UpdateFileCount()
        {
            var files = GetTargetFiles();
            lblStatus.Text = $"已选择 {files.Count} 个文件";
        }

        private List<string> GetTargetFiles()
        {
            var result = new List<string>();
            string pathText = txtPath.Text.Trim();
            if (string.IsNullOrEmpty(pathText)) return result;

            // Multiple files separated by semicolons
            if (pathText.Contains(";"))
            {
                foreach (var part in pathText.Split(';'))
                {
                    string f = part.Trim();
                    if (File.Exists(f) && SolidWorksExtensions.Contains(Path.GetExtension(f)))
                        result.Add(f);
                }
            }
            else if (Directory.Exists(pathText))
            {
                // Folder mode: find all SolidWorks files recursively
                foreach (var ext in SolidWorksExtensions)
                {
                    result.AddRange(Directory.GetFiles(pathText, "*" + ext, SearchOption.AllDirectories));
                }
            }
            else if (File.Exists(pathText))
            {
                result.Add(pathText);
            }
            return result;
        }

        private async void btnEncrypt_Click(object sender, EventArgs e)
        {
            await ProcessFiles(encrypt: true);
        }

        private async void btnDecrypt_Click(object sender, EventArgs e)
        {
            await ProcessFiles(encrypt: false);
        }

        private async Task ProcessFiles(bool encrypt)
        {
            string password = txtPassword.Text;
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("请输入密码。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("密码长度至少为 6 位。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var files = GetTargetFiles();
            if (files.Count == 0)
            {
                MessageBox.Show("未找到需要处理的文件。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetControlsEnabled(false);
            progressBar.Maximum = files.Count;
            progressBar.Value = 0;
            int success = 0;
            int failed = 0;

            string operation = encrypt ? "加密" : "解密";

            await Task.Run(() =>
            {
                foreach (var file in files)
                {
                    try
                    {
                        string outputPath;
                        if (encrypt)
                        {
                            outputPath = file + ".enc";
                        }
                        else
                        {
                            if (!file.EndsWith(".enc", StringComparison.OrdinalIgnoreCase))
                            {
                                failed++;
                                Invoke(new Action(() =>
                                {
                                    progressBar.Value++;
                                    lblStatus.Text = $"跳过（非 .enc 文件）: {Path.GetFileName(file)}";
                                }));
                                continue;
                            }
                            outputPath = file.Substring(0, file.Length - 4);
                        }

                        if (encrypt)
                        {
                            FileEncryptor.EncryptFile(file, outputPath, password);
                        }
                        else
                        {
                            FileEncryptor.DecryptFile(file, outputPath, password);
                        }

                        success++;
                        Invoke(new Action(() =>
                        {
                            progressBar.Value++;
                            lblStatus.Text = $"{operation}成功: {Path.GetFileName(file)}";
                        }));
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        Invoke(new Action(() =>
                        {
                            progressBar.Value++;
                            lblStatus.Text = $"{operation}失败: {Path.GetFileName(file)} - {ex.Message}";
                        }));
                    }
                }
            });

            lblStatus.Text = $"完成。成功: {success}, 失败: {failed}, 共: {files.Count}";
            MessageBox.Show($"{operation}完成！\n成功: {success}\n失败: {failed}\n共: {files.Count}",
                "完成", MessageBoxButtons.OK,
                failed > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            SetControlsEnabled(true);
        }

        private void SetControlsEnabled(bool enabled)
        {
            btnSelectFile.Enabled = enabled;
            btnSelectFolder.Enabled = enabled;
            btnEncrypt.Enabled = enabled;
            btnDecrypt.Enabled = enabled;
            txtPassword.Enabled = enabled;
            txtPath.Enabled = enabled;
        }
    }
}
