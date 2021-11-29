using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlanner.Data
{
    public class Gift
    {
        [ForeignKey(nameof(Person))]
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }


        //[ForeignKey(nameof(Budget))]
        //public int BudgetId { get; set; }
        //public virtual Budget Budget { get; set; }


        public Guid OwnerId { get; set; }

        [Key]
        public int GiftId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return Price * Quantity;
            }
        }

        public DateTimeOffset DateAdded { get; set; }

        public DateTimeOffset? DateModified { get; set; }
    }
}
