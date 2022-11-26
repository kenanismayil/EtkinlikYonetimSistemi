using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    //Şifreleme olan sistemlerde bizim herşeyi bir byte array formatında veri oluşturmamız gerekiyor.
    //Basit bir string ile bir key oluşturamayız.
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }  
    }
}
