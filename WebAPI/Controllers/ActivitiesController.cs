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
    public class ActivitiesController : Controller
    {
        IActivityService _activityService;        //interface'ler referans tutar.

        //IoC Container
        public ActivitiesController(IActivityService activityService)
        {
            _activityService = activityService;
        }

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

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = _activityService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

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

        [HttpPost("delete")]
        public IActionResult Delete(Activity activity)
        {
            var result = _activityService.Delete(activity);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deleteAll")]
        public IActionResult DeleteAll(Expression<Func<Activity, bool>> filter)
        {
            var result = _activityService.DeleteAll(filter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
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
