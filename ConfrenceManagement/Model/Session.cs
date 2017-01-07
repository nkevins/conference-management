using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagement.Model
{
    public class Session
    {
        public enum SessionType
        {
            Morning = 1,
            Afternoon = 2
        }

        public List<Event> events { get; }
        public int startTime { get; }
        public int endTime { get; }
        public SessionType sessionType { get; }
        public int availableMinutes { private set; get; }
        
        public Session(SessionType sessionType)
        {
            events = new List<Event>();

            if (sessionType == SessionType.Morning)
            {
                startTime = 540;
                endTime = 720;
                this.sessionType = SessionType.Morning;

                Event lunch = new Event("Lunch", 0, Event.EventType.Lunch);
                events.Add(lunch);
            }
            else
            {
                startTime = 780;
                endTime = 1020;
                this.sessionType = SessionType.Afternoon;

                Event networking = new Event("Networking Event", 0, Event.EventType.Networking);
                events.Add(networking);
            }

            availableMinutes = endTime - startTime;
        }

        public void AddTalkEvent(Event e)
        {
            if (e.eventType != Event.EventType.Talk)
            {
                throw new ApplicationException("Only Event Type Talk is allowed to be added");
            }

            if (e.duration > availableMinutes)
            {
                throw new ApplicationException("Not enough duration in current session");
            }

            int eventStartTime = endTime - availableMinutes;
            Event eventToBeInserted = new Event(e.title, e.duration, Event.EventType.Talk, eventStartTime);
            events.Insert(events.Count - 1, eventToBeInserted);
            availableMinutes -= e.duration;

            // Shift Networking time for afternoon session if talk session end after 4 PM
            if (sessionType == SessionType.Afternoon && availableMinutes < 60)
            {
                Event networkingEvent = events.Find(x => x.eventType == Event.EventType.Networking);
                Event newNetworkingEvent = new Event(networkingEvent.title, networkingEvent.duration, Event.EventType.Networking, endTime - availableMinutes);
                events.Remove(networkingEvent);
                events.Add(newNetworkingEvent);
            }
        }
    }
}
