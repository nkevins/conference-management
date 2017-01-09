using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConfrenceManagementLogic.Model;

namespace ConfrenceManagementTest.Model
{
    [TestClass]
    public class SessionTest
    {
        private Session morningSession;
        private Session afternoonSession;

        [TestInitialize()]
        public void Initialize()
        {
            morningSession = new Session(Session.SessionType.Morning, 540, 720);
            afternoonSession = new Session(Session.SessionType.Afternoon, 780, 1020);
        }

        [TestMethod]
        public void TestConstructor()
        {
            Assert.AreEqual(Session.SessionType.Morning, morningSession.sessionType);
            Assert.AreEqual(180, morningSession.availableSlotMinutes);
            Assert.AreEqual(540, morningSession.startTime);
            Assert.AreEqual(720, morningSession.endTime);
            Assert.IsNotNull(morningSession.GetEvents());
            Assert.AreEqual(0, morningSession.GetEvents().Count);

            Assert.AreEqual(Session.SessionType.Afternoon, afternoonSession.sessionType);
            Assert.AreEqual(240, afternoonSession.availableSlotMinutes);
            Assert.AreEqual(780, afternoonSession.startTime);
            Assert.AreEqual(1020, afternoonSession.endTime);
            Assert.IsNotNull(afternoonSession.GetEvents());
            Assert.AreEqual(0, afternoonSession.GetEvents().Count);
        }

        [TestMethod]
        public void TestAddInvalidTalkEventType()
        {
            Event lunch = new Event("Lunch", 0, Event.EventType.Lunch);
            Event networking = new Event("Networking Event", 0, Event.EventType.Networking); ;

            try
            {
                morningSession.AddTalkEvent(lunch);
                Assert.Fail();
            } catch (Exception) { }

            try
            {
                afternoonSession.AddTalkEvent(networking);
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void TestAddValidTalkEvent()
        {
            Event e = new Event("Python", 60);

            Assert.AreEqual(0, morningSession.GetEvents().Count);

            try
            {
                morningSession.AddTalkEvent(e);
            }
            catch (Exception) { Assert.Fail(); }

            Assert.AreEqual(1, morningSession.GetEvents().Count);
            Assert.AreEqual(120, morningSession.availableSlotMinutes);
            Assert.AreEqual(540, morningSession.GetEvents()[0].startTime);

            Event e2 = new Event(".NET", 30);

            try
            {
                morningSession.AddTalkEvent(e2);
            }
            catch (Exception) { Assert.Fail(); }

            Assert.AreEqual(2, morningSession.GetEvents().Count);
            Assert.AreEqual(90, morningSession.availableSlotMinutes);
            Assert.AreEqual(600, morningSession.GetEvents()[1].startTime);
        }

        [TestMethod]
        public void TestAddInvalidNonTalkEventType()
        {
            Event e = new Event("Python", 60);

            try
            {
                morningSession.AddNonTalkEvent(e);
                Assert.Fail();
            }
            catch (Exception) { }

            try
            {
                afternoonSession.AddNonTalkEvent(e);
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void TestAddValidNonTalkEventType()
        {
            Event networking = new Event("Networking Event", 0, Event.EventType.Networking, 990);

            Assert.AreEqual(0, afternoonSession.GetEvents().Count);

            try
            {
                afternoonSession.AddNonTalkEvent(networking);
            }
            catch (Exception) { Assert.Fail(); }

            Assert.AreEqual(1, afternoonSession.GetEvents().Count);
            Assert.AreEqual(240, afternoonSession.availableSlotMinutes);
            Assert.AreEqual(990, afternoonSession.GetEvents()[0].startTime);
        }

        [TestMethod]
        public void TestAddOverDurationEvent()
        {
            Event e = new Event("Java", 185);

            try
            {
                morningSession.AddTalkEvent(e);
                Assert.Fail();
            }
            catch (Exception) { }


            e = new Event("Java", 245);

            try
            {
                afternoonSession.AddTalkEvent(e);
                Assert.Fail();
            }
            catch (Exception) { }
        }
    }
}
