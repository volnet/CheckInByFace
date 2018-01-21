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
            InitOrRestoreCheckInResult();
        }

        private const string CHECKIN_LIST_FILE = @"Data\users.json";
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
        private List<User> LoadCheckInList()
        {
            List<User> _users = null;
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
            return _users;
        }

        private const string CHECKIN_RESULT_FILE = @"Data\checkin-result.json";
        private string _checkInResultFileFullPath = string.Empty;
        private string CheckInResultFileFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(_checkInResultFileFullPath))
                {
                    _checkInResultFileFullPath = System.IO.Path.GetFullPath(CHECKIN_RESULT_FILE);
                }
                return _checkInResultFileFullPath;
            }
        }

        private Objects.CheckInResult _checkInResult = null;
        public Objects.CheckInResult CheckInResult
        {
            get
            {
                return _checkInResult;
            }
            private set
            {
                _checkInResult = value;
            }
        }

        private void InitOrRestoreCheckInResult()
        {
            // 1. create new checkInResult from users.json
            CheckInResult newCheckInResult = null;
            List<User> users = LoadCheckInList();
            if (users != null)
            {
                newCheckInResult = new CheckInResult();
                foreach (User user in users)
                {
                    if (newCheckInResult.Users == null)
                    {
                        newCheckInResult.Users = new UserCheckInList();
                    }
                    if (!newCheckInResult.Users.ContainsKey(user.UserId))
                    {
                        UserCheckIn userCheckIn = new UserCheckIn()
                        {
                            User = user,
                            CheckInDateTime = DateTime.MinValue,
                            CheckInStatus = false
                        };
                        newCheckInResult.Users.Add(user.UserId, userCheckIn);
                    }
                    else
                    {
                        _logger.Warn(user.UserId + " is duplicate.");
                    }
                }
            }

            // 2. recover data from history (if have).
            CheckInResult historyCheckInResult = null;
            if (System.IO.File.Exists(CheckInResultFileFullPath))
            {
                string json = System.IO.File.ReadAllText(CheckInResultFileFullPath);
                if (string.IsNullOrEmpty(json))
                {
                    _logger.Warn(CheckInResultFileFullPath + " is empty.");
                }
                else
                {
                    try
                    {
                        historyCheckInResult = JsonConvert.DeserializeObject<CheckInResult>(json);
                        _logger.Info(CheckInResultFileFullPath + " loaded.");
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn("load checkInResult with mistake. " + CheckInResultFileFullPath + " " + ex.ToString());
                    }
                }
            }

            if (historyCheckInResult != null && historyCheckInResult.Users != null)
            {
                foreach (var userCheckInPair in historyCheckInResult.Users)
                {
                    string userId = userCheckInPair.Key;
                    if (newCheckInResult.Users.ContainsKey(userId))
                    {
                        newCheckInResult.Users[userId] = historyCheckInResult.Users[userId];
                    }
                }
            }

            this.CheckInResult = newCheckInResult;
        }

        public CheckInStatusTypes CheckIn(string userId, bool checkInByAI)
        {
            if (!_checkInResult.Users.ContainsKey(userId))
            {
                return CheckInStatusTypes.Unknown;
            }
            else
            {
                try
                {
                    UserCheckIn user = _checkInResult.Users[userId];
                    if (user.CheckInStatus)
                    {
                        return CheckInStatusTypes.Duplicate;
                    }

                    user.CheckInDateTime = DateTime.Now;
                    user.CheckInStatus = true;

                    if (checkInByAI)
                    {
                        user.CheckInByAI = true;
                    }
                    else
                    {
                        user.CheckInByAI = false;
                    }

                    return CheckInStatusTypes.Success;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, userId + " checkin with exception");
                    return CheckInStatusTypes.Failure;
                }
            }
        }

        public UserCheckIn FindUserCheckInByFace(string userId)
        {
            if (!string.IsNullOrEmpty(userId) 
                && _checkInResult != null
                && _checkInResult.Users != null 
                && _checkInResult.Users.ContainsKey(userId))
            {
                return _checkInResult.Users[userId];
            }
            return null;
        }

        public string FindNearlyUserId(string userIdOrUserName, bool exactlyMatch = false)
        {
            if (_checkInResult.Users.ContainsKey(userIdOrUserName))
            {
                return userIdOrUserName;
            }
            else
            {
                var result = _checkInResult.Users.Values.FirstOrDefault<UserCheckIn>((u) =>
                {
                    if (u.User.UserId == userIdOrUserName)
                    {
                        return true;
                    }
                    return false;
                });
                if (result != null)
                {
                    return result.User.UserId;
                }
            }

            if (!exactlyMatch)
            {
                string result = _checkInResult.Users.Keys.FirstOrDefault<string>((u)=> {
                    return u.Contains(userIdOrUserName);
                });
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }

                var result2 = _checkInResult.Users.Values.FirstOrDefault<UserCheckIn>((u)=> {
                    return u.User.UserName.Contains(userIdOrUserName);
                });
                if (result2 != null)
                {
                    return result2.User.UserId;
                }
            }
            return string.Empty;
        }

        public bool SaveToDisk()
        {
            try
            {
                if (_checkInResult != null)
                {
                    string json = JsonConvert.SerializeObject(_checkInResult, Formatting.Indented);
                    System.IO.File.WriteAllText(this.CheckInResultFileFullPath, json, Encoding.UTF8);
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
            Failure,
            Duplicate
        }
    }
}
