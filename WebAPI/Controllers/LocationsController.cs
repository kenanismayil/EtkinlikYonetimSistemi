using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin, super_admin")]
    public class LocationsController : Controller
    {
        ILocationService _locationService;
        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _locationService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("getById")]
        public IActionResult GetById(int locationId)
        {
            var result = _locationService.GetById(locationId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Location location)
        {
            var result = _locationService.Add(location);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Location location)
        {
            var result = _locationService.Delete(location);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Location location)
        {
            var result = _locationService.Update(location);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
