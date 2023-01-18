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
    public partial class frmPurchaseDetails : Form
    {
        public string productName;
        public string productID;
        public string purchaseQty;
        public string purchaseRate;
        public string gst;
        public string purchaseBrand;
        public string supplierID;
        public string supplierAdvance;
        public string purchaseBillNumber;
        decimal returnProductCount;

        public frmPurchaseDetails()
        {
            InitializeComponent();
        }

        private void frmPurchaseDetails_Load(object sender, EventArgs e)
        {
            lblBrandName.Text = purchaseBrand;
            lblProductName.Text = productName;
            lblID.Text = productID;
            txtPurchaseRate.Text = purchaseRate;
            txtPurQty.Text = purchaseQty;
            lblPurQty.Text = purchaseQty;
            lblSuppID.Text = supplierID;
            dtpDate.Value = DateTime.Now;
            txtReturnQty.Focus();

            loadData();
        }

        private void calculations()
        {
            Decimal gstAmount;
            Decimal purchaseReturnRate = Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0.00" : txtPurchaseRate.Text);
            Decimal returnQuantity = Convert.ToDecimal(txtReturnQty.Text == "" ? "0.00" : txtReturnQty.Text);
            Decimal gst = Convert.ToDecimal(lblgst.Text);
            Decimal previousBalance = Convert.ToDecimal(lblPrevBalance.Text);
            Decimal paidAmount = Convert.ToDecimal(txtPaidAmount.Text == "" ? "0.00" : txtPaidAmount.Text == "." ? "0.00" : txtPaidAmount.Text);
            Decimal totalPrice;
            Decimal currentBalance;
            Decimal X;
            Decimal advanceAmount = Convert.ToDecimal(lblPrevAdvance.Text);

            // GST calculation 
            gstAmount = ((purchaseReturnRate) * (returnQuantity)) * gst / 100;
            txtgstAmount.Text = string.Format("{0:0.00}", (gstAmount));

            totalPrice = (purchaseReturnRate) * (returnQuantity);
            txtTotalPrice.Text = string.Format("{0:0.00}", (totalPrice));

            currentBalance = (previousBalance) - (totalPrice);
            txtBalanceAmount.Text = string.Format("{0:0.00}", (currentBalance));

            if (advanceAmount > 0)
            {
                X = (advanceAmount + totalPrice) - paidAmount;

                if (X < 0)
                {
                    txtBalanceAmount.Text = string.Format("{0:0.00}", Math.Abs(X));
                    txtAdvanceAmount.Text = "0.00";
                }
                else
                {
                    txtAdvanceAmount.Text = string.Format("{0:0.00}", (X));
                    txtBalanceAmount.Text = "0.00";
                }
            }
            else
            {
                X = (previousBalance - totalPrice) + paidAmount;

                if (X < 0)
                {
                    txtAdvanceAmount.Text = string.Format("{0:0.00}", Math.Abs(X));
                    txtBalanceAmount.Text = "0.00";
                }
                else
                {
                    txtAdvanceAmount.Text = "0.00";
                    txtBalanceAmount.Text = string.Format("{0:0.00}", X);
                }
            }

        }

        private void loadData()
        {
            //Load GST
            {
                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
                MySqlCommand comm = new MySqlCommand();
                MySqlDataAdapter DA = new MySqlDataAdapter();
                DataSet DS = new DataSet();

                try
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "Proc_FetchingGST_PurRet";

                    comm.Connection = conn;

                    comm.Parameters.AddWithValue("prodID", productID);
                    comm.Parameters.AddWithValue("suppID", supplierID);
                    comm.Parameters.AddWithValue("billNo", purchaseBillNumber);

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    DA = new MySqlDataAdapter(comm);
                    DA.Fill(DS);

                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        lblgst.Text = DS.Tables[0].Rows[0]["gst"].ToString();
                        lblUnit1.Text = DS.Tables[0].Rows[0]["unit"].ToString();
                    }

                    if (DS.Tables[1].Rows.Count > 0)
                    {
                        lblSupplierName.Text = DS.Tables[1].Rows[0]["supplierName"].ToString();
                        lblPrevBalance.Text = string.Format("{0:0.00}", (DS.Tables[1].Rows[0]["balanceAmount"].ToString()));
                        lblPrevAdvance.Text = string.Format("{0:0.00}", (DS.Tables[1].Rows[0]["supplierAdvanceAmount"].ToString()));
                    }

                    if (DS.Tables[2].Rows.Count > 0)
                    {
                        returnProductCount = Convert.ToDecimal(DS.Tables[2].Rows[0]["returnProductCount"]);
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
            }
        }

        private decimal fetchNowStock()
        {
            //Load Now Stock
            decimal nowStock = 0;
            {
                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
                MySqlCommand comm = new MySqlCommand();
                MySqlDataAdapter DA = new MySqlDataAdapter();
                DataTable DT = new DataTable();

                try
                {
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "Proc_Stock_FetchByID";

                    comm.Connection = conn;

                    comm.Parameters.AddWithValue("prodID", productID);

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    DA = new MySqlDataAdapter(comm);
                    DA.Fill(DT);

                    if (DT.Rows.Count > 0)
                    {
                        nowStock = Convert.ToDecimal(DT.Rows[0]["nowStock"]);
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
                return nowStock;

            }
        }

        //private int fetchReturnCount()
        //{
        //    {
        //        MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
        //        MySqlCommand comm = new MySqlCommand();
        //        MySqlDataAdapter DA = new MySqlDataAdapter();
        //        DataTable DT = new DataTable();
        //        int productCount = 0;
        //        try
        //        {
        //            comm.CommandType = CommandType.StoredProcedure;
        //            comm.CommandText = "Proc_ReturnPurchase_returnProductCount";

        //            comm.Connection = conn;

        //            comm.Parameters.AddWithValue("prodID", productID);
        //            comm.Parameters.AddWithValue("suppID", supplierID);

        //            if (conn.State == ConnectionState.Closed)
        //            {
        //                conn.Open();
        //            }

        //            DA = new MySqlDataAdapter(comm);
        //            DA.Fill(DT);

        //            if (DT.Rows.Count > 0)
        //            {
        //                productCount = Convert.ToInt32(DT.Rows[0]["gst"]);
        //            }
        //        }

        //        catch (Exception ex)
        //        {
        //            string x = ex.ToString();
        //        }

        //        finally
        //        {
        //            if (conn.State == ConnectionState.Open)
        //            {
        //                conn.Close();
        //            }

        //            comm.Dispose();
        //            conn.Dispose();
        //        }

        //        return productCount;
        //    }
        //}

        private void clearForm()
        {
            loadData();
            txtPaidAmount.Text = "0.00";
            txtReturnQty.Text = "0.00";

            txtPurQty.Focus();
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
            //DialogResult DR = MessageBox.Show("Are You Sure You Want To Exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (DR == DialogResult.Yes)
            //{
                this.Close();
            //}
        }

        private void txtReturnQty_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
            lblQtyError.Visible = false;
        }

        private void txtReturnQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (Convert.ToDecimal(txtReturnQty.Text) > Convert.ToDecimal(lblPurQty.Text))
            {
                lblAlert.Location = new Point(10, 10);
                dangerAlert("Return Qty Should be less than Purchase Qty.");
                txtReturnQty.Focus();
            }
            else if (txtReturnQty.Text == "" || Convert.ToDecimal(txtReturnQty.Text) == 0)
            {
                lblAlert.Location = new Point(60, 10);
                dangerAlert("Kindly Enter Return Quantity.");
                txtReturnQty.Focus();
            }
            else if (fetchNowStock() < Convert.ToDecimal(txtReturnQty.Text))
            {
                lblAlert.Location = new Point(50, 10);
                dangerAlert("Return Qty Greater than Stock Qty");
                txtReturnQty.Focus();
            }
            else if ((lblSuppID.Text=="" || lblSuppID.Text == "0") && (Convert.ToDecimal(txtPaidAmount.Text == "" ? "0.00" : txtPaidAmount.Text == "." ? "0.00" : txtPaidAmount.Text) < Convert.ToDecimal(txtBalanceAmount.Text)))
            {
                lblAlert.Location = new Point(60, 10);
                dangerAlert("Enter Valid Paid Amount");
                txtPaidAmount.Focus();
            }
            else
            {
                DialogResult DR = MessageBox.Show("Are You Sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DR == DialogResult.Yes)
                {
                    DAL.DAL_purchaseReturnEntry objDAL = new DAL.DAL_purchaseReturnEntry();
                    BAL.BAL_purchaseReturnEntry objBAL = new BAL.BAL_purchaseReturnEntry();
                    try
                    {
                        objDAL.nowStock = fetchNowStock();
                        objDAL.suppID = Convert.ToInt32(lblSuppID.Text);
                        objDAL.prodID = Convert.ToInt32(lblID.Text);
                        objDAL.billNumber = Convert.ToInt32(purchaseBillNumber) + 1;
                        objDAL.paymentDate = dtpDate.Value;
                        objDAL.paidAmount = Convert.ToDecimal(txtPaidAmount.Text == "" ? "0.00" : txtPaidAmount.Text == "." ? "0.00" : txtPaidAmount.Text);
                        objDAL.totalPrice = Convert.ToDecimal(txtTotalPrice.Text);
                        objDAL.balanceAmount = Convert.ToDecimal(txtBalanceAmount.Text);
                        objDAL.currentAdvance = Convert.ToDecimal(txtAdvanceAmount.Text);
                        objDAL.previousAdvance = Convert.ToDecimal(lblPrevAdvance.Text);
                        objDAL.purchaseBillNumber = Convert.ToInt32(purchaseBillNumber);
                        objDAL.retQty = Convert.ToDecimal(txtReturnQty.Text);
                        objDAL.gst = Convert.ToDecimal(txtgstAmount.Text);
                        objDAL.prevBalance = Convert.ToDecimal(lblPrevBalance.Text);

                        if (objBAL.addPurchaseReturnEntry(objDAL))
                        {
                            lblAlert.Location = new Point(60, 10);
                            successAlert("Product Return Successfully.");
                            Close();
                        }
                        else
                        {
                            lblAlert.Location = new Point(70, 10);
                            dangerAlert("Error! Please try again.");
                        }
                    }
                    catch (Exception ex)
                    {
                        string x = ex.ToString();
                        MessageBox.Show(x);
                    }
                }                
            }    
        }
    
        private void txtReturnQty_Leave(object sender, EventArgs e)
        {
            if ((returnProductCount + Convert.ToDecimal(txtReturnQty.Text)) > Convert.ToDecimal(purchaseQty))
            {
                MessageBox.Show( returnProductCount + " Products are already returned.", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReturnQty.Text = "0.00";
                txtReturnQty.Focus();
            }
            else
            {
                calculations();
            }            
            txtReturnQty.Text= string.Format("{0:0.00}", Convert.ToDecimal(txtReturnQty.Text == "" ? "0.00" : txtReturnQty.Text));
            txtReturnQty.DeselectAll();
        }

        private void txtPaidAmount_Leave(object sender, EventArgs e)
        {
            txtPaidAmount.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtPaidAmount.Text == "" ? "0.00" : txtPaidAmount.Text));
            txtPaidAmount.DeselectAll();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDate.MaxDate = DateTime.Now;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //DialogResult DR = MessageBox.Show("Reset will clear the data. Continue?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if(DR==DialogResult.Yes)
            //{
                clearForm();
            //}
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
            calculations();
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
                txtReturnQty.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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

        private void txtReturnQty_Click(object sender, EventArgs e)
        {
            txtReturnQty.SelectAll();
        }

        private void txtPaidAmount_Click(object sender, EventArgs e)
        {
            txtPaidAmount.SelectAll();
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
    }
}

