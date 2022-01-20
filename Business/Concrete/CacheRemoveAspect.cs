using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Cashing;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Concrete
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)//bir manager da yeni  bir veri eklendiğinde OnSuccess olduğunda casche den getirdiğimiz datayı siliyor.
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
