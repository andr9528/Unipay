using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unipay_Lib.Building_Blocks
{
    public class Mobilsystem : Basesystem
    {
        public bool DelayNETS { get; set; }
        public string MachineAddress { get; set; }
        public string BoxName { get; set; }

        public Mobilsystem()
        {
            
        }
        public string ToStringDN()
        {
            if (DelayNETS == true)
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
