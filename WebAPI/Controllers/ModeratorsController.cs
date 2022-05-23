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
    public class ModeratorsController : Controller
    {
        IModeratorService _moderatorService;        //interface'ler referans tutar.

        //IoC Container
        public ModeratorsController(IModeratorService moderatorService)
        {
            _moderatorService = moderatorService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _moderatorService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _moderatorService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Moderator moderator)
        {
            var result = _moderatorService.Add(moderator);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Moderator moderator)
        {
            var result = _moderatorService.Delete(moderator);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deleteAll")]
        public IActionResult DeleteAll(Expression<Func<Moderator, bool>> filter)
        {
            var result = _moderatorService.DeleteAll(filter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Moderator moderator)
        {
            var result = _moderatorService.Update(moderator);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
