using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckInbyFace.CheckIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInbyFace.CheckIn.Tests
{
    [TestClass()]
    public class CheckInManagerTests
    {
        [TestMethod()]
        public void CheckInTest()
        {
            CheckInManager manager = new CheckInManager();
            CheckInManager.CheckInStatusTypes result = manager.CheckIn("user1", false);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Duplicate);

            result = manager.CheckIn("user2", true);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Success);

            result = manager.CheckIn("user3", true);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Unknown);
        }

        [TestMethod()]
        public void SaveToDiskTest()
        {
            CheckInManager manager = new CheckInManager();
            Assert.IsTrue(manager.SaveToDisk());
        }

        [TestMethod()]
        public void CheckInCountTest()
        {
            CheckInManager manager = new CheckInManager();
            Assert.AreEqual(1, manager.CheckInResult.CheckInCount);

            CheckInManager.CheckInStatusTypes result = manager.CheckIn("user1", false);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Duplicate);
            Assert.AreEqual(1, manager.CheckInResult.CheckInCount);

            result = manager.CheckIn("user2", true);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Success);
            Assert.AreEqual(2, manager.CheckInResult.CheckInCount);

            result = manager.CheckIn("user3", true);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Unknown);
            Assert.AreEqual(2, manager.CheckInResult.CheckInCount);
        }

        [TestMethod()]
        public void CheckInPercentTest()
        {
            CheckInManager manager = new CheckInManager();
            Assert.AreEqual(1, manager.CheckInResult.CheckInCount);

            CheckInManager.CheckInStatusTypes result = manager.CheckIn("user1", true);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Duplicate);
            Assert.AreEqual(1, manager.CheckInResult.CheckInCount);

            Assert.AreEqual(50, manager.CheckInResult.CheckInByAIPercent);
            Assert.AreEqual(50, manager.CheckInResult.CheckInByAdminPercent);

            result = manager.CheckIn("user2", true);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Success);
            Assert.AreEqual(2, manager.CheckInResult.CheckInCount);

            Assert.AreEqual(50, manager.CheckInResult.CheckInByAIPercent);
            Assert.AreEqual(50, manager.CheckInResult.CheckInByAdminPercent);

            result = manager.CheckIn("user3", true);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Unknown);
            Assert.AreEqual(2, manager.CheckInResult.CheckInCount);

            Assert.AreEqual(2, manager.CheckInResult.TotalCount);
        }

        [TestMethod()]
        public void FindNearlyUserIdTest()
        {
            CheckInManager manager = new CheckInManager();

            string userA = manager.FindNearlyUserId("user", true);
            Assert.IsTrue(string.IsNullOrEmpty(userA));

            string userB = manager.FindNearlyUserId("user", false);
            Assert.AreEqual("user1", userB);

            string userC = manager.FindNearlyUserId("user1", true);
            Assert.AreEqual("user1", userC);

            string userD = manager.FindNearlyUserId("user2", true);
            Assert.AreEqual("user2", userD);

            string userE = manager.FindNearlyUserId("user1", false);
            Assert.AreEqual("user1", userE);

            string userF = manager.FindNearlyUserId("user2", false);
            Assert.AreEqual("user2", userF);

            string userG = manager.FindNearlyUserId("user3", true);
            Assert.IsTrue(string.IsNullOrEmpty(userG));

            string userH = manager.FindNearlyUserId("user3", false);
            Assert.IsTrue(string.IsNullOrEmpty(userH));
        }
    }
}