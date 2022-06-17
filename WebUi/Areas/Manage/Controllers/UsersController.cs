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
    public class UsersController : Controller
    {
        IUserService _userService;        //interface'ler referans tutar.

        //IoC Container
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            ViewBag.Breadcrumb = "Users";
            return View(_userService.GetAll().Data.OrderByDescending(x => x.Id).ToList());
        }

        // GET: Panel/Companies/Create
        public IActionResult Create()
        {
            ViewBag.Breadcrumb = "New user";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] User user)
        {
            ViewBag.Breadcrumb = "New user";

            if (ModelState.IsValid)
            {
                if (_userService.GetAll().Data.Any(x => x.FirstName == user.FirstName && x.LastName == user.LastName))
                {
                    ModelState.AddModelError("UserName", $"{user.FirstName} name is exist");
                    TempData["ToastError"] = "Creating user is failed  !";
                    return View();
                }

                var result = _userService.Add(user);
                if (!result.Success)
                {
                    TempData["Toast"] = "creating user is not successfully";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Toast"] = "creating user is successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["ToastError"] = "Creating user is failed !";
            return View(user);
        }

        public IActionResult Edit(int? id, bool status = true)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetById(id ?? 0).Data;
            if (user == null)
            {
                return NotFound();
            }
            if (HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
            {
                return Json(new { res = true });
            }
            ViewBag.Breadcrumb = "Update user";
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            ViewBag.Breadcrumb = "Update user";

            if (ModelState.IsValid)
            {
                if (_userService.GetAll().Data.Any(x => x.FirstName == user.FirstName && x.Id != user.Id))
                {
                    ModelState.AddModelError("UserName", $"{user.FirstName} name is exist");
                    TempData["ToastError"] = "Creating user is failed  !";
                    return View(user);
                }
                try
                {
                    var result = _userService.Update(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Toast"] = "Updating User is successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["ToastError"] = "Updating user is failed !";
            ViewBag.Breadcrumb = "update user";
            return View(user);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _userService.GetById(id ?? 0);
            if (user == null)
            {
                return NotFound();
            }
            _userService.Delete(user.Data);
            return RedirectToAction(nameof(Index));
        }

        #region Private metods

        private bool UserExists(int id)
        {
            return _userService.GetAll().Data.Any(c => c.Id == id);
        }
        #endregion
    }
}
