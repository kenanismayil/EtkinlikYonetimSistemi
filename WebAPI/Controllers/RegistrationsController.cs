using Business.Abstract;
using Business.Helper;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authentication;
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
    public class RegistrationsController : Controller
    {
        IRegistrationService _registrationService;        //interface'ler referans tutar.
        IAuthHelper _authHelper;

        //IoC Container
        public RegistrationsController(IRegistrationService registrationService, IAuthHelper authHelper)
        {
            _registrationService = registrationService;
            _authHelper = authHelper;
        }

        [Authorize(Roles = "super_admin")]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _registrationService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("{activityId}")]
        public IActionResult GetRegisterInfo(int activityId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            var result = _registrationService.GetRegisterInfo(activityId, token);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [Authorize]
        [HttpGet("user")]
        public IActionResult GetRegisteredEvents()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            var result = _registrationService.GetRegisteredEvents(token);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("pnr")]
        public IActionResult GetUserByPnrNo(string pnrNo)
        {
            var result = _registrationService.GetUserByPnrNo(pnrNo);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize]
        [HttpPut("pnr")]
        public IActionResult UpdateUserStatusOnEventArea(string pnrNo)
        {
            var result = _registrationService.UpdateUserStatusOnEventArea(pnrNo);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        //requestin headerindeki tokeni alma 


        [HttpPost("add")]
        [Authorize]
        public IActionResult Add(int activityId)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];

            var result = _registrationService.Add(token, activityId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpDelete("delete")]
        public IActionResult Delete(RegisterForActivity registration)
        {
            var result = _registrationService.Delete(registration);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        
    }
}
