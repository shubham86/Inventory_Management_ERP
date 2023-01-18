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
    public class BAL_Supplier
    {
        //Add Supplier
        public Boolean addSupplier(DAL.DAL_Supplier objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Supplier_Add";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("name", objDAL.supplierName);
                comm.Parameters.AddWithValue("mobile", objDAL.supplierMobile);
                comm.Parameters.AddWithValue("email", objDAL.supplierEmail);
                comm.Parameters.AddWithValue("address", objDAL.supplierAddress);
                comm.Parameters.AddWithValue("adhar", objDAL.adharNumber);
                comm.Parameters.AddWithValue("gstNo", objDAL.gstNumber);
                comm.Parameters.AddWithValue("previousBalance", objDAL.perviousBalance);
                comm.Parameters.AddWithValue("supplierAdvanceAmount", objDAL.supplierAdvanceAmount);
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

        //Update Supplier
        public Boolean updateSupplier(DAL.DAL_Supplier objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Supplier_Update";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("id", objDAL.supplierID);
                comm.Parameters.AddWithValue("name", objDAL.supplierName);
                comm.Parameters.AddWithValue("mobile", objDAL.supplierMobile);
                comm.Parameters.AddWithValue("email", objDAL.supplierEmail);
                comm.Parameters.AddWithValue("address", objDAL.supplierAddress);
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
    }
}
