using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConfrenceManagementLogic.Helper
{
    public class TimeHelper
    {
        public static string FormatMinutesToTime(int minutes)
        {
            if (minutes < 0 || minutes > 1439)
            {
                throw new ApplicationException("Invalid input value");
            }

            int hourPart = minutes / 60;
            if (hourPart > 12)
            {
                hourPart -= 12;
            }
            int minutePart = minutes % 60;
            string suffix = "";
            if (minutes >= 720)
            {
                suffix = "PM";
            }
            else
            {
                suffix = "AM";
            }            

            return hourPart.ToString("D2") + ":" + minutePart.ToString("D2") + suffix;
        }

        public static int ConvertDurationToMinutes(string duration)
        {
            if (duration == "lightning")
            {
                return 5;
            }
            else
            {
                string pattern = @"(\d+)min";
                Regex r = new Regex(pattern);
                Match m = r.Match(duration);

                if (!m.Success)
                {
                    throw new ApplicationException("Invalid input format");
                }

                return int.Parse(m.Groups[1].Value.Trim());
            }
        }

        public static string ConvertMinutesToDuration(int minutes)
        {
            if (minutes == 5)
            {
                return "lightning";
            }
            else
            {
                return minutes.ToString() + "min";
            }
        }
    }
}
