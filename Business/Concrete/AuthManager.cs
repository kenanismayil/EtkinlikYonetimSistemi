using Business.Abstract;
using Business.Constants.Messages;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IRoleTypeService _roleTypeService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IRoleTypeService roleTypeService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _roleTypeService = roleTypeService;
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleTypeId = 3,
                Status = true
            };
            if (!_userService.Add(user).Success)
            {
                return new ErrorDataResult<User>(TurkishMessage.ErrorMessage);
            }

            user.RoleType = _roleTypeService.GetById(user.RoleTypeId).Data;
            return new SuccessDataResult<User>(user, TurkishMessage.UserRegistered);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _userService.GetByMail(userForLoginDto.Email);
            if (userToLogin == null)
            {
                return new ErrorDataResult<User>(TurkishMessage.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToLogin.Data.PasswordHash, userToLogin.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(TurkishMessage.PasswordError);
            }

            userToLogin.Data.RoleType = _roleTypeService.GetById(userToLogin.Data.RoleTypeId).Data;

            return new SuccessDataResult<User>(userToLogin.Data, TurkishMessage.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            var user = _userService.GetByMail(email);
            if (user.Data != null)
            {
                return new ErrorResult(TurkishMessage.UserAlreadyExists);
            }
            return new SuccessResult();

        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claim = _userService.GetClaim(user);
            var accessToken = _tokenHelper.CreateToken(user, claim.Data);
            return new SuccessDataResult<AccessToken>(accessToken, TurkishMessage.AccessTokenCreated);
        }
    }
}
