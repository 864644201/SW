using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace PrintLayout
{
    public partial class MainForm : Form
    {
        private readonly List<DrawingItem> _drawings = new List<DrawingItem>();
        private LayoutResult _currentLayout;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string path in openFileDialog.FileNames)
                {
                    if (File.Exists(path))
                    {
                        var item = new DrawingItem
                        {
                            FilePath = path,
                            FileName = Path.GetFileName(path),
                        };

                        // Try to load image to get dimensions
                        try
                        {
                            using (var img = Image.FromFile(path))
                            {
                                item.WidthMM = img.Width * 25.4 / img.HorizontalResolution;
                                item.HeightMM = img.Height * 25.4 / img.VerticalResolution;
                            }
                        }
                        catch
                        {
                            // Not an image file, use default dimensions
                            item.WidthMM = 420;
                            item.HeightMM = 297;
                        }

                        _drawings.Add(item);
                        lstFiles.Items.Add(item.FileName);
                    }
                }

                UpdateFileCount();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstFiles.SelectedIndex >= 0)
            {
                int idx = lstFiles.SelectedIndex;
                _drawings.RemoveAt(idx);
                lstFiles.Items.RemoveAt(idx);
                UpdateFileCount();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            _drawings.Clear();
            lstFiles.Items.Clear();
            UpdateFileCount();
        }

        private void UpdateFileCount()
        {
            lblFileCount.Text = $"已选择 {_drawings.Count} 个文件";
        }

        private void cboArrange_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isManual = cboArrange.SelectedIndex == 3;
            numRows.Enabled = isManual;
            numCols.Enabled = isManual;
        }

        private void SettingsChanged(object sender, EventArgs e)
        {
            // Nothing to do automatically; user clicks preview to update
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                _currentLayout = CalculateLayout();
                DrawPreview();
                UpdateLayoutInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("预览出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private LayoutResult CalculateLayout()
        {
            PaperType paper = cboPaper.SelectedIndex == 0 ? PaperType.A3 : PaperType.A4;

            ArrangeMode mode;
            switch (cboArrange.SelectedIndex)
            {
                case 0: mode = ArrangeMode.AutoFit; break;
                case 1: mode = ArrangeMode.TwoByOne; break;
                case 2: mode = ArrangeMode.TwoByTwo; break;
                case 3: mode = ArrangeMode.Manual; break;
                default: mode = ArrangeMode.AutoFit; break;
            }

            double dw = 420, dh = 297;
            double.TryParse(txtDW.Text, out dw);
            double.TryParse(txtDH.Text, out dh);

            double margin = 10, spacing = 5;
            double.TryParse(txtMargin.Text, out margin);
            double.TryParse(txtSpacing.Text, out spacing);

            return PrintLayoutEngine.Calculate(
                paper, mode, Math.Max(1, _drawings.Count),
                dw, dh, margin, spacing,
                (int)numRows.Value, (int)numCols.Value);
        }

        private void DrawPreview()
        {
            if (_currentLayout == null) return;

            // Scale: 1mm = 3px for preview
            const float scale = 3.0f;
            float pw = _currentLayout.PaperSizeMM.Width * scale;
            float ph = _currentLayout.PaperSizeMM.Height * scale;

            var bmp = new Bitmap((int)pw, (int)ph);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.PageUnit = GraphicsUnit.Pixel;

                using (var borderPen = new Pen(Color.Black, 2))
                using (var gridPen = new Pen(Color.LightGray, 1))
                using (var textBrush = new SolidBrush(Color.Blue))
                using (var font = new Font("Microsoft YaHei", 9))
                {
                    // Draw paper border
                    g.DrawRectangle(borderPen, 0, 0, pw - 1, ph - 1);

                    // Draw slot grid
                    for (int row = 0; row < _currentLayout.Rows; row++)
                    {
                        for (int col = 0; col < _currentLayout.Cols; col++)
                        {
                            float x = (float)(_currentLayout.MarginMM + col * (_currentLayout.SlotWidthMM + _currentLayout.SpacingMM)) * scale;
                            float y = (float)(_currentLayout.MarginMM + row * (_currentLayout.SlotHeightMM + _currentLayout.SpacingMM)) * scale;
                            float w = (float)_currentLayout.SlotWidthMM * scale;
                            float h = (float)_currentLayout.SlotHeightMM * scale;

                            g.DrawRectangle(gridPen, x, y, w, h);

                            // Draw slot number
                            int slotIndex = row * _currentLayout.Cols + col;
                            if (slotIndex < _drawings.Count)
                            {
                                string label = (slotIndex + 1).ToString();
                                g.DrawString(label, font, textBrush, x + 3, y + 3);

                                // Draw a cross to indicate drawing area
                                g.DrawLine(gridPen, x, y, x + w, y + h);
                                g.DrawLine(gridPen, x + w, y, x, y + h);

                                // Draw filename
                                var fname = _drawings[slotIndex].FileName;
                                if (fname.Length > 20) fname = fname.Substring(0, 17) + "...";
                                var textSize = g.MeasureString(fname, font);
                                g.DrawString(fname, font, textBrush,
                                    x + (w - textSize.Width) / 2, y + h - textSize.Height - 3);
                            }
                        }
                    }
                }
            }

            if (picPreview.Image != null)
            {
                picPreview.Image.Dispose();
            }
            picPreview.Image = bmp;
        }

        private void UpdateLayoutInfo()
        {
            if (_currentLayout == null) return;

            lblLayoutInfo.Text =
                $"纸张: {_currentLayout.PaperSizeMM.Width:F0}x{_currentLayout.PaperSizeMM.Height:F0} mm\n" +
                $"排列: {_currentLayout.Cols} 列 x {_currentLayout.Rows} 行 = {_currentLayout.ItemsPerPage} 份/页\n" +
                $"槽位: {_currentLayout.SlotWidthMM:F1} x {_currentLayout.SlotHeightMM:F1} mm\n" +
                $"总页数: {_currentLayout.TotalPages} 页 (共 {_drawings.Count} 份图纸)";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_drawings.Count == 0)
            {
                MessageBox.Show("请先添加图纸文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_currentLayout == null)
            {
                _currentLayout = CalculateLayout();
            }

            PaperType paper = cboPaper.SelectedIndex == 0 ? PaperType.A3 : PaperType.A4;
            PrintLayoutEngine.ConfigurePageSettings(printDocument, paper);

            printDocument.PrintPage -= PrintDocument_PrintPage;
            printDocument.PrintPage += PrintDocument_PrintPage;

            printPreviewDialog.ShowDialog();
        }

        private int _printPageIndex;

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (_currentLayout == null || _drawings.Count == 0) return;

            var g = e.Graphics;
            var pageBounds = e.MarginBounds;

            // Compute scale: map mm to printer units (hundredths of inch)
            float scaleX = pageBounds.Width / (float)_currentLayout.PrintableAreaMM.Width;
            float scaleY = pageBounds.Height / (float)_currentLayout.PrintableAreaMM.Height;
            float printScale = Math.Min(scaleX, scaleY);

            g.TranslateTransform(pageBounds.Left, pageBounds.Top);

            using (var gridPen = new Pen(Color.LightGray, 0.5f))
            using (var borderPen = new Pen(Color.Black, 1))
            using (var textFont = new Font("Arial", 8))
            using (var textBrush = new SolidBrush(Color.Black))
            {
                for (int row = 0; row < _currentLayout.Rows; row++)
                {
                    for (int col = 0; col < _currentLayout.Cols; col++)
                    {
                        int slotIndex = _printPageIndex * _currentLayout.ItemsPerPage + row * _currentLayout.Cols + col;
                        if (slotIndex >= _drawings.Count) continue;

                        float x = (float)(_currentLayout.MarginMM + col * (_currentLayout.SlotWidthMM + _currentLayout.SpacingMM)) * printScale;
                        float y = (float)(_currentLayout.MarginMM + row * (_currentLayout.SlotHeightMM + _currentLayout.SpacingMM)) * printScale;
                        float w = (float)_currentLayout.SlotWidthMM * printScale;
                        float h = (float)_currentLayout.SlotHeightMM * printScale;

                        g.DrawRectangle(gridPen, x, y, w, h);

                        // Try to draw the image
                        var item = _drawings[slotIndex];
                        try
                        {
                            if (File.Exists(item.FilePath))
                            {
                                using (var img = Image.FromFile(item.FilePath))
                                {
                                    var rect = PrintLayoutEngine.GetDrawingRect(_currentLayout, col, row,
                                        item.WidthMM, item.HeightMM, true);

                                    float drawX = rect.X * printScale;
                                    float drawY = rect.Y * printScale;
                                    float drawW = rect.Width * printScale;
                                    float drawH = rect.Height * printScale;

                                    g.DrawImage(img, drawX, drawY, drawW, drawH);
                                }
                            }
                        }
                        catch
                        {
                            // Draw placeholder text
                            g.DrawString(item.FileName, textFont, textBrush, x + 5, y + 5);
                            g.DrawLine(gridPen, x, y, x + w, y + h);
                        }
                    }
                }
            }

            _printPageIndex++;
            e.HasMorePages = _printPageIndex < _currentLayout.TotalPages;

            if (!e.HasMorePages)
            {
                _printPageIndex = 0;
            }
        }
    }
}
