using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SectionProfileTool
{
    internal sealed class BeamDiagramPanel : Panel
    {
        public CalculationResult Result { get; set; }
        public double AnimatedSag { get; set; }
        public string SupportType { get; set; } = "ff";
        public double LoadKg { get; set; }

        public BeamDiagramPanel()
        {
            DoubleBuffered = true;
            BackColor = Color.FromArgb(238, 242, 245);
            MinimumSize = new Size(520, 280);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var gridPen = new Pen(Color.FromArgb(226, 231, 236)))
            {
                for (int x = 0; x < Width; x += 24)
                {
                    g.DrawLine(gridPen, x, 0, x, Height);
                }

                for (int y = 0; y < Height; y += 24)
                {
                    g.DrawLine(gridPen, 0, y, Width, y);
                }
            }

            int startX = 70;
            int endX = Width - 70;
            int span = Math.Max(100, endX - startX);
            float baseY = 120f;
            float sag = (float)Math.Min(AnimatedSag, 95.0);
            float loadX = startX + (float)((Result?.PositionRatio ?? 0.5) * span);

            DrawSupports(g, startX, endX, baseY);

            Color beamColor = Color.FromArgb(0, 113, 227);
            if (Result != null && Result.StressRatio > 1.0)
            {
                beamColor = Color.FromArgb(255, 59, 48);
            }
            else if (Result != null && Result.StressRatio > 0.8)
            {
                beamColor = Color.FromArgb(255, 159, 10);
            }

            using (var beamPen = new Pen(beamColor, 14f))
            {
                beamPen.StartCap = LineCap.Round;
                beamPen.EndCap = LineCap.Round;

                var path = new GraphicsPath();
                if (SupportType == "ss")
                {
                    path.AddBezier(startX, baseY, startX + span * 0.28f, baseY, loadX, baseY + sag * 1.8f, endX, baseY);
                }
                else if (SupportType == "ff")
                {
                    path.AddBezier(startX, baseY, startX + span * 0.20f, baseY, loadX - span * 0.12f, baseY + sag, loadX, baseY + sag);
                    path.AddBezier(loadX, baseY + sag, loadX + span * 0.12f, baseY + sag, endX - span * 0.20f, baseY, endX, baseY);
                }
                else
                {
                    path.AddBezier(startX, baseY, startX + span * 0.25f, baseY, loadX, baseY + sag, endX, baseY + sag * 1.35f);
                }

                g.DrawPath(beamPen, path);
            }

            DrawLoad(g, loadX, baseY + sag);
            DrawPreviewBadge(g);
        }

        private void DrawSupports(Graphics g, int startX, int endX, float baseY)
        {
            using (var supportBrush = new SolidBrush(Color.FromArgb(96, 96, 96)))
            using (var hatchBrush = new HatchBrush(HatchStyle.DiagonalBrick, Color.FromArgb(175, 175, 175), Color.FromArgb(225, 225, 225)))
            using (var borderPen = new Pen(Color.FromArgb(130, 130, 130)))
            {
                if (SupportType == "ss")
                {
                    PointF[] leftTriangle = { new PointF(startX - 18, baseY + 2), new PointF(startX - 35, baseY + 35), new PointF(startX + 5, baseY + 35) };
                    PointF[] rightTriangle = { new PointF(endX - 5, baseY + 2), new PointF(endX - 22, baseY + 35), new PointF(endX + 18, baseY + 35) };
                    g.FillPolygon(supportBrush, leftTriangle);
                    g.FillPolygon(supportBrush, rightTriangle);
                }
                else if (SupportType == "ff")
                {
                    g.FillRectangle(hatchBrush, startX - 60, baseY - 45, 50, 110);
                    g.DrawRectangle(borderPen, startX - 60, baseY - 45, 50, 110);
                    g.FillRectangle(hatchBrush, endX + 10, baseY - 45, 50, 110);
                    g.DrawRectangle(borderPen, endX + 10, baseY - 45, 50, 110);
                }
                else
                {
                    g.FillRectangle(hatchBrush, startX - 60, baseY - 45, 50, 110);
                    g.DrawRectangle(borderPen, startX - 60, baseY - 45, 50, 110);
                }
            }
        }

        private void DrawLoad(Graphics g, float x, float y)
        {
            using (var linePen = new Pen(Color.FromArgb(120, 120, 120), 2f))
            using (var loadBrush = new SolidBrush(Color.FromArgb(255, 59, 48)))
            using (var textBrush = new SolidBrush(Color.White))
            using (var font = new Font("Microsoft YaHei UI", 10f, FontStyle.Bold))
            {
                g.DrawLine(linePen, x, y - 22, x, y - 2);
                RectangleF box = new RectangleF(x - 42, y + 8, 84, 28);
                using (GraphicsPath rounded = CreateRoundedRect(box, 8f))
                {
                    g.FillPath(loadBrush, rounded);
                }

                StringFormat format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString($"{LoadKg:0} kg", font, textBrush, box, format);
            }
        }

        private void DrawPreviewBadge(Graphics g)
        {
            Rectangle badgeRect = new Rectangle(14, 14, 110, 76);
            using (var backBrush = new SolidBrush(Color.FromArgb(245, 248, 252)))
            using (var borderPen = new Pen(Color.FromArgb(205, 210, 216)))
            using (var titleBrush = new SolidBrush(Color.FromArgb(110, 110, 110)))
            using (var titleFont = new Font("Microsoft YaHei UI", 8f, FontStyle.Bold))
            {
                using (GraphicsPath rounded = CreateRoundedRect(badgeRect, 14f))
                {
                    g.FillPath(backBrush, rounded);
                    g.DrawPath(borderPen, rounded);
                }

                g.DrawString("当前截面", titleFont, titleBrush, new PointF(24, 24));
            }
        }

        private static GraphicsPath CreateRoundedRect(RectangleF rect, float radius)
        {
            float diameter = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    internal sealed class SectionPreviewPanel : Panel
    {
        public string CategoryKey { get; set; } = "tube";
        public string Orientation { get; set; } = "strong";

        public SectionPreviewPanel()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            Size = new Size(92, 92);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Color drawColor = Orientation == "strong" ? Color.FromArgb(0, 113, 227) : Color.FromArgb(255, 59, 48);
            using (var brush = new SolidBrush(drawColor))
            using (var pen = new Pen(drawColor, 6f))
            {
                if (CategoryKey == "tube")
                {
                    g.DrawRectangle(pen, 18, 18, 56, 56);
                    g.DrawRectangle(pen, 31, 31, 30, 30);
                }
                else if (CategoryKey == "round_tube")
                {
                    g.DrawEllipse(pen, 18, 18, 56, 56);
                    g.DrawEllipse(pen, 30, 30, 32, 32);
                }
                else if (CategoryKey == "solid_round")
                {
                    g.FillEllipse(brush, 18, 18, 56, 56);
                }
                else if (CategoryKey == "solid_square")
                {
                    g.FillRectangle(brush, 20, 20, 52, 52);
                }
                else if (CategoryKey == "solid_hex")
                {
                    Point[] points = { new Point(46, 14), new Point(72, 28), new Point(72, 62), new Point(46, 78), new Point(20, 62), new Point(20, 28) };
                    g.FillPolygon(brush, points);
                }
                else if (CategoryKey == "solid_flat")
                {
                    if (Orientation == "strong")
                    {
                        g.FillRectangle(brush, 34, 16, 24, 60);
                    }
                    else
                    {
                        g.FillRectangle(brush, 16, 34, 60, 24);
                    }
                }
                else if (CategoryKey == "hbeam" || CategoryKey == "ibeam")
                {
                    g.FillRectangle(brush, 18, 18, 56, 10);
                    g.FillRectangle(brush, 41, 18, 10, 56);
                    g.FillRectangle(brush, 18, 64, 56, 10);
                }
                else if (CategoryKey == "channel")
                {
                    g.FillRectangle(brush, 18, 18, 12, 56);
                    g.FillRectangle(brush, 18, 18, 44, 10);
                    g.FillRectangle(brush, 18, 64, 44, 10);
                }
                else if (CategoryKey.Contains("angle") || CategoryKey == "lsteel")
                {
                    g.FillRectangle(brush, 22, 18, 12, 56);
                    g.FillRectangle(brush, 22, 62, 46, 12);
                }
                else if (CategoryKey == "cpurlin" || CategoryKey == "zpurlin")
                {
                    Point[] points = { new Point(24, 18), new Point(68, 18), new Point(68, 30), new Point(40, 30), new Point(40, 62), new Point(68, 62), new Point(68, 74), new Point(24, 74) };
                    g.FillPolygon(brush, points);
                }
                else
                {
                    Point[] points = { new Point(24, 18), new Point(70, 18), new Point(64, 58), new Point(46, 74), new Point(28, 58) };
                    g.FillPolygon(brush, points);
                }
            }
        }
    }
}
