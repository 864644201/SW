using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using Forms = System.Windows.Forms;

namespace MaidouBao.Core
{
    /// <summary>
    /// WPF / WinForms UI 工具类，提供常用的消息提示和文件对话框辅助方法。
    /// </summary>
    public static class UIHelper
    {
        #region 消息提示

        /// <summary>
        /// 显示信息消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="title">标题，默认为"提示"</param>
        public static void ShowMessage(string message, string title = "提示")
        {
            Forms.MessageBox.Show(message, title, Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示错误消息
        /// </summary>
        /// <param name="message">错误消息内容</param>
        /// <param name="title">标题，默认为"错误"</param>
        public static void ShowError(string message, string title = "错误")
        {
            Forms.MessageBox.Show(message, title, Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Error);
        }

        /// <summary>
        /// 显示警告消息
        /// </summary>
        /// <param name="message">警告消息内容</param>
        /// <param name="title">标题，默认为"警告"</param>
        public static void ShowWarning(string message, string title = "警告")
        {
            Forms.MessageBox.Show(message, title, Forms.MessageBoxButtons.OK, Forms.MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 显示确认对话框
        /// </summary>
        /// <param name="message">确认消息内容</param>
        /// <param name="title">标题，默认为"确认"</param>
        /// <returns>用户点击"是"返回 true，否则返回 false</returns>
        public static bool ConfirmDialog(string message, string title = "确认")
        {
            var result = Forms.MessageBox.Show(message, title, Forms.MessageBoxButtons.YesNo, Forms.MessageBoxIcon.Question);
            return result == Forms.DialogResult.Yes;
        }

        #endregion

        #region 文件对话框

        /// <summary>
        /// 打开文件对话框，支持多选和过滤器
        /// </summary>
        /// <param name="filter">文件过滤器，例如 "SolidWorks 文件|*.sldprt;*.sldasm|所有文件|*.*"</param>
        /// <param name="title">对话框标题</param>
        /// <param name="initialDirectory">初始目录</param>
        /// <param name="multiSelect">是否允许多选</param>
        /// <returns>选中的文件路径数组，取消返回 null</returns>
        public static string[] OpenFile(string filter = null, string title = null, string initialDirectory = null, bool multiSelect = false)
        {
            var dialog = new Forms.OpenFileDialog();

            if (!string.IsNullOrEmpty(filter))
                dialog.Filter = filter;

            if (!string.IsNullOrEmpty(title))
                dialog.Title = title;

            if (!string.IsNullOrEmpty(initialDirectory) && Directory.Exists(initialDirectory))
                dialog.InitialDirectory = initialDirectory;

            dialog.Multiselect = multiSelect;

            if (dialog.ShowDialog() == Forms.DialogResult.OK)
            {
                return dialog.FileNames;
            }

            return null;
        }

        /// <summary>
        /// 打开单个文件对话框（简化版本）
        /// </summary>
        /// <param name="filter">文件过滤器</param>
        /// <param name="title">对话框标题</param>
        /// <returns>选中的文件路径，取消返回 null</returns>
        public static string OpenSingleFile(string filter = null, string title = null)
        {
            string[] files = OpenFile(filter, title, null, false);
            return files != null && files.Length > 0 ? files[0] : null;
        }

        /// <summary>
        /// 保存文件对话框
        /// </summary>
        /// <param name="filter">文件过滤器，例如 "SolidWorks 零件|*.sldprt|所有文件|*.*"</param>
        /// <param name="title">对话框标题</param>
        /// <param name="initialDirectory">初始目录</param>
        /// <param name="defaultFileName">默认文件名</param>
        /// <returns>用户选择的保存路径，取消返回 null</returns>
        public static string SaveFile(string filter = null, string title = null, string initialDirectory = null, string defaultFileName = null)
        {
            var dialog = new Forms.SaveFileDialog();

            if (!string.IsNullOrEmpty(filter))
                dialog.Filter = filter;

            if (!string.IsNullOrEmpty(title))
                dialog.Title = title;

            if (!string.IsNullOrEmpty(initialDirectory) && Directory.Exists(initialDirectory))
                dialog.InitialDirectory = initialDirectory;

            if (!string.IsNullOrEmpty(defaultFileName))
                dialog.FileName = defaultFileName;

            dialog.OverwritePrompt = true;

            if (dialog.ShowDialog() == Forms.DialogResult.OK)
            {
                return dialog.FileName;
            }

            return null;
        }

        /// <summary>
        /// 选择文件夹对话框
        /// </summary>
        /// <param name="description">对话框描述文字</param>
        /// <param name="initialDirectory">初始目录</param>
        /// <param name="showNewFolderButton">是否显示"新建文件夹"按钮</param>
        /// <returns>选中的文件夹路径，取消返回 null</returns>
        public static string SelectFolder(string description = null, string initialDirectory = null, bool showNewFolderButton = true)
        {
            var dialog = new Forms.FolderBrowserDialog();

            if (!string.IsNullOrEmpty(description))
                dialog.Description = description;

            if (!string.IsNullOrEmpty(initialDirectory) && Directory.Exists(initialDirectory))
                dialog.SelectedPath = initialDirectory;

            dialog.ShowNewFolderButton = showNewFolderButton;

            if (dialog.ShowDialog() == Forms.DialogResult.OK)
            {
                return dialog.SelectedPath;
            }

            return null;
        }

        #endregion
    }
}
