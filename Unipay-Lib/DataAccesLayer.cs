using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Unipay_Lib.Building_Blocks;
using System.Reflection;
using ExcelAddOn;

namespace Unipay_Lib
{
    public class DataAccesLayer
    {
        Repository repo = Repository.GetRepository();

        List<Mobilsystem> mobilsystems = new List<Mobilsystem>();
        List<Cardsystem> cardsystems = new List<Cardsystem>();
        List<Merchant> merchants = new List<Merchant>();

        string backup = "Datafile.xlsx";
        
        public void ImportBackup()
        {
            string path = Path.Combine(Environment.CurrentDirectory, backup);
            int skip = 0;
            int sheet = 0;

            foreach (var worksheet in Workbook.Worksheets(@path))
            {
                foreach (var row in worksheet.Rows)
                {
                    if (skip == 0)
                    {
                        continue;
                    }
                    skip++;
                    if (sheet == 0) // if it is the mechants sheet
                    {
                        Merchant merc = new Merchant(row.Cells[0].ToString(), row.Cells[1].ToString(), row.Cells[2].ToString(),
                            row.Cells[3].ToString(), row.Cells[4].ToString());

                        merchants.Add(merc);
                    }
                    else if (sheet == 1) // if it is the cardsystems sheet
                    {
                        Merchant merc = null;
                        Date crd = new Date(int.Parse(row.Cells[8].ToString().Split('-')[0]),
                            int.Parse(row.Cells[8].ToString().Split('-')[1]),
                            int.Parse(row.Cells[8].ToString().Split('-')[2]));
                        Date cld = null;
                        if (row.Cells[9] != null)
                        {
                            cld = new Date(int.Parse(row.Cells[9].ToString().Split('-')[0]),
                            int.Parse(row.Cells[9].ToString().Split('-')[1]),
                            int.Parse(row.Cells[9].ToString().Split('-')[2]));
                        }
                        bool status = false;
                        bool de = false;
                        bool dc = false;

                        if (row.Cells[1].ToString() == "Aktiv")
                        {
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                        if (row.Cells[2].ToString() == "Forsinket")
                        {
                            de = true;
                        }
                        else
                        {
                            de = false;
                        }
                        if (row.Cells[3].ToString() == "Forsinket")
                        {
                            dc = true;
                        }
                        else
                        {
                            dc = false;
                        }


                        foreach (Merchant merchant in merchants)
                        {
                            if (merchant.ID == row.Cells[0].ToString())
                            {
                                merc = merchant;
                            }
                        }

                        Cardsystem card = new Cardsystem(merc, crd, row.Cells[4].ToString(),
                            row.Cells[5].ToString(), row.Cells[6].ToString(),
                            row.Cells[7].ToString(), status, de, dc,
                            row.Cells[10].ToString(), cld);

                        cardsystems.Add(card);
                    }
                    else if (sheet == 2) // if it is the mobilsystems sheet
                    {
                        Merchant merc = null;
                        Date crd = new Date(int.Parse(row.Cells[8].ToString().Split('-')[0]),
                            int.Parse(row.Cells[8].ToString().Split('-')[1]),
                            int.Parse(row.Cells[8].ToString().Split('-')[2]));
                        Date cld = null;
                        if (row.Cells[9] != null)
                        {
                            cld = new Date(int.Parse(row.Cells[9].ToString().Split('-')[0]),
                            int.Parse(row.Cells[9].ToString().Split('-')[1]),
                            int.Parse(row.Cells[9].ToString().Split('-')[2]));
                        }
                        bool status = false;
                        bool de = false;
                        bool dn = false;

                        if (row.Cells[1].ToString() == "Aktiv")
                        {
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                        if (row.Cells[2].ToString() == "Forsinket")
                        {
                            de = true;
                        }
                        else
                        {
                            de = false;
                        }
                        if (row.Cells[3].ToString() == "Forsinket")
                        {
                            dn = true;
                        }
                        else
                        {
                            dn = false;
                        }


                        foreach (Merchant merchant in merchants)
                        {
                            if (merchant.ID == row.Cells[0].ToString())
                            {
                                merc = merchant;
                            }
                        }

                        Mobilsystem mobil = new Mobilsystem(merc, crd, row.Cells[4].ToString(),
                            row.Cells[5].ToString(), row.Cells[6].ToString(),
                            row.Cells[7].ToString(), status, de, dn,
                            row.Cells[10].ToString(), cld);

                        mobilsystems.Add(mobil);
                    }
                    skip = 0;
                }
                sheet++;
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
                xlWS.Cells[y, 1] = repo.GetMobilsystems()[l].Merchant.ID;
                xlWS.Cells[y, 2] = repo.GetMobilsystems()[l].ToStringS();
                xlWS.Cells[y, 3] = repo.GetMobilsystems()[l].ToStringDE();
                xlWS.Cells[y, 4] = repo.GetMobilsystems()[l].ToStringDN();
                xlWS.Cells[y, 5] = repo.GetMobilsystems()[l].Address;
                xlWS.Cells[y, 6] = repo.GetMobilsystems()[l].SimNumber;
                xlWS.Cells[y, 7] = repo.GetMobilsystems()[l].MachineAddress;
                xlWS.Cells[y, 8] = repo.GetMobilsystems()[l].BoxName;
                xlWS.Cells[y, 9] = repo.GetMobilsystems()[l].CreationDate.ToStringDF();
                if (repo.GetMobilsystems()[l].CloseingDate != null)
                {
                    xlWS.Cells[y, 10] = repo.GetMobilsystems()[l].CloseingDate.ToStringDF();
                }
                xlWS.Cells[y, 11] = repo.GetMobilsystems()[l].Note;
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
                xlWS.Cells[y, 1] = repo.GetCardsystems()[l].Merchant.ID;
                xlWS.Cells[y, 2] = repo.GetCardsystems()[l].ToStringS();
                xlWS.Cells[y, 3] = repo.GetCardsystems()[l].ToStringDE();
                xlWS.Cells[y, 4] = repo.GetCardsystems()[l].ToStringDC();
                xlWS.Cells[y, 5] = repo.GetCardsystems()[l].Address;
                xlWS.Cells[y, 6] = repo.GetCardsystems()[l].SimNumber;
                xlWS.Cells[y, 7] = repo.GetCardsystems()[l].TerminalID;
                xlWS.Cells[y, 8] = repo.GetCardsystems()[l].PhysicalID;
                xlWS.Cells[y, 9] = repo.GetCardsystems()[l].CreationDate.ToStringDF();
                if (repo.GetCardsystems()[l].CloseingDate != null)
                {
                    xlWS.Cells[y, 10] = repo.GetCardsystems()[l].CloseingDate.ToStringDF();
                }
                xlWS.Cells[y, 11] = repo.GetCardsystems()[l].Note;
            }
            xlWS = (Excel.Worksheet)xlWB.Worksheets.get_Item(1);
            xlWS.Name = "Merchants";

            xlWS.Cells[1, 1] = "ID";
            xlWS.Cells[1, 2] = "Name";
            xlWS.Cells[1, 3] = "Firm";
            xlWS.Cells[1, 4] = "Mail";
            xlWS.Cells[1, 5] = "Note";

            for (int y = 2, l = 0; l < repo.GetCardsystems().Count; y++, l++)
            {
                xlWS.Cells[y, 1] = repo.GetMerchants()[l].ID;
                xlWS.Cells[y, 1] = repo.GetMerchants()[l].Name;
                xlWS.Cells[y, 1] = repo.GetMerchants()[l].Firm;
                xlWS.Cells[y, 1] = repo.GetMerchants()[l].Mail;
                xlWS.Cells[y, 1] = repo.GetMerchants()[l].Note;
            }

            string path = Path.Combine(Environment.CurrentDirectory, backup);

            xlWB.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal, misValue,
                misValue, true, true, Excel.XlSaveAsAccessMode.xlShared, false, false,
                misValue, misValue, misValue);
            xlWB.Close(true, misValue, misValue);
            xlApp.Quit();
        }
    }
}
