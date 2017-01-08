using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConfrenceManagementLogic.Helper;

namespace ConfrenceManagementTest
{
    [TestClass]
    public class TimeHelperTest
    {
        [TestMethod]
        public void TestBoundaryValue()
        {
            Assert.AreEqual("00:00 AM", TimeHelper.FormatMinutesToTime(0));
            Assert.AreEqual("11:59 PM", TimeHelper.FormatMinutesToTime(1439));

            try
            {
                TimeHelper.FormatMinutesToTime(-1);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid input value", ex.Message);
            }

            try
            {
                TimeHelper.FormatMinutesToTime(1440);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid input value", ex.Message);
            }
        }

        [TestMethod]
        public void TestAMPMTransition()
        {
            Assert.AreEqual("11:59 AM", TimeHelper.FormatMinutesToTime(719));
            Assert.AreEqual("12:00 PM", TimeHelper.FormatMinutesToTime(720));
            Assert.AreEqual("12:01 PM", TimeHelper.FormatMinutesToTime(721));
        }

        [TestMethod]
        public void TestValidValue()
        {
            Assert.AreEqual("07:30 AM", TimeHelper.FormatMinutesToTime(450));
            Assert.AreEqual("07:30 PM", TimeHelper.FormatMinutesToTime(1170));
        }
    }
}
