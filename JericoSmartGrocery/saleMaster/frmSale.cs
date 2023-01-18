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

namespace JericoSmartGrocery.saleMaster
{
    public partial class frmSale : Form
    {
        public int Bill_1 = 0;
        public int Bill_2 = 0;
        public int Bill_3 = 0;
        public int editBillNo = 0;
        public int editCustomerID = 0;
        public bool edit = false;
        //public int returnCustID;
        
        public frmSale()
        {
            InitializeComponent();
            //this.Size = new Size(1582, 862);
        }

        private void frmSale_Load(object sender, EventArgs e)
        {
            truncateAllHashrechord();

            dgvSaleList.Visible = true;
            dgvSearchProduct.Visible = false;
            dtpDate.Value = DateTime.Now;
            btnCustomerAdd.FlatAppearance.BorderColor = Color.FromArgb(177, 177, 177);

            txtBillNo.Text = billNo().ToString();
            Bill_1 = Convert.ToInt32(txtBillNo.Text);
            btnBill1.Text = txtBillNo.Text;
            btnBill1.FlatAppearance.BorderColor = Color.FromArgb(169,169,169);
            btnBill1.FlatAppearance.BorderSize = 2;

            dgvSaleList.Visible = true;
            dgvSearchProduct.Visible = false;

            fillCustomerCmb();
            fetchHashSale();
            cmbPayMode.SelectedIndex = 0;           

            txtPaidAmount.Enter += new EventHandler(txtPaidAmount_Focus);
            txtSearch.Enter += new EventHandler(txtSearch_Focus);
            txtDiscount.Enter += new EventHandler(txtDiscount_Focus);
            txtOtherCharges.Enter += new EventHandler(txtOtherCharges_Focus);
                        
            if (addSaleHashValidation())
            {
                addBillToHash();
            }

            //Edite Bill
            if (edit)
            {
                btnBill1.Text = editBillNo.ToString();
                txtBillNo.Text = editBillNo.ToString();
                fillSaleHashTableForEdit();
                fetchHashSale();
                btnUpdate.Visible = true;
                lblPrevDiscount.Visible = true;
                btnSave.TabIndex = 20;
                btnUpdate.TabIndex = 4;
                btnNewBill.Visible = false;
                dtpDate.Enabled = false;
            }
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

        protected void txtOtherCharges_Focus(Object sender, EventArgs e)
        {
            txtOtherCharges.SelectAll();
        }

        public void fillCustomerCmb()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            DT.Columns.Add("customerName", typeof(string));
            DT.Columns.Add("customerID", typeof(int));
            DataRow row = DT.NewRow();
            row["customerID"] = "0";
            row["customerName"] = "-- Select Customer --";
            DT.Rows.InsertAt(row, 0);

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Customer_FetchinCombobox";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    this.cmbCustomer.SelectedIndexChanged -= new EventHandler(cmbCustomer_SelectedIndexChanged);

                    cmbCustomer.DataSource = DT;
                    cmbCustomer.ValueMember = "customerID";
                    cmbCustomer.DisplayMember = "customerName";
                    this.cmbCustomer.SelectedIndexChanged += new EventHandler(cmbCustomer_SelectedIndexChanged);
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

                //if (returnCustID > 0)
                //{
                //    this.cmbCustomer.SelectedIndexChanged -= new EventHandler(cmbCustomer_SelectedIndexChanged);
                //    cmbCustomer.SelectedValue = returnCustID;
                //    this.cmbCustomer.SelectedIndexChanged += new EventHandler(cmbCustomer_SelectedIndexChanged);
                //}
            }
        }

        private void searchProduct()
        {
            dgvSearchProduct.Visible = true;
            dgvSaleList.Visible = false;

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                if (edit)
                {
                    comm.CommandText = "Proc_Product_PurEntry_Search_forEdit";
                }
                else
                {
                    comm.CommandText = "Proc_Product_PurEntry_Search";
                }

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

                    if (Global.userRole == "Operator")
                    {
                        dgvSearchProduct.Columns[10].Visible = false;
                    }
                    else
                    {
                        dgvSearchProduct.Columns[10].Visible = true;
                    }
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

        private string fetchBillNoFromHash()
        {
            string billNo = "";

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleHash_getMaxBillNo";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    billNo = dtpDate.Value.ToString("yyMMdd") + (Convert.ToInt32(DT.Rows[0]["billNo"]) + 1).ToString("000");
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

        private string billNo()
        {
            string billNo = "";

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Sale_getMaxBillNo";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    billNo = dtpDate.Value.ToString("yyMMdd") + (Convert.ToInt32(DT.Rows[0]["saleBillNumber"]) + 1).ToString("000");
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

        private void getCustomerDetails(int ID)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Customer_GetDetailbyID";

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
                    txtCustBalance.Text = DT.Rows[0]["balanceAmount"].ToString();
                    lblPrevAdv.Text = DT.Rows[0]["customerAdvanceAmount"].ToString();
                    txtAdvanceAmount.Text = DT.Rows[0]["customerAdvanceAmount"].ToString();
                    lblLastBillDate.Text = "Last Bill Date : " + Convert.ToDateTime(DT.Rows[0]["paymentDate"]).ToString("dd-MMM-yyyy");
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

        public void fetchHashSale()
        {
            dgvSearchProduct.Visible = false;
            dgvSaleList.Visible = true;

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataSet DS = new DataSet();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleHash_fetchinDgv";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("billNumber", txtBillNo.Text);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS);

                if (DS.Tables[1].Rows.Count > 0)
                {
                    cmbCustomer.SelectedValue = DS.Tables[1].Rows[0]["customerID"].ToString();
                }
                else
                {
                    cmbCustomer.SelectedIndex = 0;
                }

                if (DS.Tables[0].Rows.Count > 0)
                {
                    dgvSaleList.DataSource = DS.Tables[0];

                    int i = dgvSaleList.Rows.Count;
                    foreach (DataGridViewRow row in dgvSaleList.Rows)
                    {
                        row.Cells[0].Value = i;
                        i--;
                    }

                    Decimal total = 0;
                    Decimal gsttotal = 0;
                    foreach (DataGridViewRow row in dgvSaleList.Rows)
                    {
                        total += Convert.ToDecimal(row.Cells[15].Value);
                        gsttotal += Convert.ToDecimal(row.Cells[7].Value);
                        
                    }
                                        
                    txtBillTotal.Text = (total + Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text) - Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text)).ToString();
                    txtTotalGstAmount.Text = gsttotal.ToString();
                    txtGrandTotal.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtCustBalance.Text), 0, MidpointRounding.AwayFromZero));                    
                }
                else
                {
                    dgvSaleList.DataSource = DS.Tables[0];
                    txtBillTotal.Text = "0.00";
                    txtGrandTotal.Text = txtCustBalance.Text;
                    txtOtherCharges.Text = "0.00";
                    txtDiscount.Text = "0.00";
                    txtBalanceAmount.Text = (Convert.ToDecimal(txtGrandTotal.Text) - Convert.ToDecimal(txtPaidAmount.Text == "" ? "0.00" : txtPaidAmount.Text)).ToString();
                }                

                calculatAdvanceAndBalance();

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

                dgvSaleList.ClearSelection();
                dgvSaleList.Visible = true;
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
                comm.CommandText = "Proc_SaleHash_deleteOneRecord";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Id", ID);
                comm.Parameters.AddWithValue("bill", Convert.ToDecimal(txtBillNo.Text));

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

                fetchHashSale();
            }
        }

        private void updateQtyInHash(int ID, decimal qty)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleHash_updateQty";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Id", ID);
                comm.Parameters.AddWithValue("quantity", qty);

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
                dangerAlert("Quantity updating Error!");
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

                fetchHashSale();
            }
        }

        private void updateCustomerInHash(int custID)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleHash_updateCustomer";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("billNumber", txtBillNo.Text);
                comm.Parameters.AddWithValue("custID", custID);

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

        private void truncateAllHashrechord()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleHash_truncatetable";

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

        private void truncateHashTable(string billNo)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleHash_deleteBill";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("billNumber", billNo);
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
                fetchHashSale();

                if (txtBillNo.Text == Bill_1.ToString())
                {
                    Bill_1 = 0;
                    btnBill1.Visible = false;
                    btnNewBill.Visible = true;

                    if (Bill_2 != 0 && Bill_3 != 0)
                    {
                        btnBill2.Location = new Point(667, 51);
                        btnBill3.Location = new Point(776, 51);
                        btnNewBill.Location = new Point(885, 51);
                    }
                    else if (Bill_2 == 0 && Bill_3 != 0)
                    {                        
                        btnBill3.Location = new Point(667, 51);
                        btnNewBill.Location = new Point(776, 51);
                    }
                    else if (Bill_3 == 0 && Bill_2 != 0)
                    {
                        btnBill2.Location = new Point(667, 51);
                        btnNewBill.Location = new Point(776, 51);
                    }
                }
                else if(txtBillNo.Text == Bill_2.ToString())
                {
                    Bill_2 = 0;
                    btnBill2.Visible = false;
                    btnNewBill.Visible = true;

                    if (Bill_1 != 0 && Bill_3 != 0)
                    {
                        btnBill1.Location = new Point(667, 51);
                        btnBill3.Location = new Point(776, 51);
                        btnNewBill.Location = new Point(885, 51);
                    }
                    else if (Bill_1 == 0 && Bill_3 != 0)
                    {
                        btnBill3.Location = new Point(667, 51);
                        btnNewBill.Location = new Point(776, 51);
                    }
                    else if (Bill_3 == 0 && Bill_1 != 0)
                    {
                        btnBill1.Location = new Point(667, 51);
                        btnNewBill.Location = new Point(776, 51);
                    }
                }
                else if (txtBillNo.Text == Bill_3.ToString())
                {
                    Bill_3 = 0;
                    btnBill3.Visible = false;
                    btnNewBill.Visible = true;

                    if (Bill_2 != 0 && Bill_1 != 0)
                    {
                        btnBill1.Location = new Point(667, 51);
                        btnBill2.Location = new Point(776, 51);
                        btnNewBill.Location = new Point(885, 51);
                    }
                    else if (Bill_2 == 0 && Bill_1 != 0)
                    {
                        btnBill1.Location = new Point(667, 51);
                        btnNewBill.Location = new Point(776, 51);
                    }
                    else if (Bill_1 == 0 && Bill_2 != 0)
                    {
                        btnBill2.Location = new Point(667, 51);
                        btnNewBill.Location = new Point(776, 51);
                    }
                }
            }
        }

        private void resetBill(string billNo)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleHash_resetBill";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("billNumber", billNo);
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
                dangerAlert("Bill List Reset Error!");
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
                fetchHashSale();
            }
        }

        private Boolean addSaleHashValidation()
        {
            Boolean retVal = false;

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleHash_Addvalidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("id", 0);
                comm.Parameters.AddWithValue("billNumber", txtBillNo.Text);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    retVal = false;
                }
                else
                {
                    retVal = true;
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
            }
            return retVal;
        }

        private Boolean addBillToHash()
        {
            Boolean retVal = false;
            DAL.DAL_hashSale objDAL = new DAL.DAL_hashSale();
            BAL.BAL_hashSale objBAL = new BAL.BAL_hashSale();
            try
            {
                objDAL.productID = 0;
                objDAL.productName = "";
                objDAL.brandID = 0;
                objDAL.brand = "";
                objDAL.gstID = 0;
                objDAL.gst = Convert.ToDecimal(0.00);
                objDAL.gstamount = Convert.ToDecimal(0.00);
                objDAL.unitID = 0;
                objDAL.unit = "";
                objDAL.qty = Convert.ToDecimal(0.00);
                objDAL.purchaseRate = Convert.ToDecimal(0.00);
                objDAL.saleRate = Convert.ToDecimal(0.00);
                objDAL.totalprice = Convert.ToDecimal(0.00);
                objDAL.currentStock = Convert.ToDecimal(0.00);
                objDAL.gstOnMargine = Convert.ToDecimal(0.00);
                objDAL.customerID = Convert.ToInt32(0);
                objDAL.billNo = Convert.ToInt32(txtBillNo.Text);

                if (objBAL.addHashSale(objDAL))
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
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

        private Boolean addSaleProduct()
        {
            Boolean retVal = false;
            DAL.DAL_hashSale objDAL = new DAL.DAL_hashSale();
            BAL.BAL_hashSale objBAL = new BAL.BAL_hashSale();
            try
            {
                foreach (DataGridViewRow row in dgvSaleList.Rows)
                {
                    objDAL.billNo = Convert.ToInt32(txtBillNo.Text);
                    objDAL.saleDate = Convert.ToDateTime(dtpDate.Value.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"));
                    objDAL.productID = Convert.ToInt32(row.Cells[1].Value);
                    objDAL.unitID = Convert.ToInt32(row.Cells[8].Value);
                    objDAL.brandID = Convert.ToInt32(row.Cells[3].Value);
                    objDAL.gstID = Convert.ToInt32(row.Cells[5].Value);
                    objDAL.purchaseRate = Convert.ToDecimal(row.Cells[14].Value);
                    objDAL.qty = Convert.ToDecimal(row.Cells[10].Value);
                    objDAL.totalprice = Convert.ToDecimal(row.Cells[15].Value);
                    objDAL.saleRate = Convert.ToDecimal(row.Cells[13].Value);
                    objDAL.gstamount = Convert.ToDecimal(row.Cells[7].Value);
                    objDAL.currentStock = Convert.ToDecimal(row.Cells[17].Value);
                    objDAL.customerID = Convert.ToInt32(cmbCustomer.SelectedValue.ToString() == "" ? "0" : cmbCustomer.SelectedValue);
                    objDAL.gstOnMargine = Convert.ToDecimal(row.Cells[18].Value);                    

                    if (objBAL.addSaleProduct(objDAL))
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

        private Boolean addSalePayment()
        {
            Boolean retVal = false;
            DAL.DAL_hashSale objDAL = new DAL.DAL_hashSale();
            BAL.BAL_hashSale objBAL = new BAL.BAL_hashSale();

            try
            {
                objDAL.customerID = Convert.ToInt32(cmbCustomer.SelectedValue.ToString() == "" ? "0" : cmbCustomer.SelectedValue.ToString());
                objDAL.billNo = Convert.ToInt32(txtBillNo.Text);
                objDAL.paymentDate = Convert.ToDateTime(dtpDate.Value.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"));
                objDAL.payMode = txtPaidAmount.Text == "0.00" || txtPaidAmount.Text == "" ? "-" : cmbPayMode.Text;
                objDAL.refNumber = purchaseMaster.refNumber.refrenceNo;
                objDAL.discount =Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text);
                objDAL.billAmount = Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtDiscount.Text) - Convert.ToDecimal(txtOtherCharges.Text);
                objDAL.OtherCharges = Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text);
                objDAL.prevBalance = Convert.ToDecimal(txtCustBalance.Text);
                objDAL.balanceAmount = Convert.ToDecimal(txtBalanceAmount.Text);
                objDAL.customerAdvanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text);
                objDAL.prevAdvanceAmount = Convert.ToDecimal(lblPrevAdv.Text);
                objDAL.nowPaidAmount = Convert.ToDecimal(txtPaidAmount.Text == "" ? "0.00" : txtPaidAmount.Text);
                objDAL.grandTotalAmount = Convert.ToDecimal(txtGrandTotal.Text);
                objDAL.totalGstAmount = Convert.ToDecimal(txtTotalGstAmount.Text);
                objDAL.isSuccesful = cmbPayMode.Text == "Cash" ? true : false;
                //objDAL.isBalanceBill = objDAL.nowPaidAmount < Math.Round(objDAL.billAmount) ? true : false;
                //objDAL.isAdvanceBill = objDAL.nowPaidAmount > Math.Round(objDAL.billAmount) ? true : false;

                decimal remainingAmount = (objDAL.billAmount - objDAL.nowPaidAmount);

                objDAL.isBalanceBill = remainingAmount > 1 ? true : false;
                objDAL.isAdvanceBill = remainingAmount < -1 ? true : false;
                objDAL.commitDate = dtpVayda.Value.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd") ? "" : dtpVayda.Value.ToString("dd-MM-yyyy");


                if (objBAL.addSalePayment(objDAL))
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

        private void insertTempRepordData()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            comm.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            comm.Parameters.AddWithValue("saleBillNo", txtBillNo.Text);

            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "proc_tb_tempreport_insert";
            comm.ExecuteNonQuery();

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            comm.Dispose();
            conn.Dispose();
        }

        private void truncateTempTbl()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            comm.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            comm.CommandType = CommandType.Text;
            comm.CommandText = "truncate tb_tempreport";
            comm.ExecuteNonQuery();

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            comm.Dispose();
            conn.Dispose();
        }

        private void printInvoice()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataSet DS = new DataSet();
            crReports.rptSaleInvoice rptSaleInvoice = new crReports.rptSaleInvoice();

            try
            {
                comm.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select shopName,contactOne,contactTwo,shopAddress,gstNumber,termsConditions,termsConditions2 from tb_aboutshop";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_aboutshop");

                comm.CommandType = CommandType.Text;
                comm.CommandText = "SELECT  marathiName, saleQty, saleRate, totalPrice,unit FROM tb_sales WHERE saleBillNumber ='" + txtBillNo.Text + "'";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_sales");
                DA.Fill(DS, "tb_productdetails");

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select saleBillNumber, nowPaidAmount, otherCharges, round(billAmount + otherCharges - discountAmount) as grandTotalAmount, balanceAmount, customerAdvanceAmount, discountAmount, prevBalance, prevAdvanceAmount from tb_salespaymentdetails where saleBillNumber = '" + txtBillNo.Text + "' and transactiontype = 'Sale';";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_salespaymentdetails");

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select customerName from tb_customerdetails where customerID = '" + (cmbCustomer.SelectedValue.ToString() == "" ? "0" : cmbCustomer.SelectedValue.ToString()) + "'";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_customerdetails");

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select date,time,billStatus from tb_tempreport";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_tempreport");

                rptSaleInvoice.SetDataSource(DS);                     

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDialog1.AllowSomePages = true;
                    rptSaleInvoice.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                    rptSaleInvoice.PrintToPrinter(printDialog1.PrinterSettings.Copies, false, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);
                }                
            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                dangerAlert("Data Loading Error!");
                MessageBox.Show(x);
                //MessageBox.Show("Overrided Report is open in other app.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                rptSaleInvoice.Close();
                rptSaleInvoice.Dispose();
                comm.Dispose();
                conn.Dispose();
            }
        }

        private void ClearForm()
        {
            txtSearch.Text = "";
            txtAdvanceAmount.Text = "0.00";
            lblPrevAdv.Visible = false;
            txtCustBalance.Text = "0.00";
            txtDiscount.Text = "0.00";
            txtBillNo.Text = "";
            txtTotalGstAmount.Text = "0.00";
            txtOtherCharges.Text = "0.00";
            cmbPayMode.SelectedIndex = 0;
            txtPaidAmount.Text = "0.00";
            txtBillTotal.Text = "0.00";
            txtGrandTotal.Text = "0.00";
            txtBalanceAmount.Text = "0.00";
            cmbCustomer.SelectedIndex = 0;
            cmbPayMode.SelectedIndex = 0;
            purchaseMaster.refNumber.refrenceNo = "";
            if (Bill_1 == 0 && Bill_2 == 0 && Bill_3 == 0)
            {
                txtBillNo.Text = billNo().ToString();
                Bill_1 = Convert.ToInt32(txtBillNo.Text);

                if (addSaleHashValidation())
                {
                    addBillToHash();
                }

                btnBill1.Text = txtBillNo.Text;
                btnBill1.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
                btnBill1.FlatAppearance.BorderSize = 2;
                btnBill1.Visible = true;
                btnBill1.Location = new Point(667, 51);
                btnNewBill.Visible = true;
                btnNewBill.Location = new Point(776, 51);
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
            if (edit)
            {
                truncateHashTable(txtBillNo.Text);
                Bill_1 = 0;
                Bill_2 = 0;
                Bill_3 = 0;
                edit = false;
                editBillNo = 0;
                editCustomerID = 0;
            }

            if (Bill_1 == 0 && Bill_2 == 0 && Bill_3 == 0)
            {
                Close();
            }
            else
            {
                var confirmResult = MessageBox.Show("Are you sure to exit without saving the bill ?", "Confirm Exit!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    truncateHashTable(txtBillNo.Text);

                    if (Bill_1 == 0 && Bill_2 == 0 && Bill_3 == 0)
                    {
                        Close();
                    }
                    else
                    {
                        if (Bill_1 != 0)
                        {
                            txtBillNo.Text = Bill_1.ToString();
                            btnBill1.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
                            btnBill1.FlatAppearance.BorderSize = 2;
                        }
                        else if (Bill_2 != 0)
                        {
                            txtBillNo.Text = Bill_2.ToString();
                            btnBill2.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
                            btnBill2.FlatAppearance.BorderSize = 2;
                        }
                        else if (Bill_3 != 0)
                        {
                            txtBillNo.Text = Bill_3.ToString();
                            btnBill3.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
                            btnBill3.FlatAppearance.BorderSize = 2;
                        }
                        fetchHashSale();
                    }
                }
                else
                {
                    return;
                }
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
                
                dgvSaleList.Visible = true;
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
            else if (e.KeyCode == Keys.Escape)
            {
                txtSearch.Text = "";
                dgvSearchProduct.ClearSelection();
                dgvSaleList.Visible = true;
                dgvSearchProduct.Visible = false;
            }
        }

        private void dgvSearchProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvSearchProduct.SelectedRows[0].Cells[7].Value.ToString() == "0.00" || dgvSearchProduct.SelectedRows[0].Cells[7].Value.ToString() == "0")
                {
                    MessageBox.Show("Product is out of stock", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    frmSalePopUp frmSalePopUp = new frmSalePopUp();
                    frmSalePopUp.productID = dgvSearchProduct.SelectedRows[0].Cells[1].Value.ToString();
                    frmSalePopUp.productName = dgvSearchProduct.SelectedRows[0].Cells[2].Value.ToString();
                    frmSalePopUp.brand = dgvSearchProduct.SelectedRows[0].Cells[4].Value.ToString() == "-" ? "" : dgvSearchProduct.SelectedRows[0].Cells[4].Value.ToString();
                    frmSalePopUp.salePrice = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[9].Value);
                    frmSalePopUp.purchasePrice = dgvSearchProduct.SelectedRows[0].Cells[10].Value.ToString();
                    frmSalePopUp.gst = dgvSearchProduct.SelectedRows[0].Cells[11].Value.ToString();
                    frmSalePopUp.gstID = Convert.ToInt32(dgvSearchProduct.SelectedRows[0].Cells[12].Value);
                    frmSalePopUp.unitID = Convert.ToInt32(dgvSearchProduct.SelectedRows[0].Cells[13].Value);
                    frmSalePopUp.brandID = Convert.ToInt32(dgvSearchProduct.SelectedRows[0].Cells[14].Value);
                    frmSalePopUp.unit = dgvSearchProduct.SelectedRows[0].Cells[5].Value.ToString();
                    frmSalePopUp.currentStock = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[7].Value);
                    frmSalePopUp._5to10 = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[15].Value);
                    frmSalePopUp.Greater10 = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[16].Value);
                    frmSalePopUp.marathiName = dgvSearchProduct.SelectedRows[0].Cells[3].Value.ToString();
                    frmSalePopUp.billNo = Convert.ToInt32(txtBillNo.Text);
                    frmSalePopUp.customerID = Convert.ToInt32(cmbCustomer.SelectedValue.ToString() == "" ? "0" : cmbCustomer.SelectedValue);

                    Opacity = .50;
                    frmSalePopUp.ShowDialog();
                    Opacity = 1;
                    fetchHashSale();
                }
            }
        }


        private void dgvSearchProduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSearchProduct.SelectedRows[0].Cells[7].Value.ToString() == "0.00" || dgvSearchProduct.SelectedRows[0].Cells[7].Value.ToString() == "0")
            {
                MessageBox.Show("Product is out of stock", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                frmSalePopUp frmSalePopUp = new frmSalePopUp();
                frmSalePopUp.productID = dgvSearchProduct.SelectedRows[0].Cells[1].Value.ToString();
                frmSalePopUp.productName = dgvSearchProduct.SelectedRows[0].Cells[2].Value.ToString();
                frmSalePopUp.brand = dgvSearchProduct.SelectedRows[0].Cells[4].Value.ToString() == "-" ? "" : dgvSearchProduct.SelectedRows[0].Cells[4].Value.ToString();
                frmSalePopUp.salePrice = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[9].Value);
                frmSalePopUp.purchasePrice = dgvSearchProduct.SelectedRows[0].Cells[10].Value.ToString();
                frmSalePopUp.gst = dgvSearchProduct.SelectedRows[0].Cells[11].Value.ToString();
                frmSalePopUp.gstID = Convert.ToInt32(dgvSearchProduct.SelectedRows[0].Cells[12].Value);
                frmSalePopUp.unitID = Convert.ToInt32(dgvSearchProduct.SelectedRows[0].Cells[13].Value);
                frmSalePopUp.brandID = Convert.ToInt32(dgvSearchProduct.SelectedRows[0].Cells[14].Value);
                frmSalePopUp.unit = dgvSearchProduct.SelectedRows[0].Cells[5].Value.ToString();
                frmSalePopUp.currentStock = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[7].Value);
                frmSalePopUp._5to10 = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[15].Value);
                frmSalePopUp.Greater10 = Convert.ToDecimal(dgvSearchProduct.SelectedRows[0].Cells[16].Value);
                frmSalePopUp.marathiName = dgvSearchProduct.SelectedRows[0].Cells[3].Value.ToString();
                frmSalePopUp.billNo = Convert.ToInt32(txtBillNo.Text);
                frmSalePopUp.customerID = Convert.ToInt32(cmbCustomer.SelectedValue.ToString() == "" ? "0" : cmbCustomer.SelectedValue);

                Opacity = .50;
                frmSalePopUp.ShowDialog();
                Opacity = 1;
                fetchHashSale();
            }
        }

        private void btnCustomerAdd_Click(object sender, EventArgs e)
        {
            master.frmAddCustomer frmAddCustomer = new master.frmAddCustomer();
            frmAddCustomer.varFromSale = true;
            Opacity = .50;
            frmAddCustomer.ShowDialog();
            Opacity = 1;

            fillCustomerCmb();
            cmbCustomer.Focus();
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

            if (e.KeyChar == (char)13)
            {
                saveButtonClick();
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

            if (Convert.ToDecimal(txtBalanceAmount.Text) > 0)
            {
                lblVayada.Visible = true;
                dtpVayda.Visible = true;
            }
            else
            {
                lblVayada.Visible = false;
                dtpVayda.Visible = false;
            }
        }

        private void saveButtonClick()
        {
            if (dgvSaleList.Rows.Count > 0)
            {
                if (cmbCustomer.SelectedIndex == 0 && Convert.ToDecimal(txtPaidAmount.Text == "00.00" ? "0.00" : txtPaidAmount.Text) < Convert.ToDecimal(txtGrandTotal.Text == "00.00" ? "0.00" : txtGrandTotal.Text))
                {
                    MessageBox.Show("Either mention customer name or pay full amount.", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPaidAmount.Focus();
                }
                else if (cmbCustomer.SelectedIndex == 0 && Convert.ToDecimal(txtPaidAmount.Text == "00.00" ? "0.00" : txtPaidAmount.Text) > Convert.ToDecimal(txtGrandTotal.Text == "00.00" ? "0.00" : txtGrandTotal.Text))
                {
                    MessageBox.Show("Either mention customer name or pay only bill amount.", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPaidAmount.Focus();
                }
                else if (cmbCustomer.SelectedIndex == 0 && (cmbPayMode.SelectedIndex == 1 || cmbPayMode.SelectedIndex == 3 || cmbPayMode.SelectedIndex == 4))
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
                    if (cmbPayMode.SelectedIndex == 1 && purchaseMaster.refNumber.refrenceNo == "")
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
                            txtSearch.Focus();
                            if (edit)
                            {
                                getSaleProductIDByBillNoForEdit();
                                deleteOldEntry();
                            }

                            if (addSaleProduct() && addSalePayment())
                            {                                
                                insertTempRepordData();
                                printInvoice();
                                truncateTempTbl();
                                truncateHashTable(txtBillNo.Text);
                                successAlert("Sale Bill Save Successfully.");
                                ClearForm();

                                if (Bill_1 == 0 && Bill_2 == 0 && Bill_3 == 0)
                                {
                                    txtBillNo.Text = billNo().ToString();
                                    Bill_1 = Convert.ToInt32(txtBillNo.Text);

                                    if (addSaleHashValidation())
                                    {
                                        addBillToHash();
                                    }

                                    btnBill1.Text = txtBillNo.Text;
                                    btnBill1.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
                                    btnBill1.FlatAppearance.BorderSize = 2;
                                    btnBill1.Visible = true;
                                    btnBill1.Location = new Point(667, 51);
                                    btnNewBill.Visible = true;
                                    btnNewBill.Location = new Point(776, 51);
                                }
                                else
                                {
                                    if (Bill_1 != 0)
                                    {
                                        txtBillNo.Text = Bill_1.ToString();
                                        btnBill1.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
                                        btnBill1.FlatAppearance.BorderSize = 2;
                                    }
                                    else if (Bill_2 != 0)
                                    {
                                        txtBillNo.Text = Bill_2.ToString();
                                        btnBill2.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
                                        btnBill2.FlatAppearance.BorderSize = 2;
                                    }
                                    else if (Bill_3 != 0)
                                    {
                                        txtBillNo.Text = Bill_3.ToString();
                                        btnBill3.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
                                        btnBill3.FlatAppearance.BorderSize = 2;
                                    }
                                    fetchHashSale();
                                }

                                lblAlert.Location = new Point(58, 8);
                                lblAlert.Location = new Point(65, 8);

                                if (edit)
                                {
                                    edit = false;
                                    editBillNo = 0;
                                    editCustomerID = 0;
                                    Close();
                                }
                            }
                            else
                            {
                                lblAlert.Location = new Point(80, 8);
                                dangerAlert("Sale Bill Save Error !");
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

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateCustomerInHash(Convert.ToInt32(cmbCustomer.SelectedValue.ToString() == "" ? "0" : cmbCustomer.SelectedValue));

            if (Convert.ToInt32(cmbCustomer.SelectedValue.ToString() == "" ? "0" : cmbCustomer.SelectedValue.ToString()) == 0)
            {
                txtBalanceAmount.Text = "0.00";
                txtCustBalance.Text = "0.00";
                txtAdvanceAmount.Text = "0.00";
                lblPrevAdv.Text = "0.00";
                lblLastBillDate.Text = "";
            }
            else
            {                
                getCustomerDetails(Convert.ToInt32(cmbCustomer.SelectedValue.ToString() == "" ? "0" : cmbCustomer.SelectedValue.ToString()));                
                txtGrandTotal.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtCustBalance.Text), 0, MidpointRounding.AwayFromZero));                

                calculatAdvanceAndBalance();
            }
        }

        private void txtOtherCharges_Leave(object sender, EventArgs e)
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvSaleList.Rows)
            {
                total += Convert.ToDecimal(row.Cells[15].Value);
            }

                       
            txtBillTotal.Text = string.Format("{0:0.00}", Math.Round(total + Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text) - Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text), 0, MidpointRounding.AwayFromZero));          
            txtGrandTotal.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtCustBalance.Text), 0, MidpointRounding.AwayFromZero));
            calculatAdvanceAndBalance();

            txtOtherCharges.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text));
            txtOtherCharges.DeselectAll();
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
            calculatAdvanceAndBalance();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to discard the bill ?", "Confirm Reset!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                resetBill(txtBillNo.Text);
                txtBillTotal.Text = "0.00";
                txtOtherCharges.Text = "0.00";
                txtDiscount.Text = "0.00";
                txtPaidAmount.Text = "0.00";
                txtTotalGstAmount.Text = "0.00";
                txtGrandTotal.Text = "0.00";
                txtBalanceAmount.Text = "0.00";
                cmbPayMode.SelectedIndex = 0;
                cmbCustomer.SelectedValue = 0;
            }
            else
            {
                return;
            }
        }

        private void dgvSaleList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if click is on new row or header row
            if (e.RowIndex == dgvSaleList.NewRowIndex || e.RowIndex < 0)
            {
                return;
            }

            //Check if click is on specific column 
            if (e.ColumnIndex == 16)
            {
                deleteFromHashTable(Convert.ToInt32(dgvSaleList.SelectedRows[0].Cells[1].Value));
            }
            else if (e.ColumnIndex == 9)
            {
                decimal x = Convert.ToDecimal(dgvSaleList.SelectedRows[0].Cells[10].Value) - 1;
                if (x >= 1)
                {
                    updateQtyInHash(Convert.ToInt32(dgvSaleList.SelectedRows[0].Cells[1].Value), x);
                }
            }
            else if (e.ColumnIndex == 11)
            {
                updateQtyInHash(Convert.ToInt32(dgvSaleList.SelectedRows[0].Cells[1].Value), (Convert.ToDecimal(dgvSaleList.SelectedRows[0].Cells[10].Value) + 1));
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

        private void btnCustomerAdd_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Add New Customer", btnCustomerAdd);
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
        
        private void txtCustBalance_Leave(object sender, EventArgs e)
        {
            txtCustBalance.DeselectAll();
        }

        private void txtTotalGstAmount_Leave(object sender, EventArgs e)
        {
            txtTotalGstAmount.DeselectAll();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDate.MaxDate = DateTime.Now;
        }

        private void txtTotalGstAmount_Leave_1(object sender, EventArgs e)
        {
            txtTotalGstAmount.DeselectAll();
        }

        private void txtBillNo_Leave(object sender, EventArgs e)
        {
            txtBillNo.DeselectAll();
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
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

        private void txtDiscount_Click(object sender, EventArgs e)
        {
            txtDiscount.SelectAll();
        }

        private void btnNewBill_Click(object sender, EventArgs e) 
        {
            newBill();
        }

        private void newBill()
        {
            txtGrandTotal.Text = "0.00";
            txtBalanceAmount.Text = "0.00";
            txtCustBalance.Text = "0.00";
            txtDiscount.Text = "0.00";
            txtOtherCharges.Text = "0.00";
            txtPaidAmount.Text = "0.00";

            if (Bill_1 == 0)
            {
                txtBillNo.Text = fetchBillNoFromHash();
                Bill_1 = Convert.ToInt32(txtBillNo.Text);
                btnBill1.Text = txtBillNo.Text;
                if (addSaleHashValidation())
                {
                    addBillToHash();
                }
                fetchHashSale();
                btnBill1.Visible = true;
                btnBill1.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
                btnBill1.FlatAppearance.BorderSize = 2;
                btnBill2.FlatAppearance.BorderColor = Color.FromArgb(17, 19, 19);
                btnBill3.FlatAppearance.BorderColor = Color.FromArgb(17, 19, 19);

                if (Bill_2 != 0 && Bill_3 != 0)
                {
                    btnBill1.Location = new Point(667, 51);
                    btnBill2.Location = new Point(776, 51);
                    btnBill3.Location = new Point(885, 51);
                    btnNewBill.Visible = false;
                }
                else if (Bill_2 == 0 && Bill_3 != 0)
                {
                    btnBill1.Location = new Point(667, 51);
                    btnBill3.Location = new Point(776, 51);
                    btnNewBill.Location = new Point(885, 51);
                }
                else if (Bill_3 == 0 && Bill_2 != 0)
                {
                    btnBill1.Location = new Point(667, 51);
                    btnBill2.Location = new Point(776, 51);
                    btnNewBill.Location = new Point(885, 51);
                }

                txtSearch.Focus();
            }
            else if (Bill_2 == 0)
            {
                txtBillNo.Text = fetchBillNoFromHash();
                Bill_2 = Convert.ToInt32(txtBillNo.Text);
                btnBill2.Text = txtBillNo.Text;
                if (addSaleHashValidation())
                {
                    addBillToHash();
                }
                fetchHashSale();
                btnBill2.Visible = true;
                btnBill2.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
                btnBill2.FlatAppearance.BorderSize = 2;
                btnBill1.FlatAppearance.BorderColor = Color.FromArgb(17, 19, 19);
                btnBill3.FlatAppearance.BorderColor = Color.FromArgb(17, 19, 19);

                if (Bill_1 != 0 && Bill_3 != 0)
                {
                    btnBill1.Location = new Point(667, 51);
                    btnBill2.Location = new Point(776, 51);
                    btnBill3.Location = new Point(885, 51);
                    btnNewBill.Visible = false;
                }
                else if (Bill_1 == 0 && Bill_3 != 0)
                {
                    btnBill2.Location = new Point(667, 51);
                    btnBill3.Location = new Point(776, 51);
                    btnNewBill.Location = new Point(885, 51);
                }
                else if (Bill_3 == 0 && Bill_1 != 0)
                {
                    btnBill1.Location = new Point(667, 51);
                    btnBill2.Location = new Point(776, 51);
                    btnNewBill.Location = new Point(885, 51);
                }

                txtSearch.Focus();
            }
            else if (Bill_3 == 0)
            {
                txtBillNo.Text = fetchBillNoFromHash();
                Bill_3 = Convert.ToInt32(txtBillNo.Text);
                btnBill3.Text = txtBillNo.Text;

                if (addSaleHashValidation())
                {
                    addBillToHash();
                }
                fetchHashSale();
                btnBill3.Visible = true;
                btnBill3.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
                btnBill3.FlatAppearance.BorderSize = 2;
                btnBill1.FlatAppearance.BorderColor = Color.FromArgb(17, 19, 19);
                btnBill2.FlatAppearance.BorderColor = Color.FromArgb(17, 19, 19);

                if (Bill_2 != 0 && Bill_1 != 0)
                {
                    btnBill1.Location = new Point(667, 51);
                    btnBill2.Location = new Point(776, 51);
                    btnBill3.Location = new Point(885, 51);
                    btnNewBill.Visible = false;
                }
                else if (Bill_2 == 0 && Bill_1 != 0)
                {
                    btnBill1.Location = new Point(667, 51);
                    btnBill3.Location = new Point(776, 51);
                    btnNewBill.Location = new Point(885, 51);
                }
                else if (Bill_1 == 0 && Bill_2 != 0)
                {
                    btnBill2.Location = new Point(667, 51);
                    btnBill3.Location = new Point(776, 51);
                    btnNewBill.Location = new Point(885, 51);
                }

                txtSearch.Focus();
            }

            txtPaidAmount.Text = "0.00";
            txtOtherCharges.Text = "0.00";
            txtDiscount.Text = "0.00";
        }

        private void btnBill1_Click(object sender, EventArgs e)
        {
            btnBill1.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
            btnBill1.FlatAppearance.BorderSize = 2;
            btnBill2.FlatAppearance.BorderColor = Color.FromArgb(40, 40, 40);
            btnBill3.FlatAppearance.BorderColor = Color.FromArgb(40, 40, 40);
            
            if (Bill_1 != 0)
            {
                txtBillNo.Text = Bill_1.ToString();
                fetchHashSale();
            }

            txtPaidAmount.Text = "0.00";
            txtOtherCharges.Text = "0.00";
            txtDiscount.Text = "0.00";
            txtSearch.Focus();
        }

        private void btnBill2_Click(object sender, EventArgs e)
        {
            btnBill2.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
            btnBill2.FlatAppearance.BorderSize = 2;
            btnBill1.FlatAppearance.BorderColor = Color.FromArgb(17, 19, 19);
            btnBill3.FlatAppearance.BorderColor = Color.FromArgb(17, 19, 19);

            if (Bill_2 != 0)
            {
                txtBillNo.Text = Bill_2.ToString();
                fetchHashSale();
            }

            txtPaidAmount.Text = "0.00";
            txtOtherCharges.Text = "0.00";
            txtDiscount.Text = "0.00";
            txtSearch.Focus();
        }

        private void btnBill3_Click(object sender, EventArgs e)
        {
            btnBill3.FlatAppearance.BorderColor = Color.FromArgb(169, 169, 169);
            btnBill3.FlatAppearance.BorderSize = 2;
            btnBill1.FlatAppearance.BorderColor = Color.FromArgb(17, 19, 19);
            btnBill2.FlatAppearance.BorderColor = Color.FromArgb(17, 19, 19);

            if (Bill_3 != 0)
            {
                txtBillNo.Text = Bill_3.ToString();
                fetchHashSale();
            }

            txtPaidAmount.Text = "0.00";
            txtOtherCharges.Text = "0.00";
            txtDiscount.Text = "0.00";
            txtSearch.Focus();
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

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvSaleList.Rows)
            {
                total += Convert.ToDecimal(row.Cells[15].Value);
            }

            txtBillTotal.Text = string.Format("{0:0.00}", total - Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text) + Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text));
            txtGrandTotal.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtCustBalance.Text), 0, MidpointRounding.AwayFromZero));

            if ((total + Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text)) >= Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text))
            {
                txtBillTotal.Text = string.Format("{0:0.00}", total - Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text) + Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text));
                txtGrandTotal.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtCustBalance.Text), 0, MidpointRounding.AwayFromZero));
            }
            else
            {
                txtBillTotal.Text = string.Format("{0:0.00}", total + Convert.ToDecimal(txtOtherCharges.Text == "" ? "0.00" : txtOtherCharges.Text));
                txtGrandTotal.Text = string.Format("{0:0.00}", Math.Round(Convert.ToDecimal(txtBillTotal.Text) + Convert.ToDecimal(txtCustBalance.Text), 0, MidpointRounding.AwayFromZero));
                txtDiscount.Text = "0.00";
            }

            calculatAdvanceAndBalance();
            txtDiscount.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text));
            txtDiscount.DeselectAll();
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
            else if (keyData == (Keys.Control | Keys.U))
            {
                stockMaster.frmStockUpdate frmStockUpdate = new stockMaster.frmStockUpdate();
                frmStockUpdate.ShowDialog();
            }
            else if (keyData == (Keys.Control | Keys.A))
            {
                master.frmAddCustomer frmAddCustomer = new master.frmAddCustomer();
                frmAddCustomer.ShowDialog();
            }
            else if (keyData == (Keys.Control | Keys.N))
            {
                newBill();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void cmbCustomer_Leave(object sender, EventArgs e)
        {           
            int index = cmbCustomer.FindString(cmbCustomer.Text);
            if (index < 0)
            {
                cmbCustomer.SelectedIndex = 0;
            }
            else
            {
                cmbCustomer.SelectedIndex = index;
            }            
        }

        //Edit Bill

        private void fillSaleHashTableForEdit()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            comm.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            comm.Parameters.AddWithValue("BillNo", Convert.ToInt32(txtBillNo.Text));

            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "proc_SaleHash_fillTable_forEdit";

            DA = new MySqlDataAdapter(comm);
            DA.Fill(DT);

            if (DT.Rows.Count > 0)
            {
                this.cmbCustomer.SelectedIndexChanged -= new EventHandler(cmbCustomer_SelectedIndexChanged);
                cmbCustomer.SelectedValue = DT.Rows[0]["customerID"].ToString();
                this.cmbCustomer.SelectedIndexChanged += new EventHandler(cmbCustomer_SelectedIndexChanged);
                txtAdvanceAmount.Text = DT.Rows[0]["customerAdvanceAmount"].ToString();
                lblPrevAdv.Text = DT.Rows[0]["prevAdvanceAmount"].ToString();
                txtCustBalance.Text = DT.Rows[0]["prevBalance"].ToString();
                //txtDiscount.Text = DT.Rows[0]["discountAmount"].ToString();
                lblPrevDiscount.Text = DT.Rows[0]["discountAmount"].ToString();
                cmbPayMode.Text = DT.Rows[0]["paymentMode"].ToString();
                txtOtherCharges.Text = DT.Rows[0]["otherCharges"].ToString();
                txtPaidAmount.Text = DT.Rows[0]["nowPaidAmount"].ToString();
                txtGrandTotal.Text = DT.Rows[0]["grandTotalAmount"].ToString();
                txtBalanceAmount.Text = DT.Rows[0]["balanceAmount"].ToString();
                dtpDate.Value = Convert.ToDateTime(DT.Rows[0]["paymentDate"]);
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            comm.Dispose();
            conn.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            saveButtonClick();            
        }

        private void deleteOldEntry()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            comm.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            comm.Parameters.AddWithValue("BillNo", Convert.ToInt32(txtBillNo.Text));
            comm.Parameters.AddWithValue("cID", Convert.ToInt32(editCustomerID));
            comm.Parameters.AddWithValue("customerUpdate", (cmbCustomer.SelectedValue.ToString() == "" ? "0" : cmbCustomer.SelectedValue.ToString()) != editCustomerID.ToString() ? '1' : '0');

            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "proc_EditBill_deleteOldEntries";
            comm.ExecuteNonQuery();
            
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            comm.Dispose();
            conn.Dispose();
        }

        private void getSaleProductIDByBillNoForEdit()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            comm.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            comm.Parameters.AddWithValue("BillNo", Convert.ToInt32(txtBillNo.Text));

            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "proc_EditBill_getSaleProductID_byBill";

            DA = new MySqlDataAdapter(comm);
            DA.Fill(DT);

            if (DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    updateStockLedgerForEdit(Convert.ToInt32(DT.Rows[i]["productID"]), Convert.ToDecimal(DT.Rows[i]["saleQty"]), Convert.ToDecimal(DT.Rows[i]["nowStock"]));
                    updateStockInHashForEdit(Convert.ToInt32(DT.Rows[i]["productID"]));
                }
                fetchHashSale();
            }

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            comm.Dispose();
            conn.Dispose();
        }

        private void updateStockLedgerForEdit(int ID, decimal qty,decimal stock)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            comm.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            comm.Parameters.AddWithValue("proID", ID);
            comm.Parameters.AddWithValue("BillNo", Convert.ToInt32(txtBillNo.Text));
            comm.Parameters.AddWithValue("qty", qty);
            comm.Parameters.AddWithValue("stockNow", stock);

            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "proc_EditBill_updateStockLedger";
            comm.ExecuteNonQuery();

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            comm.Dispose();
            conn.Dispose();
        }

        private void updateStockInHashForEdit(int ID)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            comm.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            comm.Parameters.AddWithValue("proID", ID);

            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "proc_EditBill_updateStockInHash";
            comm.ExecuteNonQuery();

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            comm.Dispose();
            conn.Dispose();
        }
    }
}






/*
 private void printInvoice()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataSet DS = new DataSet();
            crReports.rptSaleInvoice rptSaleInvoice = new crReports.rptSaleInvoice();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Sale_InvoicePrint";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("billNo", txtBillNo.Text);
                comm.Parameters.AddWithValue("custID", cmbCustomer.SelectedValue.ToString() == "" ? "0" : cmbCustomer.SelectedValue);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);

                DA.Fill(DS, "tb_aboutshop");
                DA.Fill(DS, "tb_productdetails");
                DA.Fill(DS, "tb_sales");
                DA.Fill(DS, "tb_salespaymentdetails");
                DA.Fill(DS, "tb_customerdetails");

                if (DS.Tables.Count >= 0)
                {
                    rptSaleInvoice.SetDataSource(DS);

                    //string reppath = "";
                    //CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument();


                    //reppath = "D:/Development/Desktop Apps/JericoSmartGrocery/JericoSmartGrocery/crReports/trial.rpt";

                    //rd.Load(reppath);
                    //rd.SetDataSource(DT);
                    //crystalReportViewer1.ReportSource = rd;
                    //crystalReportViewer1.ShowCloseButton = true;
                    //crystalReportViewer1.Visible = true;

                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDialog1.AllowSomePages = true;
                        rptSaleInvoice.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                        rptSaleInvoice.PrintToPrinter(printDialog1.PrinterSettings.Copies, false, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);
                    }
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
            }
        }
     */
