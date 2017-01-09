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

            Assert.IsNotNull(c.tracks);
            Assert.AreEqual(0, c.tracks.Count);
        }
    }
}
