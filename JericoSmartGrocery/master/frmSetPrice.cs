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
    public partial class frmSetPrice : Form
    {
        public frmSetPrice()
        {
            InitializeComponent();
        }

        private void frmSetPrice_Load(object sender, EventArgs e)
        {
            txtSearch.Select();
            LoadDatagrid();

            txtSaleRate.Enter += new EventHandler(txtSaleRate_Enter);
        }

        protected void txtSaleRate_Enter(Object sender, EventArgs e)
        {
            txtSaleRate.SelectAll();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchProduct();
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
                comm.CommandText = "Proc_Product_Price_FetchinGridview";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvProductDetails.DataSource = DT;
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                dangerAlert("Data Loading Error!");
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

        private void searchProduct()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Product_Price_Search";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("PName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtSearch.Text.ToLower()).Trim()));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvProductDetails.DataSource = DT;
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                dangerAlert("Product Search Error!");
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

        private void updateSaleRate()
        {
            DAL.DAL_hashPurchase objDAL = new DAL.DAL_hashPurchase();
            BAL.BAL_SalePrice objBAL = new BAL.BAL_SalePrice();

            try
            {
                objDAL.productID = Convert.ToInt32(lblID.Text);
                objDAL.saleRate = Convert.ToDecimal(txtSaleRate.Text);

                if (objBAL.updateSalePrice(objDAL))
                {
                    ClearForm();
                    LoadDatagrid();
                    successAlert("Price Update successfully.");
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
                //MessageBox.Show(ex.ToString());
            }
        }

        private void ClearForm()
        {
            txtSearch.Text = "";
            txtSaleRate.Text = "00.00";

            txtSearch.Focus();
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

        private void dgvProductDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                lblID.Text = dgvProductDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
                lblPurchaseRate.Text = dgvProductDetails.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtSaleRate.Text = dgvProductDetails.Rows[e.RowIndex].Cells[4].Value.ToString();

                this.txtSearch.TextChanged -= new EventHandler(txtSearch_TextChanged);
                txtSearch.Text = dgvProductDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);

                btnUpdate.Visible = true;
                txtSearch.Select();
            }
        }

        private void txtSaleRate_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(lblPurchaseRate.Text) > Convert.ToDecimal(txtSaleRate.Text))
            {
                var confirmResult = MessageBox.Show("Are you sure to update sale rate is less than purchase rate ?", "Confirm Save !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    updateSaleRate();
                }
                else
                {
                    return;
                }
            }
            else
            {
                updateSaleRate();
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            txtSearch.DeselectAll();
        }

        private void txtSaleRate_Leave(object sender, EventArgs e)
        {
            txtSaleRate.DeselectAll();
            txtSaleRate.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtSaleRate.Text));
        }
    }
}
