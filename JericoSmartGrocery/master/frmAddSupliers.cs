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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JericoSmartGrocery.master
{
    public partial class frmAddSupplier : Form
    {
        public Boolean varFromPurchase = false;

        public frmAddSupplier()
        {
            InitializeComponent();
        }

        private void frmAddSupplier_Load(object sender, EventArgs e)
        {
            btnExit.FlatAppearance.BorderColor = Color.White;
            btnExit.FlatAppearance.BorderSize = 2;

            this.txtBalance.Enter += new EventHandler(txtBalance_Enter); // enter event==get focus event 

            txtName.Select();
            LoadDatagrid();
        }

        protected void txtBalance_Enter(Object sender, EventArgs e)
        {
            txtBalance.SelectAll();
        }

        private void txtBalance_Leave(object sender, EventArgs e)
        {
            txtBalance.DeselectAll();
            txtBalance.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtBalance.Text == "" ? "0.00" : txtBalance.Text));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveBtnClick();
        }

        private void saveBtnClick()
        {
            string email = txtEmail.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            if (txtName.Text == "")
            {
                lblNameError.Visible = true;
                txtName.Focus();
            }
            else if (txtMobile.Text.Length < 10 && txtMobile.Text != "")
            {
                lblMobileError.Visible = true;
                txtMobile.Focus();
            }
            else if (!match.Success && txtEmail.Text != "")
            {
                lblEmailError.Visible = true;
                txtEmail.Focus();
            }
            else if (txtAddress.Text == "")
            {
                lblAddressError.Visible = true;
                txtAddress.Focus();
            }
            else if (txtAdhar.Text.Length < 12 && txtAdhar.Text != "")
            {
                lblAdharError.Visible = true;
                txtAdhar.Focus();
            }
            else if (Convert.ToDecimal(txtBalance.Text == "" ? "0.00" : txtBalance.Text) > 0 && Convert.ToDecimal(txtAdvAmt.Text == "" ? "0.00" : txtAdvAmt.Text) > 0)
            {
                MessageBox.Show("Either Balance amount or Advance amount should be '0.00'", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBalance.Text = "0.00";
                txtAdvAmt.Text = "0.00";
                txtBalance.Focus();
            }
            else
            {
                string retVal = addValidation();
                if (retVal == "true")
                {
                    addSupplier();
                    txtName.Focus();
                    if (varFromPurchase)
                    {
                        varFromPurchase = false;
                        Close();
                    }
                }
                else if (retVal == "false")
                {
                    warningAlert("Supplier Already Exists.");
                }
                else
                {
                    dangerAlert("Error! Please Try Again.");
                }
            }
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
                comm.CommandText = "Proc_Supplier_FetchinGridview";

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

        private string addValidation()
        {
            string retVal = "";

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Supplier_Addvalidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtName.Text.ToLower()).Trim()));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    retVal = "false";
                }
                else
                {
                    retVal = "true";
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                retVal = "error";
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
            return retVal;
        }

        private void addSupplier()
        {
            DAL.DAL_Supplier objDAL = new DAL.DAL_Supplier();
            BAL.BAL_Supplier objBAL = new BAL.BAL_Supplier();

            try
            {
                objDAL.supplierName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtName.Text.ToLower()).Trim());
                objDAL.supplierMobile = txtMobile.Text;
                objDAL.supplierEmail = txtEmail.Text;
                objDAL.supplierAddress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtAddress.Text.ToLower()).Trim());
                objDAL.adharNumber = txtAdhar.Text;
                objDAL.gstNumber = txtGST.Text;
                objDAL.perviousBalance = Convert.ToDecimal(txtBalance.Text == "" ? "0.00" : txtBalance.Text);
                objDAL.supplierAdvanceAmount = Convert.ToDecimal(txtAdvAmt.Text == "" ? "0.00" : txtAdvAmt.Text);
                objDAL.pageNo = txtPageNo.Text;

                if (objBAL.addSupplier(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("Supplier Added Successfully.");
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

        private string updateValidation()
        {
            string retVal = "";

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Supplier_UpdateValidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Name", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtName.Text.ToLower()).Trim()));
                comm.Parameters.AddWithValue("Id", lblID.Text);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    retVal = "false";
                }
                else
                {
                    retVal = "true";
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                retVal = "error";
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
            return retVal;
        }

        private void updateSupplier()
        {
            DAL.DAL_Supplier objDAL = new DAL.DAL_Supplier();
            BAL.BAL_Supplier objBAL = new BAL.BAL_Supplier();

            try
            {
                objDAL.supplierID = Convert.ToInt32(lblID.Text);
                objDAL.supplierName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtName.Text.ToLower()).Trim());
                objDAL.supplierMobile = txtMobile.Text;
                objDAL.supplierEmail = txtEmail.Text;
                objDAL.supplierAddress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtAddress.Text.ToLower()).Trim());
                objDAL.adharNumber = txtAdhar.Text;
                objDAL.gstNumber = txtGST.Text;
                objDAL.pageNo = txtPageNo.Text;

                if (objBAL.updateSupplier(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("Supplier Updated Successfully.");
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
            updateBtnClick();
        }

        private void updateBtnClick()
        {
            string email = txtEmail.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);

            if (txtName.Text == "")
            {
                lblNameError.Visible = true;
                txtName.Focus();
            }
            else if (txtMobile.Text.Length < 10 && txtMobile.Text != "")
            {
                lblMobileError.Visible = true;
                txtMobile.Focus();
            }
            else if (!match.Success && txtEmail.Text != "")
            {
                lblEmailError.Visible = true;
                txtEmail.Focus();
            }
            else if (txtAddress.Text == "")
            {
                lblAddressError.Visible = true;
                txtAddress.Focus();
            }
            else if (txtAdhar.Text.Length < 12 && txtAdhar.Text != "")
            {
                lblAdharError.Visible = true;
                txtAdhar.Focus();
            }
            else
            {
                string retVal = updateValidation();
                if (retVal == "true")
                {
                    updateSupplier();
                    btnUpdate.Visible = false;
                }
                else if (retVal == "false")
                {
                    warningAlert("Supplier Already Exists.");
                }
                else
                {
                    dangerAlert("Error! Please Try Again.");
                }
            }
        }

        private void searchSuplier()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Supplier_Search";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("suplier", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtName.Text.ToLower()).Trim()));

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
            }
        }

        public void ClearForm()
        {
            txtName.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtAdhar.Text = "";
            txtGST.Text = "";
            txtBalance.Text = "0.00";
            txtAdvAmt.Text = "0.00";
            txtPageNo.Text = "";

            lblAddressError.Visible = false;
            lblEmailError.Visible = false;
            lblMobileError.Visible = false;
            lblNameError.Visible = false;

            txtBalance.Enabled = true;
            txtAdvAmt.Enabled = true;

            btnSave.TabIndex = 10;
            btnUpdate.TabIndex = 13;

            LoadDatagrid();
            txtName.Focus();
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblMobileError.Visible = false;
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {            
            lblNameError.Visible = false;
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblAddressError.Visible = false;
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblEmailError.Visible = false;
        }

        private void txtAdhar_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblAdharError.Visible = false;
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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

        private void txtName_DoubleClick(object sender, EventArgs e)
        {
            txtName.SelectAll();
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

        private void dgvSupplierDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                lblID.Text = dgvSupplierDetails.Rows[e.RowIndex].Cells[1].Value.ToString();

                this.txtName.TextChanged -= new EventHandler(txtName_TextChanged);
                txtName.Text = dgvSupplierDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.txtName.TextChanged += new EventHandler(txtName_TextChanged);

                txtMobile.Text = dgvSupplierDetails.Rows[e.RowIndex].Cells[3].Value.ToString() == "-" ? "" : dgvSupplierDetails.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtEmail.Text = dgvSupplierDetails.Rows[e.RowIndex].Cells[4].Value.ToString() == "-" ? "" : dgvSupplierDetails.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtAddress.Text = dgvSupplierDetails.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtAdhar.Text = dgvSupplierDetails.Rows[e.RowIndex].Cells[6].Value.ToString() == "-" ? "" : dgvSupplierDetails.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtGST.Text = dgvSupplierDetails.Rows[e.RowIndex].Cells[8].Value.ToString() == "-" ? "" : dgvSupplierDetails.Rows[e.RowIndex].Cells[8].Value.ToString();
                txtPageNo.Text = dgvSupplierDetails.Rows[e.RowIndex].Cells[11].Value.ToString() == "-" ? "" : dgvSupplierDetails.Rows[e.RowIndex].Cells[11].Value.ToString();
                txtBalance.Enabled = false;
                txtAdvAmt.Enabled = false;
                btnUpdate.Visible = true;

            }

            if (btnUpdate.Visible)
            {
                btnSave.TabIndex = 13;
                btnUpdate.TabIndex = 10;
            }
            else
            {
                btnSave.TabIndex = 10;
                btnUpdate.TabIndex = 13;
            }

            txtName.Focus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            btnUpdate.Visible = false;
            dgvSupplierDetails.ClearSelection();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            searchSuplier();
        }

        private void txtAdvAmt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtAdvAmt_Leave(object sender, EventArgs e)
        {
            txtAdvAmt.DeselectAll();
            txtAdvAmt.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtAdvAmt.Text == "" ? "0.00" : txtAdvAmt.Text));
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtName.DeselectAll();
        }

        private void txtMobile_Leave(object sender, EventArgs e)
        {
            txtMobile.DeselectAll();
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            txtEmail.DeselectAll();
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            txtAddress.DeselectAll();
        }

        private void txtAdhar_Leave(object sender, EventArgs e)
        {
            txtAdhar.DeselectAll();
        }

        private void txtGST_Leave(object sender, EventArgs e)
        {
            txtGST.DeselectAll();
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
               
                lblID.Text = dgvSupplierDetails.SelectedRows[0].Cells[1].Value.ToString();

                this.txtName.TextChanged -= new EventHandler(txtName_TextChanged);
                txtName.Text = dgvSupplierDetails.SelectedRows[0].Cells[2].Value.ToString();
                this.txtName.TextChanged += new EventHandler(txtName_TextChanged);

                txtMobile.Text = dgvSupplierDetails.SelectedRows[0].Cells[3].Value.ToString() == "-" ? "" : dgvSupplierDetails.SelectedRows[0].Cells[3].Value.ToString();
                txtEmail.Text = dgvSupplierDetails.SelectedRows[0].Cells[4].Value.ToString() == "-" ? "" : dgvSupplierDetails.SelectedRows[0].Cells[4].Value.ToString();
                txtAddress.Text = dgvSupplierDetails.SelectedRows[0].Cells[5].Value.ToString();
                txtAdhar.Text = dgvSupplierDetails.SelectedRows[0].Cells[6].Value.ToString() == "-" ? "" : dgvSupplierDetails.SelectedRows[0].Cells[6].Value.ToString();
                txtGST.Text = dgvSupplierDetails.SelectedRows[0].Cells[8].Value.ToString() == "-" ? "" : dgvSupplierDetails.SelectedRows[0].Cells[8].Value.ToString();
                txtPageNo.Text = dgvSupplierDetails.SelectedRows[0].Cells[11].Value.ToString() == "-" ? "" : dgvSupplierDetails.SelectedRows[0].Cells[11].Value.ToString();
                txtBalance.Enabled = false;
                txtAdvAmt.Enabled = false;
                btnUpdate.Visible = true;


                if (btnUpdate.Visible)
                {
                    btnSave.TabIndex = 13;
                    btnUpdate.TabIndex = 10;
                }
                else
                {
                    btnSave.TabIndex = 10;
                    btnUpdate.TabIndex = 13;
                }

                txtName.Focus();
                e.Handled = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (varFromPurchase)
                {
                    varFromPurchase = false;
                    this.Close();
                    return true;
                }
            }
            else if (keyData == Keys.F1)
            {
                txtName.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtPageNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (btnUpdate.Visible)
                {
                    updateBtnClick();
                }
                else
                {
                    saveBtnClick();
                }
            }
        }

        private void txtBalance_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtAdvAmt_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtBalance_Click(object sender, EventArgs e)
        {
            txtBalance.SelectAll();
        }

        private void txtAdvAmt_Click(object sender, EventArgs e)
        {
            txtAdvAmt.SelectAll();
        }
    }
}
