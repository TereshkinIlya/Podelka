
using Podelka.Model;
using PodelkaViewModel;
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

namespace Podelka
{
    /// <summary>
    /// Логика взаимодействия для NewProdWindow.xaml
    /// </summary>
    public partial class NewProdWindow : Window
    {
        private Purchace SelectedPurchace { get; set; } 
        public NewProdWindow(Purchace SelectedPurchase)
        {
            InitializeComponent();
            SelectedPurchace = SelectedPurchase;
            DataContext = new NewProdViewModel(this, SelectedPurchace);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
