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
   public class BAL_hashPurchase
    {
        //Add PurchaseHash entry
        public Boolean addHashPurchase(DAL.DAL_hashPurchase objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_PurchaseHash_Add";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("productID", objDAL.productID);
                comm.Parameters.AddWithValue("productName", objDAL.productName);
                comm.Parameters.AddWithValue("brandID", objDAL.brandID);
                comm.Parameters.AddWithValue("brand", objDAL.brand);
                comm.Parameters.AddWithValue("gstID", objDAL.gstID);
                comm.Parameters.AddWithValue("gst", objDAL.gst);
                comm.Parameters.AddWithValue("gstamount", objDAL.gstamount);
                comm.Parameters.AddWithValue("unitID", objDAL.unitID);
                comm.Parameters.AddWithValue("unit", objDAL.unit);
                comm.Parameters.AddWithValue("qty", objDAL.qty);
                comm.Parameters.AddWithValue("purchaseRate", objDAL.purchaseRate);
                comm.Parameters.AddWithValue("rateLess5", objDAL.rateLess5);
                comm.Parameters.AddWithValue("rate5to10", objDAL.rate5to10);
                comm.Parameters.AddWithValue("rateGreater10", objDAL.rateGreater10);
                comm.Parameters.AddWithValue("totalprice", objDAL.totalprice);
                comm.Parameters.AddWithValue("nowStock", objDAL.currentStock);

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

        //Add PurchaseProduct entry in purchase table
        public Boolean addPurchaseProduct(DAL.DAL_hashPurchase objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_PurchaseProduct_Add";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("purchaseBillNo", objDAL.billNo);
                comm.Parameters.AddWithValue("purchaseDate", objDAL.purchaseDate);
                comm.Parameters.AddWithValue("proID", objDAL.productID);
                comm.Parameters.AddWithValue("productUnitID", objDAL.unitID);
                comm.Parameters.AddWithValue("productBrandID", objDAL.brandID);
                comm.Parameters.AddWithValue("gstID", objDAL.gstID);
                comm.Parameters.AddWithValue("purchaseRate", objDAL.purchaseRate);
                comm.Parameters.AddWithValue("rateLess5", objDAL.rateLess5);
                comm.Parameters.AddWithValue("rate5to10", objDAL.rate5to10);
                comm.Parameters.AddWithValue("rateGreater10", objDAL.rateGreater10);
                comm.Parameters.AddWithValue("totalprice", objDAL.totalprice);
                comm.Parameters.AddWithValue("purQty", objDAL.qty);
                comm.Parameters.AddWithValue("supplierID", objDAL.supplierID);
                comm.Parameters.AddWithValue("currentStock", objDAL.currentStock);
                comm.Parameters.AddWithValue("gstAmount", objDAL.gstamount);

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

        //Add Purchase Payment entry in purchasePayment details table
        public Boolean addPurchasePayment(DAL.DAL_hashPurchase objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_PurchasePaymentDetails_Add";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("suppID", objDAL.supplierID);
                comm.Parameters.AddWithValue("purchaseBillNo", objDAL.billNo);
                comm.Parameters.AddWithValue("paymentDate", objDAL.paymentDate);
                comm.Parameters.AddWithValue("paymentMode", objDAL.payMode);
                comm.Parameters.AddWithValue("refNumber", objDAL.refNumber);
                comm.Parameters.AddWithValue("billAmount", objDAL.billAmount);
                comm.Parameters.AddWithValue("otherCharges", objDAL.OtherCharges);
                comm.Parameters.AddWithValue("prevBalance", objDAL.prevBalance);
                comm.Parameters.AddWithValue("balanceAmount", objDAL.balanceAmount);
                comm.Parameters.AddWithValue("supplierAdvanceAmount", objDAL.supplierAdvanceAmount);
                comm.Parameters.AddWithValue("prevAdvanceAmount", objDAL.prevAdvanceAmount);
                comm.Parameters.AddWithValue("nowPaidAmount", objDAL.nowPaidAmount);
                comm.Parameters.AddWithValue("grandTotalAmount", objDAL.grandTotalAmount);
                comm.Parameters.AddWithValue("gstAmount", objDAL.totalGstAmount);
                comm.Parameters.AddWithValue("invoiceNumber", objDAL.invoiceNo);
                comm.Parameters.AddWithValue("isSuccesful", objDAL.isSuccesful);
                comm.Parameters.AddWithValue("discount", objDAL.discount);

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

        //Add Supplier Payment entry
        public Boolean addSupplierPayment(DAL.DAL_hashPurchase objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_PurchasePaymentDetails_SupplierPaymentAdd";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("suppID", objDAL.supplierID);
                comm.Parameters.AddWithValue("paymentDate", objDAL.paymentDate);
                comm.Parameters.AddWithValue("paymentMode", objDAL.payMode);
                comm.Parameters.AddWithValue("refNumber", objDAL.refNumber);
                comm.Parameters.AddWithValue("prevBalance", objDAL.prevBalance);
                comm.Parameters.AddWithValue("balanceAmount", objDAL.balanceAmount);
                comm.Parameters.AddWithValue("supplierAdvanceAmount", objDAL.supplierAdvanceAmount);
                comm.Parameters.AddWithValue("prevAdvanceAmount", objDAL.prevAdvanceAmount);
                comm.Parameters.AddWithValue("nowPaidAmount", objDAL.nowPaidAmount);
                comm.Parameters.AddWithValue("discount", objDAL.discount);
                comm.Parameters.AddWithValue("isSuccesful", objDAL.isSuccesful);

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
