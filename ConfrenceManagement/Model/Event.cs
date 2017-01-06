using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagement.Model
{
    public class Event
    {
        public string title { get; set; }
        public int duration { get; set; }

        public Event(string title, int duration)
        {
            this.title = title;
            this.duration = duration;
        }
    }
}
