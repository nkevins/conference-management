using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfrenceManagementLogic.Model;
using ConfrenceManagementLogic.Helper;

namespace ConfrenceManagement.Input
{
    public class FileInputReader : IInputReader
    {
        private IFileHandler fileHandler;
        private string path;

        public FileInputReader(IFileHandler fileHandler, string path)
        {
            this.fileHandler = fileHandler;
            this.path = path;
        }

        public List<Event> ReadInput()
        {
            // Validate input file
            if (!fileHandler.FileExist(path))
            {
                throw new ApplicationException("Invalid file path");
            }

            List<string> input = fileHandler.ReadFile(path);
            if (input.Count == 0)
            {
                throw new ApplicationException("File content is empty");
            }

            // Parse input
            List<Event> events = new List<Event>();

            foreach (string line in input)
            {
                string title = "";
                int duration = 0;

                if (line.Trim() == "")
                {
                    continue;
                }

                List<string> words = line.Split().ToList();

                try
                {
                    duration = TimeHelper.ConvertDurationToMinutes(words.Last().Trim());
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message + ": " + line);
                }


                words.RemoveAt(words.Count - 1);
                title = words.Aggregate((i, j) => i + " " + j);

                try
                {
                    events.Add(new Event(title, duration));
                }
                catch (Exception ex)
                {
                    throw new ApplicationException(ex.Message + ": " + line, ex);
                }
            }

            return events;
        }
    }
}
