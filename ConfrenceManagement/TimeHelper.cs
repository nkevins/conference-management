using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfrenceManagement
{
    public class TimeHelper
    {
        public static string FormatMinutesToTime(int minutes)
        {
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

            return hourPart.ToString("D2") + ":" + minutePart.ToString("D2") + " " + suffix;
        }
    }
}
