using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;

namespace CheckInbyFace.Objects
{
    public class FaceDetectInfos
    {
        public class FaceDetectInfo
        {
            public string UserId { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public string ImagePath { get; set; }

            public User RestoreUser(UserCheckInList users)
            {
                if (string.IsNullOrEmpty(UserId)
                    || users == null
                    || users.Count == 0)
                {
                    return null;
                }
                if (users.ContainsKey(UserId))
                {
                    return users[UserId].User;
                }
                return null;
            }
        }
        private List<FaceDetectInfo> _faces = null;
        public List<FaceDetectInfo> Faces
        {
            get
            {
                if (_faces == null)
                {
                    _faces = new List<FaceDetectInfo>();
                }
                return _faces;
            }
        }
    }
}
