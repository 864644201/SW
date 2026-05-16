using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MaidouBao.Launcher
{
    public partial class BookReaderWindow : Window
    {
        private readonly string _filePath;
        private readonly string _notesPath;
        private bool _notesVisible = true;
        private bool _noteModified = false;

        public BookReaderWindow(string filePath)
        {
            InitializeComponent();
            _filePath = filePath;
            _notesPath = filePath + ".notes";

            Title = "阅读 - " + Path.GetFileNameWithoutExtension(filePath);
            titleText.Text = Path.GetFileNameWithoutExtension(filePath);
            filePathText.Text = filePath;

            LoadContent();
            LoadNotes();
        }

        private void LoadContent()
        {
            var ext = Path.GetExtension(_filePath).ToLowerInvariant();

            if (ext == ".pdf")
            {
                // WebBrowser IE 引擎无法渲染 PDF，提示用外部程序
                pdfViewer.Visibility = Visibility.Collapsed;
                txtScroll.Visibility = Visibility.Collapsed;
                unsupportedPanel.Visibility = Visibility.Visible;
                unsupportedExt.Text = "PDF 文件请用外部程序查看，笔记功能仍可正常使用";
                // 自动打开外部 PDF 阅读器
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = _filePath,
                        UseShellExecute = true
                    });
                }
                catch { }
            }
            else if (ext == ".chm")
            {
                pdfViewer.Visibility = Visibility.Visible;
                txtScroll.Visibility = Visibility.Collapsed;
                unsupportedPanel.Visibility = Visibility.Collapsed;
                try
                {
                    pdfViewer.Navigate(new Uri("file:///" + _filePath.Replace("\\", "/")));
                }
                catch (Exception ex)
                {
                    pdfViewer.Visibility = Visibility.Collapsed;
                    unsupportedPanel.Visibility = Visibility.Visible;
                    unsupportedExt.Text = "CHM 加载失败: " + ex.Message;
                }
            }
            else if (ext == ".txt" || ext == ".csv" || ext == ".log" || ext == ".ini" || ext == ".xml" || ext == ".json" || ext == ".md")
            {
                pdfViewer.Visibility = Visibility.Collapsed;
                txtScroll.Visibility = Visibility.Visible;
                unsupportedPanel.Visibility = Visibility.Collapsed;
                try
                {
                    txtViewer.Text = File.ReadAllText(_filePath, Encoding.UTF8);
                }
                catch
                {
                    try
                    {
                        txtViewer.Text = File.ReadAllText(_filePath, Encoding.Default);
                    }
                    catch (Exception ex)
                    {
                        txtViewer.Text = "读取文件失败: " + ex.Message;
                    }
                }
            }
            else
            {
                pdfViewer.Visibility = Visibility.Collapsed;
                txtScroll.Visibility = Visibility.Collapsed;
                unsupportedPanel.Visibility = Visibility.Visible;
                unsupportedExt.Text = $"不支持的格式: {ext}";
            }
        }

        private void LoadNotes()
        {
            if (File.Exists(_notesPath))
            {
                try
                {
                    noteTextBox.Text = File.ReadAllText(_notesPath, Encoding.UTF8);
                }
                catch
                {
                    noteTextBox.Text = "";
                }
            }
            _noteModified = false;
            UpdateNoteStatus();
        }

        private void SaveNotes()
        {
            if (!_noteModified) return;
            try
            {
                File.WriteAllText(_notesPath, noteTextBox.Text, Encoding.UTF8);
                _noteModified = false;
                UpdateNoteStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存笔记失败: " + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UpdateNoteStatus()
        {
            var len = noteTextBox.Text?.Length ?? 0;
            noteStatus.Text = len > 0 ? $"{len} 字" : "暂无笔记";
            noteSavedHint.Text = _noteModified ? "* 有未保存的修改" : "关闭窗口时自动保存";
        }

        private void BtnToggleNotes_Click(object sender, RoutedEventArgs e)
        {
            _notesVisible = !_notesVisible;
            notesPanel.Visibility = _notesVisible ? Visibility.Visible : Visibility.Collapsed;
            btnToggleNotes.Content = _notesVisible ? "隐藏笔记" : "笔记";
        }

        private void BtnOpenExternal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = _filePath,
                    UseShellExecute = true,
                    WorkingDirectory = Path.GetDirectoryName(_filePath)
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开失败: " + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnSaveNote_Click(object sender, RoutedEventArgs e)
        {
            SaveNotes();
            noteSavedHint.Text = "已保存";
        }

        private void NoteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _noteModified = true;
            UpdateNoteStatus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveNotes();
        }
    }
}
