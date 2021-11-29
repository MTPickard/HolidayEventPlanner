using HolidayPlanner.Data;
using HolidayPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlanner.Services
{
    public class BudgetService
    {
        private readonly Guid _userId;


        public BudgetService(Guid userId)
        {
            _userId = userId;
        }


        public bool CreateBudget(BudgetCreate model)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var entity = new Budget
            {
                OwnerId = _userId,
                BudgetId = model.BudgetId,
                BudgetName = model.BudgetName,
                BudgetAmount = model.BudgetAmount,
                DateOfEvent = model.DateOfEvent,
                NumberOfPaychecks = model.NumberOfPaychecks,
            };

            _db.Budgets.Add(entity);
            return _db.SaveChanges() == 1;
        }


        public IEnumerable<BudgetListItem> GetAllBudgets()
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var query = _db
                .Budgets
                .Where(e => e.OwnerId == _userId)
                .ToList()
                .Select(
                e => new BudgetListItem
                {
                    BudgetId = e.BudgetId,
                    BudgetName = e.BudgetName,
                    BudgetAmount = e.BudgetAmount,
                    DateOfEvent = e.DateOfEvent,
                    NumberOfPaychecks = e.NumberOfPaychecks,
                    CurrentBalance = e.CurrentBalance,
                    AmountLeft = e.AmountLeft,
                    AmountToSavePerPaycheck = e.AmountToSavePerPaycheck
                });

            return query.ToArray();
        }


        public BudgetDetail GetBudgetByBudgetId(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var entity = _db
                .Budgets
                .Where(e => e.OwnerId == _userId)
                .Single(e => e.BudgetId == id);

            return new BudgetDetail
            {
                BudgetName = entity.BudgetName,
                BudgetAmount = entity.BudgetAmount,
                DateOfEvent = entity.DateOfEvent,
                NumberOfPaychecks = entity.NumberOfPaychecks,
                CurrentBalance = entity.CurrentBalance,
                AmountLeft = entity.AmountLeft,
                AmountToSavePerPaycheck = entity.AmountToSavePerPaycheck
            };
        }


        public bool UpdateBudget(BudgetEdit model)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var entity = _db
                .Budgets
                .Where(e => e.BudgetId == model.BudgetId)
                .Single(e => e.OwnerId == _userId);

            entity.BudgetName = model.BudgetName;
            entity.BudgetAmount = model.BudgetAmount;
            entity.DateOfEvent = model.DateOfEvent;
            entity.NumberOfPaychecks = model.NumberOfPaychecks;

            return _db.SaveChanges() == 1;
        }


        public bool DeleteBudget(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var entity = _db
                .Budgets
                .Where(e => e.OwnerId == _userId)
                .Single(e => e.BudgetId == id);

            _db.Budgets.Remove(entity);
            return _db.SaveChanges() == 1;
        }
    }
}
