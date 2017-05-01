using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

namespace Unipay_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] Types = new string[] { "", "Mobilsystem", "Kortsystem", "Kunder"};



        List<Mobilsystem> SRMobil = new List<Mobilsystem>();
        List<Cardsystem> SRCard = new List<Cardsystem>();
        List<Merchant> SRMerc = new List<Merchant>();

        Repository repo = Repository.GetRepository();
        Control control = new Control();

        DataTable phoneView = new DataTable();
        DataTable cardView = new DataTable();
        DataTable mercView = new DataTable();

        Thread update;
        Thread search;

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
            phoneView.Columns.Add("Addresse for Enhed");

            cardView.Columns.Add("Merchant ID");
            cardView.Columns.Add("Status");
            cardView.Columns.Add("Forsinkelse Elavon");
            cardView.Columns.Add("Forsinkelse CPI");
            cardView.Columns.Add("Terminal ID");
            cardView.Columns.Add("Phys ID");
            cardView.Columns.Add("Sim Producent");
            cardView.Columns.Add("Opretelses Dato");
            cardView.Columns.Add("Addresse for Enhed");

            mercView.Columns.Add("Merchant ID");
            mercView.Columns.Add("Navn");
            mercView.Columns.Add("Firma");
            mercView.Columns.Add("Mail");

            update = new Thread(new ThreadStart(UpdateView));
            search = new Thread(new ThreadStart(Search));
            update.Start();
            search.Start();
        }

        private void Search()
        {
            while (true)
            {
                SRMobil.Clear();
                SRCard.Clear();
                SRMerc.Clear();

                if (TypeFilter.SelectedIndex == 0 || TypeFilter.SelectedIndex == 1)
                {
                    foreach (Mobilsystem mobil in repo.GetMobilsystems())
                    {

                    }
                }

                Thread.Sleep(1000);
            }
        }

        public void UpdateView()
        {
            while (true)
            {
                if (PhoneState)
                {
                    MobilGrid.Height = 600;
                    MobilGrid.Visibility = Visibility.Visible;
                    CardGrid.Visibility = Visibility.Hidden;
                    MercGrid.Visibility = Visibility.Hidden;

                    foreach (Mobilsystem mobil in repo.GetMobilsystems())
                    {

                    }
                }
                else if (CardState)
                {
                    CardGrid.Height = 600;
                    MobilGrid.Visibility = Visibility.Hidden;
                    CardGrid.Visibility = Visibility.Visible;
                    MercGrid.Visibility = Visibility.Hidden;

                    foreach (Cardsystem card in repo.GetCardsystems())
                    {

                    }
                }
                else if (MerchantState)
                {
                    MercGrid.Height = 600;
                    MobilGrid.Visibility = Visibility.Hidden;
                    CardGrid.Visibility = Visibility.Hidden;
                    MercGrid.Visibility = Visibility.Visible;

                    foreach (Merchant merc in repo.GetMerchants())
                    {

                    }
                }
                else if (SearchState)
                {
                    if (TypeFilter.SelectedIndex == 0)
                    {
                        MobilGrid.Height = 200;
                        CardGrid.Height = 200;
                        MercGrid.Height = 200;
                        MobilGrid.Margin = new Thickness(0, 0, 0, 400);
                        MercGrid.Margin = new Thickness(0, 400, 0, 0);
                        MobilGrid.Visibility = Visibility.Visible;
                        CardGrid.Visibility = Visibility.Visible;
                        MercGrid.Visibility = Visibility.Visible;
                    }
                    


                }
                Thread.Sleep(2000);
            }
        }

        private void Mobilsystem_Click(object sender, RoutedEventArgs e)
        {
            PhoneState = true;
            CardState = false;
            MerchantState = false;
            SearchState = false;
        }

        private void Kortsystem_Click(object sender, RoutedEventArgs e)
        {
            PhoneState = false;
            CardState = true;
            MerchantState = false;
            SearchState = false;
        }

        private void Søgeresultat_Click(object sender, RoutedEventArgs e)
        {
            PhoneState = false;
            CardState = false;
            MerchantState = false;
            SearchState = true;
        }

        private void Kunde_Click(object sender, RoutedEventArgs e)
        {
            PhoneState = false;
            CardState = false;
            MerchantState = true;
            SearchState = false;
        }

        private void TypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewSubscription_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditSubscription_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteSubscription_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
