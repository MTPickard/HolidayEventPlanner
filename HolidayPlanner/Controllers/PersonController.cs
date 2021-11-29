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
    [Authorize]
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PersonService(userId);
            var model = service.GetPersonByBudgetId(id);
            TempData["BudgetId"] = id;


            return View(model);
        }


        // GET: Create Person
        public ActionResult Create(int id)
        {
            TempData["BudgetId"] = id;
            return View();
        }


        // POST: Create Person
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, PersonCreate person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            var service = CreatePersonService();

            if (service.CreatePerson(id, person))
            {
                TempData["SaveResult"] = $"{person.FirstName} was added.";
                return RedirectToAction("Index", new { id = id });
            }

            ModelState.AddModelError("", $"{person.FirstName} was not added.");
            return View(person);
        }


        // GET: Edit
        public ActionResult Edit(int id)
        {
            var service = CreatePersonService();
            var detail = service.GetPersonById(id);
            var model = new PersonEdit
            {
                PersonId = detail.PersonId,
                FirstName = detail.FirstName,
                LastName = detail.LastName,
                PersonBudget = detail.PersonBudget
            };
            TempData["BudgetId"] = detail.BudgetId;

            return View(model);
        }


        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PersonEdit model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.PersonId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreatePersonService();

            if(service.UpdatePerson(model))
            {
                TempData["SaveResult"] = $"{model.FirstName} was updated.";
                return RedirectToAction("Index", new { id = id });
            }

            ModelState.AddModelError("", $"{model.FirstName} was NOT updated successfully.");
            return View(model);
        }


        // GET: Delete
        public ActionResult Delete(int id)
        {
            var service = CreatePersonService();
            var model = service.GetPersonById(id);

            return View(model);
        }


        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreatePersonService();
            var model = service.GetPersonById(id);
            service.DeletePerson(id);

            TempData["SaveResult"] = $"{model.FirstName} was successfully deleted.";

            return RedirectToAction("Index", new { id = id });
        }


        // GET: Details
        public ActionResult Details(int id)
        {
            var service = CreatePersonService();
            var model = service.GetPersonById(id);

            TempData["BudgetId"] = model.BudgetId;

            return View(model);
        }


        // HELPER METHOD
        
        private PersonService CreatePersonService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PersonService(userId);

            return service;
        }
    }
}