using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac;
using Core.Aspect.Autofac.Transactional;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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
        ICategoryService _categoryService;
        //** Bir entity manager kendisi hharic başka bir dal enjekte edemez
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }
        
        //[SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")] // Db ye  yeni bir ekleme yaptığımızda gidip hazır olan Cacheleri siliyor eski data gelmesin diye.
        public IResult Add(Product product)
        {
            #region refactoring den önce
            //business codes.
            //validation
            ///// ARTIK VALIDATION KULLANIYORUZ
            ////if (product.ProductName.Length<2) 
            ////{
            ////    //magic string 
            ////    // her yerde yazarız bir yerde değiştirmeyi unuturuz bu yüzden bunu tek bir yerde 
            ////    //standart şekilde yazıyoruz.
            ////    return new ErrorResult(Messages.ProductNameInvalid);
            ////}
            ///
            ////////// Refactoring yapıyoruz
            //////////var context = new ValidationContext<Product>(product);
            //////////ProductValidator productValidator = new ProductValidator();
            //////////var result = productValidator.Validate(context);
            //////////if (!result.IsValid)
            //////////{
            //////////    throw new ValidationException(result.Errors);
            //////////}

            ////ValidationTool.Validate(new ProductValidator(), product); attributes kullanarak buna da gerek kalmadı.
            #endregion

            // business codes 
            // polimorfizm
            IResult result = BusinessRules.Run(CheckIfProductNameExist(product.ProductName),
                                           CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                                           CheckIfCategoryLimitExceded());
            #region refactoring den önce2
            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            //{
            //    if (CheckIfProductNameExist(product.ProductName).Success)
            //    {
            //        _productDal.Add(product);
            //       return new SuccessResult(Messages.ProductAdded);
            //    }
            //}
            // return new ErrorResult();
            #endregion
            if (result != null)
            {
                return result;
            }


            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
            
        }

        [CacheAspect] //key,value
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
        [CacheAspect]
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

        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count();
            if (result >= 30)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }

            return new SuccessResult();
        }
        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }

            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>=15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }

            return new SuccessResult();
        }

      
    }
}
