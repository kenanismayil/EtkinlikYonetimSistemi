using Castle.DynamicProxy;
using Core.Aspects.Autofac.Performance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            //class'ın attribute'larını okur
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            //method'un attribute'^larını okur
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            //Ve onları Listeye koyar
            classAttributes.AddRange(methodAttributes);

            //Sistemde bütün metotları 5 saniyede bir yeniler ve performansı yükselerek sistemin daha hızlı çalışmasını sağlar
            classAttributes.Add(new PerformanceAspect(5));

            // Loglama altyapısı hazırlandıktan sonra default olarak sisteme loglama eklemek için kullanılır
            // Programcı loglama eklesin ya da eklemesin bütün metotları loglar.
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));

            //Attribute'ları önem sırasına göre sıralar. MethodInterceptionBaseAttribute içerisindeki Priority üzerinden
            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
