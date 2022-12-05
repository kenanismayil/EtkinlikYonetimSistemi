using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.AuthHelper
{
    //public class AuthHelper : IAuthHelper
    //{
    //    private unitOfWork _unitOfWork;
    //    private IHttpContextAccessor _httpContextAccessor;
    //    private string? accessToken;

    //    public AuthHelper(unitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    //    {
    //        _unitOfWork = unitOfWork;
    //        _httpContextAccessor = httpContextAccessor;
    //    }

    //    public User GetCurrentUser()
    //    {
    //        User? user = null;

    //        _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("X'Access' Token", out accessToken);
    //        TokenClaims claims = GetTokenClaims(string token);

    //    }

    //    public TokenClaims GetTokenClaims(string token)
    //    {
    //        TokenClaims tokenClaims = new TokenClaims();



    //        return tokenClaims;
    //    }
    //    public bool IsTokenExpire(Claim claim)
    //    {
    //        return claim.
    //    }
    //}
}
