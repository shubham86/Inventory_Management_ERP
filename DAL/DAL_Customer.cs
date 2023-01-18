using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Customer
    {
        public int customerID { get; set; }
        public string customerName { get; set; }
        public string customerMobile { get; set; }
        public string customerEmail { get; set; }
        public string customerAddress { get; set; }
        public string adharNumber { get; set; }
        public DateTime regDate { get; set; }
        public string gstNumber { get; set; }
        public Decimal perviousBalance { get; set; }
        public Decimal currentBalance { get; set; }
        public Decimal customerAdvanceAmount { get; set; }
        public Boolean isActive { get; set; }
        public string pageNo { get; set; }
    }
}
