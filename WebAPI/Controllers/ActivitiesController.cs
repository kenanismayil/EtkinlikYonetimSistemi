using Business.Abstract;
using Entities.Concrete;
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
    public class ActivitiesController : Controller
    {
        IActivityService _activityService;        //interface'ler referans tutar.

        //IoC Container
        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _activityService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("getById")]
        public IActionResult GetById(int activityId)
        {
            var result = _activityService.GetById(activityId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [AllowAnonymous]
        [HttpGet("activityDetailDto")]
        public IActionResult GetActivityDetails()
        {
            var result = _activityService.GetActivityDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Activity activity)
        {
            var result = _activityService.Add(activity);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Activity activity)
        {
            var result = _activityService.Delete(activity);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("deleteAll")]
        public IActionResult DeleteAll(Expression<Func<Activity, bool>> filter)
        {
            var result = _activityService.DeleteAll(filter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        public IActionResult Update(Activity activity)
        {
            var result = _activityService.Update(activity);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
