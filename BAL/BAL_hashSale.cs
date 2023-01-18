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
     public class BAL_hashSale
    {
        //Add SaleHash entry
        public Boolean addHashSale(DAL.DAL_hashSale objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleHash_Add";

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
                comm.Parameters.AddWithValue("saleRate", objDAL.saleRate);
                comm.Parameters.AddWithValue("totalprice", objDAL.totalprice);
                comm.Parameters.AddWithValue("nowStock", objDAL.currentStock);
                comm.Parameters.AddWithValue("gstOnMargine", objDAL.gstOnMargine);
                comm.Parameters.AddWithValue("marathiName", objDAL.marathiName);
                comm.Parameters.AddWithValue("customerID", objDAL.customerID);
                comm.Parameters.AddWithValue("billNo", objDAL.billNo);

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

        //Add SaleProduct entry in sale table
        public Boolean addSaleProduct(DAL.DAL_hashSale objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleProduct_Add";

                comm.Connection = conn;
                 
                comm.Parameters.AddWithValue("saleBillNo", objDAL.billNo);
                comm.Parameters.AddWithValue("saleDate", objDAL.saleDate);
                comm.Parameters.AddWithValue("proID", objDAL.productID);
                comm.Parameters.AddWithValue("productUnitID", objDAL.unitID);
                comm.Parameters.AddWithValue("productBrandID", objDAL.brandID);
                comm.Parameters.AddWithValue("gstID", objDAL.gstID);
                comm.Parameters.AddWithValue("purchaseRate", objDAL.purchaseRate);
                comm.Parameters.AddWithValue("saleRate", objDAL.saleRate);
                comm.Parameters.AddWithValue("saleQTY", objDAL.qty);
                comm.Parameters.AddWithValue("totalPrice", objDAL.totalprice);
                comm.Parameters.AddWithValue("customerID", objDAL.customerID);
                comm.Parameters.AddWithValue("currentStock", objDAL.currentStock);
                comm.Parameters.AddWithValue("gstAmount", objDAL.gstamount);
                comm.Parameters.AddWithValue("gstOnMargine", objDAL.gstOnMargine);

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

        //Add Sale Payment entry in salePayment details table
        public Boolean addSalePayment(DAL.DAL_hashSale objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SalePaymentDetails_Add";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("custID", objDAL.customerID);
                comm.Parameters.AddWithValue("saleBillNo", objDAL.billNo);
                comm.Parameters.AddWithValue("paymentDate", objDAL.paymentDate);
                comm.Parameters.AddWithValue("paymentMode", objDAL.payMode);
                comm.Parameters.AddWithValue("refNumber", objDAL.refNumber);
                comm.Parameters.AddWithValue("billAmount", objDAL.billAmount);
                comm.Parameters.AddWithValue("otherCharges", objDAL.OtherCharges);
                comm.Parameters.AddWithValue("prevBalance", objDAL.prevBalance);
                comm.Parameters.AddWithValue("balanceAmount", objDAL.balanceAmount);
                comm.Parameters.AddWithValue("customerAdvanceAmount", objDAL.customerAdvanceAmount);
                comm.Parameters.AddWithValue("prevAdvanceAmount", objDAL.prevAdvanceAmount);
                comm.Parameters.AddWithValue("nowPaidAmount", objDAL.nowPaidAmount);
                comm.Parameters.AddWithValue("grandTotalAmount", objDAL.grandTotalAmount);
                comm.Parameters.AddWithValue("gstAmount", objDAL.totalGstAmount);
                comm.Parameters.AddWithValue("discount", objDAL.discount);
                comm.Parameters.AddWithValue("isSuccesfull", objDAL.isSuccesful);
                comm.Parameters.AddWithValue("isBalanceBill", objDAL.isBalanceBill);
                comm.Parameters.AddWithValue("isAdvanceBill", objDAL.isAdvanceBill);
                comm.Parameters.AddWithValue("commitDate", objDAL.commitDate);

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
        public int addCustomerPayment(DAL.DAL_hashSale objDAL)
        {
            int retVal = 0;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SalePaymentDetails_CustomerPaymentAdd";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("custID", objDAL.customerID);
                comm.Parameters.AddWithValue("paymentDate", objDAL.paymentDate);
                comm.Parameters.AddWithValue("paymentMode", objDAL.payMode);
                comm.Parameters.AddWithValue("refNumber", objDAL.refNumber);
                comm.Parameters.AddWithValue("prevBalance", objDAL.prevBalance);
                comm.Parameters.AddWithValue("balanceAmount", objDAL.balanceAmount);
                comm.Parameters.AddWithValue("customerAdvanceAmount", objDAL.customerAdvanceAmount);
                comm.Parameters.AddWithValue("prevAdvanceAmount", objDAL.prevAdvanceAmount);
                comm.Parameters.AddWithValue("nowPaidAmount", objDAL.nowPaidAmount);
                comm.Parameters.AddWithValue("discount", objDAL.discount);
                comm.Parameters.AddWithValue("isSuccesfull", objDAL.isSuccesful);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {                    
                    return Convert.ToInt32(DT.Rows[0]["salePaymentID"]);
                }
                else
                {
                    return 0;
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
        public Boolean addCustomerReturnCash(DAL.DAL_hashSale objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SalePaymentDetails_CustomerReturnCash";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("custID", objDAL.customerID);
                comm.Parameters.AddWithValue("paymentDate", objDAL.paymentDate);
                comm.Parameters.AddWithValue("prevBalance", objDAL.prevBalance);
                comm.Parameters.AddWithValue("balanceAmount", objDAL.balanceAmount);
                comm.Parameters.AddWithValue("customerAdvanceAmount", objDAL.customerAdvanceAmount);
                comm.Parameters.AddWithValue("prevAdvanceAmount", objDAL.prevAdvanceAmount);
                comm.Parameters.AddWithValue("nowPaidAmount", objDAL.nowPaidAmount);
                comm.Parameters.AddWithValue("isSuccesfull", '1');

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (comm.ExecuteNonQuery() > 0)
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
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
