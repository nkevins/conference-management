using ConfrenceManagement.Input;
using ConfrenceManagementLogic.Model;
using ConfrenceManagementLogic.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfrenceManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Event> events = new List<Event>();

            // Read input file
            IFileHandler fileHandler = new FileHandler();
            bool isFileValid = false;

            while (!isFileValid)
            {
                Console.Write("Enter input file path: ");
                string inputFileLocation = Console.ReadLine().Trim();

                IInputReader inputReader = new FileInputReader(fileHandler, inputFileLocation);

                try
                {
                    events = inputReader.ReadInput();
                    isFileValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // Proccess event assignment
            IConfrenceScheduler scheduler = new ConfrenceScheduler(events);
            Confrence confrence;
            try
            {
                confrence = scheduler.ScheduleConfrence();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            // Print result
            int trackNo = 1;
            foreach (Track track in confrence.tracks)
            {
                Console.WriteLine("Track " + trackNo + ":");
                foreach (Session session in track.sessions.OrderBy(x => x.sessionType))
                {
                    foreach (Event e in session.GetEvents())
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
