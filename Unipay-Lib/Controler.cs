using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unipay_Lib.Building_Blocks;

namespace Unipay_Lib
{
    public class Controler
    {
        List<Mobilsystem> mobilsystems = new List<Mobilsystem>();
        List<Cardsystem> cardsystems = new List<Cardsystem>();
        List<Merchant> merchants = new List<Merchant>();

        Repository repo = Repository.GetRepository();

        public void NewMobil(int merchantIndex, string DNETS, string DElavon, string status,
            string address, string simnr, string note, string MACAddress, string BoxName, string[] CrD, string[] ClD)
        {
            UpdateInternalLists();

            bool BDNETS = false;
            bool BDElavon = false;
            bool Bstatus = true;
            Date CrDate = ConvertToDate(CrD);
            Date ClDate = ConvertToDate(ClD);
            Merchant merchant = merchants[merchantIndex];

            if (DNETS == "Forsinket")
            {
                BDNETS = true;
            }
            if (DElavon == "Forsinket")
            {
                BDElavon = true;
            }
            if (status != "Aktiv")
            {
                Bstatus = false;
            }

            Mobilsystem mobil = new Mobilsystem(merchant, CrDate, address, simnr, MACAddress,
                BoxName, Bstatus, BDElavon, BDNETS, note, ClDate);

            mobilsystems.Add(mobil);
            repo.GetMobilLists(mobilsystems);
        }

        public void NewMobil(Merchant merchant, string DNETS, string DElavon, string status,
            string address, string simnr, string note, string MACAddress, string BoxName, string[] CrD, string[] ClD)
        {
            UpdateInternalLists();

            bool BDNETS = false;
            bool BDElavon = false;
            bool Bstatus = true;
            Date CrDate = ConvertToDate(CrD);
            Date ClDate = ConvertToDate(ClD);

            if (DNETS == "Forsinket")
            {
                BDNETS = true;
            }
            if (DElavon == "Forsinket")
            {
                BDElavon = true;
            }
            if (status != "Aktiv")
            {
                Bstatus = false;
            }

            Mobilsystem mobil = new Mobilsystem(merchant, CrDate, address, simnr, MACAddress,
                BoxName, Bstatus, BDElavon, BDNETS, note, ClDate);

            mobilsystems.Add(mobil);
            repo.GetMobilLists(mobilsystems);
        }



        public void NewMobilAndMerc(string[] merchantData, string DNETS, string DElavon, string status,
            string address, string simnr, string note, string MACAddress, string BoxName, string[] CrD, string[] ClD)
        {
            UpdateInternalLists();

            bool BDNETS = false;
            bool BDElavon = false;
            bool Bstatus = true;
            Date CrDate = ConvertToDate(CrD);
            Date ClDate = ConvertToDate(ClD);
            Merchant merchant = new Merchant(merchantData[0], merchantData[1], merchantData[2], merchantData[3], merchantData[4]);

            if (DNETS == "Forsinket")
            {
                BDNETS = true;
            }
            if (DElavon == "Forsinket")
            {
                BDElavon = true;
            }
            if (status != "Aktiv")
            {
                Bstatus = false;
            }

            Mobilsystem mobil = new Mobilsystem(merchant, CrDate, address, simnr, MACAddress,
                BoxName, Bstatus, BDElavon, BDNETS, note, ClDate);

            mobilsystems.Add(mobil);
            merchants.Add(merchant);

            repo.GetMobilLists(mobilsystems);
            repo.GetMercLists(merchants);
        }

        public void NewCard(int merchantIndex, string DCPI, string DElavon, string status,
            string address, string simnr, string note, string terminalID, string physcialID, string[] CrD, string[] ClD)
        {
            UpdateInternalLists();

            bool BDCPI = false;
            bool BDElavon = false;
            bool Bstatus = true;
            Date CrDate = ConvertToDate(CrD);
            Date ClDate = ConvertToDate(ClD);
            Merchant merchant = merchants[merchantIndex];

            if (DCPI == "Forsinket")
            {
                BDCPI = true;
            }
            if (DElavon == "Forsinket")
            {
                BDElavon = true;
            }
            if (status != "Aktiv")
            {
                Bstatus = false;
            }

            Cardsystem card = new Cardsystem(merchant, CrDate, address, simnr, terminalID,
                physcialID, Bstatus, BDElavon, BDCPI, note, ClDate);

            cardsystems.Add(card);
            repo.GetCardLists(cardsystems);
        }

        public void NewCard(Merchant merchant, string DCPI, string DElavon, string status,
            string address, string simnr, string note, string terminalID, string physcialID, string[] CrD, string[] ClD)
        {
            UpdateInternalLists();

            bool BDCPI = false;
            bool BDElavon = false;
            bool Bstatus = true;
            Date CrDate = ConvertToDate(CrD);
            Date ClDate = ConvertToDate(ClD);

            if (DCPI == "Forsinket")
            {
                BDCPI = true;
            }
            if (DElavon == "Forsinket")
            {
                BDElavon = true;
            }
            if (status != "Aktiv")
            {
                Bstatus = false;
            }

            Cardsystem card = new Cardsystem(merchant, CrDate, address, simnr, terminalID,
                physcialID, Bstatus, BDElavon, BDCPI, note, ClDate);

            cardsystems.Add(card);

            repo.GetCardLists(cardsystems);
            repo.GetMercLists(merchants);
        }
        public void Delete(int what, int where)
        {
            UpdateInternalLists();

            if (what == 0)
            {
                mobilsystems.RemoveAt(where);
            }
            else if (what == 1)
            {
                cardsystems.RemoveAt(where);
            }
            else if (what == 2)
            {
                merchants.RemoveAt(where);
            }

            repo.GetMobilLists(mobilsystems);
            repo.GetCardLists(cardsystems);
            repo.GetMercLists(merchants);
        }

        public void NewCardAndMerc(string[] merchantData, string DCPI, string DElavon, string status,
            string address, string simnr, string note, string terminalID, string physcialID, string[] CrD, string[] ClD)
        {
            UpdateInternalLists();

            bool BDCPI = false;
            bool BDElavon = false;
            bool Bstatus = true;
            Date CrDate = ConvertToDate(CrD);
            Date ClDate = ConvertToDate(ClD);
            Merchant merchant = new Merchant(merchantData[0], merchantData[1], merchantData[2], merchantData[3], merchantData[4]);

            if (DCPI == "Forsinket")
            {
                BDCPI = true;
            }
            if (DElavon == "Forsinket")
            {
                BDElavon = true;
            }
            if (status != "Aktiv")
            {
                Bstatus = false;
            }

            Cardsystem card = new Cardsystem(merchant, CrDate, address, simnr, terminalID,
                physcialID, Bstatus, BDElavon, BDCPI, note, ClDate);

            cardsystems.Add(card);
            merchants.Add(merchant);

            repo.GetCardLists(cardsystems);
            repo.GetMercLists(merchants);
        }

        public void NewMerc(string[] data)
        {
            UpdateInternalLists();

            Merchant merchant = new Merchant(data[0], data[1], data[2], data[3], data[4]);

            merchants.Add(merchant);
            repo.GetMercLists(merchants);
        }

        private Date ConvertToDate(string[] arrayDate)
        {
            int day = 0;
            int month = 0;
            int year = 0;

            bool dayParse = int.TryParse(arrayDate[0], out day);
            bool monthParse = int.TryParse(arrayDate[1], out month);
            bool yearParse = int.TryParse(arrayDate[2], out year);

            Date returnDate = new Date(day, month, year);

            return returnDate;
        }
        private void UpdateInternalLists()
        {
            mobilsystems = repo.GetMobilsystems();
            cardsystems = repo.GetCardsystems();
            merchants = repo.GetMerchants();
        }
    }
}
