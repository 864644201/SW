using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace TableWorkbookLauncher
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var workbookPath = Path.Combine(baseDir, "整合计算表.xlsx");

            if (!File.Exists(workbookPath))
            {
                MessageBox.Show($"未找到表格文件：\n{workbookPath}", "表格工具大全",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = workbookPath,
                    UseShellExecute = true,
                    WorkingDirectory = Path.GetDirectoryName(workbookPath)
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开表格失败：{ex.Message}", "表格工具大全",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
