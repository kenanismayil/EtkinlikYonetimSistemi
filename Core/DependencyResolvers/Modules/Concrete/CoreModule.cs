using Core.CrossCuttingConcerns.Caching.Abstract;
using Core.CrossCuttingConcerns.Caching.Concrete.Microsoft;
using Core.Utilities.DependencyResolvers.Modules.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.DependencyResolvers.Modules.Concrete
{
    public class CoreModule : ICoreModule
    {
        //JWT icin IoC Container, HttpContextAccessor -> kullanıcının ilk istek yaparken yanıt alıncaya kadarki context'i barındırır.
        //Tüm uygulamalrımızda bu injection'ı kullanabileceğimiz için Core katmanına taşıdım.
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();     //Bellekte IMemoryCache için instance oluşturulur. _memoryCache'in karşılığı artık olmuş olur.
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
