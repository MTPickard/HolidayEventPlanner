using HolidayPlanner.Models;
using HolidayPlanner.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HolidayPlanner.Controllers
{
    public class BudgetController : Controller
    {
        // GET: Budget
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new BudgetService(userID);
            var model = service.GetAllBudgets();

            return View(model);
        }


        // GET: Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: Create Budget
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BudgetCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateBudgetService();

            if (service.CreateBudget(model))
            {
                TempData["SaveResult"] = $"{model.BudgetName} was added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", $"{model.BudgetName} was NOT added.");
            return View(model);
        }


        //GET: Edit
        public ActionResult Edit(int id)
        {
            var service = CreateBudgetService();
            var detail = service.GetBudgetByBudgetId(id);
            var model = new BudgetEdit
            {
                BudgetId = id,
                BudgetName = detail.BudgetName,
                BudgetAmount = detail.BudgetAmount,
                DateOfEvent = detail.DateOfEvent,
                NumberOfPaychecks = detail.NumberOfPaychecks
            };
            return View(model);
        }


        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BudgetEdit model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            if(model.BudgetId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateBudgetService();

            if(service.UpdateBudget(model))
            {
                TempData["SaveResult"] = $"{model.BudgetName} was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", $"{model.BudgetName} was NOT updated.");
            return View(model);
        }


        // GET: Delete
        public ActionResult Delete(int id)
        {
            var service = CreateBudgetService();
            var model = service.GetBudgetByBudgetId(id);

            return View(model);
        }


        // DELETE: Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBudget(int id)
        {
            var service = CreateBudgetService();
            var model = service.GetBudgetByBudgetId(id);
            service.DeleteBudget(id);

            TempData["SaveResult"] = $"{model.BudgetName} was deleted.";

            return RedirectToAction("Index");
        }


        // GET: Details
        public ActionResult Details(int id)
        {
            var service = CreateBudgetService();
            var model = service.GetBudgetByBudgetId(id);

            return View(model);
        }


        //HELPER METHOD
        public BudgetService CreateBudgetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BudgetService(userId);

            return service;
        }
    }
}