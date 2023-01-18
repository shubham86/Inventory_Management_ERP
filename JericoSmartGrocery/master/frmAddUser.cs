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
    public partial class frmAddUser : Form
    {
        public frmAddUser()
        {
            InitializeComponent();
        }

        private void frmAddUser_Load(object sender, EventArgs e)
        {
            txtName.Select();
            LoadDatagrid();
            cmbRole.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                lblNameError.Visible = true;
                txtName.Focus();
            }
            else if(txtUserName.Text == "")
            {
                lblUserNameError.Visible = true;
                txtUserName.Focus();
            }
            else if (txtPassword.Text == "")
            {
                lblPasswordError.Visible = true;
                txtPassword.Focus();
            }
            else
            {
                string retVal = addValidation();
                if (retVal == "true")
                {
                    addUser();
                }
                else if (retVal == "false")
                {
                    warningAlert("User Already Exists.");
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
                comm.CommandText = "Proc_User_FetchinGridview";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvUser.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvUser.Rows)
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

                dgvUser.ClearSelection();
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
                comm.CommandText = "Proc_User_Addvalidation";

                comm.Connection = conn;
                                
                comm.Parameters.AddWithValue("Uname", txtUserName.Text);

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

                dgvUser.ClearSelection();
            }
            return retVal;
        }

        private void addUser()
        {
            DAL.DAL_User objDAL = new DAL.DAL_User();
            BAL.BAL_User objBAL = new BAL.BAL_User();

            try
            {
                objDAL.fullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtName.Text.ToLower()).Trim());
                objDAL.userName = txtUserName.Text;
                objDAL.password = txtPassword.Text;
                objDAL.userRole = cmbRole.SelectedItem.ToString();

                if (objBAL.addUser(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("User Added successfully.");
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
                comm.CommandText = "Proc_User_UpdateValidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Id", lblID.Text);
                comm.Parameters.AddWithValue("Uname", txtUserName.Text);

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

                dgvUser.ClearSelection();
            }
            return retVal;
        }

        private void updateUser()
        {
            DAL.DAL_User objDAL = new DAL.DAL_User();
            BAL.BAL_User objBAL = new BAL.BAL_User();

            try
            {
                objDAL.userID = Convert.ToInt32(lblID.Text);
                objDAL.fullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtName.Text.ToLower()).Trim());
                objDAL.userName = txtUserName.Text;
                objDAL.password = txtPassword.Text;
                objDAL.userRole = cmbRole.SelectedItem.ToString();

                if (objBAL.updateUser(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("User Updated successfully.");
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
            if (txtName.Text == "")
            {
                lblNameError.Visible = true;
                txtName.Focus();
            }
            else if (txtUserName.Text == "")
            {
                lblUserNameError.Visible = true;
                txtUserName.Focus();
            }
            else if (txtPassword.Text == "")
            {
                lblPasswordError.Visible = true;
                txtPassword.Focus();
            }
            else
            {
                string retVal = updateValidation();
                if (retVal == "true")
                {
                    updateUser();
                    btnUpdate.Visible = false;
                }
                else if (retVal == "false")
                {
                    warningAlert("User Already Exists.");
                }
                else
                {
                    dangerAlert("Error! Please Try Again.");
                }
            }
        }


        private void searchUser()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_User_Search";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Uname", txtName.Text);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvUser.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvUser.Rows)
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
            txtName.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
            cmbRole.Text = "Operator";

            lblNameError.Visible = false;
            lblPasswordError.Visible = false;
            lblUserNameError.Visible = false;

            btnSave.TabIndex = 5;
            btnUpdate.TabIndex = 10;

            LoadDatagrid();
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

        private void dgvUser_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                lblID.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();

                this.txtName.TextChanged -= new EventHandler(txtName_TextChanged);
                txtName.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.txtName.TextChanged -= new EventHandler(txtName_TextChanged);

                txtUserName.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtPassword.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();
                cmbRole.Text = dgvUser.Rows[e.RowIndex].Cells[5].Value.ToString();
                btnUpdate.Visible = true;
                txtName.Select();
            }

            if (btnUpdate.Visible)
            {
                btnSave.TabIndex = 10;
                btnUpdate.TabIndex = 5;
            }
            else
            {
                btnSave.TabIndex = 5;
                btnUpdate.TabIndex = 10;
            }

            txtName.Focus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            btnUpdate.Visible = false;
            dgvUser.ClearSelection();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblNameError.Visible = false;           
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblUserNameError.Visible = false;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblPasswordError.Visible = false;
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtName.DeselectAll();
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            txtUserName.DeselectAll();
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.DeselectAll();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            searchUser();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvUser.ClearSelection();
                if (dgvUser.RowCount > 0)
                {
                    dgvUser.Rows[0].Selected = true;
                    dgvUser.Select();
                }
            }
        }

        private void dgvUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lblID.Text = dgvUser.SelectedRows[0].Cells[1].Value.ToString();

                this.txtName.TextChanged -= new EventHandler(txtName_TextChanged);
                txtName.Text = dgvUser.SelectedRows[0].Cells[2].Value.ToString();
                this.txtName.TextChanged -= new EventHandler(txtName_TextChanged);

                txtUserName.Text = dgvUser.SelectedRows[0].Cells[3].Value.ToString();
                txtPassword.Text = dgvUser.SelectedRows[0].Cells[4].Value.ToString();
                cmbRole.Text = dgvUser.SelectedRows[0].Cells[5].Value.ToString();
                btnUpdate.Visible = true;
                txtName.Select();

                if (btnUpdate.Visible)
                {
                    btnSave.TabIndex = 10;
                    btnUpdate.TabIndex = 5;
                }
                else
                {
                    btnSave.TabIndex = 5;
                    btnUpdate.TabIndex = 10;
                }

                txtName.Focus();
                e.Handled = true;
            }
        }

        private void cmbRole_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {
                if (btnUpdate.Visible)
                {
                    if (txtName.Text == "")
                    {
                        lblNameError.Visible = true;
                        txtName.Focus();
                    }
                    else if (txtUserName.Text == "")
                    {
                        lblUserNameError.Visible = true;
                        txtUserName.Focus();
                    }
                    else if (txtPassword.Text == "")
                    {
                        lblPasswordError.Visible = true;
                        txtPassword.Focus();
                    }
                    else
                    {
                        string retVal = updateValidation();
                        if (retVal == "true")
                        {
                            updateUser();
                            btnUpdate.Visible = false;
                        }
                        else if (retVal == "false")
                        {
                            warningAlert("User Already Exists.");
                        }
                        else
                        {
                            dangerAlert("Error! Please Try Again.");
                        }
                    }
                }
                else
                {
                    if (txtName.Text == "")
                    {
                        lblNameError.Visible = true;
                        txtName.Focus();
                    }
                    else if (txtUserName.Text == "")
                    {
                        lblUserNameError.Visible = true;
                        txtUserName.Focus();
                    }
                    else if (txtPassword.Text == "")
                    {
                        lblPasswordError.Visible = true;
                        txtPassword.Focus();
                    }
                    else
                    {
                        string retVal = addValidation();
                        if (retVal == "true")
                        {
                            addUser();
                        }
                        else if (retVal == "false")
                        {
                            warningAlert("User Already Exists.");
                        }
                        else
                        {
                            dangerAlert("Error! Please Try Again.");
                        }
                    }
                }
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
