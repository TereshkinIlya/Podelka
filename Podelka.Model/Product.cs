using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podelka.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public bool IsBought { get; set; }
        public Purchace Purchace { get; set; }
        public int PurchaceId { get; set; }
    }
    
}
