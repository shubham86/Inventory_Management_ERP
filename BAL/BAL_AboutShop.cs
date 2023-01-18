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
    public class BAL_AboutShop
    {
        // ADD SHOP INFO
        public Boolean addShopInfo(DAL.DAL_AboutShop objBAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_aboutShop_Add";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("ownerName", objBAL.ownerName);
                comm.Parameters.AddWithValue("contactOne", objBAL.contactOne);
                comm.Parameters.AddWithValue("contactTwo", objBAL.contactTwo);
                comm.Parameters.AddWithValue("email", objBAL.emailID);
                comm.Parameters.AddWithValue("address", objBAL.address);
                comm.Parameters.AddWithValue("gstNumber", objBAL.gst);
                comm.Parameters.AddWithValue("shopName", objBAL.shopName);
                comm.Parameters.AddWithValue("termsConditions", objBAL.termOne);
                comm.Parameters.AddWithValue("termsConditions2", objBAL.termTwo);
                comm.Parameters.AddWithValue("termsConditions3", objBAL.termThree);

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
                // throw ex;
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


        public Boolean updateShopInfo(DAL.DAL_AboutShop objBAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "PROC_aboutShopUpdate";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("ownerName", objBAL.ownerName);
                comm.Parameters.AddWithValue("contactOne", objBAL.contactOne);
                comm.Parameters.AddWithValue("contactTwo", objBAL.contactTwo);
                comm.Parameters.AddWithValue("email", objBAL.emailID);
                comm.Parameters.AddWithValue("address", objBAL.address);
                comm.Parameters.AddWithValue("gstNumber", objBAL.gst);
                comm.Parameters.AddWithValue("shopName", objBAL.shopName);
                comm.Parameters.AddWithValue("termsConditions", objBAL.termOne);

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
                // throw ex;
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
