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

namespace JericoSmartGrocery.purchaseMaster
{
    public partial class frmPurchaseReturnPopUp : Form
    {
        public int billNumber;
        public int purReturnID;

        public frmPurchaseReturnPopUp()
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
                    comm.CommandText = "Proc_PurchaseBill_fetchinForm";

                    comm.Connection = conn;

                    comm.Parameters.AddWithValue("billNumber", billNumber);
                    comm.Parameters.AddWithValue("purReturnID", purReturnID);                    

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    DA = new MySqlDataAdapter(comm);
                    DA.Fill(DT);

                    if (DT.Rows.Count > 0)
                    {
                        if(DT.Rows[0]["supplierName"].ToString()=="")
                        {
                            lblSupplierName.Visible = false;
                        }
                        else
                        {
                            lblSupplierName.Text = DT.Rows[0]["supplierName"].ToString();

                        }
                        lblDate.Text = DT.Rows[0]["purchaseDate"].ToString();
                        txtInvoiceNumber.Text = DT.Rows[0]["invoiceNumber"].ToString();
                        lblsuppID.Text = DT.Rows[0]["supplierID"].ToString();

                        dgvPurchaseBillDetails.DataSource = DT;

                        //int i = 1;
                        //foreach (DataGridViewRow row in dgvPurchaseBillDetails.Rows)
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

                    dgvPurchaseBillDetails.ClearSelection();
                }
                            
            }

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPurchaseReturnPopUp_Load(object sender, EventArgs e)
        {
            txtBillNumber.Focus();
            txtBillNumber.Text = billNumber.ToString();
            loadComponents();
            txtBillNumber.DeselectAll();

            this.txtBillNumber.Enter += new EventHandler(txtBillNumber_Focus);
        }

        protected void txtBillNumber_Focus(Object sender, EventArgs e)
        {
            txtBillNumber.DeselectAll();
        }

        private void dgvPurchaseBillDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {        
            if(e.ColumnIndex==14)
            {
                frmPurchaseDetails frmPurchaseDetails = new frmPurchaseDetails();
               
                frmPurchaseDetails.productID = (dgvPurchaseBillDetails.Rows[e.RowIndex].Cells[2].Value.ToString());
                frmPurchaseDetails.productName = (dgvPurchaseBillDetails.Rows[e.RowIndex].Cells[3].Value.ToString());
                frmPurchaseDetails.purchaseQty = (dgvPurchaseBillDetails.Rows[e.RowIndex].Cells[10].Value.ToString());
                frmPurchaseDetails.purchaseRate = (dgvPurchaseBillDetails.Rows[e.RowIndex].Cells[8].Value.ToString());
                frmPurchaseDetails.purchaseBrand = (dgvPurchaseBillDetails.Rows[e.RowIndex].Cells[7].Value.ToString());
                frmPurchaseDetails.supplierID = lblsuppID.Text;
                frmPurchaseDetails.purchaseBillNumber = txtBillNumber.Text;

                Opacity = .50;
                frmPurchaseDetails.ShowDialog();
                Opacity = 1;
            }          
        }

        private void txtBillNumber_Leave(object sender, EventArgs e)
        {
            txtBillNumber.DeselectAll();
        }

        private void dgvPurchaseBillDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                frmPurchaseDetails frmPurchaseDetails = new frmPurchaseDetails();

                frmPurchaseDetails.productID = (dgvPurchaseBillDetails.SelectedRows[0].Cells[3].Value.ToString());
                frmPurchaseDetails.productName = (dgvPurchaseBillDetails.SelectedRows[0].Cells[4].Value.ToString());
                frmPurchaseDetails.purchaseQty = (dgvPurchaseBillDetails.SelectedRows[0].Cells[11].Value.ToString());
                frmPurchaseDetails.purchaseRate = (dgvPurchaseBillDetails.SelectedRows[0].Cells[9].Value.ToString());
                frmPurchaseDetails.purchaseBrand = (dgvPurchaseBillDetails.SelectedRows[0].Cells[8].Value.ToString());
                frmPurchaseDetails.supplierID = lblsuppID.Text;
                frmPurchaseDetails.purchaseBillNumber = txtBillNumber.Text;

                Opacity = .50;
                frmPurchaseDetails.ShowDialog();
                Opacity = 1;

                e.Handled = true;
            }
        }

        private void txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvPurchaseBillDetails.ClearSelection();
                if (dgvPurchaseBillDetails.RowCount > 0)
                {
                    dgvPurchaseBillDetails.Rows[0].Selected = true;
                    dgvPurchaseBillDetails.Select();
                }
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
    }
}
