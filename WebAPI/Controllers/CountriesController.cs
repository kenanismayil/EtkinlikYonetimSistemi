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
    public class CountriesController : Controller
    {
        ICountryService _countryService;        //interface'ler referans tutar.

        //IoC Container
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _countryService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _countryService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Country country)
        {
            var result = _countryService.Add(country);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Country country)
        {
            var result = _countryService.Delete(country);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deleteAll")]
        public IActionResult DeleteAll(Expression<Func<Country, bool>> filter)
        {
            var result = _countryService.DeleteAll(filter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Country country)
        {
            var result = _countryService.Update(country);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
