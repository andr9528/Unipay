using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipay_Lib.Building_Blocks
{
    public class Basesystem
    {
        public Merchant Merchant { get; set; }
        // is true while it is active and false while it is inactive.
        public bool Status { get; set; }
        // is true while waiting on respone from elavon and false while not waiting.
        public bool DelayElavon { get; set; }
        public Date CreationDate { get; set; }
        public string Address { get; set; }

        public string ToStringDE()
        {
            if (DelayElavon == true)
            {
                return "Forsinket";
            }
            else
            {
                return "Ikke Forsinkket";
            }
        }
        public string ToStringS()
        {
            if (Status == true)
            {
                return "Aktiv";
            }
            else
            {
                return "Inaktiv";
            }
        }

    }
}
