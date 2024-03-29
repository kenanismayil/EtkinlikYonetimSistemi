﻿using Business.Abstract;
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
    public class ActivityTypesController : Controller
    {
        IActivityTypeService _activityTypeService;        //interface'ler referans tutar.

        //IoC Container
        public ActivityTypesController(IActivityTypeService activityTypeService)
        {
            _activityTypeService = activityTypeService;
        }


        [AllowAnonymous]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _activityTypeService.GetAll();
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
            var result = _activityTypeService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "admin, super_admin")]
        [HttpPost("add")]
        public IActionResult Add(ActivityType activityType)
        {
            var result = _activityTypeService.Add(activityType);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "super_admin")]
        [HttpDelete("delete")]
        public IActionResult Delete(ActivityType activityType)
        {
            var result = _activityTypeService.Delete(activityType);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "super_admin")]
        [HttpDelete("deleteAll")]
        public IActionResult DeleteAll(Expression<Func<ActivityType, bool>> filter)
        {
            var result = _activityTypeService.DeleteAll(filter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize(Roles = "super_admin")]
        [HttpPut("update")]
        public IActionResult Update(ActivityType activityType)
        {
            var result = _activityTypeService.Update(activityType);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
