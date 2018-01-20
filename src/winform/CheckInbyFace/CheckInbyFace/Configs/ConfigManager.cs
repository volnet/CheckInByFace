﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInbyFace.Configs
{
    public class ConfigManager
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
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

        private static string ResultFolderPath
        {
            get
            {
                return GetAppSetting("ResultFolderPath");
            }
        }

        private static string _resultFolderFullPath = null;
        public static string ResultFolderFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(_resultFolderFullPath))
                {
                    _resultFolderFullPath = System.IO.Path.GetFullPath(ResultFolderPath);
                }
                return _resultFolderFullPath;
            }
        }

        private static string _saveFramesFullPath = string.Empty;
        public static string SaveFramesFolderFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(_saveFramesFullPath))
                {

                    string path = System.IO.Path.Combine(ResultFolderFullPath, @"SaveFrames\");
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
                    if (di != null)
                    {
                        try
                        {
                            if (!di.Exists)
                            {
                                di.Create();
                            }
                            _saveFramesFullPath = path;
                        }
                        catch (Exception ex)
                        {
                            _logger.Debug(ex.ToString());
                        }
                    }
                }
                return _saveFramesFullPath;
            }
        }
    }
}
