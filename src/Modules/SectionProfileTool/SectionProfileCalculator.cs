using System;
using System.Collections.Generic;

namespace SectionProfileTool
{
    internal sealed class ProfileCategoryDefinition
    {
        public string Key { get; set; }
        public string DisplayName { get; set; }
        public bool UsesDatabase { get; set; }
        public bool AllowOrientation { get; set; }
        public string Dimension1Label { get; set; }
        public string Dimension2Label { get; set; }
        public string Dimension3Label { get; set; }
        public bool ShowDimension2 { get; set; }
        public bool ShowDimension3 { get; set; }
    }

    internal sealed class SectionSpec
    {
        public SectionSpec(string name, double ix, double wx, double iy, double wy)
        {
            Name = name;
            Ix = ix;
            Wx = wx;
            Iy = iy;
            Wy = wy;
        }

        public string Name { get; }
        public double Ix { get; }
        public double Wx { get; }
        public double Iy { get; }
        public double Wy { get; }
        public override string ToString() => Name;
    }

    internal sealed class CommonTubeSpec
    {
        public CommonTubeSpec(double h, double b, double t)
        {
            Height = h;
            Width = b;
            Thickness = t;
        }

        public double Height { get; }
        public double Width { get; }
        public double Thickness { get; }
    }

    internal sealed class CalculationInput
    {
        public string CategoryKey { get; set; }
        public string Orientation { get; set; }
        public string SupportType { get; set; }
        public double YieldStrength { get; set; }
        public double LengthMeters { get; set; }
        public double PositionMeters { get; set; }
        public double LoadKg { get; set; }
        public double Dimension1 { get; set; }
        public double Dimension2 { get; set; }
        public double Dimension3 { get; set; }
        public SectionSpec SelectedSpec { get; set; }
    }

    internal sealed class Recommendation
    {
        public string Name { get; set; }
        public string CategoryKey { get; set; }
        public bool UsesDatabase { get; set; }
        public int SpecIndex { get; set; }
        public double Dimension1 { get; set; }
        public double Dimension2 { get; set; }
        public double Dimension3 { get; set; }
        public double RankingInertia { get; set; }

        public override string ToString() => Name;
    }

    internal sealed class CalculationResult
    {
        public double InertiaMm4 { get; set; }
        public double SectionModulusMm3 { get; set; }
        public double BreakingLoadKg { get; set; }
        public double SafeLoadKg { get; set; }
        public double ActualDeflectionMm { get; set; }
        public double AllowableDeflectionMm { get; set; }
        public double StressMpa { get; set; }
        public double StressRatio { get; set; }
        public double PositionRatio { get; set; }
        public double RelativeSag { get; set; }
        public bool IsBroken { get; set; }
        public string StatusText { get; set; }
        public Recommendation[] Recommendations { get; set; } = Array.Empty<Recommendation>();
    }

    internal static class SectionProfileCalculator
    {
        private const double ElasticModulus = 206000.0;
        private const double Gravity = 9.8;

        public static CalculationResult Calculate(CalculationInput input)
        {
            double lengthMeters = Math.Max(0.5, input.LengthMeters);
            double positionMeters = Math.Max(0.0, Math.Min(lengthMeters, input.PositionMeters));
            double loadKg = Math.Max(0.0, input.LoadKg);
            double spanOtherSideMeters = lengthMeters - positionMeters;
            double lengthMm = lengthMeters * 1000.0;
            double aMm = positionMeters * 1000.0;
            double bMm = spanOtherSideMeters * 1000.0;

            double inertiaMm4;
            double sectionModulusMm3;
            ResolveSectionProperties(input, out inertiaMm4, out sectionModulusMm3);

            double momentFactor;
            double deflectionFactor;
            double deflectionFactorForCheck;

            if (input.SupportType == "ss")
            {
                momentFactor = lengthMm <= 0 ? 0 : (aMm * bMm) / lengthMm;
                deflectionFactor = lengthMm <= 0
                    ? 0
                    : (Math.Pow(aMm, 2) * Math.Pow(bMm, 2)) / (3 * ElasticModulus * inertiaMm4 * lengthMm);
                deflectionFactorForCheck = deflectionFactor;
            }
            else if (input.SupportType == "ff")
            {
                momentFactor = lengthMm <= 0
                    ? 0
                    : Math.Max(aMm * bMm * bMm / (lengthMm * lengthMm), aMm * aMm * bMm / (lengthMm * lengthMm));
                deflectionFactor = lengthMm <= 0
                    ? 0
                    : (Math.Pow(aMm, 3) * Math.Pow(bMm, 3)) / (3 * ElasticModulus * inertiaMm4 * Math.Pow(lengthMm, 3));
                deflectionFactorForCheck = deflectionFactor;
            }
            else
            {
                momentFactor = aMm;
                deflectionFactor = Math.Pow(aMm, 3) / (3 * ElasticModulus * inertiaMm4);
                deflectionFactorForCheck = aMm * aMm * (3 * lengthMm - aMm) / (6 * ElasticModulus * inertiaMm4);
            }

            double breakingLoadKg = (sectionModulusMm3 * input.YieldStrength / Math.Max(momentFactor, 1e-6)) / Gravity;
            double allowableDeflectionMm = lengthMm / (input.SupportType == "cantilever" ? 150.0 : 250.0);
            double safeLoadByStress = breakingLoadKg / 1.5;
            double safeLoadByDeflection = allowableDeflectionMm / Math.Max(deflectionFactorForCheck, 1e-9) / Gravity;
            double safeLoadKg = Math.Min(safeLoadByStress, safeLoadByDeflection);
            double actualDeflectionMm = loadKg * Gravity * deflectionFactor;
            double stressMpa = loadKg * Gravity * momentFactor / Math.Max(sectionModulusMm3, 1e-6);
            double stressRatio = stressMpa / Math.Max(input.YieldStrength, 1e-6);
            bool isBroken = stressMpa >= input.YieldStrength;

            return new CalculationResult
            {
                InertiaMm4 = inertiaMm4,
                SectionModulusMm3 = sectionModulusMm3,
                BreakingLoadKg = SafeNumber(breakingLoadKg),
                SafeLoadKg = SafeNumber(safeLoadKg),
                ActualDeflectionMm = SafeNumber(actualDeflectionMm),
                AllowableDeflectionMm = SafeNumber(allowableDeflectionMm),
                StressMpa = SafeNumber(stressMpa),
                StressRatio = SafeNumber(stressRatio),
                PositionRatio = lengthMeters <= 0 ? 0.5 : positionMeters / lengthMeters,
                RelativeSag = Math.Min((actualDeflectionMm / Math.Max(lengthMm, 1e-6)) * 3000.0, 100.0),
                IsBroken = isBroken,
                StatusText = BuildStatusText(isBroken, actualDeflectionMm, allowableDeflectionMm, stressMpa, input.YieldStrength),
                Recommendations = BuildRecommendations(input, momentFactor, allowableDeflectionMm).ToArray()
            };
        }

        private static void ResolveSectionProperties(CalculationInput input, out double inertiaMm4, out double sectionModulusMm3)
        {
            string category = input.CategoryKey ?? "tube";
            string orientation = input.Orientation ?? "strong";
            double d1 = Math.Max(input.Dimension1, 1);
            double d2 = Math.Max(input.Dimension2, 1);
            double d3 = Math.Max(input.Dimension3, 0.1);

            if (category == "tube")
            {
                double h = orientation == "strong" ? d1 : d2;
                double b = orientation == "strong" ? d2 : d1;
                inertiaMm4 = (b * Math.Pow(h, 3) - (b - 2 * d3) * Math.Pow(h - 2 * d3, 3)) / 12.0;
                sectionModulusMm3 = inertiaMm4 / (h / 2.0);
            }
            else if (category == "round_tube")
            {
                inertiaMm4 = Math.PI * (Math.Pow(d1, 4) - Math.Pow(d1 - 2 * d3, 4)) / 64.0;
                sectionModulusMm3 = inertiaMm4 / (d1 / 2.0);
            }
            else if (category == "solid_round")
            {
                inertiaMm4 = Math.PI * Math.Pow(d1, 4) / 64.0;
                sectionModulusMm3 = Math.PI * Math.Pow(d1, 3) / 32.0;
            }
            else if (category == "solid_square")
            {
                inertiaMm4 = Math.Pow(d1, 4) / 12.0;
                sectionModulusMm3 = Math.Pow(d1, 3) / 6.0;
            }
            else if (category == "solid_hex")
            {
                inertiaMm4 = 0.0601 * Math.Pow(d1, 4);
                sectionModulusMm3 = inertiaMm4 / (d1 / 2.0);
            }
            else if (category == "solid_flat")
            {
                double h = orientation == "strong" ? d1 : d2;
                double b = orientation == "strong" ? d2 : d1;
                inertiaMm4 = b * Math.Pow(h, 3) / 12.0;
                sectionModulusMm3 = b * Math.Pow(h, 2) / 6.0;
            }
            else
            {
                SectionSpec spec = input.SelectedSpec ?? SectionProfileDatabase.GetSpecs(category)[0];
                bool useStrongAxis = orientation == "strong";
                inertiaMm4 = (useStrongAxis ? spec.Ix : spec.Iy) * 10000.0;
                sectionModulusMm3 = (useStrongAxis ? spec.Wx : spec.Wy) * 1000.0;
            }
        }

        private static List<Recommendation> BuildRecommendations(CalculationInput input, double momentFactor, double allowableDeflectionMm)
        {
            var candidates = new List<Recommendation>();
            double loadN = input.LoadKg * Gravity;
            if (loadN <= 0)
            {
                return candidates;
            }

            double requiredSectionModulusCm3 = loadN * momentFactor * 1.5 / Math.Max(input.YieldStrength, 1e-6) / 1000.0;
            double lengthMm = input.LengthMeters * 1000.0;
            double aMm = input.PositionMeters * 1000.0;
            double bMm = lengthMm - aMm;
            double geom;

            if (input.SupportType == "ss")
            {
                geom = (Math.Pow(aMm, 2) * Math.Pow(bMm, 2)) / (3.0 * Math.Max(lengthMm, 1e-6));
            }
            else if (input.SupportType == "ff")
            {
                geom = (Math.Pow(aMm, 3) * Math.Pow(bMm, 3)) / (3.0 * Math.Pow(Math.Max(lengthMm, 1e-6), 3));
            }
            else
            {
                geom = aMm * aMm * (3.0 * lengthMm - aMm) / 6.0;
            }

            double requiredInertiaCm4 = loadN * geom / (ElasticModulus * Math.Max(allowableDeflectionMm, 1e-6)) / 10000.0;

            foreach (KeyValuePair<string, IReadOnlyList<SectionSpec>> pair in SectionProfileDatabase.DatabaseSections)
            {
                if (pair.Key.Contains("angle") || pair.Key.Contains("purlin"))
                {
                    continue;
                }

                IReadOnlyList<SectionSpec> specs = pair.Value;
                for (int i = 0; i < specs.Count; i++)
                {
                    SectionSpec spec = specs[i];
                    if (spec.Ix >= requiredInertiaCm4 && spec.Wx >= requiredSectionModulusCm3)
                    {
                        candidates.Add(new Recommendation
                        {
                            UsesDatabase = true,
                            CategoryKey = pair.Key,
                            SpecIndex = i,
                            Name = spec.Name,
                            RankingInertia = spec.Ix
                        });
                    }
                }
            }

            foreach (CommonTubeSpec tube in SectionProfileDatabase.CommonTubeSpecs)
            {
                double inertiaCm4 = (tube.Width * Math.Pow(tube.Height, 3) - (tube.Width - 2 * tube.Thickness) * Math.Pow(tube.Height - 2 * tube.Thickness, 3)) / 120000.0;
                double sectionModulusCm3 = (inertiaCm4 * 10000.0) / (tube.Height / 2.0) / 1000.0;
                if (inertiaCm4 >= requiredInertiaCm4 && sectionModulusCm3 >= requiredSectionModulusCm3)
                {
                    candidates.Add(new Recommendation
                    {
                        UsesDatabase = false,
                        CategoryKey = "tube",
                        Dimension1 = tube.Height,
                        Dimension2 = tube.Width,
                        Dimension3 = tube.Thickness,
                        Name = (Math.Abs(tube.Height - tube.Width) < 0.001 ? "方管 " : "矩管 ") +
                               $"{tube.Height:0}x{tube.Width:0}x{tube.Thickness:0.0}",
                        RankingInertia = inertiaCm4
                    });
                }
            }

            candidates.Sort((left, right) => left.RankingInertia.CompareTo(right.RankingInertia));
            if (candidates.Count > 5)
            {
                candidates.RemoveRange(5, candidates.Count - 5);
            }

            return candidates;
        }

        private static string BuildStatusText(bool isBroken, double actualDeflectionMm, double allowableDeflectionMm, double stressMpa, double yieldStrength)
        {
            if (isBroken)
            {
                return "极限破坏：荷载已超过型材屈服极限，结构失效。";
            }

            if (actualDeflectionMm > allowableDeflectionMm)
            {
                return "挠度过大：虽未屈服，但已出现明显下挠，建议加大截面或缩短跨度。";
            }

            if (stressMpa > yieldStrength * 0.8)
            {
                return "高应力状态：材料已接近极限区间，建议预留更大安全裕量。";
            }

            return "工程安全：当前载荷、挠度和应力均在允许范围内。";
        }

        private static double SafeNumber(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                return 0;
            }

            return Math.Max(0, value);
        }
    }
}
