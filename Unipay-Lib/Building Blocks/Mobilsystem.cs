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
        // might be able to be unified with SimProducer from cardsystem in basesystem
        public string SimNumber { get; set; }

        public Mobilsystem()
        {
            
        }
    }
}
