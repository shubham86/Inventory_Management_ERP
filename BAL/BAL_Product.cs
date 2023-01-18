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
    public class BAL_Product
    {

        //Add Product
        public Boolean addProduct(DAL.DAL_Product objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Product_Add";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("productName", objDAL.productName);
                comm.Parameters.AddWithValue("marathiName", objDAL.marathiName);
                comm.Parameters.AddWithValue("brandID", objDAL.brandID);
                comm.Parameters.AddWithValue("unitID", objDAL.unitID);
                comm.Parameters.AddWithValue("currentStock", objDAL.currentStock);
                comm.Parameters.AddWithValue("rateLess5", objDAL.rateLess5);
                comm.Parameters.AddWithValue("rate5to10", objDAL.rate5to10);
                comm.Parameters.AddWithValue("rateGreater10", objDAL.rateGreater10);
                comm.Parameters.AddWithValue("gstID", objDAL.gstID);
                comm.Parameters.AddWithValue("description", objDAL.description);
                comm.Parameters.AddWithValue("purRate", objDAL.purchaseRate);

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

        //Update Product
        public Boolean updateProduct(DAL.DAL_Product objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Product_Update";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("id", objDAL.productID);
                comm.Parameters.AddWithValue("productName", objDAL.productName);
                comm.Parameters.AddWithValue("marathiName", objDAL.marathiName);
                comm.Parameters.AddWithValue("brandID", objDAL.brandID);
                comm.Parameters.AddWithValue("unitID", objDAL.unitID);
                comm.Parameters.AddWithValue("currentStock", objDAL.currentStock);
                comm.Parameters.AddWithValue("rateLess5", objDAL.rateLess5);
                comm.Parameters.AddWithValue("rate5to10", objDAL.rate5to10);
                comm.Parameters.AddWithValue("rateGreater10", objDAL.rateGreater10);
                comm.Parameters.AddWithValue("gstID", objDAL.gstID);
                comm.Parameters.AddWithValue("description", objDAL.description);
                comm.Parameters.AddWithValue("stockCr", objDAL.stockCr);
                comm.Parameters.AddWithValue("stockDr", objDAL.stockDr);

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

        //Update Product stock
        public Boolean updateProductStock(DAL.DAL_Product objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Stock_Update";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("id", objDAL.productID);
                comm.Parameters.AddWithValue("currentStock", objDAL.currentStock);
                comm.Parameters.AddWithValue("description", objDAL.description);
                comm.Parameters.AddWithValue("stockCr", objDAL.stockCr);
                comm.Parameters.AddWithValue("stockDr", objDAL.stockDr);
                comm.Parameters.AddWithValue("remark", objDAL.remark);

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

        //Update Product Price
        public Boolean updateProductPrice(DAL.DAL_Product objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleRate_Update";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("id", objDAL.productID);
                comm.Parameters.AddWithValue("rateLess5", objDAL.rateLess5);
                comm.Parameters.AddWithValue("rate5to10", objDAL.rate5to10);
                comm.Parameters.AddWithValue("rateGreater10", objDAL.rateGreater10);

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
