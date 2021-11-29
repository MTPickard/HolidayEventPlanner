using HolidayPlanner.Data;
using HolidayPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlanner.Services
{
    public class PersonService
    {
        private readonly Guid _userId;


        public PersonService(Guid userId)
        {
            _userId = userId;
        }


        // CREATE
        public bool CreatePerson(int id, PersonCreate model)
        {
            var person = new Person()
            {
                OwnerId = _userId,
                BudgetId = id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PersonBudget = model.PersonBudget,
            };

            ApplicationDbContext _db = new ApplicationDbContext();

            _db.People.Add(person);
            var budget = GetBudgetByPersonBudgetId(id);
            budget.People.Add(person);

            return _db.SaveChanges() == 1;
        }



        // GET PERSON BY BUDGET ID
        public IEnumerable<PersonListItem> GetPersonByBudgetId(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            var query = _db
                .People
                .Where(e => e.OwnerId == _userId && e.BudgetId == id)
                .ToList()
                .Select(
                e => new PersonListItem
                {
                    BudgetId = e.Budget.BudgetId,
                    PersonId = e.PersonId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PersonBudget = e.PersonBudget,
                    AmountLeft = e.AmountLeft,
                    CurrentBalance = e.CurrentBalance,
                    NumberOfGifts = e.NumberOfGifts
                });
            return query.ToArray();
        }


        // GET PERSON BY ID
        public PersonDetail GetPersonById(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            Person targetPerson = _db
                .People
                .Single(e => e.PersonId == id && e.OwnerId == _userId);

            return new PersonDetail
            {
                BudgetId = targetPerson.BudgetId,
                PersonId = targetPerson.PersonId,
                FirstName = targetPerson.FirstName,
                LastName = targetPerson.LastName,
                PersonBudget = targetPerson.PersonBudget,
                NumberOfGifts = targetPerson.NumberOfGifts,
                CurrentBalance = targetPerson.CurrentBalance,
                AmountLeft = targetPerson.AmountLeft
            };
        }


        // UPDATE
        public bool UpdatePerson(PersonEdit model)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            Person updatedPerson = _db
                .People
                .Where(e => e.OwnerId == _userId)
                .Single(e => e.PersonId == model.PersonId);

            var budget = GetBudgetByPersonBudgetId(updatedPerson.BudgetId);
            budget.People.Remove(updatedPerson);
            
            updatedPerson.PersonId = model.PersonId;
            updatedPerson.FirstName = model.FirstName;
            updatedPerson.LastName = model.LastName;
            updatedPerson.PersonBudget = model.PersonBudget;

            budget.People.Add(updatedPerson);

            return _db.SaveChanges() == 1;
        }


        // DELETE
        public bool DeletePerson(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            Person targetPerson = _db.People
                .Where(e => e.OwnerId == _userId)
                .Single(e => e.PersonId == id);

            _db.People.Remove(targetPerson);

            var budget = GetBudgetByPersonBudgetId(targetPerson.BudgetId);
            budget.People.Remove(targetPerson);

            return _db.SaveChanges() == 1;
        }



        public Budget GetBudgetByPersonBudgetId(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var budget = _db
                .Budgets
                .Single(e => e.OwnerId == _userId && e.BudgetId == id);

            return budget;
        }
    }
}
