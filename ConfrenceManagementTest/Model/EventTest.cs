using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConfrenceManagementLogic.Model;

namespace ConfrenceManagementTest.Model
{
    [TestClass]
    public class EventTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Event e = new Event("Python", 60);
            Assert.AreEqual("Python", e.title);
            Assert.AreEqual(60, e.duration);
            Assert.AreEqual(Event.EventType.Talk, e.eventType);
        }

        [TestMethod]
        public void TestEventDuration()
        {
            try
            {
                Event e = new Event("Python", 42);
                Assert.Fail();
            } catch (Exception) { }
        }

        [TestMethod]
        public void TestToString()
        {
            Event e = new Event("Python", 30, Event.EventType.Talk, 540);
            Assert.AreEqual("09:00 AM Python 30min", e.ToString());

            e = new Event("Lunch", 30, Event.EventType.Lunch, 720);
            Assert.AreEqual("12:00 PM Lunch", e.ToString());

            e = new Event("Networking Event", 30, Event.EventType.Networking, 960);
            Assert.AreEqual("04:00 PM Networking Event", e.ToString());
        }
    }
}
