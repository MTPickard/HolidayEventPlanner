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
    public class PersonListItem
    {
        [ForeignKey(nameof(Budget))]
        public int BudgetId { get; set; }
        public Budget Budget { get; set; }


        public int PersonId { get; set; }


        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return ($"{FirstName} {LastName}");
            }
        }

        [Display(Name = "Budget")]
        public decimal PersonBudget { get; set; }


        [Display(Name = "Amount Left")]
        public decimal AmountLeft { get; set; }

        [Display(Name = "Current Balance")]
        public decimal CurrentBalance { get; set; }

        [Display(Name = "Number of Gifts")]
        public int NumberOfGifts { get; set; }


        public virtual List<Gift> Gifts { get; set; } = new List<Gift>();
    }
}
