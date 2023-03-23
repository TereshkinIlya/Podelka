using Podelka.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodelkaViewModel.MessageBus
{
    public static class MessengerStatic
    {
        public static event Action<object> BusAdd;
        public static event Action<object> BusChange;

        public static void SendAdd(object data)        //как все это сделать по-человечески?
        {
            if (data== null) throw new ArgumentNullException();

            Delegate[] subscribers = BusAdd.GetInvocationList();
            foreach (Delegate subscriber in subscribers)
            {
                if (data.GetType() == typeof(Purchace) && subscriber.Method.Name == "AddToPurchCollection")
                {
                    BusAdd?.GetInvocationList()[1].DynamicInvoke(data);
                    break;                                                
                }
                if (data.GetType() == typeof(Product) && subscriber.Method.Name == "AddToProdCollection")
                {
                    BusAdd?.GetInvocationList()[0].DynamicInvoke(data);
                    break;
                }
            }
        }
        public static void SendChange(object data)
        {
            if (data == null) throw new ArgumentNullException();

            Delegate[] subscribers = BusChange.GetInvocationList();
            foreach (Delegate subscriber in subscribers)
            {
                if (data.GetType() == typeof(Product) && subscriber.Method.Name == "ChangeProdInCollections")
                {
                    BusChange?.GetInvocationList()[0].DynamicInvoke(data);
                    break;
                }
            }
        }
    }
}
