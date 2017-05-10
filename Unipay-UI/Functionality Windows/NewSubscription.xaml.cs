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
    /// Interaction logic for NewSubscription.xaml
    /// </summary>
    public partial class NewSubscription : Window
    {
        Repository repo = Repository.GetRepository();
        Control control = new Control();

        public NewSubscription()
        {
            InitializeComponent();
        }

        private void TypeToCreate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MerchantSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {


            Close();
        }
    }
}
