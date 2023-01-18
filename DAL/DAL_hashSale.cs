using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_hashSale
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public string marathiName { get; set; }
        public int brandID { get; set; }
        public string brand { get; set; }
        public int gstID { get; set; }
        public Decimal gst { get; set; }
        public Decimal gstamount { get; set; }
        public int unitID { get; set; }
        public string unit { get; set; }
        public Decimal qty { get; set; }
        public Decimal purchaseRate { get; set; }
        public Decimal saleRate { get; set; }
        public Decimal totalprice { get; set; }


        public int customerID { get; set; }
        public int billNo { get; set; }
        public Decimal billAmount { get; set; }
        public Decimal OtherCharges { get; set; }
        public Decimal grandTotalAmount { get; set; }
        public Decimal totalGstAmount { get; set; }
        public Decimal prevBalance { get; set; }
        public Decimal balanceAmount { get; set; }
        public Decimal customerAdvanceAmount { get; set; }
        public Decimal prevAdvanceAmount { get; set; }
        public Decimal nowPaidAmount { get; set; }
        public Decimal currentStock { get; set; }
        public Decimal discount { get; set; }
        public Decimal gstOnMargine { get; set; }
        public string payMode { get; set; }
        public string refNumber { get; set; }
        public DateTime paymentDate { get; set; }
        public DateTime saleDate { get; set; }
        public Boolean isSuccesful { get; set; }
        public Boolean isBalanceBill { get; set; }
        public Boolean isAdvanceBill { get; set; }

        public string commitDate { get; set; }
    }
}
