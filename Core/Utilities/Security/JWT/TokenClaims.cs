using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    //Tokenin içindeki bilgileri tutar
    public class TokenClaims
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string NameIdentifier { get; set; }
        public DateTime TokenExpirationDate { get; set; }
    }
}
