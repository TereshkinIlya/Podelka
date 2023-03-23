using Podelka.Behaviour;
using Podelka.Model;
using PodelkaViewModel.MessageBus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PodelkaViewModel
{
    public class NewListViewModel : INotifyPropertyChanged
    {
        MainBehaviour Behaviour { get; set; }
        Window AddListWindow { get; set; }
        private string _name { get; set; }
        private DateTime _date { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public NewListViewModel(Window wind)
        {
            Behaviour = new MainBehaviour();
            AddListWindow = wind;
            _date= DateTime.Now;
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
        public DateTime TextDate
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("TextDate");
            }
        }
        RelayCommand _addList;
        public RelayCommand AddList
        {
            get
            {
                return _addList ??
                  (_addList = new RelayCommand((ob) =>
                  {
                      try
                      {
                          Purchace NewList = new Purchace()
                          {
                              Name = TextName,
                              DateofPurhchace = DateOnly.FromDateTime(TextDate),
                          };
                          Purchace list = Behaviour.GetPurchace(NewList);

                          if (list == null)
                          {
                              MessengerStatic.SendAdd(NewList);
                              Behaviour.PostPurchace(NewList);
                              
                              AddListWindow.Close();
                          }
                          else MessageBox.Show("Такой список уже есть");
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
