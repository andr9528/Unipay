using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipay_Lib.Building_Blocks
{
    public class Date
    {
        public string Event { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        

        public Date(int day, int month, int year, string eventName = "")
        {
            Event = eventName;
            Year = year;
            Month = month;
            Day = day;
        }

        public string ToStringDF()
        {
            string output = "";

            if (Day < 10)
            {
                output += "0" + Day + "-";
            }
            else
            {
                output += Day + "-";
            }
            if (Month < 10)
            {
                output += "0" + Month + "-";
            }
            else
            {
                output += Month + "-";
            }
            output += Year;

            return output;
        }
    }
}
