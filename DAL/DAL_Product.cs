using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Product
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public string marathiName { get; set; }
        public int brandID { get; set; }
        public int unitID { get; set; }
        public Decimal currentStock { get; set; }
        public Decimal rateLess5 { get; set; }
        public Decimal rate5to10 { get; set; }
        public Decimal rateGreater10 { get; set; }
        public string description { get; set; }
        public int gstID { get; set; }
        public string barcode { get; set; }
        public string size { get; set; }
        public Decimal stockCr { get; set; }
        public Decimal stockDr { get; set; }
        public Decimal purchaseRate { get; set; }
        public string remark { get; set; }
    }
}
