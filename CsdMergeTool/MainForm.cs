﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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

            float defaultScale = 1.0f;
            scaleInput.Text = defaultScale.ToString("0.0");

            imagePathInput.Text = "animation";
        }

        // 选择源CSD路径
        private void srcBrowserBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "csd文件|*.csd";
            dialog.FileName = srcPathInput.Text;

            if (!String.IsNullOrEmpty(srcPathInput.Text))
                dialog.InitialDirectory = Path.GetDirectoryName(srcPathInput.Text);

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                srcPathInput.Text = Path.GetFullPath(dialog.FileName);
            }
        }

        // 选择输出路径
        private void dstBrowserBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "csd files (*.csd)|*.csd";
            dialog.FileName = dstPathInput.Text;

            if (!String.IsNullOrEmpty(dstPathInput.Text))
                dialog.InitialDirectory = Path.GetDirectoryName(dstPathInput.Text);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                dstPathInput.Text = Path.GetFullPath(dialog.FileName);
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

            if (String.IsNullOrEmpty(imagePathInput.Text))
            {
                MessageBox.Show("请设置图片路径");
                return;
            }

            float scale = 1.0f;
            try
            {
                scale = Convert.ToSingle(scaleInput.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("请输入合法的缩放值");
                return;
            }

            CsdMerge csdMerge = new CsdMerge(srcPathInput.Text, dstPathInput.Text, imagePathInput.Text, scale);
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
