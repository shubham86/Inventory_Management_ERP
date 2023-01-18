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
    public partial class frmAddUnits : Form
    {
        public Boolean varFromProduct = false;

        public frmAddUnits()
        {
            InitializeComponent();
        }

        private void frmAddUnits_Load(object sender, EventArgs e)
        {
            txtUnit.Select();
            LoadDatagrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUnit.Text == "")
            {
                lblUnitError.Visible = true;
                txtUnit.Focus();
            }
            else
            {
                string retVal = addValidation();
                if (retVal == "true")
                {
                    addUnit();
                    if (varFromProduct)
                    {
                        Close();
                    }
                }
                else if (retVal == "false")
                {
                    warningAlert("Unit is Allready Exist.");
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
                comm.CommandText = "Proc_Unit_FetchinGridview";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvUnitDetails.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvUnitDetails.Rows)
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

                dgvUnitDetails.ClearSelection();
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
                comm.CommandText = "Proc_Unit_Addvalidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("UnitName", txtUnit.Text);

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

                dgvUnitDetails.ClearSelection();
            }
            return retVal;
        }

        private void addUnit()
        {
            DAL.DAL_Unit objDAL = new DAL.DAL_Unit();
            BAL.BAL_Unit objBAL = new BAL.BAL_Unit();

            try
            {
                objDAL.unit = txtUnit.Text;

                if (objBAL.addUnit(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("Unit Added successfully.");
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
                comm.CommandText = "Proc_Unit_UpdateValidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("UnitName", txtUnit.Text);
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

                dgvUnitDetails.ClearSelection();
            }
            return retVal;
        }

        private void updateUnit()
        {
            DAL.DAL_Unit objDAL = new DAL.DAL_Unit();
            BAL.BAL_Unit objBAL = new BAL.BAL_Unit();

            try
            {
                objDAL.unitID = Convert.ToInt32(lblID.Text);
                objDAL.unit = txtUnit.Text;

                if (objBAL.updateUnit(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("Unit Updated successfully.");
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
            if (txtUnit.Text == "")
            {
                lblUnitError.Visible = true;
                txtUnit.Focus();
            }
            else
            {
                string retVal = updateValidation();
                if (retVal == "true")
                {
                    updateUnit();
                    btnUpdate.Visible = false;
                    if (varFromProduct)
                    {
                        Close();
                    }
                }
                else if (retVal == "false")
                {
                    warningAlert("Unit Already Exists.");
                }
                else
                {
                    dangerAlert("Error! Please Try Again.");
                }
            }
        }

        private void searchUnit()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Unit_Search";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("UnitName", txtUnit.Text);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvUnitDetails.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvUnitDetails.Rows)
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
            txtUnit.Text = "";

            lblUnitError.Visible = false;

            btnSave.TabIndex = 2;
            btnUpdate.TabIndex = 5;

            LoadDatagrid();
            txtUnit.Focus();
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

        private void txtUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblUnitError.Visible = false;
            if (e.KeyChar == (char)13)
            {
                if (btnUpdate.Visible)
                {
                    if (txtUnit.Text == "")
                    {
                        lblUnitError.Visible = true;
                        txtUnit.Focus();
                    }
                    else
                    {
                        string retVal = updateValidation();
                        if (retVal == "true")
                        {
                            updateUnit();
                            btnUpdate.Visible = false;
                            if (varFromProduct)
                            {
                                Close();
                            }
                        }
                        else if (retVal == "false")
                        {
                            warningAlert("Unit Already Exists.");
                        }
                        else
                        {
                            dangerAlert("Error! Please Try Again.");
                        }
                    }
                }
                else
                {
                    if (txtUnit.Text == "")
                    {
                        lblUnitError.Visible = true;
                        txtUnit.Focus();
                    }
                    else
                    {
                        string retVal = addValidation();
                        if (retVal == "true")
                        {
                            addUnit();
                            if (varFromProduct)
                            {
                                Close();
                            }
                        }
                        else if (retVal == "false")
                        {
                            warningAlert("Unit Already Exists.");
                        }
                        else
                        {
                            dangerAlert("Error! Please Try Again.");
                        }
                    }
                }
            }
        }

        private void dgvUnitDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                lblID.Text = dgvUnitDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtUnit.Text = dgvUnitDetails.Rows[e.RowIndex].Cells[2].Value.ToString();

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

            txtUnit.Focus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            btnUpdate.Visible = false;
            dgvUnitDetails.ClearSelection();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtUnit_DoubleClick(object sender, EventArgs e)
        {
            txtUnit.SelectAll();
        }

        private void txtUnit_TextChanged(object sender, EventArgs e)
        {
            searchUnit();
        }

        private void txtUnit_Leave(object sender, EventArgs e)
        {
            txtUnit.DeselectAll();
        }

        private void txtUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvUnitDetails.ClearSelection();
                if (dgvUnitDetails.RowCount > 0)
                {
                    dgvUnitDetails.Rows[0].Selected = true;
                    dgvUnitDetails.Select();
                }
            }
        }

        private void dgvUnitDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lblID.Text = dgvUnitDetails.SelectedRows[0].Cells[1].Value.ToString();
                txtUnit.Text = dgvUnitDetails.SelectedRows[0].Cells[2].Value.ToString();

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

                txtUnit.Focus();
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
                txtUnit.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}