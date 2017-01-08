using ConfrenceManagement.Input;
using ConfrenceManagementLogic.Model;
using ConfrenceManagementLogic.Scheduler;
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
            bool isFileValid = false;
            string inputFileLocation = "";

            while (!isFileValid)
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
                    events = inputProcessor.ParseInput(File.ReadAllLines(inputFileLocation).ToList());
                    isFileValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
            

            // Proccess event assignment
            ConfrenceScheduler scheduler = new ConfrenceScheduler(events);
            Confrence confrence = scheduler.ScheduleConfrence();

            // Print result
            int trackNo = 1;
            foreach (Track track in confrence.tracks)
            {
                Console.WriteLine("Track " + trackNo + ":");
                foreach (Session session in track.sessions.OrderBy(x => x.sessionType))
                {
                    foreach (Event e in session.events)
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
