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
        List<Mobilsystem> mobilList = new List<Mobilsystem>();
        List<Cardsystem> cardList = new List<Cardsystem>();
        List<Merchant> merchantList = new List<Merchant>();

        Repository repo = Repository.GetRepository();

        public void NewMobil(int merchantIndex, string DNETS, string DElavon, string status,
            string address, string simnr, string note, string MACAddress, string BoxName, string[] CrD, string[] ClD)
        {
            mobilList = repo.GetMobilsystems();
            merchantList = repo.GetMerchants();

            bool BDNETS = false;
            bool BDElavon = false;
            bool Bstatus = true;
            Date CrDate = ConvertToDate(CrD);
            Date ClDate = ConvertToDate(ClD);
            Merchant merchant = merchantList[merchantIndex];

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

            mobilList.Add(mobil);
            repo.GetMobilLists(mobilList);
        }

        

        public void NewMobilAndMerc(string[] merchantData, string DNETS, string DElavon, string status,
            string address, string simnr, string note, string MACAddress, string BoxName, string[] CrD, string[] ClD)
        {
            mobilList = repo.GetMobilsystems();
            merchantList = repo.GetMerchants();

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

            mobilList.Add(mobil);
            merchantList.Add(merchant);

            repo.GetMobilLists(mobilList);
            repo.GetMercLists(merchantList);
        }

        public void NewCard(int merchantIndex, string DCPI, string DElavon, string status,
            string address, string simnr, string note, string terminalID, string physcialID, string[] CrD, string[] ClD)
        {
            cardList = repo.GetCardsystems();
            merchantList = repo.GetMerchants();

            bool BDCPI = false;
            bool BDElavon = false;
            bool Bstatus = true;
            Date CrDate = ConvertToDate(CrD);
            Date ClDate = ConvertToDate(ClD);
            Merchant merchant = merchantList[merchantIndex];

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

            cardList.Add(card);
            merchantList.Add(merchant);

            repo.GetCardLists(cardList);
            repo.GetMercLists(merchantList);
        }

        public void NewCardAndMerc(string[] merchantData, string DCPI, string DElavon, string status,
            string address, string simnr, string note, string terminalID, string physcialID, string[] CrD, string[] ClD)
        {
            cardList = repo.GetCardsystems();
            merchantList = repo.GetMerchants();

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

            cardList.Add(card);
            merchantList.Add(merchant);

            repo.GetCardLists(cardList);
            repo.GetMercLists(merchantList);
        }

        public void NewMerc(string[] data)
        {
            merchantList = repo.GetMerchants();

            Merchant merchant = new Merchant(data[0], data[1], data[2], data[3], data[4]);

            merchantList.Add(merchant);
            repo.GetMercLists(merchantList);
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
    }
}
