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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBoxHeadFrame = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeadFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxHeadFrame
            // 
            this.pictureBoxHeadFrame.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxHeadFrame.Image = global::CheckInbyFace.Properties.Resources.head_frame;
            this.pictureBoxHeadFrame.Location = new System.Drawing.Point(618, 106);
            this.pictureBoxHeadFrame.Name = "pictureBoxHeadFrame";
            this.pictureBoxHeadFrame.Size = new System.Drawing.Size(683, 683);
            this.pictureBoxHeadFrame.TabIndex = 0;
            this.pictureBoxHeadFrame.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CheckInbyFace.Properties.Resources.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1894, 1009);
            this.Controls.Add(this.pictureBoxHeadFrame);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "CheckInByFace";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeadFrame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxHeadFrame;
    }
}

