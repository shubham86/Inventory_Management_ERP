using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class DAL_Purchase
    {
        public int purchaseID { get; set; }
        public string purchaseBillNumber { get; set; }
        public DateTime purchaseDate { get; set; }
        public int productID { get; set; }
        public Decimal purchaseRate { get; set; }
        public Decimal  purchaseQty { get; set; }
        public int  supplierID { get; set; }
        public string invoiceNumber { get; set; }

    }
}
