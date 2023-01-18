using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class DAL_SaleReturnEntry
    {
        public int custID { get; set; }
        public int prodID { get; set; }
        public int billNumber { get; set; }
        public DateTime paymentDate { get; set; }
        public decimal totalPrice { get; set; }
        public decimal paidAmount { get; set; }
        public decimal balanceAmount { get; set; }
        public decimal prevBalance { get; set; }
        public decimal currentAdvance { get; set; }
        public decimal previousAdvance { get; set; }
        public int saleBillNumber { get; set; }
        public decimal retQty { get; set; }
        public decimal gst { get; set; }
        public decimal nowStock { get; set; }
        public decimal purchaseGST { get; set; }
        public decimal saleReturnRate { get; set; }
        public decimal totalReturnAmount { get; set; }
    }
}
