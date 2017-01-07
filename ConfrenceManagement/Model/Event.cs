using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagement.Model
{
    public class Event
    {
        public enum EventType
        {
            Talk = 1,
            Lunch = 2,
            Networking = 3
        }

        public string title { get; set; }
        public int duration { get; }
        public int startTime { get; set; }
        public EventType eventType { get; }

        public Event(string title, int duration, EventType eventType = EventType.Talk)
        {
            this.title = title;
            this.duration = duration;
            this.eventType = eventType;
            
            if (eventType == EventType.Lunch)
            {
                startTime = 720;
            }
            else if (eventType == EventType.Networking)
            {
                startTime = 960;
            }
        }
    }
}
