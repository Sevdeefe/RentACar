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
    public class CacheAspect : MethodInterception
    {
        private int _duration;// Önbellekte saklanacak verinin süresi(varsayılan olarak 60 saniye)
        private ICacheManager _cacheManager;// Önbelleği yönetecek olan ICacheManager arayüzü
        // Constructor: Önbellek süresi parametre olarak alınabilir, varsayılan 60 saniye
        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            // ICacheManager servisini Dependency Injection ile alıyoruz
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {// Methodun tam adını ve argumentlerini kullanarak benzersiz bir önbellek anahtarı oluşturuyoruz
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            // Önbellekte anahtar varsa, önbellekten değeri alıp işlemi sonlandırıyoruz
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
