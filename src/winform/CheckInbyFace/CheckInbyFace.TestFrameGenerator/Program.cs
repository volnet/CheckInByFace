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
        static void Main(string[] args)
        {
            CommunicateByFile();
        }

        static void CommunicateByFile()
        {
            string folderPath = @"../../../../../data/public/samples/";
            string jsonFilePath = System.IO.Path.GetFullPath(folderPath + "faceDetectInfos.json");
            string frameFilePath = System.IO.Path.GetFullPath(folderPath + "frame.jpg");
            string demoFrameFilePath = System.IO.Path.GetFullPath(@"demo/frame.jpg");

            if (!System.IO.File.Exists(demoFrameFilePath))
            {
                Console.WriteLine(demoFrameFilePath + " not exist.");
            }
            else
            {
                System.IO.File.Copy(demoFrameFilePath, frameFilePath, true);
                Console.WriteLine(frameFilePath + " created.");
            }

            Random r = new Random(DateTime.Now.Millisecond);
            while (true)
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
            }
        }
    }
}
