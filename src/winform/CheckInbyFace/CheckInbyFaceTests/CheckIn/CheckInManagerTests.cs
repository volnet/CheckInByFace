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
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Success);

            result = manager.CheckIn("user3", true);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Unknown);
        }

        [TestMethod()]
        public void SaveToDiskTest()
        {
            CheckInManager manager = new CheckInManager();
            CheckInManager.CheckInStatusTypes result = manager.CheckIn("user1", false);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Success);

            result = manager.CheckIn("user3", true);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Unknown);

            Assert.IsTrue(manager.SaveToDisk());
        }

        [TestMethod()]
        public void CheckInCountTest()
        {
            CheckInManager manager = new CheckInManager();
            Assert.AreEqual(0, manager.CheckInCount);

            CheckInManager.CheckInStatusTypes result = manager.CheckIn("user1", false);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Success);
            Assert.AreEqual(1, manager.CheckInCount);

            result = manager.CheckIn("user3", true); 
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Unknown);
            Assert.AreEqual(1, manager.CheckInCount);
        }

        [TestMethod()]
        public void CheckInPercentTest()
        {
            CheckInManager manager = new CheckInManager();
            Assert.AreEqual(0, manager.CheckInCount);

            CheckInManager.CheckInStatusTypes result = manager.CheckIn("user1", true);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Success);
            Assert.AreEqual(1, manager.CheckInCount);

            result = manager.CheckIn("user2", false);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Success);
            Assert.AreEqual(2, manager.CheckInCount);

            Assert.AreEqual(50, manager.CheckInByAIPercent);
            Assert.AreEqual(50, manager.CheckInByAdminPercent);

            result = manager.CheckIn("user3", true);
            Assert.AreEqual(result, CheckInManager.CheckInStatusTypes.Unknown);
            Assert.AreEqual(2, manager.CheckInCount);

            Assert.AreEqual(2, manager.TotalCount);
        }
    }
}