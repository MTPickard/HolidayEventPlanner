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
    public class PersonCreate
    {
        [ForeignKey(nameof(Budget))]
        public int BudgetId { get; set; }
        public Budget Budget { get; set; }


        [Required]
        [MinLength(1, ErrorMessage = "Please enter first name.")]
        [MaxLength(25, ErrorMessage = "Too many characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Please enter last name.")]
        [MaxLength(25, ErrorMessage = "Too many characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Budget")]
        public decimal PersonBudget { get; set; }

    }
}
