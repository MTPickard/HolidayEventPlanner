using HolidayPlanner.Data;
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
    public class GiftController : Controller
    {

        // GET: Index
        public ActionResult Index(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GiftService(userId);
            var model = service.GetGiftByPersonId(id);
            TempData["BudgetId"] = GetBudgetIdByPersonId(id);
            TempData["PersonId"] = id;

            return View(model);
        }


        // GET: Create Gift
        public ActionResult Create(int id)
        {
            TempData["PersonId"] = id;
            return View();
        }


        // POST: Create Gift
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, GiftCreate gift)
        {
            if(!ModelState.IsValid)
            {
                return View(gift);
            }

            var service = CreateGiftService();

            if(service.Create(id, gift))
            {
                TempData["SaveResult"] = $"{gift.Name} was successfully added";
                return RedirectToAction("Index", new { id = id });
            }

            ModelState.AddModelError("", $"{gift.Name} was NOT added.");
            return View(gift);
        }


        // GET: Edit
        public ActionResult Edit(int id)
        {
            var service = CreateGiftService();
            var detail = service.GetGiftsById(id);
            var entity = new GiftEdit
            {
                GiftId = detail.GiftId,
                Name = detail.Name,
                Quantity = detail.Quantity,
                Price = detail.Price,
            };

            TempData["PersonId"] = detail.PersonId;

            return View(entity);
        }


        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GiftEdit model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            if(model.GiftId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateGiftService();

            if(service.UpdateGift(model))
            {
                TempData["SaveResult"] = $"{model.Name} was updated.";
                return RedirectToAction("Index", new { id = id});
            }

            ModelState.AddModelError("", $"{model.Name} was NOT updated.");
            return View(model);
        }


        // GET: Delete
        public ActionResult Delete(int id)
        {
            var service = CreateGiftService();
            var entity = service.GetGiftsById(id);

            TempData["PersonId"] = entity.PersonId;

            return View(entity);
        }


        //POST: Delete
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGift(int id)
        {
            var service = CreateGiftService();
            var model = service.GetGiftsById(id);
            service.DeleteGift(id);

            TempData["SaveResult"] = $"{model.Name} was deleted";
            return RedirectToAction("Index", new { id = model.PersonId});
        }


        // GET: Details
        public ActionResult Details(int id)
        {
            var service = CreateGiftService();
            var model = service.GetGiftsById(id);
            TempData["PersonId"] = model.PersonId;

            return View(model);
        }


        // HELPER METHOD
        public GiftService CreateGiftService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new GiftService(userId);

            return service;
        }

        private int GetBudgetIdByPersonId(int id)
        {            
            PersonService service = new PersonService(Guid.Parse(User.Identity.GetUserId()));
            var person = service.GetPersonById(id);

            return person.BudgetId;
        }

    }
}