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
using MySql.Data.MySqlClient;

namespace JericoSmartGrocery.purchaseMaster
{
    public partial class frmPurchase : Form
    {
        public decimal varBillTotal = 0;

        public frmPurchase()
        {
            InitializeComponent();
            //this.Size = new Size(1582, 862);
        }

        private void frmPurchase_Load(object sender, EventArgs e)
        {
            dgvPurchaseList.Visible = true;
            dgvSearchProduct.Visible = false;
            dtpDate.Value = DateTime.Now;
            btnSupplierAdd.FlatAppearance.BorderColor = Color.FromArgb(177, 177, 177);
            btnAddProduct.FlatAppearance.BorderColor = Color.FromArgb(177, 177, 177);
            txtBillNo.Text = billNo().ToString();

            dgvPurchaseList.Visible = true;
            dgvSearchProduct.Visible = false;

            fetchHashPurchase();
            cmbPayMode.SelectedIndex = 0;
            fillSupplierCmb();

            truncateHashTable();

            txtPaidAmount.Enter += new EventHandler(txtPaidAmount_Focus);
            txtSearch.Enter += new EventHandler(txtSearch_Focus);
            txtInvoice.Enter += new EventHandler(txtInvoice_Focus);
            txtDiscount.Enter += new EventHandler(txtDiscount_Focus);
            txtOtherCharges.Enter += new EventHandler(txtOtherCharges_Focus);
        }

        protected void txtPaidAmount_Focus(Object sender, EventArgs e)
        {
            txtPaidAmount.SelectAll();
        }

        protected void txtSearch_Focus(Object sender, EventArgs e)
        {
            txtSearch.SelectAll();
        }

        protected void txtDiscount_Focus(Object sender, EventArgs e)
        {
            txtDiscount.SelectAll();
        }

        protected void txtInvoice_Focus(Object sender, EventArgs e)
        {
            txtInvoice.SelectAll();
        }

        protected void txtOtherCharges_Focus(Object sender, EventArgs e)
        {
            txtOtherCharges.SelectAll();
        }

        public void fillSupplierCmb()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            DT.Columns.Add("supplierName", typeof(string));
            DT.Columns.Add("supplierID", typeof(int));
            DataRow row = DT.NewRow();
            row["supplierID"] = "0";
            row["supplierName"] = "-- Select Supplier --";
            DT.Rows.InsertAt(row, 0);

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Supplier_FetchinCmb";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    this.cmbSupplier.SelectedIndexChanged -= new EventHandler(cmbSupplier_SelectedIndexChanged);

                    cmbSupplier.DataSource = DT;
                    cmbSupplier.ValueMember = "supplierID";
                    cmbSupplier.DisplayMember = "supplierName";

                    this.cmbSupplier.SelectedIndexChanged += new EventHandler(cmbSupplier_SelectedIndexChanged);
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                lblAlert.Location = new Point(100, 8);
                dangerAlert("Brands Loading Error!");
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

        private void searchProduct()
        {
            dgvSearchProduct.Visible = true;
            dgvPurchaseList.Visible = false;

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Product_PurEntry_Search";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("product", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtSearch.Text.ToLower()).Trim()));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvSearchProduct.DataSource = DT;

                    //int i = 1;
                    //foreach (DataGridViewRow row in dgvSearchProduct.Rows)
                    //{
                    //    row.Cells[0].Value = i;
                    //    i++;
                    //}
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                lblAlert.Location = new Point(100, 8);
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

                dgvSearchProduct.ClearSelection();
            }
        }

        private int billNo()
        {
            int billNo = 0;

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Purchase_getMaxBillNo";

                comm.Connection = conn;
                
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    billNo = Convert.ToInt32(DT.Rows[0]["purchaseBillNumber"]) + 1;
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                lblAlert.Location = new Point(80, 8);
                dangerAlert("Bill No. Loading Error!");
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

            return billNo;
        }

        private void getSupplierDetails(int ID)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Supplier_GetDetailbyID";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Id", ID);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    txtSuppBalance.Text = DT.Rows[0]["balanceAmount"].ToString();
                    lblPrevAdv.Text = DT.Rows[0]["supplierAdvanceAmount"].ToString();
                    txtAdvanceAmount.Text = DT.Rows[0]["supplierAdvanceAmount"].ToString();
                }
            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                lblAlert.Location = new Point(100, 8);
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

                dgvSearchProduct.ClearSelection();
            }
        }

        public void fetchHashPurchase()
        {
            dgvSearchProduct.Visible = false;
            dgvPurchaseList.Visible = true;

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_PurchaseHash_fetchinDgv";

                comm.Connection = conn;
                
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvPurchaseList.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvPurchaseList.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;
                    }

                    Decimal total = 0;
                    Decimal gsttotal = 0;
                    foreach (DataGridViewRow row in dgvPurchaseList.Rows)
                    {
                        total += Convert.ToDecimal(row.Cells[13].Value);
                        gsttotal += Convert.ToDecimal(row.Cells[7].Value);
                        varBillTotal = total + Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text) - Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text);
                        txtBillTotal.Text = varBillTotal.ToString();
                        txtTotalGstAmount.Text = gsttotal.ToString();
                        txtGrandTotal.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text) + Convert.ToDecimal(txtSuppBalance.Text), 0, MidpointRounding.AwayFromZero));
                        if (txtPaidAmount.Text != "")
                        {
                            calculatAdvanceAndBalance();
                        }
                    }
                }
                else
                {
                    dgvPurchaseList.DataSource = DT;
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                lblAlert.Location = new Point(100, 8);
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

                dgvPurchaseList.ClearSelection();
                dgvPurchaseList.Visible = true;
                dgvSearchProduct.Visible = false;

                txtSearch.Focus();
                txtSearch.Text = "";
            }
        }

        private void deleteFromHashTable(int ID)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_PurchaseHash_deleteOneRecord";

                comm.Connection = conn;
                
                comm.Parameters.AddWithValue("Id", ID);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                comm.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                lblAlert.Location = new Point(100, 8);
                dangerAlert("Record Removing Error!");
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

        private void truncateHashTable()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_PurchaseHash_truncatetable";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                comm.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                lblAlert.Location = new Point(80, 8);
                dangerAlert("Bill List Removing Error!");
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
                fetchHashPurchase();
            }
        }

        private Boolean addPurchaseProduct()
        {
            Boolean retVal = false;
            DAL.DAL_hashPurchase objDAL = new DAL.DAL_hashPurchase();
            BAL.BAL_hashPurchase objBAL = new BAL.BAL_hashPurchase();

            try
            {
                foreach (DataGridViewRow row in dgvPurchaseList.Rows)
                {
                    objDAL.billNo = Convert.ToInt32(txtBillNo.Text);
                    objDAL.purchaseDate = dtpDate.Value;
                    objDAL.productID = Convert.ToInt32(row.Cells[1].Value);
                    objDAL.unitID = Convert.ToInt32(row.Cells[8].Value);
                    objDAL.brandID = Convert.ToInt32(row.Cells[3].Value);
                    objDAL.gstID = Convert.ToInt32(row.Cells[5].Value);
                    objDAL.purchaseRate = Convert.ToDecimal(row.Cells[12].Value);
                    objDAL.qty = Convert.ToDecimal(row.Cells[9].Value);
                    objDAL.totalprice = Convert.ToDecimal(row.Cells[13].Value);
                    objDAL.rateLess5 = Convert.ToDecimal(row.Cells[11].Value);
                    objDAL.gstamount = Convert.ToDecimal(row.Cells[7].Value);
                    objDAL.currentStock = Convert.ToDecimal(row.Cells[15].Value);
                    objDAL.rate5to10 = Convert.ToDecimal(row.Cells[16].Value);
                    objDAL.rateGreater10 = Convert.ToDecimal(row.Cells[17].Value);
                    objDAL.supplierID = Convert.ToInt32(cmbSupplier.SelectedValue.ToString() == "" ? "0" : cmbSupplier.SelectedValue);

                    if (objBAL.addPurchaseProduct(objDAL))
                    {
                        retVal = true;
                    }
                    else
                    {
                        retVal = false;
                    }
                }
               
            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                lblAlert.Location = new Point(100, 8);
                dangerAlert("Error! Please Try Again.");
                MessageBox.Show(x);
            }

            return retVal;
        }

        private Boolean addPurchasePayment()
        {
            Boolean retVal = false;
            DAL.DAL_hashPurchase objDAL = new DAL.DAL_hashPurchase();
            BAL.BAL_hashPurchase objBAL = new BAL.BAL_hashPurchase();

            try
            {
                objDAL.supplierID = Convert.ToInt32(cmbSupplier.SelectedValue.ToString() == "" ? "0" : cmbSupplier.SelectedValue.ToString());
                objDAL.billNo = Convert.ToInt32(txtBillNo.Text);
                objDAL.paymentDate = Convert.ToDateTime(dtpDate.Value);
                objDAL.payMode = cmbPayMode.Text;
                objDAL.refNumber =refNumber.refrenceNo;
                objDAL.billAmount = Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtDiscount.Text) - Convert.ToDecimal(txtOtherCharges.Text);
                objDAL.OtherCharges = Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text);
                objDAL.prevBalance = Convert.ToDecimal(txtSuppBalance.Text);
                objDAL.balanceAmount = Convert.ToDecimal(txtBalanceAmount.Text);
                objDAL.supplierAdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                objDAL.prevAdvanceAmount = Convert.ToDecimal(lblPrevAdv.Text);
                objDAL.nowPaidAmount = Convert.ToDecimal(txtPaidAmount.Text == "" ? "0.00" : txtPaidAmount.Text);
                objDAL.grandTotalAmount = Convert.ToDecimal(txtGrandTotal.Text);
                objDAL.totalGstAmount = Convert.ToDecimal(txtTotalGstAmount.Text);
                objDAL.isSuccesful = cmbPayMode.Text == "Cheque" ? false : true;
                objDAL.invoiceNo = txtInvoice.Text;
                objDAL.discount = Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text);

                if (objBAL.addPurchasePayment(objDAL))
                {
                    retVal = true;
                }
                else
                {
                    retVal = false; ;
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                lblAlert.Location = new Point(100, 8);
                dangerAlert("Error! Please Try Again.");
                MessageBox.Show(x);
            }
            return retVal;
        }

        private void ClearForm()
        {
            lblPrevAdv.Text = "0.00";
            lblPrevAdv.Visible = false;
            txtSearch.Text = "";
            txtAdvanceAmount.Text = "";
            txtSuppBalance.Text = "0.00";
            txtInvoice.Text = "";
            txtBillNo.Text = "";
            txtTotalGstAmount.Text = "0.00";
            txtOtherCharges.Text = "0.00";
            cmbPayMode.SelectedIndex = 0;
            txtPaidAmount.Text = "0.00";
            txtBillTotal.Text = "0.00";
            txtGrandTotal.Text = "0.00";
            txtBalanceAmount.Text = "0.00";
            txtDiscount.Text = "0.00";
            cmbSupplier.SelectedIndex = 0;
            cmbPayMode.SelectedIndex = 0;
            txtBillNo.Text = billNo().ToString();

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
            if (dgvPurchaseList.Rows.Count > 0)
            {
                var confirmResult = MessageBox.Show("Are you sure to exit without saving the bill ?", "Confirm Exit!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    truncateHashTable();
                    Close();
                }
                else
                {
                    return;
                }
            }
            else
            {
                Close();
            }
        }
        
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                searchProduct();
            }
            else
            {
                dgvSearchProduct.ClearSelection();
                
                dgvPurchaseList.Visible = true;
                dgvSearchProduct.Visible = false;
            }            
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvSearchProduct.ClearSelection();
                if (dgvSearchProduct.RowCount > 0)
                {
                    dgvSearchProduct.Rows[0].Selected = true;
                    dgvSearchProduct.Select();
                }                
            }
        }

        private void dgvSearchProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //for (int i = 1; i <= 17; i++)
                //{
                //    MessageBox.Show(dgvSearchProduct.SelectedRows[0].Cells[i].Value.ToString());
                //}

                frmPurchasePopUp frmPurchasePopUp = new frmPurchasePopUp();                
                frmPurchasePopUp.productID = dgvSearchProduct.SelectedRows[0].Cells[1].Value.ToString();
                frmPurchasePopUp.productName = dgvSearchProduct.SelectedRows[0].Cells[2].Value.ToString();
                frmPurchasePopUp.brand = dgvSearchProduct.SelectedRows[0].Cells[4].Value.ToString();
                frmPurchasePopUp.Less5 = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[9].Value);
                frmPurchasePopUp.prevPurchasePrice = dgvSearchProduct.SelectedRows[0].Cells[10].Value.ToString();
                frmPurchasePopUp.gst = dgvSearchProduct.SelectedRows[0].Cells[11].Value.ToString();
                frmPurchasePopUp.gstID = Convert.ToInt32(dgvSearchProduct.SelectedRows[0].Cells[12].Value);
                frmPurchasePopUp.unitID = Convert.ToInt32(dgvSearchProduct.SelectedRows[0].Cells[13].Value);
                frmPurchasePopUp.brandID = Convert.ToInt32(dgvSearchProduct.SelectedRows[0].Cells[14].Value);
                frmPurchasePopUp.unit = dgvSearchProduct.SelectedRows[0].Cells[5].Value.ToString();
                frmPurchasePopUp.currentStock = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[7].Value);
                frmPurchasePopUp._5to10 = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[15].Value);
                frmPurchasePopUp.Greater10 = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[16].Value);

                Opacity = .50;
                frmPurchasePopUp.ShowDialog();
                Opacity = 1;

                fetchHashPurchase();

            }
        }

        private void dgvSearchProduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmPurchasePopUp frmPurchasePopUp = new frmPurchasePopUp();
            frmPurchasePopUp.productID = dgvSearchProduct.SelectedRows[0].Cells[1].Value.ToString();
            frmPurchasePopUp.productName = dgvSearchProduct.SelectedRows[0].Cells[2].Value.ToString();
            frmPurchasePopUp.brand = dgvSearchProduct.SelectedRows[0].Cells[4].Value.ToString();
            frmPurchasePopUp.Less5 = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[9].Value);
            frmPurchasePopUp.prevPurchasePrice = dgvSearchProduct.SelectedRows[0].Cells[10].Value.ToString();
            frmPurchasePopUp.gst = dgvSearchProduct.SelectedRows[0].Cells[11].Value.ToString();
            frmPurchasePopUp.gstID = Convert.ToInt32(dgvSearchProduct.SelectedRows[0].Cells[12].Value);
            frmPurchasePopUp.unitID = Convert.ToInt32(dgvSearchProduct.SelectedRows[0].Cells[13].Value);
            frmPurchasePopUp.brandID = Convert.ToInt32(dgvSearchProduct.SelectedRows[0].Cells[14].Value);
            frmPurchasePopUp.unit = dgvSearchProduct.SelectedRows[0].Cells[5].Value.ToString();
            frmPurchasePopUp.currentStock = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[7].Value);
            frmPurchasePopUp._5to10 = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[15].Value);
            frmPurchasePopUp.Greater10 = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[16].Value);

            Opacity = .50;
            frmPurchasePopUp.ShowDialog();
            Opacity = 1;

            fetchHashPurchase();
        }

        private void btnSupplierAdd_Click(object sender, EventArgs e)
        {            
            master.frmAddSupplier frmAddSupplier = new master.frmAddSupplier();
            frmAddSupplier.varFromPurchase = true;

            Opacity = .50;
            frmAddSupplier.ShowDialog();
            Opacity = 1;

            fillSupplierCmb();
            cmbSupplier.Focus();
        }

        private void txtPaidAmount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtOtherCharges_KeyPress(object sender, KeyPressEventArgs e)
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

        private void calculatAdvanceAndBalance()
        {
            Decimal x;

            if (Convert.ToDecimal(lblPrevAdv.Text) > 0)
            {
                lblPrevAdv.Visible = true;
                x = (Convert.ToDecimal(lblPrevAdv.Text) - Convert.ToDecimal(txtGrandTotal.Text)) + Convert.ToDecimal(txtPaidAmount.Text == "" ? "0.00" : txtPaidAmount.Text);

                if (x < 0)
                {
                    txtBalanceAmount.Text = string.Format("{0:0.00}", Math.Abs(x));
                    txtAdvanceAmount.Text = "0.00";
                }
                else
                {
                    txtAdvanceAmount.Text = string.Format("{0:0.00}", x);
                    txtBalanceAmount.Text = "0.00";
                }
            }
            else
            {
                lblPrevAdv.Visible = false;
                x = (Convert.ToDecimal(lblPrevAdv.Text) + Convert.ToDecimal(txtGrandTotal.Text)) - Convert.ToDecimal(txtPaidAmount.Text == "" ? "0.00" : txtPaidAmount.Text);

                if (x < 0)
                {
                    txtBalanceAmount.Text = "0.00";
                    txtAdvanceAmount.Text = string.Format("{0:0.00}", Math.Abs(x));
                }
                else
                {
                    txtAdvanceAmount.Text = "0.00";
                    txtBalanceAmount.Text = string.Format("{0:0.00}", x); 
                }
            }
        }

        private void saveButtonClick()
        {
            if (dgvPurchaseList.Rows.Count > 0)
            {
                if (cmbSupplier.SelectedIndex == 0 && Convert.ToDecimal(txtPaidAmount.Text == "" ? "0.00" : txtPaidAmount.Text) < Convert.ToDecimal(txtGrandTotal.Text == "00.00" ? "0.00" : txtGrandTotal.Text))
                {
                    MessageBox.Show("Either mention supplier name or pay full amount.", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPaidAmount.Focus();
                }
                else if (cmbSupplier.SelectedIndex == 0 && Convert.ToDecimal(txtPaidAmount.Text == "" ? "0.00" : txtPaidAmount.Text) > Convert.ToDecimal(txtGrandTotal.Text == "00.00" ? "0.00" : txtGrandTotal.Text))
                {
                    MessageBox.Show("Either mention customer name or pay only bill amount.", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPaidAmount.Focus();
                }
                else if (cmbSupplier.SelectedIndex == 0 && (cmbPayMode.SelectedIndex == 1 || cmbPayMode.SelectedIndex == 3 || cmbPayMode.SelectedIndex == 4))
                {
                    MessageBox.Show("Either mention customer name or pay full 'cash' amount.", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPaidAmount.Focus();
                }
                else if (cmbPayMode.SelectedIndex != 0 && Convert.ToDecimal(txtPaidAmount.Text == "00.00" ? "0.00" : txtPaidAmount.Text) <= 0)
                {
                    MessageBox.Show("Mention paid amount.", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPaidAmount.Focus();
                }
                else
                {
                    if (cmbPayMode.SelectedIndex == 1 && refNumber.refrenceNo == "")
                    {
                        frmRefrenceNoPopup frmRefrenceNoPopup = new frmRefrenceNoPopup();
                        Opacity = .50;
                        frmRefrenceNoPopup.ShowDialog();
                        Opacity = 1;
                    }
                    else
                    {
                        var confirmResult = MessageBox.Show("Are you sure to save this Bill ?", "Confirm Save !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmResult == DialogResult.Yes)
                        {
                            if (addPurchaseProduct() && addPurchasePayment())
                            {
                                truncateHashTable();
                                ClearForm();
                                lblAlert.Location = new Point(58, 8);
                                successAlert("Purchase Bill Save Successfully.");
                                lblAlert.Location = new Point(65, 8);
                            }
                            else
                            {
                                lblAlert.Location = new Point(80, 8);
                                dangerAlert("Purchase Bill Save Error !");
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            else
            {
                lblAlert.Location = new Point(80, 8);
                dangerAlert("Add products to the bill.");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveButtonClick();
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbSupplier.SelectedValue.ToString() == "" ? "0" : cmbSupplier.SelectedValue.ToString()) == 0)
            {
                txtGrandTotal.Text = "0.00";
                txtBalanceAmount.Text = "0.00";
                txtAdvanceAmount.Text = "0.00";
                lblPrevAdv.Visible = false;
            }
            else
            {
                getSupplierDetails(Convert.ToInt32(cmbSupplier.SelectedValue.ToString() == "" ? "0" : cmbSupplier.SelectedValue.ToString()));
                               
                if (txtOtherCharges.Text != "")
                {
                    txtGrandTotal.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text) + Convert.ToDecimal(txtSuppBalance.Text), 0, MidpointRounding.AwayFromZero));
                }

                calculatAdvanceAndBalance();
            }            
        }
        
        private void txtOtherCharges_Leave(object sender, EventArgs e)
        {
            txtOtherCharges.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text));
            if (txtOtherCharges.Text != "")
            {
                txtBillTotal.Text = string.Format("{0:0.00}", Math.Round(varBillTotal + Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text) - Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text), 0, MidpointRounding.AwayFromZero));
            }
            txtGrandTotal.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtSuppBalance.Text), 0, MidpointRounding.AwayFromZero));
            calculatAdvanceAndBalance();
            txtOtherCharges.DeselectAll();
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";

            calculatAdvanceAndBalance();

            #region oldCalculation
            //if (Convert.ToDecimal(txtAdvanceAmount.Text) > 0)
            //{
            //    Decimal RemainingAdvance = Convert.ToDecimal(lblPrevAdv.Text) - Convert.ToDecimal(txtGrandTotal.Text);
            //    lblPrevAdv.Visible = true;
            //    if (RemainingAdvance > 0)
            //    {
            //        txtBalanceAmount.Text = "0.00";
            //        txtAdvanceAmount.Text = RemainingAdvance.ToString();
            //    }
            //    else
            //    {
            //        txtBalanceAmount.Text = string.Format("{0:0.00}", Math.Abs(RemainingAdvance));
            //    }
            //}
            //else
            //{
            //    lblPrevAdv.Visible = false;

            //    if (txtPaidAmount.Text != "")
            //    {
            //        txtBalanceAmount.Text = (Convert.ToDecimal(txtGrandTotal.Text) - Convert.ToDecimal(txtPaidAmount.Text == "" ? "0.00" : txtPaidAmount.Text)).ToString();
            //    }
            //}
            #endregion
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to discard the bill ?", "Confirm Reset!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                ClearForm();
                truncateHashTable();
            }
            else
            {
                return;
            }
        }

        private void dgvPurchaseList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if click is on new row or header row
            if (e.RowIndex == dgvPurchaseList.NewRowIndex || e.RowIndex < 0)
            {
                return;
            }              

            //Check if click is on specific column 
            if (e.ColumnIndex == 14)
            {
                deleteFromHashTable(Convert.ToInt32(dgvPurchaseList.SelectedRows[0].Cells[1].Value));
                fetchHashPurchase();
            }
        }

        private void cmbPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPayMode.SelectedIndex == 1 || cmbPayMode.SelectedIndex == 3 || cmbPayMode.SelectedIndex == 4)
            {
                frmRefrenceNoPopup frmRefrenceNoPopup = new frmRefrenceNoPopup();
                Opacity = .50;
                frmRefrenceNoPopup.ShowDialog();
                Opacity = 1;
            }
        }
            
        private void txtPaidAmount_Click(object sender, EventArgs e)
        {
            txtPaidAmount.SelectAll();
        }

        private void txtPaidAmount_Leave(object sender, EventArgs e)
        {
            txtPaidAmount.DeselectAll();
            txtPaidAmount.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtPaidAmount.Text == "" ? "0.00" : txtPaidAmount.Text));
        }
        
        private void btnAddProduct_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Add New Product", btnAddProduct);
        }

        private void btnSupplierAdd_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Add New Supplier", btnSupplierAdd);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            master.frmAddProduct frmAddProduct = new master.frmAddProduct();
            frmAddProduct.varFromPurchase = true;
            Opacity = .50;
            frmAddProduct.ShowDialog();
            Opacity = 1;

            txtSearch.Focus();
        }

        private void txtBillTotal_Leave(object sender, EventArgs e)
        {
            txtBillTotal.DeselectAll();
        }

        private void txtGrandTotal_Leave(object sender, EventArgs e)
        {
            txtGrandTotal.DeselectAll();
        }

        private void txtBalanceAmount_Leave(object sender, EventArgs e)
        {
            txtBalanceAmount.DeselectAll();
        }

        private void txtSuppContact_Leave(object sender, EventArgs e)
        {
            txtAdvanceAmount.DeselectAll();
        }

        private void txtSuppBalance_Leave(object sender, EventArgs e)
        {
            txtSuppBalance.DeselectAll();
        }

        private void txtInvoice_Leave(object sender, EventArgs e)
        {
            txtInvoice.DeselectAll();
        }

        private void txtTotalGstAmount_Leave(object sender, EventArgs e)
        {
            txtTotalGstAmount.DeselectAll();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDate.MaxDate = DateTime.Now;
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            //if (txtDiscount.Text != "")
            //{
            //    txtGrandTotal.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text) + Convert.ToDecimal(txtSuppBalance.Text) - Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text)));
            //}
            if (varBillTotal > Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text))
            {
                txtBillTotal.Text = string.Format("{0:0.00}", varBillTotal - Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text) + Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text));
                txtGrandTotal.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtSuppBalance.Text), 0, MidpointRounding.AwayFromZero));
            }
            else
            {
                txtBillTotal.Text = string.Format("{0:0.00}", varBillTotal + Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text));
                txtGrandTotal.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtSuppBalance.Text), 0, MidpointRounding.AwayFromZero));
                txtDiscount.Text = "0.00";
            }

            calculatAdvanceAndBalance();
            txtDiscount.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text));
            txtDiscount.DeselectAll();
        }

        private void txtDiscount_Click(object sender, EventArgs e)
        {
            txtDiscount.SelectAll();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtOtherCharges_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtOtherCharges_Click(object sender, EventArgs e)
        {
            txtOtherCharges.SelectAll();
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                saveButtonClick();
                return true;
            }
            else if (keyData == Keys.F1)
            {
                txtSearch.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void cmbSupplier_Leave(object sender, EventArgs e)
        {
            int index = cmbSupplier.FindString(cmbSupplier.Text);
            if (index < 0)
            {
                cmbSupplier.SelectedIndex = 0;
            }
            else
            {
                cmbSupplier.SelectedIndex = index;
            }
        }
    }

    public static class refNumber
    {
        public static string refrenceNo;
    }
}
