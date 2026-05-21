namespace ConnDesign
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // 左面板：TreeView导航
            var panelLeft = new System.Windows.Forms.Panel();
            panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            panelLeft.Width = 180;
            panelLeft.Padding = new System.Windows.Forms.Padding(5);

            this.treeNav = new System.Windows.Forms.TreeView();
            treeNav.Dock = System.Windows.Forms.DockStyle.Fill;
            treeNav.Font = new System.Drawing.Font("Microsoft YaHei", 9.5F);

            // 键连接节点
            var nodeKeys = new System.Windows.Forms.TreeNode("键连接设计");
            nodeKeys.Nodes.Add("平键静连接");
            nodeKeys.Nodes.Add("平键动连接");
            nodeKeys.Nodes.Add("半圆键连接");
            nodeKeys.Nodes.Add("楔键连接");
            nodeKeys.Nodes.Add("切向键连接");
            nodeKeys.Nodes.Add("矩形花键连接");
            nodeKeys.Nodes.Add("渐开线花键连接");
            treeNav.Nodes.Add(nodeKeys);

            // 螺栓连接节点
            var nodeBolts = new System.Windows.Forms.TreeNode("螺栓连接设计");
            nodeBolts.Nodes.Add("松螺栓连接");
            nodeBolts.Nodes.Add("铰制孔螺栓连接");
            nodeBolts.Nodes.Add("紧螺栓连接(横向)");
            nodeBolts.Nodes.Add("紧连接(静载荷)");
            nodeBolts.Nodes.Add("紧连接(动载荷)");
            treeNav.Nodes.Add(nodeBolts);

            nodeKeys.Expand();
            nodeBolts.Expand();
            treeNav.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnNodeSelected);
            panelLeft.Controls.Add(treeNav);

            // 右面板：输入+结果
            var panelRight = new System.Windows.Forms.Panel();
            panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            panelRight.Padding = new System.Windows.Forms.Padding(10);
            panelRight.AutoScroll = true;

            // 标题
            this.lblTitle = new System.Windows.Forms.Label();
            lblTitle.Text = "连接设计";
            lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(10, 5);
            lblTitle.AutoSize = true;
            panelRight.Controls.Add(lblTitle);
            int y = 35;

            // 转矩 T
            var lblT = new System.Windows.Forms.Label();
            lblT.Text = "转矩 T (N·mm):";
            lblT.Location = new System.Drawing.Point(10, y); lblT.AutoSize = true;
            panelRight.Controls.Add(lblT); y += 22;
            this.txtTorque = new System.Windows.Forms.TextBox();
            txtTorque.Location = new System.Drawing.Point(10, y); txtTorque.Size = new System.Drawing.Size(150, 23); txtTorque.Text = "100000";
            panelRight.Controls.Add(txtTorque); y += 30;

            // 轴径 d
            var lblD = new System.Windows.Forms.Label();
            lblD.Text = "轴径 d (mm):";
            lblD.Location = new System.Drawing.Point(10, y); lblD.AutoSize = true;
            panelRight.Controls.Add(lblD); y += 22;
            this.txtShaftDia = new System.Windows.Forms.TextBox();
            txtShaftDia.Location = new System.Drawing.Point(10, y); txtShaftDia.Size = new System.Drawing.Size(150, 23); txtShaftDia.Text = "40";
            panelRight.Controls.Add(txtShaftDia); y += 30;

            // 键长度 L
            var lblL = new System.Windows.Forms.Label();
            lblL.Text = "键长度 L (mm):";
            lblL.Location = new System.Drawing.Point(10, y); lblL.AutoSize = true;
            panelRight.Controls.Add(lblL); y += 22;
            this.txtKeyLength = new System.Windows.Forms.TextBox();
            txtKeyLength.Location = new System.Drawing.Point(10, y); txtKeyLength.Size = new System.Drawing.Size(150, 23); txtKeyLength.Text = "50";
            panelRight.Controls.Add(txtKeyLength); y += 30;

            // 材料
            var lblMat = new System.Windows.Forms.Label();
            lblMat.Text = "轮毂材料:";
            lblMat.Location = new System.Drawing.Point(10, y); lblMat.AutoSize = true;
            panelRight.Controls.Add(lblMat); y += 22;
            this.cmbMaterial = new System.Windows.Forms.ComboBox();
            cmbMaterial.Location = new System.Drawing.Point(10, y); cmbMaterial.Size = new System.Drawing.Size(150, 23);
            cmbMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbMaterial.Items.AddRange(new object[] { "钢", "铸铁" });
            cmbMaterial.SelectedIndex = 0;
            panelRight.Controls.Add(cmbMaterial); y += 30;

            // 载荷类型
            var lblLoad = new System.Windows.Forms.Label();
            lblLoad.Text = "载荷类型:";
            lblLoad.Location = new System.Drawing.Point(10, y); lblLoad.AutoSize = true;
            panelRight.Controls.Add(lblLoad); y += 22;
            this.cmbLoadType = new System.Windows.Forms.ComboBox();
            cmbLoadType.Location = new System.Drawing.Point(10, y); cmbLoadType.Size = new System.Drawing.Size(150, 23);
            cmbLoadType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbLoadType.Items.AddRange(new object[] { "静载荷", "轻微冲击", "冲击" });
            cmbLoadType.SelectedIndex = 0;
            panelRight.Controls.Add(cmbLoadType); y += 30;

            // 键类型（平键A/B/C型）
            var lblKeyType = new System.Windows.Forms.Label();
            lblKeyType.Text = "键类型:";
            lblKeyType.Location = new System.Drawing.Point(10, y); lblKeyType.AutoSize = true;
            panelRight.Controls.Add(lblKeyType); y += 22;
            this.cmbKeyType = new System.Windows.Forms.ComboBox();
            cmbKeyType.Location = new System.Drawing.Point(10, y); cmbKeyType.Size = new System.Drawing.Size(150, 23);
            cmbKeyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbKeyType.Items.AddRange(new object[] { "A型(圆头)", "B型(方头)", "C型(单圆头)" });
            cmbKeyType.SelectedIndex = 0;
            panelRight.Controls.Add(cmbKeyType); y += 35;

            // 查询按钮
            this.btnCalc = new System.Windows.Forms.Button();
            btnCalc.Text = "计 算";
            btnCalc.Location = new System.Drawing.Point(10, y);
            btnCalc.Size = new System.Drawing.Size(100, 35);
            btnCalc.Click += new System.EventHandler(this.OnCalculate);
            panelRight.Controls.Add(btnCalc);
            y += 45;

            // 截面尺寸显示
            this.lblSection = new System.Windows.Forms.Label();
            lblSection.Text = "";
            lblSection.Location = new System.Drawing.Point(10, y);
            lblSection.Size = new System.Drawing.Size(450, 20);
            panelRight.Controls.Add(lblSection);
            y += 22;

            // 结果
            this.txtResult = new System.Windows.Forms.TextBox();
            txtResult.Location = new System.Drawing.Point(10, y);
            txtResult.Size = new System.Drawing.Size(450, 250);
            txtResult.Multiline = true;
            txtResult.ReadOnly = true;
            txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            txtResult.Font = new System.Drawing.Font("Consolas", 10F);
            txtResult.WordWrap = false;
            panelRight.Controls.Add(txtResult);

            // 组装
            this.Controls.Add(panelRight);
            this.Controls.Add(panelLeft);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 520);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Text = "连接设计";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TreeView treeNav;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTorque;
        private System.Windows.Forms.TextBox txtShaftDia;
        private System.Windows.Forms.TextBox txtKeyLength;
        private System.Windows.Forms.ComboBox cmbMaterial;
        private System.Windows.Forms.ComboBox cmbLoadType;
        private System.Windows.Forms.ComboBox cmbKeyType;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.TextBox txtResult;
    }
}
