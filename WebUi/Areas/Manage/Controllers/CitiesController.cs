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
    public class CitiesController : Controller
    {
        ICityService _cityService;        //interface'ler referans tutar.

        //IoC Container
        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }
        public IActionResult Index()
        {
            ViewBag.Breadcrumb = "Activities";
            return View(_cityService.GetAll().Data.OrderByDescending(x => x.Id).ToList());
        }

        // GET: Panel/Companies/Create
        public IActionResult Create()
        {
            ViewBag.Breadcrumb = "New activity";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ActivityName,Id")] City city)
        {
            ViewBag.Breadcrumb = "New activity";

            if (ModelState.IsValid)
            {
                if (_cityService.GetAll().Data.Any(x => x.CityName == city.CityName))
                {
                    ModelState.AddModelError("CityName", $"{city.CityName} name is exist");
                    TempData["ToastError"] = "Creating activity is failed  !";
                    return View(city);
                }

                var result = _cityService.Add(city);
                //sehirin basari kontrolu
                if (!result.Success)
                {
                    TempData["Toast"] = "creating city is not successfully";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Toast"] = "creating activity is successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["ToastError"] = "Creating activity is failed !";
            return View(city);
        }

        public IActionResult Edit(int? id, bool status = true)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = _cityService.GetById(id ?? 0).Data;
            if (city == null)
            {
                return NotFound();
            }
            if (HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
            {
                return Json(new { res = true });
            }
            ViewBag.Breadcrumb = "Update activity";
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CityName,Id")] City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            ViewBag.Breadcrumb = "Update activity";

            if (ModelState.IsValid)
            {
                if (_cityService.GetAll().Data.Any(x => x.CityName == city.CityName && x.Id != city.Id))
                {
                    ModelState.AddModelError("ActivityName", $"{city.CityName} name is exist");
                    TempData["ToastError"] = "Creating activity is failed  !";
                    return View(city);
                }
                try
                {
                    _cityService.Update(city);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Toast"] = "Updating City is successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["ToastError"] = "Updating city is failed !";
            ViewBag.Breadcrumb = "update city";
            return View(city);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = _cityService.GetById(id ?? 0);
            if (city == null)
            {
                return NotFound();
            }
            _cityService.Delete(city.Data);
            return RedirectToAction(nameof(Index));
        }

        #region Private metods

        private bool CityExists(int id)
        {
            return _cityService.GetAll().Data.Any(c => c.Id == id);
        }
        #endregion
    }
}
