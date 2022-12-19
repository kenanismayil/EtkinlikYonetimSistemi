using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Helper
{
    public interface IAuthHelper
    {
        IDataResult<User> GetCurrentUser(string token);
        IDataResult<TokenClaims> GetTokenClaims(string token);
        IResult IsTokenValid(TokenClaims tokenClaims);
    }
}
