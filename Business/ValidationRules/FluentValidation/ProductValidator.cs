using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p=>p.CategoryId==1);

            //Senaryo Biz ürün eklemek istiyoruz fakat ürünlerin ismi a ile başlamalı gibi bir kural
            RuleFor(p => p.ProductName).Must(StartWhitA).WithMessage("Ürünler A harfi ile başlamalı.");
        }

        //A ile başlıyorsa true döner başlamıyor ise false döner ve RuleFor patlar 
        private bool StartWhitA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
