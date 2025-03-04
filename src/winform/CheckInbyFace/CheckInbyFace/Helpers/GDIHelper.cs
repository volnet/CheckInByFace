﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckInbyFace.Helpers
{
    public class GDIHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="rec">Cut Region</param>
        /// <param name="size">Target Region</param>
        /// <returns></returns>
        public static Image CutEllipse(Image image, Rectangle rec, Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (TextureBrush br = new TextureBrush(image, System.Drawing.Drawing2D.WrapMode.Clamp, rec))
                {
                    br.ScaleTransform(bitmap.Width / (float)rec.Width, bitmap.Height / (float)rec.Height);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(br, new Rectangle(Point.Empty, size));
                }
            }
            return bitmap;
        }

        public static Image Layout(Image image, Size size, ImageLayout layout)
        {
            if (image == null)
            {
                return null;
            }
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (layout == ImageLayout.None)
            {
                g.DrawImageUnscaled(image, Point.Empty);
            }
            else if (layout == ImageLayout.Tile)
            {
                int x = 0, y = 0;
                while (y < size.Height)
                {
                    g.DrawImageUnscaled(image, new Point(x, y));
                    x += image.Width;
                    if (x >= size.Width)
                    {
                        x = 0;
                        y += image.Height;
                    }
                }
            }
            else if (layout == ImageLayout.Zoom)
            {
                float scaleX = size.Width * 1.0F / image.Width;
                float scaleY = size.Height * 1.0F / image.Height;
                RectangleF rectF = new RectangleF();
                if (scaleX < scaleY)
                {
                    rectF.Width = image.Width * scaleX;
                    rectF.Height = image.Height * scaleX;
                }
                else
                {
                    rectF.Width = image.Width * scaleY;
                    rectF.Height = image.Height * scaleY;
                }
                rectF.X = (size.Width - rectF.Width) / 2.0F;
                rectF.Y = (size.Height - rectF.Height) / 2.0F;
                g.DrawImage(image, rectF);
            }
            else if (layout == ImageLayout.Stretch)
            {
                g.DrawImage(image, new Rectangle(Point.Empty, size));
            }

            return bitmap;
        }

        /// <summary>
        /// Overlay Image
        /// </summary>
        /// <param name="imageBackground"></param>
        /// <param name="imageFront"></param>
        /// <param name="imageFrontAlpha">0.1f ~ 1.0f</param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Image OverlayImage(Image imageBackground, Image imageFront, float imageFrontAlpha, Point location)
        {
            if (imageBackground == null
                || imageFront == null
                || imageFrontAlpha == 0.0f)
            {
                return null;
            }

            ImageAttributes attr = GetAlphaImgAttr((int)(imageFrontAlpha * 100));

            Bitmap bmPhoto = new Bitmap(imageBackground.Width, imageBackground.Height, PixelFormat.Format32bppArgb);
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
                  GraphicsUnit.Pixel,
                  attr);
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
                    GraphicsUnit.Pixel,
                    attr);
            }

            return bmWatermark;
        }

        private static ImageAttributes GetAlphaImgAttr(int opcity)
        {
            if (opcity < 0 || opcity > 100)
            {
                throw new ArgumentOutOfRangeException("opcity: 0~100");
            }
            //颜色矩阵
            float[][] matrixItems =
            {
                  new float[]{1,0,0,0,0},
                  new float[]{0,1,0,0,0},
                  new float[]{0,0,1,0,0},
                  new float[]{0,0,0,(float)opcity / 100,0},
                  new float[]{0,0,0,0,1}
             };
            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
            ImageAttributes imageAtt = new ImageAttributes();
            imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            return imageAtt;
        }

        public static Image OverlayImageWithCutEllipse(Image imageBackground,
            Image imageFront, Rectangle imageFrontCutRect, float imageFrontAlpha, Point originalLocation, Size originalImageFrontTargetSize, Size targetSize)
        {
            if (imageBackground == null || imageFront == null)
            {
                throw new ArgumentNullException("imageBackground == null || imageFront == null");
            }

            Image cutImageFront = CutEllipse(imageFront, imageFrontCutRect, originalImageFrontTargetSize);
            Image overlayImage = OverlayImage(imageBackground, cutImageFront, imageFrontAlpha, originalLocation);
            Image result = Layout(overlayImage, targetSize, ImageLayout.Zoom);

            return result;
        }
    }
}
