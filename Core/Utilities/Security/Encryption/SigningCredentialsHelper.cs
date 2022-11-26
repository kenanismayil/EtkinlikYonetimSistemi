using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        //Web apinin kendisinin de gelen bir jwt'i doğrulaması gerekiyor. O yüzden burayı yazdım.
        //Mesela kullanıcı adı ve parolanız bir credentialdır.
        //Bir sisteme giriş yapabilmek için elinizde olan şeylerdir. Credential dediğimiz bizim anahtarımızdır.
        //Bu metot bize SecurityKey'in imzalama nesnesini döndürüyor olacaktır.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            //asp.net'e diyoruz ki, bir tane hashing işlemi yapacaksın ve anahtar olarak bu securityKey'i kullanacaksın.
            //Şifreleme olarak ise güvenlik algoritmalarından HmacSha512Signature'ı kullan diyoruz.
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
