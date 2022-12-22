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
    public class CertificatesController : Controller
    {
        ICertificateService _certificateService;        //interface'ler referans tutar.      

        //IoC Container
        public CertificatesController(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet("getCertificatesInfoByUserId")]
        public IActionResult GetCertificatesInfoByUserId(int userId)
        {
            var result = _certificateService.GetCertificatesInfoByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(CertificateForView certificate, string pnrNo)
        {
            var result = _certificateService.Add(certificate, pnrNo);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

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
    }
}
