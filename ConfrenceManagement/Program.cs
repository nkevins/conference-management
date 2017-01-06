using ConfrenceManagement.Helper;
using ConfrenceManagement.Input;
using ConfrenceManagement.Model;
using ConfrenceManagement.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
namespace ConfrenceManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Event> events = new List<Event>();
            InputProcessor inputProcessor = new InputProcessor();
            bool validFile = false;
            string inputFileLocation = "";

            while (!validFile)
            {
                Console.Write("Enter input file path: ");
                inputFileLocation = Console.ReadLine().Trim();

                if (inputFileLocation != "" && File.Exists(inputFileLocation))
                {
                    validFile = true;
                }
                else
                {
                    Console.WriteLine("Invalid file location, please try again");
                }
            }
            events = inputProcessor.ParseLines(File.ReadAllLines(inputFileLocation).ToList());

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

                Console.WriteLine("");
            }

            Console.ReadLine();
        }
    }
}
