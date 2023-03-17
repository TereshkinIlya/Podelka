using Podelka.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Podelka.Behaviour.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Podelka.Behaviour
{
    public class MainBehaviour
    {
        AppDB DB { get; set; }
        public Product GetProduct(int Id)
        {
            using (DB = new AppDB())
            {
                return DB.Products.FirstOrDefault(p => p.Id == Id);
            }
        }
        public void PostProduct(Product prod)
        {
            using (DB = new AppDB())
            {
                DB.Products.Add(prod);
                DB.SaveChanges();
            }
        }
        public void UpdateProduct(Product prod) 
        {
            using (DB = new AppDB())
            {
                DB.Products.Update(prod);
                DB.SaveChanges();
            }
        }
        public void DeleteProduct(Product prod)
        {
            using (DB = new AppDB())
            {
                Product product = DB.Products.FirstOrDefault(p => p.Name == prod.Name);
                if (product != null)
                    DB.Remove(product);
                DB.SaveChanges();
            }
        }
        public Purchace[] GetAllPurchaces()
        {
            using (DB = new AppDB())
            {
                DB.Purchaces.Load();
                return DB.Purchaces.ToArray();
            }
        }
        public Purchace GetPurchace(Purchace purchace)
        {
            using (DB = new AppDB())
            {
                return DB.Purchaces.FirstOrDefault(p => p.Id == purchace.Id);
            }
        }
        public Purchace GetPurchace(int Id)
        {
            using (DB = new AppDB())
            {
                Purchace purchace = DB.Purchaces.FirstOrDefault(p => p.Id == Id);
                List<Product> products = DB.Products.Include(p => p.Purchace).
                    Where(p => p.PurchaceId== Id).ToList();
                purchace.PurchacingList = products;
                return purchace;
            }
        }
        public void PostPurchace(Purchace purch)
        {
            using (DB = new AppDB())
            {
                DB.Purchaces.Add(purch);
                DB.SaveChanges();
            }
        }
        public void UpdatePurchace(Purchace purch)
        {
            using (DB = new AppDB())
            {
                DB.Purchaces.Update(purch);
                DB.SaveChanges();
            }
        }
        public void DeletePurchace(Purchace purch)
        {
            using (DB = new AppDB())
            {
                Purchace purchace = DB.Purchaces.FirstOrDefault(p => p.Name == purch.Name);
                if (purchace != null)
                    DB.Remove(purchace);
                DB.SaveChanges();
            }
        }
    }
}
