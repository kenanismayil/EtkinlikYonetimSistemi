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

    public class ActivitiesTypeController : Controller
    {
        IActivityTypeService _activityTypeService;        //interface'ler referans tutar.

        //IoC Container
        public ActivitiesTypeController(IActivityTypeService activityTypeService)
        {
            _activityTypeService = activityTypeService;
        }

        public IActionResult Index()
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:27231/api/Countries/getAll");

            ViewBag.Breadcrumb = "Activitivity Type";
            return View(_activityTypeService.GetAll().Data.OrderByDescending(x => x.Id).ToList());
        }

        // GET: Panel/Companies/Create
        public IActionResult Create()
        {
            ViewBag.Breadcrumb = "New activity type";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ActivityType activityType)
        {
            ViewBag.Breadcrumb = "New activity type";

            if (ModelState.IsValid)
            {
                if (_activityTypeService.GetAll().Data.Any(x => x.ActivityTypeName == activityType.ActivityTypeName))
                {
                    ModelState.AddModelError("ActivityTypeName", $"{activityType.ActivityTypeName} name is exist");
                    TempData["ToastError"] = "Creating activity type is failed  !";
                    return View(activityType);
                }

                var result = _activityTypeService.Add(activityType);
                //ulkenin basari kontrolu
                if (!result.Success)
                {
                    TempData["Toast"] = "creating activity type is not successfully";
                    return RedirectToAction(nameof(Index));
                }

                TempData["Toast"] = "creating activity type is successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["ToastError"] = "Creating activity type is failed !";
            return View(activityType);
        }

        public IActionResult Edit(int? id, bool status = true)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityType = _activityTypeService.GetById(id ?? 0).Data;
            if (activityType == null)
            {
                return NotFound();
            }
            if (HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest")
            {
                return Json(new { res = true });
            }
            ViewBag.Breadcrumb = "Update activity type";
            return View(activityType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ActivityTypeName,Id")] ActivityType activityType)
        {
            if (id != activityType.Id)
            {
                return NotFound();
            }

            ViewBag.Breadcrumb = "Update activity type";

            if (ModelState.IsValid)
            {
                if (_activityTypeService.GetAll().Data.Any(x => x.ActivityTypeName == activityType.ActivityTypeName && x.Id != activityType.Id))
                {
                    ModelState.AddModelError("Activity Type", $"{activityType.ActivityTypeName} name is exist");
                    TempData["ToastError"] = "Creating activity type is failed  !";
                    return View(activityType);
                }
                try
                {
                    _activityTypeService.Update(activityType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityTypeExists(activityType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Toast"] = "Updating activity type is successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["ToastError"] = "Updating activity type is failed !";
            ViewBag.Breadcrumb = "update activity type";
            return View(activityType);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activityType = _activityTypeService.GetById(id ?? 0);
            if (activityType == null)
            {
                return NotFound();
            }
            _activityTypeService.Delete(activityType.Data);
            return RedirectToAction(nameof(Index));
        }

        #region Private metods

        private bool ActivityTypeExists(int id)
        {
            return _activityTypeService.GetAll().Data.Any(e => e.Id == id);
        }


        #endregion
    }
}
