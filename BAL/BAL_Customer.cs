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
    public class BAL_Customer
    {
        //Add Customer
        public int addCustomer(DAL.DAL_Customer objDAL)
        {
            int retVal = 0;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Customer_Add";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("name", objDAL.customerName);
                comm.Parameters.AddWithValue("mobile", objDAL.customerMobile);
                comm.Parameters.AddWithValue("email", objDAL.customerEmail);
                comm.Parameters.AddWithValue("address", objDAL.customerAddress);
                comm.Parameters.AddWithValue("adhar", objDAL.adharNumber);
                comm.Parameters.AddWithValue("gstNo", objDAL.gstNumber);
                comm.Parameters.AddWithValue("currentBalance", objDAL.currentBalance);
                comm.Parameters.AddWithValue("customerAdvanceAmount", objDAL.customerAdvanceAmount);
                comm.Parameters.AddWithValue("pageNo", objDAL.pageNo);

             try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (Convert.ToInt32(DT.Rows[0]["returnID"]) > 0)
                {
                    retVal = Convert.ToInt32(DT.Rows[0]["returnID"]);
                    return retVal;
                }
                else
                {
                    return retVal;
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

        //Update Customer
        public Boolean updateCustomer(DAL.DAL_Customer objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Customer_Update";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("id", objDAL.customerID);
                comm.Parameters.AddWithValue("name", objDAL.customerName);
                comm.Parameters.AddWithValue("mobile", objDAL.customerMobile);
                comm.Parameters.AddWithValue("email", objDAL.customerEmail);
                comm.Parameters.AddWithValue("address", objDAL.customerAddress);
                comm.Parameters.AddWithValue("adhar", objDAL.adharNumber);
                comm.Parameters.AddWithValue("gstNo", objDAL.gstNumber);
                comm.Parameters.AddWithValue("pageNo", objDAL.pageNo);

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

        //Update Balance and Advance
        public Boolean updateBal_Adv(DAL.DAL_Customer objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Customer_Balance-Advance_Update";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("id", objDAL.customerID);
                comm.Parameters.AddWithValue("balance", objDAL.currentBalance);
                comm.Parameters.AddWithValue("advance", objDAL.customerAdvanceAmount);

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
