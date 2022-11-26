using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    //Token oluşturma mekanizmasına ihtiyaç vardır.
    public interface ITokenHelper
    {
        //İlgili kullanıcı için, ilgili kullanıcının claim'lerini içerecek token üretecektir.
        AccessToken CreateToken(User user, List<OperationClaim> operationClaim);
    }
}
