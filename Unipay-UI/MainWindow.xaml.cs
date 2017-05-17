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
        #region Unused
        List<string> Types = new List<string> { "", "Mobilsystem", "Kortsystem", "Kunder" };
        List<string> ColumnAll = new List<string> { "" };
        List<string> ColumnMobil = new List<string> { "" };
        List<string> ColumnCard = new List<string> { "" };
        List<string> ColumnMerc = new List<string> { "" };
        #endregion

        List<Mobilsystem> SRMobil = new List<Mobilsystem>();
        List<Cardsystem> SRCard = new List<Cardsystem>();
        List<Merchant> SRMerc = new List<Merchant>();

        Repository repo = Repository.GetRepository();
        Controler control = new Controler();
        DataAccesLayer data = new DataAccesLayer();

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

            try
            {
                data.ImportBackup();
            }
            catch (Exception)
            {

            }

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


            UpdateView();
            HideUnused();
        }

        private void Search()
        {
            SRMobil.Clear();
            SRCard.Clear();
            SRMerc.Clear();

            
            
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
                        row["Noter"] = mobil.Note;

                        phoneView.Rows.Add(row);
                    }
                    MobilGrid.ItemsSource = phoneView.AsDataView();
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
                        row["Noter"] = card.Note;

                        cardView.Rows.Add(row);
                    }
                    CardGrid.ItemsSource = cardView.AsDataView();
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
                        row["Noter"] = merc.Note;

                        mercView.Rows.Add(row);
                    }
                    MercGrid.ItemsSource = mercView.AsDataView();
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
            data.ExportBackup();
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
                row["Noter"] = mobil.Note;

                phoneView.Rows.Add(row);
            }
            MobilGrid.ItemsSource = phoneView.AsDataView();
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
                row["Noter"] = card.Note;

                cardView.Rows.Add(row);
            }
            CardGrid.ItemsSource = cardView.AsDataView();
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
                row["Noter"] = merc.Note;

                mercView.Rows.Add(row);
            }
            MercGrid.ItemsSource = mercView.AsDataView();
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

        
        private void NewSubscription_Click(object sender, RoutedEventArgs e)
        {
            NewSubscription newsub = new NewSubscription();
            newsub.Title = "Opret";

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
        #region Unused
        private void Import_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HideUnused()
        {
            FilterText.Visibility = Visibility.Hidden;
            Import.Visibility = Visibility.Hidden;
            Export.Visibility = Visibility.Hidden;
            TypeFilter.Visibility = Visibility.Hidden;
            ColumnFilter.Visibility = Visibility.Hidden;
        }
        private void SetFilterDropdowns()
        {
            TypeFilter.ItemsSource = Types;

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
        }
        #endregion
    }
}
