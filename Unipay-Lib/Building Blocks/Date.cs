using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipay_Lib.Building_Blocks
{
    public class Date
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }

        public Date(int day, int month, int year, int minute = 0, int hour = 0 )
        {
            Year = year;
            Month = month;
            Day = day;
            Hour = hour;
            Minute = minute;
        }
        public string ToStringHF() // hour format
        {
            string output = "";

            if (Hour < 10)
            {
                output += "0" + Hour + ":";
            }
            else
            {
                output += Hour + ":";
            }

            if (Minute < 10)
            {
                output += "0" + Minute;
            }
            else
            {
                output += Minute;
            }
            output += " ";

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
                output += Day + "-";
            }
            output += Year;

            return output;
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
                output += Day + "-";
            }
            output += Year;

            return output;
        }
    }
}
