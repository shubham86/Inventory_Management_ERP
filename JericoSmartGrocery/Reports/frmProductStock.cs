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

namespace JericoSmartGrocery.Reports
{
    public partial class frmProductStock : Form
    {
        //Decimal oldStock;
        //int ID;

        public frmProductStock()
        {
            InitializeComponent();
        }

        private void frmProductStock_Load(object sender, EventArgs e)
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
            txtName.Text = "";
            LoadDatagrid();
            txtName.Focus();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            productSearch();
        }

        private void dgvProductStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if click is on new row or header row
            if (e.RowIndex == dgvProductStock.NewRowIndex || e.RowIndex < 0)
            {
                return;
            }

            //Check if click is on specific column 
            if (e.ColumnIndex == 6)
            {
                frmStockLedger stockLedger = new frmStockLedger();
                stockLedger.productName = dgvProductStock.SelectedRows[0].Cells[2].Value.ToString();
                stockLedger.currentStock = dgvProductStock.SelectedRows[0].Cells[4].Value.ToString();
                stockLedger.proID = dgvProductStock.SelectedRows[0].Cells[1].Value.ToString();
                Opacity = .50;
                stockLedger.ShowDialog();
                Opacity = 1;
            }
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
                frmStockLedger stockLedger = new frmStockLedger();
                stockLedger.productName = dgvProductStock.SelectedRows[0].Cells[2].Value.ToString();
                stockLedger.currentStock = dgvProductStock.SelectedRows[0].Cells[4].Value.ToString();
                stockLedger.proID = dgvProductStock.SelectedRows[0].Cells[1].Value.ToString();

                Opacity = .50;
                stockLedger.ShowDialog();
                Opacity = 1;
                e.Handled = true;
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
