using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CheckInbyFace.Objects
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public GenderTypes Gender { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime FirstCome { get; set; }
        public string Mobile { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
        public string AvatarURI { get; set; }
    
        public enum GenderTypes
        {
            Unknown = 0,
            Male = 1,
            Female = 2
        }
    }
}
