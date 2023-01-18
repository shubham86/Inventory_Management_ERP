using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Globalization;

namespace JericoSmartGrocery.master
{
    public partial class frmTempPurchasePrice : Form
    {
        public frmTempPurchasePrice()
        {
            InitializeComponent();
        }

        private void searchProduct()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Product_Search_PurPrice";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("product", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtProductName.Text.ToLower()).Trim()));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvProductDetails.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvProductDetails.Rows)
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

        public void clearForm()
        {
            txtProductName.Text = "";
            txtPurchaseRate.Text = "0.00";
            txtSaleRate.Text = "0.00";
            txtProductName.Focus();
            txtProductName.Enabled = true;
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

        private void frmTempPurchasePrice_Load(object sender, EventArgs e)
        {

            txtProductName.Select();
            btnUpdate.Visible = true;
            LoadDatagrid();
        }

        public void LoadDatagrid()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_fetchProductPurchaseRateTemp";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count >= 0)
                {
                    dgvProductDetails.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvProductDetails.Rows)
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

                dgvProductDetails.ClearSelection();
            }
        }

        private void txtMarathiName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            searchProduct();
        }

        private void dgvProductDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                lblID.Text = dgvProductDetails.Rows[e.RowIndex].Cells[1].Value.ToString();

                this.txtProductName.TextChanged -= new EventHandler(txtProductName_TextChanged);
                txtProductName.Text = dgvProductDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.txtProductName.TextChanged += new EventHandler(txtProductName_TextChanged);

              
                txtSaleRate.Text = dgvProductDetails.Rows[e.RowIndex].Cells[8].Value.ToString().Replace("/", " / ");
                txtPurchaseRate.Text = dgvProductDetails.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtSaleRate.Enabled = false;
              
                btnUpdate.Visible = true;
                txtPurchaseRate.Enabled = true;
                txtProductName.Select();
            }
            if (btnUpdate.Visible)
            {
                btnUpdate.TabIndex = 10;
            }
            else
            {
                btnUpdate.TabIndex = 13;
            }
            txtPurchaseRate.Focus();
        }


        private void dgvProductDetails_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                lblID.Text = dgvProductDetails.SelectedRows[0].Cells[1].Value.ToString();

                this.txtProductName.TextChanged -= new EventHandler(txtProductName_TextChanged);
                txtProductName.Text = dgvProductDetails.SelectedRows[0].Cells[2].Value.ToString();
                this.txtProductName.TextChanged += new EventHandler(txtProductName_TextChanged);


                txtSaleRate.Text = dgvProductDetails.SelectedRows[0].Cells[8].Value.ToString().Replace("/", " / ");
                txtPurchaseRate.Text = dgvProductDetails.SelectedRows[0].Cells[6].Value.ToString();
                txtSaleRate.Enabled = false;

                btnUpdate.Visible = true;
                txtPurchaseRate.Enabled = true;
                txtProductName.Select();

                txtPurchaseRate.Focus();
            }
            if (btnUpdate.Visible)
            {
                btnUpdate.TabIndex = 10;
            }
            else
            {
                btnUpdate.TabIndex = 13;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            update();
        }

        private void update()
        {
            if (txtProductName.Text == "")
            {
                lblNameError.Visible = true;
                txtProductName.Focus();
            }
            else if (txtPurchaseRate.Text == "" || txtPurchaseRate.Text == "0.00")
            {
                lblPurchasePrice.Visible = true;
                txtPurchaseRate.Focus();
            }
            else
            {
                DAL.DAL_ProductPurchasePrice objDAL = new DAL.DAL_ProductPurchasePrice();
                BAL.BAL_ProductPurchasePrice objBAL = new BAL.BAL_ProductPurchasePrice();

                try
                {
                    objDAL.productID = Convert.ToInt32(lblID.Text);
                    objDAL.purchaseRate = txtPurchaseRate.Text == "" ? Convert.ToDecimal("0.00") : Convert.ToDecimal(txtPurchaseRate.Text);

                    if (objBAL.insertPurchasePrice(objDAL))
                    {
                        successAlert("Product Price Updated Successfully.");
                    }
                    else
                    {
                        dangerAlert("Error! Please Try Again.");
                    }
                }
                catch (Exception ex)
                {
                    string x = ex.ToString();

                    MessageBox.Show(x);
                    dangerAlert("Error! Please Try Again.");
                }
            }
            clearForm();
            LoadDatagrid();
            txtProductName.Focus();
        }

        private void txtPurchaseRate_Leave(object sender, EventArgs e)
        {
            txtPurchaseRate.DeselectAll();
            txtPurchaseRate.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0.00" : txtPurchaseRate.Text));
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void txtPurchaseRate_KeyPress(object sender, KeyPressEventArgs e)
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
                update();
            }
        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvProductDetails.ClearSelection();
                if (dgvProductDetails.RowCount > 0)
                {
                    dgvProductDetails.Rows[0].Selected = true;
                    dgvProductDetails.Select();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                txtProductName.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
