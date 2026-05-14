using System.Collections.Generic;

namespace SectionProfileTool
{
    internal static class SectionProfileDatabase
    {
        public static IReadOnlyList<ProfileCategoryDefinition> Categories { get; } =
            new List<ProfileCategoryDefinition>
            {
                new ProfileCategoryDefinition { Key = "tube", DisplayName = "方形/矩形空心型钢 GB/T 6728 / 3094", UsesDatabase = false, AllowOrientation = true, Dimension1Label = "外高 H (mm)", Dimension2Label = "外宽 B (mm)", Dimension3Label = "壁厚 t (mm)", ShowDimension2 = true, ShowDimension3 = true },
                new ProfileCategoryDefinition { Key = "round_tube", DisplayName = "圆管 (直缝电焊管/无缝管)", UsesDatabase = false, AllowOrientation = false, Dimension1Label = "外径 D (mm)", Dimension2Label = "外宽 B (mm)", Dimension3Label = "壁厚 t (mm)", ShowDimension2 = false, ShowDimension3 = true },
                new ProfileCategoryDefinition { Key = "hbeam", DisplayName = "H型钢 / 热轧H型钢", UsesDatabase = true, AllowOrientation = true, Dimension1Label = "", Dimension2Label = "", Dimension3Label = "", ShowDimension2 = false, ShowDimension3 = false },
                new ProfileCategoryDefinition { Key = "ibeam", DisplayName = "热轧工字钢 GB/T 706", UsesDatabase = true, AllowOrientation = true, Dimension1Label = "", Dimension2Label = "", Dimension3Label = "", ShowDimension2 = false, ShowDimension3 = false },
                new ProfileCategoryDefinition { Key = "channel", DisplayName = "热轧槽钢 GB/T 706", UsesDatabase = true, AllowOrientation = true, Dimension1Label = "", Dimension2Label = "", Dimension3Label = "", ShowDimension2 = false, ShowDimension3 = false },
                new ProfileCategoryDefinition { Key = "lsteel", DisplayName = "热轧 L 型钢", UsesDatabase = true, AllowOrientation = true, Dimension1Label = "", Dimension2Label = "", Dimension3Label = "", ShowDimension2 = false, ShowDimension3 = false },
                new ProfileCategoryDefinition { Key = "angle_equal", DisplayName = "热轧等边角钢", UsesDatabase = true, AllowOrientation = true, Dimension1Label = "", Dimension2Label = "", Dimension3Label = "", ShowDimension2 = false, ShowDimension3 = false },
                new ProfileCategoryDefinition { Key = "angle_unequal", DisplayName = "热轧不等边角钢", UsesDatabase = true, AllowOrientation = true, Dimension1Label = "", Dimension2Label = "", Dimension3Label = "", ShowDimension2 = false, ShowDimension3 = false },
                new ProfileCategoryDefinition { Key = "solid_square", DisplayName = "热轧方钢 GB/T 702", UsesDatabase = false, AllowOrientation = false, Dimension1Label = "边长 a (mm)", Dimension2Label = "外宽 B (mm)", Dimension3Label = "壁厚 t (mm)", ShowDimension2 = false, ShowDimension3 = false },
                new ProfileCategoryDefinition { Key = "solid_round", DisplayName = "热轧圆钢 GB/T 702", UsesDatabase = false, AllowOrientation = false, Dimension1Label = "直径 D (mm)", Dimension2Label = "外宽 B (mm)", Dimension3Label = "壁厚 t (mm)", ShowDimension2 = false, ShowDimension3 = false },
                new ProfileCategoryDefinition { Key = "solid_flat", DisplayName = "热轧扁钢 GB/T 704", UsesDatabase = false, AllowOrientation = true, Dimension1Label = "高度 H (mm)", Dimension2Label = "宽度 B (mm)", Dimension3Label = "壁厚 t (mm)", ShowDimension2 = true, ShowDimension3 = false },
                new ProfileCategoryDefinition { Key = "solid_hex", DisplayName = "热轧六角钢 / 八角钢 GB/T 705", UsesDatabase = false, AllowOrientation = false, Dimension1Label = "对边尺寸 s (mm)", Dimension2Label = "外宽 B (mm)", Dimension3Label = "壁厚 t (mm)", ShowDimension2 = false, ShowDimension3 = false },
                new ProfileCategoryDefinition { Key = "cpurlin", DisplayName = "冷弯 C 型钢", UsesDatabase = true, AllowOrientation = true, Dimension1Label = "", Dimension2Label = "", Dimension3Label = "", ShowDimension2 = false, ShowDimension3 = false },
                new ProfileCategoryDefinition { Key = "zpurlin", DisplayName = "冷弯 Z 型钢", UsesDatabase = true, AllowOrientation = true, Dimension1Label = "", Dimension2Label = "", Dimension3Label = "", ShowDimension2 = false, ShowDimension3 = false },
                new ProfileCategoryDefinition { Key = "rail", DisplayName = "起重机钢轨 / 重轨 / 轻轨", UsesDatabase = true, AllowOrientation = true, Dimension1Label = "", Dimension2Label = "", Dimension3Label = "", ShowDimension2 = false, ShowDimension3 = false }
            };

        public static IReadOnlyDictionary<string, IReadOnlyList<SectionSpec>> DatabaseSections { get; } =
            new Dictionary<string, IReadOnlyList<SectionSpec>>
            {
                ["hbeam"] = new List<SectionSpec>
                {
                    new SectionSpec("HW 100x100x6x8", 383, 76.5, 134, 26.7),
                    new SectionSpec("HM 150x100x3.2x4.5", 606, 80.8, 105, 21.0),
                    new SectionSpec("HW 150x150x7x10", 1640, 219, 563, 75.1),
                    new SectionSpec("HN 175x90x5x8", 749, 85.6, 96.4, 21.4),
                    new SectionSpec("HN 200x100x5.5x8", 1130, 112, 134, 26.7),
                    new SectionSpec("HW 200x200x8x12", 4720, 472, 1600, 160),
                    new SectionSpec("HM 250x175x7x11", 8150, 652, 1850, 211),
                    new SectionSpec("HW 300x300x10x15", 20400, 1360, 6750, 450)
                },
                ["ibeam"] = new List<SectionSpec>
                {
                    new SectionSpec("10# (100x68)", 245, 49.0, 17.5, 5.1),
                    new SectionSpec("12.6# (126x74)", 489, 77.7, 31.5, 8.5),
                    new SectionSpec("14# (140x80)", 712, 102, 49.0, 12.3),
                    new SectionSpec("16# (160x88)", 1130, 141, 73.3, 16.6),
                    new SectionSpec("18# (180x94)", 1660, 185, 101, 21.5),
                    new SectionSpec("20#a (200x100)", 2370, 237, 158, 31.6),
                    new SectionSpec("25#a (250x116)", 5010, 401, 322, 55.5),
                    new SectionSpec("32#a (320x130)", 11080, 693, 680, 105)
                },
                ["channel"] = new List<SectionSpec>
                {
                    new SectionSpec("8# (80x43)", 101, 25.3, 15.0, 5.6),
                    new SectionSpec("10# (100x48)", 256, 51.2, 37.8, 12.2),
                    new SectionSpec("12.6# (126x53)", 459, 72.9, 63.1, 17.5),
                    new SectionSpec("14#a (140x58)", 631, 90.1, 80.3, 22.4),
                    new SectionSpec("16#a (160x63)", 934, 116, 108, 25.3),
                    new SectionSpec("20#a (200x73)", 1780, 178, 178, 34.8),
                    new SectionSpec("25#a (250x78)", 2940, 235, 223, 43.0),
                    new SectionSpec("32#a (320x88)", 6280, 393, 397, 63.7)
                },
                ["lsteel"] = new List<SectionSpec>
                {
                    new SectionSpec("L 100x80x8", 204, 24.8, 90.3, 13.0),
                    new SectionSpec("L 125x80x10", 337, 38.4, 109, 17.0),
                    new SectionSpec("L 150x100x10", 421, 43.1, 160, 21.6),
                    new SectionSpec("L 180x110x12", 752, 67.8, 289, 34.0),
                    new SectionSpec("L 200x125x12", 980, 86.0, 402, 44.0),
                    new SectionSpec("L 200x150x12", 1160, 90.5, 554, 53.4)
                },
                ["angle_equal"] = new List<SectionSpec>
                {
                    new SectionSpec("L40x4", 5.95, 2.10, 5.95, 2.10),
                    new SectionSpec("L50x5", 11.2, 3.13, 11.2, 3.13),
                    new SectionSpec("L63x6", 24.6, 5.09, 24.6, 5.09),
                    new SectionSpec("L75x8", 52.0, 9.20, 52.0, 9.20),
                    new SectionSpec("L90x9", 95.3, 14.4, 95.3, 14.4),
                    new SectionSpec("L100x10", 146, 20.2, 146, 20.2),
                    new SectionSpec("L125x12", 311, 35.0, 311, 35.0)
                },
                ["angle_unequal"] = new List<SectionSpec>
                {
                    new SectionSpec("L75x50x5", 28.1, 5.85, 10.6, 3.35),
                    new SectionSpec("L90x56x6", 52.4, 9.45, 19.6, 5.07),
                    new SectionSpec("L100x75x8", 112, 16.5, 52.8, 9.55),
                    new SectionSpec("L125x80x10", 203, 25.7, 79.0, 12.4),
                    new SectionSpec("L140x90x10", 275, 31.7, 102, 14.3),
                    new SectionSpec("L160x100x12", 456, 43.5, 159, 20.2)
                },
                ["cpurlin"] = new List<SectionSpec>
                {
                    new SectionSpec("C80x40x15x2.0", 31.7, 7.92, 8.21, 3.08),
                    new SectionSpec("C100x50x20x2.0", 60.5, 12.1, 16.8, 5.2),
                    new SectionSpec("C120x50x20x2.5", 101, 16.9, 19.5, 5.8),
                    new SectionSpec("C140x60x20x2.5", 164, 23.4, 28.2, 7.4),
                    new SectionSpec("C160x60x20x3.0", 255, 31.8, 34.7, 8.7),
                    new SectionSpec("C180x70x20x3.0", 390, 43.3, 49.1, 10.8)
                },
                ["zpurlin"] = new List<SectionSpec>
                {
                    new SectionSpec("Z120x50x20x2.0", 74.6, 12.4, 17.2, 4.9),
                    new SectionSpec("Z140x50x20x2.5", 144, 20.5, 22.0, 5.9),
                    new SectionSpec("Z160x60x2.5", 238, 29.8, 50.1, 11.2),
                    new SectionSpec("Z180x70x20x3.0", 420, 46.7, 68.0, 13.2),
                    new SectionSpec("Z200x70x20x3.0", 560, 56.0, 76.5, 15.0)
                },
                ["rail"] = new List<SectionSpec>
                {
                    new SectionSpec("P18 轻轨", 245, 54.4, 46.5, 10.3),
                    new SectionSpec("P24 轻轨", 405, 73.6, 74.4, 14.1),
                    new SectionSpec("P38 重轨", 1020, 163, 192, 33.0),
                    new SectionSpec("P43 重轨", 1489, 237, 260, 45.6),
                    new SectionSpec("QU70 起重机轨", 1320, 188, 179, 28.3),
                    new SectionSpec("QU80 起重机轨", 2090, 261, 268, 35.8)
                }
            };

        public static IReadOnlyList<CommonTubeSpec> CommonTubeSpecs { get; } =
            new List<CommonTubeSpec>
            {
                new CommonTubeSpec(40, 40, 2.5), new CommonTubeSpec(40, 40, 3.0),
                new CommonTubeSpec(50, 30, 2.5), new CommonTubeSpec(50, 50, 3.0),
                new CommonTubeSpec(60, 40, 3.0), new CommonTubeSpec(60, 40, 4.0), new CommonTubeSpec(60, 60, 4.0),
                new CommonTubeSpec(80, 40, 4.0), new CommonTubeSpec(80, 60, 4.0), new CommonTubeSpec(80, 80, 5.0),
                new CommonTubeSpec(100, 50, 4.0), new CommonTubeSpec(100, 60, 4.0), new CommonTubeSpec(100, 100, 5.0), new CommonTubeSpec(100, 100, 6.0),
                new CommonTubeSpec(120, 60, 5.0), new CommonTubeSpec(120, 80, 5.0), new CommonTubeSpec(120, 120, 6.0),
                new CommonTubeSpec(150, 100, 6.0), new CommonTubeSpec(150, 100, 8.0), new CommonTubeSpec(150, 150, 8.0),
                new CommonTubeSpec(160, 80, 6.0), new CommonTubeSpec(180, 100, 8.0),
                new CommonTubeSpec(200, 100, 8.0), new CommonTubeSpec(200, 150, 8.0), new CommonTubeSpec(200, 200, 10.0),
                new CommonTubeSpec(250, 150, 8.0), new CommonTubeSpec(250, 150, 10.0), new CommonTubeSpec(250, 250, 10.0),
                new CommonTubeSpec(300, 200, 10.0)
            };

        public static ProfileCategoryDefinition GetCategory(string key)
        {
            foreach (ProfileCategoryDefinition category in Categories)
            {
                if (category.Key == key)
                {
                    return category;
                }
            }

            return Categories[0];
        }

        public static IReadOnlyList<SectionSpec> GetSpecs(string key)
        {
            if (DatabaseSections.TryGetValue(key, out IReadOnlyList<SectionSpec> specs))
            {
                return specs;
            }

            return new List<SectionSpec>();
        }
    }
}
