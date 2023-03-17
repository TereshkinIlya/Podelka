
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PurchList.SelectedItem == null)
            {
                MessageBox.Show("Выберите список");
                return;
            }

            NewProdWindow newProd = new NewProdWindow((Purchace)PurchList.SelectedItem);
            newProd.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NewListWindow newList = new NewListWindow();
            newList.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (ProdList.SelectedItem == null)
            {
                MessageBox.Show("Выберите продукт");
                return;
            }
            ChangeProdWindow changeProd = new ChangeProdWindow((Product)ProdList.SelectedItem);
            changeProd.ShowDialog();
        }
    }
}
