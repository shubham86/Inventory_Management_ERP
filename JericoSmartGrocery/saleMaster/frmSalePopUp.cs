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
    public partial class frmSalePopUp : Form
    {
        public int billNo;
        public int customerID;
        public string productID;
        public string productName;
        public string brand;
        public decimal salePrice;
        public decimal _5to10;
        public decimal Greater10;
        public string purchasePrice;
        public string gst;
        public string unit;
        public int gstID;
        public int unitID;
        public int brandID;
        public Decimal currentStock;
        public string marathiName;

        public frmSalePopUp()
        {
            InitializeComponent();
        }
        
        private void frmSalePopUp_Load(object sender, EventArgs e)
        {
            initializeVariables();
            txtQty.SelectAll();

            if (Global.userRole == "Operator")
            {
                txtPurchaseRate.Visible = false;
                label5.Visible = false;
                lblUnit2.Visible = false;
            }
            else
            {            
                txtPurchaseRate.Visible = true;
                label5.Visible = true;
                lblUnit2.Visible = true;
            }
        }

        public void initializeVariables()
        {
            txtQty.Select();
            lblBrandName.Text = brand;
            lblUnit1.Text = unit;
            lblUnit2.Text = "/ " + unit;
            lblUnit3.Text = "/ " + unit;
            lblProductName.Text = productName;
            lblID.Text = productID;
            txtSaleRate.Text = salePrice.ToString();
            lblPrevSaleRate.Text = salePrice + " / " + _5to10 + " / " + Greater10 + " ( ₹ )";
            txtPurchaseRate.Text = purchasePrice;
            lblgst.Text = gst + " %";
        }

        private string addValidation()
        {
            string retVal = "";

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_SaleHash_Addvalidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("id", lblID.Text);
                comm.Parameters.AddWithValue("billNumber", billNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    retVal = "false";
                }
                else
                {
                    retVal = "true";
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                retVal = "error";
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

        private void addHashSale()
        {
            DAL.DAL_hashSale objDAL = new DAL.DAL_hashSale();
            BAL.BAL_hashSale objBAL = new BAL.BAL_hashSale();

            try
            {
                objDAL.productID = Convert.ToInt16(lblID.Text);
                objDAL.productName = lblProductName.Text;
                objDAL.brandID = brandID;
                objDAL.brand = lblBrandName.Text;
                objDAL.gstID = gstID;
                objDAL.gst = Convert.ToDecimal(gst);
                objDAL.gstamount = Convert.ToDecimal(txtgstAmount.Text);
                objDAL.unitID = unitID;
                objDAL.unit = unit;
                objDAL.qty = Convert.ToDecimal(txtQty.Text == "" ? "0.000" : txtQty.Text);
                objDAL.purchaseRate = Convert.ToDecimal(txtPurchaseRate.Text);
                objDAL.saleRate = Convert.ToDecimal(txtSaleRate.Text == "" ? "0.00" : txtSaleRate.Text);
                objDAL.totalprice = Convert.ToDecimal(txtTotalPrice.Text);
                objDAL.currentStock = Convert.ToDecimal(currentStock);
                objDAL.gstOnMargine = Convert.ToDecimal(GSTonMargin());
                objDAL.marathiName = marathiName;
                objDAL.billNo = billNo;
                objDAL.customerID = customerID;

                if (objBAL.addHashSale(objDAL))
                {
                    clearForm();
                    lblAlert.Location = new Point(75, 10);
                    successAlert("Product Add successfully.");
                }
                else
                {
                    lblAlert.Location = new Point(75, 10);
                    dangerAlert("Error! Please Try Again.");
                }
            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                lblAlert.Location = new Point(75, 10);
                dangerAlert("Error! Please Try Again.");
                MessageBox.Show(x);
            }
        }


        public void calculate()
        {
            txtgstAmount.Text = (Convert.ToDecimal(txtTotalPrice.Text) * (Convert.ToDecimal(gst) / 100)).ToString("0.00");
        }

        private string GSTonMargin()
        {
            string Mgst = "0.00";
            Decimal totalPurchaseAmt = Convert.ToDecimal(txtPurchaseRate.Text) * Convert.ToDecimal(txtQty.Text == "" ? "0.000" : txtQty.Text);
            Mgst = ((Convert.ToDecimal(txtTotalPrice.Text) * (Convert.ToDecimal(gst) / 100)) - (totalPurchaseAmt * (Convert.ToDecimal(gst) / 100))).ToString("0.00");
            return Mgst;
        }

        private void clearForm()
        {
            txtQty.Text = "0.000";
            txtSaleRate.Text = salePrice.ToString();
            txtPurchaseRate.Text = purchasePrice;
            txtTotalPrice.Text = "0.00";

            lblQtyError.Visible = false;
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

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
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
            clearForm();
        }

        private void saveButtonClick()
        {
            if (Convert.ToDecimal(txtQty.Text == "" ? "0.000" : txtQty.Text) == Convert.ToDecimal(0))
            {
                lblQtyError.Visible = true;
                txtQty.Focus();
            }
            else if (currentStock < Convert.ToDecimal(txtQty.Text == "" ? "0.000" : txtQty.Text))
            {
                MessageBox.Show("Only " + currentStock + " " + unit + " available.", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQty.Focus();
            }
            else
            {
                string retVal = addValidation();
                if (retVal == "true")
                {
                    addHashSale();
                    Close();
                }
                else if (retVal == "false")
                {
                    lblAlert.Location = new Point(75, 10);
                    warningAlert("Product is Allready Exist.");
                }
                else
                {
                    lblAlert.Location = new Point(75, 10);
                    dangerAlert("Error! Please Try Again.");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveButtonClick();
        }

        private void txtSaleRate_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";

            txtTotalPrice.Text = (Convert.ToDecimal(txtQty.Text == "" ? "0.000" : txtQty.Text) * Convert.ToDecimal(txtSaleRate.Text == "" ? "0.00" : txtSaleRate.Text)).ToString("0.00");
            calculate();
        }

        private void txtSaleRate_Leave(object sender, EventArgs e)
        {
            txtSaleRate.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtSaleRate.Text == "" ? "0.00" : txtSaleRate.Text));
            txtSaleRate.DeselectAll();
        }

        private void txtPurchaseRate_Leave(object sender, EventArgs e)
        {
            txtPurchaseRate.DeselectAll();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                clearForm();
                this.Close();
                return true;
            }
            else if (keyData == Keys.F1)
            {
                txtQty.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";

            if (Convert.ToDecimal(txtQty.Text == "" ? "0.000" : txtQty.Text) <= 5)
            {
                txtSaleRate.Text = salePrice.ToString();
            }
            else if (Convert.ToDecimal(txtQty.Text == "" ? "0.000" : txtQty.Text) > 5 && Convert.ToDecimal(txtQty.Text == "" ? "0.000" : txtQty.Text) <= 10)
            {
                txtSaleRate.Text = _5to10.ToString();
            }
            else if (Convert.ToDecimal(txtQty.Text == "" ? "0.000" : txtQty.Text) > 10)
            {
                txtSaleRate.Text = Greater10.ToString();
            }
            
            txtTotalPrice.Text = (Convert.ToDecimal(txtQty.Text == "" ? "0.000" : txtQty.Text) * Convert.ToDecimal(txtSaleRate.Text == "" ? "0.00" : txtSaleRate.Text)).ToString("0.00");
            calculate();            
        }

        private void txtPurchaseRate_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            txtQty.Text = string.Format("{0:0.000}", Convert.ToDecimal(txtQty.Text == "" ? "0.000" : txtQty.Text));
            txtQty.DeselectAll();
        }

        private void txtQty_Click(object sender, EventArgs e)
        {
            txtQty.SelectAll();
        }

        private void txtSaleRate_Click(object sender, EventArgs e)
        {
            txtSaleRate.SelectAll();
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                saveButtonClick();
                e.Handled = true;
            }
        }

        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtTotalPrice_Leave(object sender, EventArgs e)
        {
            decimal amt = Convert.ToDecimal(txtTotalPrice.Text == "" ? "0.00" : txtTotalPrice.Text);

            if (salePrice != 0 && salePrice.ToString() != "0.00" && txtQty.Text == "0.000" || txtQty.Text == "0" )
            {
                this.txtTotalPrice.TextChanged -= new EventHandler(txtTotalPrice_TextChanged);
                txtQty.Text = (Convert.ToDecimal(txtTotalPrice.Text == "" ? "0.00" : txtTotalPrice.Text) / salePrice).ToString("0.000");
                this.txtTotalPrice.TextChanged += new EventHandler(txtTotalPrice_TextChanged);

            }

            txtTotalPrice.Text = amt.ToString();
            txtTotalPrice.DeselectAll();
        }

        private void txtTotalPrice_Click(object sender, EventArgs e)
        {
            txtTotalPrice.SelectAll();
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

        private void txtTotalPrice_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
