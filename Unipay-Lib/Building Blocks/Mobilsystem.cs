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

        public Mobilsystem(Merchant merc, Date crd, string address,
            string simnr, string macaddress, string boxname,
            bool status = true, bool de = false,
            bool dn = false, string note = "", Date cld = null)
        {
            Merchant = merc;
            CreationDate = crd;
            CloseingDate = cld;

            Status = status;
            DelayElavon = de;
            DelayNETS = dn;

            MachineAddress = macaddress;
            BoxName = boxname;
            Address = address;
            SimNumber = simnr;
            Note = note;
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

        public string TostringM()
        {
            string output = "";

            output += Merchant.ID + ", ";
            output += ToStringS() + ", ";
            output += Address + ", ";
            output += MachineAddress + ", ";
            output += BoxName;

            return output;
        }
    }
}
