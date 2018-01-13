using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckInbyFace
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private const int SCREEN_WIDTH = 1920;
        private const int SCREEN_HEIGHT = 1080;
        private void MainForm_Resize(object sender, EventArgs e)
        {
            double zoomRate = (double)this.ClientSize.Width / SCREEN_WIDTH;
            //// head frame
            //int headFrameLeft = (SCREEN_WIDTH - pictureBoxHeadFrame.ClientSize.Width) / 2;
            //int headFrameTop = pictureBoxHeadFrame.Top;
            //pictureBoxHeadFrame.Location = this.PointToClient(new Point(
            //    (int)((double)headFrameLeft / zoomRate),
            //    (int)((double)headFrameTop / zoomRate)));
        }
    }
}
