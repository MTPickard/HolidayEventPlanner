using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlanner.Models
{
    public class BudgetEdit
    {
        public int BudgetId { get; set; }

        public string BudgetName { get; set; }

        [Display(Name = "Budget")]
        public decimal BudgetAmount { get; set; }

        public DateTimeOffset? DateOfEvent { get; set; }

        public int NumberOfPaychecks { get; set; }

    }
}
