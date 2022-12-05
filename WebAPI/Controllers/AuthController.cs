using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;
        private IUserService _userService;


        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data);
            var model = new AuthorizationModel()
            {
                Token = result.Data.Token,
                User = _userService.GetUserForView(userToLogin.Data).Data
            };

            if (result.Success)
            {
                return Ok(model);
            }


            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto);
            var result = _authService.CreateAccessToken(registerResult.Data);

            var model = new AuthorizationModel()
            {
                Token = result.Data.Token,
                ExpirationDate = result.Data.Expiration,
                User = _userService.GetUserForView(registerResult.Data).Data
            };

            if (result.Success)
            {
                return Ok(model);
            }

            return BadRequest(result.Message);
        }


    }
}
