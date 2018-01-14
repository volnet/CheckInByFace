using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInbyFace.Helpers
{
    public class GDIHelper
    {
        public static Image CutEllipse(Image img, Rectangle rec, Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (TextureBrush br = new TextureBrush(img, System.Drawing.Drawing2D.WrapMode.Clamp, rec))
                {
                    br.ScaleTransform(bitmap.Width / (float)rec.Width, bitmap.Height / (float)rec.Height);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(br, new Rectangle(Point.Empty, size));
                }
            }
            return bitmap;
        }

        // !error!
        public static Image OverlayImage(Image image1, Image image2, Point location)
        {
            Bitmap bitmap = new Bitmap(image1.Size.Width, image1.Size.Height);

            using (Graphics g = Graphics.FromImage(image1))
            {
                g.DrawImage(image2, location);
            }
            return bitmap;
        }
    }
}
