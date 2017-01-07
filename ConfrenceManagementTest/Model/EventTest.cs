using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConfrenceManagement.Model;

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
            Session morningSession = new Session(Session.SessionType.Morning);
            Event e = new Event("Python", 30);
            morningSession.AddTalkEvent(e);

            Assert.AreEqual("09:00 AM Python - 30", morningSession.events[0].ToString());
        }
    }
}
