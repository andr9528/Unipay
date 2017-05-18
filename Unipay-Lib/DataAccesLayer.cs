﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Unipay_Lib.Building_Blocks;
using System.Reflection;


namespace Unipay_Lib
{
    public class DataAccesLayer
    {
        Repository repo = Repository.GetRepository();

        char toText = '\'';

        List<Mobilsystem> mobilsystems = new List<Mobilsystem>();
        List<Cardsystem> cardsystems = new List<Cardsystem>();
        List<Merchant> merchants = new List<Merchant>();

        string backup = "Datafile.xls";
        
        public DataAccesLayer()
        {
            
        }

        public void ImportBackup()
        {
            string path = Path.Combine(Environment.CurrentDirectory, backup);
            int sheet = 0;

            Excel.Application xlApp = new Excel.Application();
            xlApp.DisplayAlerts = false;

            Excel.Workbook xlWB;

            object misValue = Missing.Value;

            xlWB = xlApp.Workbooks.Open(path);


            foreach (Excel.Worksheet xlWS in xlWB.Worksheets)
            {

                if (sheet == 0) // if it is merchant sheet
                {
                    for (int y = 2; y <= xlWS.UsedRange.Rows.Count; y++)
                    {
                        Merchant merc = new Merchant
                            (
                            xlWS.Cells[y, 1].Text,
                            xlWS.Cells[y, 2].Text,
                            xlWS.Cells[y, 3].Text,
                            xlWS.Cells[y, 4].Text,
                            xlWS.Cells[y, 5].Text
                            );

                        merchants.Add(merc);
                    }
                    sheet++;
                }
                else if (sheet == 1) // if it is cardsystem sheet
                {
                    for (int y = 2; y <= xlWS.UsedRange.Rows.Count; y++)
                    {
                        Merchant merc = null;
                        bool status = true;
                        bool delayCPI = false;
                        bool delayEvalon = false;
                        Date crd = new Date(int.Parse(xlWS.Cells[y, 9].Text.Split('-')[0]), int.Parse(xlWS.Cells[y, 9].Text.Split('-')[1]), int.Parse(xlWS.Cells[y, 9].Text.Split('-')[2]), "Opretelses Dato");
                        Date cld = new Date(int.Parse(xlWS.Cells[y, 10].Text.Split('-')[0]), int.Parse(xlWS.Cells[y, 10].Text.Split('-')[1]), int.Parse(xlWS.Cells[y, 10].Text.Split('-')[2]), "Luknings Dato");

                        foreach (Merchant merchant in merchants)
                        {
                            if (merchant.ID == xlWS.Cells[y, 1].Text)
                            {
                                merc = merchant;
                                break;
                            }
                        }

                        if (xlWS.Cells[y, 2].Text != "Aktiv")
                        {
                            status = false;
                        }

                        if (xlWS.Cells[y, 3].Text == "Forsinket")
                        {
                            delayEvalon = true;
                        }
                        if (xlWS.Cells[y, 4].Text == "Forsinket")
                        {
                            delayCPI = true;
                        }

                        Cardsystem card = new Cardsystem
                            (
                            merc,
                            crd,
                            xlWS.Cells[y, 5].Text,
                            xlWS.Cells[y, 6].Text,
                            xlWS.Cells[y, 7].Text,
                            xlWS.Cells[y, 8].Text,
                            status,
                            delayEvalon,
                            delayCPI,
                            xlWS.Cells[y, 11].Text,
                            cld
                            );

                        cardsystems.Add(card);
                    }

                    sheet++;
                }
                else if (sheet == 2) // if it is mobilsystem sheet
                {
                    for (int y = 2; y <= xlWS.UsedRange.Rows.Count; y++)
                    {
                        Merchant merc = null;
                        bool status = true;
                        bool delayNETS = false;
                        bool delayEvalon = false;
                        Date crd = new Date(int.Parse(xlWS.Cells[y, 9].Text.Split('-')[0]), int.Parse(xlWS.Cells[y, 9].Text.Split('-')[1]), int.Parse(xlWS.Cells[y, 9].Text.Split('-')[2]), "Opretelses Dato");
                        Date cld = new Date(int.Parse(xlWS.Cells[y, 10].Text.Split('-')[0]), int.Parse(xlWS.Cells[y, 10].Text.Split('-')[1]), int.Parse(xlWS.Cells[y, 10].Text.Split('-')[2]), "Luknings Dato");

                        foreach (Merchant merchant in merchants)
                        {
                            if (merchant.ID == xlWS.Cells[y, 1].Text)
                            {
                                merc = merchant;
                                break;
                            }
                        }

                        if (xlWS.Cells[y, 2].Text != "Aktiv")
                        {
                            status = false;
                        }

                        if (xlWS.Cells[y, 3].Text == "Forsinket")
                        {
                            delayEvalon = true;
                        }
                        if (xlWS.Cells[y, 4].Text == "Forsinket")
                        {
                            delayNETS = true;
                        }

                        Mobilsystem mobil = new Mobilsystem
                            (
                            merc,
                            crd,
                            xlWS.Cells[y, 5].Text,
                            xlWS.Cells[y, 6].Text,
                            xlWS.Cells[y, 7].Text,
                            xlWS.Cells[y, 8].Text,
                            status,
                            delayEvalon,
                            delayNETS,
                            xlWS.Cells[y, 11].Text,
                            cld
                            );

                        mobilsystems.Add(mobil);
                    }

                    sheet++;
                }
            }

            repo.GetCardLists(cardsystems);
            repo.GetMobilLists(mobilsystems);
            repo.GetMercLists(merchants);
        }

        public void ExportBackup()
        {
            Excel.Application xlApp = new Excel.Application();

            Excel.Workbook xlWB;
            Excel.Worksheet xlWS;

            // http://csharp.net-informations.com/excel/csharp-create-excel.htm
            // misValue is a form of null that the program does like.
            object misValue = Missing.Value;

            xlWB = xlApp.Workbooks.Add(misValue);
            xlWB.Worksheets.Add();
            xlWB.Worksheets.Add();

            xlWS = (Excel.Worksheet)xlWB.Worksheets.get_Item(3);
            xlWS.Name = "Mobilsystems";

            xlWS.Cells[1, 1] = "MerchantID";
            xlWS.Cells[1, 2] = "Status";
            xlWS.Cells[1, 3] = "DelayElavon";
            xlWS.Cells[1, 4] = "DelayNETS";
            xlWS.Cells[1, 5] = "Address";
            xlWS.Cells[1, 6] = "SimNumber";
            xlWS.Cells[1, 7] = "MachineAddres";
            xlWS.Cells[1, 8] = "BoxName";
            xlWS.Cells[1, 9] = "CreationDate";
            xlWS.Cells[1, 10] = "CloseingDate";
            xlWS.Cells[1, 11] = "Note";

            for (int y = 2, l = 0; l < repo.GetMobilsystems().Count; y++, l++)
            {
                xlWS.Cells[y, 1] = toText + repo.GetMobilsystems()[l].Merchant.ID;
                xlWS.Cells[y, 2] = toText + repo.GetMobilsystems()[l].ToStringS();
                xlWS.Cells[y, 3] = toText + repo.GetMobilsystems()[l].ToStringDE();
                xlWS.Cells[y, 4] = toText + repo.GetMobilsystems()[l].ToStringDN();
                xlWS.Cells[y, 5] = toText + repo.GetMobilsystems()[l].Address;
                xlWS.Cells[y, 6] = toText + repo.GetMobilsystems()[l].SimNumber;
                xlWS.Cells[y, 7] = toText + repo.GetMobilsystems()[l].MachineAddress;
                xlWS.Cells[y, 8] = toText + repo.GetMobilsystems()[l].BoxName;
                xlWS.Cells[y, 9] = toText + repo.GetMobilsystems()[l].CreationDate.ToStringDF();
                if (repo.GetMobilsystems()[l].CloseingDate != null)
                {
                    xlWS.Cells[y, 10] = toText + repo.GetMobilsystems()[l].CloseingDate.ToStringDF();
                }
                xlWS.Cells[y, 11] = toText + repo.GetMobilsystems()[l].Note;
            }

            xlWS = (Excel.Worksheet)xlWB.Worksheets.get_Item(2);
            xlWS.Name = "Cardsystems";

            xlWS.Cells[1, 1] = "MerchantID";
            xlWS.Cells[1, 2] = "Status";
            xlWS.Cells[1, 3] = "DelayElavon";
            xlWS.Cells[1, 4] = "DelayCPI";
            xlWS.Cells[1, 5] = "Address";
            xlWS.Cells[1, 6] = "SimNumber";
            xlWS.Cells[1, 7] = "TerminalID";
            xlWS.Cells[1, 8] = "PhysicalID";
            xlWS.Cells[1, 9] = "CreationDate";
            xlWS.Cells[1, 10] = "CloseingDate";
            xlWS.Cells[1, 11] = "Note";

            for (int y = 2, l = 0; l < repo.GetCardsystems().Count; y++, l++)
            {
                xlWS.Cells[y, 1] = toText + repo.GetCardsystems()[l].Merchant.ID;
                xlWS.Cells[y, 2] = toText + repo.GetCardsystems()[l].ToStringS();
                xlWS.Cells[y, 3] = toText + repo.GetCardsystems()[l].ToStringDE();
                xlWS.Cells[y, 4] = toText + repo.GetCardsystems()[l].ToStringDC();
                xlWS.Cells[y, 5] = toText + repo.GetCardsystems()[l].Address;
                xlWS.Cells[y, 6] = toText + repo.GetCardsystems()[l].SimNumber;
                xlWS.Cells[y, 7] = toText + repo.GetCardsystems()[l].TerminalID;
                xlWS.Cells[y, 8] = toText + repo.GetCardsystems()[l].PhysicalID;
                xlWS.Cells[y, 9] = toText + repo.GetCardsystems()[l].CreationDate.ToStringDF();
                if (repo.GetCardsystems()[l].CloseingDate != null)
                {
                    xlWS.Cells[y, 10] = toText + repo.GetCardsystems()[l].CloseingDate.ToStringDF();
                }
                xlWS.Cells[y, 11] = toText + repo.GetCardsystems()[l].Note;
            }
            xlWS = (Excel.Worksheet)xlWB.Worksheets.get_Item(1);
            xlWS.Name = "Merchants";

            xlWS.Cells[1, 1] = "ID";
            xlWS.Cells[1, 2] = "Name";
            xlWS.Cells[1, 3] = "Firm";
            xlWS.Cells[1, 4] = "Mail";
            xlWS.Cells[1, 5] = "Note";

            for (int y = 2, l = 0; l < repo.GetMerchants().Count; y++, l++)
            {
                xlWS.Cells[y, 1] = toText + repo.GetMerchants()[l].ID;
                xlWS.Cells[y, 2] = toText + repo.GetMerchants()[l].Name;
                xlWS.Cells[y, 3] = toText + repo.GetMerchants()[l].Firm;
                xlWS.Cells[y, 4] = toText + repo.GetMerchants()[l].Mail;
                xlWS.Cells[y, 5] = toText + repo.GetMerchants()[l].Note;
            }

            string path = Path.Combine(Environment.CurrentDirectory, backup);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            xlWB.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal, misValue,
                misValue, true, true, Excel.XlSaveAsAccessMode.xlShared, false, false,
                misValue, misValue, misValue);
            xlWB.Close(true, misValue, misValue);
            xlApp.Quit();
        }
    }
}
