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
    public partial class frmAddFinancialYear : Form
    {
        public frmAddFinancialYear()
        {
            InitializeComponent();
        }

        private void frmAddFinancialYear_Load(object sender, EventArgs e)
        {
            txtYearName.Select();
            LoadDatagrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtYearName.Text == "")
            {
                lblYearError.Visible = true;
                lblYearError.Focus();
            }
            else
            {
                string retVal = addValidation();
                if (retVal == "true")
                {
                    addYear();
                }
                else if (retVal == "false")
                {
                    warningAlert("Year is Allready Exist.");
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
                comm.CommandText = "Proc_Year_FetchinGridview";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvFinancialYear.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvFinancialYear.Rows)
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

                dgvFinancialYear.ClearSelection();
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
                comm.CommandText = "Proc_Year_Addvalidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Yname", txtYearName.Text);

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

                dgvFinancialYear.ClearSelection();
            }
            return retVal;
        }

        private void addYear()
        {
            DAL.DAL_FinancialYear objDAL = new DAL.DAL_FinancialYear();
            BAL.BAL_FinancialYear objBAL = new BAL.BAL_FinancialYear();

            try
            {
                objDAL.yearName = txtYearName.Text;
                objDAL.startDate = Convert.ToDateTime(dtpSartDate.Text);
                objDAL.endDate = Convert.ToDateTime(dtpEndDate.Text);

                if (objBAL.addYear(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("Year Add successfully.");
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
                comm.CommandText = "Proc_Year_UpdateValidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Id", lblID.Text);
                comm.Parameters.AddWithValue("Yname", txtYearName.Text);

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

                dgvFinancialYear.ClearSelection();
            }
            return retVal;
        }

        private void updateYear()
        {
            DAL.DAL_FinancialYear objDAL = new DAL.DAL_FinancialYear();
            BAL.BAL_FinancialYear objBAL = new BAL.BAL_FinancialYear();

            try
            {
                objDAL.yearID = Convert.ToInt32(lblID.Text);
                objDAL.yearName = txtYearName.Text;
                objDAL.startDate = Convert.ToDateTime(dtpSartDate.Text);
                objDAL.endDate = Convert.ToDateTime(dtpEndDate.Text);

                if (objBAL.updateYear(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("Year Update successfully.");
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
            if (txtYearName.Text == "")
            {
                lblYearError.Visible = true;
                txtYearName.Focus();
            }
            else
            {
                string retVal = updateValidation();
                if (retVal == "true")
                {
                    updateYear();
                    btnUpdate.Visible = false;
                }
                else if (retVal == "false")
                {
                    warningAlert("Year is Allready Exist.");
                }
                else
                {
                    dangerAlert("Error! Please Try Again.");
                }
            }
        }

        private void searchYear()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Year_Search";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("year", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtYearName.Text.ToLower()).Trim()));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvFinancialYear.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvFinancialYear.Rows)
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
            txtYearName.Text = "";
            dtpSartDate.Text = (DateTime.Now).ToString();
            dtpEndDate.Text = (DateTime.Now).ToString();
                        
            lblYearError.Visible = false;

            btnSave.TabIndex = 4;
            btnUpdate.TabIndex = 7;

            LoadDatagrid();
            txtYearName.Focus();
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

        private void dgvFinancialYear_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                this.txtYearName.TextChanged -= new EventHandler(txtYearName_TextChanged);
                lblID.Text = dgvFinancialYear.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtYearName.Text = dgvFinancialYear.Rows[e.RowIndex].Cells[3].Value.ToString();
                dtpSartDate.Text = dgvFinancialYear.Rows[e.RowIndex].Cells[4].Value.ToString();
                dtpEndDate.Text = dgvFinancialYear.Rows[e.RowIndex].Cells[5].Value.ToString();
                this.txtYearName.TextChanged += new EventHandler(txtYearName_TextChanged);

                btnUpdate.Visible = true;
            }

            if (btnUpdate.Visible)
            {
                btnSave.TabIndex = 7;
                btnUpdate.TabIndex = 4;
            }
            else
            {
                btnSave.TabIndex = 4;
                btnUpdate.TabIndex = 7;
            }

            txtYearName.Focus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            btnUpdate.Visible = false;
            dgvFinancialYear.ClearSelection();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtYearName_TextChanged(object sender, EventArgs e)
        {
            searchYear();
        }

        private void txtYearName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvFinancialYear.ClearSelection();
                if (dgvFinancialYear.RowCount > 0)
                {
                    dgvFinancialYear.Rows[0].Selected = true;
                    dgvFinancialYear.Select();
                }
            }
        }

        private void dgvFinancialYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtYearName.TextChanged -= new EventHandler(txtYearName_TextChanged);
                lblID.Text = dgvFinancialYear.SelectedRows[0].Cells[2].Value.ToString();
                txtYearName.Text = dgvFinancialYear.SelectedRows[0].Cells[3].Value.ToString();
                dtpSartDate.Text = dgvFinancialYear.SelectedRows[0].Cells[4].Value.ToString();
                dtpEndDate.Text = dgvFinancialYear.SelectedRows[0].Cells[5].Value.ToString();
                this.txtYearName.TextChanged += new EventHandler(txtYearName_TextChanged);

                btnUpdate.Visible = true;
            }

            if (btnUpdate.Visible)
            {
                btnSave.TabIndex = 7;
                btnUpdate.TabIndex = 4;
            }
            else
            {
                btnSave.TabIndex = 4;
                btnUpdate.TabIndex = 7;
            }

            txtYearName.Focus();

            e.Handled = true;
        }

        private void txtYearName_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblYearError.Visible = false;            
        }

        private void dtpEndDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (btnUpdate.Visible)
                {
                    if (txtYearName.Text == "")
                    {
                        lblYearError.Visible = true;
                        txtYearName.Focus();
                    }
                    else
                    {
                        string retVal = updateValidation();
                        if (retVal == "true")
                        {
                            updateYear();
                            btnUpdate.Visible = false;
                        }
                        else if (retVal == "false")
                        {
                            warningAlert("Year is Allready Exist.");
                        }
                        else
                        {
                            dangerAlert("Error! Please Try Again.");
                        }
                    }
                }
                else
                {
                    if (txtYearName.Text == "")
                    {
                        lblYearError.Visible = true;
                        lblYearError.Focus();
                    }
                    else
                    {
                        string retVal = addValidation();
                        if (retVal == "true")
                        {
                            addYear();
                        }
                        else if (retVal == "false")
                        {
                            warningAlert("Year is Allready Exist.");
                        }
                        else
                        {
                            dangerAlert("Error! Please Try Again.");
                        }
                    }
                }
            }
        }
    }
}
