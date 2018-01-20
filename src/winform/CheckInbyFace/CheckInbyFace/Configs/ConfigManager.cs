using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInbyFace.Configs
{
    public class ConfigManager
    {
        public static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// File, SQLite
        /// </summary>
        public static string DataCommunicateMode
        {
            get
            {
                return GetAppSetting("DataCommunicateMode");
            }
        }

        private static string DataCommunicateModeFileFilePath
        {
            get
            {
                return GetAppSetting("DataCommunicateMode.File.FilePath");
            }
        }

        private static string _dataCommunicateModeFileFileFullPath = null;
        public static string DataCommunicateModeFileFileFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(_dataCommunicateModeFileFileFullPath))
                {
                    _dataCommunicateModeFileFileFullPath = System.IO.Path.GetFullPath(DataCommunicateModeFileFilePath);
                }
                return _dataCommunicateModeFileFileFullPath;
            }
        }
    }
}
