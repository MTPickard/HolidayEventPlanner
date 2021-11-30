using HolidayPlanner.Data;
using HolidayPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlanner.Services
{
    public class GiftService
    {
        private readonly Guid _userId;

        public GiftService(Guid userId)
        {
            _userId = userId;
        }


        // CREATE
        public bool Create(int id, GiftCreate model)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            var entity = new Gift()
            {
                OwnerId = _userId,
                PersonId = id,
                Name = model.Name,
                Quantity = model.Quantity,
                Price = model.Price,
                DateAdded = DateTimeOffset.Now
            };


            _db.Gifts.Add(entity);

            var person = GetPersonByGiftPersonId(entity.PersonId);
            person.Gifts.Add(entity);

            return _db.SaveChanges() == 1;
        }


        // GET ALL GIFTS
        public IEnumerable<GiftListItem> GetGifts()
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            var query = _db
                .Gifts
                .Where(e => e.OwnerId == _userId)
                .Select(e =>
                new GiftListItem
                {
                    PersonId = e.Person.PersonId,
                    GiftId = e.GiftId,
                    Name = e.Name,
                    Price = e.Price,
                    Quantity = e.Quantity,
                    DateAdded = e.DateAdded
                });

            return query.ToArray();
        }

        public IEnumerable<GiftListItem> GetGiftByPersonId(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            var query = _db
                .Gifts
                .Where(e => e.OwnerId == _userId && e.PersonId == id)
                .Select(e => new GiftListItem
                {
                    PersonId = e.Person.PersonId,
                    GiftId = e.GiftId,
                    Name = e.Name,
                    Price = e.Price,
                    Quantity = e.Quantity,
                    DateAdded = e.DateAdded
                });

            return query.ToArray();
        }


        // GET GIFTS BY ID 
        public GiftDetail GetGiftsById(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            var entity = _db
                .Gifts
                .Single(e => e.GiftId == id && e.OwnerId == _userId);

            return new GiftDetail
            {
                PersonId = entity.PersonId,
                GiftId = entity.GiftId,
                Name = entity.Name,
                Quantity = entity.Quantity,
                Price = entity.Price,
                DateAdded = entity.DateAdded,
            };
        }

        public bool UpdateGift(GiftEdit model)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            var entity = _db
                .Gifts
                .Where(e => e.OwnerId == _userId)
                .Single(e => e.GiftId == model.GiftId);

            var person = GetPersonByGiftPersonId(entity.PersonId);
            person.Gifts.Remove(entity);

            entity.GiftId = model.GiftId;
            entity.Name = model.Name;
            entity.Quantity = model.Quantity;
            entity.Price = model.Price;
            entity.DateModified = DateTimeOffset.Now;

            person.Gifts.Add(entity);

            return _db.SaveChanges() == 1;
        }


        public bool DeleteGift(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var entity = _db.Gifts.Where(e => e.GiftId == id).Single(e => e.OwnerId == _userId);
            _db.Gifts.Remove(entity);

            var person = GetPersonByGiftPersonId(entity.PersonId);
            person.Gifts.Remove(entity);

            return _db.SaveChanges() == 1;
        }


        // HELPER METHOD



        public Person GetPersonByGiftPersonId(int id)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            Person targetPerson = _db
                .People
                .Single(e => e.PersonId == id && e.OwnerId == _userId);

            return targetPerson;
        }
    }
}
