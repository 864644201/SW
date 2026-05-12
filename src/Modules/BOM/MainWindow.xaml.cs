using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using McBom.Controller;
using McBom.Model;
using McBom.ViewModel;
using Microsoft.Win32;

namespace McBom
{
    public partial class MainWindow : Window
    {
        private readonly BomViewModel _viewModel;
        private readonly BomController _controller;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new BomViewModel();
            _controller = new BomController();
            DataContext = _viewModel;
        }

        private void BtnOpenAssembly_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Title = "打开SolidWorks装配体",
                Filter = "SolidWorks装配体 (*.sldasm)|*.sldasm|所有文件 (*.*)|*.*",
                FilterIndex = 1
            };

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    txtStatus.Text = "正在加载装配体...";
                    _viewModel.LoadBomFromAssembly(dlg.FileName);
                    RefreshDataGrid();
                    BuildAssemblyTree(dlg.FileName);
                    txtStatus.Text = $"已加载: {Path.GetFileName(dlg.FileName)}";
                    txtItemCount.Text = $"共 {_viewModel.BomItems.Count} 项";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"加载装配体失败: {ex.Message}", "错误",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    txtStatus.Text = "加载失败";
                }
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtStatus.Text = "正在刷新BOM...";
                _viewModel.RefreshBom();
                RefreshDataGrid();
                txtStatus.Text = "BOM已刷新";
                txtItemCount.Text = $"共 {_viewModel.FilteredItems.Count} 项";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"刷新失败: {ex.Message}", "错误",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                Title = "导出Excel",
                Filter = "Excel文件 (*.xlsx)|*.xlsx",
                DefaultExt = ".xlsx",
                FileName = $"BOM_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            };

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    txtStatus.Text = "正在导出Excel...";
                    _viewModel.ExportToExcel(dlg.FileName);
                    txtStatus.Text = $"已导出: {Path.GetFileName(dlg.FileName)}";
                    MessageBox.Show("Excel导出成功!", "提示",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"导出失败: {ex.Message}", "错误",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnExportPdf_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                Title = "导出PDF",
                Filter = "PDF文件 (*.pdf)|*.pdf",
                DefaultExt = ".pdf",
                FileName = $"BOM_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
            };

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    txtStatus.Text = "正在导出PDF...";
                    _viewModel.ExportToPdf(dlg.FileName);
                    txtStatus.Text = $"已导出: {Path.GetFileName(dlg.FileName)}";
                    MessageBox.Show("PDF导出成功!", "提示",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"导出失败: {ex.Message}", "错误",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnScreenshot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dlg = new SaveFileDialog
                {
                    Title = "保存截图",
                    Filter = "PNG图片 (*.png)|*.png",
                    DefaultExt = ".png",
                    FileName = $"BOM_Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png"
                };

                if (dlg.ShowDialog() == true)
                {
                    _controller.CaptureScreenshot(this, dlg.FileName);
                    txtStatus.Text = $"截图已保存: {Path.GetFileName(dlg.FileName)}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"截图失败: {ex.Message}", "错误",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            var configWindow = new ConfigWindow(_viewModel.Config)
            {
                Owner = this
            };
            if (configWindow.ShowDialog() == true)
            {
                _viewModel.Config = configWindow.Config;
                RefreshDataGrid();
                txtStatus.Text = "配置已更新";
            }
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SearchText = txtSearch.Text;
            RefreshDataGrid();
        }

        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_viewModel == null) return;
            var selected = (cmbFilter.SelectedItem as ComboBoxItem)?.Content?.ToString();
            _viewModel.ApplyFilter(selected);
            RefreshDataGrid();
        }

        private void CmbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_viewModel == null) return;
            var selected = (cmbGroup.SelectedItem as ComboBoxItem)?.Content?.ToString();
            _viewModel.ApplyGrouping(selected);
            RefreshDataGrid();
        }

        private void TreeAssembly_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TreeViewItem item && item.Tag is BomItem bomItem)
            {
                _viewModel.SelectItem(bomItem);
                UpdatePropertyPanel(bomItem);
                HighlightGridRow(bomItem);
            }
        }

        private void DgBom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgBom.SelectedItem is BomItem item)
            {
                _viewModel.SelectItem(item);
                UpdatePropertyPanel(item);
            }
        }

        private void RefreshDataGrid()
        {
            dgBom.ItemsSource = null;
            dgBom.ItemsSource = _viewModel.FilteredItems;
            txtItemCount.Text = $"共 {_viewModel.FilteredItems.Count} 项";
        }

        private void BuildAssemblyTree(string filePath)
        {
            treeAssembly.Items.Clear();
            var treeNodes = _controller.BuildAssemblyTree(filePath);
            foreach (var node in treeNodes)
            {
                treeAssembly.Items.Add(CreateTreeViewItem(node));
            }
        }

        private TreeViewItem CreateTreeViewItem(BomItem item)
        {
            var tvItem = new TreeViewItem
            {
                Header = $"{item.PartNo} - {item.Name}",
                Tag = item,
                IsExpanded = item.Level == 0
            };

            if (item.IsAssembly)
            {
                tvItem.Foreground = new SolidColorBrush(Colors.DarkBlue);
            }

            foreach (var child in _viewModel.GetChildren(item.ID))
            {
                tvItem.Items.Add(CreateTreeViewItem(child));
            }

            return tvItem;
        }

        private void UpdatePropertyPanel(BomItem item)
        {
            panelProperties.Children.Clear();

            AddPropertyRow("序号", item.ID.ToString());
            AddPropertyRow("零件号", item.PartNo);
            AddPropertyRow("名称", item.Name);
            AddPropertyRow("材料", item.Material ?? "-");
            AddPropertyRow("数量", item.Quantity.ToString());
            AddPropertyRow("单重", $"{item.UnitWeight:F3} kg");
            AddPropertyRow("总重", $"{item.TotalWeight:F3} kg");
            AddPropertyRow("层级", item.Level.ToString());
            AddPropertyRow("类型", item.IsAssembly ? "装配体" : "零件");
            AddPropertyRow("隐藏", item.IsHidden ? "是" : "否");
            AddPropertyRow("文件路径", item.FilePath ?? "-");
            AddPropertyRow("备注", item.Remark ?? "-");
        }

        private void AddPropertyRow(string label, string value)
        {
            var panel = new DockPanel { Margin = new Thickness(5, 3, 5, 3) };
            var lbl = new TextBlock
            {
                Text = label,
                FontWeight = FontWeights.SemiBold,
                Width = 70,
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.Gray)
            };
            var val = new TextBlock
            {
                Text = value,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 12
            };
            DockPanel.SetDock(lbl, Dock.Left);
            panel.Children.Add(lbl);
            panel.Children.Add(val);

            var border = new Border
            {
                BorderBrush = new SolidColorBrush(Color.FromRgb(230, 230, 230)),
                BorderThickness = new Thickness(0, 0, 0, 1),
                Child = panel
            };
            panelProperties.Children.Add(border);
        }

        private void HighlightGridRow(BomItem item)
        {
            dgBom.SelectedItem = item;
            dgBom.ScrollIntoView(item);
        }
    }

    internal class ConfigWindow : Window
    {
        public BomConfig Config { get; private set; }

        public ConfigWindow(BomConfig config)
        {
            Config = config;
            Title = "BOM设置";
            Width = 450;
            Height = 400;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var panel = new StackPanel { Margin = new Thickness(15) };

            var chkHideHidden = new CheckBox
            {
                Content = "隐藏IsHidden标记的项目",
                IsChecked = config.HideHiddenItems,
                Margin = new Thickness(0, 5, 0, 5)
            };

            var chkIncludeWeight = new CheckBox
            {
                Content = "自动计算重量",
                IsChecked = config.AutoCalculateWeight,
                Margin = new Thickness(0, 5, 0, 5)
            };

            var chkIncludeSubAssemblies = new CheckBox
            {
                Content = "包含子装配体零件",
                IsChecked = config.IncludeSubAssemblies,
                Margin = new Thickness(0, 5, 0, 5)
            };

            var lblDensity = new TextBlock { Text = "默认材料密度 (kg/m3):", Margin = new Thickness(0, 10, 0, 3) };
            var txtDensity = new TextBox
            {
                Text = config.DefaultDensity.ToString(),
                Width = 150,
                Margin = new Thickness(0, 0, 0, 5)
            };

            var lblPrefix = new TextBlock { Text = "序号前缀:", Margin = new Thickness(0, 10, 0, 3) };
            var txtPrefix = new TextBox
            {
                Text = config.ItemPrefix ?? "",
                Width = 150,
                Margin = new Thickness(0, 0, 0, 5)
            };

            var btnPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 20, 0, 0)
            };

            var btnOk = new Button
            {
                Content = "确定",
                Width = 80,
                Margin = new Thickness(5, 0, 5, 0),
                IsDefault = true
            };
            btnOk.Click += (s, e) =>
            {
                config.HideHiddenItems = chkHideHidden.IsChecked == true;
                config.AutoCalculateWeight = chkIncludeWeight.IsChecked == true;
                config.IncludeSubAssemblies = chkIncludeSubAssemblies.IsChecked == true;
                double.TryParse(txtDensity.Text, out var density);
                config.DefaultDensity = density > 0 ? density : 7850;
                config.ItemPrefix = txtPrefix.Text;
                Config = config;
                DialogResult = true;
            };

            var btnCancel = new Button
            {
                Content = "取消",
                Width = 80,
                Margin = new Thickness(5, 0, 5, 0),
                IsCancel = true
            };

            btnPanel.Children.Add(btnOk);
            btnPanel.Children.Add(btnCancel);

            panel.Children.Add(chkHideHidden);
            panel.Children.Add(chkIncludeWeight);
            panel.Children.Add(chkIncludeSubAssemblies);
            panel.Children.Add(lblDensity);
            panel.Children.Add(txtDensity);
            panel.Children.Add(lblPrefix);
            panel.Children.Add(txtPrefix);
            panel.Children.Add(btnPanel);

            Content = panel;
        }
    }
}
