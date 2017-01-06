using ConfrenceManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ConfrenceManagement.Input
{
    public class FileInputReader
    {
        private List<Event> events;
        private string fileDirectory;

        public FileInputReader(string fileDirectory)
        {
            events = new List<Event>();
            this.fileDirectory = fileDirectory;
        }

        public List<Event> ReadInput()
        {
            string[] lines = File.ReadAllLines(fileDirectory);

            foreach (string line in lines)
            {
                events.Add(ParseLine(line));
            }

            return events;
        }

        private Event ParseLine(string input)
        {
            string title = "";
            int duration = 0;

            if (input.EndsWith(" lightning"))
            {
                title = input.Substring(0, input.Length - 10);
                duration = 5;

                return new Event(title, duration);
            }

            string pattern = @"^([^0-9]+)(\d+)min";
            Regex r = new Regex(pattern);
            Match m = r.Match(input);

            if (!m.Success)
            {
                throw new ApplicationException("Invalid input format: " + input);
            }

            title = m.Groups[1].Value.Trim();
            duration = int.Parse(m.Groups[2].Value.Trim());

            return new Event(title, duration);
        }
    }
}
