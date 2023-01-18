using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JericoSmartGrocery.PaymentMaster
{
    public partial class frmCustomerPendingPayments : Form
    {
        public frmCustomerPendingPayments()
        {
            InitializeComponent();
        }
        
        private void frmCustomerPendingPayments_Load(object sender, EventArgs e)
        {
            txtSearch.Focus();
            LoadDatagrid();
        }

        private void LoadDatagrid()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Customer_Pending_Payments";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count >= 0)
                {
                    dgvpaymentDetails.DataSource = DT;

                    //int i = 1;
                    //foreach (DataGridViewRow row in dgvpaymentDetails.Rows)
                    //{
                    //    row.Cells[0].Value = i;
                    //    i++;
                    //}
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                dangerAlert("Data Loading Error!");
                MessageBox.Show(x);
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                comm.Dispose();
                conn.Dispose();

                dgvpaymentDetails.ClearSelection();
            }
        }

        private void searchCustomer()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            string nameKeyword;
            string refNokeyword;

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Customer_Pending_Payments_Search";

                comm.Connection = conn;


                int parsedvalue;
                if (int.TryParse(txtSearch.Text, out parsedvalue))
                {
                    refNokeyword = txtSearch.Text;
                    nameKeyword = "";
                }
                else
                {
                    refNokeyword = "";
                    nameKeyword = txtSearch.Text;
                }

                comm.Parameters.AddWithValue("refNokeyword", refNokeyword);
                comm.Parameters.AddWithValue("nameKeyword", nameKeyword);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvpaymentDetails.DataSource = DT;

                    //int i = 1;
                    //foreach (DataGridViewRow row in dgvpaymentDetails.Rows)
                    //{
                    //    row.Cells[0].Value = i;
                    //    i++;
                    //}
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                dangerAlert("Data Loading Error!");
                MessageBox.Show(x);
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                comm.Dispose();
                conn.Dispose();

                dgvpaymentDetails.ClearSelection();
            }
        }

        private Boolean updateStatus(int ID)
        {
            Boolean retVal = false;
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SalepaymentDetails_isSuccessfull";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("salePayID", ID);

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
                lblAlert.Location = new Point(100, 8);
                dangerAlert("Record Removing Error!");
                MessageBox.Show(x);
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                comm.Dispose();
                conn.Dispose();
            }

            return retVal;
        }

        private async void successAlert(string msg)
        {
            lblAlert.Visible = true;
            pnlAlert.Visible = true;

            lblAlert.Text = msg;
            pnlAlert.BackColor = Color.FromArgb(48, 70, 38);
            lblAlert.ForeColor = Color.DarkSeaGreen;

            await Task.Delay(Convert.ToInt32(ConfigurationManager.AppSettings["alertTime"]));

            lblAlert.Visible = false;
            pnlAlert.Visible = false;
        }

        private async void warningAlert(string msg)
        {
            lblAlert.Visible = true;
            pnlAlert.Visible = true;

            lblAlert.Text = msg;
            pnlAlert.BackColor = Color.FromArgb(78, 70, 35);
            lblAlert.ForeColor = Color.Khaki;

            await Task.Delay(Convert.ToInt32(ConfigurationManager.AppSettings["alertTime"]));

            lblAlert.Visible = false;
            pnlAlert.Visible = false;
        }

        private async void dangerAlert(string msg)
        {
            lblAlert.Visible = true;
            pnlAlert.Visible = true;

            lblAlert.Text = msg;
            pnlAlert.BackColor = Color.FromArgb(54, 43, 41);
            lblAlert.ForeColor = Color.IndianRed;

            await Task.Delay(Convert.ToInt32(ConfigurationManager.AppSettings["alertTime"]));

            lblAlert.Visible = false;
            pnlAlert.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            LoadDatagrid();
            txtSearch.Focus();
        }
        
        private void dgvpaymentDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if click is on new row or header row
            if (e.RowIndex == dgvpaymentDetails.NewRowIndex || e.RowIndex < 0)
            {
                return;
            }

            //Check if click is on specific column 
            if (e.ColumnIndex == 6)
            {
                var confirmResult = MessageBox.Show("Are you sure ? Payment recived.", "Confirm Save !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    if (updateStatus(Convert.ToInt32(dgvpaymentDetails.SelectedRows[0].Cells[1].Value)))
                    {
                        successAlert("Payment recived successfilly.");
                    }
                    else
                    {
                        dangerAlert("Error! please try again.");
                    }
                    txtSearch.Text = "";
                    LoadDatagrid();
                    txtSearch.Focus();
                }
                else
                {
                    return;
                }               
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchCustomer();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                txtSearch.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
