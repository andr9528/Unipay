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
    /// Interaction logic for EditSubscription.xaml
    /// </summary>
    public partial class EditSubscription : Window
    {
        //1305921897 - Simons CPR nr

        Repository repo = Repository.GetRepository();
        Controller control = new Controller();

        string[] types = new string[] { "Mobilsystem", "Kortsystem", "Kunde" };
        string[] delays = new string[] { "Ikke Forsinket", "Forsinket" };
        string[] status = new string[] { "Aktiv", "Inaktiv" };

        List<string> objects = new List<string>();

        List<Mobilesystem> mobilesystems;
        List<Cardsystem> cardsystems;
        List<Merchant> merchants;

        public EditSubscription()
        {
            InitializeComponent();

            UpdateInternalLists();

            TypeToEdit.ItemsSource = types;
            TypeToEdit.SelectedIndex = 0;

            DElavonDrop.ItemsSource = delays;
            DElavonDrop.SelectedIndex = 0;

            DNETSDrop.ItemsSource = delays;
            DNETSDrop.SelectedIndex = 0;

            DCPIDrop.ItemsSource = delays;
            DCPIDrop.SelectedIndex = 0;

            StatusDrop.ItemsSource = status;
            StatusDrop.SelectedIndex = 0;
        }

        private void TypeToEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInternalLists();
            ResetInputs();

            objects.Clear();
            ObjectToEdit.ItemsSource = null;

            if (TypeToEdit.SelectedIndex == 0)
            {
                foreach (var mobile in mobilesystems)
                {
                    objects.Add(mobile.TostringM());
                }

                EnableInputs();

                DCPIDrop.IsEnabled = false;
                TIDInput.IsEnabled = false;
                PhysInput.IsEnabled = false;
                IDInput.IsEnabled = false;
                NameInput.IsEnabled = false;
                FirmInput.IsEnabled = false;
                MailInput.IsEnabled = false;
                NoteMercInput.IsEnabled = false;
            }
            else if (TypeToEdit.SelectedIndex == 1)
            {
                foreach (var card in cardsystems)
                {
                    objects.Add(card.ToStringC());
                }

                EnableInputs();

                DNETSDrop.IsEnabled = false;
                MacAddInput.IsEnabled = false;
                BoxNameInput.IsEnabled = false;
                IDInput.IsEnabled = false;
                NameInput.IsEnabled = false;
                FirmInput.IsEnabled = false;
                MailInput.IsEnabled = false;
                NoteMercInput.IsEnabled = false;
            }
            else if (TypeToEdit.SelectedIndex == 2)
            {
                foreach (var merc in merchants)
                {
                    objects.Add(merc.ToStringM());
                }

                EnableInputs();

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
            }

            if (objects.Count() != 0)
            {
                ObjectToEdit.ItemsSource = objects;
                ObjectToEdit.SelectedIndex = 0;
            }
        }
        private void ObjectToEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInternalLists();
            ResetInputs();

            if (TypeToEdit.SelectedIndex == 0 && ObjectToEdit.SelectedIndex != -1)
            {
                if (mobilesystems[ObjectToEdit.SelectedIndex].DelayElavon != false)
                {
                    DElavonDrop.SelectedIndex = 1;
                }
                if (mobilesystems[ObjectToEdit.SelectedIndex].DelayNETS != false)
                {
                    DNETSDrop.SelectedIndex = 1;
                }
                if (mobilesystems[ObjectToEdit.SelectedIndex].Status != true)
                {
                    StatusDrop.SelectedIndex = 1;
                }

                MacAddInput.Text = mobilesystems[ObjectToEdit.SelectedIndex].MACAddress;
                BoxNameInput.Text = mobilesystems[ObjectToEdit.SelectedIndex].BoxName;

                AddressInput.Text = mobilesystems[ObjectToEdit.SelectedIndex].Address;
                SimNrInput.Text = mobilesystems[ObjectToEdit.SelectedIndex].SimNumber;
                NoteSysInput.Text = mobilesystems[ObjectToEdit.SelectedIndex].Note;

                CrDDateInput.Text = "" + mobilesystems[ObjectToEdit.SelectedIndex].CreationDate.Day;
                CrDMonthInput.Text = "" + mobilesystems[ObjectToEdit.SelectedIndex].CreationDate.Month;
                CrDYearInput.Text = "" + mobilesystems[ObjectToEdit.SelectedIndex].CreationDate.Year;

                ClDDateInput.Text = "" + mobilesystems[ObjectToEdit.SelectedIndex].CloseingDate.Day;
                ClDMonthInput.Text = "" + mobilesystems[ObjectToEdit.SelectedIndex].CloseingDate.Month;
                ClDYearInput.Text = "" + mobilesystems[ObjectToEdit.SelectedIndex].CloseingDate.Year;

                IDInput.Text = mobilesystems[ObjectToEdit.SelectedIndex].Merchant.ID;
                NameInput.Text = mobilesystems[ObjectToEdit.SelectedIndex].Merchant.Name;
                FirmInput.Text = mobilesystems[ObjectToEdit.SelectedIndex].Merchant.Firm;
                MailInput.Text = mobilesystems[ObjectToEdit.SelectedIndex].Merchant.Mail;
                NoteMercInput.Text = mobilesystems[ObjectToEdit.SelectedIndex].Merchant.Note;
            }
            else if (TypeToEdit.SelectedIndex == 1 && ObjectToEdit.SelectedIndex != -1)
            {
                if (cardsystems[ObjectToEdit.SelectedIndex].DelayElavon != false)
                {
                    DElavonDrop.SelectedIndex = 1;
                }
                if (cardsystems[ObjectToEdit.SelectedIndex].DelayCPI != false)
                {
                    DCPIDrop.SelectedIndex = 1;
                }
                if (cardsystems[ObjectToEdit.SelectedIndex].Status != true)
                {
                    StatusDrop.SelectedIndex = 1;
                }

                TIDInput.Text = cardsystems[ObjectToEdit.SelectedIndex].TerminalID;
                PhysInput.Text = cardsystems[ObjectToEdit.SelectedIndex].PhysicalID;

                AddressInput.Text = cardsystems[ObjectToEdit.SelectedIndex].Address;
                SimNrInput.Text = cardsystems[ObjectToEdit.SelectedIndex].SimNumber;
                NoteSysInput.Text = cardsystems[ObjectToEdit.SelectedIndex].Note;

                CrDDateInput.Text = "" + cardsystems[ObjectToEdit.SelectedIndex].CreationDate.Day;
                CrDMonthInput.Text = "" + cardsystems[ObjectToEdit.SelectedIndex].CreationDate.Month;
                CrDYearInput.Text = "" + cardsystems[ObjectToEdit.SelectedIndex].CreationDate.Year;

                ClDDateInput.Text = "" + cardsystems[ObjectToEdit.SelectedIndex].CloseingDate.Day;
                ClDMonthInput.Text = "" + cardsystems[ObjectToEdit.SelectedIndex].CloseingDate.Month;
                ClDYearInput.Text = "" + cardsystems[ObjectToEdit.SelectedIndex].CloseingDate.Year;

                IDInput.Text = cardsystems[ObjectToEdit.SelectedIndex].Merchant.ID;
                NameInput.Text = cardsystems[ObjectToEdit.SelectedIndex].Merchant.Name;
                FirmInput.Text = cardsystems[ObjectToEdit.SelectedIndex].Merchant.Firm;
                MailInput.Text = cardsystems[ObjectToEdit.SelectedIndex].Merchant.Mail;
                NoteMercInput.Text = cardsystems[ObjectToEdit.SelectedIndex].Merchant.Note; ;
            }
            else if (TypeToEdit.SelectedIndex == 2 && ObjectToEdit.SelectedIndex != -1)
            {
                IDInput.Text = merchants[ObjectToEdit.SelectedIndex].ID;
                NameInput.Text = merchants[ObjectToEdit.SelectedIndex].Name;
                FirmInput.Text = merchants[ObjectToEdit.SelectedIndex].Firm;
                MailInput.Text = merchants[ObjectToEdit.SelectedIndex].Mail;
                NoteMercInput.Text = merchants[ObjectToEdit.SelectedIndex].Note;
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            UpdateInternalLists();

            control.Delete(TypeToEdit.SelectedIndex, ObjectToEdit.SelectedIndex);

            if (TypeToEdit.SelectedIndex == 0)
            {
                string[] crd = new string[] { CrDDateInput.Text, CrDMonthInput.Text, CrDYearInput.Text };
                string[] cld = new string[] { ClDDateInput.Text, ClDMonthInput.Text, ClDYearInput.Text };
                Merchant merchant = null;

                foreach (var merc in merchants)
                {
                    if (merc.ID == IDInput.Text)
                    {
                        merchant = merc;
                    }
                }

                control.NewMobile(merchant, delays[DNETSDrop.SelectedIndex], delays[DElavonDrop.SelectedIndex],
                    status[StatusDrop.SelectedIndex], AddressInput.Text, SimNrInput.Text,
                    NoteSysInput.Text, MacAddInput.Text, BoxNameInput.Text, crd, cld);

            }
            else if (TypeToEdit.SelectedIndex == 1)
            {
                string[] crd = new string[] { CrDDateInput.Text, CrDMonthInput.Text, CrDYearInput.Text };
                string[] cld = new string[] { ClDDateInput.Text, ClDMonthInput.Text, ClDYearInput.Text };
                Merchant merchant = null;

                foreach (var merc in merchants)
                {
                    if (merc.ID == IDInput.Text)
                    {
                        merchant = merc;
                    }
                }

                control.NewCard(merchant, delays[DCPIDrop.SelectedIndex], delays[DElavonDrop.SelectedIndex],
                    status[StatusDrop.SelectedIndex], AddressInput.Text, SimNrInput.Text,
                    NoteSysInput.Text, TIDInput.Text, PhysInput.Text, crd, cld);
            }
            else if (TypeToEdit.SelectedIndex == 2)
            {
                string[] data = new string[] { IDInput.Text, NameInput.Text, FirmInput.Text, MailInput.Text, NoteMercInput.Text };
                control.NewMerc(data);
            }

            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void UpdateInternalLists()
        {
            mobilesystems = repo.GetMobilesystems();
            cardsystems = repo.GetCardsystems();
            merchants = repo.GetMerchants();
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

            IDInput.IsEnabled = true;
            NameInput.IsEnabled = true;
            FirmInput.IsEnabled = true;
            MailInput.IsEnabled = true;
            NoteMercInput.IsEnabled = true;
        }
        private void ResetInputs()
        {
            DElavonDrop.SelectedIndex = 0;
            DNETSDrop.SelectedIndex = 0;
            DCPIDrop.SelectedIndex = 0;
            StatusDrop.SelectedIndex = 0;

            MacAddInput.Text = "";
            BoxNameInput.Text = "";
            TIDInput.Text = "";
            PhysInput.Text = "";

            AddressInput.Text = "";
            SimNrInput.Text = "";
            NoteSysInput.Text = "";

            CrDDateInput.Text = "";
            CrDMonthInput.Text = "";
            CrDYearInput.Text = "";

            ClDDateInput.Text = "";
            ClDMonthInput.Text = "";
            ClDYearInput.Text = "";

            IDInput.Text = "";
            NameInput.Text = "";
            FirmInput.Text = "";
            MailInput.Text = "";
            NoteMercInput.Text = "";
        }


    }
}
