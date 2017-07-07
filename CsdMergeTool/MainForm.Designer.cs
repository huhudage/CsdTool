namespace CsdMergeTool
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
            this.srcPathInput = new System.Windows.Forms.TextBox();
            this.srcBrowserBtn = new System.Windows.Forms.Button();
            this.dstPathLabel = new System.Windows.Forms.Label();
            this.dstPathInput = new System.Windows.Forms.TextBox();
            this.dstBrowserBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.scaleInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // srcPathLabel
            // 
            this.srcPathLabel.AutoSize = true;
            this.srcPathLabel.Location = new System.Drawing.Point(13, 13);
            this.srcPathLabel.Name = "srcPathLabel";
            this.srcPathLabel.Size = new System.Drawing.Size(65, 12);
            this.srcPathLabel.TabIndex = 0;
            this.srcPathLabel.Text = "csd 路径：";
            // 
            // srcPathInput
            // 
            this.srcPathInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.srcPathInput.Location = new System.Drawing.Point(78, 10);
            this.srcPathInput.Name = "srcPathInput";
            this.srcPathInput.Size = new System.Drawing.Size(386, 21);
            this.srcPathInput.TabIndex = 2;
            this.srcPathInput.TextChanged += new System.EventHandler(this.srcPathInput_TextChanged);
            // 
            // srcBrowserBtn
            // 
            this.srcBrowserBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.srcBrowserBtn.Location = new System.Drawing.Point(470, 8);
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
            this.dstPathInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dstPathInput.Location = new System.Drawing.Point(78, 42);
            this.dstPathInput.Name = "dstPathInput";
            this.dstPathInput.Size = new System.Drawing.Size(386, 21);
            this.dstPathInput.TabIndex = 5;
            this.dstPathInput.TextChanged += new System.EventHandler(this.dstPathInput_TextChanged);
            // 
            // dstBrowserBtn
            // 
            this.dstBrowserBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dstBrowserBtn.Location = new System.Drawing.Point(470, 40);
            this.dstBrowserBtn.Name = "dstBrowserBtn";
            this.dstBrowserBtn.Size = new System.Drawing.Size(19, 23);
            this.dstBrowserBtn.TabIndex = 6;
            this.dstBrowserBtn.Text = ">";
            this.dstBrowserBtn.UseVisualStyleBackColor = true;
            this.dstBrowserBtn.Click += new System.EventHandler(this.dstBrowserBtn_Click);
            // 
            // startBtn
            // 
            this.startBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startBtn.Location = new System.Drawing.Point(202, 144);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(99, 36);
            this.startBtn.TabIndex = 7;
            this.startBtn.Text = "合并";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "统一缩放：";
            // 
            // scaleInput
            // 
            this.scaleInput.Location = new System.Drawing.Point(78, 89);
            this.scaleInput.MaxLength = 5;
            this.scaleInput.Name = "scaleInput";
            this.scaleInput.Size = new System.Drawing.Size(81, 21);
            this.scaleInput.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 211);
            this.Controls.Add(this.scaleInput);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.TextBox srcPathInput;
        private System.Windows.Forms.Button srcBrowserBtn;
        private System.Windows.Forms.Label dstPathLabel;
        private System.Windows.Forms.TextBox dstPathInput;
        private System.Windows.Forms.Button dstBrowserBtn;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox scaleInput;
    }
}

