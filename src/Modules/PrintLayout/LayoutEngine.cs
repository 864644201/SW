using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace PrintLayout
{
    /// <summary>
    /// 纸张类型
    /// </summary>
    public enum PaperType
    {
        A3,
        A4
    }

    /// <summary>
    /// 排列方式
    /// </summary>
    public enum ArrangeMode
    {
        /// <summary>自动适应 - 根据图纸尺寸自动排列</summary>
        AutoFit,
        /// <summary>2x1 横排</summary>
        TwoByOne,
        /// <summary>2x2 四宫格</summary>
        TwoByTwo,
        /// <summary>手动指定行列数</summary>
        Manual
    }

    /// <summary>
    /// 图纸项
    /// </summary>
    public class DrawingItem
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public Image Thumbnail { get; set; }
        /// <summary>图纸原始宽度 (mm)</summary>
        public double WidthMM { get; set; }
        /// <summary>图纸原始高度 (mm)</summary>
        public double HeightMM { get; set; }
        /// <summary>旋转90度</summary>
        public bool Rotated { get; set; }
    }

    /// <summary>
    /// 拼图布局结果
    /// </summary>
    public class LayoutResult
    {
        /// <summary>纸张尺寸 (mm)</summary>
        public SizeF PaperSizeMM { get; set; }
        /// <summary>可打印区域 (mm)</summary>
        public SizeF PrintableAreaMM { get; set; }
        /// <summary>列数</summary>
        public int Cols { get; set; }
        /// <summary>行数</summary>
        public int Rows { get; set; }
        /// <summary>每页图纸数量</summary>
        public int ItemsPerPage { get; set; }
        /// <summary>总页数</summary>
        public int TotalPages { get; set; }
        /// <summary>单个图纸槽位宽度 (mm)</summary>
        public double SlotWidthMM { get; set; }
        /// <summary>单个图纸槽位高度 (mm)</summary>
        public double SlotHeightMM { get; set; }
        /// <summary>图纸间距 (mm)</summary>
        public double SpacingMM { get; set; }
        /// <summary>页边距 (mm)</summary>
        public double MarginMM { get; set; }
    }

    /// <summary>
    /// 拼图排版引擎
    /// </summary>
    public static class PrintLayoutEngine
    {
        // 标准纸张尺寸 (mm)
        private static readonly SizeF A3Size = new SizeF(420f, 297f);
        private static readonly SizeF A4Size = new SizeF(297f, 210f);

        // 标准页边距 (mm)
        private const double DefaultMargin = 10.0;
        private const double DefaultSpacing = 5.0;

        /// <summary>
        /// 获取纸张尺寸 (mm)
        /// </summary>
        public static SizeF GetPaperSizeMM(PaperType paper)
        {
            return paper == PaperType.A3 ? A3Size : A4Size;
        }

        /// <summary>
        /// 计算拼图布局
        /// </summary>
        public static LayoutResult Calculate(
            PaperType paper,
            ArrangeMode mode,
            int drawingCount,
            double drawingWidthMM,
            double drawingHeightMM,
            double marginMM,
            double spacingMM,
            int manualRows,
            int manualCols)
        {
            var result = new LayoutResult
            {
                PaperSizeMM = GetPaperSizeMM(paper),
                MarginMM = marginMM,
                SpacingMM = spacingMM
            };

            double pw = result.PaperSizeMM.Width;
            double ph = result.PaperSizeMM.Height;
            double aw = pw - 2 * marginMM;
            double ah = ph - 2 * marginMM;
            result.PrintableAreaMM = new SizeF((float)aw, (float)ah);

            switch (mode)
            {
                case ArrangeMode.TwoByOne:
                    result.Cols = 2;
                    result.Rows = 1;
                    break;
                case ArrangeMode.TwoByTwo:
                    result.Cols = 2;
                    result.Rows = 2;
                    break;
                case ArrangeMode.Manual:
                    result.Cols = Math.Max(1, manualCols);
                    result.Rows = Math.Max(1, manualRows);
                    break;
                case ArrangeMode.AutoFit:
                default:
                    AutoFit(aw, ah, drawingWidthMM, drawingHeightMM, spacingMM,
                            out int cols, out int rows);
                    result.Cols = cols;
                    result.Rows = rows;
                    break;
            }

            result.ItemsPerPage = result.Cols * result.Rows;
            result.TotalPages = result.ItemsPerPage > 0
                ? (int)Math.Ceiling((double)drawingCount / result.ItemsPerPage)
                : 0;

            double totalSpacingW = (result.Cols - 1) * spacingMM;
            double totalSpacingH = (result.Rows - 1) * spacingMM;
            result.SlotWidthMM = (aw - totalSpacingW) / result.Cols;
            result.SlotHeightMM = (ah - totalSpacingH) / result.Rows;

            return result;
        }

        private static void AutoFit(double areaWidth, double areaHeight,
            double dw, double dh, double spacing, out int cols, out int rows)
        {
            cols = 1;
            rows = 1;
            double bestScale = 0;

            for (int c = 1; c <= 10; c++)
            {
                for (int r = 1; r <= 10; r++)
                {
                    double totalSpacingW = (c - 1) * spacing;
                    double totalSpacingH = (r - 1) * spacing;
                    double slotW = (areaWidth - totalSpacingW) / c;
                    double slotH = (areaHeight - totalSpacingH) / r;

                    if (slotW <= 0 || slotH <= 0) continue;

                    double scaleW = slotW / dw;
                    double scaleH = slotH / dh;
                    double scale = Math.Min(scaleW, scaleH);

                    if (scale >= 0.01)
                    {
                        double score = c * r * 1000 + scale * 100;
                        double bestScore = cols * rows * 1000 + bestScale * 100;
                        if (score > bestScore)
                        {
                            cols = c;
                            rows = r;
                            bestScale = scale;
                        }
                    }
                }
            }

            if (bestScale < 0.01)
            {
                cols = 1;
                rows = 1;
            }
        }

        /// <summary>
        /// 计算单个图纸在槽位中的绘制矩形 (mm坐标)
        /// </summary>
        public static RectangleF GetDrawingRect(
            LayoutResult layout,
            int col, int row,
            double drawingWidthMM, double drawingHeightMM,
            bool rotateToFit)
        {
            double slotX = layout.MarginMM + col * (layout.SlotWidthMM + layout.SpacingMM);
            double slotY = layout.MarginMM + row * (layout.SlotHeightMM + layout.SpacingMM);

            double dw = drawingWidthMM;
            double dh = drawingHeightMM;

            double scaleX = layout.SlotWidthMM / dw;
            double scaleY = layout.SlotHeightMM / dh;
            double scale = Math.Min(scaleX, scaleY);
            bool rotated = false;

            if (rotateToFit)
            {
                double scaleXr = layout.SlotWidthMM / dh;
                double scaleYr = layout.SlotHeightMM / dw;
                double scaleR = Math.Min(scaleXr, scaleYr);

                if (scaleR > scale)
                {
                    scale = scaleR;
                    rotated = true;
                }
            }

            double drawW, drawH;
            if (rotated)
            {
                drawW = dh * scale;
                drawH = dw * scale;
            }
            else
            {
                drawW = dw * scale;
                drawH = dh * scale;
            }

            double x = slotX + (layout.SlotWidthMM - drawW) / 2.0;
            double y = slotY + (layout.SlotHeightMM - drawH) / 2.0;

            return new RectangleF((float)x, (float)y, (float)drawW, (float)drawH);
        }

        /// <summary>
        /// 设置 PrintDocument 页面设置
        /// </summary>
        public static void ConfigurePageSettings(PrintDocument doc, PaperType paper)
        {
            var ps = doc.DefaultPageSettings;
            ps.Margins = new Margins(0, 0, 0, 0);
            ps.Landscape = (paper == PaperType.A3);

            if (paper == PaperType.A3)
            {
                ps.PaperSize = FindPaperSize(doc, "A3", 420, 297);
            }
            else
            {
                ps.PaperSize = FindPaperSize(doc, "A4", 297, 210);
            }
        }

        private static PaperSize FindPaperSize(PrintDocument doc, string name, int widthMM, int heightMM)
        {
            int w = (int)(widthMM / 25.4 * 100);
            int h = (int)(heightMM / 25.4 * 100);

            foreach (PaperSize ps in doc.PrinterSettings.PaperSizes)
            {
                if (ps.PaperName.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                    return ps;
            }

            return new PaperSize("Custom", w, h);
        }

        /// <summary>
        /// 毫米转为百分之一英寸 (用于 .NET PrintDocument)
        /// </summary>
        public static float MmToHundredthsOfInch(float mm)
        {
            return mm / 25.4f * 100f;
        }
    }
}
