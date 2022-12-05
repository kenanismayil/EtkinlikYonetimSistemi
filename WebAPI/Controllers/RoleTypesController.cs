using Business.Abstract;
using Core.Entities.Concrete;
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
    [Authorize(Roles = "super_admin")]
    public class RoleTypesController : Controller
    {
        IRoleTypeService _roleTypeService;        //interface'ler referans tutar.

        //IoC Container
        public RoleTypesController(IRoleTypeService roleTypeService)
        {
            _roleTypeService = roleTypeService;
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _roleTypeService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int roleTypeId)
        {
            var result = _roleTypeService.GetById(roleTypeId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(RoleType roleType)
        {
            var result = _roleTypeService.Add(roleType);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(RoleType roleType)
        {
            var result = _roleTypeService.Delete(roleType);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(RoleType roleType)
        {
            var result = _roleTypeService.Update(roleType);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
