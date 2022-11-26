using Business.Abstract;
using Business.Constants.PathConstants;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class ActivityImages : ControllerBase
    {
        private IActivityImageService _activityImageService;
        private IWebHostEnvironment _webHostEnvironment;
        private string _rootPath;

        public ActivityImages(IActivityImageService activityImageService, IWebHostEnvironment webHostEnvironment)
        {
            _activityImageService = activityImageService;
            _webHostEnvironment = webHostEnvironment;
            _rootPath = _webHostEnvironment.WebRootPath;
        }

        [HttpPost("add")]
        public IActionResult Add(IFormFile formFile, [FromForm] ActivityImage activityImage)
        {
            var a = PathConstants.ImagesPath;
            var result = _activityImageService.Add(formFile, activityImage, _rootPath);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(ActivityImage activityImage)
        {
            var result = _activityImageService.Delete(activityImage, _rootPath);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("update")]
        public IActionResult Update(IFormFile formFile, [FromForm] ActivityImage activityImage)
        {
            var result = _activityImageService.Update(formFile, activityImage, _rootPath);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("getByActivityTypeId")]
        public IActionResult GetImageByActivityId(int activityId)
        {
            var result = _activityImageService.GetImageByActivityId(activityId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _activityImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
