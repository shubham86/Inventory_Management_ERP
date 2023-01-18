using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_GstSlab
    {
        public int gstID { get; set; }
        public string slabName { get; set; }
        public Decimal cgst { get; set; }
        public Decimal sgst { get; set; }
        public Decimal gst { get; set; }
        public Int32 isZero { get; set; }
    }
}
