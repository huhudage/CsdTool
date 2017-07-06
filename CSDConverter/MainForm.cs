using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsdMergeTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // 应用本地设置
            applyLocalConfig();
        }

        // 应用本地设置
        private void applyLocalConfig()
        {
            srcPathInput.Text = LocalConfig.GetInstance().Get<String>("srcPath");
            dstPathInput.Text = LocalConfig.GetInstance().Get<String>("dstPath");
        }

        // 选择源CSD路径
        private void srcBrowserBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            if (dialog.ShowDialog() == DialogResult.OK || dialog.ShowDialog() == DialogResult.Yes)
            {
                srcPathInput.Text = dialog.SelectedPath;
            }
        }

        // 选择输出路径
        private void dstBrowserBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            if (dialog.ShowDialog() == DialogResult.OK || dialog.ShowDialog() == DialogResult.Yes)
            {
                dstPathInput.Text = dialog.SelectedPath;
            }
        }

        // 点击开始按钮
        private void startBtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(srcPathInput.Text))
            {
                MessageBox.Show("请选择需要源CSD路径");
                return;
            }

            if (String.IsNullOrEmpty(dstPathInput.Text))
            {
                MessageBox.Show("请选择输出路径");
                return;
            }

            CsdMerge csdMerge = new CsdMerge(srcPathInput.Text, dstPathInput.Text);
            csdMerge.Merge();
        }

        private void srcPathInput_TextChanged(object sender, EventArgs e)
        {
            LocalConfig.GetInstance().Set("srcPath", srcPathInput.Text);
        }

        private void dstPathInput_TextChanged(object sender, EventArgs e)
        {
            LocalConfig.GetInstance().Set("dstPath", dstPathInput.Text);
        }
    }
}
