using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckInbyFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CheckInbyFace.Tests
{
    [TestClass()]
    public class MainFormTests
    {
        [TestMethod()]
        public void GetFaceDetectInfosTest()
        {
            //Thread thread1 = new Thread(new ThreadStart(() =>
            //{
            //    string testGeneratorFilePath = @"../../../CheckInbyFace.TestFrameGenerator/bin/Debug/CheckInbyFace.TestFrameGenerator.exe";
            //    string testGeneratorFileFullPath = System.IO.Path.GetFullPath(testGeneratorFilePath);
            //    System.Diagnostics.Trace.WriteLine("testGeneratorFilePath=" + testGeneratorFilePath);
            //    System.Diagnostics.Trace.WriteLine("testGeneratorFileFullPath=" + testGeneratorFileFullPath);

            //    string folderPath = @"../../../../../data/public/samples/";
            //    string folderFullPath = System.IO.Path.GetFullPath(folderPath);
            //    System.Diagnostics.Trace.WriteLine("folderFullPath=" + folderFullPath);

            //    try
            //    {
            //        Shell.CMD(testGeneratorFileFullPath, @"CommunicateByFile 1 " + folderFullPath, null);
            //        System.Diagnostics.Trace.WriteLine("Shell.CMD executed.");
            //    }
            //    catch (Exception ex)
            //    {   
            //        System.Diagnostics.Trace.WriteLine(ex.ToString());
            //    }
            //}));

            //Thread thread2 = new Thread(new ThreadStart(() =>
            //{
            DateTime end = DateTime.Now.AddMinutes(1);
            int emptyCount = 0;
            int successCount = 0;
            int errorCount = 0;
            int count = 0;
            string jsonFileFullPath = System.IO.Path.GetFullPath(@"../../../../../data/public/samples/faceDetectInfos.json");
            string frameFileFullPath = System.IO.Path.GetFullPath(@"../../../../../data/public/samples/frame.jpg");
            while (DateTime.Now < end)
            {
                try
                {
                    System.Diagnostics.Trace.Write("Read data " + (++count).ToString() + " times.");
                    var result = MainForm.GetFaceDetectInfos(
                       "File",
                       jsonFileFullPath,
                       frameFileFullPath
                    );
                    if (result == null)
                    {
                        ++emptyCount;
                        System.Diagnostics.Trace.WriteLine("++emptyCount;");
                    }
                    else
                    {
                        Assert.IsNotNull(result.Item1);
                        Assert.IsNotNull(result.Item2);
                        Assert.IsTrue(result.Item2.Length > 0);
                        ++successCount;
                        System.Diagnostics.Trace.WriteLine("++successCount;");
                    }
                }
                catch (Exception ex)
                {
                    ++errorCount;
                    System.Diagnostics.Trace.WriteLine(ex.ToString());
                }
            }
            System.Diagnostics.Trace.WriteLine(string.Format("emptyCount = {0} / successCount = {1} / errorCount = {2}", emptyCount, successCount, errorCount));
            //}));
            //thread1.Start();
            //thread2.Start();
            //Thread.Sleep(1000 * 10);
        }
    }
}