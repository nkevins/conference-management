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
    }
}
