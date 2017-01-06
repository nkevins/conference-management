using ConfrenceManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Event> events = new List<Event>();
            FileInputReader reader = new FileInputReader("input.txt");
            events = reader.ReadInput();

            ConfrenceScheduler scheduler = new ConfrenceScheduler(events);
            Confrence result = scheduler.ScheduleConfrence();

            // Print result
            int trackNo = 1;
            foreach (Track t in result.tracks)
            {
                Console.WriteLine("Track " + trackNo + ":");
                foreach (Session s in t.sessions.OrderBy(x => x.sessionType))
                {
                    int cumulativeMinutes = s.startTime;
                    foreach (Event e in s.events)
                    {
                        Console.WriteLine(TimeHelper.FormatMinutesToTime(cumulativeMinutes) + " " + e.title + " - " + e.duration);
                        cumulativeMinutes += e.duration;
                    }
                }
                trackNo++;

                Console.WriteLine(Environment.NewLine);
            }

            Console.ReadLine();
        }
    }
}
