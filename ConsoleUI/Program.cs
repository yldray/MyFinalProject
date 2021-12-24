using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    internal class Program
    {

        //SOLID
        //Open Closed Principle
        // Yaptığın yazılıma yeni bir özellik ekliyorsan mevcut kodlara dokunamazsın.

        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            foreach (var product  in productManager.GetByUnitProice(2,300))
            {
                Console.WriteLine(product.ProductName);
            }
            
        }
    }
}
