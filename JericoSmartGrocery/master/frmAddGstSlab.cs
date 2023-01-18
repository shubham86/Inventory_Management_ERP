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
    public partial class frmAddGstSlab : Form
    {
        public Boolean varFromProduct = false;

        public frmAddGstSlab()
        {
            InitializeComponent();
        }

        private void frmAddGstSlab_Load(object sender, EventArgs e)
        {
            txtgst.Select();
            LoadDatagrid();

            this.txtCgst.Enter += new EventHandler(txtCgst_Focus);
            this.txtSgst.Enter += new EventHandler(txtSgst_Focus);
            this.txtgst.Enter += new EventHandler(txtgst_Enter);
        }

        protected void txtCgst_Focus(Object sender, EventArgs e)
        {
            txtCgst.SelectAll();
        }

        protected void txtSgst_Focus(Object sender, EventArgs e)
        {
            txtSgst.SelectAll();
        }

        protected void txtgst_Enter(Object sender, EventArgs e)
        {
            txtgst.SelectAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string retVal = addValidation();
            if (retVal == "true")
            {
                addGstSlab();
                if (varFromProduct)
                {
                    varFromProduct = false;
                    Close();
                }
            }
            else if (retVal == "false")
            {
                warningAlert("Slab is Allready Exist.");
            }
            else
            {
                dangerAlert("Error! Please Try Again.");
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
                comm.CommandText = "Proc_GstSlab_FetchinGridview";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvGstSlab.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvGstSlab.Rows)
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

                dgvGstSlab.ClearSelection();
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
                comm.CommandText = "Proc_GstSlab_Addvalidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("c_gst", txtCgst.Text);
                comm.Parameters.AddWithValue("s_gst", txtSgst.Text);

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

                dgvGstSlab.ClearSelection();
            }
            return retVal;
        }

        private void addGstSlab()
        {
            DAL.DAL_GstSlab objDAL = new DAL.DAL_GstSlab();
            BAL.BAL_GstSlab objBAL = new BAL.BAL_GstSlab();

            int z = Convert.ToInt32(txtgst.Text == "00.00" ? "0" : txtgst.Text == "0.00" ? "0" : Convert.ToDouble(txtgst.Text == "" ? "0.00" : txtgst.Text).ToString());


            try
            {
                objDAL.slabName = txtgst.Text + "% = S-" + Convert.ToDecimal(txtSgst.Text == "" ? "0.00" : txtSgst.Text) + " + C-" + Convert.ToDecimal(txtCgst.Text == "" ? "0.00" : txtCgst.Text);
                objDAL.cgst = Convert.ToDecimal(txtCgst.Text == "" ? "0.00" : txtCgst.Text);
                objDAL.sgst = Convert.ToDecimal(txtSgst.Text == "" ? "0.00" : txtSgst.Text);
                objDAL.gst = Convert.ToDecimal(txtgst.Text == "" ? "0.00" : txtgst.Text);
                objDAL.isZero = z == 0 ? 1 : 0;

                if (objBAL.addGstSlab(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("Slab Add successfully.");
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
                comm.CommandText = "Proc_GstSlab_UpdateValidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("c_gst", txtCgst.Text);
                comm.Parameters.AddWithValue("s_gst", txtSgst.Text);
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

                dgvGstSlab.ClearSelection();
            }
            return retVal;
        }

        private void updateGstSlab()
        {
            DAL.DAL_GstSlab objDAL = new DAL.DAL_GstSlab();
            BAL.BAL_GstSlab objBAL = new BAL.BAL_GstSlab();

            try
            {
                objDAL.gstID = Convert.ToInt32(lblID.Text);
                objDAL.slabName = txtgst.Text + "% = S - " + Convert.ToDecimal(txtSgst.Text == "" ? "0.00" : txtSgst.Text) + " + C - " + Convert.ToDecimal(txtCgst.Text == "" ? "0.00" : txtCgst.Text);
                objDAL.cgst = Convert.ToDecimal(txtCgst.Text == "" ? "0.00" : txtCgst.Text);
                objDAL.sgst = Convert.ToDecimal(txtSgst.Text == "" ? "0.00" : txtSgst.Text);
                objDAL.gst = Convert.ToDecimal(txtgst.Text == "" ? "0.00" : txtgst.Text);

                if (objBAL.updateGstSlab(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("Slab Update successfully.");
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
            string retVal = updateValidation();
            if (retVal == "true")
            {
                updateGstSlab();
                btnUpdate.Visible = false;
                if (varFromProduct)
                {
                    Close();
                }
            }
            else if (retVal == "false")
            {
                warningAlert("Slab is Allready Exist.");
            }
            else
            {
                dangerAlert("Error! Please Try Again.");
            }
        }

        private void searchSlab()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_gstSlab_Search";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("totalGST", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtgst.Text.ToLower()).Trim()));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvGstSlab.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvGstSlab.Rows)
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
            txtCgst.Text = "0.00";
            txtSgst.Text = "0.00";
            txtgst.Text = "0.00";

            btnSave.TabIndex = 4;
            btnUpdate.TabIndex = 10;
            LoadDatagrid();
            txtgst.Focus();
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
        
        private void dgvGstSlab_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvGstSlab.Rows[e.RowIndex].Cells[6].Value.ToString() != "0.00")
            {
                lblID.Text = dgvGstSlab.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtCgst.Text = dgvGstSlab.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtSgst.Text = dgvGstSlab.Rows[e.RowIndex].Cells[5].Value.ToString();

                this.txtgst.TextChanged -= new EventHandler(txtgst_TextChanged);
                txtgst.Text = dgvGstSlab.Rows[e.RowIndex].Cells[6].Value.ToString();
                this.txtgst.TextChanged += new EventHandler(txtgst_TextChanged);

                btnUpdate.Visible = true;
                txtCgst.Select();
            }

            if (btnUpdate.Visible)
            {
                btnSave.TabIndex = 10;
                btnUpdate.TabIndex = 4;
            }
            else
            {
                btnSave.TabIndex = 4;
                btnUpdate.TabIndex = 10;
            }

            txtgst.Focus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            btnUpdate.Visible = false;
            dgvGstSlab.ClearSelection();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtgst_Leave(object sender, EventArgs e)
        {
            txtgst.DeselectAll();
            txtgst.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtgst.Text == "" ? "0.00" : txtgst.Text));           
        }

        private void txtCgst_Leave(object sender, EventArgs e)
        {
            txtCgst.DeselectAll();
            txtCgst.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtCgst.Text == "" ? "0.00" : txtCgst.Text));

            if (Convert.ToDecimal(txtgst.Text == "" ? "0.00" : txtgst.Text) > 0)
            {
                txtSgst.Text = (Convert.ToDecimal(txtgst.Text == "" ? "0.00" : txtgst.Text) - Convert.ToDecimal(txtCgst.Text == "" ? "0.00" : txtCgst.Text)).ToString("0.00");
                txtgst.Text = (Convert.ToDecimal(txtCgst.Text == "" ? "0.00" : txtCgst.Text) + Convert.ToDecimal(txtSgst.Text == "" ? "0.00" : txtSgst.Text)).ToString("0.00");
            }
            else
            {
                txtCgst.Text = "0.00";
                txtSgst.Text = "0.00";
            }
        }

        private void txtSgst_Leave(object sender, EventArgs e)
        {
            txtSgst.DeselectAll();
            txtSgst.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtSgst.Text == "" ? "0.00" : txtSgst.Text));
            //txtgst.Text = (Convert.ToDecimal(txtCgst.Text == "" ? "0.00" : txtCgst.Text) + Convert.ToDecimal(txtSgst.Text == "" ? "0.00" : txtSgst.Text)).ToString("0.00");
        }

        private void txtSgst_KeyPress(object sender, KeyPressEventArgs e)
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

            if (e.KeyChar == (char)13)
            {
                if (btnUpdate.Visible)
                {
                    string retVal = updateValidation();
                    if (retVal == "true")
                    {
                        updateGstSlab();
                        btnUpdate.Visible = false;
                        if (varFromProduct)
                        {
                            Close();
                        }
                    }
                    else if (retVal == "false")
                    {
                        warningAlert("Slab is Allready Exist.");
                    }
                    else
                    {
                        dangerAlert("Error! Please Try Again.");
                    }
                }
                else
                {
                    string retVal = addValidation();
                    if (retVal == "true")
                    {
                        addGstSlab();
                        if (varFromProduct)
                        {
                            varFromProduct = false;
                            Close();
                        }
                    }
                    else if (retVal == "false")
                    {
                        warningAlert("Slab is Allready Exist.");
                    }
                    else
                    {
                        dangerAlert("Error! Please Try Again.");
                    }
                }
            }
        }
        
        private void txtCgst_KeyPress(object sender, KeyPressEventArgs e)
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
        
        private void txtgst_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
            searchSlab();
        }

        private void txtgst_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtgst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvGstSlab.ClearSelection();
                if (dgvGstSlab.RowCount > 0)
                {
                    dgvGstSlab.Rows[0].Selected = true;
                    dgvGstSlab.Select();
                }
            }
        }

        private void dgvGstSlab_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvGstSlab.SelectedRows[0].Cells[6].Value.ToString() != "0.00")
                {
                    lblID.Text = dgvGstSlab.SelectedRows[0].Cells[2].Value.ToString();
                    txtCgst.Text = dgvGstSlab.SelectedRows[0].Cells[4].Value.ToString();
                    txtSgst.Text = dgvGstSlab.SelectedRows[0].Cells[5].Value.ToString();

                    this.txtgst.TextChanged -= new EventHandler(txtgst_TextChanged);
                    txtgst.Text = dgvGstSlab.SelectedRows[0].Cells[6].Value.ToString();
                    this.txtgst.TextChanged += new EventHandler(txtgst_TextChanged);

                    btnUpdate.Visible = true;
                    txtCgst.Select();
                }

                if (btnUpdate.Visible)
                {
                    btnSave.TabIndex = 10;
                    btnUpdate.TabIndex = 4;
                }
                else
                {
                    btnSave.TabIndex = 4;
                    btnUpdate.TabIndex = 10;
                }

                txtgst.Focus();

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
                txtCgst.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtCgst_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtSgst_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtgst_Click(object sender, EventArgs e)
        {
            txtgst.SelectAll();
        }

        private void txtCgst_Click(object sender, EventArgs e)
        {
            txtCgst.SelectAll();
        }

        private void txtSgst_Click(object sender, EventArgs e)
        {
            txtSgst.SelectAll();
        }
    }
}