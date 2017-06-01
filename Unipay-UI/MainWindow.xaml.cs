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

        List<Mobilesystem> SRMobile = new List<Mobilesystem>();
        List<Cardsystem> SRCard = new List<Cardsystem>();
        List<Merchant> SRMerc = new List<Merchant>();

        List<Mobilesystem> mobilesystems;
        List<Cardsystem> cardsystems;
        List<Merchant> merchants;

        Repository repo = Repository.GetRepository();
        DataAccessLayer data = new DataAccessLayer();

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

            SearchBox.KeyDown += new KeyEventHandler(ListenEnter_KeyDown);

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

        private void ListenEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search();
                UpdateView();
            }
        }

        private void Search()
        {
            SRMobile.Clear();
            SRCard.Clear();
            SRMerc.Clear();

            UpdateInternalLists();

            var IESRMobil = from mobile in mobilesystems
                            where mobile.Address.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                            mobile.BoxName.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                            mobile.MACAddress.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                            mobile.SimNumber.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                            mobile.ToStringDE().ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                            mobile.ToStringDN().ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                            mobile.ToStringS().ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                            mobile.Merchant.ID.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                            mobile.Note.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                            mobile.CreationDate.ToStringDF().ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                            mobile.CloseingDate.ToStringDF().ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant())
                            select mobile;

            var IESRCard = from card in cardsystems
                           where card.Address.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           card.TerminalID.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           card.PhysicalID.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           card.SimNumber.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           card.ToStringDE().ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           card.ToStringDC().ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           card.ToStringS().ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           card.Merchant.ID.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           card.Note.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           card.CreationDate.ToStringDF().ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           card.CloseingDate.ToStringDF().ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant())
                           select card;

            var IESRMerc = from merc in merchants
                           where merc.ID.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           merc.Name.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           merc.Firm.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           merc.Mail.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant()) ||
                           merc.Note.ToLowerInvariant().Contains(SearchBox.Text.ToLowerInvariant())
                           select merc;


            foreach (var mobile in IESRMobil)
            {
                SRMobile.Add(mobile);
            }

            foreach (var card in IESRCard)
            {
                SRCard.Add(card);
            }

            foreach (var merc in IESRMerc)
            {
                SRMerc.Add(merc);
            }
            
        }

        public void UpdateView()
        {
            phoneView.Clear();
            cardView.Clear();
            mercView.Clear();

            UpdateInternalLists();

            if (PhoneState)
                {
                if (mobilesystems.Count() != 0)
                {
                    MobileGrid.Height = 600;
                    CardGrid.Height = 0;
                    MercGrid.Height = 0;
                    MobileGrid.Margin = new Thickness(0, 0, 0, 0);
                    CardGrid.Margin = new Thickness(0, 0, 0, 0);
                    MercGrid.Margin = new Thickness(0, 0, 0, 0);
                    MobileGrid.Visibility = Visibility.Visible;
                    CardGrid.Visibility = Visibility.Hidden;
                    MercGrid.Visibility = Visibility.Hidden;

                    foreach (Mobilesystem mobile in mobilesystems)
                    {
                        DataRow row = phoneView.NewRow();

                        row["Merchant ID"] = mobile.Merchant.ID;
                        row["Status"] = mobile.ToStringS();
                        row["Forsinkelse Elavon"] = mobile.ToStringDE();
                        row["Forsinkelse NETS"] = mobile.ToStringDN();
                        row["MAC Addresse"] = mobile.MACAddress;
                        row["Boks Navn"] = mobile.BoxName;
                        row["Sim Nummer"] = mobile.SimNumber;
                        row["Opretelses Dato"] = mobile.CreationDate.ToStringDF();
                        if (mobile.CloseingDate != null)
                        {
                            row["Luknings Dato"] = mobile.CloseingDate.ToStringDF();
                        }
                        row["Addresse for Enhed"] = mobile.Address;
                        row["Noter"] = mobile.Note;

                        phoneView.Rows.Add(row);
                    }
                    MobileGrid.ItemsSource = phoneView.AsDataView();
                }
                else
                {
                    MobileGrid.Height = 0;
                    CardGrid.Height = 0;
                    MercGrid.Height = 0;
                    MobileGrid.Margin = new Thickness(0, 0, 0, 0);
                    CardGrid.Margin = new Thickness(0, 0, 0, 0);
                    MercGrid.Margin = new Thickness(0, 0, 0, 0);
                    MobileGrid.Visibility = Visibility.Hidden;
                    CardGrid.Visibility = Visibility.Hidden;
                    MercGrid.Visibility = Visibility.Hidden;
                }
                
                
            }
                else if (CardState)
                {
                if (cardsystems.Count() != 0)
                {
                    MobileGrid.Height = 0;
                    CardGrid.Height = 600;
                    MercGrid.Height = 0;
                    MobileGrid.Margin = new Thickness(0, 0, 0, 0);
                    CardGrid.Margin = new Thickness(0, 0, 0, 0);
                    MercGrid.Margin = new Thickness(0, 0, 0, 0);
                    MobileGrid.Visibility = Visibility.Hidden;
                    CardGrid.Visibility = Visibility.Visible;
                    MercGrid.Visibility = Visibility.Hidden;

                    foreach (Cardsystem card in cardsystems)
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
                        if (card.CloseingDate != null)
                        {
                            row["Luknings Dato"] = card.CloseingDate.ToStringDF();
                        }
                        row["Addresse for Enhed"] = card.Address;
                        row["Noter"] = card.Note;

                        cardView.Rows.Add(row);
                    }
                    CardGrid.ItemsSource = cardView.AsDataView();
                }
                else
                {
                    MobileGrid.Height = 0;
                    CardGrid.Height = 0;
                    MercGrid.Height = 0;
                    MobileGrid.Margin = new Thickness(0, 0, 0, 0);
                    CardGrid.Margin = new Thickness(0, 0, 0, 0);
                    MercGrid.Margin = new Thickness(0, 0, 0, 0);
                    MobileGrid.Visibility = Visibility.Hidden;
                    CardGrid.Visibility = Visibility.Hidden;
                    MercGrid.Visibility = Visibility.Hidden;
                }
                
                
            }
                else if (MerchantState)
                {
                if (merchants.Count() != 0)
                {
                    MobileGrid.Height = 0;
                    CardGrid.Height = 0;
                    MercGrid.Height = 600;
                    MobileGrid.Margin = new Thickness(0, 0, 0, 0);
                    CardGrid.Margin = new Thickness(0, 0, 0, 0);
                    MercGrid.Margin = new Thickness(0, 0, 0, 0);
                    MobileGrid.Visibility = Visibility.Hidden;
                    CardGrid.Visibility = Visibility.Hidden;
                    MercGrid.Visibility = Visibility.Visible;

                    foreach (Merchant merc in merchants)
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
                    MobileGrid.Height = 0;
                    CardGrid.Height = 0;
                    MercGrid.Height = 0;
                    MobileGrid.Margin = new Thickness(0, 0, 0, 0);
                    CardGrid.Margin = new Thickness(0, 0, 0, 0);
                    MercGrid.Margin = new Thickness(0, 0, 0, 0);
                    MobileGrid.Visibility = Visibility.Hidden;
                    CardGrid.Visibility = Visibility.Hidden;
                    MercGrid.Visibility = Visibility.Hidden;
                }
                
                
            }
                else if (SearchState)
                {
                    if (SRMobile.Count != 0 && SRCard.Count != 0 && SRMerc.Count != 0) // all have hits
                    {
                        MobileGrid.Height = 200;
                        CardGrid.Height = 200;
                        MercGrid.Height = 200;
                        MobileGrid.Margin = new Thickness(0, 0, 0, 400);
                        CardGrid.Margin = new Thickness(0, 0, 0, 0);
                        MercGrid.Margin = new Thickness(0, 400, 0, 0);
                        MobileGrid.Visibility = Visibility.Visible;
                        CardGrid.Visibility = Visibility.Visible;
                        MercGrid.Visibility = Visibility.Visible;
                    }
                    else if (SRMobile.Count != 0 && SRCard.Count != 0 && SRMerc.Count == 0) // all but Merc have hits
                    {
                        MobileGrid.Height = 300;
                        CardGrid.Height = 300;
                        MercGrid.Height = 0;
                        MobileGrid.Margin = new Thickness(0, 0, 0, 300);
                        CardGrid.Margin = new Thickness(0, 300, 0, 0);
                        MercGrid.Margin = new Thickness(0, 0, 0, 0);
                        MobileGrid.Visibility = Visibility.Visible;
                        CardGrid.Visibility = Visibility.Visible;
                        MercGrid.Visibility = Visibility.Hidden;
                    }
                    else if (SRMobile.Count != 0 && SRCard.Count == 0 && SRMerc.Count != 0) // all but Card have hits
                    {
                        MobileGrid.Height = 300;
                        CardGrid.Height = 0;
                        MercGrid.Height = 300;
                        MobileGrid.Margin = new Thickness(0, 0, 0, 300);
                        CardGrid.Margin = new Thickness(0, 0, 0, 0);
                        MercGrid.Margin = new Thickness(0, 300, 0, 0);
                        MobileGrid.Visibility = Visibility.Visible;
                        CardGrid.Visibility = Visibility.Hidden;
                        MercGrid.Visibility = Visibility.Visible;
                    }
                    else if (SRMobile.Count == 0 && SRCard.Count != 0 && SRMerc.Count != 0) // all but Mobil have hits
                    {
                        MobileGrid.Height = 0;
                        CardGrid.Height = 300;
                        MercGrid.Height = 300;
                        MobileGrid.Margin = new Thickness(0, 0, 0, 0);
                        CardGrid.Margin = new Thickness(0, 0, 0, 300);
                        MercGrid.Margin = new Thickness(0, 300, 0, 0);
                        MobileGrid.Visibility = Visibility.Hidden;
                        CardGrid.Visibility = Visibility.Visible;
                        MercGrid.Visibility = Visibility.Visible;
                    }
                    else if (SRMobile.Count != 0 && SRCard.Count == 0 && SRMerc.Count == 0) // Mobil have hits
                    {
                        MobileGrid.Height = 600;
                        CardGrid.Height = 0;
                        MercGrid.Height = 0;
                        MobileGrid.Margin = new Thickness(0, 0, 0, 0);
                        CardGrid.Margin = new Thickness(0, 0, 0, 0);
                        MercGrid.Margin = new Thickness(0, 0, 0, 0);
                        MobileGrid.Visibility = Visibility.Visible;
                        CardGrid.Visibility = Visibility.Hidden;
                        MercGrid.Visibility = Visibility.Hidden;
                    }
                    else if (SRMobile.Count == 0 && SRCard.Count != 0 && SRMerc.Count == 0) // Card have hits
                    {
                        MobileGrid.Height = 0;
                        CardGrid.Height = 600;
                        MercGrid.Height = 0;
                        MobileGrid.Margin = new Thickness(0, 0, 0, 0);
                        CardGrid.Margin = new Thickness(0, 0, 0, 0);
                        MercGrid.Margin = new Thickness(0, 0, 0, 0);
                        MobileGrid.Visibility = Visibility.Hidden;
                        CardGrid.Visibility = Visibility.Visible;
                        MercGrid.Visibility = Visibility.Hidden;
                    }
                    else if (SRMobile.Count == 0 && SRCard.Count == 0 && SRMerc.Count != 0) // Merc have hits
                    {
                        MobileGrid.Height = 0;
                        CardGrid.Height = 0;
                        MercGrid.Height = 600;
                        MobileGrid.Margin = new Thickness(0, 0, 0, 0);
                        CardGrid.Margin = new Thickness(0, 0, 0, 0);
                        MercGrid.Margin = new Thickness(0, 0, 0, 0);
                        MobileGrid.Visibility = Visibility.Hidden;
                        CardGrid.Visibility = Visibility.Hidden;
                        MercGrid.Visibility = Visibility.Visible;
                    }
                    else if (SRMobile.Count == 0 && SRCard.Count == 0 && SRMerc.Count == 0) // None have hits
                    {
                        MobileGrid.Height = 0;
                        CardGrid.Height = 0;
                        MercGrid.Height = 0;
                        MobileGrid.Margin = new Thickness(0, 0, 0, 0);
                        CardGrid.Margin = new Thickness(0, 0, 0, 0);
                        MercGrid.Margin = new Thickness(0, 0, 0, 0);
                        MobileGrid.Visibility = Visibility.Hidden;
                        CardGrid.Visibility = Visibility.Hidden;
                        MercGrid.Visibility = Visibility.Hidden;
                    }
                if (SRMobile.Count != 0)
                {
                    SetMobilGrid();
                }
                if (SRCard.Count != 0)
                {
                    SetCardGrid();
                }
                if (SRMerc.Count != 0)
                {
                    SetMercGrid();
                }
            }
        }

        private void SetMobilGrid()
        {
            foreach (Mobilesystem mobile in SRMobile)
            {
                DataRow row = phoneView.NewRow();

                row["Merchant ID"] = mobile.Merchant.ID;
                row["Status"] = mobile.ToStringS();
                row["Forsinkelse Elavon"] = mobile.ToStringDE();
                row["Forsinkelse NETS"] = mobile.ToStringDN();
                row["MAC Addresse"] = mobile.MACAddress;
                row["Boks Navn"] = mobile.BoxName;
                row["Sim Nummer"] = mobile.SimNumber;
                row["Opretelses Dato"] = mobile.CreationDate.ToStringDF();
                row["Luknings Dato"] = mobile.CloseingDate.ToStringDF();
                row["Addresse for Enhed"] = mobile.Address;
                row["Noter"] = mobile.Note;

                phoneView.Rows.Add(row);
            }
            MobileGrid.ItemsSource = phoneView.AsDataView();
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
                row["Luknings Dato"] = card.CloseingDate.ToStringDF();
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

        private void Mobilesystem_Click(object sender, RoutedEventArgs e)
        {
            PhoneState = true;
            CardState = false;
            MerchantState = false;
            SearchState = false;

            UpdateView();
        }

        private void Cardsystem_Click(object sender, RoutedEventArgs e)
        {
            PhoneState = false;
            CardState = true;
            MerchantState = false;
            SearchState = false;

            UpdateView();
        }

        private void SearchResult_Click(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == "")
            {
                SRMobile.Clear();
                SRCard.Clear();
                SRMerc.Clear();
            }

            PhoneState = false;
            CardState = false;
            MerchantState = false;
            SearchState = true;

            UpdateView();
        }

        private void Merchant_Click(object sender, RoutedEventArgs e)
        {
            PhoneState = false;
            CardState = false;
            MerchantState = true;
            SearchState = false;

            UpdateView();
        }
        
        private void NewSubscription_Click(object sender, RoutedEventArgs e)
        {
            NewSubscription newsub = new NewSubscription();
            newsub.Title = "Opret";

            newsub.ShowDialog();

            data.ExportBackup();

            UpdateView();
        }

        private void EditSubscription_Click(object sender, RoutedEventArgs e)
        {
            EditSubscription editsub = new EditSubscription();
            editsub.Title = "Edit";

            editsub.ShowDialog();

            data.ExportBackup();

            UpdateView();
        }

        private void DeleteSubscription_Click(object sender, RoutedEventArgs e)
        {
            DeleteSubscription delsub = new DeleteSubscription();
            delsub.Title = "Delete";

            delsub.ShowDialog();

            data.ExportBackup();

            UpdateView();
        }

        private void UpdateInternalLists()
        {
            mobilesystems = repo.GetMobilesystems();
            cardsystems = repo.GetCardsystems();
            merchants = repo.GetMerchants();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region Unused
        private void ColumnFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
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
