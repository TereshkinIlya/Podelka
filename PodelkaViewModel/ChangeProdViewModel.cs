using Podelka.Behaviour;
using Podelka.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PodelkaViewModel
{
    public class ChangeProdViewModel : INotifyPropertyChanged
    {
        Product SelectedProduct { get; set; }
        MainBehaviour Behaviour { get; set; }
        Window ChangeProdWindow { get; set; }
        private string _name { get; set; }
        private int _quantity { get; set; }
        public ChangeProdViewModel(Window wind, Product SelProd)
        {
            ChangeProdWindow = wind;
            SelectedProduct = SelProd;
            Behaviour = new MainBehaviour();
            _name = SelProd.Name;
            _quantity = SelProd.Quantity;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public string TextName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("TextName");
            }
        }
        public int TextQuantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged("TextQuantity");
            }
        }
        RelayCommand _changeProd;
        public RelayCommand ChangeProd
        {
            get
            {
                return _changeProd ??
                  (_changeProd = new RelayCommand((ob) =>
                  {
                      try
                      {

                          Product prod = Behaviour.GetProduct(SelectedProduct.Id);
                          
                          if (prod != null)
                          {
                              prod.Name = TextName;
                              prod.Quantity = TextQuantity;

                              Behaviour.UpdateProduct(prod);

                              Purchace currPurch = Behaviour.GetPurchace(prod.PurchaceId);

                              MainViewModel.AllProductsStorage.Remove(prod.PurchaceId);
                              
                              MainViewModel.AllProductsStorage.Add(prod.PurchaceId, 
                                  new ObservableCollection<Product>(currPurch.PurchacingList));

                              ChangeProdWindow.Close();
                          }
                          //else MessageBox.Show("Такой продукт уже есть");
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }
    }
}
