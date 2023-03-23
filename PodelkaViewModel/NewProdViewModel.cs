using Podelka.Behaviour;
using Podelka.Model;
using PodelkaViewModel.MessageBus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace PodelkaViewModel
{
    public class NewProdViewModel : INotifyPropertyChanged
    {
        MainBehaviour Behaviour { get; set; }
        Window AddProdWindow { get; set; }
        private string _name { get; set; }
        private int _quantity { get; set; }
        private Purchace SelectedPurchace { get; set; }
        public NewProdViewModel(Window wind, Purchace SelPurch)
        {
            Behaviour = new MainBehaviour();
            AddProdWindow = wind;
            SelectedPurchace = SelPurch;
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

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        RelayCommand _addProd;
        public RelayCommand AddProd
        {
            get
            {
                return _addProd ??
                  (_addProd = new RelayCommand((ob) =>
                  {
                      try
                      {

                          Product NewProduct = new Product()
                          {
                              Name = TextName,
                              Quantity = TextQuantity,
                              IsBought = false
                          };
                          Product prod = Behaviour.GetProduct(NewProduct.Id);

                          if (prod == null)
                          {
                              Purchace currPurchace = Behaviour.GetPurchace(SelectedPurchace.Id);
                              currPurchace.PurchacingList.Add(NewProduct);
                              
                              Behaviour.UpdatePurchace(currPurchace);
                              MessengerStatic.SendAdd(NewProduct);
                              AddProdWindow.Close();
                          }
                          else MessageBox.Show("Такой продукт уже есть");
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
