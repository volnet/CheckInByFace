using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckInbyFace.Objects;
using Newtonsoft.Json;

namespace CheckInbyFaceTests.Objects
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestJSON()
        {
            User user1 = new User()
            {
                UserId = "user1",
                UserName = "Eric User",
                Mobile = "123456789",
                Birthday = new DateTime(1960, 11, 11),
                FirstCome = new DateTime(2010, 3, 20),
                Title = "Boss1",
                AvatarURI = "VP"
            };

            User user2 = new User()
            {
                UserId = "user2",
                UserName = "Frank User",
                Mobile = "987654321",
                Birthday = new DateTime(1961, 12, 12),
                Title = "Boss2",
                AvatarURI = "VP"
            };

            List<User> users1 = new List<User>();
            users1.Add(user1);
            users1.Add(user2);
            string json1 = JsonConvert.SerializeObject(users1, Formatting.Indented);
            Assert.IsNotNull(json1);

            System.Diagnostics.Trace.WriteLine(json1);

            List<User> users2 = JsonConvert.DeserializeObject<List<User>>(json1);
            Assert.IsNotNull(users2);
            Assert.AreEqual(users1[0].UserId, users2[0].UserId);
        }
    }
}
