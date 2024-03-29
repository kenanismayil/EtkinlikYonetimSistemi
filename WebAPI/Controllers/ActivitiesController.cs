﻿using Business.Abstract;
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
                return Ok(result.Data);
            }

            return BadRequest(result);
        }

        //[Authorize(Roles = "admin, super_admin")]
        //[HttpGet("getParticipiants")]
        //public IActionResult GetParticipiants(int activityId)
        //{
        //    var result = _activityService.GetParticipiants(activityId);
        //    if (result.Success)
        //    {
        //        return Ok(result.Data);
        //    }

        //    return BadRequest(result);
        //}

        [AllowAnonymous]
        [HttpGet("{activityId}")]
        public IActionResult GetById(int activityId)
        {
            var result = _activityService.GetById(activityId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [Authorize(Roles = "admin, super_admin")]
        [HttpPost("add")]
        public IActionResult Add(ActivityCreatingByAdmin activity)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            var result = _activityService.Add(activity, token);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "admin, super_admin")]
        [HttpDelete("delete")]
        public IActionResult Delete(string activityId)
        {
            var result = _activityService.Delete(activityId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "super_admin")]
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

        [Authorize(Roles = "admin, super_admin")]
        [HttpPut("update")]
        public IActionResult Update(ActivityCreatingByAdmin activity)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            var result = _activityService.Update(activity, token);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
