using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipay_Lib.Building_Blocks
{
    public class Merchant
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Firm { get; set; }
        public string Mail { get; set; }

        public string ToStringM()
        {
            string output = "";

            output += ID + ", ";
            output += Name + ", ";
            output += Firm + ", ";
            output += Mail;

            return output;
        }
    }
}
