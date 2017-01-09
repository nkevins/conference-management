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
            Assert.AreEqual("00:00AM", TimeHelper.FormatMinutesToTime(0));
            Assert.AreEqual("11:59PM", TimeHelper.FormatMinutesToTime(1439));

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
            Assert.AreEqual("11:59AM", TimeHelper.FormatMinutesToTime(719));
            Assert.AreEqual("12:00PM", TimeHelper.FormatMinutesToTime(720));
            Assert.AreEqual("12:01PM", TimeHelper.FormatMinutesToTime(721));
        }

        [TestMethod]
        public void TestValidValue()
        {
            Assert.AreEqual("07:30AM", TimeHelper.FormatMinutesToTime(450));
            Assert.AreEqual("07:30PM", TimeHelper.FormatMinutesToTime(1170));
        }

        [TestMethod]
        public void TestConvertDurationToMinutes()
        {
            Assert.AreEqual(5, TimeHelper.ConvertDurationToMinutes("lightning"));
            Assert.AreEqual(30, TimeHelper.ConvertDurationToMinutes("30min"));

            try
            {
                int result = TimeHelper.ConvertDurationToMinutes("30 min");
                Assert.Fail();
            } catch (Exception) { }
        }

        [TestMethod]
        public void TestConvertMinutesToDuration()
        {
            Assert.AreEqual("lightning", TimeHelper.ConvertMinutesToDuration(5));
            Assert.AreEqual("60min", TimeHelper.ConvertMinutesToDuration(60));
        }
    }
}
