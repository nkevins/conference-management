using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConfrenceManagementLogic.Model;

namespace ConfrenceManagementTest.Model
{
    [TestClass]
    public class ConfrenceTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Confrence c = new Confrence();

            // Initially 1 track will be created
            Assert.IsNotNull(c.tracks);
            Assert.AreEqual(1, c.tracks.Count);
        }
    }
}
