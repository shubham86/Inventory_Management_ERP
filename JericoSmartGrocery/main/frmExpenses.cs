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

namespace JericoSmartGrocery.main
{
    public partial class frmExpenses : Form
    {
        public Boolean varFromProduct = false;

        public frmExpenses()
        {
            InitializeComponent();
        }

        private void frmExpenses_Load(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Now;
            cmbType.SelectedText = "-- Select Expense Type --";

            this.dtpTo.ValueChanged -= new EventHandler(dtpTo_ValueChanged);
            dtpTo.Value = DateTime.Now;
            dtpTo.MaxDate = DateTime.Now;
            this.dtpTo.ValueChanged += new EventHandler(dtpTo_ValueChanged);
            this.dtpFrom.ValueChanged -= new EventHandler(dtpFrom_ValueChanged);
            DateTime now = DateTime.Now;
            dtpFrom.Value = new DateTime(now.Year, now.Month, 1);//DateTime.Now;
            dtpFrom.MaxDate = DateTime.Now;
            this.dtpFrom.ValueChanged += new EventHandler(dtpFrom_ValueChanged);

            LoadDatagrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbType.Text == "-- Select Expense Type --")
            {
                lblTypeError.Visible = true;
                cmbType.Focus();
            }
            else if (txtExpenseAmount.Text == "" ||  Convert.ToDecimal(txtExpenseAmount.Text) <= 0)
            {
                lblAmountError.Visible = true;
                txtExpenseAmount.Focus();
            }
            else
            {               
                addExpense();                
            }
        }

        private void LoadDatagrid()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataSet DS = new DataSet();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Expenses_FetchinGridview";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("fromDate", dtpFrom.Value.ToString("yyyy-MM-dd"));
                comm.Parameters.AddWithValue("toDate", dtpTo.Value.ToString("yyyy-MM-dd"));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    dgvExpenses.DataSource = DS.Tables[0];

                    int i = 1;
                    foreach (DataGridViewRow row in dgvExpenses.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;
                    }

                    lblTotalExpenses.Text = DS.Tables[1].Rows[0]["totalExpense"].ToString();
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

                dgvExpenses.ClearSelection();
            }
        }

        private void addExpense()
        {
            DAL.DAL_Expenses objDAL = new DAL.DAL_Expenses();
            BAL.BAL_Expenses objBAL = new BAL.BAL_Expenses();
            try
            {
                objDAL.date = dtpDate.Value;
                objDAL.type = cmbType.Text;
                objDAL.expenses = txtDetails.Text;
                objDAL.expenseAmount = Convert.ToDecimal(txtExpenseAmount.Text);

                if (objBAL.addExpens(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("Expense Added successfully.");
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
                MessageBox.Show(x);
            }
        }
        
        private void updateExpense()
        {
            DAL.DAL_Expenses objDAL = new DAL.DAL_Expenses();
            BAL.BAL_Expenses objBAL = new BAL.BAL_Expenses();

            try
            {
                objDAL.expenseID = Convert.ToInt32(lblID.Text);
                objDAL.date = dtpDate.Value;
                objDAL.type = cmbType.Text;
                objDAL.expenses = txtDetails.Text;
                objDAL.expenseAmount = Convert.ToDecimal(txtExpenseAmount.Text);

                if (objBAL.updateExpense(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("Expense Updated successfully.");
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
                MessageBox.Show(x);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDetails.Text == "")
            {
                lblAmountError.Visible = true;
                txtDetails.Focus();
            }
            else
            {
                updateExpense();
                btnUpdate.Visible = false;
            }
        }

        private void searchExpense()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataSet DS = new DataSet();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Expenses_search";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("exType", cmbType.Text);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    dgvExpenses.DataSource = DS.Tables[0];

                    int i = 1;
                    foreach (DataGridViewRow row in dgvExpenses.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;
                    }

                    lblTotalExpenses.Text = DS.Tables[1].Rows[0]["totalExpense"].ToString();
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
            }
        }

        public void ClearForm()
        {
            txtDetails.Text = "";
            txtExpenseAmount.Text = "0.00";
            cmbType.SelectedIndex = 0;
            dtpDate.Value = DateTime.Now;

            lblAmountError.Visible = false;
            
            btnSave.TabIndex = 5;
            btnUpdate.TabIndex = 8;

            LoadDatagrid();
            txtDetails.Focus();
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
                
        private void dgvExpenses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                lblID.Text = dgvExpenses.Rows[e.RowIndex].Cells[1].Value.ToString();
                cmbType.Text = dgvExpenses.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtDetails.Text = dgvExpenses.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtExpenseAmount.Text = dgvExpenses.Rows[e.RowIndex].Cells[4].Value.ToString();
                dtpDate.Value= Convert.ToDateTime(dgvExpenses.Rows[e.RowIndex].Cells[5].Value);

                btnUpdate.Visible = true;
            }

            if (btnUpdate.Visible)
            {
                btnSave.TabIndex = 8;
                btnUpdate.TabIndex = 5;
            }
            else
            {
                btnSave.TabIndex = 5;
                btnUpdate.TabIndex = 8;
            }

            txtDetails.Focus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            btnUpdate.Visible = false;
            dgvExpenses.ClearSelection();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void dgvExpenses_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lblID.Text = dgvExpenses.SelectedRows[0].Cells[1].Value.ToString();
                txtDetails.Text = dgvExpenses.SelectedRows[0].Cells[2].Value.ToString();

                btnUpdate.Visible = true;

                if (btnUpdate.Visible)
                {
                    btnSave.TabIndex = 8;
                    btnUpdate.TabIndex = 5;
                }
                else
                {
                    btnSave.TabIndex = 5;
                    btnUpdate.TabIndex = 8;
                }

                txtDetails.Focus();

                e.Handled = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (varFromProduct)
                {
                    varFromProduct = false;
                    this.Close();
                    return true;
                }
            }
            else if (keyData == Keys.F1)
            {
                txtDetails.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void cmbType_Click(object sender, EventArgs e)
        {
            lblTypeError.Visible = false;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTypeError.Visible = false;
            searchExpense();
        }

        private void txtExpenseAmount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtExpenseAmount_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtExpenseAmount_Leave(object sender, EventArgs e)
        {
            txtExpenseAmount.DeselectAll();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFrom.Value > dtpTo.Value)
            {
                dtpTo.Value = dtpFrom.Value;
            }
            dtpTo.MaxDate = DateTime.Now;
            LoadDatagrid();
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            dtpFrom.MaxDate = DateTime.Now;
            LoadDatagrid();
        }
    }
}