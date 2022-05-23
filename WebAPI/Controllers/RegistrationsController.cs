using Business.Abstract;
using Entities.Concrete;
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
        public IActionResult Add(Registration registration)
        {
            var result = _registrationService.Add(registration);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Registration registration)
        {
            var result = _registrationService.Delete(registration);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deleteAll")]
        public IActionResult DeleteAll(Expression<Func<Registration, bool>> filter)
        {
            var result = _registrationService.DeleteAll(filter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Registration registration)
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
