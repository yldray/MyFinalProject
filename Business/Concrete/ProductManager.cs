﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            //business codes.
            if (product.ProductName.Length<2)
            {
                //magic string 
                // her yerde yazarız bir yerde değiştirmeyi unuturuz bu yüzden bunu tek bir yerde 
                //standart şekilde yazıyoruz.
                return new ErrorResult(Messages.ProductNameInvalid);
            }
           _productDal.Add(product);
            
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            //İş kodları var burada mesela yetkisi varmı gibi bakıyor
            // saat 10 da ürün vermemek için
            //if (DateTime.Now.Hour == 12)
            //{
            //    return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            //}

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
        }
        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
           return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(x => x.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitProice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            // Sistem saati 14 ise Datayı gönderme!
            //////if (DateTime.Now.Hour==14)
            //////{
            //////    return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            //////}
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
    }
}
