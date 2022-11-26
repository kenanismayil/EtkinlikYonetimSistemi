using Core.Utilities.DependencyResolvers.Modules.Abstract;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    //IServiceCollection -> Bizim Asp.Net uygulamamızın, yani kısacası Api'mizin servis bağımlılıklarını eklediğimiz,
    //ya da araya girmesini istediğimiz servisleri ekledğimiz kolleksiyonlardır.
    //Core katmanı da dahil olmak üzere ekleyeceğimiz bütün injectionları bir arada toplayabileceğimiz yapıya dönüştürmüş olduk.
    public static class ServiceCollectionExtensions
    {
        //Polymorphism yaptım, this -> neyi genişletmek istiyorsak onu veririz.
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, 
            ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }

            return ServiceTool.Create(serviceCollection);
        }
    }
}
