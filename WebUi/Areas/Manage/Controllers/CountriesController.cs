using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebUi.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CountriesController : Controller
    {
        ICountryService _countryService;        //interface'ler referans tutar.

        //IoC Container
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public IActionResult Index()
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:27231/api/Countries/getAll");

            ViewBag.Breadcrumb = "Countryies";
            return View( _countryService.GetAll().Data.OrderByDescending(x => x.Id).ToList());
        }

        // GET: Panel/Companies/Create
        public IActionResult Create()
        {
            ViewBag.Breadcrumb = "New country";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CountryName,Id")] Country country)
        {
            ViewBag.Breadcrumb = "New country";

            if (ModelState.IsValid)
            {
                if ( _countryService.GetAll().Data.Any(x => x.CountryName ==country.CountryName))
                {
                    ModelState.AddModelError("CountryName", $"{country.CountryName} name is exist");
                    TempData["ToastError"] = "Creating country is failed  !";
                    return View(country);
                }
                _countryService.Add(country);

                TempData["Toast"] = "creating country is successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["ToastError"] = "Creating country is failed !";
            return View(country);
        }

        public IActionResult Edit(int? id, bool status = true)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country =  _countryService.GetById(id??0).Data;
            if (country == null)
            {
                return NotFound();
            }
            if (HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
            {
                return Json(new { res = true });
            }
            ViewBag.Breadcrumb = "Update country";
            return View(country);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CountryName,Id")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            ViewBag.Breadcrumb = "Update country";

            if (ModelState.IsValid)
            {
                if ( _countryService.GetAll().Data.Any(x => x.CountryName == country.CountryName && x.Id != country.Id))
                {
                    ModelState.AddModelError("CountryName", $"{country.CountryName} name is exist");
                    TempData["ToastError"] = "Creating country is failed  !";
                    return View(country);
                }
                try
                {
                    _countryService.Update(country);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Toast"] = "Updating Country is successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["ToastError"] = "Updating country is failed !";
            ViewBag.Breadcrumb = "update country";
            return View(country);
        }

        public  IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = _countryService.GetById(id??0);
            if (country == null)
            {
                return NotFound();
            }
            _countryService.Delete(country.Data);
            return RedirectToAction(nameof(Index));
        }

        #region Private metods

        private bool CountryExists(int id)
        {
            return _countryService.GetAll().Data.Any(e => e.Id == id);
        }

        
        #endregion
    }
}
