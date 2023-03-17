using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podelka.Model
{
    public class Purchace
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Product> PurchacingList { get; set; }
        public DateOnly DateofPurhchace { get; set; }
        public Purchace() => PurchacingList = new List<Product>();
        
    }
}
