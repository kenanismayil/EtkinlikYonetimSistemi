using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        IUserService _userService;        //interface'ler referans tutar.

        //IoC Container
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "super_admin")]
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


        [Authorize(Roles = "super_admin")]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            var result = _userService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);          
        }


        [HttpPut("update")]
        [Authorize]
        public IActionResult Update(UserForInfoChange user)
        {
            var result = _userService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "super_admin")]
        [HttpPut("updateUserInfoBySuperAdmin")]
        public IActionResult UpdateUserInfoBySuperAdmin(User user)
        {
            var result = _userService.UpdateUserInfoBySuperAdmin(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("changePassword")]
        [Authorize]
        public IActionResult ChangePassword(UserForPasswordChange user)
        {
            var result = _userService.ChangePassword(user.Id, user.oldPassword, user.newPassword);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "super_admin")]
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


        [HttpGet("getClaims")]
        [Authorize]
        public IActionResult GetClaim(User user)
        {
            var result = _userService.GetClaim(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getUserForView")]
        [Authorize]
        public IActionResult GetUserForView(User user)
        {
            var result = _userService.GetUserForView(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "super_admin")]
        [HttpGet("getById")]
        public IActionResult GetById(int userId)
        {
            var result = _userService.GetById(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "super_admin")]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
