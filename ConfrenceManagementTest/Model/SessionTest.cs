using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConfrenceManagementLogic.Model;

namespace ConfrenceManagementTest.Model
{
    [TestClass]
    public class SessionTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Session morningSession = new Session(Session.SessionType.Morning);
            Assert.AreEqual(180, morningSession.availableSlotMinutes);
            Assert.AreEqual(540, morningSession.startTime);
            Assert.AreEqual(720, morningSession.endTime);
            Assert.IsNotNull(morningSession.events);
            Assert.AreEqual(1, morningSession.events.Count);
            Assert.AreEqual(1, morningSession
                .events.FindAll(x => x.eventType == Event.EventType.Lunch).Count);
            Event lunch = morningSession
                .events.Find(x => x.eventType == Event.EventType.Lunch);
            Assert.AreEqual("Lunch", lunch.title);
            Assert.AreEqual(Event.EventType.Lunch, lunch.eventType);
            Assert.AreEqual(720, lunch.startTime);


            Session afternoonSession = new Session(Session.SessionType.Afternoon);
            Assert.AreEqual(240, afternoonSession.availableSlotMinutes);
            Assert.AreEqual(780, afternoonSession.startTime);
            Assert.AreEqual(1020, afternoonSession.endTime);
            Assert.IsNotNull(afternoonSession.events);
            Assert.AreEqual(1, afternoonSession.events.Count);
            Assert.AreEqual(1, afternoonSession
                .events.FindAll(x => x.eventType == Event.EventType.Networking).Count);
            Event networking = afternoonSession
                .events.Find(x => x.eventType == Event.EventType.Networking);
            Assert.AreEqual("Networking Event", networking.title);
            Assert.AreEqual(Event.EventType.Networking, networking.eventType);
            Assert.AreEqual(960, networking.startTime);
        }

        [TestMethod]
        public void TestAddInvalidEventType()
        {
            Session morningSession = new Session(Session.SessionType.Morning);
            Event lunch = new Event("Lunch", 0, Event.EventType.Lunch);
            Event networking = new Event("Lunch", 0, Event.EventType.Networking); ;

            try
            {
                morningSession.AddTalkEvent(lunch);
                Assert.Fail();
            } catch (Exception) { }

            try
            {
                morningSession.AddTalkEvent(networking);
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void TestAddValidEventToMorningSession()
        {
            Session morningSession = new Session(Session.SessionType.Morning);

            Assert.AreEqual(1, morningSession.events.Count);

            Event e = new Event("Java", 180);

            try
            {
                morningSession.AddTalkEvent(e);
            }
            catch (Exception) { Assert.Fail(); }

            Assert.AreEqual(2, morningSession.events.Count);
            Assert.AreEqual(0, morningSession.availableSlotMinutes);
            Assert.AreEqual(540, morningSession.events[0].startTime);


            morningSession = new Session(Session.SessionType.Morning);

            Assert.AreEqual(1, morningSession.events.Count);

            e = new Event("Python", 60);

            try
            {
                morningSession.AddTalkEvent(e);
            }
            catch (Exception) { Assert.Fail(); }

            Assert.AreEqual(2, morningSession.events.Count);
            Assert.AreEqual(120, morningSession.availableSlotMinutes);
            Assert.AreEqual(540, morningSession.events[0].startTime);

            Event e2 = new Event(".NET", 30);

            try
            {
                morningSession.AddTalkEvent(e2);
            }
            catch (Exception) { Assert.Fail(); }

            Assert.AreEqual(3, morningSession.events.Count);
            Assert.AreEqual(90, morningSession.availableSlotMinutes);
            Assert.AreEqual(600, morningSession.events[1].startTime);
        }

        [TestMethod]
        public void TestAddValidEventToAfternoonSession()
        {
            Session afternoonSession = new Session(Session.SessionType.Afternoon);

            Assert.AreEqual(1, afternoonSession.events.Count);

            Event e = new Event("Java", 240);

            try
            {
                afternoonSession.AddTalkEvent(e);
            }
            catch (Exception) { Assert.Fail(); }

            Assert.AreEqual(2, afternoonSession.events.Count);
            Assert.AreEqual(0, afternoonSession.availableSlotMinutes);
            Assert.AreEqual(780, afternoonSession.startTime);


            afternoonSession = new Session(Session.SessionType.Afternoon);

            Assert.AreEqual(1, afternoonSession.events.Count);

            e = new Event("Python", 60);

            try
            {
                afternoonSession.AddTalkEvent(e);
            }
            catch (Exception) { Assert.Fail(); }

            Assert.AreEqual(2, afternoonSession.events.Count);
            Assert.AreEqual(180, afternoonSession.availableSlotMinutes);
            Assert.AreEqual(780, afternoonSession.startTime);

            Event e2 = new Event(".NET", 30);

            try
            {
                afternoonSession.AddTalkEvent(e2);
            }
            catch (Exception) { Assert.Fail(); }

            Assert.AreEqual(3, afternoonSession.events.Count);
            Assert.AreEqual(150, afternoonSession.availableSlotMinutes);
            Assert.AreEqual(840, afternoonSession.events[1].startTime);
        }

        [TestMethod]
        public void TestAddOverDurationEvent()
        {
            Session morningSession = new Session(Session.SessionType.Morning);
            Event e = new Event("Java", 185);

            try
            {
                morningSession.AddTalkEvent(e);
                Assert.Fail();
            }
            catch (Exception) { }


            Session afternoonSession = new Session(Session.SessionType.Afternoon);
            e = new Event("Java", 245);

            try
            {
                afternoonSession.AddTalkEvent(e);
                Assert.Fail();
            }
            catch (Exception) { }
        }

        [TestMethod]
        public void TestNetworkingSessionTimeShift()
        {
            Session afternoonSession = new Session(Session.SessionType.Afternoon);
            Assert.AreEqual(1, afternoonSession.events.Count);
            Event e = new Event("Java", 185);

            try
            {
                afternoonSession.AddTalkEvent(e);
            }
            catch (Exception) { Assert.Fail(); }

            Assert.AreEqual(2, afternoonSession.events.Count);
            Assert.AreEqual(55, afternoonSession.availableSlotMinutes);

            Event networkingEvent = afternoonSession.events.Find(x => x.eventType == Event.EventType.Networking);
            Assert.AreEqual(965, networkingEvent.startTime);

            Event e2 = new Event("Ruby on Rails", 30);
            try
            {
                afternoonSession.AddTalkEvent(e2);
            }
            catch (Exception) { Assert.Fail(); }

            Assert.AreEqual(3, afternoonSession.events.Count);
            Assert.AreEqual(25, afternoonSession.availableSlotMinutes);

            networkingEvent = afternoonSession.events.Find(x => x.eventType == Event.EventType.Networking);
            Assert.AreEqual(995, networkingEvent.startTime);
        }
    }
}
