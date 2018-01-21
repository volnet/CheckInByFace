using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CheckInbyFace.Objects
{
    public class CheckInResult
    {
        public int TotalCount
        {
            get
            {
                if (Users == null)
                    return 0;
                return Users.Count;
            }
        }
        
        public int CheckInCount
        {
            get
            {
                int i = 0;
                if (Users != null)
                {
                    foreach (UserCheckIn userCheckIn in Users.Values)
                    {
                        if (userCheckIn.CheckInStatus)
                            ++i;
                    }
                }
                return i;
            }
        }
        
        public int CheckInByAICount
        {
            get
            {
                int i = 0;
                if (Users != null)
                {
                    foreach (UserCheckIn userCheckIn in Users.Values)
                    {
                        if (userCheckIn.CheckInStatus && userCheckIn.CheckInByAI)
                        {
                            ++i;
                        }
                    }
                }
                return i;
            }
        }

        public int CheckInByAdminCount
        {
            get
            {
                int result = CheckInCount - CheckInByAICount;
                if (result < 0)
                {
                    throw new ArgumentOutOfRangeException("CheckInByAdminCount/CheckInByAICount/CheckInCount is error.");
                }
                return result;
            }
        }

        public float CheckInByAdminPercent
        {
            get
            {
                return ((float)CheckInByAdminCount / (float)TotalCount * 100);
            }
        }

        public float CheckInByAIPercent
        {
            get
            {
                if (CheckInCount == 0)
                {
                    return 0;
                }
                return 100 - CheckInByAdminPercent;
            }
        }
        public UserCheckInList Users { get; set; }
    }
}
