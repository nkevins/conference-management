using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ConfrenceManagementLogic.Scheduler;
using ConfrenceManagementLogic.Model;

namespace ConfrenceManagementTest
{
    [TestClass]
    public class ConfrenceSchedulerTest
    {
        [TestMethod]
        public void TestOneTrackScenario()
        {
            List<Event> events = new List<Event>();
            events.Add(new Event("Event 1", 45));
            events.Add(new Event("Event 2", 135));
            events.Add(new Event("Event 3", 60));
            ConfrenceScheduler scheduler = new ConfrenceScheduler(events);
            Confrence result = scheduler.ScheduleConfrence();

            Assert.AreEqual(1, result.tracks.Count);
            Assert.AreEqual(2, result.tracks[0].sessions.Count);

            Event lunch = result.tracks[0].sessions[0].GetEvents().Find(x => x.eventType == Event.EventType.Lunch);
            Assert.AreEqual(720, lunch.startTime);

            Event networking = result.tracks[0].sessions[1].GetEvents().Find(x => x.eventType == Event.EventType.Networking);
            Assert.AreEqual(960, networking.startTime);
        }

        [TestMethod]
        public void TestMultipleTrackScenario()
        {
            List<Event> events = new List<Event>();
            events.Add(new Event("Event 1", 180));
            events.Add(new Event("Event 2", 240));
            events.Add(new Event("Event 3", 180));
            events.Add(new Event("Event 4", 210));
            ConfrenceScheduler scheduler = new ConfrenceScheduler(events);
            Confrence result = scheduler.ScheduleConfrence();

            Assert.AreEqual(2, result.tracks.Count);
            Assert.AreEqual(2, result.tracks[0].sessions.Count);
            Assert.AreEqual(2, result.tracks[1].sessions.Count);

            Event firstTrackLunch = result.tracks[0].sessions[0].GetEvents().Find(x => x.eventType == Event.EventType.Lunch);
            Assert.AreEqual(720, firstTrackLunch.startTime);

            Event firstTrackLNetworking = result.tracks[0].sessions[1].GetEvents().Find(x => x.eventType == Event.EventType.Networking);
            Assert.AreEqual(1020, firstTrackLNetworking.startTime);

            Event secondTrackLunch = result.tracks[1].sessions[0].GetEvents().Find(x => x.eventType == Event.EventType.Lunch);
            Assert.AreEqual(720, secondTrackLunch.startTime);

            Event secondTrackLNetworking = result.tracks[1].sessions[1].GetEvents().Find(x => x.eventType == Event.EventType.Networking);
            Assert.AreEqual(990, secondTrackLNetworking.startTime);
        }

        [TestMethod]
        public void TestOverdurationEvent()
        {
            List<Event> events = new List<Event>();
            events.Add(new Event("Event 1", 280));
            ConfrenceScheduler scheduler = new ConfrenceScheduler(events);

            try
            {
                Confrence result = scheduler.ScheduleConfrence();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Duration is greater than available slot"));
            }
        }
    }
}
