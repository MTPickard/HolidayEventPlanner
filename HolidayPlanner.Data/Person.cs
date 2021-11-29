using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlanner.Data
{
    public class Person
    {
        [ForeignKey(nameof(Budget))]
        public int BudgetId { get; set; }
        public virtual Budget Budget { get; set; }


        [Required]
        public Guid OwnerId { get; set; }

        [Key]
        public int PersonId { get; set; }


        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return ($"{FirstName} {LastName}");
            }
        }

        [Required]
        public decimal PersonBudget { get; set; }



        [Display(Name = "Amount Left")]
        public decimal AmountLeft
        {
            get
            {
                decimal totalLeft = 0;
                foreach (var gift in Gifts)
                {
                    totalLeft += (gift.Price * gift.Quantity);
                }

                return PersonBudget - totalLeft;
            }
        }

        public decimal CurrentBalance
        {
            get
            {
                decimal currentBalance = 0;
                foreach(var gift in Gifts)
                {
                    currentBalance += (gift.Price * gift.Quantity);
                }

                return currentBalance;
            }
        }


        [Display(Name = "Number of Gifts")]
        public int NumberOfGifts
        {
            get
            {
                int totalGifts = 0;
                foreach (var gift in Gifts)
                {
                    totalGifts += gift.Quantity;
                }
                return totalGifts;
            }
        }

        public virtual List<Gift> Gifts { get; set; }
    }
}
