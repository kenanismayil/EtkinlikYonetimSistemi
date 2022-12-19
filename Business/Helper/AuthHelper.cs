using Business.Abstract;
using Business.Constants.Messages;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.JWT;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Business.Helper
{
    public class AuthHelper : IAuthHelper
    {
        private IUserService _userService;

        public AuthHelper(IUserService userService)
        {
            _userService = userService;
        }

        public IDataResult<User> GetCurrentUser(string token)
        {
            User user = null;

            var tokenClaims = GetTokenClaims(token).Data;

            if (!IsTokenValid(tokenClaims).Success)
            {
                return new ErrorDataResult<User>(TurkishMessage.TokenIsExpired);
            }

            if (!string.IsNullOrEmpty(tokenClaims.Email))
            {
                user = _userService.GetById(Convert.ToInt32(tokenClaims.NameIdentifier)).Data;
            }
            if (user is null)
            {
                return new ErrorDataResult<User>(TurkishMessage.UserNotFound);
            }
            return new SuccessDataResult<User>(user, TurkishMessage.SuccessMessage);
        }

        public IDataResult<TokenClaims> GetTokenClaims(string token)
        {
            TokenClaims tokenClaims = new TokenClaims();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);

            var securityToken = jsonToken as JwtSecurityToken;
            tokenClaims.Name = securityToken?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            tokenClaims.NameIdentifier = securityToken?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            tokenClaims.Email = securityToken?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var expirationDate = securityToken?.Claims?.FirstOrDefault(x => x.Type == "exp")?.Value;
            var timeSpan = long.Parse(expirationDate);

            tokenClaims.TokenExpirationDate = DateTimeOffset.FromUnixTimeSeconds(timeSpan).UtcDateTime;

            return new SuccessDataResult<TokenClaims>(tokenClaims);
        }

        public IResult IsTokenValid(TokenClaims tokenClaims)
        {
            return tokenClaims.TokenExpirationDate < DateTime.Now ? new SuccessResult() : new ErrorResult();
        }
    }
}
