using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Abstract
{
    //key -> cache'te tutacağımız şeyi belirtmek için kullanılır ve string tipindedir.
    //cache'e bir şey ekleyebiliriz, key value şeklinde olacak, gelecek data bütün veri tipleri(object) olabilir
    //duration -> cache'de ne kadar duracağını gösterir.
    //T Get<T>(string key) -> ilk T her şey dönebilir, ikinci T ile hangi tipte tuttuğumuzu belirtiriz.
    //string key -> key veriyoruz ve bu keye karşılık gelen datayı bellekten bize ver diyoruz
    public interface ICacheManager
    {
        T Get<T>(string key);       //generic olan versiyonu
        object Get(string key);     //generic olmayan versiyonu, ama tip dönüiümü yapmak gerekir
        void Add(string key, object value, int duration);
        bool IsAdd(string key);     //cache'te var mı diye kontrol eder
        void Remove(string key);                //key'e karşılık gelen datayı siler
        void RemoveByPattern(string pattern);   //parametrik olan fonksiyonlarda pattern kullanılır.Ör:ismi get ile başlayanları sil gibi

    }
}
