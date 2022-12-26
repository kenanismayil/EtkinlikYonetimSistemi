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

    public class CertificatesController : Controller
    {
        ICertificateService _certificateService;        //interface'ler referans tutar.      

        //IoC Container
        public CertificatesController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        [Authorize(Roles = "admin, super_admin")]
        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _certificateService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize(Roles = "admin, super_admin")]
        [HttpGet("getByCertificateId")]
        public IActionResult GetByCertificateId(int certificateId)
        {
            var result = _certificateService.GetByCertificateId(certificateId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("getCertificatesInfoByUser")]
        public IActionResult GetCertificatesForUser()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];

            var result = _certificateService.GetCertificatesForUser(token);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //[AllowAnonymous]
        //[HttpGet("getCertificates")]
        //public IActionResult GetCertificates(int activityId, int userId)
        //{
        //    var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
        //    var result = _certificateService.GetCertificates(activityId, token);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

        [Authorize]
        [HttpPost("add")]
        public IActionResult Add(CertificateForView certificate)
        {
            var result = _certificateService.Add(certificate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpDelete("delete")]
        public IActionResult Delete(int certificateId)
        {
            var result = _certificateService.Delete(certificateId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpPut("update")]
        public IActionResult Update(CertificateForView certificate)
        {
            var result = _certificateService.Update(certificate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [AllowAnonymous]
        [HttpPut("certificateImage")]
        public IActionResult UpdateCertificateImage(UpdateCertificateImageDto data)
        {
            var result = _certificateService.UpdateCertificateImage(data);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
