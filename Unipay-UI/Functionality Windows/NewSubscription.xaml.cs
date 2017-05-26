using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unipay_Lib;
using Unipay_Lib.Building_Blocks;

namespace Unipay_UI.Functionality_Windows
{
    /// <summary>
    /// Interaction logic for NewSubscription.xaml
    /// </summary>
    public partial class NewSubscription : Window
    {
        Repository repo = Repository.GetRepository();
        Controler control = new Controler();

        string[] types = new string[] { "Mobilsystem", "Kortsystem", "Kunde"};
        string[] delays = new string[] { "Ikke Forsinket", "Forsinket" };
        string[] status = new string[] { "Aktiv", "Inaktiv" };
        List<string> merchantsSource = new List<string>() { "< Ny Kunde >" };

        List<Mobilesystem> mobilesystems;
        List<Cardsystem> cardsystems;
        List<Merchant> merchants;

        public NewSubscription()
        {
            InitializeComponent();

            UpdateInternalLists();

            TypeToCreate.ItemsSource = types;
            TypeToCreate.SelectedIndex = 0;

            DElavonDrop.ItemsSource = delays;
            DElavonDrop.SelectedIndex = 0;

            DNETSDrop.ItemsSource = delays;
            DNETSDrop.SelectedIndex = 0;

            DCPIDrop.ItemsSource = delays;
            DCPIDrop.SelectedIndex = 0;

            StatusDrop.ItemsSource = status;
            StatusDrop.SelectedIndex = 0;

            foreach (Merchant merc in merchants)
            {
                merchantsSource.Add(merc.ToStringM());
            }

            MerchantSelector.ItemsSource = merchantsSource;
            MerchantSelector.SelectedIndex = 0;
        }

        private void TypeToCreate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeToCreate.SelectedIndex == 0)
            {
                EnableInputs();
                
                DCPIDrop.IsEnabled = false;
                TIDInput.IsEnabled = false;
                PhysInput.IsEnabled = false;

            }
            else if (TypeToCreate.SelectedIndex == 1)
            {
                EnableInputs();

                DNETSDrop.IsEnabled = false;
                MacAddInput.IsEnabled = false;
                BoxNameInput.IsEnabled = false;
            }
            else if (TypeToCreate.SelectedIndex == 2)
            {
                DElavonDrop.IsEnabled = false;
                DNETSDrop.IsEnabled = false;
                DCPIDrop.IsEnabled = false;
                StatusDrop.IsEnabled = false;

                MacAddInput.IsEnabled = false;
                BoxNameInput.IsEnabled = false;
                TIDInput.IsEnabled = false;
                PhysInput.IsEnabled = false;

                AddressInput.IsEnabled = false;
                SimNrInput.IsEnabled = false;
                NoteSysInput.IsEnabled = false;

                CrDDateInput.IsEnabled = false;
                CrDMonthInput.IsEnabled = false;
                CrDYearInput.IsEnabled = false;

                ClDDateInput.IsEnabled = false;
                ClDMonthInput.IsEnabled = false;
                ClDYearInput.IsEnabled = false;

                MerchantSelector.IsEnabled = false;
                MerchantSelector.SelectedIndex = 0;
            }
        }

        private void MerchantSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInternalLists();

            if (MerchantSelector.SelectedIndex == 0)
            {
                IDInput.IsReadOnly = false;
                NameInput.IsReadOnly = false;
                FirmInput.IsReadOnly = false;
                MailInput.IsReadOnly = false;
                NoteMercInput.IsReadOnly = false;

                IDInput.Text = "";
                NameInput.Text = "";
                FirmInput.Text = "";
                MailInput.Text = "";
                NoteMercInput.Text = "";
            }
            else
            {
                IDInput.IsReadOnly = true;
                NameInput.IsReadOnly = true;
                FirmInput.IsReadOnly = true;
                MailInput.IsReadOnly = true;
                NoteMercInput.IsReadOnly = true;

                IDInput.Text = merchants[MerchantSelector.SelectedIndex - 1].ID;
                NameInput.Text = merchants[MerchantSelector.SelectedIndex - 1].Name;
                FirmInput.Text = merchants[MerchantSelector.SelectedIndex - 1].Firm;
                MailInput.Text = merchants[MerchantSelector.SelectedIndex - 1].Mail;
                NoteMercInput.Text = merchants[MerchantSelector.SelectedIndex - 1].Note;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (TypeToCreate.SelectedIndex == 0 && MerchantSelector.SelectedIndex != 0)
            {
                string[] crd = new string[] { CrDDateInput.Text, CrDMonthInput.Text, CrDYearInput.Text};
                string[] cld = new string[] { ClDDateInput.Text, ClDMonthInput.Text, ClDYearInput.Text };
                int merchant = MerchantSelector.SelectedIndex - 1;

                control.NewMobil(merchant, delays[DNETSDrop.SelectedIndex], delays[DElavonDrop.SelectedIndex],
                    status[StatusDrop.SelectedIndex], AddressInput.Text, SimNrInput.Text,
                    NoteSysInput.Text, MacAddInput.Text, BoxNameInput.Text, crd, cld);
            }
            else if (TypeToCreate.SelectedIndex == 0 && MerchantSelector.SelectedIndex == 0)
            {
                string[] crd = new string[] { CrDDateInput.Text, CrDMonthInput.Text, CrDYearInput.Text };
                string[] cld = new string[] { ClDDateInput.Text, ClDMonthInput.Text, ClDYearInput.Text };
                string[] data = new string[] { IDInput.Text, NameInput.Text, FirmInput.Text, MailInput.Text, NoteMercInput.Text };

                control.NewMobilAndMerc(data, delays[DNETSDrop.SelectedIndex], delays[DElavonDrop.SelectedIndex],
                    status[StatusDrop.SelectedIndex], AddressInput.Text, SimNrInput.Text,
                    NoteSysInput.Text, MacAddInput.Text, BoxNameInput.Text, crd, cld);
            }
            else if (TypeToCreate.SelectedIndex == 1 && MerchantSelector.SelectedIndex != 0)
            {
                string[] crd = new string[] { CrDDateInput.Text, CrDMonthInput.Text, CrDYearInput.Text };
                string[] cld = new string[] { ClDDateInput.Text, ClDMonthInput.Text, ClDYearInput.Text };
                int merchant = MerchantSelector.SelectedIndex - 1;

                control.NewCard(merchant, delays[DCPIDrop.SelectedIndex], delays[DElavonDrop.SelectedIndex],
                    status[StatusDrop.SelectedIndex], AddressInput.Text, SimNrInput.Text,
                    NoteSysInput.Text, TIDInput.Text, PhysInput.Text, crd, cld);
            }
            else if (TypeToCreate.SelectedIndex == 1 && MerchantSelector.SelectedIndex == 0)
            {
                string[] crd = new string[] { CrDDateInput.Text, CrDMonthInput.Text, CrDYearInput.Text };
                string[] cld = new string[] { ClDDateInput.Text, ClDMonthInput.Text, ClDYearInput.Text };
                string[] data = new string[] { IDInput.Text, NameInput.Text, FirmInput.Text, MailInput.Text, NoteMercInput.Text };

                control.NewCardAndMerc(data, delays[DCPIDrop.SelectedIndex], delays[DElavonDrop.SelectedIndex],
                    status[StatusDrop.SelectedIndex], AddressInput.Text, SimNrInput.Text,
                    NoteSysInput.Text, TIDInput.Text, PhysInput.Text, crd, cld);
            }
            else if (TypeToCreate.SelectedIndex == 2)
            {
                string[] data = new string[] { IDInput.Text, NameInput.Text, FirmInput.Text, MailInput.Text, NoteMercInput.Text };
                control.NewMerc(data);
            }

            Close();
        }
        private void EnableInputs()
        {
            DElavonDrop.IsEnabled = true;
            DNETSDrop.IsEnabled = true;
            DCPIDrop.IsEnabled = true;
            StatusDrop.IsEnabled = true;

            MacAddInput.IsEnabled = true;
            BoxNameInput.IsEnabled = true;
            TIDInput.IsEnabled = true;
            PhysInput.IsEnabled = true;

            AddressInput.IsEnabled = true;
            SimNrInput.IsEnabled = true;
            NoteSysInput.IsEnabled = true;

            CrDDateInput.IsEnabled = true;
            CrDMonthInput.IsEnabled = true;
            CrDYearInput.IsEnabled = true;

            ClDDateInput.IsEnabled = true;
            ClDMonthInput.IsEnabled = true;
            ClDYearInput.IsEnabled = true;

            MerchantSelector.IsEnabled = true;
        }

        private void UpdateInternalLists()
        {
            mobilesystems = repo.GetMobilesystems();
            cardsystems = repo.GetCardsystems();
            merchants = repo.GetMerchants();
        }
    }
}
