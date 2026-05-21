namespace Dictionary
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

            // 搜索区域
            var lblSearch = new System.Windows.Forms.Label();
            lblSearch.Text = "搜索:";
            lblSearch.Location = new System.Drawing.Point(10, 15);
            lblSearch.AutoSize = true;
            this.Controls.Add(lblSearch);

            this.txtSearch = new System.Windows.Forms.TextBox();
            txtSearch.Location = new System.Drawing.Point(50, 12);
            txtSearch.Size = new System.Drawing.Size(400, 25);
            txtSearch.Font = new System.Drawing.Font("Microsoft YaHei", 11F);
            txtSearch.TextChanged += new System.EventHandler(this.OnSearchTextChanged);
            this.Controls.Add(txtSearch);

            this.btnSearch = new System.Windows.Forms.Button();
            btnSearch.Text = "搜索";
            btnSearch.Location = new System.Drawing.Point(460, 11);
            btnSearch.Size = new System.Drawing.Size(70, 27);
            btnSearch.Click += new System.EventHandler(this.OnSearch);
            this.Controls.Add(btnSearch);

            // 结果列表
            this.lstResults = new System.Windows.Forms.ListBox();
            lstResults.Location = new System.Drawing.Point(10, 48);
            lstResults.Size = new System.Drawing.Size(520, 400);
            lstResults.Font = new System.Drawing.Font("Consolas", 10F);
            lstResults.SelectedIndexChanged += new System.EventHandler(this.OnSelectedChanged);
            this.Controls.Add(lstResults);

            // 详情区域
            this.txtDetail = new System.Windows.Forms.TextBox();
            txtDetail.Location = new System.Drawing.Point(10, 455);
            txtDetail.Size = new System.Drawing.Size(520, 60);
            txtDetail.Multiline = true;
            txtDetail.ReadOnly = true;
            txtDetail.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.Controls.Add(txtDetail);

            // 状态栏
            this.lblStatus = new System.Windows.Forms.Label();
            lblStatus.Text = "正在加载...";
            lblStatus.Location = new System.Drawing.Point(10, 520);
            lblStatus.Size = new System.Drawing.Size(520, 20);
            lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.Controls.Add(lblStatus);

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 545);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.MinimumSize = new System.Drawing.Size(500, 450);
            this.Text = "机械设计字典 - 英汉术语查询";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.Label lblStatus;
    }
}
