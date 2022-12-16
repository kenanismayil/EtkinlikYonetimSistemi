using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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

        //IoC Container
        public RegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
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

        [Authorize(Roles = "super_admin")]
        [HttpGet("getAllForFilter")]
        public IActionResult GetAllForFilter(Expression<Func<Registration, bool>> filter)
        {
            var result = _registrationService.GetAllForFilter(filter);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "super_admin")]
        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _registrationService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("add")]
        public IActionResult Add(RegisterForActivity registration)
        {
            var result = _registrationService.Add(registration);
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


        [HttpPut("update")]
        public IActionResult Update(RegisterForActivity registration)
        {
            var result = _registrationService.Update(registration);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
