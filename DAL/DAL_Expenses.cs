using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Expenses
    { 
        public int expenseID { get; set; }
        public string type { get; set; }
        public string expenses { get; set; }
        public decimal expenseAmount { get; set; }
        public DateTime date { get; set; }
    }
}
