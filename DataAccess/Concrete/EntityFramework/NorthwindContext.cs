using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    //Context : Db tabloları ile proje classlarını bağlamak.
    public class NorthwindContext:DbContext
    {
        // PROJENİN HANGİ VERİ TABANI İLE İLİŞKİLİ OLDUĞUNU BELİRTEN METOTDUR.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=127.154.55.5;Database=Bağlanacağımızdbninisminiyaz;KullanıcıAdı=şifre");

            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true");
        }

        public DbSet<Product> Products{ get; set; }
        public DbSet<Customer> Customers{ get; set; }
        public DbSet<Category> Categories{ get; set; }
    }
}
