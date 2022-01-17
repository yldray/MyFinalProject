using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspect.Autofac.Validation
{
     public class ValidationAspect:MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil ");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            //çalışma anında bir şeyleri çalıştırmaya yarıyor
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            // örnek productvalidatorun base type ını bul ve onun çalışma tipini ve parametlerini bul
            // (invocaliton metot demek) her birini tek tek gez ve
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator,entity);//validation toolsu kullanarak valide et....!!
            }
        }
    }
}
