using ConfrenceManagementLogic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConfrenceManagement.Input
{
    public class InputProcessor
    {
        public List<Event> ParseInput(List<string> input)
        {
            List<Event> events = new List<Event>();

            foreach (string line in input)
            {
                string title = "";
                int duration = 0;

                if (line.EndsWith(" lightning"))
                {
                    title = line.Substring(0, line.Length - 10);
                    duration = 5;

                    events.Add(new Event(title, duration));
                }
                else
                {
                    string pattern = @"^([^0-9]+)(\d+)min";
                    Regex r = new Regex(pattern);
                    Match m = r.Match(line);

                    if (!m.Success)
                    {
                        throw new ApplicationException("Invalid input format: " + line);
                    }

                    title = m.Groups[1].Value.Trim();
                    duration = int.Parse(m.Groups[2].Value.Trim());

                    try
                    {
                        events.Add(new Event(title, duration));
                    }
                    catch (Exception ex)
                    {
                        throw new ApplicationException(ex.Message + ": " + line, ex);
                    }
                    
                }                
            }

            return events;     
        }
    }
}
