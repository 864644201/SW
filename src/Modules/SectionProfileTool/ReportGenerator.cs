using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace SectionProfileTool
{
    internal static class ReportGenerator
    {
        private static readonly Color ColorSafe = Color.FromArgb(0, 180, 80);
        private static readonly Color ColorWarning = Color.FromArgb(255, 165, 0);
        private static readonly Color ColorDanger = Color.FromArgb(220, 40, 40);
        private static readonly Color ColorBlue = Color.FromArgb(0, 113, 227);
        private static readonly Color ColorGrid = Color.FromArgb(230, 235, 240);

        public static void GeneratePdf(string filePath, CalculationResult result, CalculationInput input,
            string profileDesc, string orientationText, string supportText, string materialText)
        {
            double defRatio = result.AllowableDeflectionMm > 0 ? result.ActualDeflectionMm / result.AllowableDeflectionMm : 0;
            string defLimit = input.SupportType == "cantilever" ? "L/150" : "L/250";

            // 生成图片
            string stressImg = BitmapToBase64(DrawStressCloudBitmap(900, 360, result, input));
            string dispImg = BitmapToBase64(DrawDisplacementBitmap(900, 360, result, input));
            string strainImg = BitmapToBase64(DrawStrainBitmap(900, 360, result, input));
            string sectionImg = BitmapToBase64(DrawSectionStressBitmap(900, 400, result, input));

            // 可靠性评估
            string grade, gradeColor, conclusion;
            EvaluateReliability(result, defRatio, out grade, out gradeColor, out conclusion);

            var html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html><html><head><meta charset='utf-8'>");
            html.AppendLine("<title>型材工具工程结论报告</title>");
            html.AppendLine("<style>");
            html.AppendLine("body{font-family:'Microsoft YaHei',SimHei,Arial,sans-serif;margin:0;padding:20px;color:#333;background:#fff;font-size:14px;line-height:1.6}");
            html.AppendLine(".page{max-width:1100px;margin:0 auto 30px;page-break-after:always}");
            html.AppendLine("h1{text-align:center;color:#1a3a5c;font-size:22px;margin:20px 0 5px}");
            html.AppendLine("h1.en{text-align:center;color:#888;font-size:13px;font-weight:normal;margin:0 0 15px}");
            html.AppendLine("h2{color:#1a3a5c;font-size:16px;border-bottom:2px solid #d0d8e0;padding-bottom:5px;margin:20px 0 10px}");
            html.AppendLine("table{width:100%;border-collapse:collapse;margin:8px 0}");
            html.AppendLine("td{padding:4px 8px;border-bottom:1px solid #eee;vertical-align:top}");
            html.AppendLine("td.label{width:140px;color:#666;font-weight:bold;white-space:nowrap}");
            html.AppendLine(".verdict{font-weight:bold;padding:2px 8px;border-radius:4px;display:inline-block}");
            html.AppendLine(".ok{background:#e6f9e6;color:#006600}.warn{background:#fff3e0;color:#cc6600}.bad{background:#ffe0e0;color:#cc0000}");
            html.AppendLine(".grade{font-size:18px;font-weight:bold;padding:10px 15px;border-radius:6px;margin:10px 0}");
            html.AppendLine(".grade-box{background:#f8f9fa;border:1px solid #d0d8e0;border-radius:8px;padding:12px 16px;margin:10px 0}");
            html.AppendLine("img.chart{width:100%;max-width:1050px;border:1px solid #e0e0e0;border-radius:6px;margin:10px 0}");
            html.AppendLine(".footer{text-align:center;color:#999;font-size:11px;margin-top:30px;border-top:1px solid #eee;padding-top:10px}");
            html.AppendLine(".info-row{display:flex;gap:30px;margin:5px 0;font-size:13px;color:#555}");
            html.AppendLine("@media print{body{padding:0}.page{margin:0;page-break-after:always}}");
            html.AppendLine("</style></head><body>");

            // ===== 第一部分：报告正文 =====
            html.AppendLine("<div class='page'>");
            html.AppendLine("<h1>型 材 工 具 工 程 结 论 报 告</h1>");
            html.AppendLine("<h1 class='en'>Section Profile Engineering Report</h1>");
            html.AppendLine($"<p style='text-align:center;color:#888;font-size:12px'>报告日期: {DateTime.Now:yyyy-MM-dd HH:mm:ss} | 麦豆宝型材工具 v2.0</p>");

            // 一、工况参数
            html.AppendLine("<h2>一、工况参数 Input Parameters</h2><table>");
            html.AppendLine(Tr("型材截面", profileDesc));
            html.AppendLine(Tr("摆放姿态", orientationText));
            html.AppendLine(Tr("支撑条件", supportText));
            html.AppendLine(Tr("材料牌号", $"{materialText}（屈服强度 {input.YieldStrength:F0} MPa）"));
            html.AppendLine(Tr("受力跨度", $"L = {input.LengthMeters:F2} m"));
            html.AppendLine(Tr("载荷位置", $"a = {input.PositionMeters:F2} m（距左端 {(input.LengthMeters > 0 ? input.PositionMeters / input.LengthMeters * 100 : 50):F1}%）"));
            html.AppendLine(Tr("施加载荷", $"P = {input.LoadKg:F0} kg（{input.LoadKg * 9.8 / 1000:F2} kN）"));
            html.AppendLine("</table>");

            // 二、截面力学参数
            html.AppendLine("<h2>二、截面力学参数 Section Properties</h2><table>");
            html.AppendLine(Tr("惯性矩 Ix", $"{result.InertiaMm4 / 10000.0:F2} cm⁴（{result.InertiaMm4:F0} mm⁴）"));
            html.AppendLine(Tr("截面模量 W", $"{result.SectionModulusMm3 / 1000.0:F2} cm³（{result.SectionModulusMm3:F0} mm³）"));
            html.AppendLine("</table>");

            // 三、强度校核
            html.AppendLine("<h2>三、强度校核 Strength Check</h2><table>");
            html.AppendLine(Tr("极限破坏载荷", $"{result.BreakingLoadKg:N0} kg（{result.BreakingLoadKg * 9.8 / 1000:F2} kN）"));
            html.AppendLine(Tr("建议安全载荷", $"{result.SafeLoadKg:N0} kg（安全系数 K=1.5）"));
            html.AppendLine(Tr("最大弯曲正应力", $"σ = {result.StressMpa:F2} MPa"));
            html.AppendLine(Tr("应力饱和度", $"σ/fy = {result.StressRatio * 100:F1}%"));
            html.AppendLine(Tr("强度判定", VerdictHtml(result.StressRatio)));
            html.AppendLine("</table>");

            // 四、刚度校核
            html.AppendLine("<h2>四、刚度校核 Deflection Check</h2><table>");
            html.AppendLine(Tr("挠度控制标准", $"{defLimit} = {result.AllowableDeflectionMm:F2} mm"));
            html.AppendLine(Tr("实际计算挠度", $"δ = {result.ActualDeflectionMm:F2} mm"));
            html.AppendLine(Tr("挠度饱和度", $"δ/δlim = {defRatio * 100:F1}%"));
            html.AppendLine(Tr("刚度判定", VerdictHtml(defRatio)));
            html.AppendLine("</table>");

            // 五、工程综合判断
            html.AppendLine("<h2>五、工程综合判断</h2>");
            html.AppendLine($"<p>{result.StatusText}</p>");

            // 六、结构可靠性评估
            html.AppendLine("<h2>六、结构可靠性评估 Structural Reliability</h2>");
            html.AppendLine($"<div class='grade' style='color:{gradeColor}'>{grade}</div>");
            html.AppendLine($"<div class='grade-box'><p>{conclusion}</p>");
            html.AppendLine($"<p>强度裕量: {(1.0 - result.StressRatio) * 100:F0}% &nbsp;|&nbsp; 刚度裕量: {(1.0 - defRatio) * 100:F0}%</p></div>");

            // 七、计算依据与标准
            html.AppendLine("<h2>七、计算依据与标准 Calculation Basis & Standards</h2>");
            html.AppendLine("<table>");
            html.AppendLine("<tr><td class='label' colspan='2' style='font-size:13px;color:#1a3a5c'>引用标准</td></tr>");
            html.AppendLine(Tr("GB 50017-2017", "《钢结构设计标准》— 挠度限值、强度验算、安全系数取值依据"));
            html.AppendLine(Tr("GB 50009-2012", "《建筑结构荷载规范》— 荷载组合与分项系数"));
            html.AppendLine(Tr("GB 50018-2002", "《冷弯薄壁型钢结构技术规范》— 冷弯型材截面特性与设计规定"));
            html.AppendLine(Tr("GB/T 11263-2017", "《热轧H型钢和剖分T型钢》— H型钢截面规格与力学参数"));
            html.AppendLine(Tr("GB/T 6728-2017", "《结构用冷弯空心型钢》— 方管/矩管截面规格"));
            html.AppendLine(Tr("GB/T 700-2006", "《碳素结构钢》— Q235 等材料力学性能"));
            html.AppendLine(Tr("GB/T 1591-2018", "《低合金高强度结构钢》— Q345 等材料力学性能"));
            html.AppendLine("<tr><td class='label' colspan='2' style='font-size:13px;color:#1a3a5c;padding-top:12px'>计算公式</td></tr>");
            html.AppendLine(Tr("弯曲正应力", "σ = M / W = P·a·b / (L·W)　（简支梁集中载荷）"));
            html.AppendLine(Tr("挠度计算", $"δ = P·a²·b² / (3·E·I·L)　（简支梁集中载荷）— 许用值: {defLimit}"));
            html.AppendLine(Tr("安全系数", "K = 1.5（静载荷工况，依据 GB 50017-2017 第 3.3 节）"));
            html.AppendLine(Tr("弹性模量", "E = 206,000 MPa（钢材标准值）"));
            html.AppendLine(Tr("屈服强度", $"fy = {input.YieldStrength:F0} MPa（{materialText}）"));
            html.AppendLine("</table>");

            // 推荐截面
            if (result.Recommendations.Length > 0)
            {
                html.AppendLine("<h2>八、推荐替代截面</h2>");
                html.AppendLine("<p>以下截面满足当前工况的强度和刚度要求：</p><ol>");
                foreach (var r in result.Recommendations)
                    html.AppendLine($"<li>{r.Name}</li>");
                html.AppendLine("</ol>");
            }
            html.AppendLine("<div class='footer'>本报告由麦豆宝型材工具自动生成，仅供参考。实际工程设计应结合现场工况综合判定。</div>");
            html.AppendLine("</div>");

            // ===== 第二部分：应力云图 =====
            html.AppendLine("<div class='page'>");
            html.AppendLine("<h2>应力分布云图 Stress Distribution Cloud Map</h2>");
            html.AppendLine($"<img class='chart' src='data:image/png;base64,{stressImg}' />");
            html.AppendLine($"<p style='font-size:13px;color:#555'>最大应力: σ = {result.StressMpa:F1} MPa | 应力饱和度: {result.StressRatio * 100:F1}% | 材料屈服: {input.YieldStrength:F0} MPa</p>");
            html.AppendLine("</div>");

            // ===== 第三部分：位移变形图 =====
            html.AppendLine("<div class='page'>");
            html.AppendLine("<h2>位移变形图 Displacement Deformation Map</h2>");
            html.AppendLine($"<img class='chart' src='data:image/png;base64,{dispImg}' />");
            html.AppendLine($"<p style='font-size:13px;color:#555'>最大挠度: δ = {result.ActualDeflectionMm:F2} mm | 许用挠度: {result.AllowableDeflectionMm:F2} mm | 挠度比: {defRatio * 100:F1}%</p>");
            html.AppendLine("</div>");

            // ===== 第四部分：应变分布图 =====
            html.AppendLine("<div class='page'>");
            html.AppendLine("<h2>应变分布图 Strain Distribution Map</h2>");
            html.AppendLine($"<img class='chart' src='data:image/png;base64,{strainImg}' />");
            html.AppendLine($"<p style='font-size:13px;color:#555'>最大应变: ε = {result.StressMpa / 206000.0 * 1e6:F1} µε | 弹性模量: E = 206,000 MPa | ε = σ/E</p>");
            html.AppendLine("</div>");

            // ===== 第五部分：截面应力分布 =====
            html.AppendLine("<div class='page'>");
            html.AppendLine("<h2>截面应力分布图 Cross-Section Stress Distribution</h2>");
            html.AppendLine($"<img class='chart' src='data:image/png;base64,{sectionImg}' />");
            html.AppendLine($"<p style='font-size:13px;color:#555'>最大正应力: σ = {result.StressMpa:F2} MPa | σ = M/W | 中性轴处应力为零</p>");
            html.AppendLine("</div>");

            html.AppendLine("</body></html>");

            File.WriteAllText(filePath, html.ToString(), Encoding.UTF8);
        }

        private static void EvaluateReliability(CalculationResult result, double defRatio,
            out string grade, out string color, out string conclusion)
        {
            bool stressOk = result.StressRatio < 1.0;
            bool deflectionOk = defRatio < 1.0;
            bool stressGood = result.StressRatio < 0.6;
            bool deflectionGood = defRatio < 0.6;

            if (!stressOk || !deflectionOk)
            {
                grade = "★ 不可靠 NOT RELIABLE";
                color = "#cc0000";
                conclusion = "该结构在当前工况下不满足强度或刚度要求，存在失效风险，不可投入使用。建议更换更大截面、缩短跨度或降低载荷。";
            }
            else if (result.StressRatio >= 0.8 || defRatio >= 0.8)
            {
                grade = "★ 可靠性不足 MARGINAL";
                color = "#cc6600";
                conclusion = "虽未失效，但安全裕量偏小，在动载荷或疲劳工况下存在隐患。建议适当加大截面规格。";
            }
            else if (stressGood && deflectionGood)
            {
                grade = "★ 可靠 RELIABLE";
                color = "#006600";
                conclusion = "强度与刚度均满足要求，安全裕量充足，可放心使用。各项指标均处于安全区间。";
            }
            else
            {
                grade = "★ 基本可靠 ACCEPTABLE";
                color = "#0066cc";
                conclusion = "满足要求，但裕量一般。若存在动载荷或疲劳工况，建议进一步校核。";
            }
        }

        private static string Tr(string label, string value)
            => $"<tr><td class='label'>{label}</td><td>{value}</td></tr>";

        private static string VerdictHtml(double ratio)
        {
            if (ratio >= 1.0) return "<span class='verdict bad'>❌ 不合格</span>";
            if (ratio >= 0.8) return "<span class='verdict warn'>⚠ 勉强</span>";
            if (ratio >= 0.6) return "<span class='verdict warn'>△ 一般</span>";
            return "<span class='verdict ok'>✔ 合格</span>";
        }

        private static string BitmapToBase64(Bitmap bmp)
        {
            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Png);
                bmp.Dispose();
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        // ========================================================
        //  应力云图
        // ========================================================
        private static Bitmap DrawStressCloudBitmap(int width, int height, CalculationResult result, CalculationInput input)
        {
            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);
                int margin = 60, beamTop = 80, beamH = 70, spanW = width - 2 * margin;
                int trimX = input.SupportType == "ff" ? 22 : 0;
                int beamLeft = margin + trimX, beamRight = margin + spanW - trimX, beamW = beamRight - beamLeft;

                // 网格
                using (var gridPen = new Pen(ColorGrid, 0.5f))
                    for (int x = margin; x < width - margin; x += 30)
                        g.DrawLine(gridPen, x, beamTop - 40, x, beamTop + beamH + 80);

                using (var titleFont = new Font("黑体", 13, FontStyle.Bold))
                    g.DrawString("应力分布云图 Stress Distribution", titleFont, Brushes.DarkBlue, margin, 20);

                // 支撑（先画，再画梁覆盖端面）
                DrawSupport(g, margin, beamTop + beamH, input.SupportType, true);
                DrawSupport(g, margin + spanW, beamTop + beamH, input.SupportType, false);

                // 应力渐变梁
                for (int i = 0; i < beamW; i++)
                {
                    double pos = trimX > 0 ? (double)(i + trimX) / spanW : (double)i / spanW;
                    double localStress = CalcLocalStress(pos, input, result);
                    double ratio = Math.Min(localStress / Math.Max(input.YieldStrength, 1), 1.0);
                    using (var pen = new Pen(StressColor(ratio), 2f))
                        g.DrawLine(pen, beamLeft + i, beamTop, beamLeft + i, beamTop + beamH);
                }
                using (var outlinePen = new Pen(Color.FromArgb(60, 60, 60), 2f))
                    g.DrawRectangle(outlinePen, beamLeft, beamTop, beamW, beamH);

                // 载荷
                int loadX = margin + (int)((input.LengthMeters > 0 ? input.PositionMeters / input.LengthMeters : 0.5) * spanW);
                using (var arrowPen = new Pen(ColorDanger, 3f) { EndCap = LineCap.ArrowAnchor })
                    g.DrawLine(arrowPen, loadX, beamTop - 45, loadX, beamTop - 5);
                using (var loadFont = new Font("黑体", 10, FontStyle.Bold))
                    g.DrawString($"{input.LoadKg:F0} kg", loadFont, Brushes.Red, loadX - 30, beamTop - 65);

                // 应力刻度
                using (var labelFont = new Font("宋体", 9))
                    for (int i = 0; i <= 4; i++)
                    {
                        int x = margin + i * spanW / 4;
                        g.DrawString($"{i * 25}%", labelFont, Brushes.DimGray, x - 10, beamTop + beamH + 8);
                    }

                // 最大应力
                using (var maxFont = new Font("黑体", 11, FontStyle.Bold))
                    g.DrawString($"max σ = {result.StressMpa:F1} MPa ({result.StressRatio * 100:F1}%)",
                        maxFont, new SolidBrush(stressOkColor(result.StressRatio)), margin + spanW / 2 - 80, beamTop + beamH + 30);

                // 颜色条
                DrawColorBar(g, margin, beamTop + beamH + 55, spanW, 14);
            }
            return bmp;
        }

        // ========================================================
        //  位移变形图
        // ========================================================
        private static Bitmap DrawDisplacementBitmap(int width, int height, CalculationResult result, CalculationInput input)
        {
            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);
                int margin = 60, baseY = 120, spanW = width - 2 * margin;
                int trimX = input.SupportType == "ff" ? 22 : 0;
                int beamL = margin + trimX, beamR = margin + spanW - trimX;

                using (var titleFont = new Font("黑体", 13, FontStyle.Bold))
                    g.DrawString("位移变形图 Displacement Deformation", titleFont, Brushes.DarkBlue, margin, 20);

                // 支撑（先画）
                DrawSupport(g, margin, baseY, input.SupportType, true);
                DrawSupport(g, margin + spanW, baseY, input.SupportType, false);

                // 原始梁（虚线）
                using (var dashPen = new Pen(Color.FromArgb(180, 180, 180), 2f) { DashStyle = DashStyle.Dash })
                    g.DrawLine(dashPen, beamL, baseY, beamR, baseY);

                // 变形梁
                float maxSag = (float)Math.Min(result.ActualDeflectionMm * 8, 120);
                Color beamColor = result.IsBroken ? ColorDanger : result.StressRatio > 0.8 ? ColorWarning : ColorBlue;
                using (var beamPen = new Pen(beamColor, 6f) { StartCap = LineCap.Round, EndCap = LineCap.Round })
                {
                    float loadX = margin + (float)((input.LengthMeters > 0 ? input.PositionMeters / input.LengthMeters : 0.5) * spanW);
                    var path = new GraphicsPath();
                    if (input.SupportType == "ss")
                        path.AddBezier(beamL, baseY, beamL + spanW * 0.28f, baseY, loadX, baseY + maxSag * 1.8f, beamR, baseY);
                    else if (input.SupportType == "ff")
                    {
                        path.AddBezier(beamL, baseY, beamL + spanW * 0.20f, baseY, loadX - spanW * 0.12f, baseY + maxSag, loadX, baseY + maxSag);
                        path.AddBezier(loadX, baseY + maxSag, loadX + spanW * 0.12f, baseY + maxSag, beamR - spanW * 0.20f, baseY, beamR, baseY);
                    }
                    else
                        path.AddBezier(beamL, baseY, beamL + spanW * 0.25f, baseY, loadX, baseY + maxSag, beamR, baseY + maxSag * 1.35f);
                    g.DrawPath(beamPen, path);
                }

                // 挠度标注
                int loadXi = margin + (int)((input.LengthMeters > 0 ? input.PositionMeters / input.LengthMeters : 0.5) * spanW);
                using (var dimPen = new Pen(ColorDanger, 1.5f) { DashStyle = DashStyle.Dot })
                    g.DrawLine(dimPen, loadXi, baseY, loadXi, baseY + maxSag);
                using (var dimFont = new Font("黑体", 10, FontStyle.Bold))
                    g.DrawString($"δ = {result.ActualDeflectionMm:F2} mm", dimFont, Brushes.Red, loadXi + 8, baseY + maxSag / 2 - 10);

                // 载荷
                using (var arrowPen = new Pen(ColorDanger, 3f) { EndCap = LineCap.ArrowAnchor })
                    g.DrawLine(arrowPen, loadXi, baseY - 50, loadXi, baseY - 5);
                using (var loadFont = new Font("黑体", 10, FontStyle.Bold))
                    g.DrawString($"{input.LoadKg:F0} kg", loadFont, Brushes.Red, loadXi - 30, baseY - 70);

                using (var noteFont = new Font("宋体", 9))
                    g.DrawString("注: 变形已放大显示，实际变形量请参考标注值", noteFont, Brushes.Gray, margin, baseY + maxSag + 30);
            }
            return bmp;
        }

        // ========================================================
        //  应变分布图
        // ========================================================
        private static Bitmap DrawStrainBitmap(int width, int height, CalculationResult result, CalculationInput input)
        {
            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);
                int margin = 60, beamTop = 70, beamH = 50, spanW = width - 2 * margin;
                int trimX = input.SupportType == "ff" ? 22 : 0;
                int beamLeft = margin + trimX, beamRight = margin + spanW - trimX, beamW = beamRight - beamLeft;

                using (var titleFont = new Font("黑体", 13, FontStyle.Bold))
                    g.DrawString("应变分布图 Strain Distribution", titleFont, Brushes.DarkBlue, margin, 20);

                // 支撑（先画）
                DrawSupport(g, margin, beamTop + beamH, input.SupportType, true);
                DrawSupport(g, margin + spanW, beamTop + beamH, input.SupportType, false);

                // 应变云图
                double maxStrain = result.StressMpa / 206000.0;
                for (int i = 0; i < beamW; i++)
                {
                    double pos = trimX > 0 ? (double)(i + trimX) / spanW : (double)i / spanW;
                    double localStress = CalcLocalStress(pos, input, result);
                    double ratio = Math.Min(localStress / Math.Max(input.YieldStrength, 1), 1.0);
                    using (var pen = new Pen(StrainColor(ratio), 2f))
                        g.DrawLine(pen, beamLeft + i, beamTop, beamLeft + i, beamTop + beamH);
                }
                using (var outlinePen = new Pen(Color.FromArgb(60, 60, 60), 2f))
                    g.DrawRectangle(outlinePen, beamLeft, beamTop, beamW, beamH);

                // 应变曲线
                int graphTop = beamTop + beamH + 50, graphH = 150;
                using (var axisPen = new Pen(Color.FromArgb(80, 80, 80), 1.5f))
                {
                    g.DrawLine(axisPen, margin, graphTop, margin, graphTop + graphH);
                    g.DrawLine(axisPen, margin, graphTop + graphH, margin + spanW, graphTop + graphH);
                }

                var points = new PointF[spanW];
                for (int i = 0; i < spanW; i++)
                {
                    double pos = (double)i / spanW;
                    double localStress = CalcLocalStress(pos, input, result);
                    float y = graphTop + graphH - (float)(localStress / Math.Max(input.YieldStrength, 1)) * graphH;
                    points[i] = new PointF(margin + i, y);
                }
                if (points.Length > 1)
                {
                    using (var curvePen = new Pen(ColorBlue, 2.5f))
                        g.DrawCurve(curvePen, points);
                    var fillPts = new PointF[points.Length + 2];
                    Array.Copy(points, fillPts, points.Length);
                    fillPts[points.Length] = new PointF(margin + spanW, graphTop + graphH);
                    fillPts[points.Length + 1] = new PointF(margin, graphTop + graphH);
                    using (var fillBrush = new LinearGradientBrush(new Point(0, graphTop), new Point(0, graphTop + graphH),
                        Color.FromArgb(80, 0, 113, 227), Color.FromArgb(10, 0, 113, 227)))
                        g.FillPolygon(fillBrush, fillPts);
                }

                using (var labelFont = new Font("宋体", 9))
                {
                    g.DrawString("σ/fy", labelFont, Brushes.DimGray, margin - 10, graphTop - 15);
                    g.DrawString("跨度方向", labelFont, Brushes.DimGray, margin + spanW / 2 - 20, graphTop + graphH + 10);
                    double strainMicro = maxStrain * 1e6;
                    g.DrawString($"ε = {strainMicro:F1} µε", new Font("黑体", 10, FontStyle.Bold), Brushes.Blue, margin + spanW / 2 - 40, graphTop - 15);
                }
            }
            return bmp;
        }

        // ========================================================
        //  截面应力分布
        // ========================================================
        private static Bitmap DrawSectionStressBitmap(int width, int height, CalculationResult result, CalculationInput input)
        {
            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.White);

                using (var titleFont = new Font("黑体", 13, FontStyle.Bold))
                    g.DrawString("截面应力分布 Cross-Section Stress", titleFont, Brushes.DarkBlue, 40, 20);

                int cx = 200, cy = 220, size = 140;
                DrawSectionOutline(g, cx, cy, size, input.CategoryKey, input.Orientation);

                // 中性轴
                using (var neutralPen = new Pen(Color.FromArgb(100, 100, 100), 1.5f) { DashStyle = DashStyle.Dash })
                    g.DrawLine(neutralPen, cx - size, cy, cx + size, cy);

                using (var f = new Font("黑体", 10))
                {
                    g.DrawString("压应力区", f, Brushes.Blue, cx - size - 30, cy - size / 2 - 20);
                    g.DrawString("拉应力区", f, Brushes.Red, cx - size - 30, cy + size / 2 + 5);
                    g.DrawString("中性轴", f, Brushes.DimGray, cx - size - 30, cy - 8);
                }

                // 应力梯度色条
                for (int i = -size / 2; i <= size / 2; i += 4)
                {
                    double sr = Math.Abs(2.0 * i / size) * result.StressRatio;
                    Color c = StressColor(Math.Min(sr, 1.0));
                    int barLen = (int)(Math.Abs((double)i / (size / 2)) * 100);
                    using (var pen = new Pen(c, 3f))
                        g.DrawLine(pen, cx + size / 2 + 20, cy + i, cx + size / 2 + 20 + barLen, cy + i);
                }

                // 右侧应力曲线
                int gx = 500, gw = 350, gh = 280, gy = 80;
                using (var axisPen = new Pen(Color.FromArgb(80, 80, 80), 1.5f))
                {
                    g.DrawLine(axisPen, gx, gy, gx, gy + gh);
                    g.DrawLine(axisPen, gx, gy + gh / 2, gx + gw, gy + gh / 2);
                }

                var pts = new PointF[size + 1];
                for (int i = 0; i <= size; i++)
                {
                    double yr = (double)i / size;
                    double stress = yr * result.StressRatio * input.YieldStrength;
                    pts[i] = new PointF(gx + (float)(stress / Math.Max(input.YieldStrength, 1) * gw), gy + i);
                }
                using (var curvePen = new Pen(ColorDanger, 2.5f))
                    g.DrawCurve(curvePen, pts);

                using (var labelFont = new Font("宋体", 9))
                {
                    g.DrawString("σ (MPa)", labelFont, Brushes.DimGray, gx + gw / 2 - 15, gy - 15);
                    g.DrawString("截面高度", labelFont, Brushes.DimGray, gx - 40, gy + gh / 2 - 10);
                    g.DrawString($"{input.YieldStrength:F0}", labelFont, Brushes.DimGray, gx + gw - 25, gy + gh + 5);
                    g.DrawString($"σ = {result.StressMpa:F1} MPa", new Font("黑体", 10, FontStyle.Bold), Brushes.Red, gx + gw / 2 - 40, gy + gh + 20);
                }

                // 图例
                int lx = 480, ly = gy + gh + 45;
                using (var lf = new Font("宋体", 9))
                {
                    g.FillRectangle(Brushes.Blue, lx, ly, 12, 12);
                    g.DrawString($"压应力 σ = {result.StressMpa:F1} MPa", lf, Brushes.Black, lx + 18, ly);
                    g.FillRectangle(Brushes.Red, lx, ly + 18, 12, 12);
                    g.DrawString($"拉应力 σ = {result.StressMpa:F1} MPa", lf, Brushes.Black, lx + 18, ly + 18);
                }
            }
            return bmp;
        }

        // ========================================================
        //  辅助方法
        // ========================================================
        private static double CalcLocalStress(double pos, CalculationInput input, CalculationResult result)
        {
            double a = input.LengthMeters > 0 ? input.PositionMeters / input.LengthMeters : 0.5;
            double b = 1.0 - a;
            double mf;
            if (input.SupportType == "ss")
                mf = pos <= a ? pos * b : a * (1.0 - pos);
            else if (input.SupportType == "ff")
            {
                double m1 = a * b * b, m2 = a * a * b;
                mf = Math.Abs(pos <= a ? -m1 + pos * (m1 + m2) / a : -m2 + (1.0 - pos) * (m1 + m2) / b);
            }
            else
                mf = pos;
            double maxMf = input.SupportType == "cantilever" ? a
                : input.SupportType == "ff" ? Math.Max(a * b * b, a * a * b) : a * b;
            return maxMf > 0 ? result.StressMpa * (mf / maxMf) : 0;
        }

        private static Color StressColor(double ratio)
        {
            ratio = Math.Max(0, Math.Min(1, ratio));
            return ratio < 0.5
                ? Color.FromArgb((int)(ratio * 2 * 255), 200, 0)
                : Color.FromArgb(255, (int)((1.0 - (ratio - 0.5) * 2) * 200), 0);
        }

        private static Color StrainColor(double ratio)
        {
            ratio = Math.Max(0, Math.Min(1, ratio));
            return ratio < 0.33 ? Color.FromArgb((int)(ratio * 3 * 100), 180, 255)
                : ratio < 0.66 ? Color.FromArgb((int)((ratio - 0.33) * 3 * 255), 180, (int)((0.66 - ratio) * 3 * 255))
                : Color.FromArgb(255, (int)((1.0 - ratio) * 3 * 180), 0);
        }

        private static Color stressOkColor(double ratio)
            => ratio >= 1.0 ? ColorDanger : ratio >= 0.8 ? ColorWarning : ColorSafe;

        private static void DrawSupport(Graphics g, int x, int y, string type, bool isLeft)
        {
            using (var brush = new SolidBrush(Color.FromArgb(96, 96, 96)))
            using (var lineBrush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.FromArgb(120, 120, 120), Color.FromArgb(200, 200, 200)))
            {
                if (type == "ss")
                {
                    var tri = isLeft
                        ? new Point[] { new Point(x - 12, y + 2), new Point(x - 24, y + 25), new Point(x + 2, y + 25) }
                        : new Point[] { new Point(x - 2, y + 2), new Point(x - 14, y + 25), new Point(x + 12, y + 25) };
                    g.FillPolygon(brush, tri);
                    g.DrawPolygon(Pens.DimGray, tri);
                }
                else
                {
                    // 固定端：画在梁端面位置，不侵入梁体
                    int w = 22, h = 80;
                    int rx = isLeft ? x - w : x;
                    g.FillRectangle(lineBrush, rx, y - h / 2, w, h);
                    using (var pen = new Pen(Color.FromArgb(60, 60, 60), 2f))
                        g.DrawRectangle(pen, rx, y - h / 2, w, h);
                }
            }
        }

        private static void DrawSectionOutline(Graphics g, int cx, int cy, int size, string cat, string ori)
        {
            using (var pen = new Pen(ColorBlue, 3f))
            using (var brush = new SolidBrush(Color.FromArgb(30, 0, 113, 227)))
            {
                int h = size / 2;
                if (cat == "tube") { g.FillRectangle(brush, cx - h, cy - h, size, size); g.DrawRectangle(pen, cx - h, cy - h, size, size); }
                else if (cat == "round_tube") { g.FillEllipse(brush, cx - h, cy - h, size, size); g.DrawEllipse(pen, cx - h, cy - h, size, size); }
                else if (cat == "solid_round") { g.FillEllipse(brush, cx - h, cy - h, size, size); g.DrawEllipse(pen, cx - h, cy - h, size, size); }
                else if (cat == "hbeam" || cat == "ibeam")
                {
                    g.FillRectangle(brush, cx - h, cy - h, size, size / 5);
                    g.FillRectangle(brush, cx - size / 10, cy - h, size / 5, size);
                    g.FillRectangle(brush, cx - h, cy + h - size / 5, size, size / 5);
                    g.DrawRectangle(pen, cx - h, cy - h, size, size / 5);
                    g.DrawRectangle(pen, cx - size / 10, cy - h, size / 5, size);
                    g.DrawRectangle(pen, cx - h, cy + h - size / 5, size, size / 5);
                }
                else if (cat == "channel")
                {
                    g.FillRectangle(brush, cx - h, cy - h, size / 5, size);
                    g.FillRectangle(brush, cx - h, cy - h, size * 2 / 3, size / 5);
                    g.FillRectangle(brush, cx - h, cy + h - size / 5, size * 2 / 3, size / 5);
                }
                else { g.FillRectangle(brush, cx - h, cy - h, size, size); g.DrawRectangle(pen, cx - h, cy - h, size, size); }
            }
        }

        private static void DrawColorBar(Graphics g, int x, int y, int w, int h)
        {
            for (int i = 0; i < w; i++)
            {
                double ratio = (double)i / w;
                using (var pen = new Pen(StressColor(ratio), 1.2f))
                    g.DrawLine(pen, x + i, y, x + i, y + h);
            }
            using (var f = new Font("宋体", 8))
            {
                g.DrawString("0%", f, Brushes.DimGray, x, y + h + 2);
                g.DrawString("100%", f, Brushes.DimGray, x + w - 25, y + h + 2);
                g.DrawString("σ/fy", f, Brushes.DimGray, x + w / 2 - 12, y + h + 2);
            }
        }
    }
}
