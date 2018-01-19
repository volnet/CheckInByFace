using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
