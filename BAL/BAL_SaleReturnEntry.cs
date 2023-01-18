using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
   public class BAL_SaleReturnEntry
    {
        public Boolean addSaleReturnEntry(DAL.DAL_SaleReturnEntry objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleReturn_Entry";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("custID", objDAL.custID);
                comm.Parameters.AddWithValue("billNumber", objDAL.billNumber);
                comm.Parameters.AddWithValue("paymentDate", objDAL.paymentDate);
                comm.Parameters.AddWithValue("totalPrice", objDAL.totalPrice);
                comm.Parameters.AddWithValue("paidAmount", objDAL.paidAmount);
                comm.Parameters.AddWithValue("balanceAmount", objDAL.balanceAmount);
                comm.Parameters.AddWithValue("previousAdvance", objDAL.previousAdvance);
                comm.Parameters.AddWithValue("prevBalance", objDAL.prevBalance);
                comm.Parameters.AddWithValue("currentAdvance", objDAL.currentAdvance);
                comm.Parameters.AddWithValue("saleBillNumber", objDAL.saleBillNumber);
                comm.Parameters.AddWithValue("prodID", objDAL.prodID);
                comm.Parameters.AddWithValue("retQty", objDAL.retQty);
                comm.Parameters.AddWithValue("gst", objDAL.gst);
                comm.Parameters.AddWithValue("nowStock", objDAL.nowStock);
                comm.Parameters.AddWithValue("purchaseGST", objDAL.purchaseGST);
                comm.Parameters.AddWithValue("saleReturnRate", objDAL.saleReturnRate);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                if (comm.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string x = ex.ToString();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                comm.Dispose();
            }
            return retVal;

        }

    }
}
