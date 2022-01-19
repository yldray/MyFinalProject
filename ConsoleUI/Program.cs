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
            //Data Transformation Object
             //ProductTest();
            //IoC
            //CategoryTest();

        }

        //private static void CategoryTest()
        //{
        //    CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        //    foreach (var categor in categoryManager.GetAll())
        //    {
        //        Console.WriteLine(categor.CategoryName);

        //    }
        //}

        //private static void ProductTest()
        //{
        //    ProductManager productManager = new ProductManager(new EfProductDal());

        //    var result = productManager.GetProductDetails();
        //    if (result.Success==true)
        //    {
        //        foreach (var product in result.Data)
        //        {
        //            Console.WriteLine(product.ProductName + "/" + product.CategoryName);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine(result.Message);
        //    }
            
        //}
    }
}
