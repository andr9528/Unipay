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
        public string Note { get; set; }

        public Merchant(string id, string name, string firm, string mail, string note = "")
        {
            ID = id;
            Name = name;
            Firm = firm;
            Mail = mail;
            Note = note;
        }

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
