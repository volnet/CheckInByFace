using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckInbyFace.Objects;
using Newtonsoft.Json;

namespace CheckInbyFace.CheckIn
{
    public class CheckInManager
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public CheckInManager()
        {
            LoadCheckInList();
            ConvertToUserCheckInList();
        }
        private List<User> _users = null;
        private const string CHECKIN_LIST_FILE = @"Data\users.json";
        private const string CHECKIN_RESULT_FILE = @"Data\checkin-result.json";
        private string _checkInListFileFullPath = string.Empty;
        private string CheckInListFileFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(_checkInListFileFullPath))
                {
                    _checkInListFileFullPath = System.IO.Path.GetFullPath(CHECKIN_LIST_FILE);
                }
                return _checkInListFileFullPath;
            }
        }
        private void LoadCheckInList()
        {
            if (!System.IO.File.Exists(CheckInListFileFullPath))
            {
                throw new ArgumentNullException(CheckInListFileFullPath + " not exists.");
            }
            string json = System.IO.File.ReadAllText(CheckInListFileFullPath);
            _users = JsonConvert.DeserializeObject<List<User>>(json);
            if (_users == null)
            {
                throw new ArgumentNullException("LoadCheckInList with exception, _list is null");
            }
        }

        private UserCheckInList _userCheckInList = null;
        private void ConvertToUserCheckInList()
        {
            if (_users != null)
            {
                _userCheckInList = new UserCheckInList();
                foreach (User user in _users)
                {
                    if (!_userCheckInList.ContainsKey(user.UserId))
                    {
                        UserCheckIn userCheckIn = new UserCheckIn()
                        {
                            User = user,
                            CheckInDateTime = DateTime.MinValue,
                            CheckInStatus = false
                        };
                        _userCheckInList.Add(user.UserId, userCheckIn);
                    }
                    else
                    {
                        _logger.Warn(user.UserId + " is duplicate.");
                    }
                }
            }
        }

        public int TotalCount
        {
            get
            {
                if (_userCheckInList == null)
                    return 0;
                return _userCheckInList.Count;
            }
        }

        public int CheckInCount
        {
            get
            {
                if (_userCheckInList == null)
                    return 0;
                int i = 0;
                foreach (UserCheckIn uci in _userCheckInList.Values)
                {
                    if (uci.CheckInStatus)
                        ++i;
                }
                return i;
            }
        }

        public CheckInStatusTypes CheckIn(string userId)
        {
            if (!_userCheckInList.ContainsKey(userId))
            {
                return CheckInStatusTypes.Unknown;
            }
            else
            {
                try
                {
                    UserCheckIn user = _userCheckInList[userId];
                    user.CheckInDateTime = DateTime.Now;
                    user.CheckInStatus = true;
                    return CheckInStatusTypes.Success;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, userId + " checkin with exception");
                    return CheckInStatusTypes.Failure;
                }
            }
        }

        public bool SaveToDisk()
        {
            try
            {
                if (_userCheckInList != null)
                {
                    string json = JsonConvert.SerializeObject(_userCheckInList, Formatting.Indented);
                    System.IO.File.WriteAllText(CHECKIN_RESULT_FILE, json, Encoding.UTF8);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SaveToDisk with exception");
                return false;
            }
            return false;
        }

        public enum CheckInStatusTypes
        {
            Unknown,
            Success,
            Failure
        }
    }
}
