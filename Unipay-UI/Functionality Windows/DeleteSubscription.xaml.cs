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
    /// Interaction logic for DeleteSubscription.xaml
    /// </summary>
    public partial class DeleteSubscription : Window
    {
        string[] types = new string[] { "Mobilsystem", "Kortsystem", "Kunde" };

        List<string> objects = new List<string>();

        Repository repo = Repository.GetRepository();
        Controller control = new Controller();

        List<Mobilesystem> mobilesystems;
        List<Cardsystem> cardsystems;
        List<Merchant> merchants;

        public DeleteSubscription()
        {
            InitializeComponent();

            TypeToDelete.ItemsSource = types;
            TypeToDelete.SelectedIndex = 0;
        }

        private void TypeToDelete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInternalLists();

            objects.Clear();
            ObjectToDelete.ItemsSource = null;

            if (TypeToDelete.SelectedIndex == 0) // mobilsystem
            {
                if (mobilesystems.Count() != 0)
                {
                    foreach (var mobile in mobilesystems)
                    {
                        objects.Add(mobile.TostringM());
                    }
                }
            }
            else if (TypeToDelete.SelectedIndex == 1) // cardsystem
            {
                if (cardsystems.Count() != 0)
                {
                    foreach (var card in cardsystems)
                    {
                        objects.Add(card.ToStringC());
                    }
                }
            }
            else if (TypeToDelete.SelectedIndex == 2) // merchant
            {
                if (merchants.Count != 0)
                {
                    foreach (var merc in merchants)
                    {
                        objects.Add(merc.ToStringM());
                    }
                }
            }
            if (objects.Count() != 0)
            {
                ObjectToDelete.ItemsSource = objects;
                ObjectToDelete.SelectedIndex = 0;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            UpdateInternalLists();

            bool ableToDeleteMerc = true;

            if (TypeToDelete.SelectedIndex == 2)
            {
                foreach (var mobile in mobilesystems)
                {
                    if (mobile.Merchant.ID == merchants[ObjectToDelete.SelectedIndex].ID)
                    {
                        ableToDeleteMerc = false;
                    }
                }
                foreach (var card in cardsystems)
                {
                    if (card.Merchant.ID == merchants[ObjectToDelete.SelectedIndex].ID)
                    {
                        ableToDeleteMerc = false;
                    }
                }
            }

            if (ableToDeleteMerc == true)
            {
                if (MessageBox.Show("Er du sikker på du vil slette dette element?", "Advarsel", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    control.Delete(TypeToDelete.SelectedIndex, ObjectToDelete.SelectedIndex);

                    Close();
                }
            }
            else
            {
                MessageBox.Show("Kan ikke slette valgte Kunde da den ejer en eller flere systemer i programmet", "Notification", MessageBoxButton.OK);
            }
        }

        private void UpdateInternalLists()
        {
            mobilesystems = repo.GetMobilesystems();
            cardsystems = repo.GetCardsystems();
            merchants = repo.GetMerchants();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
