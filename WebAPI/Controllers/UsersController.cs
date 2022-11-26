using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin, super_admin")]
    public class UsersController : Controller
    {
        IUserService _userService;        //interface'ler referans tutar.

        //IoC Container
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            var result = _userService.Add(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getByEmail")]
        public IActionResult GetByMail(string email)
        {
            var result = _userService.GetByMail(email);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "user")]
        [HttpGet("userDetailDto")]
        public IActionResult GetUserDetails()
        {
            var result = _userService.GetUserDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getClaims")]
        public IActionResult GetClaims(User user)
        {
            var result = _userService.GetClaims(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
