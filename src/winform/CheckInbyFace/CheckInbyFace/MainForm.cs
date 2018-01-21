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
            TextBoxUserIdOrUserNameVisable(false);
            LabelResultYesNoVisable(false);
            RefreshValues2UI();
            RefreshFace2UI();
            ResetLabelResult();
            timerForFaceDetect.Start();
        }

        private CheckIn.CheckInManager _checkInManager = new CheckIn.CheckInManager();
        private CheckIn.FaceDetector _faceDetector = new CheckIn.FaceDetector();
        private DateTime labelResultShowDateTime = DateTime.MinValue;

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
                            pictureBoxHeadFrame.Image = GetFaceImage(face);
                        }
                    }
                }
            }
        }

        private Image GetFaceImage(Objects.FaceDetectInfos.FaceDetectInfo face)
        {
            if (face != null && System.IO.File.Exists(face.ImagePath))
            {
                //string userId = face.UserId;
                //string saveFramesFolderFullPath = ConfigManager.SaveFramesFolderFullPath;
                //string saveFrameRawFullPath = System.IO.Path.Combine(saveFramesFolderFullPath, userId + @"-raw.jpg");
                //string saveFrameCutFullPath = System.IO.Path.Combine(saveFramesFolderFullPath, userId + @"-cut.jpg");
                string image1Path = AppDomain.CurrentDomain.BaseDirectory + @"UI\MainForm\head-frame.png";

                Image image1 = Image.FromFile(image1Path);
                Image image2 = Image.FromFile(face.ImagePath);
                
                Image result = Helpers.GDIHelper.OverlayImageWithCutEllipse(image1, image2, new Rectangle(face.X, face.Y, face.Width, face.Height),
                    1.0F, new Point(image1.Width / 7, image1.Height / 7), new Size(image1.Width / 7 * 5, image1.Height / 7 * 5),
                    new Size(this.pictureBoxHeadFrame.Width, this.pictureBoxHeadFrame.Height));

                return result;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="status">0:success, 1:error, 2:normal, 3:warning</param>
        private void SetLabelResult(string text, int status)
        {
            if (status == 0)
            {
                labelResult.ForeColor = Color.Green;
            }
            else if (status == 1)
            {
                labelResult.ForeColor = Color.Red;
            }
            else if (status == 2)
            {
                labelResult.ForeColor = Color.White;
            }
            else if (status == 3)
            {
                labelResult.ForeColor = Color.Orange;
            }
            labelResultShowDateTime = DateTime.Now;
            labelResult.Text = text;
        }

        private void ResetLabelResult()
        {
            labelResult.Text = string.Empty;
        }

        private void TextBoxUserIdOrUserNameVisable(bool visable)
        {
            this.textBoxUserIdOrUserName.Visible = visable;
            this.pictureBoxButtonYes2.Visible = visable;

            this.labelUserName.Visible = !visable;
            this.pictureBoxButtonYes.Enabled = !visable;
        }

        private void LabelResultYesNoVisable(bool visable)
        {
            this.labelResultNo.Visible = visable;
            this.labelResultYes.Visible = visable;
            this.pictureBoxResultNo.Visible = visable;
            this.pictureBoxResultYes.Visible = visable;
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

            this.textBoxUserIdOrUserName.Top = Math.Max(this.pictureBoxHeadFrame.Bottom,
                this.pictureBoxButtonNo.Top + (this.pictureBoxButtonNo.ClientSize.Height - this.textBoxUserIdOrUserName.Height) / 2);
            this.textBoxUserIdOrUserName.Left = this.pictureBoxHeadFrame.Left + this.pictureBoxHeadFrame.ClientSize.Width / 2 - this.textBoxUserIdOrUserName.ClientSize.Width / 2;

            this.pictureBoxButtonYes2.Width = this.textBoxUserIdOrUserName.Height - 12;
            this.pictureBoxButtonYes2.Height = this.textBoxUserIdOrUserName.Height - 12;
            this.pictureBoxButtonYes2.Left = this.textBoxUserIdOrUserName.Right - this.pictureBoxButtonYes2.Width - 6;
            this.pictureBoxButtonYes2.Top = this.textBoxUserIdOrUserName.Top + 6;

            this.pictureBoxButtonInfomation.Top = this.ClientSize.Height - this.pictureBoxButtonInfomation.Height - 8;
            this.pictureBoxButtonInfomation.Left = this.ClientSize.Width - this.pictureBoxButtonInfomation.Width - 8;

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
            TextBoxUserIdOrUserNameVisable(true);
        }

        private void pictureBoxButtonYes2_Click(object sender, EventArgs e)
        {
            TextBoxUserIdOrUserNameVisable(false);
            string userIdOrUserNameInput = this.textBoxUserIdOrUserName.Text;
            this.textBoxUserIdOrUserName.Text = string.Empty;

            this.labelUserName.Text = userIdOrUserNameInput;
            string userId = _checkInManager.FindNearlyUserId(userIdOrUserNameInput);

            if (!string.IsNullOrEmpty(userId))
            {
                ConfirmCheckIn(userId, false);
            }

            ResizeLabelUserName();
        }

        private void pictureBoxButtonYes_Click(object sender, EventArgs e)
        {
            var face = _faceDetector.CurrentFace;
            if (face != null)
            {
                ConfirmCheckIn(face.UserId, true);
            }
        }

        private void ConfirmCheckIn(string userId, bool checkInByAI)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var result = _checkInManager.CheckIn(userId, checkInByAI);
                if (result == CheckIn.CheckInManager.CheckInStatusTypes.Unknown)
                {
                    SetLabelResult(string.Format("对不起，{0}不在签到列表中！", userId), 1);
                }
                else if (result == CheckIn.CheckInManager.CheckInStatusTypes.Failure)
                {
                    SetLabelResult(string.Format("对不起，发生未知错误!", userId), 1);
                }
                else if (result == CheckIn.CheckInManager.CheckInStatusTypes.Duplicate)
                {
                    var userCheckInInfo = _checkInManager.FindUserCheckInByFace(userId);
                    if (userCheckInInfo != null)
                    {
                        SetLabelResult(string.Format("{0}已经于{1}签过到了，请勿重复签到!", userId, userCheckInInfo.CheckInDateTime.ToString("HH:MM:ss")), 3);
                    }
                }
                else if (result == CheckIn.CheckInManager.CheckInStatusTypes.Success)
                {
                    SetLabelResult(string.Format("签到成功，欢迎光临 {0}!", userId), 0);
                }
            }
            RefreshValues2UI();
        }

        private void timerForFaceDetect_Tick(object sender, EventArgs e)
        {
            RefreshFace2UI();
            if (labelResultShowDateTime.AddSeconds(6) < DateTime.Now)
            {
                ResetLabelResult();
            }
        }

        private void pictureBoxButtonInfomation_Click(object sender, EventArgs e)
        {
            if (this.labelResultNo.Visible)
            {
                this.pictureBoxButtonInfomation.Image = global::CheckInbyFace.Properties.Resources.info_dark;
                LabelResultYesNoVisable(false);
            }
            else
            {
                this.pictureBoxButtonInfomation.Image = global::CheckInbyFace.Properties.Resources.info_light;
                LabelResultYesNoVisable(true);
            }
        }
    }
}
