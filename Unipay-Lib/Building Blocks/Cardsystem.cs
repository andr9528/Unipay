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

        public Cardsystem()
        {

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
    }
}
