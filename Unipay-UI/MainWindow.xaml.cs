using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unipay_Lib;
using Unipay_Lib.Building_Blocks;
using Unipay_UI.Functionality_Windows;

namespace Unipay_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> Types = new List<string> { "", "Mobilsystem", "Kortsystem", "Kunder"};
        List<string> ColumnAll = new List<string> { "" };
        List<string> ColumnMobil = new List<string> { "" };
        List<string> ColumnCard = new List<string> { "" };
        List<string> ColumnMerc = new List<string> { "" };


        List<Mobilsystem> SRMobil = new List<Mobilsystem>();
        List<Cardsystem> SRCard = new List<Cardsystem>();
        List<Merchant> SRMerc = new List<Merchant>();

        Repository repo = Repository.GetRepository();
        Control control = new Control();

        DataTable phoneView = new DataTable();
        DataTable cardView = new DataTable();
        DataTable mercView = new DataTable();

        public bool PhoneState { get; set; }
        public bool CardState { get; set; }
        public bool MerchantState { get; set; }
        public bool SearchState { get; set; }

        
        public MainWindow()
        {
            InitializeComponent();
            TypeFilter.ItemsSource = Types;

            PhoneState = true;
            CardState = false;
            MerchantState = false;
            SearchState = false;

            phoneView.Columns.Add("Merchant ID");
            phoneView.Columns.Add("Status");
            phoneView.Columns.Add("Forsinkelse Elavon");
            phoneView.Columns.Add("Forsinkelse NETS");
            phoneView.Columns.Add("MAC Addresse");
            phoneView.Columns.Add("Boks Navn");
            phoneView.Columns.Add("Sim Nummer");
            phoneView.Columns.Add("Opretelses Dato");
            phoneView.Columns.Add("Luknings Dato");
            phoneView.Columns.Add("Addresse for Enhed");
            phoneView.Columns.Add("Noter");

            cardView.Columns.Add("Merchant ID");
            cardView.Columns.Add("Status");
            cardView.Columns.Add("Forsinkelse Elavon");
            cardView.Columns.Add("Forsinkelse CPI");
            cardView.Columns.Add("Terminal ID");
            cardView.Columns.Add("Phys ID");
            cardView.Columns.Add("Sim Producent");
            cardView.Columns.Add("Opretelses Dato");
            cardView.Columns.Add("Luknings Dato");
            cardView.Columns.Add("Addresse for Enhed");
            cardView.Columns.Add("Noter");

            mercView.Columns.Add("Merchant ID");
            mercView.Columns.Add("Navn");
            mercView.Columns.Add("Firma");
            mercView.Columns.Add("Mail");
            mercView.Columns.Add("Noter");

            foreach (DataColumn column in phoneView.Columns)
            {
                if (!ColumnAll.Contains(column.ColumnName))
                {
                    ColumnAll.Add(column.ColumnName);
                }
                if (!ColumnMobil.Contains(column.ColumnName))
                {
                    ColumnMobil.Add(column.ColumnName);
                }
                
            }
            foreach (DataColumn column in cardView.Columns)
            {
                if (!ColumnAll.Contains(column.ColumnName)) 
                {
                    ColumnAll.Add(column.ColumnName);
                }
                if (!ColumnCard.Contains(column.ColumnName))
                {
                    ColumnCard.Add(column.ColumnName);
                }

            }
            foreach (DataColumn column in mercView.Columns)
            {
                if (!ColumnAll.Contains(column.ColumnName))
                {
                    ColumnAll.Add(column.ColumnName);
                }
                if (!ColumnMerc.Contains(column.ColumnName))
                {
                    ColumnMerc.Add(column.ColumnName);
                }
            }

            ColumnFilter.ItemsSource = ColumnAll;

            UpdateView();
        }

        private void Search()
        {
            SRMobil.Clear();
            SRCard.Clear();
            SRMerc.Clear();

            if (TypeFilter.SelectedIndex == 0 || TypeFilter.SelectedIndex == 1)
            {
                foreach (Mobilsystem mobil in repo.GetMobilsystems())
                {
                    if (ColumnFilter.SelectedIndex == 0)
                    {
                        if (mobil.Address.Contains(SearchBox.Text) || mobil.BoxName.Contains(SearchBox.Text) || mobil.CreationDate.ToStringDF().Contains(SearchBox.Text)
                            || mobil.ToStringDE().Contains(SearchBox.Text) || mobil.ToStringDN().Contains(SearchBox.Text)
                            || mobil.MachineAddress.Contains(SearchBox.Text) || mobil.Merchant.ID.Contains(SearchBox.Text)
                            || mobil.SimNumber.Contains(SearchBox.Text) || mobil.CloseingDate.ToStringDF().Contains(SearchBox.Text)
                            || mobil.Note.Contains(SearchBox.Text))
                        {
                            SRMobil.Add(mobil);
                        }
                    }
                    else
                    {
                        if (TypeFilter.SelectedIndex == 0)
                        {
                            if (ColumnAll[ColumnFilter.SelectedIndex] == "Merchant ID")
                            {
                                if (mobil.Merchant.ID.Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Status")
                            {
                                if (mobil.ToStringS().Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Forsinkelse Elavon")
                            {
                                if (mobil.ToStringDE().Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Forsinkelse NETS")
                            {
                                if (mobil.ToStringDN().Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "MAC Addresse")
                            {
                                if (mobil.MachineAddress.Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Boks Navn")
                            {
                                if (mobil.BoxName.Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Sim Nummer")
                            {
                                if (mobil.SimNumber.Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Opretelses Dato")
                            {
                                if (mobil.CreationDate.ToStringDF().Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Addresse for Enhed")
                            {
                                if (mobil.Address.Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Luknings Dato")
                            {
                                if (mobil.CloseingDate.ToStringDF().Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Noter")
                            {
                                if (mobil.Note.Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                        }
                        else
                        {
                            if (ColumnMobil[ColumnFilter.SelectedIndex] == "Merchant ID")
                            {
                                if (mobil.Merchant.ID.Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnMobil[ColumnFilter.SelectedIndex] == "Status")
                            {
                                if (mobil.ToStringS().Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnMobil[ColumnFilter.SelectedIndex] == "Forsinkelse Elavon")
                            {
                                if (mobil.ToStringDE().Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnMobil[ColumnFilter.SelectedIndex] == "Forsinkelse NETS")
                            {
                                if (mobil.ToStringDN().Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnMobil[ColumnFilter.SelectedIndex] == "MAC Addresse")
                            {
                                if (mobil.MachineAddress.Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnMobil[ColumnFilter.SelectedIndex] == "Boks Navn")
                            {
                                if (mobil.BoxName.Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnMobil[ColumnFilter.SelectedIndex] == "Sim Nummer")
                            {
                                if (mobil.SimNumber.Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnMobil[ColumnFilter.SelectedIndex] == "Opretelses Dato")
                            {
                                if (mobil.CreationDate.ToStringDF().Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnMobil[ColumnFilter.SelectedIndex] == "Addresse for Enhed")
                            {
                                if (mobil.Address.Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnMobil[ColumnFilter.SelectedIndex] == "Luknings Dato")
                            {
                                if (mobil.CloseingDate.ToStringDF().Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                            else if (ColumnMobil[ColumnFilter.SelectedIndex] == "Noter")
                            {
                                if (mobil.Note.Contains(SearchBox.Text))
                                {
                                    SRMobil.Add(mobil);
                                }
                            }
                        }
                    }
                }
            }
            if (TypeFilter.SelectedIndex == 0 || TypeFilter.SelectedIndex == 2)
            {
                foreach (Cardsystem card in repo.GetCardsystems())
                {
                    if (ColumnFilter.SelectedIndex == 0)
                    {
                        if (card.Address.Contains(SearchBox.Text) || card.PhysicalID.Contains(SearchBox.Text) || card.CreationDate.ToStringDF().Contains(SearchBox.Text)
                            || card.ToStringDE().Contains(SearchBox.Text) || card.ToStringDC().Contains(SearchBox.Text)
                            || card.PhysicalID.Contains(SearchBox.Text) || card.Merchant.ID.Contains(SearchBox.Text)
                            || card.SimNumber.Contains(SearchBox.Text) || card.CloseingDate.ToStringDF().Contains(SearchBox.Text)
                            || card.Note.Contains(SearchBox.Text))
                        {
                            SRCard.Add(card);
                        }
                    }
                    else
                    {
                        if (TypeFilter.SelectedIndex == 0)
                        {
                            if (ColumnAll[ColumnFilter.SelectedIndex] == "Merchant ID")
                            {
                                if (card.Merchant.ID.Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Status")
                            {
                                if (card.ToStringS().Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Forsinkelse Elavon")
                            {
                                if (card.ToStringDE().Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Forsinkelse CPI")
                            {
                                if (card.ToStringDC().Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Terminal ID")
                            {
                                if (card.TerminalID.Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Phys ID")
                            {
                                if (card.PhysicalID.Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Sim Producent")
                            {
                                if (card.SimNumber.Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Opretelses Dato")
                            {
                                if (card.CreationDate.ToStringDF().Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Addresse for Enhed")
                            {
                                if (card.Address.Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Luknings Dato")
                            {
                                if (card.CloseingDate.ToStringDF().Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Noter")
                            {
                                if (card.Note.Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                        }
                        else
                        {
                            if (ColumnCard[ColumnFilter.SelectedIndex] == "Merchant ID")
                            {
                                if (card.Merchant.ID.Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnCard[ColumnFilter.SelectedIndex] == "Status")
                            {
                                if (card.ToStringS().Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnCard[ColumnFilter.SelectedIndex] == "Forsinkelse Elavon")
                            {
                                if (card.ToStringDE().Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnCard[ColumnFilter.SelectedIndex] == "Forsinkelse CPI")
                            {
                                if (card.ToStringDC().Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnCard[ColumnFilter.SelectedIndex] == "MAC Addresse")
                            {
                                if (card.TerminalID.Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnCard[ColumnFilter.SelectedIndex] == "Phys ID")
                            {
                                if (card.PhysicalID.Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnCard[ColumnFilter.SelectedIndex] == "Sim Producent")
                            {
                                if (card.SimNumber.Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnCard[ColumnFilter.SelectedIndex] == "Opretelses Dato")
                            {
                                if (card.CreationDate.ToStringDF().Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnCard[ColumnFilter.SelectedIndex] == "Addresse for Enhed")
                            {
                                if (card.Address.Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnCard[ColumnFilter.SelectedIndex] == "Luknings Dato")
                            {
                                if (card.CloseingDate.ToStringDF().Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                            else if (ColumnCard[ColumnFilter.SelectedIndex] == "Noter")
                            {
                                if (card.Note.Contains(SearchBox.Text))
                                {
                                    SRCard.Add(card);
                                }
                            }
                        }
                    }
                }
            }
            if (TypeFilter.SelectedIndex == 0 || TypeFilter.SelectedIndex == 3)
            {
                foreach (Merchant merc in repo.GetMerchants())
                {
                    if (ColumnFilter.SelectedIndex == 0)
                    {
                        if (merc.ID.Contains(SearchBox.Text) || merc.Name.Contains(SearchBox.Text) 
                            || merc.Firm.Contains(SearchBox.Text) || merc.Mail.Contains(SearchBox.Text)
                            || merc.Note.Contains(SearchBox.Text))
                        {
                            SRMerc.Add(merc);
                        }
                    }
                    else
                    {
                        if (TypeFilter.SelectedIndex == 0)
                        {
                            if (ColumnAll[ColumnFilter.SelectedIndex] == "Merchant ID")
                            {
                                if (merc.ID.Contains(SearchBox.Text))
                                {
                                    SRMerc.Add(merc);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Navn")
                            {
                                if (merc.Name.Contains(SearchBox.Text))
                                {
                                    SRMerc.Add(merc);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Firma")
                            {
                                if (merc.Firm.Contains(SearchBox.Text))
                                {
                                    SRMerc.Add(merc);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Mail")
                            {
                                if (merc.Mail.Contains(SearchBox.Text))
                                {
                                    SRMerc.Add(merc);
                                }
                            }
                            else if (ColumnAll[ColumnFilter.SelectedIndex] == "Noter")
                            {
                                if (merc.Note.Contains(SearchBox.Text))
                                {
                                    SRMerc.Add(merc);
                                }
                            }
                        }
                        else
                        {
                            if (ColumnMerc[ColumnFilter.SelectedIndex] == "Merchant ID")
                            {
                                if (merc.ID.Contains(SearchBox.Text))
                                {
                                    SRMerc.Add(merc);
                                }
                            }
                            else if (ColumnMerc[ColumnFilter.SelectedIndex] == "Navn")
                            {
                                if (merc.Name.Contains(SearchBox.Text))
                                {
                                    SRMerc.Add(merc);
                                }
                            }
                            else if (ColumnMerc[ColumnFilter.SelectedIndex] == "Firma")
                            {
                                if (merc.Firm.Contains(SearchBox.Text))
                                {
                                    SRMerc.Add(merc);
                                }
                            }
                            else if (ColumnMerc[ColumnFilter.SelectedIndex] == "Mail")
                            {
                                if (merc.Mail.Contains(SearchBox.Text))
                                {
                                    SRMerc.Add(merc);
                                }
                            }
                            else if (ColumnMerc[ColumnFilter.SelectedIndex] == "Noter")
                            {
                                if (merc.Note.Contains(SearchBox.Text))
                                {
                                    SRMerc.Add(merc);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void UpdateView()
        {
            phoneView.Clear();
            cardView.Clear();
            mercView.Clear();

            if (PhoneState)
                {
                if (repo.GetMobilsystems().Count() != 0)
                {
                    MobilGrid.Height = 600;
                    CardGrid.Height = 0;
                    MercGrid.Height = 0;
                    MobilGrid.Margin = new Thickness(0, 0, 0, 0);
                    CardGrid.Margin = new Thickness(0, 0, 0, 0);
                    MercGrid.Margin = new Thickness(0, 0, 0, 0);
                    MobilGrid.Visibility = Visibility.Visible;
                    CardGrid.Visibility = Visibility.Hidden;
                    MercGrid.Visibility = Visibility.Hidden;

                    foreach (Mobilsystem mobil in repo.GetMobilsystems())
                    {
                        DataRow row = phoneView.NewRow();

                        row["Merchant ID"] = mobil.Merchant.ID;
                        row["Status"] = mobil.ToStringS();
                        row["Forsinkelse Elavon"] = mobil.ToStringDE();
                        row["Forsinkelse NETS"] = mobil.ToStringDN();
                        row["MAC Addresse"] = mobil.MachineAddress;
                        row["Boks Navn"] = mobil.BoxName;
                        row["Sim Nummer"] = mobil.SimNumber;
                        row["Opretelses Dato"] = mobil.CreationDate.ToStringDF();
                        row["Addresse for Enhed"] = mobil.Address;

                        phoneView.Rows.Add(row);
                    }
                }
                else
                {
                    MobilGrid.Height = 0;
                    CardGrid.Height = 0;
                    MercGrid.Height = 0;
                    MobilGrid.Margin = new Thickness(0, 0, 0, 0);
                    CardGrid.Margin = new Thickness(0, 0, 0, 0);
                    MercGrid.Margin = new Thickness(0, 0, 0, 0);
                    MobilGrid.Visibility = Visibility.Hidden;
                    CardGrid.Visibility = Visibility.Hidden;
                    MercGrid.Visibility = Visibility.Hidden;
                }
                
                
            }
                else if (CardState)
                {
                if (repo.GetCardsystems().Count() != 0)
                {
                    MobilGrid.Height = 0;
                    CardGrid.Height = 600;
                    MercGrid.Height = 0;
                    MobilGrid.Margin = new Thickness(0, 0, 0, 0);
                    CardGrid.Margin = new Thickness(0, 0, 0, 0);
                    MercGrid.Margin = new Thickness(0, 0, 0, 0);
                    MobilGrid.Visibility = Visibility.Hidden;
                    CardGrid.Visibility = Visibility.Visible;
                    MercGrid.Visibility = Visibility.Hidden;

                    foreach (Cardsystem card in repo.GetCardsystems())
                    {
                        DataRow row = cardView.NewRow();

                        row["Merchant ID"] = card.Merchant.ID;
                        row["Status"] = card.ToStringS();
                        row["Forsinkelse Elavon"] = card.ToStringDE();
                        row["Forsinkelse CPI"] = card.ToStringDC();
                        row["Terminal ID"] = card.TerminalID;
                        row["Phys ID"] = card.PhysicalID;
                        row["Sim Producent"] = card.SimNumber;
                        row["Opretelses Dato"] = card.CreationDate.ToStringDF();
                        row["Addresse for Enhed"] = card.Address;

                        cardView.Rows.Add(row);
                    }
                }
                else
                {
                    MobilGrid.Height = 0;
                    CardGrid.Height = 0;
                    MercGrid.Height = 0;
                    MobilGrid.Margin = new Thickness(0, 0, 0, 0);
                    CardGrid.Margin = new Thickness(0, 0, 0, 0);
                    MercGrid.Margin = new Thickness(0, 0, 0, 0);
                    MobilGrid.Visibility = Visibility.Hidden;
                    CardGrid.Visibility = Visibility.Hidden;
                    MercGrid.Visibility = Visibility.Hidden;
                }
                
                
            }
                else if (MerchantState)
                {
                if (repo.GetMerchants().Count() != 0)
                {
                    MobilGrid.Height = 0;
                    CardGrid.Height = 0;
                    MercGrid.Height = 600;
                    MobilGrid.Margin = new Thickness(0, 0, 0, 0);
                    CardGrid.Margin = new Thickness(0, 0, 0, 0);
                    MercGrid.Margin = new Thickness(0, 0, 0, 0);
                    MobilGrid.Visibility = Visibility.Hidden;
                    CardGrid.Visibility = Visibility.Hidden;
                    MercGrid.Visibility = Visibility.Visible;

                    foreach (Merchant merc in repo.GetMerchants())
                    {
                        DataRow row = mercView.NewRow();

                        row["Merchant ID"] = merc.ID;
                        row["Navn"] = merc.Name;
                        row["Firma"] = merc.Firm;
                        row["Mail"] = merc.Mail;

                        mercView.Rows.Add(row);
                    }
                }
                else
                {
                    MobilGrid.Height = 0;
                    CardGrid.Height = 0;
                    MercGrid.Height = 0;
                    MobilGrid.Margin = new Thickness(0, 0, 0, 0);
                    CardGrid.Margin = new Thickness(0, 0, 0, 0);
                    MercGrid.Margin = new Thickness(0, 0, 0, 0);
                    MobilGrid.Visibility = Visibility.Hidden;
                    CardGrid.Visibility = Visibility.Hidden;
                    MercGrid.Visibility = Visibility.Hidden;
                }
                
                
            }
                else if (SearchState)
                {
                    if (SRMobil.Count != 0 && SRCard.Count != 0 && SRMerc.Count != 0) // all have hits
                    {
                        MobilGrid.Height = 200;
                        CardGrid.Height = 200;
                        MercGrid.Height = 200;
                        MobilGrid.Margin = new Thickness(0, 0, 0, 400);
                        CardGrid.Margin = new Thickness(0, 0, 0, 0);
                        MercGrid.Margin = new Thickness(0, 400, 0, 0);
                        MobilGrid.Visibility = Visibility.Visible;
                        CardGrid.Visibility = Visibility.Visible;
                        MercGrid.Visibility = Visibility.Visible;

                        SetMobilGrid();
                        SetCardGrid();
                        SetMercGrid();
                    }
                    else if (SRMobil.Count != 0 && SRCard.Count != 0 && SRMerc.Count == 0) // all but Merc have hits
                    {
                        MobilGrid.Height = 300;
                        CardGrid.Height = 300;
                        MercGrid.Height = 0;
                        MobilGrid.Margin = new Thickness(0, 0, 0, 300);
                        CardGrid.Margin = new Thickness(0, 300, 0, 0);
                        MercGrid.Margin = new Thickness(0, 0, 0, 0);
                        MobilGrid.Visibility = Visibility.Visible;
                        CardGrid.Visibility = Visibility.Visible;
                        MercGrid.Visibility = Visibility.Hidden;

                        SetMobilGrid();
                        SetCardGrid();
                    }
                    else if (SRMobil.Count != 0 && SRCard.Count == 0 && SRMerc.Count != 0) // all but Card have hits
                    {
                        MobilGrid.Height = 300;
                        CardGrid.Height = 0;
                        MercGrid.Height = 300;
                        MobilGrid.Margin = new Thickness(0, 0, 0, 300);
                        CardGrid.Margin = new Thickness(0, 0, 0, 0);
                        MercGrid.Margin = new Thickness(0, 300, 0, 0);
                        MobilGrid.Visibility = Visibility.Visible;
                        CardGrid.Visibility = Visibility.Hidden;
                        MercGrid.Visibility = Visibility.Visible;

                        SetMobilGrid();
                        SetMercGrid();
                    }
                    else if (SRMobil.Count == 0 && SRCard.Count != 0 && SRMerc.Count != 0) // all but Mobil have hits
                    {
                        MobilGrid.Height = 0;
                        CardGrid.Height = 300;
                        MercGrid.Height = 300;
                        MobilGrid.Margin = new Thickness(0, 0, 0, 0);
                        CardGrid.Margin = new Thickness(0, 0, 0, 300);
                        MercGrid.Margin = new Thickness(0, 300, 0, 0);
                        MobilGrid.Visibility = Visibility.Hidden;
                        CardGrid.Visibility = Visibility.Visible;
                        MercGrid.Visibility = Visibility.Visible;

                        SetCardGrid();
                        SetMercGrid();
                    }
                    else if (SRMobil.Count != 0 && SRCard.Count == 0 && SRMerc.Count == 0) // Mobil have hits
                    {
                        MobilGrid.Height = 600;
                        CardGrid.Height = 0;
                        MercGrid.Height = 0;
                        MobilGrid.Margin = new Thickness(0, 0, 0, 0);
                        CardGrid.Margin = new Thickness(0, 0, 0, 0);
                        MercGrid.Margin = new Thickness(0, 0, 0, 0);
                        MobilGrid.Visibility = Visibility.Visible;
                        CardGrid.Visibility = Visibility.Hidden;
                        MercGrid.Visibility = Visibility.Hidden;

                        SetMobilGrid();
                    }
                    else if (SRMobil.Count == 0 && SRCard.Count != 0 && SRMerc.Count == 0) // Card have hits
                    {
                        MobilGrid.Height = 0;
                        CardGrid.Height = 600;
                        MercGrid.Height = 0;
                        MobilGrid.Margin = new Thickness(0, 0, 0, 0);
                        CardGrid.Margin = new Thickness(0, 0, 0, 0);
                        MercGrid.Margin = new Thickness(0, 0, 0, 0);
                        MobilGrid.Visibility = Visibility.Hidden;
                        CardGrid.Visibility = Visibility.Visible;
                        MercGrid.Visibility = Visibility.Hidden;

                        SetCardGrid();
                    }
                    else if (SRMobil.Count == 0 && SRCard.Count == 0 && SRMerc.Count != 0) // Merc have hits
                    {
                        MobilGrid.Height = 0;
                        CardGrid.Height = 0;
                        MercGrid.Height = 600;
                        MobilGrid.Margin = new Thickness(0, 0, 0, 0);
                        CardGrid.Margin = new Thickness(0, 0, 0, 0);
                        MercGrid.Margin = new Thickness(0, 0, 0, 0);
                        MobilGrid.Visibility = Visibility.Hidden;
                        CardGrid.Visibility = Visibility.Hidden;
                        MercGrid.Visibility = Visibility.Visible;

                        SetMercGrid();
                    }
                    else if (SRMobil.Count == 0 && SRCard.Count == 0 && SRMerc.Count == 0) // None have hits
                    {
                        MobilGrid.Height = 0;
                        CardGrid.Height = 0;
                        MercGrid.Height = 0;
                        MobilGrid.Margin = new Thickness(0, 0, 0, 0);
                        CardGrid.Margin = new Thickness(0, 0, 0, 0);
                        MercGrid.Margin = new Thickness(0, 0, 0, 0);
                        MobilGrid.Visibility = Visibility.Hidden;
                        CardGrid.Visibility = Visibility.Hidden;
                        MercGrid.Visibility = Visibility.Hidden;
                    }
                }
        }

        private void SetMobilGrid()
        {
            foreach (Mobilsystem mobil in SRMobil)
            {
                DataRow row = phoneView.NewRow();

                row["Merchant ID"] = mobil.Merchant.ID;
                row["Status"] = mobil.ToStringS();
                row["Forsinkelse Elavon"] = mobil.ToStringDE();
                row["Forsinkelse NETS"] = mobil.ToStringDN();
                row["MAC Addresse"] = mobil.MachineAddress;
                row["Boks Navn"] = mobil.BoxName;
                row["Sim Nummer"] = mobil.SimNumber;
                row["Opretelses Dato"] = mobil.CreationDate.ToStringDF();
                row["Addresse for Enhed"] = mobil.Address;

                phoneView.Rows.Add(row);
            }
        }

        private void SetCardGrid()
        {
            foreach (Cardsystem card in SRCard)
            {
                DataRow row = cardView.NewRow();

                row["Merchant ID"] = card.Merchant.ID;
                row["Status"] = card.ToStringS();
                row["Forsinkelse Elavon"] = card.ToStringDE();
                row["Forsinkelse CPI"] = card.ToStringDC();
                row["Terminal ID"] = card.TerminalID;
                row["Phys ID"] = card.PhysicalID;
                row["Sim Producent"] = card.SimNumber;
                row["Opretelses Dato"] = card.CreationDate.ToStringDF();
                row["Addresse for Enhed"] = card.Address;

                cardView.Rows.Add(row);
            }
        }

        private void SetMercGrid()
        {
            foreach (Merchant merc in SRMerc)
            {
                DataRow row = mercView.NewRow();

                row["Merchant ID"] = merc.ID;
                row["Navn"] = merc.Name;
                row["Firma"] = merc.Firm;
                row["Mail"] = merc.Mail;

                mercView.Rows.Add(row);
            }
        }

        private void Mobilsystem_Click(object sender, RoutedEventArgs e)
        {
            PhoneState = true;
            CardState = false;
            MerchantState = false;
            SearchState = false;

            UpdateView();
        }

        private void Kortsystem_Click(object sender, RoutedEventArgs e)
        {
            PhoneState = false;
            CardState = true;
            MerchantState = false;
            SearchState = false;

            UpdateView();
        }

        private void Søgeresultat_Click(object sender, RoutedEventArgs e)
        {
            PhoneState = false;
            CardState = false;
            MerchantState = false;
            SearchState = true;

            UpdateView();
        }

        private void Kunde_Click(object sender, RoutedEventArgs e)
        {
            PhoneState = false;
            CardState = false;
            MerchantState = true;
            SearchState = false;

            UpdateView();
        }

        private void TypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeFilter.SelectedIndex == 0)
            {
                ColumnFilter.ItemsSource = ColumnAll;
            }
            else if (TypeFilter.SelectedIndex == 1)
            {
                ColumnFilter.ItemsSource = ColumnMobil;
            }
            else if (TypeFilter.SelectedIndex == 2)
            {
                ColumnFilter.ItemsSource = ColumnCard;
            }
            else if (TypeFilter.SelectedIndex == 3)
            {
                ColumnFilter.ItemsSource = ColumnMerc;
            }

            Search();
            UpdateView();
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {


            UpdateView();
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewSubscription_Click(object sender, RoutedEventArgs e)
        {
            NewSubscription newsub = new NewSubscription();

            newsub.ShowDialog();

            UpdateView();
        }

        private void EditSubscription_Click(object sender, RoutedEventArgs e)
        {


            UpdateView();
        }

        private void DeleteSubscription_Click(object sender, RoutedEventArgs e)
        {


            UpdateView();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
            UpdateView();
        }

        private void ColumnFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
            UpdateView();
        }
    }
}
