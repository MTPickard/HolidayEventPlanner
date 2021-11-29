using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayPlanner.Data
{
    public class Budget
    {
        public Guid OwnerId { get; set; }

        [Key]
        public int BudgetId { get; set; }

        public string BudgetName { get; set; }

        public decimal BudgetAmount { get; set; }

        public DateTimeOffset? DateOfEvent { get; set; }

        public int NumberOfPaychecks { get; set; }

        public decimal AmountLeft
        {
            get
            {
                decimal totalSpent = 0;
                foreach (var person in People)
                {
                    totalSpent += (person.PersonBudget - person.AmountLeft);
                }
                return BudgetAmount - totalSpent;
            }
        }

        public decimal CurrentBalance
        {
            get
            {
                decimal currentBalance = 0;
                foreach(var person in People)
                {
                    currentBalance += (person.CurrentBalance);
                }

                return currentBalance;
            }
        }


        public decimal AmountToSavePerPaycheck
        {
            get
            {
                return CurrentBalance / NumberOfPaychecks;
            }
        }

        public virtual List<Person> People { get; set; }
    }
}
