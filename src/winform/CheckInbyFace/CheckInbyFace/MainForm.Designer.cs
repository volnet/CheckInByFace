namespace CheckInbyFace
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBoxBackground = new System.Windows.Forms.PictureBox();
            this.pictureBoxHeadFrame = new System.Windows.Forms.PictureBox();
            this.pictureBoxButtonYes = new System.Windows.Forms.PictureBox();
            this.pictureBoxButtonNo = new System.Windows.Forms.PictureBox();
            this.pictureBoxResultNo = new System.Windows.Forms.PictureBox();
            this.pictureBoxResultYes = new System.Windows.Forms.PictureBox();
            this.labelResultNo = new System.Windows.Forms.Label();
            this.labelResultYes = new System.Windows.Forms.Label();
            this.textBoxUserIdOrUserName = new System.Windows.Forms.TextBox();
            this.labelCheckinCount = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.pictureBoxButtonYes2 = new System.Windows.Forms.PictureBox();
            this.timerForFaceDetect = new System.Windows.Forms.Timer(this.components);
            this.labelResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeadFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonYes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResultNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResultYes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonYes2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxBackground
            // 
            this.pictureBoxBackground.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxBackground.Image = global::CheckInbyFace.Properties.Resources.background;
            this.pictureBoxBackground.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxBackground.Name = "pictureBoxBackground";
            this.pictureBoxBackground.Size = new System.Drawing.Size(1894, 1009);
            this.pictureBoxBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBackground.TabIndex = 0;
            this.pictureBoxBackground.TabStop = false;
            this.pictureBoxBackground.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxBackground_MouseDoubleClick);
            // 
            // pictureBoxHeadFrame
            // 
            this.pictureBoxHeadFrame.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxHeadFrame.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxHeadFrame.Image = global::CheckInbyFace.Properties.Resources.head_frame;
            this.pictureBoxHeadFrame.Location = new System.Drawing.Point(581, 80);
            this.pictureBoxHeadFrame.Name = "pictureBoxHeadFrame";
            this.pictureBoxHeadFrame.Size = new System.Drawing.Size(700, 700);
            this.pictureBoxHeadFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHeadFrame.TabIndex = 1;
            this.pictureBoxHeadFrame.TabStop = false;
            this.pictureBoxHeadFrame.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxHeadFrame_MouseDoubleClick);
            // 
            // pictureBoxButtonYes
            // 
            this.pictureBoxButtonYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxButtonYes.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxButtonYes.Image = global::CheckInbyFace.Properties.Resources.button_yes;
            this.pictureBoxButtonYes.Location = new System.Drawing.Point(1391, 672);
            this.pictureBoxButtonYes.Name = "pictureBoxButtonYes";
            this.pictureBoxButtonYes.Size = new System.Drawing.Size(208, 208);
            this.pictureBoxButtonYes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxButtonYes.TabIndex = 2;
            this.pictureBoxButtonYes.TabStop = false;
            this.pictureBoxButtonYes.Click += new System.EventHandler(this.pictureBoxButtonYes_Click);
            // 
            // pictureBoxButtonNo
            // 
            this.pictureBoxButtonNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxButtonNo.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxButtonNo.Image = global::CheckInbyFace.Properties.Resources.button_no;
            this.pictureBoxButtonNo.Location = new System.Drawing.Point(274, 672);
            this.pictureBoxButtonNo.Name = "pictureBoxButtonNo";
            this.pictureBoxButtonNo.Size = new System.Drawing.Size(208, 208);
            this.pictureBoxButtonNo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxButtonNo.TabIndex = 3;
            this.pictureBoxButtonNo.TabStop = false;
            this.pictureBoxButtonNo.Click += new System.EventHandler(this.pictureBoxButtonNo_Click);
            // 
            // pictureBoxResultNo
            // 
            this.pictureBoxResultNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxResultNo.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxResultNo.Image = global::CheckInbyFace.Properties.Resources.result_no;
            this.pictureBoxResultNo.Location = new System.Drawing.Point(205, 178);
            this.pictureBoxResultNo.Name = "pictureBoxResultNo";
            this.pictureBoxResultNo.Size = new System.Drawing.Size(150, 78);
            this.pictureBoxResultNo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxResultNo.TabIndex = 4;
            this.pictureBoxResultNo.TabStop = false;
            // 
            // pictureBoxResultYes
            // 
            this.pictureBoxResultYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxResultYes.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxResultYes.Image = global::CheckInbyFace.Properties.Resources.result_yes;
            this.pictureBoxResultYes.Location = new System.Drawing.Point(1490, 178);
            this.pictureBoxResultYes.Name = "pictureBoxResultYes";
            this.pictureBoxResultYes.Size = new System.Drawing.Size(150, 78);
            this.pictureBoxResultYes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxResultYes.TabIndex = 5;
            this.pictureBoxResultYes.TabStop = false;
            // 
            // labelResultNo
            // 
            this.labelResultNo.AutoSize = true;
            this.labelResultNo.BackColor = System.Drawing.Color.Transparent;
            this.labelResultNo.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelResultNo.ForeColor = System.Drawing.Color.White;
            this.labelResultNo.Location = new System.Drawing.Point(184, 284);
            this.labelResultNo.Name = "labelResultNo";
            this.labelResultNo.Size = new System.Drawing.Size(193, 124);
            this.labelResultNo.TabIndex = 6;
            this.labelResultNo.Text = "0%";
            // 
            // labelResultYes
            // 
            this.labelResultYes.AutoSize = true;
            this.labelResultYes.BackColor = System.Drawing.Color.Transparent;
            this.labelResultYes.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelResultYes.ForeColor = System.Drawing.Color.White;
            this.labelResultYes.Location = new System.Drawing.Point(1469, 294);
            this.labelResultYes.Name = "labelResultYes";
            this.labelResultYes.Size = new System.Drawing.Size(193, 124);
            this.labelResultYes.TabIndex = 7;
            this.labelResultYes.Text = "0%";
            // 
            // textBoxUserName
            // 
            this.textBoxUserIdOrUserName.Font = new System.Drawing.Font("微软雅黑", 22.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxUserIdOrUserName.Location = new System.Drawing.Point(695, 826);
            this.textBoxUserIdOrUserName.Name = "textBoxUserName";
            this.textBoxUserIdOrUserName.Size = new System.Drawing.Size(474, 85);
            this.textBoxUserIdOrUserName.TabIndex = 8;
            // 
            // labelCheckinCount
            // 
            this.labelCheckinCount.AutoSize = true;
            this.labelCheckinCount.BackColor = System.Drawing.Color.Transparent;
            this.labelCheckinCount.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCheckinCount.ForeColor = System.Drawing.Color.White;
            this.labelCheckinCount.Location = new System.Drawing.Point(828, 917);
            this.labelCheckinCount.Name = "labelCheckinCount";
            this.labelCheckinCount.Size = new System.Drawing.Size(214, 83);
            this.labelCheckinCount.TabIndex = 9;
            this.labelCheckinCount.Text = "27/78";
            // 
            // labelUserName
            // 
            this.labelUserName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelUserName.AutoSize = true;
            this.labelUserName.BackColor = System.Drawing.Color.Transparent;
            this.labelUserName.Font = new System.Drawing.Font("微软雅黑", 32.125F, System.Drawing.FontStyle.Bold);
            this.labelUserName.ForeColor = System.Drawing.Color.White;
            this.labelUserName.Location = new System.Drawing.Point(828, 827);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(220, 114);
            this.labelUserName.TabIndex = 10;
            this.labelUserName.Text = "李晨";
            // 
            // pictureBoxButtonYes2
            // 
            this.pictureBoxButtonYes2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxButtonYes2.BackColor = System.Drawing.Color.White;
            this.pictureBoxButtonYes2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxButtonYes2.Image")));
            this.pictureBoxButtonYes2.Location = new System.Drawing.Point(1098, 839);
            this.pictureBoxButtonYes2.Name = "pictureBoxButtonYes2";
            this.pictureBoxButtonYes2.Size = new System.Drawing.Size(60, 60);
            this.pictureBoxButtonYes2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxButtonYes2.TabIndex = 11;
            this.pictureBoxButtonYes2.TabStop = false;
            this.pictureBoxButtonYes2.Click += new System.EventHandler(this.pictureBoxButtonYes2_Click);
            // 
            // timerForFaceDetect
            // 
            this.timerForFaceDetect.Interval = 3000;
            this.timerForFaceDetect.Tick += new System.EventHandler(this.timerForFaceDetect_Tick);
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.BackColor = System.Drawing.Color.Transparent;
            this.labelResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelResult.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelResult.ForeColor = System.Drawing.Color.White;
            this.labelResult.Location = new System.Drawing.Point(0, 967);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(110, 42);
            this.labelResult.TabIndex = 12;
            this.labelResult.Text = "result";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CheckInbyFace.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1894, 1009);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.pictureBoxButtonYes2);
            this.Controls.Add(this.labelUserName);
            this.Controls.Add(this.labelCheckinCount);
            this.Controls.Add(this.textBoxUserIdOrUserName);
            this.Controls.Add(this.labelResultYes);
            this.Controls.Add(this.labelResultNo);
            this.Controls.Add(this.pictureBoxResultYes);
            this.Controls.Add(this.pictureBoxResultNo);
            this.Controls.Add(this.pictureBoxButtonNo);
            this.Controls.Add(this.pictureBoxButtonYes);
            this.Controls.Add(this.pictureBoxHeadFrame);
            this.Controls.Add(this.pictureBoxBackground);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "CheckInByFace";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeadFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonYes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResultNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResultYes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonYes2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBackground;
        private System.Windows.Forms.PictureBox pictureBoxHeadFrame;
        private System.Windows.Forms.PictureBox pictureBoxButtonYes;
        private System.Windows.Forms.PictureBox pictureBoxButtonNo;
        private System.Windows.Forms.PictureBox pictureBoxResultNo;
        private System.Windows.Forms.PictureBox pictureBoxResultYes;
        private System.Windows.Forms.Label labelResultNo;
        private System.Windows.Forms.Label labelResultYes;
        private System.Windows.Forms.TextBox textBoxUserIdOrUserName;
        private System.Windows.Forms.Label labelCheckinCount;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.PictureBox pictureBoxButtonYes2;
        private System.Windows.Forms.Timer timerForFaceDetect;
        private System.Windows.Forms.Label labelResult;
    }
}

