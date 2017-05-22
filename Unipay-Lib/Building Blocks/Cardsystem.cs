using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipay_Lib.Building_Blocks
{
    public class Cardsystem : Basesystem
    {
        // is true while waiting on respone from CPI and false while not waiting.
        public bool DelayCPI { get; set; }
        public string TerminalID { get; set; }
        public string PhysicalID { get; set; }

        public Cardsystem(Merchant merc, Date crd, string address,
            string simnr, string tid, string physid,
            bool status = true, bool de = false,
            bool dc = false, string note = "", Date cld = null)
        {
            Merchant = merc;
            CreationDate = crd;
            CloseingDate = cld;

            Status = status;
            DelayElavon = de;
            DelayCPI = dc;

            TerminalID = tid;
            PhysicalID = physid;
            Address = address;
            SimNumber = simnr;
            Note = note;
        }
        public string ToStringDC()
        {
            if (DelayCPI == true)
            {
                return "Forsinket";
            }
            else
            {
                return "Ikke Forsinkket";
            }
        }

        public string ToStringC()
        {
            string output = "";

            output += Merchant.ID + ", ";
            output += ToStringS() + ", ";
            output += Address + ", ";
            output += TerminalID + ", ";
            output += PhysicalID;

            return output;
        }
    }
}
