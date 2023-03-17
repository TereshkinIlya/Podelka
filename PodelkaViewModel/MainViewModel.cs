using Podelka.Behaviour;
using Podelka.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PodelkaViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainBehaviour Behaviour { get; set; }

        private Product _selectedProduct;

        private Purchace _selectedPurchace;

        public static ObservableCollection<Product>? _prodCollection;
        public static ObservableCollection<Purchace>? PurchCollection { get; set; }
        public static Dictionary<int, ObservableCollection<Product>> AllProductsStorage { get; set; }

        public MainViewModel()
        {
            Behaviour = new MainBehaviour();
            PurchCollection = new ObservableCollection<Purchace>(Behaviour.GetAllPurchaces());
            AllProductsStorage = new Dictionary<int, ObservableCollection<Product>>();
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }
        public Purchace SelectedPurchace
        {
            get { return _selectedPurchace; }
            set
            {
                _selectedPurchace = value;
                OnPropertyChanged("SelectedPurchace");
            }
        }

        public ObservableCollection<Product> ProdCollection
        {
            get { return _prodCollection; }
            set
            {
                _prodCollection = value;
                OnPropertyChanged("ProdCollection");
            }
        }

        RelayCommand _removeProd;
        public RelayCommand RemoveProd
        {
            get
            {
                return _removeProd ??
                  (_removeProd = new RelayCommand((ob) =>
                  {
                      try
                      {
                          if (SelectedProduct != null)
                          {
                              Behaviour.DeleteProduct(SelectedProduct);
                              ProdCollection.Remove(SelectedProduct);
                          }
                          else MessageBox.Show("Выберите позицию");
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        RelayCommand _removeList;
        public RelayCommand RemoveList
        {
            get
            {
                return _removeList ??
                  (_removeList = new RelayCommand((ob) =>
                  {
                      try
                      {
                          if (SelectedPurchace != null)
                          {
                              int tempId = SelectedPurchace.Id;

                              Behaviour.DeletePurchace(SelectedPurchace);
                              PurchCollection.Remove(SelectedPurchace);

                              AllProductsStorage[tempId].Clear();

                              SelectedPurchace = null;
                          }
                          else MessageBox.Show("Выберите позицию");
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        RelayCommand _checkedPurchacing;
        public RelayCommand CheckedPurchacing
        {
            get
            {
                return _checkedPurchacing ??
                  (_checkedPurchacing = new RelayCommand((ob) =>
                  {
                      try
                      {
                          Product prod = Behaviour.GetProduct((int)ob);
                          prod.IsBought = true;
                          Behaviour?.UpdateProduct(prod);
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        RelayCommand _unCheckedPurchacing;
        public RelayCommand UnCheckedPurchacing
        {
            get
            {
                return _unCheckedPurchacing ??
                  (_unCheckedPurchacing = new RelayCommand((ob) =>
                  {
                      try
                      {
                          Product prod = Behaviour.GetProduct((int)ob);
                          prod.IsBought = false;
                          Behaviour?.UpdateProduct(prod);
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }
        RelayCommand _loadProductInList;
        public RelayCommand LoadProductInList
        {
            get
            {
                return _loadProductInList ??
                  (_loadProductInList = new RelayCommand((ob) =>
                  {
                      try
                      {
                          Purchace currPurchace;
                          if (SelectedPurchace != null)
                          {
                              currPurchace = Behaviour.GetPurchace(SelectedPurchace.Id);

                              if (AllProductsStorage.ContainsKey(SelectedPurchace.Id))
                              {
                                  ProdCollection = AllProductsStorage[SelectedPurchace.Id];
                              }
                              else
                              {
                                  ProdCollection = new ObservableCollection<Product>(currPurchace.PurchacingList);
                                  AllProductsStorage.Add(SelectedPurchace.Id, ProdCollection);
                              }
                          }
                          else
                              return;
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
