using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaidouBao.Plugin;

namespace MaidouBao.Launcher
{
    public partial class MainWindow : Window
    {
        private string _dataDir;
        private string _toolsDir;
        private string _tablesDir;
        private List<PluginInfo> _plugins;
        private List<PluginInfo> _allModuleEntries;
        private List<CustomToolEntry> _customTools;
        private string _customToolsFile;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
            _toolsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tools");
            _tablesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tables");
            _customToolsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "custom_tools.txt");
            if (!Directory.Exists(_dataDir)) Directory.CreateDirectory(_dataDir);
            if (!Directory.Exists(_toolsDir)) Directory.CreateDirectory(_toolsDir);
            if (!Directory.Exists(_tablesDir)) Directory.CreateDirectory(_tablesDir);
            LoadAllPlugins();
            LoadCustomTools();
            LoadTableTools();
        }

        private void LoadAllPlugins()
        {
            _plugins = PluginManager.LoadPlugins(_dataDir);
            _allModuleEntries = new List<PluginInfo>();

            foreach (var plugin in _plugins)
            {
                _allModuleEntries.Add(plugin);
            }

            ScanStandaloneModules("设计工具");
            ScanStandaloneModules("二维工具");
            ScanStandaloneModules("常用工具");
            ScanStandaloneModules("表格工具大全");

            RenderModuleTabs();
        }

        private Button CreateModuleButton(PluginInfo plugin)
        {
            var btn = new Button
            {
                Style = (Style)FindResource("ModuleButton"),
                Tag = plugin,
                Content = new StackPanel
                {
                    Children =
                    {
                        new TextBlock
                        {
                            Text = plugin.Name,
                            TextWrapping = TextWrapping.Wrap,
                            TextAlignment = TextAlignment.Left,
                            Foreground = Brushes.White,
                            FontSize = 15,
                            FontWeight = FontWeights.Bold
                        },
                        new TextBlock
                        {
                            Text = plugin.Explain ?? "点击启动模块",
                            TextWrapping = TextWrapping.Wrap,
                            TextAlignment = TextAlignment.Left,
                            Foreground = new SolidColorBrush(Color.FromRgb(230, 240, 250)),
                            FontSize = 11,
                            Margin = new Thickness(0, 8, 0, 6),
                            MaxHeight = 34
                        },
                        new TextBlock
                        {
                            Text = "单击启动  |  右键打开目录",
                            TextAlignment = TextAlignment.Left,
                            Foreground = new SolidColorBrush(Color.FromRgb(210, 225, 240)),
                            FontSize = 10
                        }
                    }
                }
            };
            btn.Click += ModuleButton_Click;
            ToolTipService.SetToolTip(btn, plugin.Explain ?? plugin.Name);
            var menu = new ContextMenu();
            var openFolderItem = new MenuItem { Header = "打开模块目录" };
            openFolderItem.Click += (sender, args) => OpenModuleFolder(plugin);
            menu.Items.Add(openFolderItem);
            btn.ContextMenu = menu;
            return btn;
        }

        private void ModuleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is PluginInfo plugin)
            {
                LaunchModule(plugin);
            }
        }

        private void LaunchModule(PluginInfo plugin)
        {
            try
            {
                string moduleDir = plugin.DirPath ?? Path.Combine(_dataDir, plugin.Name);
                string exePath = null;
                if (!string.IsNullOrEmpty(plugin.StartExe))
                {
                    exePath = Path.Combine(moduleDir, plugin.StartExe);
                }

                if (exePath != null && File.Exists(exePath))
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = exePath,
                        WorkingDirectory = moduleDir
                    };
                    Process.Start(psi);
                    statusText.Text = $"已启动: {plugin.Name}";
                }
                else
                {
                    if (Directory.Exists(moduleDir))
                    {
                        var exes = Directory.GetFiles(moduleDir, "*.exe");
                        if (exes.Length > 0)
                        {
                            var psi = new ProcessStartInfo
                            {
                                FileName = exes[0],
                                WorkingDirectory = moduleDir
                            };
                            Process.Start(psi);
                            statusText.Text = $"已启动: {plugin.Name}";
                            return;
                        }
                    }
                    MessageBox.Show($"找不到模块的可执行文件: {plugin.Name}", "启动失败",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动模块失败: {ex.Message}", "错误",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenModuleFolder(PluginInfo plugin)
        {
            string moduleDir = plugin.DirPath ?? Path.Combine(_dataDir, plugin.Name);
            if (!Directory.Exists(moduleDir))
            {
                MessageBox.Show($"找不到模块目录: {plugin.Name}", "打开失败",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Process.Start(new ProcessStartInfo
            {
                FileName = moduleDir,
                UseShellExecute = true
            });
            statusText.Text = $"已打开目录: {plugin.Name}";
        }

        private void ScanStandaloneModules(string category)
        {
            if (!Directory.Exists(_dataDir)) return;

            foreach (var dir in Directory.GetDirectories(_dataDir))
            {
                var dirName = Path.GetFileName(dir);
                var pluginXml = Path.Combine(dir, "Plugin.xml");
                if (File.Exists(pluginXml)) continue;

                var exes = Directory.GetFiles(dir, "*.exe");
                if (exes.Length == 0) continue;

                var exeName = Path.GetFileName(exes[0]);
                if (exeName.Equals("pgrun.exe", StringComparison.OrdinalIgnoreCase) ||
                    exeName.Equals("MD3DToolsData.exe", StringComparison.OrdinalIgnoreCase) ||
                    exeName.Equals("CheckEnvironment.exe", StringComparison.OrdinalIgnoreCase))
                    continue;

                var plugin = new PluginInfo
                {
                    Name = dirName,
                    StartExe = exeName,
                    DirPath = dir,
                    ClassId = category == "设计工具" ? "2090_2219_2221" :
                              category == "二维工具" ? "2090_2219_2222" : "2090_2219_2223",
                    Explain = dirName
                };

                if (category == "表格工具大全")
                {
                    plugin.ClassId = "2090_2219_2224";
                }

                _allModuleEntries.Add(plugin);
            }
        }

        private void RenderModuleTabs()
        {
            designToolsPanel.Children.Clear();
            draftingToolsPanel.Children.Clear();
            commonToolsPanel.Children.Clear();
            tableToolsPanel.Children.Clear();

            string keyword = moduleSearchBox?.Text?.Trim();
            int visibleCount = 0;

            foreach (var plugin in _allModuleEntries)
            {
                if (!IsModuleMatched(plugin, keyword))
                {
                    continue;
                }

                visibleCount++;
                var btn = CreateModuleButton(plugin);
                switch (plugin.ClassId)
                {
                    case "2090_2219_2221":
                        designToolsPanel.Children.Add(btn);
                        break;
                    case "2090_2219_2222":
                        draftingToolsPanel.Children.Add(btn);
                        break;
                    case "2090_2219_2223":
                        commonToolsPanel.Children.Add(btn);
                        break;
                    case "2090_2219_2224":
                        tableToolsPanel.Children.Add(btn);
                        break;
                    default:
                        commonToolsPanel.Children.Add(btn);
                        break;
                }
            }

            moduleSummaryText.Text = $"自研模块 {_allModuleEntries.Count} 个，当前显示 {visibleCount} 个";
            statusText.Text = string.IsNullOrWhiteSpace(keyword)
                ? $"已加载 {_allModuleEntries.Count} 个模块"
                : $"搜索“{keyword}”得到 {visibleCount} 个模块";
        }

        private static bool IsModuleMatched(PluginInfo plugin, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return true;
            }

            return (plugin.Name?.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0 ||
                   (plugin.Explain?.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0;
        }

        private void ModuleSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_allModuleEntries == null) return;
            RenderModuleTabs();
        }

        private void BtnRefreshModules_Click(object sender, RoutedEventArgs e)
        {
            LoadAllPlugins();
        }

        // ========== 大国工匠 - 自定义工具 ==========

        private void LoadCustomTools()
        {
            _customTools = new List<CustomToolEntry>();
            customToolsPanel.Children.Clear();
            customToolsListView.Items.Clear();

            // 1. 加载 tools 目录下自动发现的 exe
            if (Directory.Exists(_toolsDir))
            {
                foreach (var dir in Directory.GetDirectories(_toolsDir))
                {
                    var dirName = Path.GetFileName(dir);
                    var exes = Directory.GetFiles(dir, "*.exe");
                    if (exes.Length > 0)
                    {
                        var entry = new CustomToolEntry { Name = dirName, ExePath = exes[0], IsAutoDiscovered = true };
                        _customTools.Add(entry);
                    }
                }
                foreach (var exe in Directory.GetFiles(_toolsDir, "*.exe"))
                {
                    var name = Path.GetFileNameWithoutExtension(exe);
                    _customTools.Add(new CustomToolEntry { Name = name, ExePath = exe, IsAutoDiscovered = true });
                }
            }

            // 2. 加载手动添加的工具（custom_tools.txt）
            if (File.Exists(_customToolsFile))
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                foreach (var line in File.ReadAllLines(_customToolsFile))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var parts = line.Split('|');
                    if (parts.Length != 2) continue;

                    // 解析路径：相对路径基于 exe 所在目录
                    var exePath = parts[1];
                    if (!Path.IsPathRooted(exePath))
                    {
                        exePath = Path.Combine(baseDir, exePath);
                    }

                    if (!File.Exists(exePath)) continue;

                    // 跳过与自动发现重复的工具
                    bool isDuplicate = false;
                    foreach (var existing in _customTools)
                    {
                        if (string.Equals(existing.ExePath, exePath, StringComparison.OrdinalIgnoreCase))
                        {
                            isDuplicate = true;
                            break;
                        }
                    }
                    if (isDuplicate) continue;

                    _customTools.Add(new CustomToolEntry { Name = parts[0], ExePath = exePath });
                }
            }

            // 填充图标视图和列表视图
            foreach (var entry in _customTools)
            {
                customToolsPanel.Children.Add(CreateCustomToolButton(entry));
                customToolsListView.Items.Add(new { entry.Name, entry.ExePath, Source = entry.IsAutoDiscovered ? "tools" : "手动" });
            }
        }

        private void SaveCustomTools()
        {
            var lines = new List<string>();
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            foreach (var entry in _customTools)
            {
                // 保存相对路径
                var relPath = entry.ExePath;
                if (relPath.StartsWith(baseDir, StringComparison.OrdinalIgnoreCase))
                {
                    relPath = relPath.Substring(baseDir.Length).TrimStart('\\', '/');
                }
                lines.Add($"{entry.Name}|{relPath}");
            }
            File.WriteAllLines(_customToolsFile, lines);
        }

        private Button CreateCustomToolButton(CustomToolEntry entry)
        {
            var btn = new Button
            {
                Style = (Style)FindResource("ModuleButton"),
                Tag = entry,
                Content = new TextBlock
                {
                    Text = entry.Name,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    Foreground = Brushes.White
                }
            };
            btn.Click += CustomToolButton_Click;
            ToolTipService.SetToolTip(btn, entry.ExePath);

            // 右键菜单
            var menu = new ContextMenu();
            var renameItem = new MenuItem { Header = "重命名" };
            renameItem.Click += (s, args) => RenameCustomTool(entry, btn);
            var deleteItem = new MenuItem { Header = "删除" };
            deleteItem.Click += (s, args) => DeleteCustomTool(entry);
            menu.Items.Add(renameItem);
            menu.Items.Add(deleteItem);
            btn.ContextMenu = menu;

            return btn;
        }

        private void RenameCustomTool(CustomToolEntry entry, Button btn)
        {
            var inputWin = new Window
            {
                Title = "重命名",
                Width = 350,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this,
                ResizeMode = ResizeMode.NoResize
            };
            var panel = new StackPanel { Margin = new Thickness(15) };
            panel.Children.Add(new TextBlock { Text = "输入新名称:", Margin = new Thickness(0, 0, 0, 8) });
            var textBox = new TextBox { Text = entry.Name, Margin = new Thickness(0, 0, 0, 12) };
            panel.Children.Add(textBox);
            var btnPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
            var okBtn = new Button { Content = "确定", Width = 70, Margin = new Thickness(0, 0, 10, 0), IsDefault = true };
            var cancelBtn = new Button { Content = "取消", Width = 70, IsCancel = true };
            btnPanel.Children.Add(okBtn);
            btnPanel.Children.Add(cancelBtn);
            panel.Children.Add(btnPanel);
            inputWin.Content = panel;

            textBox.SelectAll();
            textBox.Focus();

            okBtn.Click += (s, args) =>
            {
                var newName = textBox.Text.Trim();
                if (string.IsNullOrEmpty(newName))
                {
                    MessageBox.Show("名称不能为空", "提示");
                    return;
                }

                var oldName = entry.Name;
                entry.Name = newName;

                // 更新按钮文字
                if (btn.Content is TextBlock tb)
                {
                    tb.Text = newName;
                }

                // 如果是 tools 目录下的自动发现工具，重命名文件夹
                if (entry.IsAutoDiscovered)
                {
                    var exeDir = Path.GetDirectoryName(entry.ExePath);
                    var toolsRoot = _toolsDir;
                    // 只有当 exe 在 tools 的子文件夹中时才重命名文件夹
                    if (exeDir != null && Path.GetDirectoryName(exeDir) == toolsRoot)
                    {
                        var newDir = Path.Combine(toolsRoot, newName);
                        if (Directory.Exists(newDir))
                        {
                            MessageBox.Show($"目标文件夹已存在: {newName}", "重命名失败");
                            entry.Name = oldName;
                            if (btn.Content is TextBlock tb2) tb2.Text = oldName;
                            return;
                        }
                        try
                        {
                            Directory.Move(exeDir, newDir);
                            // 更新 exe 路径
                            var exeFileName = Path.GetFileName(entry.ExePath);
                            entry.ExePath = Path.Combine(newDir, exeFileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"重命名文件夹失败: {ex.Message}", "错误");
                            entry.Name = oldName;
                            if (btn.Content is TextBlock tb3) tb3.Text = oldName;
                            return;
                        }
                    }
                }
                else
                {
                    // 手动添加的工具，保存到 custom_tools.txt
                    SaveCustomTools();
                }

                ToolTipService.SetToolTip(btn, entry.ExePath);
                statusText.Text = $"已重命名: {oldName} -> {newName}";
                inputWin.Close();

                // 刷新列表视图
                LoadCustomTools();
            };
            cancelBtn.Click += (s, args) => inputWin.Close();
            inputWin.ShowDialog();
        }

        private void DeleteCustomTool(CustomToolEntry entry)
        {
            var result = MessageBox.Show($"确定删除 \"{entry.Name}\" 吗？", "确认删除",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            if (entry.IsAutoDiscovered)
            {
                // 从 tools 目录删除文件夹
                var exeDir = Path.GetDirectoryName(entry.ExePath);
                if (exeDir != null && Directory.Exists(exeDir) && Path.GetDirectoryName(exeDir) == _toolsDir)
                {
                    try
                    {
                        Directory.Delete(exeDir, true);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"删除文件夹失败: {ex.Message}", "错误");
                        return;
                    }
                }
            }
            else
            {
                _customTools.Remove(entry);
                SaveCustomTools();
            }

            LoadCustomTools();
            statusText.Text = $"已删除: {entry.Name}";
        }

        private void CustomToolButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is CustomToolEntry entry)
            {
                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = entry.ExePath,
                        WorkingDirectory = Path.GetDirectoryName(entry.ExePath)
                    };
                    Process.Start(psi);
                    statusText.Text = $"已启动: {entry.Name}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"启动失败: {ex.Message}", "错误",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnAddCustomTool_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                Title = "选择可执行文件",
                Filter = "可执行文件|*.exe|所有文件|*.*"
            };
            if (dlg.ShowDialog() == true)
            {
                var exePath = dlg.FileName;
                var name = Path.GetFileNameWithoutExtension(exePath);

                foreach (var existing in _customTools)
                {
                    if (string.Equals(existing.ExePath, exePath, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("该程序已添加过了", "提示");
                        return;
                    }
                }

                var entry = new CustomToolEntry { Name = name, ExePath = exePath };
                _customTools.Add(entry);
                customToolsPanel.Children.Add(CreateCustomToolButton(entry));
                SaveCustomTools();
                statusText.Text = $"已添加: {name}";
            }
        }

        private void BtnRemoveCustomTool_Click(object sender, RoutedEventArgs e)
        {
            // Only list manually added tools (not auto-discovered from tools folder)
            var manualTools = new List<int>(); // indices in _customTools
            for (int i = 0; i < _customTools.Count; i++)
            {
                if (!_customTools[i].IsAutoDiscovered)
                    manualTools.Add(i);
            }

            if (manualTools.Count == 0)
            {
                MessageBox.Show("没有手动添加的程序可删除\n（tools 文件夹中的程序请直接删除文件夹）", "提示");
                return;
            }

            var selectWin = new Window
            {
                Title = "选择要删除的程序",
                Width = 300,
                Height = 400,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this
            };
            var listBox = new ListBox { Margin = new Thickness(10) };
            foreach (var idx in manualTools) listBox.Items.Add(_customTools[idx].Name);
            var okBtn = new Button { Content = "删除", Width = 80, Margin = new Thickness(10) };
            var cancelBtn = new Button { Content = "取消", Width = 80, Margin = new Thickness(10) };
            var panel = new StackPanel();
            panel.Children.Add(listBox);
            var btnPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center };
            btnPanel.Children.Add(okBtn);
            btnPanel.Children.Add(cancelBtn);
            panel.Children.Add(btnPanel);
            selectWin.Content = panel;

            okBtn.Click += (s, args) =>
            {
                if (listBox.SelectedIndex >= 0)
                {
                    int realIdx = manualTools[listBox.SelectedIndex];
                    var removed = _customTools[realIdx];
                    _customTools.RemoveAt(realIdx);
                    SaveCustomTools();
                    LoadCustomTools(); // refresh entire panel
                    statusText.Text = $"已删除: {removed.Name}";
                    selectWin.Close();
                }
                else
                {
                    MessageBox.Show("请先选择一个程序", "提示");
                }
            };
            cancelBtn.Click += (s, args) => selectWin.Close();
            selectWin.ShowDialog();
        }

        private void BtnRefreshCustomTools_Click(object sender, RoutedEventArgs e)
        {
            LoadCustomTools();
            statusText.Text = $"已刷新，共 {_customTools.Count} 个工具";
        }

        // ========== 表格工具大全 ==========

        private void LoadTableTools()
        {
            tableToolsPanel.Children.Clear();

            if (!Directory.Exists(_tablesDir))
            {
                tableToolsHint.Text = "tables 目录不存在，正在创建...";
                Directory.CreateDirectory(_tablesDir);
                return;
            }

            var files = new List<string>();
            files.AddRange(Directory.GetFiles(_tablesDir, "*.xlsx"));
            files.AddRange(Directory.GetFiles(_tablesDir, "*.xls"));

            // 也扫描子目录
            foreach (var dir in Directory.GetDirectories(_tablesDir))
            {
                files.AddRange(Directory.GetFiles(dir, "*.xlsx"));
                files.AddRange(Directory.GetFiles(dir, "*.xls"));
            }

            foreach (var file in files)
            {
                tableToolsPanel.Children.Add(CreateTableToolButton(file));
            }

            tableToolsHint.Text = $"共 {files.Count} 个表格文件 | 将 xlsx/xls 放入 tables 文件夹可自动显示";
        }

        private Button CreateTableToolButton(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var fileInfo = new FileInfo(filePath);
            var sizeText = fileInfo.Length > 1024 * 1024
                ? $"{fileInfo.Length / 1024 / 1024.0:F1} MB"
                : $"{fileInfo.Length / 1024.0:F0} KB";

            var btn = new Button
            {
                Style = (Style)FindResource("ModuleButton"),
                Tag = filePath,
                Content = new StackPanel
                {
                    Children =
                    {
                        new TextBlock
                        {
                            Text = fileName,
                            TextWrapping = TextWrapping.Wrap,
                            TextAlignment = TextAlignment.Left,
                            Foreground = Brushes.White,
                            FontSize = 15,
                            FontWeight = FontWeights.Bold
                        },
                        new TextBlock
                        {
                            Text = $"大小: {sizeText}",
                            TextWrapping = TextWrapping.Wrap,
                            TextAlignment = TextAlignment.Left,
                            Foreground = new SolidColorBrush(Color.FromRgb(230, 240, 250)),
                            FontSize = 11,
                            Margin = new Thickness(0, 8, 0, 6)
                        },
                        new TextBlock
                        {
                            Text = "单击打开表格  |  右键打开目录",
                            TextAlignment = TextAlignment.Left,
                            Foreground = new SolidColorBrush(Color.FromRgb(210, 225, 240)),
                            FontSize = 10
                        }
                    }
                }
            };
            btn.Click += TableToolButton_Click;
            ToolTipService.SetToolTip(btn, filePath);

            var menu = new ContextMenu();
            var openFolderItem = new MenuItem { Header = "打开所在目录" };
            openFolderItem.Click += (s, args) =>
            {
                var dir = Path.GetDirectoryName(filePath);
                if (Directory.Exists(dir))
                    Process.Start(new ProcessStartInfo { FileName = dir, UseShellExecute = true });
            };
            menu.Items.Add(openFolderItem);
            btn.ContextMenu = menu;

            return btn;
        }

        private void TableToolButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string filePath)
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true,
                        WorkingDirectory = Path.GetDirectoryName(filePath)
                    });
                    statusText.Text = $"已打开: {Path.GetFileName(filePath)}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"打开表格失败：{ex.Message}", "错误",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnRefreshTableTools_Click(object sender, RoutedEventArgs e)
        {
            LoadTableTools();
        }

        private void ViewMode_Changed(object sender, RoutedEventArgs e)
        {
            if (radListView == null || gridViewScroll == null || customToolsListView == null) return;

            if (radListView.IsChecked == true)
            {
                gridViewScroll.Visibility = Visibility.Collapsed;
                customToolsListView.Visibility = Visibility.Visible;
            }
            else
            {
                gridViewScroll.Visibility = Visibility.Visible;
                customToolsListView.Visibility = Visibility.Collapsed;
            }
        }

        private void CustomToolsListView_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (customToolsListView.SelectedIndex >= 0 && customToolsListView.SelectedIndex < _customTools.Count)
            {
                var entry = _customTools[customToolsListView.SelectedIndex];
                try
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = entry.ExePath,
                        WorkingDirectory = Path.GetDirectoryName(entry.ExePath)
                    };
                    Process.Start(psi);
                    statusText.Text = $"已启动: {entry.Name}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"启动失败: {ex.Message}", "错误",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

    public class CustomToolEntry
    {
        public string Name { get; set; }
        public string ExePath { get; set; }
        public bool IsAutoDiscovered { get; set; }
    }
}
