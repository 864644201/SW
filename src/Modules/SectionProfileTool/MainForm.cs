using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SectionProfileTool
{
    public class MainForm : Form
    {
        private readonly ComboBox _categoryCombo = CreateDropDown();
        private readonly ComboBox _orientationCombo = CreateDropDown();
        private readonly ComboBox _materialCombo = CreateDropDown();
        private readonly ComboBox _supportCombo = CreateDropDown();
        private readonly ComboBox _specCombo = CreateDropDown();
        private readonly NumericUpDown _dim1Input = CreateNumeric(120, 10, 1000, 0);
        private readonly NumericUpDown _dim2Input = CreateNumeric(80, 10, 1000, 0);
        private readonly NumericUpDown _dim3Input = CreateNumeric(5, 0.5m, 100, 1);
        private readonly NumericUpDown _lengthInput = CreateNumeric(4, 0.5m, 30, 2);
        private readonly NumericUpDown _positionInput = CreateNumeric(2, 0, 30, 2);
        private readonly NumericUpDown _positionPercentInput = CreateNumeric(50, 0, 100, 1);
        private readonly NumericUpDown _loadInput = CreateNumeric(2000, 0, 100000, 0, 50);
        private readonly TrackBar _lengthBar = CreateBar(5, 300, 40, 5);
        private readonly TrackBar _positionBar = CreateBar(0, 100, 50, 1);
        private readonly TrackBar _loadBar = CreateBar(0, 2000, 200, 10);
        private readonly Label _dim1Label = CreateCaptionLabel();
        private readonly Label _dim2Label = CreateCaptionLabel();
        private readonly Label _dim3Label = CreateCaptionLabel();
        private readonly Label _specLabel = CreateCaptionLabel("特定型号库");
        private readonly Panel _dynamicPanel = new Panel();
        private readonly Panel _specPanel = new Panel();
        private readonly Label _positionPercentLabel = new Label();
        private readonly Label _breakingLoadValue = CreateValueLabel();
        private readonly Label _safeLoadValue = CreateValueLabel();
        private readonly Label _statusLabel = new Label();
        private readonly Label _summaryLabel = new Label();
        private readonly Button _copyReportButton = CreateActionButton("复制结论");
        private readonly Button _saveReportButton = CreateActionButton("导出报告");
        private readonly Label _deflectionValue = CreateDataValueLabel();
        private readonly Label _ratioValue = CreateDataValueLabel();
        private readonly Label _inertiaValue = CreateDataValueLabel();
        private readonly Label _stressValue = CreateDataValueLabel();
        private readonly ListBox _recommendationList = new ListBox();
        private readonly BeamDiagramPanel _beamPanel = new BeamDiagramPanel();
        private readonly SectionPreviewPanel _sectionPreview = new SectionPreviewPanel();
        private readonly Timer _animationTimer = new Timer();
        private SplitContainer _mainSplit;

        private CalculationResult _result;
        private CalculationInput _lastInput;
        private double _animatedSag;
        private bool _isInitializing;
        private bool _isUpdatingPositionControls;

        public MainForm()
        {
            Text = "型材工具";
            Width = 1480;
            Height = 940;
            MinimumSize = new Size(1320, 820);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245, 245, 247);
            Load += (sender, args) => ApplyResponsiveSplitLayout();
            Resize += (sender, args) => ApplyResponsiveSplitLayout();

            BuildLayout();
            InitializeData();

            _animationTimer.Interval = 16;
            _animationTimer.Tick += AnimationTimer_Tick;
            _animationTimer.Start();
        }

        private void BuildLayout()
        {
            _mainSplit = new SplitContainer
            {
                Dock = DockStyle.Fill,
                FixedPanel = FixedPanel.Panel1,
                IsSplitterFixed = false,
                BackColor = BackColor
            };
            Controls.Add(_mainSplit);

            _mainSplit.Panel1.Padding = new Padding(18);
            _mainSplit.Panel2.Padding = new Padding(18, 18, 18, 18);

            _mainSplit.Panel1.Controls.Add(BuildSidebar());
            _mainSplit.Panel2.Controls.Add(BuildMainArea());
        }

        private Control BuildSidebar()
        {
            var root = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(251, 251, 253),
                Padding = new Padding(16)
            };

            var layout = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };
            root.Controls.Add(layout);
            layout.SizeChanged += (sender, args) => ResizeSidebarItems(layout);

            var title = new Label
            {
                Text = "全系国标型材仿真",
                Font = new Font("Microsoft YaHei UI", 14f, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 6)
            };
            var subtitle = new Label
            {
                Text = "Weldments Engineering System",
                ForeColor = Color.FromArgb(0, 113, 227),
                Font = new Font("Microsoft YaHei UI", 8.5f, FontStyle.Bold),
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 14)
            };
            layout.Controls.Add(title);
            layout.Controls.Add(subtitle);

            layout.Controls.Add(BuildField("型材类别", _categoryCombo));

            var row = new TableLayoutPanel { ColumnCount = 2, Height = 70, Margin = new Padding(0, 0, 0, 10) };
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            row.Controls.Add(BuildField("摆放姿态", _orientationCombo), 0, 0);
            row.Controls.Add(BuildField("牌号", _materialCombo), 1, 0);
            layout.Controls.Add(row);

            layout.Controls.Add(BuildField("支撑条件", _supportCombo));

            _dynamicPanel.Height = 146;
            _dynamicPanel.Margin = new Padding(0, 0, 0, 10);
            _dynamicPanel.Padding = new Padding(12);
            _dynamicPanel.BackColor = Color.FromArgb(240, 247, 255);
            BuildDynamicPanel();
            layout.Controls.Add(_dynamicPanel);

            _specPanel.Height = 84;
            _specPanel.Padding = new Padding(12);
            _specPanel.Margin = new Padding(0, 0, 0, 10);
            _specPanel.BackColor = Color.FromArgb(238, 242, 245);
            _specPanel.Controls.Add(BuildField(_specLabel.Text, _specCombo));
            layout.Controls.Add(_specPanel);

            layout.Controls.Add(BuildRangeCard("受力跨度 L (m)", _lengthInput, _lengthBar, Color.FromArgb(0, 113, 227)));
            layout.Controls.Add(BuildPositionRangeCard());
            layout.Controls.Add(BuildRangeCard("载荷重量 P (kg)", _loadInput, _loadBar, Color.FromArgb(255, 59, 48)));

            ResizeSidebarItems(layout);

            return root;
        }

        private void BuildDynamicPanel()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));

            layout.Controls.Add(BuildLabeledInput(_dim1Label, _dim1Input), 0, 0);
            layout.Controls.Add(BuildLabeledInput(_dim2Label, _dim2Input), 1, 0);
            layout.Controls.Add(BuildLabeledInput(_dim3Label, _dim3Input), 0, 1);
            layout.SetColumnSpan(layout.GetControlFromPosition(0, 1), 2);
            _dynamicPanel.Controls.Add(layout);
        }

        private Control BuildMainArea()
        {
            var scrollHost = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = BackColor
            };
            scrollHost.SizeChanged += (sender, args) => ResizeMainCards(scrollHost);

            var summaryPanel = new Panel { Height = 104, Margin = new Padding(0, 0, 0, 12), BackColor = Color.FromArgb(248, 250, 252), Padding = new Padding(16, 10, 16, 10) };
            var summaryActions = new Panel { Dock = DockStyle.Top, Height = 34 };
            _saveReportButton.Dock = DockStyle.Right;
            _copyReportButton.Dock = DockStyle.Right;
            _copyReportButton.Margin = new Padding(0, 0, 8, 0);
            _copyReportButton.Click += CopyReportButton_Click;
            _saveReportButton.Click += SaveReportButton_Click;
            summaryActions.Controls.Add(_saveReportButton);
            summaryActions.Controls.Add(_copyReportButton);

            _summaryLabel.Dock = DockStyle.Fill;
            _summaryLabel.Font = new Font("Microsoft YaHei UI", 9.5f, FontStyle.Regular);
            _summaryLabel.ForeColor = Color.FromArgb(70, 70, 70);
            _summaryLabel.TextAlign = ContentAlignment.TopLeft;
            summaryPanel.Controls.Add(_summaryLabel);
            summaryPanel.Controls.Add(summaryActions);
            scrollHost.Controls.Add(summaryPanel);

            var beamHost = new Panel { Height = 300, Padding = new Padding(0, 0, 0, 10), Margin = new Padding(0, 0, 0, 12) };
            _beamPanel.Dock = DockStyle.Fill;
            beamHost.Controls.Add(_beamPanel);
            _sectionPreview.Location = new Point(24, 24);
            _sectionPreview.Parent = _beamPanel;
            _sectionPreview.BringToFront();
            scrollHost.Controls.Add(beamHost);

            var stats = new TableLayoutPanel { Height = 104, ColumnCount = 2, Margin = new Padding(0, 0, 0, 12) };
            stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            stats.Controls.Add(CreateHeroCard("此点位破坏载荷", _breakingLoadValue, Color.FromArgb(44, 62, 80)), 0, 0);
            stats.Controls.Add(CreateHeroCard("建议安全载荷 (K=1.5)", _safeLoadValue, Color.FromArgb(0, 113, 227)), 1, 0);
            scrollHost.Controls.Add(stats);

            _statusLabel.Height = 72;
            _statusLabel.TextAlign = ContentAlignment.MiddleLeft;
            _statusLabel.Padding = new Padding(18, 0, 18, 0);
            _statusLabel.ForeColor = Color.White;
            _statusLabel.Font = new Font("Microsoft YaHei UI", 10f, FontStyle.Bold);
            _statusLabel.BackColor = Color.FromArgb(149, 165, 166);
            _statusLabel.Margin = new Padding(0, 0, 0, 12);
            scrollHost.Controls.Add(_statusLabel);

            var dataGrid = new TableLayoutPanel { Height = 102, ColumnCount = 4, Margin = new Padding(0, 0, 0, 12) };
            for (int i = 0; i < 4; i++)
            {
                dataGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            }
            dataGrid.Controls.Add(CreateDataCard("实际挠度 (f)", _deflectionValue), 0, 0);
            dataGrid.Controls.Add(CreateDataCard("应力饱和度", _ratioValue), 1, 0);
            dataGrid.Controls.Add(CreateDataCard("惯性矩 (I)", _inertiaValue), 2, 0);
            dataGrid.Controls.Add(CreateDataCard("最大正应力", _stressValue), 3, 0);
            scrollHost.Controls.Add(dataGrid);

            var recPanel = new Panel { Height = 280, BackColor = Color.FromArgb(240, 247, 255), Padding = new Padding(14), Margin = new Padding(0, 0, 0, 8) };
            var recTitle = new Label
            {
                Text = "基于当前工况的智能选型推荐（双击即可应用）",
                Dock = DockStyle.Top,
                Height = 28,
                ForeColor = Color.FromArgb(0, 77, 153),
                Font = new Font("Microsoft YaHei UI", 10f, FontStyle.Bold)
            };
            _recommendationList.Dock = DockStyle.Fill;
            _recommendationList.Font = new Font("Microsoft YaHei UI", 10f);
            _recommendationList.DoubleClick += RecommendationList_DoubleClick;
            recPanel.Controls.Add(_recommendationList);
            recPanel.Controls.Add(recTitle);
            scrollHost.Controls.Add(recPanel);

            ResizeMainCards(scrollHost);

            return scrollHost;
        }

        private void InitializeData()
        {
            _isInitializing = true;

            foreach (ProfileCategoryDefinition category in SectionProfileDatabase.Categories)
            {
                _categoryCombo.Items.Add(category);
            }
            _categoryCombo.DisplayMember = "DisplayName";

            _orientationCombo.Items.Add(new KeyValuePair<string, string>("strong", "立放 (强轴受弯)"));
            _orientationCombo.Items.Add(new KeyValuePair<string, string>("weak", "平放 (弱轴受弯)"));
            _orientationCombo.DisplayMember = "Value";

            _materialCombo.Items.Add(new KeyValuePair<double, string>(235, "Q235"));
            _materialCombo.Items.Add(new KeyValuePair<double, string>(355, "Q355"));
            _materialCombo.Items.Add(new KeyValuePair<double, string>(270, "不锈钢/铝"));
            _materialCombo.DisplayMember = "Value";

            _supportCombo.Items.Add(new KeyValuePair<string, string>("ss", "两端简支"));
            _supportCombo.Items.Add(new KeyValuePair<string, string>("ff", "两端固支"));
            _supportCombo.Items.Add(new KeyValuePair<string, string>("cantilever", "悬臂结构"));
            _supportCombo.DisplayMember = "Value";

            _categoryCombo.SelectedIndexChanged += (sender, args) => UpdateCategoryUI(true);
            _orientationCombo.SelectedIndexChanged += (sender, args) => Recalculate();
            _materialCombo.SelectedIndexChanged += (sender, args) => Recalculate();
            _supportCombo.SelectedIndexChanged += (sender, args) => Recalculate();
            _specCombo.SelectedIndexChanged += (sender, args) => Recalculate();
            _dim1Input.ValueChanged += (sender, args) => Recalculate();
            _dim2Input.ValueChanged += (sender, args) => Recalculate();
            _dim3Input.ValueChanged += (sender, args) => Recalculate();
            _lengthInput.ValueChanged += LengthInput_ValueChanged;
            _positionInput.ValueChanged += PositionInput_ValueChanged;
            _positionPercentInput.ValueChanged += PositionPercentInput_ValueChanged;
            _loadInput.ValueChanged += LoadInput_ValueChanged;
            _lengthBar.Scroll += LengthBar_Scroll;
            _positionBar.Scroll += PositionBar_Scroll;
            _loadBar.Scroll += LoadBar_Scroll;

            _orientationCombo.SelectedIndex = 0;
            _materialCombo.SelectedIndex = 0;
            _supportCombo.SelectedIndex = 1;
            _categoryCombo.SelectedIndex = 0;
            _positionBar.Value = 50;
            SyncPositionFromPercent();
            _loadBar.Value = (int)(_loadInput.Value / 10);
            _isInitializing = false;
            Recalculate();
        }

        private void UpdateCategoryUI(bool recalculate)
        {
            ProfileCategoryDefinition category = SelectedCategory;
            _dynamicPanel.Visible = !category.UsesDatabase;
            _specPanel.Visible = category.UsesDatabase;
            _orientationCombo.Enabled = category.AllowOrientation;
            if (!category.AllowOrientation)
            {
                _orientationCombo.SelectedIndex = 0;
            }

            _dim1Label.Text = category.Dimension1Label;
            _dim2Label.Text = category.Dimension2Label;
            _dim3Label.Text = category.Dimension3Label;
            _dim2Input.Visible = category.ShowDimension2;
            _dim3Input.Visible = category.ShowDimension3;
            _dim2Label.Visible = category.ShowDimension2;
            _dim3Label.Visible = category.ShowDimension3;

            _specCombo.Items.Clear();
            if (category.UsesDatabase)
            {
                IReadOnlyList<SectionSpec> specs = SectionProfileDatabase.GetSpecs(category.Key);
                foreach (SectionSpec spec in specs)
                {
                    _specCombo.Items.Add(spec);
                }

                if (_specCombo.Items.Count > 0)
                {
                    _specCombo.SelectedIndex = 0;
                }
            }

            if (recalculate)
            {
                Recalculate();
            }
        }

        private void Recalculate()
        {
            if (_isInitializing ||
                _categoryCombo.SelectedItem == null ||
                _orientationCombo.SelectedItem == null ||
                _materialCombo.SelectedItem == null ||
                _supportCombo.SelectedItem == null)
            {
                return;
            }

            CalculationInput input = new CalculationInput
            {
                CategoryKey = SelectedCategory.Key,
                Orientation = SelectedOrientation.Key,
                SupportType = SelectedSupport.Key,
                YieldStrength = SelectedMaterial.Key,
                LengthMeters = (double)_lengthInput.Value,
                PositionMeters = (double)_positionInput.Value,
                LoadKg = (double)_loadInput.Value,
                Dimension1 = (double)_dim1Input.Value,
                Dimension2 = (double)_dim2Input.Value,
                Dimension3 = (double)_dim3Input.Value,
                SelectedSpec = _specCombo.SelectedItem as SectionSpec
            };
            _lastInput = input;

            _result = SectionProfileCalculator.Calculate(input);
            _sectionPreview.CategoryKey = input.CategoryKey;
            _sectionPreview.Orientation = input.Orientation;
            _sectionPreview.Invalidate();

            _beamPanel.Result = _result;
            _beamPanel.SupportType = input.SupportType;
            _beamPanel.LoadKg = input.LoadKg;
            _summaryLabel.Text =
                $"当前工况：{BuildCurrentProfileDescriptor()}  |  {SelectedOrientation.Value}  |  {SelectedSupport.Value}  |  材料 {SelectedMaterial.Value}" + Environment.NewLine +
                $"跨度 {input.LengthMeters:F2} m，载荷点 {input.PositionMeters:F2} m（{_positionPercentInput.Value:F1}%），载荷 {input.LoadKg:F0} kg；建议安全载荷 {_result.SafeLoadKg:N0} kg，许用挠度 {_result.AllowableDeflectionMm:F2} mm。";

            _positionPercentLabel.Text = $"{_positionInput.Value:F2} m / {_positionPercentInput.Value:F1}%";
            _breakingLoadValue.Text = $"{_result.BreakingLoadKg:N0} kg";
            _safeLoadValue.Text = $"{_result.SafeLoadKg:N0} kg";
            _deflectionValue.Text = $"{_result.ActualDeflectionMm:F2} mm";
            _ratioValue.Text = $"{_result.StressRatio * 100:F1}%";
            _inertiaValue.Text = $"{_result.InertiaMm4 / 10000.0:F1} cm4";
            _stressValue.Text = $"{_result.StressMpa:F1} MPa";
            _statusLabel.Text = _result.StatusText;
            _statusLabel.BackColor = ResolveStatusColor(_result);

            _recommendationList.Items.Clear();
            if (_result.Recommendations.Length == 0)
            {
                _recommendationList.Items.Add("当前载荷未生成可用推荐，请尝试调整跨度、点位或载荷。");
            }
            else
            {
                foreach (Recommendation recommendation in _result.Recommendations)
                {
                    _recommendationList.Items.Add(recommendation);
                }
            }

            _beamPanel.Invalidate();
        }

        private void ApplyRecommendation(Recommendation recommendation)
        {
            for (int i = 0; i < _categoryCombo.Items.Count; i++)
            {
                if (((ProfileCategoryDefinition)_categoryCombo.Items[i]).Key == recommendation.CategoryKey)
                {
                    _categoryCombo.SelectedIndex = i;
                    break;
                }
            }

            _orientationCombo.SelectedIndex = 0;

            if (recommendation.UsesDatabase)
            {
                if (recommendation.SpecIndex >= 0 && recommendation.SpecIndex < _specCombo.Items.Count)
                {
                    _specCombo.SelectedIndex = recommendation.SpecIndex;
                }
            }
            else
            {
                _dim1Input.Value = ClampToRange((decimal)recommendation.Dimension1, _dim1Input);
                _dim2Input.Value = ClampToRange((decimal)recommendation.Dimension2, _dim2Input);
                _dim3Input.Value = ClampToRange((decimal)recommendation.Dimension3, _dim3Input);
            }

            Recalculate();
        }

        private void RecommendationList_DoubleClick(object sender, EventArgs e)
        {
            if (_recommendationList.SelectedItem is Recommendation recommendation)
            {
                ApplyRecommendation(recommendation);
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            double target = _result?.RelativeSag ?? 0;
            _animatedSag += (target - _animatedSag) * 0.18;
            if (_result != null && _result.IsBroken)
            {
                _animatedSag = Math.Min(110, _animatedSag + 1.4);
            }

            _beamPanel.AnimatedSag = _animatedSag;
            _beamPanel.Invalidate();
        }

        private void LengthInput_ValueChanged(object sender, EventArgs e)
        {
            _lengthBar.Value = Clamp((int)Math.Round(_lengthInput.Value * 10), _lengthBar.Minimum, _lengthBar.Maximum);
            SyncPositionFromPercent();
            Recalculate();
        }

        private void PositionInput_ValueChanged(object sender, EventArgs e)
        {
            SyncPercentFromPosition();
            Recalculate();
        }

        private void PositionPercentInput_ValueChanged(object sender, EventArgs e)
        {
            SyncPositionFromPercent();
            Recalculate();
        }

        private void LoadInput_ValueChanged(object sender, EventArgs e)
        {
            _loadBar.Value = Clamp((int)Math.Round(_loadInput.Value / 10), _loadBar.Minimum, _loadBar.Maximum);
            Recalculate();
        }

        private void LengthBar_Scroll(object sender, EventArgs e)
        {
            _lengthInput.Value = ClampToRange(_lengthBar.Value / 10m, _lengthInput);
        }

        private void PositionBar_Scroll(object sender, EventArgs e)
        {
            _positionPercentInput.Value = ClampToRange(_positionBar.Value, _positionPercentInput);
        }

        private void LoadBar_Scroll(object sender, EventArgs e)
        {
            _loadInput.Value = ClampToRange(_loadBar.Value * 10m, _loadInput);
        }

        private void SyncPercentFromPosition()
        {
            if (_isUpdatingPositionControls)
            {
                return;
            }

            _isUpdatingPositionControls = true;
            try
            {
                decimal meters = Math.Min(_positionInput.Value, _lengthInput.Value);
                if (meters != _positionInput.Value)
                {
                    _positionInput.Value = meters;
                }

                decimal percent = _lengthInput.Value <= 0 ? 0 : meters / _lengthInput.Value * 100m;
                _positionPercentInput.Value = ClampToRange(percent, _positionPercentInput);
                _positionBar.Value = Clamp((int)Math.Round(_positionPercentInput.Value), _positionBar.Minimum, _positionBar.Maximum);
            }
            finally
            {
                _isUpdatingPositionControls = false;
            }
        }

        private void SyncPositionFromPercent()
        {
            if (_isUpdatingPositionControls)
            {
                return;
            }

            _isUpdatingPositionControls = true;
            try
            {
                decimal percent = ClampToRange(_positionPercentInput.Value, _positionPercentInput);
                _positionBar.Value = Clamp((int)Math.Round(percent), _positionBar.Minimum, _positionBar.Maximum);
                decimal meters = _lengthInput.Value * percent / 100m;
                _positionInput.Value = ClampToRange(meters, _positionInput);
            }
            finally
            {
                _isUpdatingPositionControls = false;
            }
        }

        private ProfileCategoryDefinition SelectedCategory => (ProfileCategoryDefinition)_categoryCombo.SelectedItem;
        private KeyValuePair<string, string> SelectedOrientation => (KeyValuePair<string, string>)_orientationCombo.SelectedItem;
        private KeyValuePair<double, string> SelectedMaterial => (KeyValuePair<double, string>)_materialCombo.SelectedItem;
        private KeyValuePair<string, string> SelectedSupport => (KeyValuePair<string, string>)_supportCombo.SelectedItem;

        private static Color ResolveStatusColor(CalculationResult result)
        {
            if (result.IsBroken)
            {
                return Color.FromArgb(255, 59, 48);
            }

            if (result.ActualDeflectionMm > result.AllowableDeflectionMm)
            {
                return Color.FromArgb(255, 159, 10);
            }

            if (result.StressRatio > 0.8)
            {
                return Color.FromArgb(230, 126, 34);
            }

            return Color.FromArgb(52, 199, 89);
        }

        private static Panel BuildField(string caption, Control input)
        {
            return BuildField(CreateCaptionLabel(caption), input);
        }

        private static Panel BuildField(Label caption, Control input)
        {
            var panel = new Panel { Height = 64, Margin = new Padding(0, 0, 0, 10) };
            caption.Dock = DockStyle.Top;
            input.Dock = DockStyle.Top;
            panel.Controls.Add(input);
            panel.Controls.Add(caption);
            return panel;
        }

        private static Control BuildLabeledInput(Label caption, Control input)
        {
            var panel = new Panel { Dock = DockStyle.Fill, Height = 52, Margin = new Padding(0, 0, 8, 6) };
            caption.Dock = DockStyle.Top;
            input.Dock = DockStyle.Top;
            panel.Controls.Add(input);
            panel.Controls.Add(caption);
            return panel;
        }

        private static Control BuildRangeCard(string caption, NumericUpDown input, TrackBar bar, Color accent, Label suffixLabel = null)
        {
            var panel = new Panel
            {
                Height = 110,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(12),
                BackColor = Color.White
            };

            var header = new Panel { Dock = DockStyle.Top, Height = 28 };
            var title = new Label
            {
                Text = caption,
                Dock = DockStyle.Left,
                Width = 180,
                Font = new Font("Microsoft YaHei UI", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(96, 96, 96)
            };
            header.Controls.Add(title);

            if (suffixLabel != null)
            {
                suffixLabel.AutoSize = true;
                suffixLabel.Dock = DockStyle.Right;
                suffixLabel.TextAlign = ContentAlignment.MiddleRight;
                suffixLabel.ForeColor = accent;
                suffixLabel.Font = new Font("Microsoft YaHei UI", 8.5f, FontStyle.Bold);
                header.Controls.Add(suffixLabel);
            }

            input.Width = 92;
            input.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            input.Location = new Point(panel.Width - input.Width - 16, 0);
            input.ForeColor = accent;
            header.Controls.Add(input);
            header.Resize += (sender, args) => input.Left = header.Width - input.Width - (suffixLabel == null ? 0 : 42);

            bar.Dock = DockStyle.Bottom;
            bar.BackColor = Color.White;

            panel.Controls.Add(bar);
            panel.Controls.Add(header);
            return panel;
        }

        private Control BuildPositionRangeCard()
        {
            var accent = Color.FromArgb(142, 68, 173);
            var panel = new Panel
            {
                Height = 110,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(12),
                BackColor = Color.White
            };

            var header = new Panel { Dock = DockStyle.Top, Height = 28 };
            var title = new Label
            {
                Text = "受力位置 a",
                Dock = DockStyle.Left,
                Width = 140,
                Font = new Font("Microsoft YaHei UI", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(96, 96, 96)
            };
            header.Controls.Add(title);

            _positionInput.Width = 90;
            _positionInput.ForeColor = accent;
            _positionInput.Dock = DockStyle.Right;
            header.Controls.Add(_positionInput);

            var meterUnit = new Label
            {
                Text = "m",
                AutoSize = true,
                Dock = DockStyle.Right,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = accent,
                Font = new Font("Microsoft YaHei UI", 8.5f, FontStyle.Bold)
            };
            header.Controls.Add(meterUnit);

            var spacer = new Panel { Dock = DockStyle.Right, Width = 10 };
            header.Controls.Add(spacer);

            _positionPercentInput.Width = 76;
            _positionPercentInput.DecimalPlaces = 1;
            _positionPercentInput.ForeColor = accent;
            _positionPercentInput.Dock = DockStyle.Right;
            header.Controls.Add(_positionPercentInput);

            var percentUnit = new Label
            {
                Text = "%",
                AutoSize = true,
                Dock = DockStyle.Right,
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = accent,
                Font = new Font("Microsoft YaHei UI", 8.5f, FontStyle.Bold)
            };
            header.Controls.Add(percentUnit);

            _positionPercentLabel.AutoSize = true;
            _positionPercentLabel.Dock = DockStyle.Bottom;
            _positionPercentLabel.TextAlign = ContentAlignment.MiddleRight;
            _positionPercentLabel.ForeColor = accent;
            _positionPercentLabel.Font = new Font("Microsoft YaHei UI", 8.5f, FontStyle.Bold);

            _positionBar.Dock = DockStyle.Bottom;
            _positionBar.BackColor = Color.White;

            panel.Controls.Add(_positionPercentLabel);
            panel.Controls.Add(_positionBar);
            panel.Controls.Add(header);
            return panel;
        }

        private static Panel CreateHeroCard(string title, Label valueLabel, Color color)
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = color, Margin = new Padding(0, 0, 12, 0), Padding = new Padding(18) };
            var titleLabel = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Height = 28,
                ForeColor = Color.FromArgb(230, 235, 240),
                Font = new Font("Microsoft YaHei UI", 8.5f, FontStyle.Bold)
            };
            valueLabel.Dock = DockStyle.Fill;
            panel.Controls.Add(valueLabel);
            panel.Controls.Add(titleLabel);
            return panel;
        }

        private static Button CreateActionButton(string text)
        {
            return new Button
            {
                Text = text,
                Width = 92,
                Height = 28,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(32, 76, 136),
                Font = new Font("Microsoft YaHei UI", 8.5f, FontStyle.Bold)
            };
        }

        private static Panel CreateDataCard(string title, Label valueLabel)
        {
            var panel = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Margin = new Padding(0, 0, 12, 0), Padding = new Padding(14) };
            var titleLabel = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Height = 22,
                ForeColor = Color.FromArgb(125, 125, 125),
                Font = new Font("Microsoft YaHei UI", 8.5f)
            };
            valueLabel.Dock = DockStyle.Fill;
            panel.Controls.Add(valueLabel);
            panel.Controls.Add(titleLabel);
            return panel;
        }

        private static Label CreateCaptionLabel(string text = "")
        {
            return new Label
            {
                Text = text,
                Height = 22,
                AutoSize = false,
                Font = new Font("Microsoft YaHei UI", 8.8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(110, 110, 110)
            };
        }

        private static Label CreateValueLabel()
        {
            return new Label
            {
                ForeColor = Color.White,
                Font = new Font("Microsoft YaHei UI", 20f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private static Label CreateDataValueLabel()
        {
            return new Label
            {
                ForeColor = Color.FromArgb(33, 33, 33),
                Font = new Font("Microsoft YaHei UI", 12f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
        }

        private static ComboBox CreateDropDown()
        {
            return new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Height = 32,
                Font = new Font("Microsoft YaHei UI", 9.5f),
                Margin = new Padding(0)
            };
        }

        private static NumericUpDown CreateNumeric(decimal value, decimal min, decimal max, int decimals, int increment = 1)
        {
            var input = new NumericUpDown
            {
                Minimum = min,
                Maximum = max,
                DecimalPlaces = decimals,
                Increment = increment,
                Width = 110,
                Height = 30,
                Font = new Font("Microsoft YaHei UI", 10f, FontStyle.Bold)
            };

            input.Value = ClampDecimal(value, min, max);
            return input;
        }

        private static TrackBar CreateBar(int min, int max, int value, int tickFrequency)
        {
            return new TrackBar
            {
                Minimum = min,
                Maximum = max,
                Value = value,
                TickStyle = TickStyle.None,
                TickFrequency = tickFrequency,
                Height = 36
            };
        }

        private static int Clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        private static decimal ClampToRange(decimal value, NumericUpDown input)
        {
            if (value < input.Minimum)
            {
                return input.Minimum;
            }

            if (value > input.Maximum)
            {
                return input.Maximum;
            }

            return value;
        }

        private static decimal ClampDecimal(decimal value, decimal min, decimal max)
        {
            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        private static void ResizeSidebarItems(FlowLayoutPanel layout)
        {
            int width = layout.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 4;
            foreach (Control control in layout.Controls)
            {
                if (!(control is Label))
                {
                    control.Width = Math.Max(280, width);
                }
            }
        }

        private static void ResizeMainCards(FlowLayoutPanel layout)
        {
            int width = layout.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 6;
            foreach (Control control in layout.Controls)
            {
                control.Width = Math.Max(720, width);
            }
        }

        private void ApplyResponsiveSplitLayout()
        {
            if (_mainSplit == null || _mainSplit.ClientSize.Width <= 0)
            {
                return;
            }

            int availableWidth = _mainSplit.ClientSize.Width;
            _mainSplit.Panel1MinSize = 380;
            _mainSplit.Panel2MinSize = 480;

            int desiredLeft = Math.Max(420, Math.Min(560, availableWidth * 38 / 100));
            int maxLeft = Math.Max(_mainSplit.Panel1MinSize, availableWidth - _mainSplit.Panel2MinSize);
            desiredLeft = Math.Min(desiredLeft, maxLeft);
            desiredLeft = Math.Max(desiredLeft, _mainSplit.Panel1MinSize);

            if (desiredLeft > 0 && desiredLeft < availableWidth)
            {
                _mainSplit.SplitterDistance = desiredLeft;
            }
        }

        private string BuildCurrentProfileDescriptor()
        {
            if (SelectedCategory.UsesDatabase && _specCombo.SelectedItem is SectionSpec spec)
            {
                return $"{SelectedCategory.DisplayName} / {spec.Name}";
            }

            return $"{SelectedCategory.DisplayName} / {_dim1Input.Value:0.##} x {_dim2Input.Value:0.##} x {_dim3Input.Value:0.##} mm";
        }

        private string BuildSectionReport()
        {
            if (_result == null)
            {
                return "当前无可导出的计算结果。";
            }

            var builder = new StringBuilder();
            string line = new string('=', 56);
            string thinLine = new string('-', 56);

            // ===== 报告头 =====
            builder.AppendLine(line);
            builder.AppendLine("        型 材 工 具 工 程 结 论 报 告");
            builder.AppendLine("        Section Profile Engineering Report");
            builder.AppendLine(line);
            builder.AppendLine($"报告日期: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            builder.AppendLine($"软件版本: 麦豆宝型材工具 v2.0");
            builder.AppendLine(thinLine);

            // ===== 一、工况参数 =====
            builder.AppendLine();
            builder.AppendLine("【一、工况参数 Input Parameters】");
            builder.AppendLine();
            builder.AppendLine($"  型材截面:  {BuildCurrentProfileDescriptor()}");
            builder.AppendLine($"  摆放姿态:  {SelectedOrientation.Value}");
            builder.AppendLine($"  支撑条件:  {SelectedSupport.Value}");
            builder.AppendLine($"  材料牌号:  {SelectedMaterial.Value}  (屈服强度 {SelectedMaterial.Key:F0} MPa)");
            builder.AppendLine($"  受力跨度:  L = {_lengthInput.Value:F2} m");
            builder.AppendLine($"  载荷位置:  a = {_positionInput.Value:F2} m  (距左端 {_positionPercentInput.Value:F1}%)");
            builder.AppendLine($"  施加载荷:  P = {_loadInput.Value:F0} kg  ({(double)_loadInput.Value * 9.8 / 1000:F2} kN)");

            // ===== 二、截面力学参数 =====
            builder.AppendLine();
            builder.AppendLine(thinLine);
            builder.AppendLine("【二、截面力学参数 Section Properties】");
            builder.AppendLine();
            builder.AppendLine($"  惯性矩 Ix:    {_result.InertiaMm4 / 10000.0:F2} cm4  ({_result.InertiaMm4:F0} mm4)");
            builder.AppendLine($"  抗弯截面模量 W: {_result.SectionModulusMm3 / 1000.0:F2} cm3  ({_result.SectionModulusMm3:F0} mm3)");

            // ===== 三、强度校核 =====
            builder.AppendLine();
            builder.AppendLine(thinLine);
            builder.AppendLine("【三、强度校核 Strength Check】");
            builder.AppendLine();
            builder.AppendLine($"  极限破坏载荷:  {_result.BreakingLoadKg:N0} kg  ({_result.BreakingLoadKg * 9.8 / 1000:F2} kN)");
            builder.AppendLine($"  建议安全载荷:  {_result.SafeLoadKg:N0} kg  (安全系数 K = 1.5)");
            builder.AppendLine($"  最大弯曲正应力: σ = {_result.StressMpa:F2} MPa");
            builder.AppendLine($"  材料屈服强度:  fy = {SelectedMaterial.Key:F0} MPa");
            builder.AppendLine($"  应力饱和度:    σ/fy = {_result.StressRatio * 100:F1}%");
            string stressVerdict;
            if (_result.StressRatio >= 1.0)
                stressVerdict = "  ▶ 强度判定:  ❌ 不合格 — 应力已超过屈服强度，结构将发生塑性破坏";
            else if (_result.StressRatio >= 0.8)
                stressVerdict = "  ▶ 强度判定:  ⚠ 勉强 — 应力接近屈服极限，安全裕度不足";
            else if (_result.StressRatio >= 0.6)
                stressVerdict = "  ▶ 强度判定:  △ 一般 — 应力在允许范围内，但裕量偏小";
            else
                stressVerdict = "  ▶ 强度判定:  ✔ 合格 — 应力水平较低，安全裕量充足";
            builder.AppendLine(stressVerdict);

            // ===== 四、刚度校核 =====
            builder.AppendLine();
            builder.AppendLine(thinLine);
            builder.AppendLine("【四、刚度校核 Deflection Check】");
            builder.AppendLine();
            string deflectionLimit = SelectedSupport.Key == "cantilever" ? "L/150" : "L/250";
            builder.AppendLine($"  挠度控制标准:  {deflectionLimit} = {_result.AllowableDeflectionMm:F2} mm");
            builder.AppendLine($"  实际计算挠度:  δ = {_result.ActualDeflectionMm:F2} mm");
            double deflectionRatio = _result.AllowableDeflectionMm > 0 ? _result.ActualDeflectionMm / _result.AllowableDeflectionMm : 0;
            builder.AppendLine($"  挠度饱和度:    δ/δlim = {deflectionRatio * 100:F1}%");
            string deflectionVerdict;
            if (deflectionRatio >= 1.0)
                deflectionVerdict = "  ▶ 刚度判定:  ❌ 不合格 — 挠度超出许用值，结构变形过大";
            else if (deflectionRatio >= 0.8)
                deflectionVerdict = "  ▶ 刚度判定:  ⚠ 勉强 — 挠度接近限值，可能影响使用功能";
            else if (deflectionRatio >= 0.6)
                deflectionVerdict = "  ▶ 刚度判定:  △ 一般 — 挠度在允许范围内，但变形较明显";
            else
                deflectionVerdict = "  ▶ 刚度判定:  ✔ 合格 — 挠度较小，结构刚度良好";
            builder.AppendLine(deflectionVerdict);

            // ===== 五、工程综合判断 =====
            builder.AppendLine();
            builder.AppendLine(thinLine);
            builder.AppendLine("【五、工程综合判断 Engineering Judgment】");
            builder.AppendLine();
            builder.AppendLine($"  {_result.StatusText}");

            // ===== 六、结构可靠性评估 =====
            builder.AppendLine();
            builder.AppendLine(thinLine);
            builder.AppendLine("【六、结构可靠性评估 Structural Reliability Assessment】");
            builder.AppendLine();

            // 综合评估
            bool stressOk = _result.StressRatio < 1.0;
            bool deflectionOk = deflectionRatio < 1.0;
            bool stressGood = _result.StressRatio < 0.6;
            bool deflectionGood = deflectionRatio < 0.6;
            bool stressMarginal = _result.StressRatio >= 0.8;
            bool deflectionMarginal = deflectionRatio >= 0.8;

            string reliabilityGrade;
            string reliabilityConclusion;
            string[] reliabilityDetails;

            if (!stressOk || !deflectionOk)
            {
                reliabilityGrade = "不可靠 (NOT RELIABLE)";
                reliabilityConclusion = "该结构在当前工况下不满足强度或刚度要求，存在失效风险，不可投入使用。";
                reliabilityDetails = new[]
                {
                    !stressOk ? "弯曲应力已超过材料屈服强度，截面将发生塑性破坏。" : "挠度超出许用限值，结构变形不满足使用要求。",
                    "建议：更换更大截面规格、缩短跨度、降低载荷或改用更高强度材料。"
                };
            }
            else if (stressMarginal || deflectionMarginal)
            {
                reliabilityGrade = "可靠性不足 (MARGINAL)";
                reliabilityConclusion = "该结构虽未失效，但安全裕量偏小，在动载荷、冲击或长期疲劳工况下存在隐患。";
                reliabilityDetails = new[]
                {
                    stressMarginal ? $"应力饱和度达 {_result.StressRatio * 100:F0}%，安全储备不足。" : $"挠度饱和度达 {deflectionRatio * 100:F0}%，变形余量偏小。",
                    "建议：适当加大截面规格以提高安全裕量，或在设计中增加约束条件。"
                };
            }
            else if (stressGood && deflectionGood)
            {
                reliabilityGrade = "可靠 (RELIABLE)";
                reliabilityConclusion = "该结构在当前工况下强度与刚度均满足要求，安全裕量充足，可放心使用。";
                reliabilityDetails = new[]
                {
                    $"强度裕量: {(1.0 - _result.StressRatio) * 100:F0}%  |  刚度裕量: {(1.0 - deflectionRatio) * 100:F0}%",
                    "各项指标均处于安全区间，结构设计合理。"
                };
            }
            else
            {
                reliabilityGrade = "基本可靠 (ACCEPTABLE)";
                reliabilityConclusion = "该结构在当前工况下满足强度与刚度要求，但安全裕量一般，建议关注长期使用工况。";
                reliabilityDetails = new[]
                {
                    $"强度裕量: {(1.0 - _result.StressRatio) * 100:F0}%  |  刚度裕量: {(1.0 - deflectionRatio) * 100:F0}%",
                    "满足静力工况要求，若存在动载荷或疲劳工况，建议进一步校核。"
                };
            }

            builder.AppendLine($"  ★ 综合评级:  {reliabilityGrade}");
            builder.AppendLine();
            builder.AppendLine($"  {reliabilityConclusion}");
            builder.AppendLine();
            foreach (var detail in reliabilityDetails)
            {
                builder.AppendLine($"  - {detail}");
            }

            // 评估依据
            builder.AppendLine();
            builder.AppendLine("  评估依据:");
            builder.AppendLine($"    强度准则: σ ≤ fy/K, K=1.5 (安全系数)");
            builder.AppendLine($"    刚度准则: δ ≤ {deflectionLimit} (GB 50017 钢结构设计标准)");
            builder.AppendLine($"    材料弹性模量: E = 206,000 MPa (钢材)");

            // ===== 七、计算依据与标准 =====
            builder.AppendLine();
            builder.AppendLine(thinLine);
            builder.AppendLine("【七、计算依据与标准 Calculation Basis & Standards】");
            builder.AppendLine();
            builder.AppendLine("  引用标准:");
            builder.AppendLine("    GB 50017-2017  《钢结构设计标准》— 挠度限值、强度验算、安全系数取值依据");
            builder.AppendLine("    GB 50009-2012  《建筑结构荷载规范》— 荷载组合与分项系数");
            builder.AppendLine("    GB 50018-2002  《冷弯薄壁型钢结构技术规范》— 冷弯型材截面特性与设计规定");
            builder.AppendLine("    GB/T 11263-2017 《热轧H型钢和剖分T型钢》— H型钢截面规格与力学参数");
            builder.AppendLine("    GB/T 6728-2017  《结构用冷弯空心型钢》— 方管/矩管截面规格");
            builder.AppendLine("    GB/T 700-2006   《碳素结构钢》— Q235 等材料力学性能");
            builder.AppendLine("    GB/T 1591-2018  《低合金高强度结构钢》— Q345 等材料力学性能");
            builder.AppendLine();
            builder.AppendLine("  计算公式:");
            builder.AppendLine("    弯曲正应力: σ = M / W = P·a·b / (L·W)  (简支梁集中载荷)");
            builder.AppendLine($"    挠度计算:   δ = P·a²·b² / (3·E·I·L)  (简支梁集中载荷) — 许用值: {deflectionLimit}");
            builder.AppendLine($"    安全系数:   K = 1.5 (静载荷工况, 依据 GB 50017-2017 第 3.3 节)");
            builder.AppendLine($"    弹性模量:   E = 206,000 MPa (钢材标准值)");
            builder.AppendLine($"    屈服强度:   fy = {SelectedMaterial.Key:F0} MPa ({SelectedMaterial.Value})");

            // ===== 八、推荐截面 =====
            if (_result.Recommendations.Length > 0)
            {
                builder.AppendLine();
                builder.AppendLine(thinLine);
                builder.AppendLine("【八、推荐替代截面 Recommended Alternatives】");
                builder.AppendLine();
                builder.AppendLine("  以下截面满足当前工况的强度和刚度要求（按惯性矩从小到大排列）:");
                builder.AppendLine();
                for (int i = 0; i < _result.Recommendations.Length; i++)
                {
                    builder.AppendLine($"  {i + 1}. {_result.Recommendations[i].Name}");
                }
                builder.AppendLine();
                builder.AppendLine("  注: 双击列表中的推荐项可直接应用到当前计算。");
            }

            builder.AppendLine();
            builder.AppendLine(line);
            builder.AppendLine("  本报告由麦豆宝型材工具自动生成，仅供参考。");
            builder.AppendLine("  实际工程设计应结合现场工况、连接方式、加工工艺等因素综合判定。");
            builder.AppendLine(line);

            return builder.ToString();
        }

        private void CopyReportButton_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(BuildSectionReport());
                MessageBox.Show("型材结论已复制到剪贴板。", "已复制", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("复制失败: " + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SaveReportButton_Click(object sender, EventArgs e)
        {
            if (_result == null || _lastInput == null)
            {
                MessageBox.Show("请先进行计算，再导出报告。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "HTML报告 (*.html)|*.html|文本报告 (*.txt)|*.txt";
                dialog.FileName = "型材工具工程结论.html";
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    if (dialog.FileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        System.IO.File.WriteAllText(dialog.FileName, BuildSectionReport(), Encoding.UTF8);
                    }
                    else
                    {
                        ReportGenerator.GeneratePdf(dialog.FileName, _result, _lastInput,
                            BuildCurrentProfileDescriptor(), SelectedOrientation.Value,
                            SelectedSupport.Value, SelectedMaterial.Value);
                    }
                    MessageBox.Show("报告已导出。", "导出成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出失败: " + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
