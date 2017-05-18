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
        Controler control = new Controler();

        public DeleteSubscription()
        {
            InitializeComponent();

            TypeToDelete.ItemsSource = types;
            TypeToDelete.SelectedIndex = 0;


        }

        private void TypeToDelete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
