using Podelka.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Podelka.Behaviour.DataBase
{
    public class AppDB : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Purchace> Purchaces { get; set; } = null!;

        public AppDB()
        {
            Database.EnsureCreated();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=PodelkaDB.db");
        }
    }
}
