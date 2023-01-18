using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JericoSmartGrocery.saleMaster
{
    public partial class frmSaleReturn : Form
    {
        public frmSaleReturn()
        {
            InitializeComponent();
        }

        private void clearForm()
        {
            dgvFetchBill.Rows.Clear();
            txtSearch.Text = "";
            txtSearch.Select();
            this.dtpFromDate.ValueChanged -= new EventHandler(dtpFromDate_ValueChanged);
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            this.dtpFromDate.ValueChanged += new EventHandler(dtpFromDate_ValueChanged);
            this.dtpToDate.ValueChanged -= new EventHandler(dtpToDate_ValueChanged);
            dtpToDate.Value = DateTime.Now;
            this.dtpToDate.ValueChanged += new EventHandler(dtpToDate_ValueChanged);
            LoadDatagrid();
        }

        private void LoadDatagrid()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            string nameKeyword;
            string billkeyword;

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleBill_Fetchin";

                comm.Connection = conn;


                int parsedvalue;
                if (int.TryParse(txtSearch.Text, out parsedvalue))
                {
                    billkeyword = txtSearch.Text;
                    nameKeyword = "";
                }
                else
                {
                    billkeyword = "";
                    nameKeyword = txtSearch.Text;
                }

                comm.Parameters.AddWithValue("billkeyword", billkeyword);
                comm.Parameters.AddWithValue("nameKeyword", nameKeyword);

                comm.Parameters.AddWithValue("fromDate", string.Format("{0:yyyy-MM-dd}", dtpFromDate.Value));
                comm.Parameters.AddWithValue("toDate", string.Format("{0:yyyy-MM-dd}", dtpToDate.Value));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvFetchBill.DataSource = DT;

                    //int i = 1;
                    //foreach (DataGridViewRow row in dgvFetchBill.Rows)
                    //{
                    //    row.Cells[0].Value = i;
                    //    i++;
                    //}
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
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

                dgvFetchBill.ClearSelection();
            }
        }

        private void frmSaleReturn_Load(object sender, EventArgs e)
        {
            clearForm();
            LoadDatagrid();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDatagrid();
        }

        private void dgvFetchBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                frmSaleReturnPopUp frmSaleReturnPopUp = new frmSaleReturnPopUp();
                frmSaleReturnPopUp.billNumber = Convert.ToInt32(dgvFetchBill.Rows[e.RowIndex].Cells[3].Value);
                Opacity = .50;
                frmSaleReturnPopUp.ShowDialog();
                Opacity = 1;
            }
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            dtpFromDate.MaxDate = DateTime.Now;
            LoadDatagrid();
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value > dtpToDate.Value)
            {
                dtpToDate.Value = dtpFromDate.Value;
            }
            dtpToDate.MaxDate = DateTime.Now;
            LoadDatagrid();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvFetchBill.ClearSelection();
                if (dgvFetchBill.RowCount > 0)
                {
                    dgvFetchBill.Rows[0].Selected = true;
                    dgvFetchBill.Select();
                }
            }
        }

        private void dgvFetchBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                frmSaleReturnPopUp frmSaleReturnPopUp = new frmSaleReturnPopUp();
                frmSaleReturnPopUp.billNumber = Convert.ToInt32(dgvFetchBill.SelectedRows[0].Cells[3].Value);
                Opacity = .50;
                frmSaleReturnPopUp.ShowDialog();
                Opacity = 1;

                e.Handled = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void dtpFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                txtSearch.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
