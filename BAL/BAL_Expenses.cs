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
    public class BAL_Expenses
    {
        //Add Expenses
        public Boolean addExpens(DAL.DAL_Expenses objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Expense_Add";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("expDate", objDAL.date);
                comm.Parameters.AddWithValue("exptype", objDAL.type);
                comm.Parameters.AddWithValue("expDetails", objDAL.expenses);
                comm.Parameters.AddWithValue("expAmount", objDAL.expenseAmount);

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

        //Update Expense
        public Boolean updateExpense(DAL.DAL_Expenses objDAL)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Expense_Update";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("id", objDAL.expenseID);
                comm.Parameters.AddWithValue("expDate", objDAL.date);
                comm.Parameters.AddWithValue("exptype", objDAL.type);
                comm.Parameters.AddWithValue("expDetails", objDAL.expenses);
                comm.Parameters.AddWithValue("expAmount", objDAL.expenseAmount);

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
