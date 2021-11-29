using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlanner.Models
{
    public class GiftEdit
    {
        public int GiftId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Date Modified")]
        public DateTimeOffset? DateModified { get; set; }
    }
}
