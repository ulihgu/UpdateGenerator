namespace UpdateGenerator
{
    partial class UpdateGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateGenerator));
            this.progressBar1 = new MyProgressBar();
            this._ButPath = new System.Windows.Forms.Button();
            this._Path = new System.Windows.Forms.TextBox();
            this._generatedFile = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1, 273);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(550, 23);
            this.progressBar1.TabIndex = 27;
            // 
            // _ButPath
            // 
            this._ButPath.BackColor = System.Drawing.Color.SteelBlue;
            this._ButPath.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._ButPath.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._ButPath.ForeColor = System.Drawing.Color.Transparent;
            this._ButPath.Location = new System.Drawing.Point(333, 4);
            this._ButPath.Name = "_ButPath";
            this._ButPath.Size = new System.Drawing.Size(103, 23);
            this._ButPath.TabIndex = 12;
            this._ButPath.Text = "选择程序目录";
            this._ButPath.UseVisualStyleBackColor = false;
            this._ButPath.Click += new System.EventHandler(this._ButPath_Click);
            // 
            // _Path
            // 
            this._Path.Location = new System.Drawing.Point(2, 3);
            this._Path.Name = "_Path";
            this._Path.Size = new System.Drawing.Size(325, 21);
            this._Path.TabIndex = 24;
            // 
            // _generatedFile
            // 
            this._generatedFile.BackColor = System.Drawing.Color.SteelBlue;
            this._generatedFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._generatedFile.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._generatedFile.ForeColor = System.Drawing.Color.Transparent;
            this._generatedFile.Location = new System.Drawing.Point(440, 4);
            this._generatedFile.Name = "_generatedFile";
            this._generatedFile.Size = new System.Drawing.Size(109, 23);
            this._generatedFile.TabIndex = 25;
            this._generatedFile.Text = "生成配置文件";
            this._generatedFile.UseVisualStyleBackColor = false;
            this._generatedFile.Click += new System.EventHandler(this._generatedFile_Click);
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.BackColor = System.Drawing.SystemColors.Control;
            this.tbLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLog.Location = new System.Drawing.Point(2, 30);
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.Size = new System.Drawing.Size(550, 239);
            this.tbLog.TabIndex = 26;
            this.tbLog.Text = "";
            // 
            // UpdateGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(552, 297);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this._generatedFile);
            this.Controls.Add(this._Path);
            this.Controls.Add(this._ButPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UpdateGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "更新配置文件生成器v2.0";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _ButPath;
        private System.Windows.Forms.TextBox _Path;
        private System.Windows.Forms.Button _generatedFile;
        private System.Windows.Forms.RichTextBox tbLog;
        //private System.Windows.Forms.ProgressBar progressBar1;
        private MyProgressBar progressBar1;
    }
}

