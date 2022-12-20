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
    [Authorize(Roles = "admin, super_admin")]
    public class CommentsController : Controller
    {
        ICommentService _commentService;        //interface'ler referans tutar.

        //IoC Container
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _commentService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [AllowAnonymous]
        [HttpGet("")]
        public IActionResult GetByActivityId(int activityId)
        {
            var result = _commentService.GetByActivityId(activityId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPost("add")]
        [Authorize]
        public IActionResult Add(CommentForUser comment)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];

            var result = _commentService.Add(comment, token);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        [Authorize]
        public IActionResult Delete(string commentId)
        {
            var result = _commentService.Delete(commentId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("update")]
        [Authorize]
        public IActionResult Update(CommentForUser comment)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];

            var result = _commentService.Update(comment, token);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
