using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //Kullanıcıların passwordları'nın hem hash'i, hem de salt'ı oluşturacağımız metottur. Dışarıya, yani veri kaynağımıza bu iki değeri birden çıkaracak bir yapı
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                //hmac.Key -> her kullanıcının şifresi için başka bir key değeri oluşturur. Bu yüzden biz veritabanında salt değerini de tutacağız. Yarın obürgün bu değeri değiştirebiliriz.
                passwordSalt = hmac.Key;  //ilgili kullandığımız algoritmanın o an oluşturduğu key değeridir. Şifreyi çözerken bizim bu salt'a ihtiyacımız olacaktır.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        //Kullanıcının sisteme sonradan girdiği parolayı, hesaplanan hash değerini bizim veritabanımızdaki hash değeri ile karşılaştırmak için kullanılan yapı
        //HMACSHA512(passwordSalt) -> Hesaplanan hash değeri salt'u kullarak elde edilmektedir. Yani o salt değerini aslında bu algoritmaya vermiş oluyoruz.
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                //computedHash -> bizim sisteme giriş yaparken sonradan gönderdiğimiz has'in değeri
                //passwordHash -> bizim veritabanından gönderilen has'in değeri
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
