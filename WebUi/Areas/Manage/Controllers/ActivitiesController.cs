using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUi.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ActivitiesController : Controller
    {

        IActivityService _activityService;        //interface'ler referans tutar.
        IActivityTypeService _activityTypeService;
        ICityService _cityService;
        //IoC Container
        public ActivitiesController(IActivityService activityService,IActivityTypeService activityTypeService, ICityService cityService)
        {
            _activityService = activityService;
            _activityTypeService = activityTypeService;
            _cityService = cityService;
        }
        public IActionResult Index()
        {
            ViewBag.Breadcrumb = "Activities";
            return View(_activityService.GetAll().Data.OrderByDescending(x => x.Id).ToList());
        }

        // GET: Panel/Companies/Create
        public IActionResult Create()
        {
            ViewBag.Breadcrumb = "New activity";
            ViewData["ActivityTypeId"] = new SelectList(_activityTypeService.GetAll().Data, "Id", "ActivityTypeName");
            ViewData["CityId"] = new SelectList(_cityService.GetAll().Data, "Id", "CityName");

            return View(new Activity());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (Activity activity)
        {
            ViewBag.Breadcrumb = "New activity";
            ViewData["ActivityTypeId"] = new SelectList(_activityTypeService.GetAll().Data, "Id", "ActivityTypeName");
            ViewData["CityId"] = new SelectList(_cityService.GetAll().Data, "Id", "CityName");

            if (ModelState.IsValid)
            {
                if (_activityService.GetAll().Data.Any(x => x.ActivityName == activity.ActivityName))
                {
                    ModelState.AddModelError("ActivityName", $"{activity.ActivityName} name is exist");
                    TempData["ToastError"] = "Creating activity is failed  !";
                    return View(activity);
                }

                var result =_activityService.Add(activity);
                //aktivitenin basari kontrolu
                if (!result.Success)
                {
                    TempData["Toast"] = "creating activity is not successfully";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Toast"] = "creating activity is successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["ToastError"] = "Creating activity is failed !";
            return View(activity);
        }

        public IActionResult Edit(int? id, bool status = true)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["ActivityTypeId"] = new SelectList(_activityTypeService.GetAll().Data, "Id", "ActivityTypeName");
            ViewData["CityId"] = new SelectList(_cityService.GetAll().Data, "Id", "CityName");

            var activity = _activityService.GetById(id ?? 0).Data;
            if (activity == null)
            {
                return NotFound();
            }
            if (HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
            {
                return Json(new { res = true });
            }
            ViewBag.Breadcrumb = "Update activity";
            return View(activity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Activity activity)
        {


            if (id != activity.Id)
            {
                return NotFound();
            }

            ViewBag.Breadcrumb = "Update activity";
            ViewData["ActivityTypeId"] = new SelectList(_activityTypeService.GetAll().Data, "Id", "ActivityTypeName");
            ViewData["CityId"] = new SelectList(_cityService.GetAll().Data, "Id", "CityName");

            if (ModelState.IsValid)
            {
                if (_activityService.GetAll().Data.Any(x => x.ActivityName == activity.ActivityName && x.Id != activity.Id))
                {
                    ModelState.AddModelError("ActivityName", $"{activity.ActivityName} name is exist");
                    TempData["ToastError"] = "Creating activity is failed  !";
                    return View(activity);
                }
                try
                {
                    _activityService.Update(activity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(activity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Toast"] = "Updating Activity is successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["ToastError"] = "Updating activity is failed !";
            ViewBag.Breadcrumb = "update activity";
            return View(activity);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = _activityService.GetById(id ?? 0);
            if (activity == null)
            {
                return NotFound();
            }
            _activityService.Delete(activity.Data);
            return RedirectToAction(nameof(Index));
        }

        #region Private metods

        private bool ActivityExists(int id)
        {
            return _activityService.GetAll().Data.Any(a => a.Id == id);
        }


        #endregion
    }
}
