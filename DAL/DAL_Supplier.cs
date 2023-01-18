using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Supplier
    {
        public int supplierID { get; set; }
        public string supplierName { get; set; }
        public string supplierMobile { get; set; }
        public string supplierEmail { get; set; }
        public string supplierAddress { get; set; }
        public string adharNumber { get; set; }
        public DateTime regDate { get; set; }
        public string gstNumber { get; set; }
        public Decimal perviousBalance { get; set; }
        public Decimal supplierAdvanceAmount { get; set; }
        public Decimal currentBalance { get; set; }
        public Boolean isActive { get; set; }
        public string pageNo { get; set; }
    }
}
