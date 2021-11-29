using HolidayPlanner.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlanner.Models
{
    public class GiftListItem
    {
        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        public Person Person { get; set; }


        public int GiftId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return Price * Quantity;
            }
        }

        [Display(Name = "Date Added")]
        public DateTimeOffset DateAdded { get; set; }

        [Display(Name = "Date Modified")]
        public DateTimeOffset? DateModified { get; set; }
    }
}
