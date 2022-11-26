using Core.CrossCuttingConcerns.Caching.Abstract;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Concrete.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //Adapter Pattern uyguladım. Yani Microsoft'un bu kütüphanesinden yararlanarak varolan sistemi, kendi sistemime uyarladım.
        //Microsoft'un kendi kütüphanesini kullandım. IMemoryCache isimli bir interface'i vardır.
        //Constructor'dan enjekte edemeyiz, çalışmaz. Çünkü zincir Web api->Business->Data Access şeklinde ilerler.
        //Aspect bambaşka bir zincirin içinde olduğundan dolayı bağımlılık zincirinin içinde değil!
        //Bunun için Core'da bir tane ServiceTool yazmıştım.
        IMemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            //out _ -> data döndürmesini istemiyorum, sadece bellekte böyle bir key var mı yok mu onu bulmak istiyorum.
            return _memoryCache.TryGetValue(key, out _);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        //Çalışma anında bellekten silmeye yarıyor. Bunu reflectionla yapıyoruz. Çalışma anında oluşan nesnelere mudahele ediyorum.
        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
