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
        private int endTime;
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

                Event lunch = new Event("Lunch", 60);
                events.Add(lunch);
            }
            else
            {
                startTime = 780;
                endTime = 1020;
                this.sessionType = SessionType.Afternoon;

                Event networking = new Event("Networking Event", 60);
                events.Add(networking);
            }

            availableMinutes = endTime - startTime;
        }

        public void AddEvent(Event e)
        {
            events.Insert(events.Count - 1, e);
            availableMinutes -= e.duration;
        }
    }
}
