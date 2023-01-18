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
    public partial class frmSupplierListForLedger : Form
    {
        public frmSupplierListForLedger()
        {
            InitializeComponent();
        }

        private void frmSupplierListForLedger_Load(object sender, EventArgs e)
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
                comm.CommandText = "Proc_Supplier_Balance_FetchinGridview";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvSupplierDetails.DataSource = DT;

                    //int i = 1;
                    //foreach (DataGridViewRow row in dgvSupplierDetails.Rows)
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

                dgvSupplierDetails.ClearSelection();
            }
        }

        private void searchSupplier()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Supplier_Balance_Search";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("supplier", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtName.Text.ToLower()).Trim()));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvSupplierDetails.DataSource = DT;

                    //int i = 1;
                    //foreach (DataGridViewRow row in dgvSupplierDetails.Rows)
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

                dgvSupplierDetails.ClearSelection();
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
            searchSupplier();
        }


        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvSupplierDetails.ClearSelection();
                if (dgvSupplierDetails.RowCount > 0)
                {
                    dgvSupplierDetails.Rows[0].Selected = true;
                    dgvSupplierDetails.Select();
                }
            }
        }

        private void dgvSupplierDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                frmSupplierLedger frmSupplierLedger = new frmSupplierLedger();

                frmSupplierLedger.ID = dgvSupplierDetails.SelectedRows[0].Cells[1].Value.ToString();
                frmSupplierLedger.CustName = dgvSupplierDetails.SelectedRows[0].Cells[2].Value.ToString();
                frmSupplierLedger.balanceAmt = Convert.ToDecimal(dgvSupplierDetails.SelectedRows[0].Cells[6].Value);
                frmSupplierLedger.advanceAmt = Convert.ToDecimal(dgvSupplierDetails.SelectedRows[0].Cells[5].Value);

                Opacity = .50;
                frmSupplierLedger.ShowDialog();
                LoadDatagrid();
                Opacity = 1;

                e.Handled = true;
            }
        }

        private void dgvSupplierDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if click is on new row or header row
            if (e.RowIndex == dgvSupplierDetails.NewRowIndex || e.RowIndex < 0)
            {
                return;
            }
                        
            //Check if click is on Ledger
            if (e.ColumnIndex == 7)
            {
                frmSupplierLedger frmSupplierLedger = new frmSupplierLedger();

                frmSupplierLedger.ID = dgvSupplierDetails.SelectedRows[0].Cells[1].Value.ToString();
                frmSupplierLedger.CustName = dgvSupplierDetails.SelectedRows[0].Cells[2].Value.ToString();
                frmSupplierLedger.balanceAmt = Convert.ToDecimal(dgvSupplierDetails.SelectedRows[0].Cells[6].Value);
                frmSupplierLedger.advanceAmt = Convert.ToDecimal(dgvSupplierDetails.SelectedRows[0].Cells[5].Value);

                Opacity = .50;
                frmSupplierLedger.ShowDialog();
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
