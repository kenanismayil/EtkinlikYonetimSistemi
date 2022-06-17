using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUi.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class RegistrationsController : Controller
    {

        IRegistrationService _registrationService;        //interface'ler referans tutar.

        //IoC Container
        public RegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }
        public IActionResult Index()
        {
            ViewBag.Breadcrumb = "Registration";
            return View(_registrationService.GetAll().Data.OrderByDescending(x => x.Id).ToList());
        }

        // GET: Panel/Companies/Create
        public IActionResult Create()
        {
            ViewBag.Breadcrumb = "New registration";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Registration,Id")] Registration registration)
        {
            ViewBag.Breadcrumb = "New registration";

            if (ModelState.IsValid)
            {
                if (_registrationService.GetAll().Data.Any(x => x.UserId == registration.UserId))
                {
                    ModelState.AddModelError("User", $"{registration.User} name is exist");
                    TempData["ToastError"] = "Creating user is failed  !";
                    return View(registration);
                }

                var result = _registrationService.Add(registration);
                //kayıtın basari kontrolu
                if (!result.Success)
                {
                    TempData["Toast"] = "creating registration is not successfully";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Toast"] = "creating registration is successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["ToastError"] = "Creating registration is failed !";
            return View(registration);
        }

        public IActionResult Edit(int? id, bool status = true)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = _registrationService.GetById(id ?? 0).Data;
            if (registration == null)
            {
                return NotFound();
            }
            if (HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
            {
                return Json(new { res = true });
            }
            ViewBag.Breadcrumb = "Update registration";
            return View(registration);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("UserId,Id")] Registration registration)
        {
            if (id != registration.Id)
            {
                return NotFound();
            }

            ViewBag.Breadcrumb = "Update registration";

            if (ModelState.IsValid)
            {
                if (_registrationService.GetAll().Data.Any(x => x.UserId == registration.UserId && x.Id != registration.Id))
                {
                    ModelState.AddModelError("UserId", $"{registration.UserId} is exist");
                    TempData["ToastError"] = "Creating registration is failed  !";
                    return View(registration);
                }
                try
                {
                    _registrationService.Update(registration);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Toast"] = "Updating Registration is successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["ToastError"] = "Updating registration is failed !";
            ViewBag.Breadcrumb = "update registration";
            return View(registration);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = _registrationService.GetById(id ?? 0);
            if (registration == null)
            {
                return NotFound();
            }
            _registrationService.Delete(registration.Data);
            return RedirectToAction(nameof(Index));
        }

        #region Private metods

        private bool RegistrationExists(int id)
        {
            return _registrationService.GetAll().Data.Any(a => a.Id == id);
        }


        #endregion
    }
}
