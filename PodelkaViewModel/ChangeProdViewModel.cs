using Podelka.Behaviour;
using Podelka.Model;
using PodelkaViewModel.MessageBus;
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
                              MessengerStatic.SendChange(prod);
                              ChangeProdWindow.Close();
                          }
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
