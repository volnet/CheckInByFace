using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckInbyFace.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CheckInbyFace.Helpers.Tests
{
    [TestClass()]
    public class GDIHelperTests
    {
        [TestMethod()]
        public void CutEllipseTest()
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"\UI\demo.jpg";

                Assert.IsNotNull(string.IsNullOrEmpty(path));
                Assert.IsTrue(System.IO.File.Exists(path));

                Image image = Image.FromFile(path);
                Assert.IsNotNull(image);

                Image newImage = Helpers.GDIHelper.CutEllipse(image,
                    new Rectangle(0, 0, 300, 300),
                    new Size(300, 300));

                string newPath = path.Replace(".jpg", "-CutEllipseTest.jpg");
                newImage.Save(newPath);
                Assert.IsNotNull(string.IsNullOrEmpty(newPath));
                Assert.IsTrue(System.IO.File.Exists(newPath));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void OverlayImageTest()
        {
            try
            {
                string image1Path = AppDomain.CurrentDomain.BaseDirectory + @"\UI\head-frame.png";
                string image2Path = AppDomain.CurrentDomain.BaseDirectory + @"\UI\demo.jpg";

                Assert.IsNotNull(string.IsNullOrEmpty(image1Path));
                Assert.IsTrue(System.IO.File.Exists(image1Path));
                Image image1 = Image.FromFile(image1Path);
                Assert.IsNotNull(image1);
                image1.Save(image1Path.Replace(".png", "-raw-OverlayImageTest.png"), System.Drawing.Imaging.ImageFormat.Png);

                Assert.IsNotNull(string.IsNullOrEmpty(image2Path));
                Assert.IsTrue(System.IO.File.Exists(image2Path));
                Image image2 = Image.FromFile(image2Path);
                Assert.IsNotNull(image2);
                image2.Save(image2Path.Replace(".jpg", "-raw-OverlayImageTest.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

                int size = (int)(Math.Min(image1.Width * 0.6, image2.Width));
                Image newImage = Helpers.GDIHelper.CutEllipse(image2,
                    new Rectangle(0, 0, size, size),
                    new Size(size, size));
                Assert.IsNotNull(newImage);
                newImage.Save(image2Path.Replace(".jpg", "-CutEllipse.png"), System.Drawing.Imaging.ImageFormat.Png);

                Point location = new Point((image1.Width - newImage.Width) / 2, (image1.Height - newImage.Height) / 2);
                Image newImage2 = Helpers.GDIHelper.OverlayImage(image1, newImage, 1.0f, location);
                Assert.IsNotNull(newImage2);

                string newPath = image1Path.Replace(".png", "-OverlayImageTest.png");
                newImage2.Save(newPath, System.Drawing.Imaging.ImageFormat.Png);
                Assert.IsNotNull(string.IsNullOrEmpty(newPath));
                Assert.IsTrue(System.IO.File.Exists(newPath));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
    }
}