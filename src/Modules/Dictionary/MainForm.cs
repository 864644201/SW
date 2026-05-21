using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Dictionary
{
    public partial class MainForm : Form
    {
        private List<DictEntry> allEntries = new List<DictEntry>();
        private string dataPath;

        public MainForm()
        {
            InitializeComponent();
            dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
            if (!Directory.Exists(dataPath))
            {
                // 开发时从源码目录读取
                string srcPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "..", "..", "..", "data"));
                if (Directory.Exists(srcPath))
                    dataPath = srcPath;
            }
            LoadDictionary();
        }

        private void LoadDictionary()
        {
            if (!Directory.Exists(dataPath))
            {
                lblStatus.Text = "数据目录不存在: " + dataPath;
                return;
            }

            allEntries.Clear();
            string[] letters = { "A","B","C","D","E","F","G","H","I","J","K","L","M",
                                 "N","O","P","Q","R","S","T","U","V","W","X","Y","Z" };

            foreach (string letter in letters)
            {
                string file = Path.Combine(dataPath, letter + ".txt");
                if (!File.Exists(file)) continue;

                foreach (string line in File.ReadLines(file, Encoding.UTF8))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    int sep = line.IndexOf("&&");
                    if (sep < 0) continue;

                    string en = line.Substring(0, sep).Trim();
                    string cn = line.Substring(sep + 2).Trim();
                    if (en.Length > 0)
                        allEntries.Add(new DictEntry { English = en, Chinese = cn });
                }
            }

            lblStatus.Text = string.Format("已加载 {0} 条词条", allEntries.Count);
        }

        private void OnSearch(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void OnSearchTextChanged(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void DoSearch()
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            if (keyword.Length == 0)
            {
                lstResults.Items.Clear();
                lblStatus.Text = string.Format("已加载 {0} 条词条", allEntries.Count);
                return;
            }

            lstResults.Items.Clear();
            int count = 0;
            int maxResults = 500;

            foreach (DictEntry entry in allEntries)
            {
                if (count >= maxResults) break;
                if (entry.English.ToLower().Contains(keyword) ||
                    entry.Chinese.Contains(keyword))
                {
                    lstResults.Items.Add(entry.English + "  →  " + entry.Chinese);
                    count++;
                }
            }

            lblStatus.Text = count >= maxResults
                ? string.Format("找到 {0}+ 条结果（已截断）", maxResults)
                : string.Format("找到 {0} 条结果", count);
        }

        private void OnSelectedChanged(object sender, EventArgs e)
        {
            if (lstResults.SelectedIndex >= 0)
            {
                string item = lstResults.SelectedItem.ToString();
                int arrow = item.IndexOf("→");
                if (arrow >= 0)
                {
                    txtDetail.Text = "英文: " + item.Substring(0, arrow).Trim() +
                        "\r\n中文: " + item.Substring(arrow + 1).Trim();
                }
            }
        }

        private class DictEntry
        {
            public string English;
            public string Chinese;
        }
    }
}
