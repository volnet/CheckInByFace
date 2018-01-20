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
                    int minutes = 60;
                    string folderPath = string.Empty;
                    if (args.Length == 3)
                    {
                        int.TryParse(args[1], out minutes);
                        folderPath = args[2];
                        _logger.Debug("CommunicateByFile(\"" + minutes.ToString() + "\", \"" + folderPath + "\")");
                        CommunicateByFile(minutes, folderPath);
                    }
                    else
                    {
                        _logger.Debug("CommunicateByFile(\"" + minutes.ToString() + "\")");
                        CommunicateByFile(minutes);
                    }
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
            string frameTemplatePath = System.IO.Path.GetFullPath(folderPath + "frame{No}.jpg");
            string demoFrameTemplatePath = AppDomain.CurrentDomain.BaseDirectory + @"demo/frame{No}.jpg";

            Random r = new Random(DateTime.Now.Millisecond);
            DateTime end = DateTime.Now.AddMinutes(minutes);
            int i = 0;
            while (DateTime.Now < end)
            {
                if (i > 10000)
                {
                    i = 0;
                }
                else
                {
                    ++i;
                }

                string frameFilePath = string.Empty;
                string demoFrameFilePath = string.Empty;

                try
                {
                    int x = r.Next(0, 200);
                    int y = r.Next(0, 200);
                    int width = r.Next(100, 200);
                    int height = r.Next(100, 200);

                    if (i % 3 == 1)
                    {
                        x = 275;
                        y = 107;
                        width = 250;
                        height = 250;
                    }
                    else if (i % 3 == 2)
                    {
                        x = 335;
                        y = 35;
                        width = 220;
                        height = 220;
                    }

                    demoFrameFilePath = demoFrameTemplatePath.Replace("{No}", (i % 3).ToString());

                    if (!System.IO.File.Exists(demoFrameFilePath))
                    {
                        _logger.Debug(demoFrameFilePath + " not exist.");
                    }
                    else
                    {
                        frameFilePath = frameTemplatePath.Replace("{No}", i.ToString());
                        System.IO.File.Copy(demoFrameFilePath, frameFilePath, true);
                        _logger.Debug(frameFilePath + " created.");
                    }

                    FaceDetectInfos fdis = new FaceDetectInfos();
                    fdis.Faces.Add(new FaceDetectInfos.FaceDetectInfo()
                    {
                        UserId = "user" + (i % 3).ToString(),
                        X = x,
                        Y = y,
                        Width = width,
                        Height = height,
                        ImagePath = frameFilePath
                    });

                    string json = JsonConvert.SerializeObject(fdis, Formatting.Indented);
                    System.IO.File.WriteAllText(jsonFilePath, json);

                    _logger.Debug(jsonFilePath + " created." + json);
                }
                catch (Exception ex)
                {
                    _logger.Debug(ex.ToString());
                }

                System.Threading.Thread.Sleep(2000);
            }
        }
    }
}
