using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagementLogic.Model
{
    public class Session
    {
        public enum SessionType
        {
            Morning = 1,
            Afternoon = 2
        }

        private List<Event> events;
        public int startTime { get; }
        public int endTime { get; }
        public SessionType sessionType { get; }
        public int availableSlotMinutes { private set; get; }
        
        public Session(SessionType sessionType, int startTime, int endTime)
        {
            events = new List<Event>();
            this.sessionType = sessionType;
            this.startTime = startTime;
            this.endTime = endTime;
            availableSlotMinutes = endTime - startTime;
        }

        public List<Event> GetEvents()
        {
            return events;
        }

        public void AddTalkEvent(Event e)
        {
            if (e.eventType != Event.EventType.Talk)
            {
                throw new ApplicationException("Only Event Type Talk is allowed to be added");
            }

            if (e.duration > availableSlotMinutes)
            {
                throw new ApplicationException("Not enough duration in current session");
            }

            int eventStartTime = endTime - availableSlotMinutes;
            Event eventToBeInserted = new Event(e.title, e.duration, Event.EventType.Talk, eventStartTime);
            events.Add(eventToBeInserted);
            availableSlotMinutes -= e.duration;
        }

        public void AddNonTalkEvent(Event e)
        {
            if (e.eventType == Event.EventType.Talk)
            {
                throw new ApplicationException("Only Event Type other than Talk is allowed to be added");
            }

            Event eventToBeInserted = new Event(e.title, e.duration, e.eventType, e.startTime);
            events.Add(eventToBeInserted);
        }
    }
}
