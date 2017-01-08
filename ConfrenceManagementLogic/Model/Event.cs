using ConfrenceManagementLogic.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagementLogic.Model
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
        public int startTime { get; }
        public EventType eventType { get; }

        public Event(string title, int duration, EventType eventType = EventType.Talk, int startTime = 0)
        {
            this.title = title;

            if (duration % 5 != 0)
            {
                throw new Exception("Event duration must be in 5 minutes interval");
            }

            this.duration = duration;
            this.eventType = eventType;
            
            if (eventType == EventType.Lunch)
            {
                this.startTime = 720;
            }
            else if (eventType == EventType.Networking && startTime == 0)
            {
                this.startTime = 960;
            }
            else
            {
                this.startTime = startTime;
            }
        }

        public override string ToString()
        {
            if (eventType != EventType.Talk)
            {
                return TimeHelper.FormatMinutesToTime(startTime) + " " + title;
            }
            else
            {
                return TimeHelper.FormatMinutesToTime(startTime) + " " + title + " " + duration + "min";
            } 
        }
    }
}
