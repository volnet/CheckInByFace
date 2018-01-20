using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckInbyFace.Objects;

namespace CheckInbyFace.CheckIn
{
    public class FaceDetector
    {
        public FaceDetectInfos CurrentFaces { get; private set; }
        public FaceDetectInfos.FaceDetectInfo CurrentFace { get; private set; }

        public CheckInbyFace.Objects.FaceDetectInfos Read()
        {
            CheckInbyFace.Objects.FaceDetectInfos fdis = GetFaceDetectInfos(
                Configs.ConfigManager.DataCommunicateMode,
                Configs.ConfigManager.DataCommunicateModeFileFileFullPath);
            if (fdis == null)
            {

            }
            return fdis;
        }

        public static CheckInbyFace.Objects.FaceDetectInfos GetFaceDetectInfos(
            string dataCommunicateMode,
            string jsonFileFullPath)
        {
            if (dataCommunicateMode == "File")
            {
                CheckInbyFace.Objects.FaceDetectInfos fdis = null;

                try
                {
                    string json = System.IO.File.ReadAllText(jsonFileFullPath);
                    fdis = Newtonsoft.Json.JsonConvert.DeserializeObject<CheckInbyFace.Objects.FaceDetectInfos>(json);
                }
                catch (Exception ex)
                {
                    fdis = null;
                    System.Diagnostics.Trace.WriteLine(ex.ToString());
                }

                return fdis;
            }
            return null;
        }
    }
}
