using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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
        public static Image OverlayImage(Image imageBackground, Image imageFront, float imageFrontAlpha, Point location)
        {
            if (imageBackground == null
                || imageFront == null
                || imageFrontAlpha == 0.0f)
            {
                return null;
            }

            Bitmap bmPhoto = new Bitmap(imageBackground.Width, imageBackground.Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imageBackground.HorizontalResolution, imageBackground.VerticalResolution);

            using (Graphics grPhoto = Graphics.FromImage(bmPhoto))
            {
                grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
                grPhoto.DrawImage(imageBackground,
                  new Rectangle(0, 0, imageBackground.Width, imageBackground.Height),
                  0,
                  0,
                  imageBackground.Width,
                  imageBackground.Height,
                  GraphicsUnit.Pixel);
            }

            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(imageBackground.HorizontalResolution, imageBackground.VerticalResolution);

            using (Graphics grWatermark = Graphics.FromImage(bmWatermark))
            {
                grWatermark.DrawImage(imageFront,
                    new Rectangle(location.X,
                                    location.Y,
                                    imageFront.Width,
                                    imageFront.Height),
                    0,
                    0,
                    imageFront.Width,
                    imageFront.Height,
                    GraphicsUnit.Pixel);
            }

            return bmWatermark;
        }
    }
}
