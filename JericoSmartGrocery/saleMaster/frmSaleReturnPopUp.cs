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

    public partial class frmSaleReturnPopUp : Form
    {

        public int billNumber;

        public frmSaleReturnPopUp()
        {
            InitializeComponent();
        }


        private void loadComponents()
        {
            {
                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
                MySqlCommand comm = new MySqlCommand();
                MySqlDataAdapter DA = new MySqlDataAdapter();
                DataTable DT = new DataTable();

                try
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "Proc_SaleBill_fetchinForm";

                    comm.Connection = conn;

                    comm.Parameters.AddWithValue("billNumber", billNumber);

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    DA = new MySqlDataAdapter(comm);
                    DA.Fill(DT);

                    if (DT.Rows.Count > 0)
                    {
                        lblCustomerName.Text = DT.Rows[0]["customerName"].ToString();
                        lblDate.Text = DT.Rows[0]["saleDate"].ToString();
                        lblCustomerID.Text = DT.Rows[0]["customerID"].ToString();

                        dgvSaleBillDetails.DataSource = DT;

                        //int i = 1;
                        //foreach (DataGridViewRow row in dgvSaleBillDetails.Rows)
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

                    dgvSaleBillDetails.ClearSelection();
                }

            }

        }

        private void frmSaleReturnPopUp_Load(object sender, EventArgs e)
        {
            txtBillNumber.Text = billNumber.ToString();
            loadComponents();
            txtBillNumber.DeselectAll();
            this.txtBillNumber.Enter += new EventHandler(txtBillNumber_Focus);
        }

        private void txtBillNumber_Focus(object sender, EventArgs e)
        {
            txtBillNumber.DeselectAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBillNumber_Leave(object sender, EventArgs e)
        {
            txtBillNumber.DeselectAll();
        }

        private void dgvSaleBillDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 13)
            {
                frmSaleDetails frmSaleDetails = new frmSaleDetails()
                {
                    productID = (dgvSaleBillDetails.Rows[e.RowIndex].Cells[2].Value.ToString()),
                    productName = (dgvSaleBillDetails.Rows[e.RowIndex].Cells[3].Value.ToString()),
                    saleQty = (dgvSaleBillDetails.Rows[e.RowIndex].Cells[9].Value.ToString()),
                    saleRate = (dgvSaleBillDetails.Rows[e.RowIndex].Cells[8].Value.ToString()),
                    saleBrand = (dgvSaleBillDetails.Rows[e.RowIndex].Cells[7].Value.ToString()),
                    customerID = lblCustomerID.Text,
                    saleBillNumber = txtBillNumber.Text,
                    purchaseRate = (dgvSaleBillDetails.Rows[e.RowIndex].Cells[14].Value.ToString())
                };

                Opacity = .50;
                frmSaleDetails.ShowDialog();
                Opacity = 1;
            }            
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            else if (keyData == Keys.F1)
            {
                txtBillNumber.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvSaleBillDetails.ClearSelection();
                if (dgvSaleBillDetails.RowCount > 0)
                {
                    dgvSaleBillDetails.Rows[0].Selected = true;
                    dgvSaleBillDetails.Select();
                }
            }
        }

        private void dgvSaleBillDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                frmSaleDetails frmSaleDetails = new frmSaleDetails()
                {
                    productID = (dgvSaleBillDetails.SelectedRows[0].Cells[2].Value.ToString()),
                    productName = (dgvSaleBillDetails.SelectedRows[0].Cells[3].Value.ToString()),
                    saleQty = (dgvSaleBillDetails.SelectedRows[0].Cells[9].Value.ToString()),
                    saleRate = (dgvSaleBillDetails.SelectedRows[0].Cells[8].Value.ToString()),
                    saleBrand = (dgvSaleBillDetails.SelectedRows[0].Cells[7].Value.ToString()),
                    customerID = lblCustomerID.Text,
                    saleBillNumber = txtBillNumber.Text,
                    purchaseRate = (dgvSaleBillDetails.SelectedRows[0].Cells[14].Value.ToString())
                };

                Opacity = .50;
                frmSaleDetails.ShowDialog();
                Opacity = 1;

                e.Handled = true;
            }
        }
    }
}
