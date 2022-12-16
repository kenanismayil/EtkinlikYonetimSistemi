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
    public class CitiesController : Controller
    {
        ICityService _cityService;        //interface'ler referans tutar.

        //IoC Container
        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _cityService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _cityService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "admin, super_admin")]
        [HttpPost("add")]
        public IActionResult Add(CityRequirements city)
        {
            var result = _cityService.Add(city);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [Authorize(Roles = "super_admin")]
        [HttpDelete("delete")]
        public IActionResult Delete(string cityId)
        {
            var result = _cityService.Delete(cityId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "admin, super_admin")]
        [HttpPut("update")]
        public IActionResult Update(CityRequirements city)
        {
            var result = _cityService.Update(city);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
