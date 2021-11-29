using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlanner.Models
{
    public class BudgetDetail
    {
        public int BudgetId { get; set; }

        public string BudgetName { get; set; }

        [Display(Name = "Budget")]
        public decimal BudgetAmount { get; set; }

        public DateTimeOffset? DateOfEvent { get; set; }

        public int NumberOfPaychecks { get; set; }

        [Display(Name = "Amount Left")]
        public decimal AmountLeft { get; set; }

        [Display(Name ="Current Balance")]
        public decimal CurrentBalance { get; set; }

        [Display(Name = "Save Per Paycheck")]
        public decimal AmountToSavePerPaycheck { get; set; }
        
    }
}
