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

namespace JericoSmartGrocery.Reports
{
    public partial class frmCustomerListForLedger : Form
    {
        public frmCustomerListForLedger()
        {
            InitializeComponent();
        }

        private void frmCustomerListForLedger_Load(object sender, EventArgs e)
        {
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
                comm.CommandText = "Proc_Customer_Balance_FetchinGridview";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvCustomerDetails.DataSource = DT;

                    //int i = 1;
                    //foreach (DataGridViewRow row in dgvCustomerDetails.Rows)
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

                dgvCustomerDetails.ClearSelection();
            }
        }

        private void searchCustomer()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Customer_Balance_Search";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("customer", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtName.Text.ToLower()).Trim()));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvCustomerDetails.DataSource = DT;
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

                dgvCustomerDetails.ClearSelection();
            }
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
            txtName.Text = "";
            LoadDatagrid();
            txtName.Focus();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            searchCustomer();
        }


        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvCustomerDetails.ClearSelection();
                if (dgvCustomerDetails.RowCount > 0)
                {
                    dgvCustomerDetails.Rows[0].Selected = true;
                    dgvCustomerDetails.Select();
                }
            }
        }

        private void dgvCustomerDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                frmCustomerLedger frmCustomerLedger = new frmCustomerLedger();

                frmCustomerLedger.ID = dgvCustomerDetails.SelectedRows[0].Cells[1].Value.ToString();
                frmCustomerLedger.CustName = dgvCustomerDetails.SelectedRows[0].Cells[2].Value.ToString();
                frmCustomerLedger.balanceAmt = Convert.ToDecimal(dgvCustomerDetails.SelectedRows[0].Cells[6].Value);
                frmCustomerLedger.advanceAmt = Convert.ToDecimal(dgvCustomerDetails.SelectedRows[0].Cells[5].Value);

                Opacity = .50;
                frmCustomerLedger.ShowDialog();
                LoadDatagrid();
                Opacity = 1;

                e.Handled = true;
            }
        }

        private void dgvCustomerDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if click is on new row or header row
            if (e.RowIndex == dgvCustomerDetails.NewRowIndex || e.RowIndex < 0)
            {
                return;
            }
                        
            //Check if click is on Ledger
            if (e.ColumnIndex == 7)
            {
                frmCustomerLedger frmCustomerLedger = new frmCustomerLedger();

                frmCustomerLedger.ID = dgvCustomerDetails.SelectedRows[0].Cells[1].Value.ToString();
                frmCustomerLedger.CustName = dgvCustomerDetails.SelectedRows[0].Cells[2].Value.ToString();
                frmCustomerLedger.balanceAmt = Convert.ToDecimal(dgvCustomerDetails.SelectedRows[0].Cells[6].Value);
                frmCustomerLedger.advanceAmt = Convert.ToDecimal(dgvCustomerDetails.SelectedRows[0].Cells[5].Value);

                Opacity = .50;
                frmCustomerLedger.ShowDialog();
                LoadDatagrid();
                Opacity = 1;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                txtName.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
