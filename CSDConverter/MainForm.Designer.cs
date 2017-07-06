﻿namespace CsdMergeTool
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.srcPathLabel = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.srcPathInput = new System.Windows.Forms.TextBox();
            this.srcBrowserBtn = new System.Windows.Forms.Button();
            this.dstPathLabel = new System.Windows.Forms.Label();
            this.dstPathInput = new System.Windows.Forms.TextBox();
            this.dstBrowserBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // srcPathLabel
            // 
            this.srcPathLabel.AutoSize = true;
            this.srcPathLabel.Location = new System.Drawing.Point(13, 13);
            this.srcPathLabel.Name = "srcPathLabel";
            this.srcPathLabel.Size = new System.Drawing.Size(71, 12);
            this.srcPathLabel.TabIndex = 0;
            this.srcPathLabel.Text = "源Csd路径：";
            // 
            // srcPathInput
            // 
            this.srcPathInput.Location = new System.Drawing.Point(78, 10);
            this.srcPathInput.Name = "srcPathInput";
            this.srcPathInput.Size = new System.Drawing.Size(394, 21);
            this.srcPathInput.TabIndex = 2;
            this.srcPathInput.TextChanged += new System.EventHandler(this.srcPathInput_TextChanged);
            // 
            // srcBrowserBtn
            // 
            this.srcBrowserBtn.Location = new System.Drawing.Point(478, 8);
            this.srcBrowserBtn.Name = "srcBrowserBtn";
            this.srcBrowserBtn.Size = new System.Drawing.Size(19, 23);
            this.srcBrowserBtn.TabIndex = 3;
            this.srcBrowserBtn.Text = ">";
            this.srcBrowserBtn.UseVisualStyleBackColor = true;
            this.srcBrowserBtn.Click += new System.EventHandler(this.srcBrowserBtn_Click);
            // 
            // dstPathLabel
            // 
            this.dstPathLabel.AutoSize = true;
            this.dstPathLabel.Location = new System.Drawing.Point(13, 45);
            this.dstPathLabel.Name = "dstPathLabel";
            this.dstPathLabel.Size = new System.Drawing.Size(65, 12);
            this.dstPathLabel.TabIndex = 4;
            this.dstPathLabel.Text = "输出路径：";
            // 
            // dstPathInput
            // 
            this.dstPathInput.Location = new System.Drawing.Point(78, 42);
            this.dstPathInput.Name = "dstPathInput";
            this.dstPathInput.Size = new System.Drawing.Size(394, 21);
            this.dstPathInput.TabIndex = 5;
            this.dstPathInput.TextChanged += new System.EventHandler(this.dstPathInput_TextChanged);
            // 
            // dstBrowserBtn
            // 
            this.dstBrowserBtn.Location = new System.Drawing.Point(478, 40);
            this.dstBrowserBtn.Name = "dstBrowserBtn";
            this.dstBrowserBtn.Size = new System.Drawing.Size(19, 23);
            this.dstBrowserBtn.TabIndex = 6;
            this.dstBrowserBtn.Text = ">";
            this.dstBrowserBtn.UseVisualStyleBackColor = true;
            this.dstBrowserBtn.Click += new System.EventHandler(this.dstBrowserBtn_Click);
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(215, 212);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 7;
            this.startBtn.Text = "开始";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 269);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.dstBrowserBtn);
            this.Controls.Add(this.dstPathInput);
            this.Controls.Add(this.dstPathLabel);
            this.Controls.Add(this.srcBrowserBtn);
            this.Controls.Add(this.srcPathInput);
            this.Controls.Add(this.srcPathLabel);
            this.Name = "MainForm";
            this.Text = "Csd合并工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label srcPathLabel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox srcPathInput;
        private System.Windows.Forms.Button srcBrowserBtn;
        private System.Windows.Forms.Label dstPathLabel;
        private System.Windows.Forms.TextBox dstPathInput;
        private System.Windows.Forms.Button dstBrowserBtn;
        private System.Windows.Forms.Button startBtn;
    }
}

