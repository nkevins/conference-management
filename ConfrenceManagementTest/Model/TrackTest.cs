using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConfrenceManagement.Model;

namespace ConfrenceManagementTest.Model
{
    [TestClass]
    public class TrackTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            // Initally each track will have morning and afternoon session
            Track t = new Track();
            Assert.IsNotNull(t.sessions);
            Assert.AreEqual(2, t.sessions.Count);
            Assert.AreEqual(1, t.sessions.FindAll(x => x.sessionType == Session.SessionType.Morning).Count);
            Assert.AreEqual(1, t.sessions.FindAll(x => x.sessionType == Session.SessionType.Afternoon).Count);
        }
    }
}
