using Core.CrossCuttingConcers.Caching.Microsoft;
using Core.CrossCuttingConcers.Caching;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Core.Utilities.IoC;
namespace Core.DepensdencyResolver
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            // IServiceCollection parametresi aracılığıyla servisleri eklemek için kullanılır.

            // AddMemoryCache() metodu, uygulama içinde MemoryCache servisini kullanabilmemizi sağlar.
            serviceCollection.AddMemoryCache();

            // AddSingleton() metodu, IHttpContextAccessor arayüzünü uygulamada tek bir örnek olarak kullanılacak şekilde kaydeder.
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // AddSingleton() metodu, ICacheManager arayüzünü uygulamada tek bir örnek olarak MemoryCacheManager sınıfıyla kaydeder.
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();

            // AddSingleton() metodu, Stopwatch sınıfını uygulamada tek bir örnek olarak kullanılacak şekilde kaydeder.
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}