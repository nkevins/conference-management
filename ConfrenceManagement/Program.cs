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

            // Read input file
            InputProcessor inputProcessor = new InputProcessor();
            bool validFile = false;
            string inputFileLocation = "";

            while (!validFile)
            {
                Console.Write("Enter input file path: ");
                inputFileLocation = Console.ReadLine().Trim();

                if (inputFileLocation == "" || !File.Exists(inputFileLocation))
                {
                    Console.WriteLine("Invalid file location, please try again");
                    continue;
                }

                List<string> fileContents = File.ReadAllLines(inputFileLocation).ToList();

                if (fileContents.Count == 0)
                {
                    Console.WriteLine("File content is empty");
                    continue;
                }

                try
                {
                    events = inputProcessor.ParseLines(File.ReadAllLines(inputFileLocation).ToList());
                    validFile = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
            

            // Proccess event assignment
            ConfrenceScheduler scheduler = new ConfrenceScheduler(events);
            Confrence result = scheduler.ScheduleConfrence();

            // Print result
            int trackNo = 1;
            foreach (Track t in result.tracks)
            {
                Console.WriteLine("Track " + trackNo + ":");
                foreach (Session s in t.sessions.OrderBy(x => x.sessionType))
                {
                    foreach (Event e in s.events)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                trackNo++;

                Console.WriteLine("");
            }

            Console.ReadLine();
        }
    }
}
