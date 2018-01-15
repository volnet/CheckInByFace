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
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\UI\demo.jpg";

            Assert.IsNotNull(string.IsNullOrEmpty(path));
            Assert.IsTrue(System.IO.File.Exists(path));

            Image image = Image.FromFile(path);
            Assert.IsNotNull(image);

            Image newImage = Helpers.GDIHelper.CutEllipse(image,
                new Rectangle(0, 0, 300, 300),
                new Size(1000, 300));

            string newPath = path.Replace(".jpg", "-CutEllipseTest.jpg");
            newImage.Save(newPath);
            Assert.IsNotNull(string.IsNullOrEmpty(newPath));
            Assert.IsTrue(System.IO.File.Exists(newPath));

        }

        [TestMethod()]
        public void OverlayImageTest()
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

        [TestMethod()]
        public void LayoutTest()
        {
            string image1Path = AppDomain.CurrentDomain.BaseDirectory + @"\UI\demo.jpg";
            Assert.IsNotNull(string.IsNullOrEmpty(image1Path));
            Assert.IsTrue(System.IO.File.Exists(image1Path));
            Image image1 = Image.FromFile(image1Path);
            Assert.IsNotNull(image1);

            Image imageNone = GDIHelper.Layout(image1, new Size(1000, 1000), System.Windows.Forms.ImageLayout.None);
            Image imageTile = GDIHelper.Layout(image1, new Size(1000, 1000), System.Windows.Forms.ImageLayout.Tile);
            Image imageCenter = GDIHelper.Layout(image1, new Size(1000, 1000), System.Windows.Forms.ImageLayout.Center);
            Image imageZoom = GDIHelper.Layout(image1, new Size(1000, 1000), System.Windows.Forms.ImageLayout.Zoom);
            Image imageStretch = GDIHelper.Layout(image1, new Size(1000, 1000), System.Windows.Forms.ImageLayout.Stretch);

            Image imageTile2 = GDIHelper.Layout(image1, new Size(100, 100), System.Windows.Forms.ImageLayout.Tile);
            Image imageCenter2 = GDIHelper.Layout(image1, new Size(100, 100), System.Windows.Forms.ImageLayout.Center);
            Image imageZoom2 = GDIHelper.Layout(image1, new Size(100, 100), System.Windows.Forms.ImageLayout.Zoom);
            Image imageStretch2 = GDIHelper.Layout(image1, new Size(100, 100), System.Windows.Forms.ImageLayout.Stretch);

            string folderPath = AppDomain.CurrentDomain.BaseDirectory + @"\UI\";
            imageNone.Save(folderPath + "LayoutTest_imageNone.png", System.Drawing.Imaging.ImageFormat.Png);
            imageTile.Save(folderPath + "LayoutTest_imageTile.png", System.Drawing.Imaging.ImageFormat.Png);
            imageCenter.Save(folderPath + "LayoutTest_imageCenter.png", System.Drawing.Imaging.ImageFormat.Png);
            imageZoom.Save(folderPath + "LayoutTest_imageZoom.png", System.Drawing.Imaging.ImageFormat.Png);
            imageStretch.Save(folderPath + "LayoutTest_imageStretch.png", System.Drawing.Imaging.ImageFormat.Png);
            imageTile2.Save(folderPath + "LayoutTest_imageTile2.png", System.Drawing.Imaging.ImageFormat.Png);
            imageCenter2.Save(folderPath + "LayoutTest_imageCenter2.png", System.Drawing.Imaging.ImageFormat.Png);
            imageZoom2.Save(folderPath + "LayoutTest_imageZoom2.png", System.Drawing.Imaging.ImageFormat.Png);
            imageStretch2.Save(folderPath + "LayoutTest_imageStretch2.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        [TestMethod()]
        public void OverlayImageWithCutEllipseTest()
        {
            string image1Path = AppDomain.CurrentDomain.BaseDirectory + @"\UI\head-frame.png";
            string image2Path = AppDomain.CurrentDomain.BaseDirectory + @"\UI\demo2.jpg";

            Assert.IsNotNull(string.IsNullOrEmpty(image1Path));
            Assert.IsTrue(System.IO.File.Exists(image1Path));
            Image image1 = Image.FromFile(image1Path);
            Assert.IsNotNull(image1);
            image1.Save(image1Path.Replace(".png", "-raw-OverlayImageWithCutEllipseTest.png"), System.Drawing.Imaging.ImageFormat.Png);

            Assert.IsNotNull(string.IsNullOrEmpty(image2Path));
            Assert.IsTrue(System.IO.File.Exists(image2Path));
            Image image2 = Image.FromFile(image2Path);
            Assert.IsNotNull(image2);
            image2.Save(image2Path.Replace(".jpg", "-raw-OverlayImageWithCutEllipseTest.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);

            Image result = GDIHelper.OverlayImageWithCutEllipse(image1, image2, new Rectangle(image2.Width / 50 * 20, image2.Height / 16 * 1, image2.Width / 7, image2.Width / 7),
                1.0F, new Point(image1.Width / 7, image1.Height / 7), new Size(image1.Width / 7 * 5, image1.Height / 7 * 5),
                new Size(image1.Width / 3 * 2, image1.Height / 3 * 2));

            string resultPath = AppDomain.CurrentDomain.BaseDirectory + @"\UI\OverlayImageWithCutEllipseTest-result.png";
            result.Save(resultPath, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}