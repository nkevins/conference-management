using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConfrenceManagementLogic.Model;

namespace ConfrenceManagementTest.Model
{
    [TestClass]
    public class TrackTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Track t = new Track();
            Assert.IsNotNull(t.sessions);
            Assert.AreEqual(0, t.sessions.Count);
        }
    }
}
