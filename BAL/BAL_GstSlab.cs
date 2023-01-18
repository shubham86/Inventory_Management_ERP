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
    public class BAL_GstSlab
    {
        //Add GstSlab
        public Boolean addGstSlab(DAL.DAL_GstSlab objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_GstSlab_Add";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("slabName", objDAL.slabName);
                comm.Parameters.AddWithValue("cgst", objDAL.cgst);
                comm.Parameters.AddWithValue("sgst", objDAL.sgst);
                comm.Parameters.AddWithValue("gst", objDAL.gst);
                comm.Parameters.AddWithValue("isZero", objDAL.isZero);

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

        //Update GstSlab
        public Boolean updateGstSlab(DAL.DAL_GstSlab objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_GstSlab_Update";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("id", objDAL.gstID);
                comm.Parameters.AddWithValue("slabName", objDAL.slabName);
                comm.Parameters.AddWithValue("cgst", objDAL.cgst);
                comm.Parameters.AddWithValue("sgst", objDAL.sgst);
                comm.Parameters.AddWithValue("gst", objDAL.gst);

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
