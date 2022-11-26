using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching.Abstract;
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
    //Bir Managerde Cache yönetimi yapıyorsak, veriyi manipüle eden metotlarımıza CacheRemoveAspect uygularız.
    //Datamız bozulduğu zaman(yeni data eklenirse,güncellenirse, silinirse) bunu uygulamamız gerekir.
    //OnSuccess olduğu zaman RemoveByPattern uygularız, çünkü belki de add işlemi yaptığımızda hata verebilir ve veritabanına yeni data eklenemeyecktir.
    //Yeni bir data eklediğimizde hata verebileceğinden dolayı sadece OnSuccess olduğunda RemoveByPattern'i uygulamamız mantıklı olacaktır.
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);    //patern' göre silme işlemi yapıyor.
        }
    }
}
