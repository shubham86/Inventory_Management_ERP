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

namespace JericoSmartGrocery.stockMaster
{
    public partial class frmStockUpdate : Form
    {
        Decimal oldStock;
        int ID;

        public frmStockUpdate()
        {
            InitializeComponent();
        }

        private void frmStockUpdate_Load(object sender, EventArgs e)
        {
            LoadDatagrid();
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
                comm.CommandText = "Proc_Stock_FetchInGrigview";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvProductStock.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvProductStock.Rows)
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

                dgvProductStock.ClearSelection();
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
                comm.CommandText = "Proc_Stock_productSearch";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("PName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtName.Text.ToLower()).Trim()));                

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvProductStock.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvProductStock.Rows)
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

                dgvProductStock.ClearSelection();
            }
        }

        private void updateProductStock()
        {
            DAL.DAL_Product objDAL = new DAL.DAL_Product();
            BAL.BAL_Product objBAL = new BAL.BAL_Product();

            try
            {
                objDAL.productID = Convert.ToInt32(ID);
                objDAL.currentStock = Convert.ToDecimal(txtStock.Text);
                objDAL.remark = txtRemark.Text;

                if (oldStock > Convert.ToDecimal(txtStock.Text))
                {
                    objDAL.stockDr = oldStock - Convert.ToDecimal(txtStock.Text);
                    objDAL.stockCr = Convert.ToDecimal("0.00");
                }
                else
                {
                    objDAL.stockCr = Convert.ToDecimal(txtStock.Text) - oldStock;
                    objDAL.stockDr = Convert.ToDecimal("0.00");
                }


                if (objBAL.updateProductStock(objDAL))
                {
                    clearForm();
                    successAlert("Product stock update successfully.");                    
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
            clearForm();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
            productSearch();
        }
        
        private void dgvProductStock_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.txtName.TextChanged -= new EventHandler(txtName_TextChanged);
                txtName.Text = dgvProductStock.SelectedRows[0].Cells[2].Value.ToString();
                this.txtName.TextChanged += new EventHandler(txtName_TextChanged);
                txtStock.Text = dgvProductStock.SelectedRows[0].Cells[4].Value.ToString();
                oldStock = Convert.ToDecimal(dgvProductStock.SelectedRows[0].Cells[4].Value);
                ID = Convert.ToInt32(dgvProductStock.SelectedRows[0].Cells[1].Value);

                lblError.Visible = false;
                txtStock.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                lblError.Visible = true;
                txtName.Focus();
            }
            else
            {
                if (oldStock != Convert.ToDecimal(txtStock.Text))
                {
                    updateProductStock();
                    LoadDatagrid();
                }
            }            
        }

        private void clearForm()
        {
            txtName.Text = "";
            txtStock.Text = "";
            txtRemark.Text = "";
            lblError.Visible = false;

            LoadDatagrid();
            txtName.Focus();
        }

        private void txtStock_Leave(object sender, EventArgs e)
        {
            txtStock.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtStock.Text == "" ? "0.00" : txtStock.Text));
            txtStock.DeselectAll();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvProductStock.ClearSelection();
                if (dgvProductStock.RowCount > 0)
                {
                    dgvProductStock.Rows[0].Selected = true;
                    dgvProductStock.Select();
                }
            }
        }

        private void dgvProductStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtName.TextChanged -= new EventHandler(txtName_TextChanged);
                txtName.Text = dgvProductStock.SelectedRows[0].Cells[2].Value.ToString();
                this.txtName.TextChanged += new EventHandler(txtName_TextChanged);
                txtStock.Text = dgvProductStock.SelectedRows[0].Cells[4].Value.ToString();
                oldStock = Convert.ToDecimal(dgvProductStock.SelectedRows[0].Cells[4].Value);
                ID = Convert.ToInt32(dgvProductStock.SelectedRows[0].Cells[1].Value);

                lblError.Visible = false;
                txtStock.Focus();

                e.Handled = true;
            }
        }

        private void txtRemark_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {           
                if (oldStock != Convert.ToDecimal(txtStock.Text))
                {
                    updateProductStock();
                }
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtStock_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtStock_Click(object sender, EventArgs e)
        {
            txtStock.SelectAll();
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
