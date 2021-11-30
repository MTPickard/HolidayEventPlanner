using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlanner.Models
{
    public class BudgetCreate
    {
        public int BudgetId { get; set; }

        [Display(Name = "Budget Name")]
        public string BudgetName { get; set; }

        [Display(Name = "Budget Amount")]
        public decimal BudgetAmount { get; set; }

        [Display(Name ="Date of Event")]
        public DateTimeOffset? DateOfEvent { get; set; }

        [Display(Name ="Number of Paychecks")]
        public int NumberOfPaychecks { get; set; }
    }
}
