﻿using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    // I interface Product  Dal hangi katmana karşılık geldiğini Dal data Acces Layer  Dau
    public interface IProductDal:IEntityRepository<Product>
    {
       
        List<ProductDetailDto> GetProductDetails();
    }
}

//Code Refactoring
