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
    public partial class frmAddBrand : Form
    {
        public Boolean varFromProduct = false;

        public frmAddBrand()
        {
            InitializeComponent();
        }

        private void frmAddBrand_Load(object sender, EventArgs e)
        {
            txtBrand.Select();
            LoadDatagrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBrand.Text == "")
            {
                lblBrandError.Visible = true;
                txtBrand.Focus();
            }
            else
            {
                string retVal = addValidation();
                if (retVal == "true")
                {
                    addBrand();
                    if (varFromProduct)
                    {
                        varFromProduct = false;
                        Close();
                    }

                }
                else if (retVal == "false")
                {
                    warningAlert("Brand Already Exists.");
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
                comm.CommandText = "Proc_Brand_FetchinGridview";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvBrand.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvBrand.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;
                    }
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

                dgvBrand.ClearSelection();
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
                comm.CommandText = "Proc_Brand_Addvalidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Brand", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtBrand.Text.ToLower()).Trim());

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

                dgvBrand.ClearSelection();
            }
            return retVal;
        }

        private void addBrand()
        {
            DAL.DAL_Brand objDAL = new DAL.DAL_Brand();
            BAL.BAL_Brand objBAL = new BAL.BAL_Brand();
            try
            {
                objDAL.brandName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtBrand.Text.ToLower()).Trim());

                if (objBAL.addBrand(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("Brand Added successfully.");
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
                comm.CommandText = "Proc_Brand_UpdateValidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Brand", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtBrand.Text.ToLower()).Trim());
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

                dgvBrand.ClearSelection();
            }
            return retVal;
        }

        private void updateBrand()
        {
            DAL.DAL_Brand objDAL = new DAL.DAL_Brand();
            BAL.BAL_Brand objBAL = new BAL.BAL_Brand();

            try
            {
                objDAL.brandID = Convert.ToInt32(lblID.Text == "" ? "0" : lblID.Text);
                objDAL.brandName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtBrand.Text.ToLower()).Trim();

                if (objBAL.updateBrand(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("Brand Updated successfully.");
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
            if (txtBrand.Text == "")
            {
                lblBrandError.Visible = true;
                txtBrand.Focus();
            }
            else
            {
                string retVal = updateValidation();
                if (retVal == "true")
                {
                    updateBrand();
                    btnUpdate.Visible = false;
                    if (varFromProduct)
                    {
                        Close();
                    }
                }
                else if (retVal == "false")
                {
                    warningAlert("Brand Already Exists.");
                }
                else
                {
                    dangerAlert("Error! Please Try Again.");
                }
            }
        }

        private void searchBrand()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Brand_Search";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Brand", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtBrand.Text.ToLower()).Trim()));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvBrand.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvBrand.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;
                    }
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
            txtBrand.Text = "";

            lblBrandError.Visible = false;
            
            btnSave.TabIndex = 2;
            btnUpdate.TabIndex = 5;

            LoadDatagrid();
            txtBrand.Focus();
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

        private void txtBrand_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblBrandError.Visible = false;

            if (e.KeyChar == (char)13)
            {
                if (btnUpdate.Visible)
                {
                    if (txtBrand.Text == "")
                    {
                        lblBrandError.Visible = true;
                        txtBrand.Focus();
                    }
                    else
                    {
                        string retVal = updateValidation();
                        if (retVal == "true")
                        {
                            updateBrand();
                            btnUpdate.Visible = false;
                            if (varFromProduct)
                            {
                                Close();
                            }
                        }
                        else if (retVal == "false")
                        {
                            warningAlert("Brand Already Exists.");
                        }
                        else
                        {
                            dangerAlert("Error! Please Try Again.");
                        }
                    }
                }
                else
                {
                    if (txtBrand.Text == "")
                    {
                        lblBrandError.Visible = true;
                        txtBrand.Focus();
                    }
                    else
                    {
                        string retVal = addValidation();
                        if (retVal == "true")
                        {
                            addBrand();
                            if (varFromProduct)
                            {
                                varFromProduct = false;
                                Close();
                            }

                        }
                        else if (retVal == "false")
                        {
                            warningAlert("Brand Already Exists.");
                        }
                        else
                        {
                            dangerAlert("Error! Please Try Again.");
                        }
                    }
                }
            }
        }

        private void dgvBrand_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                lblID.Text = dgvBrand.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtBrand.Text = dgvBrand.Rows[e.RowIndex].Cells[2].Value.ToString();

                btnUpdate.Visible = true;
            }

            if (btnUpdate.Visible)
            {
                btnSave.TabIndex = 5;
                btnUpdate.TabIndex = 2;
            }
            else
            {
                btnSave.TabIndex = 2;
                btnUpdate.TabIndex = 5;
            }

            txtBrand.Focus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            btnUpdate.Visible = false;
            dgvBrand.ClearSelection();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtBrand_DoubleClick(object sender, EventArgs e)
        {
            txtBrand.SelectAll();
        }

        private void txtBrand_TextChanged(object sender, EventArgs e)
        {
            searchBrand();
        }

        private void txtBrand_Leave(object sender, EventArgs e)
        {
            txtBrand.DeselectAll();
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvBrand.ClearSelection();
                if (dgvBrand.RowCount > 0)
                {
                    dgvBrand.Rows[0].Selected = true;
                    dgvBrand.Select();
                }
            }
        }

        private void dgvBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lblID.Text = dgvBrand.SelectedRows[0].Cells[1].Value.ToString();
                txtBrand.Text = dgvBrand.SelectedRows[0].Cells[2].Value.ToString();

                btnUpdate.Visible = true;

                if (btnUpdate.Visible)
                {
                    btnSave.TabIndex = 5;
                    btnUpdate.TabIndex = 2;
                }
                else
                {
                    btnSave.TabIndex = 2;
                    btnUpdate.TabIndex = 5;
                }

                txtBrand.Focus();

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
                txtBrand.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}