using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckInbyFace.Objects;
using Newtonsoft.Json;

namespace CheckInbyFace.TestFrameGenerator
{
    class Program
    {
        static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            _logger.Debug("start");
            _logger.Debug("args.Length = " + args.Length);
            string mode = string.Empty;

            try
            {
                if (args.Length > 0)
                {
                    mode = args[0];
                    for (int i = 0, length = args.Length; i < length; ++i)
                    {
                        string arg = args[i];
                        _logger.Debug("   args[" + i + "] = " + arg);
                    }
                }
                if (args.Length == 0 || mode == "CommunicateByFile")
                {
                    int minutes = 1;
                    string folderPath = string.Empty;
                    if (args.Length == 3)
                    {
                        int.TryParse(args[1], out minutes);
                        folderPath = args[2];
                    }
                    _logger.Debug("CommunicateByFile(\"" + minutes.ToString() + "\", \"" + folderPath + "\")");
                    CommunicateByFile(minutes, folderPath);
                }
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.ToString());
            }
            finally
            {
                _logger.Debug("end");
            }
        }

        static void CommunicateByFile(int minutes, string folderPath = @"../../../../../data/public/samples/")
        {
            string jsonFilePath = System.IO.Path.GetFullPath(folderPath + "faceDetectInfos.json");
            string frameFilePath = System.IO.Path.GetFullPath(folderPath + "frame.jpg");
            string demoFrameFilePath = AppDomain.CurrentDomain.BaseDirectory + @"demo/frame.jpg";

            Random r = new Random(DateTime.Now.Millisecond);
            DateTime end = DateTime.Now.AddMinutes(minutes);
            while (DateTime.Now < end)
            {
                int x = r.Next(0, 200);
                int y = r.Next(0, 200);
                int width = r.Next(100, 200);
                int height = r.Next(100, 200);

                FaceDetectInfos fdis = new FaceDetectInfos();
                fdis.Faces.Add(new FaceDetectInfos.FaceDetectInfo()
                {
                    UserId = "user1",
                    X = x,
                    Y = y,
                    Width = width,
                    Height = height
                });
                string json = JsonConvert.SerializeObject(fdis, Formatting.Indented);
                System.IO.File.WriteAllText(jsonFilePath, json);

                Console.WriteLine(jsonFilePath + " created.");

                if (!System.IO.File.Exists(demoFrameFilePath))
                {
                    Console.WriteLine(demoFrameFilePath + " not exist.");
                    _logger.Debug(demoFrameFilePath + " not exist.");
                }
                else
                {
                    System.IO.File.Copy(demoFrameFilePath, frameFilePath, true);
                    Console.WriteLine(frameFilePath + " created.");
                    _logger.Debug(frameFilePath + " created.");
                }
            }
        }
    }
}
