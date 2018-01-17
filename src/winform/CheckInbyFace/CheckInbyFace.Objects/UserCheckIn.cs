using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInbyFace.Objects
{
    public class UserCheckIn
    {
        public User User { get; set; }
        public bool CheckInStatus { get; set; }
        public DateTime CheckInDateTime { get; set; }
    }
}
