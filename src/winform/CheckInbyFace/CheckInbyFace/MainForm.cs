using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckInbyFace.Configs;
using System.IO;

namespace CheckInbyFace
{
    public partial class MainForm : Form
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public MainForm()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            Helpers.UIHelper.SwitchFullScreen(this);
            TextBoxUserNameVisable(false);
            RefreshValues2UI();
            timerForFaceDetect.Start();
        }

        private CheckIn.CheckInManager _checkInManager = new CheckIn.CheckInManager();
        private CheckIn.FaceDetector _faceDetector = new CheckIn.FaceDetector();

        private void RefreshValues2UI()
        {
            string checkInCount = string.Format("{0}/{1}", _checkInManager.CheckInCount, _checkInManager.TotalCount);
            string resultNo = _checkInManager.CheckInByAdminPercent.ToString("0.00") + "%";
            string resultYes = _checkInManager.CheckInByAIPercent.ToString("0.00") + "%";

            labelResultNo.Text = resultNo;
            labelResultYes.Text = resultYes;
            labelCheckinCount.Text = checkInCount;
        }

        private void RefreshFace2UI()
        {
            var fdis = _faceDetector.Read();
            if (fdis != null 
                && fdis.Faces != null && fdis.Faces.Count > 0)
            {
                var face = fdis.Faces[0];
                string userId = face.UserId;
                if (!string.IsNullOrEmpty(userId))
                {
                    var user = face.RestoreUser(_checkInManager.UserCheckInList);
                    if (user != null)
                    {
                        labelUserName.Text = string.Format("{0}({1})", user.UserName, user.UserId);
                        ResizeLabelUserName();

                        if (System.IO.File.Exists(face.ImagePath))
                        {
                            pictureBoxHeadFrame.Image = Image.FromFile(face.ImagePath);
                        }
                    }
                }
            }
        }

        private void TextBoxUserNameVisable(bool visable)
        {
            this.textBoxUserName.Visible = visable;
            this.pictureBoxButtonYes2.Visible = visable;

            this.labelUserName.Visible = !visable;
        }

        private const int SCREEN_WIDTH = 1920;
        private const int SCREEN_HEIGHT = 1080;

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Size delta = this.pictureBoxBackground.ClientSize - this.pictureBoxHeadFrame.ClientSize;
            this.pictureBoxHeadFrame.Left = Math.Abs(delta.Width / 2);
            this.pictureBoxHeadFrame.Top = Math.Abs((int)(delta.Height * 0.7 / 2));
            
            this.pictureBoxResultNo.Top = this.pictureBoxHeadFrame.Top + 60;
            this.pictureBoxResultNo.Left = (this.pictureBoxBackground.Width - this.pictureBoxHeadFrame.Width) / 4 - this.pictureBoxResultNo.Width / 2;
            this.pictureBoxResultYes.Top = this.pictureBoxHeadFrame.Top + 60;
            this.pictureBoxResultYes.Left = this.pictureBoxBackground.Width / 4 * 3 + this.pictureBoxHeadFrame.Width / 4 - this.pictureBoxResultYes.Width / 2;

            ResizeLabelResult();

            this.pictureBoxButtonNo.Top = this.pictureBoxHeadFrame.Bottom;
            this.pictureBoxButtonNo.Left = this.pictureBoxHeadFrame.Left - this.pictureBoxButtonNo.ClientSize.Width * 2;
            this.pictureBoxButtonYes.Top = this.pictureBoxHeadFrame.Bottom;
            this.pictureBoxButtonYes.Left = this.pictureBoxHeadFrame.Right + this.pictureBoxButtonYes.ClientSize.Width;

            this.textBoxUserName.Top = Math.Max(this.pictureBoxHeadFrame.Bottom,
                this.pictureBoxButtonNo.Top + (this.pictureBoxButtonNo.ClientSize.Height - this.textBoxUserName.Height) / 2);
            this.textBoxUserName.Left = this.pictureBoxHeadFrame.Left + this.pictureBoxHeadFrame.ClientSize.Width / 2 - this.textBoxUserName.ClientSize.Width / 2;

            this.pictureBoxButtonYes2.Width = this.textBoxUserName.Height - 12;
            this.pictureBoxButtonYes2.Height = this.textBoxUserName.Height - 12;
            this.pictureBoxButtonYes2.Left = this.textBoxUserName.Right - this.pictureBoxButtonYes2.Width - 6;
            this.pictureBoxButtonYes2.Top = this.textBoxUserName.Top + 6;

            ResizeLabelUserName();
            ResizeLabelCheckInCount();
        }

        private void ResizeLabelCheckInCount()
        {
            this.labelCheckinCount.Top = Math.Max(this.pictureBoxHeadFrame.Bottom, this.pictureBoxButtonNo.Bottom);
            this.labelCheckinCount.Left = this.pictureBoxHeadFrame.Left + this.pictureBoxHeadFrame.ClientSize.Width / 2 - this.labelCheckinCount.ClientSize.Width / 2;
        }

        private void ResizeLabelUserName()
        {
            this.labelUserName.Top = Math.Max(this.pictureBoxHeadFrame.Bottom,
                this.pictureBoxButtonNo.Top + (this.pictureBoxButtonNo.ClientSize.Height - this.labelUserName.Height) / 2);
            this.labelUserName.Left = this.pictureBoxHeadFrame.Left + this.pictureBoxHeadFrame.ClientSize.Width / 2 - this.labelUserName.ClientSize.Width / 2;
        }

        private void ResizeLabelResult()
        {
            this.labelResultNo.Top = this.pictureBoxResultNo.Bottom + this.labelResultNo.ClientSize.Height;
            this.labelResultNo.Left = this.pictureBoxResultNo.Left + (this.pictureBoxResultNo.ClientSize.Width - this.labelResultNo.ClientSize.Width) / 2;
            this.labelResultYes.Top = this.pictureBoxResultYes.Bottom + this.labelResultYes.ClientSize.Height;
            this.labelResultYes.Left = this.pictureBoxResultYes.Left + (this.pictureBoxResultYes.ClientSize.Width - this.labelResultYes.ClientSize.Width) / 2;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11 || e.KeyCode == Keys.Escape)
            {
                Helpers.UIHelper.SwitchFullScreen(this);
            }
        }

        private void pictureBoxBackground_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Helpers.UIHelper.SwitchFullScreen(this);
        }

        private void pictureBoxHeadFrame_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Helpers.UIHelper.SwitchFullScreen(this);
        }

        private void pictureBoxButtonNo_Click(object sender, EventArgs e)
        {
            TextBoxUserNameVisable(true);
        }

        private void pictureBoxButtonYes2_Click(object sender, EventArgs e)
        {
            TextBoxUserNameVisable(false);
            string userNameInput = this.textBoxUserName.Text;
            this.textBoxUserName.Text = string.Empty;

            this.labelUserName.Text = userNameInput;
            ResizeLabelUserName();
        }

        private void pictureBoxButtonYes_Click(object sender, EventArgs e)
        {

        }

        private void timerForFaceDetect_Tick(object sender, EventArgs e)
        {
            RefreshFace2UI();
        }
    }
}
