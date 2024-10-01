using Castle.DynamicProxy;
using Core.CrossCuttingConcers.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;// Önbellekten silinecek öğelerin desenini belirten desen
        private ICacheManager _cacheManager; // Önbelleği yöneten ICacheManager arayüzü

        // Constructor: Önbellekten silinecek öğelerin desenini alır
        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            // ICacheManager servisini Dependency Injection ile alır
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {// Önbellekten belirtilen desen (pattern) ile eşleşen öğeleri silme işlemi yapılır
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
