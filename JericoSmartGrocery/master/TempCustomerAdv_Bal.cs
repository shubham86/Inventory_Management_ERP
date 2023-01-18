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

namespace JericoSmartGrocery.master
{
    public partial class TempCustomerAdv_Bal : Form
    {
        public TempCustomerAdv_Bal()
        {
            InitializeComponent();
        }

        private void TempCustomerAdv_Bal_Load(object sender, EventArgs e)
        {

            LoadDatagrid();
            this.txtBalance.Enter += new EventHandler(txtBalance_Focus);
            this.txtAdvance.Enter += new EventHandler(txtAdvance_Focus);
        }

        protected void txtBalance_Focus(Object sender, EventArgs e)
        {
            txtBalance.SelectAll();
        }

        protected void txtAdvance_Focus(Object sender, EventArgs e)
        {
            txtAdvance.SelectAll();
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
                    dgvCustomer.DataSource = DT;

                    //int i = 1;
                    //foreach (DataGridViewRow row in dgvCustomer.Rows)
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

                dgvCustomer.ClearSelection();
            }
        }

        private void productSearch()
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
                    dgvCustomer.DataSource = DT;

                    //int i = 1;
                    //foreach (DataGridViewRow row in dgvCustomer.Rows)
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

                dgvCustomer.ClearSelection();
            }
        }

        private void updateAdvBal()
        {
            DAL.DAL_Customer objDAL = new DAL.DAL_Customer();
            BAL.BAL_Customer objBAL = new BAL.BAL_Customer();

            try
            {
                objDAL.customerID = Convert.ToInt32(lblID.Text);
                objDAL.currentBalance = Convert.ToDecimal(txtBalance.Text);
                objDAL.customerAdvanceAmount = Convert.ToDecimal(txtAdvance.Text);

                if (objBAL.updateBal_Adv(objDAL))
                {
                    successAlert("Bal_Adv update successfully.");
                }
                else
                {
                    dangerAlert("Error! Please Try Again.");
                }
            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                dangerAlert("Error! Please Try Again.");
                MessageBox.Show(ex.ToString());
            }

            txtAdvance.Text = "0.00";
            txtBalance.Text = "0.00";
            txtName.Text = "";
            lblID.Text = "0";

            txtName.Focus();
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
            txtBalance.Text = "";
            txtAdvance.Text = "";
            lblID.Text = "0";

            LoadDatagrid();
            txtName.Focus();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            productSearch();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            updateAdvBal();
            LoadDatagrid();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvCustomer.ClearSelection();
                if (dgvCustomer.RowCount > 0)
                {
                    dgvCustomer.Rows[0].Selected = true;
                    dgvCustomer.Select();
                }
            }
        }

        private void dgvCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtName.TextChanged -= new EventHandler(txtName_TextChanged);
                txtName.Text = dgvCustomer.SelectedRows[0].Cells[2].Value.ToString();
                this.txtName.TextChanged += new EventHandler(txtName_TextChanged);
                txtBalance.Text = dgvCustomer.SelectedRows[0].Cells[6].Value.ToString();
                txtAdvance.Text = dgvCustomer.SelectedRows[0].Cells[5].Value.ToString();
                lblID.Text = dgvCustomer.SelectedRows[0].Cells[1].Value.ToString();

                txtBalance.Focus();

                e.Handled = true;
            }
        }

        private void dgvCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.txtName.TextChanged -= new EventHandler(txtName_TextChanged);
                txtName.Text = dgvCustomer.SelectedRows[0].Cells[2].Value.ToString();
                this.txtName.TextChanged += new EventHandler(txtName_TextChanged);
                txtBalance.Text = dgvCustomer.SelectedRows[0].Cells[6].Value.ToString();
                txtAdvance.Text = dgvCustomer.SelectedRows[0].Cells[5].Value.ToString();
                lblID.Text = dgvCustomer.SelectedRows[0].Cells[1].Value.ToString();

                txtBalance.Focus();
            }
        }

        private void txtAdvance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtBalance_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtBalance_Leave(object sender, EventArgs e)
        {
            txtBalance.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtBalance.Text == "" ? "0.00" : txtBalance.Text));
            if (Convert.ToDecimal(txtBalance.Text) > 0)
            {
                txtAdvance.Text = "0.00";
            }
        }

        private void txtAdvance_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtAdvance_Leave(object sender, EventArgs e)
        {
            txtAdvance.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtAdvance.Text == "" ? "0.00" : txtAdvance.Text));
            if (Convert.ToDecimal(txtAdvance.Text) > 0)
            {
                txtBalance.Text = "0.00";
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
