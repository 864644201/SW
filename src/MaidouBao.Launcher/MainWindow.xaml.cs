using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using MaidouBao.Plugin;

namespace MaidouBao.Launcher
{
    public partial class MainWindow : Window
    {
        private string _dataDir;
        private string _toolsDir;
        private string _tablesDir;
        private string _booksDir;
        private string _booksCatFile;
        private Dictionary<string, List<string>> _bookCategories; // filePath -> categories
        private string _sortOrderFile;
        private List<string> _sortOrder; // 模块名排序列表
        private List<PluginInfo> _plugins;
        private List<PluginInfo> _allModuleEntries;
        private List<CustomToolEntry> _customTools;
        private string _customToolsFile;
        private Dictionary<string, string> _toolsNameMap;
        private string _manualDir;
        private List<CustomToolEntry> _manualTools;

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
            _booksDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books");
            _booksCatFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books_categories.txt");
            _bookCategories = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            _sortOrderFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tools_sort.txt");
            _sortOrder = new List<string>();
            _customToolsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "custom_tools.txt");
            _manualDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "2008");
            if (!Directory.Exists(_dataDir)) Directory.CreateDirectory(_dataDir);
            if (!Directory.Exists(_toolsDir)) Directory.CreateDirectory(_toolsDir);
            if (!Directory.Exists(_tablesDir)) Directory.CreateDirectory(_tablesDir);
            if (!Directory.Exists(_booksDir)) Directory.CreateDirectory(_booksDir);
            LoadBookCategories();
            LoadSortOrder();
            LoadToolsNameMap();
            LoadAllPlugins();
            LoadCustomTools();
            LoadTableTools();
            LoadBooks();
            LoadManualTools();
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
                            Text = "单击启动  |  右键排序/打开目录",
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
            var topItem = new MenuItem { Header = "置顶" };
            topItem.Click += (sender, args) => MoveModuleSort(plugin.Name, "top");
            var upItem = new MenuItem { Header = "上移" };
            upItem.Click += (sender, args) => MoveModuleSort(plugin.Name, "up");
            var downItem = new MenuItem { Header = "下移" };
            downItem.Click += (sender, args) => MoveModuleSort(plugin.Name, "down");
            var bottomItem = new MenuItem { Header = "置底" };
            bottomItem.Click += (sender, args) => MoveModuleSort(plugin.Name, "bottom");
            var openFolderItem = new MenuItem { Header = "打开模块目录" };
            openFolderItem.Click += (sender, args) => OpenModuleFolder(plugin);
            menu.Items.Add(topItem);
            menu.Items.Add(upItem);
            menu.Items.Add(downItem);
            menu.Items.Add(bottomItem);
            menu.Items.Add(new Separator());
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

        private static string GetCategoryName(string classId)
        {
            switch (classId)
            {
                case "2090_2219_2221":
                case "2090_2219_2222":
                case "2090_2219_2223": return "设计工具";
                case "2090_2219_2224": return "表格工具大全";
                default: return "设计工具";
            }
        }

        private void RenderModuleTabs()
        {
            string keyword = moduleSearchBox?.Text?.Trim();
            bool isSearching = !string.IsNullOrWhiteSpace(keyword);

            // 搜索模式：统一结果视图
            if (isSearching)
            {
                tabControl.Visibility = Visibility.Collapsed;
                searchResultScroll.Visibility = Visibility.Visible;
                searchResultPanel.Children.Clear();

                // 按分类分组（自研模块）
                var groups = new Dictionary<string, List<PluginInfo>>();
                int visibleCount = 0;
                foreach (var plugin in _allModuleEntries)
                {
                    if (!IsModuleMatched(plugin, keyword)) continue;
                    visibleCount++;
                    var cat = GetCategoryName(plugin.ClassId);
                    if (!groups.ContainsKey(cat)) groups[cat] = new List<PluginInfo>();
                    groups[cat].Add(plugin);
                }

                // 搜索机械设计手册
                var matchedManualTools = new List<CustomToolEntry>();
                if (_manualTools != null)
                {
                    foreach (var entry in _manualTools)
                    {
                        if ((entry.Name?.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0)
                        {
                            matchedManualTools.Add(entry);
                            visibleCount++;
                        }
                    }
                }

                // 搜索大国工匠
                var matchedCustomTools = new List<CustomToolEntry>();
                if (_customTools != null)
                {
                    foreach (var entry in _customTools)
                    {
                        if ((entry.Name?.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0)
                        {
                            matchedCustomTools.Add(entry);
                            visibleCount++;
                        }
                    }
                }

                // 搜索书籍
                var matchedBooks = new List<string>();
                if (Directory.Exists(_booksDir))
                {
                    var bookExts = new[] { ".pdf", ".chm", ".djvu", ".epub", ".mobi", ".txt", ".doc", ".docx" };
                    foreach (var file in Directory.GetFiles(_booksDir, "*.*", SearchOption.AllDirectories))
                    {
                        var ext = Path.GetExtension(file).ToLowerInvariant();
                        bool isBook = false;
                        foreach (var e in bookExts) { if (ext == e) { isBook = true; break; } }
                        if (!isBook) continue;
                        var name = Path.GetFileNameWithoutExtension(file);
                        if ((name?.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0)
                        {
                            matchedBooks.Add(file);
                            visibleCount++;
                        }
                    }
                }

                // 机械设计手册分组
                if (matchedManualTools.Count > 0)
                {
                    searchResultPanel.Children.Add(new TextBlock
                    {
                        Text = $"机械设计手册（{matchedManualTools.Count}）",
                        FontSize = 15,
                        FontWeight = FontWeights.Bold,
                        Foreground = new SolidColorBrush(Color.FromRgb(45, 95, 138)),
                        Margin = new Thickness(4, 12, 4, 6)
                    });
                    searchResultPanel.Children.Add(new Border
                    {
                        Height = 1,
                        Background = new SolidColorBrush(Color.FromRgb(220, 225, 230)),
                        Margin = new Thickness(4, 0, 4, 4)
                    });
                    var manualWrap = new WrapPanel { Margin = new Thickness(2, 0, 2, 8) };
                    foreach (var entry in matchedManualTools)
                    {
                        manualWrap.Children.Add(CreateManualToolButton(entry));
                    }
                    searchResultPanel.Children.Add(manualWrap);
                }

                // 按固定顺序渲染分组
                string[] order = { "设计工具", "表格工具大全" };
                foreach (var cat in order)
                {
                    if (!groups.ContainsKey(cat)) continue;
                    // 分组标题
                    var header = new TextBlock
                    {
                        Text = $"{cat}（{groups[cat].Count}）",
                        FontSize = 15,
                        FontWeight = FontWeights.Bold,
                        Foreground = new SolidColorBrush(Color.FromRgb(45, 95, 138)),
                        Margin = new Thickness(4, 12, 4, 6)
                    };
                    searchResultPanel.Children.Add(header);
                    // 分隔线
                    searchResultPanel.Children.Add(new Border
                    {
                        Height = 1,
                        Background = new SolidColorBrush(Color.FromRgb(220, 225, 230)),
                        Margin = new Thickness(4, 0, 4, 4)
                    });
                    // 按钮面板
                    var wrap = new WrapPanel { Margin = new Thickness(2, 0, 2, 8) };
                    foreach (var plugin in groups[cat])
                    {
                        wrap.Children.Add(CreateModuleButton(plugin));
                    }
                    searchResultPanel.Children.Add(wrap);
                }

                // 大国工匠分组
                if (matchedCustomTools.Count > 0)
                {
                    searchResultPanel.Children.Add(new TextBlock
                    {
                        Text = $"大国工匠（{matchedCustomTools.Count}）",
                        FontSize = 15,
                        FontWeight = FontWeights.Bold,
                        Foreground = new SolidColorBrush(Color.FromRgb(45, 95, 138)),
                        Margin = new Thickness(4, 12, 4, 6)
                    });
                    searchResultPanel.Children.Add(new Border
                    {
                        Height = 1,
                        Background = new SolidColorBrush(Color.FromRgb(220, 225, 230)),
                        Margin = new Thickness(4, 0, 4, 4)
                    });
                    var customWrap = new WrapPanel { Margin = new Thickness(2, 0, 2, 8) };
                    foreach (var entry in matchedCustomTools)
                    {
                        customWrap.Children.Add(CreateCustomToolButton(entry));
                    }
                    searchResultPanel.Children.Add(customWrap);
                }

                // 书籍资料分组
                if (matchedBooks.Count > 0)
                {
                    searchResultPanel.Children.Add(new TextBlock
                    {
                        Text = $"书籍资料（{matchedBooks.Count}）",
                        FontSize = 15,
                        FontWeight = FontWeights.Bold,
                        Foreground = new SolidColorBrush(Color.FromRgb(45, 95, 138)),
                        Margin = new Thickness(4, 12, 4, 6)
                    });
                    searchResultPanel.Children.Add(new Border
                    {
                        Height = 1,
                        Background = new SolidColorBrush(Color.FromRgb(220, 225, 230)),
                        Margin = new Thickness(4, 0, 4, 4)
                    });
                    var booksWrap = new WrapPanel { Margin = new Thickness(2, 0, 2, 8) };
                    foreach (var file in matchedBooks)
                    {
                        booksWrap.Children.Add(CreateBookButton(file));
                    }
                    searchResultPanel.Children.Add(booksWrap);
                }

                var manualCount = _manualTools?.Count ?? 0;
                moduleSummaryText.Text = $"自研模块 {_allModuleEntries.Count} 个，手册工具 {manualCount} 个，当前显示 {visibleCount} 个";
                statusText.Text = "搜索\"" + keyword + "\"得到 " + visibleCount + " 个结果";
                return;
            }

            // 非搜索模式：恢复分类 Tab
            tabControl.Visibility = Visibility.Visible;
            searchResultScroll.Visibility = Visibility.Collapsed;

            SortModulesList();
            designToolsPanel.Children.Clear();
            tableToolsPanel.Children.Clear();

            int totalVisible = 0;
            foreach (var plugin in _allModuleEntries)
            {
                totalVisible++;
                var btn = CreateModuleButton(plugin);
                switch (plugin.ClassId)
                {
                    case "2090_2219_2221":
                    case "2090_2219_2222":
                    case "2090_2219_2223":
                        designToolsPanel.Children.Add(btn);
                        break;
                    case "2090_2219_2224":
                        tableToolsPanel.Children.Add(btn);
                        break;
                    default:
                        designToolsPanel.Children.Add(btn);
                        break;
                }
            }

            moduleSummaryText.Text = $"自研模块 {_allModuleEntries.Count} 个";
            statusText.Text = $"已加载 {_allModuleEntries.Count} 个模块";
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

            // 1. 加载 tools 目录下自动发现的 exe（支持嵌套目录）
            if (Directory.Exists(_toolsDir))
            {
                foreach (var dir in Directory.GetDirectories(_toolsDir))
                {
                    var dirName = Path.GetFileName(dir);
                    // 递归查找所有 exe 文件
                    var allExes = Directory.GetFiles(dir, "*.exe", SearchOption.AllDirectories);
                    if (allExes.Length > 0)
                    {
                        // 过滤掉辅助程序
                        string mainExe = null;
                        foreach (var exe in allExes)
                        {
                            var exeName = Path.GetFileName(exe);
                            if (exeName.Equals("pgrun.exe", StringComparison.OrdinalIgnoreCase) ||
                                exeName.Equals("CheckEnvironment.exe", StringComparison.OrdinalIgnoreCase) ||
                                exeName.Equals("MD3DToolsData.exe", StringComparison.OrdinalIgnoreCase) ||
                                exeName.Equals("BoltToolUpdate.exe", StringComparison.OrdinalIgnoreCase) ||
                                exeName.Equals("jrzzRunTest.exe", StringComparison.OrdinalIgnoreCase) ||
                                exeName.Equals("Registered.exe", StringComparison.OrdinalIgnoreCase) ||
                                exeName.Equals("RunSw.exe", StringComparison.OrdinalIgnoreCase) ||
                                exeName.Equals("OpenUrlHelp.exe", StringComparison.OrdinalIgnoreCase) ||
                                exeName.EndsWith(".vshost.exe", StringComparison.OrdinalIgnoreCase))
                                continue;
                            mainExe = exe;
                            break;
                        }
                        if (mainExe == null) mainExe = allExes[0]; // 全是辅助程序时兜底选第一个

                        // 优先从 Plugin.xml 读取中文名称，其次查名称映射表，最后用文件名
                        var exeBaseName = Path.GetFileNameWithoutExtension(mainExe);
                        var displayName = GetPluginName(dir)
                            ?? (_toolsNameMap.TryGetValue(exeBaseName, out var mappedName) ? mappedName : exeBaseName);
                        var entry = new CustomToolEntry { Name = displayName, ExePath = mainExe, IsAutoDiscovered = true };
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

            // 按排序顺序排列
            _customTools.Sort((a, b) =>
            {
                int ia = GetSortIndex(a.Name);
                int ib = GetSortIndex(b.Name);
                return ia.CompareTo(ib);
            });

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
            // 查找工具目录中的图片
            var toolDir = Path.GetDirectoryName(entry.ExePath);
            var imagePath = toolDir != null ? FindToolImage(toolDir) : null;

            UIElement content;
            if (imagePath != null)
            {
                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(imagePath, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.DecodePixelWidth = 200;
                    bitmap.EndInit();
                    bitmap.Freeze();

                    content = new Grid
                    {
                        Children =
                        {
                            new Image
                            {
                                Source = bitmap,
                                Stretch = Stretch.UniformToFill,
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                VerticalAlignment = VerticalAlignment.Stretch
                            },
                            new Border
                            {
                                Background = new SolidColorBrush(Color.FromArgb(160, 0, 0, 0)),
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                VerticalAlignment = VerticalAlignment.Stretch,
                                Child = new TextBlock
                                {
                                    Text = entry.Name,
                                    TextWrapping = TextWrapping.Wrap,
                                    TextAlignment = TextAlignment.Center,
                                    HorizontalAlignment = HorizontalAlignment.Stretch,
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Foreground = Brushes.White,
                                    FontSize = 13,
                                    FontWeight = FontWeights.Bold
                                }
                            }
                        }
                    };
                }
                catch
                {
                    // 图片加载失败，回退到纯文字
                    content = new TextBlock
                    {
                        Text = entry.Name,
                        TextWrapping = TextWrapping.Wrap,
                        TextAlignment = TextAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Center,
                        Foreground = Brushes.White
                    };
                }
            }
            else
            {
                content = new TextBlock
                {
                    Text = entry.Name,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = Brushes.White
                };
            }

            var btn = new Button
            {
                Style = (Style)FindResource("ModuleButton"),
                Tag = entry,
                Content = content
            };
            btn.Click += CustomToolButton_Click;
            ToolTipService.SetToolTip(btn, entry.ExePath);

            // 右键菜单
            var menu = new ContextMenu();
            var topItem = new MenuItem { Header = "置顶" };
            topItem.Click += (s, args) => MoveModuleSort(entry.Name, "top");
            var upItem = new MenuItem { Header = "上移" };
            upItem.Click += (s, args) => MoveModuleSort(entry.Name, "up");
            var downItem = new MenuItem { Header = "下移" };
            downItem.Click += (s, args) => MoveModuleSort(entry.Name, "down");
            var bottomItem = new MenuItem { Header = "置底" };
            bottomItem.Click += (s, args) => MoveModuleSort(entry.Name, "bottom");
            var openFolderItem = new MenuItem { Header = "打开文件夹" };
            openFolderItem.Click += (s, args) =>
            {
                var dir = Path.GetDirectoryName(entry.ExePath);
                if (Directory.Exists(dir))
                    Process.Start(new ProcessStartInfo { FileName = dir, UseShellExecute = true });
            };
            var renameItem = new MenuItem { Header = "重命名" };
            renameItem.Click += (s, args) => RenameCustomTool(entry, btn);
            var deleteItem = new MenuItem { Header = "删除" };
            deleteItem.Click += (s, args) => DeleteCustomTool(entry);
            menu.Items.Add(topItem);
            menu.Items.Add(upItem);
            menu.Items.Add(downItem);
            menu.Items.Add(bottomItem);
            menu.Items.Add(new Separator());
            menu.Items.Add(openFolderItem);
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

        /// <summary>
        /// 加载工具名称映射表 (tools_name_map.txt)
        /// </summary>
        private void LoadToolsNameMap()
        {
            _toolsNameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var mapFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tools_name_map.txt");
            if (!File.Exists(mapFile)) return;
            foreach (var line in File.ReadAllLines(mapFile))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split('|');
                if (parts.Length == 2 && !string.IsNullOrWhiteSpace(parts[0]) && !string.IsNullOrWhiteSpace(parts[1]))
                {
                    _toolsNameMap[parts[0].Trim()] = parts[1].Trim();
                }
            }
        }

        /// <summary>
        /// 从目录或子目录中的 Plugin.xml 读取中文名称
        /// </summary>
        private string GetPluginName(string dir)
        {
            // 先在当前目录找 Plugin.xml
            var pluginXml = Path.Combine(dir, "Plugin.xml");
            if (File.Exists(pluginXml))
            {
                return ReadPluginXmlName(pluginXml);
            }
            // 再在子目录找
            foreach (var subDir in Directory.GetDirectories(dir))
            {
                pluginXml = Path.Combine(subDir, "Plugin.xml");
                if (File.Exists(pluginXml))
                {
                    return ReadPluginXmlName(pluginXml);
                }
            }
            return null;
        }

        private string ReadPluginXmlName(string xmlPath)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(xmlPath);
                var nameNode = doc.SelectSingleNode("//name");
                return nameNode?.InnerText?.Trim();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 在工具目录中查找代表性图片作为按钮背景
        /// </summary>
        private string FindToolImage(string dir)
        {
            var imageExts = new[] { ".png", ".jpg", ".jpeg", ".bmp" };
            // 优先查找 icon/logo/preview 等命名的图片（含子目录）
            foreach (var ext in imageExts)
            {
                foreach (var name in new[] { "icon", "logo", "preview", "thumb", "banner", "bg" })
                {
                    var path = Path.Combine(dir, name + ext);
                    if (File.Exists(path)) return path;
                }
            }
            // 在根目录找第一张图片
            foreach (var ext in imageExts)
            {
                var files = Directory.GetFiles(dir, "*" + ext, SearchOption.TopDirectoryOnly);
                if (files.Length > 0) return files[0];
            }
            // 递归搜索子目录，优先找名为 image.png 的文件，限制深度避免太慢
            try
            {
                foreach (var subDir in Directory.GetDirectories(dir))
                {
                    // 第二层：直接子目录
                    foreach (var ext in imageExts)
                    {
                        var imgPath = Path.Combine(subDir, "image" + ext);
                        if (File.Exists(imgPath)) return imgPath;
                        var files = Directory.GetFiles(subDir, "*" + ext, SearchOption.TopDirectoryOnly);
                        if (files.Length > 0) return files[0];
                    }
                    // 第三层：再深一层
                    foreach (var subSubDir in Directory.GetDirectories(subDir))
                    {
                        foreach (var ext in imageExts)
                        {
                            var imgPath = Path.Combine(subSubDir, "image" + ext);
                            if (File.Exists(imgPath)) return imgPath;
                            var files = Directory.GetFiles(subSubDir, "*" + ext, SearchOption.TopDirectoryOnly);
                            if (files.Length > 0) return files[0];
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// 创建带图片背景的大国工匠按钮
        /// </summary>

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

        // ========== 机械设计手册 ==========

        private void LoadManualTools()
        {
            _manualTools = new List<CustomToolEntry>();
            manualToolsPanel.Children.Clear();

            if (!Directory.Exists(_manualDir))
            {
                manualToolsHint.Text = "2008 目录不存在";
                return;
            }

            foreach (var dir in Directory.GetDirectories(_manualDir))
            {
                var dirName = Path.GetFileName(dir);
                var allExes = Directory.GetFiles(dir, "*.exe", SearchOption.AllDirectories);
                if (allExes.Length == 0) continue;

                // 优先选择与目录名匹配的中文 exe
                string mainExe = null;
                foreach (var exe in allExes)
                {
                    var exeBaseName = Path.GetFileNameWithoutExtension(exe);
                    if (exeBaseName.Equals(dirName, StringComparison.OrdinalIgnoreCase))
                    {
                        mainExe = exe;
                        break;
                    }
                }
                // 其次选中文命名的 exe（包含中文字符）
                if (mainExe == null)
                {
                    foreach (var exe in allExes)
                    {
                        var exeBaseName = Path.GetFileNameWithoutExtension(exe);
                        bool hasChinese = false;
                        foreach (char c in exeBaseName) { if (c >= 0x4e00 && c <= 0x9fff) { hasChinese = true; break; } }
                        if (hasChinese) { mainExe = exe; break; }
                    }
                }
                // 兜底选第一个
                if (mainExe == null) mainExe = allExes[0];

                var entry = new CustomToolEntry { Name = dirName, ExePath = mainExe, IsAutoDiscovered = true };
                _manualTools.Add(entry);
            }

            // 按排序顺序排列
            _manualTools.Sort((a, b) =>
            {
                int ia = GetSortIndex(a.Name);
                int ib = GetSortIndex(b.Name);
                return ia.CompareTo(ib);
            });

            foreach (var entry in _manualTools)
            {
                manualToolsPanel.Children.Add(CreateManualToolButton(entry));
            }

            manualToolsHint.Text = $"共 {_manualTools.Count} 个手册工具";
        }

        private Button CreateManualToolButton(CustomToolEntry entry)
        {
            var btn = new Button
            {
                Style = (Style)FindResource("ModuleButton"),
                Tag = entry,
                Content = new StackPanel
                {
                    Children =
                    {
                        new TextBlock
                        {
                            Text = entry.Name,
                            TextWrapping = TextWrapping.Wrap,
                            TextAlignment = TextAlignment.Left,
                            Foreground = Brushes.White,
                            FontSize = 15,
                            FontWeight = FontWeights.Bold
                        },
                        new TextBlock
                        {
                            Text = "机械设计手册",
                            TextWrapping = TextWrapping.Wrap,
                            TextAlignment = TextAlignment.Left,
                            Foreground = new SolidColorBrush(Color.FromRgb(230, 240, 250)),
                            FontSize = 11,
                            Margin = new Thickness(0, 8, 0, 6)
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
            btn.Click += ManualToolButton_Click;
            ToolTipService.SetToolTip(btn, entry.ExePath);

            var menu = new ContextMenu();
            var topItem = new MenuItem { Header = "置顶" };
            topItem.Click += (s, args) => MoveModuleSort(entry.Name, "top");
            var upItem = new MenuItem { Header = "上移" };
            upItem.Click += (s, args) => MoveModuleSort(entry.Name, "up");
            var downItem = new MenuItem { Header = "下移" };
            downItem.Click += (s, args) => MoveModuleSort(entry.Name, "down");
            var bottomItem = new MenuItem { Header = "置底" };
            bottomItem.Click += (s, args) => MoveModuleSort(entry.Name, "bottom");
            var openFolderItem = new MenuItem { Header = "打开文件夹" };
            openFolderItem.Click += (s, args) =>
            {
                var dir = Path.GetDirectoryName(entry.ExePath);
                if (Directory.Exists(dir))
                    Process.Start(new ProcessStartInfo { FileName = dir, UseShellExecute = true });
            };
            menu.Items.Add(topItem);
            menu.Items.Add(upItem);
            menu.Items.Add(downItem);
            menu.Items.Add(bottomItem);
            menu.Items.Add(new Separator());
            menu.Items.Add(openFolderItem);
            btn.ContextMenu = menu;

            return btn;
        }

        private void ManualToolButton_Click(object sender, RoutedEventArgs e)
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

        private void BtnRefreshManualTools_Click(object sender, RoutedEventArgs e)
        {
            LoadManualTools();
            statusText.Text = $"已刷新机械设计手册，共 {_manualTools.Count} 个工具";
        }

        // ========== 模块排序 ==========

        private void LoadSortOrder()
        {
            _sortOrder.Clear();
            if (!File.Exists(_sortOrderFile)) return;
            foreach (var line in File.ReadAllLines(_sortOrderFile))
            {
                var name = line.Trim();
                if (name.Length > 0) _sortOrder.Add(name);
            }
        }

        private void SaveSortOrder()
        {
            File.WriteAllLines(_sortOrderFile, _sortOrder);
        }

        private int GetSortIndex(string name)
        {
            for (int i = 0; i < _sortOrder.Count; i++)
                if (string.Equals(_sortOrder[i], name, StringComparison.OrdinalIgnoreCase))
                    return i;
            return int.MaxValue; // 未排序的排最后
        }

        private void SortModulesList()
        {
            _allModuleEntries.Sort((a, b) =>
            {
                int ia = GetSortIndex(a.Name);
                int ib = GetSortIndex(b.Name);
                return ia.CompareTo(ib);
            });
        }

        private void MoveModuleSort(string name, string direction)
        {
            // 确保所有模块都在排序列表中
            foreach (var p in _allModuleEntries)
            {
                bool found = false;
                foreach (var s in _sortOrder)
                    if (string.Equals(s, p.Name, StringComparison.OrdinalIgnoreCase)) { found = true; break; }
                if (!found) _sortOrder.Add(p.Name);
            }

            int idx = -1;
            for (int i = 0; i < _sortOrder.Count; i++)
                if (string.Equals(_sortOrder[i], name, StringComparison.OrdinalIgnoreCase)) { idx = i; break; }
            if (idx < 0) return;

            switch (direction)
            {
                case "top":
                    _sortOrder.RemoveAt(idx);
                    _sortOrder.Insert(0, name);
                    break;
                case "up":
                    if (idx > 0) { _sortOrder.RemoveAt(idx); _sortOrder.Insert(idx - 1, name); }
                    break;
                case "down":
                    if (idx < _sortOrder.Count - 1) { _sortOrder.RemoveAt(idx); _sortOrder.Insert(idx + 1, name); }
                    break;
                case "bottom":
                    _sortOrder.RemoveAt(idx);
                    _sortOrder.Add(name);
                    break;
            }
            SaveSortOrder();
            SortModulesList();
            RenderModuleTabs();
            LoadCustomTools();
        }

        // ========== 书籍资料 ==========

        private void LoadBookCategories()
        {
            _bookCategories.Clear();
            if (!File.Exists(_booksCatFile)) return;
            foreach (var line in File.ReadAllLines(_booksCatFile))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var sep = line.IndexOf('|');
                if (sep < 0) continue;
                var path = line.Substring(0, sep).Trim();
                var cats = line.Substring(sep + 1).Split(',');
                var list = new List<string>();
                foreach (var c in cats) { var t = c.Trim(); if (t.Length > 0) list.Add(t); }
                if (list.Count > 0) _bookCategories[path] = list;
            }
        }

        private void SaveBookCategories()
        {
            var lines = new List<string>();
            foreach (var kv in _bookCategories)
            {
                if (kv.Value.Count > 0)
                    lines.Add(kv.Key + "|" + string.Join(",", kv.Value));
            }
            File.WriteAllLines(_booksCatFile, lines);
        }

        private List<string> GetAllCategories()
        {
            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var kv in _bookCategories)
                foreach (var c in kv.Value) set.Add(c);
            var list = new List<string>(set);
            list.Sort(StringComparer.OrdinalIgnoreCase);
            return list;
        }

        private string _booksFilterCat = null; // null = 全部, "" = 未分类

        private void LoadBooks()
        {
            booksPanel.Children.Clear();

            if (!Directory.Exists(_booksDir))
            {
                booksHint.Text = "books 目录不存在，正在创建...";
                Directory.CreateDirectory(_booksDir);
                return;
            }

            var bookExts = new[] { ".pdf", ".chm", ".djvu", ".epub", ".mobi", ".txt", ".doc", ".docx" };
            var allFiles = new List<string>();
            foreach (var file in Directory.GetFiles(_booksDir, "*.*", SearchOption.AllDirectories))
            {
                var ext = Path.GetExtension(file).ToLowerInvariant();
                foreach (var e in bookExts) { if (ext == e) { allFiles.Add(file); break; } }
            }

            allFiles.Sort((a, b) => string.Compare(Path.GetFileName(a), Path.GetFileName(b), StringComparison.OrdinalIgnoreCase));

            // 按分类筛选
            var files = new List<string>();
            foreach (var f in allFiles)
            {
                if (_booksFilterCat == null) { files.Add(f); continue; }
                var cats = _bookCategories.ContainsKey(f) ? _bookCategories[f] : null;
                if (_booksFilterCat == "")
                {
                    if (cats == null || cats.Count == 0) files.Add(f);
                }
                else
                {
                    if (cats != null)
                        foreach (var c in cats)
                            if (string.Equals(c, _booksFilterCat, StringComparison.OrdinalIgnoreCase))
                            { files.Add(f); break; }
                }
            }

            // 按分类分组显示
            if (_booksFilterCat == null)
            {
                // 全部模式：按分类分组
                var grouped = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
                var uncategorized = new List<string>();
                foreach (var f in files)
                {
                    var cats = _bookCategories.ContainsKey(f) ? _bookCategories[f] : null;
                    if (cats == null || cats.Count == 0) { uncategorized.Add(f); continue; }
                    foreach (var c in cats)
                    {
                        if (!grouped.ContainsKey(c)) grouped[c] = new List<string>();
                        grouped[c].Add(f);
                    }
                }
                var catList = new List<string>(grouped.Keys);
                catList.Sort(StringComparer.OrdinalIgnoreCase);
                foreach (var cat in catList)
                {
                    booksPanel.Children.Add(new TextBlock
                    {
                        Text = $"{cat}（{grouped[cat].Count}）",
                        FontSize = 14, FontWeight = FontWeights.Bold,
                        Foreground = new SolidColorBrush(Color.FromRgb(45, 95, 138)),
                        Margin = new Thickness(4, 12, 4, 4)
                    });
                    booksPanel.Children.Add(new Border
                    {
                        Height = 1,
                        Background = new SolidColorBrush(Color.FromRgb(220, 225, 230)),
                        Margin = new Thickness(4, 0, 4, 4)
                    });
                    var wrap = new WrapPanel { Margin = new Thickness(2, 0, 2, 8) };
                    foreach (var f in grouped[cat]) wrap.Children.Add(CreateBookButton(f));
                    booksPanel.Children.Add(wrap);
                }
                if (uncategorized.Count > 0)
                {
                    booksPanel.Children.Add(new TextBlock
                    {
                        Text = $"未分类（{uncategorized.Count}）",
                        FontSize = 14, FontWeight = FontWeights.Bold,
                        Foreground = new SolidColorBrush(Color.FromRgb(140, 140, 140)),
                        Margin = new Thickness(4, 12, 4, 4)
                    });
                    booksPanel.Children.Add(new Border
                    {
                        Height = 1,
                        Background = new SolidColorBrush(Color.FromRgb(220, 225, 230)),
                        Margin = new Thickness(4, 0, 4, 4)
                    });
                    var wrap = new WrapPanel { Margin = new Thickness(2, 0, 2, 8) };
                    foreach (var f in uncategorized) wrap.Children.Add(CreateBookButton(f));
                    booksPanel.Children.Add(wrap);
                }
            }
            else
            {
                // 筛选模式：平铺
                foreach (var f in files) booksPanel.Children.Add(CreateBookButton(f));
            }

            // 刷新分类筛选栏
            RefreshBookCategoryBar();

            var subDirCount = Directory.GetDirectories(_booksDir).Length;
            var filterText = _booksFilterCat == null ? "全部" : _booksFilterCat == "" ? "未分类" : _booksFilterCat;
            booksHint.Text = $"显示: {filterText}（{files.Count}/{allFiles.Count}）| 将文件放入 books 文件夹可自动显示";
        }

        private void RefreshBookCategoryBar()
        {
            bookCategoryBar.Children.Clear();

            // "全部"按钮
            var allBtn = new Button
            {
                Content = "全部",
                Padding = new Thickness(10, 3, 10, 3),
                Margin = new Thickness(0, 0, 6, 0),
                FontWeight = _booksFilterCat == null ? FontWeights.Bold : FontWeights.Normal,
                Background = _booksFilterCat == null
                    ? new SolidColorBrush(Color.FromRgb(45, 95, 138))
                    : new SolidColorBrush(Color.FromRgb(220, 225, 230)),
                Foreground = _booksFilterCat == null ? Brushes.White : Brushes.Black
            };
            allBtn.Click += (s, args) => { _booksFilterCat = null; LoadBooks(); };
            bookCategoryBar.Children.Add(allBtn);

            // "未分类"按钮
            var uncatBtn = new Button
            {
                Content = "未分类",
                Padding = new Thickness(10, 3, 10, 3),
                Margin = new Thickness(0, 0, 6, 0),
                FontWeight = _booksFilterCat == "" ? FontWeights.Bold : FontWeights.Normal,
                Background = _booksFilterCat == ""
                    ? new SolidColorBrush(Color.FromRgb(45, 95, 138))
                    : new SolidColorBrush(Color.FromRgb(220, 225, 230)),
                Foreground = _booksFilterCat == "" ? Brushes.White : Brushes.Black
            };
            uncatBtn.Click += (s, args) => { _booksFilterCat = ""; LoadBooks(); };
            bookCategoryBar.Children.Add(uncatBtn);

            // 各分类按钮
            foreach (var cat in GetAllCategories())
            {
                var catBtn = new Button
                {
                    Content = cat,
                    Padding = new Thickness(10, 3, 10, 3),
                    Margin = new Thickness(0, 0, 6, 0),
                    FontWeight = string.Equals(_booksFilterCat, cat, StringComparison.OrdinalIgnoreCase) ? FontWeights.Bold : FontWeights.Normal,
                    Background = string.Equals(_booksFilterCat, cat, StringComparison.OrdinalIgnoreCase)
                        ? new SolidColorBrush(Color.FromRgb(45, 95, 138))
                        : new SolidColorBrush(Color.FromRgb(220, 225, 230)),
                    Foreground = string.Equals(_booksFilterCat, cat, StringComparison.OrdinalIgnoreCase) ? Brushes.White : Brushes.Black
                };
                var capturedCat = cat;
                catBtn.Click += (s, args) => { _booksFilterCat = capturedCat; LoadBooks(); };
                bookCategoryBar.Children.Add(catBtn);
            }
        }

        private Button CreateBookButton(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var ext = Path.GetExtension(filePath).TrimStart('.').ToUpperInvariant();
            var fileInfo = new FileInfo(filePath);
            var sizeText = fileInfo.Length > 1024 * 1024
                ? $"{fileInfo.Length / 1024 / 1024.0:F1} MB"
                : $"{fileInfo.Length / 1024.0:F0} KB";

            // 相对于 books 目录的路径
            var relPath = filePath;
            if (filePath.StartsWith(_booksDir, StringComparison.OrdinalIgnoreCase))
                relPath = filePath.Substring(_booksDir.Length).TrimStart('\\', '/');

            // 当前分类标签
            var cats = _bookCategories.ContainsKey(filePath) ? _bookCategories[filePath] : null;
            var catText = (cats != null && cats.Count > 0) ? string.Join(" / ", cats) : "未分类";
            var catColor = (cats != null && cats.Count > 0)
                ? Color.FromRgb(100, 200, 140)
                : Color.FromRgb(160, 160, 160);

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
                            FontWeight = FontWeights.Bold,
                            MaxHeight = 40
                        },
                        new TextBlock
                        {
                            Text = $"{ext}  |  {sizeText}",
                            TextWrapping = TextWrapping.Wrap,
                            TextAlignment = TextAlignment.Left,
                            Foreground = new SolidColorBrush(Color.FromRgb(230, 240, 250)),
                            FontSize = 11,
                            Margin = new Thickness(0, 8, 0, 4)
                        },
                        new TextBlock
                        {
                            Text = catText,
                            TextWrapping = TextWrapping.Wrap,
                            TextAlignment = TextAlignment.Left,
                            Foreground = new SolidColorBrush(catColor),
                            FontSize = 10,
                            Margin = new Thickness(0, 0, 0, 4)
                        }
                    }
                }
            };
            btn.Click += BookButton_Click;
            ToolTipService.SetToolTip(btn, filePath);

            var menu = new ContextMenu();
            var catItem = new MenuItem { Header = "设置分类..." };
            catItem.Click += (s, args) => ShowBookCategoryDialog(filePath, btn);
            var openFolderItem = new MenuItem { Header = "打开所在目录" };
            openFolderItem.Click += (s, args) =>
            {
                var dir = Path.GetDirectoryName(filePath);
                if (Directory.Exists(dir))
                    Process.Start(new ProcessStartInfo { FileName = dir, UseShellExecute = true });
            };
            menu.Items.Add(catItem);
            menu.Items.Add(openFolderItem);
            btn.ContextMenu = menu;

            return btn;
        }

        private void ShowBookCategoryDialog(string filePath, Button btn)
        {
            var allCats = GetAllCategories();
            var currentCats = _bookCategories.ContainsKey(filePath) ? new List<string>(_bookCategories[filePath]) : new List<string>();

            var win = new Window
            {
                Title = "设置书籍分类",
                Width = 420,
                Height = 400,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = this,
                ResizeMode = ResizeMode.NoResize
            };

            var panel = new StackPanel { Margin = new Thickness(15) };
            panel.Children.Add(new TextBlock
            {
                Text = $"《{Path.GetFileNameWithoutExtension(filePath)}》",
                FontWeight = FontWeights.Bold,
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 10)
            });
            panel.Children.Add(new TextBlock
            {
                Text = "勾选分类（可多选）:",
                Margin = new Thickness(0, 0, 0, 8)
            });

            // 已有分类的 CheckBox 列表
            var checkBoxes = new List<CheckBox>();
            var checkBoxPanel = new StackPanel { MaxHeight = 180 };
            var scrollViewer = new ScrollViewer { Content = checkBoxPanel, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, MaxHeight = 180 };
            foreach (var cat in allCats)
            {
                var cb = new CheckBox
                {
                    Content = cat,
                    IsChecked = currentCats.Exists(c => string.Equals(c, cat, StringComparison.OrdinalIgnoreCase)),
                    Margin = new Thickness(0, 2, 0, 2),
                    FontSize = 13
                };
                checkBoxes.Add(cb);
                checkBoxPanel.Children.Add(cb);
            }
            panel.Children.Add(scrollViewer);

            // 新增分类
            panel.Children.Add(new TextBlock
            {
                Text = "新增分类:",
                Margin = new Thickness(0, 12, 0, 4)
            });
            var newCatPanel = new StackPanel { Orientation = Orientation.Horizontal };
            var newCatBox = new TextBox { Width = 240, VerticalContentAlignment = VerticalAlignment.Center, Height = 28 };
            var addCatBtn = new Button { Content = "添加", Padding = new Thickness(12, 2, 12, 2), Margin = new Thickness(8, 0, 0, 0) };
            newCatPanel.Children.Add(newCatBox);
            newCatPanel.Children.Add(addCatBtn);
            panel.Children.Add(newCatPanel);

            addCatBtn.Click += (s, args) =>
            {
                var newCat = newCatBox.Text.Trim();
                if (string.IsNullOrEmpty(newCat)) return;
                // 检查是否已存在
                foreach (var cb in checkBoxes)
                    if (string.Equals(cb.Content as string, newCat, StringComparison.OrdinalIgnoreCase))
                    { cb.IsChecked = true; newCatBox.Text = ""; return; }
                // 新增
                var cb2 = new CheckBox { Content = newCat, IsChecked = true, Margin = new Thickness(0, 2, 0, 2), FontSize = 13 };
                checkBoxes.Add(cb2);
                checkBoxPanel.Children.Add(cb2);
                newCatBox.Text = "";
            };

            // 确定/取消
            var btnPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 15, 0, 0) };
            var okBtn = new Button { Content = "确定", Width = 70, Margin = new Thickness(0, 0, 10, 0), IsDefault = true };
            var cancelBtn = new Button { Content = "取消", Width = 70, IsCancel = true };
            btnPanel.Children.Add(okBtn);
            btnPanel.Children.Add(cancelBtn);
            panel.Children.Add(btnPanel);

            win.Content = panel;

            okBtn.Click += (s, args) =>
            {
                var selected = new List<string>();
                foreach (var cb in checkBoxes)
                    if (cb.IsChecked == true) selected.Add(cb.Content as string);

                if (selected.Count > 0)
                    _bookCategories[filePath] = selected;
                else if (_bookCategories.ContainsKey(filePath))
                    _bookCategories.Remove(filePath);

                SaveBookCategories();
                LoadBooks();
                win.Close();
            };
            cancelBtn.Click += (s, args) => win.Close();
            win.ShowDialog();
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string filePath)
            {
                try
                {
                    var reader = new BookReaderWindow(filePath) { Owner = this };
                    reader.Show();
                    statusText.Text = $"已打开: {Path.GetFileName(filePath)}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"打开文档失败：{ex.Message}", "错误",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnRefreshBooks_Click(object sender, RoutedEventArgs e)
        {
            LoadBooks();
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
