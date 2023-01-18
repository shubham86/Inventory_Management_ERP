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
    public partial class frmPurchasePopUp : Form
    {
        public string productID;
        public string productName;
        public string brand;
        public Decimal Less5;
        public Decimal _5to10;
        public Decimal Greater10;
        public string prevPurchasePrice;
        public string gst;
        public string unit;
        public int gstID;
        public int unitID;
        public int brandID;
        public Decimal currentStock;
        master.frmSaleRatePopUp frmSaleRatePopUp = new master.frmSaleRatePopUp();

        public frmPurchasePopUp()
        {
            InitializeComponent();
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
            txtPurchaseRate.Text = prevPurchasePrice;
            txtSaleRate.Text = Less5 + " / " + _5to10 + " / " + Greater10;
            lblPrevPurchaseRate.Text = "( " + prevPurchasePrice + " )";
            lblPrevSaleRate.Text = "( " + Less5 + " / " + _5to10 + " / " + Greater10 + " )";
            lblgst.Text = gst + " %";
            frmSaleRatePopUp.Less5 = Less5;
            frmSaleRatePopUp._5to10 = _5to10;
            frmSaleRatePopUp.Greater10 = Greater10;
        }

        private void frmPurchasePopUp_Load(object sender, EventArgs e)
        {
            initializeVariables();
            txtQty.SelectAll();
            this.txtPurchaseRate.Enter += new EventHandler(txtPurRate_Focus);
            this.txtSaleRate.Enter += new EventHandler(txtSaleRate_Focus);
            this.txtgstAmount.Enter += new EventHandler(txtgstAmount_Focus);
            this.txtSaleRate.Enter += new EventHandler(txtSlaeRate_Focus);
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
                comm.CommandText = "Proc_PurchaseHash_Addvalidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("id", lblID.Text);

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

        private void addHashPurchase()
        {
            DAL.DAL_hashPurchase objDAL = new DAL.DAL_hashPurchase();
            BAL.BAL_hashPurchase objBAL = new BAL.BAL_hashPurchase();

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
                objDAL.qty = Convert.ToDecimal(txtQty.Text);
                objDAL.purchaseRate = Convert.ToDecimal(txtPurchaseRate.Text);
                objDAL.rateLess5 = frmSaleRatePopUp.Less5;
                objDAL.rate5to10 = frmSaleRatePopUp._5to10;
                objDAL.rateGreater10 = frmSaleRatePopUp.Greater10;
                objDAL.totalprice = Convert.ToDecimal(txtTotalPrice.Text);
                objDAL.currentStock = Convert.ToDecimal(currentStock);

                if (objBAL.addHashPurchase(objDAL))
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

        private void clearForm()
        {
            txtQty.Text = "0.00";
            txtPurchaseRate.Text = prevPurchasePrice;
            txtSaleRate.Text = Less5 + "/" + _5to10 + "/" + Greater10;
            txtTotalPrice.Text = "0.00";

            lblQtyError.Visible = false;
            lblPRateError.Visible = false;
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

        protected void txtSlaeRate_Focus(Object sender, EventArgs e)
        {
            Opacity = .50;
            frmSaleRatePopUp.ShowDialog();
            Opacity = 1;

            txtSaleRate.Text = frmSaleRatePopUp.Less5 + " / " + frmSaleRatePopUp._5to10 + " / " + frmSaleRatePopUp.Greater10;
        }

        protected void txtPurRate_Focus(Object sender, EventArgs e)
        {
            txtPurchaseRate.SelectAll();
        }

        protected void txtSaleRate_Focus(Object sender, EventArgs e)
        {
            txtSaleRate.SelectAll();
        }

        protected void txtgstAmount_Focus(Object sender, EventArgs e)
        {
            txtgstAmount.SelectAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            txtQty.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtQty.Text));
            txtQty.DeselectAll();
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

        private void txtPurchaseRate_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPurchaseRate_Leave(object sender, EventArgs e)
        {
            txtPurchaseRate.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtPurchaseRate.Text));
            txtTotalPrice.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtPurchaseRate.Text)).ToString("0.00");
            calculate();
            txtPurchaseRate.DeselectAll();
        }

        private void txtSaleRate_Leave(object sender, EventArgs e)
        {
            txtSaleRate.DeselectAll();
        }

        private void txtTotalPrice_Leave(object sender, EventArgs e)
        {
            txtTotalPrice.DeselectAll();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearForm();
        }
        private void saveButtonClick()
        {
            if (Convert.ToDecimal(txtQty.Text == "" ? "0.00" : txtQty.Text) == Convert.ToDecimal(0))
            {
                lblQtyError.Visible = true;
                txtQty.Focus();
            }
            else if (Convert.ToDecimal(txtPurchaseRate.Text) > Convert.ToDecimal(frmSaleRatePopUp.Less5) && Convert.ToDecimal(txtPurchaseRate.Text) > Convert.ToDecimal(frmSaleRatePopUp._5to10) && Convert.ToDecimal(txtPurchaseRate.Text) > Convert.ToDecimal(frmSaleRatePopUp.Greater10))
            {
                lblAlert.Location = new Point(10, 10);
                MessageBox.Show("Sale rate must be greater than purchase rate", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSaleRate.Focus();
            }
            else
            {
                string retVal = addValidation();
                if (retVal == "true")
                {
                    addHashPurchase();
                    //frmPurchase frmPurchase = new frmPurchase();
                    //frmPurchase.fromPurchasePopup = true;
                    //frmPurchase.fetchHashPurchase();                    
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

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";

            txtTotalPrice.Text = (Convert.ToDecimal(txtQty.Text == "" ? "0.00" : txtQty.Text) * Convert.ToDecimal(txtPurchaseRate.Text == "" ? "0.00" : txtPurchaseRate.Text)).ToString("0.00");
            calculate();
        }

        private void txtQty_Click(object sender, EventArgs e)
        {
            txtQty.SelectAll();
        }

        private void txtPurchaseRate_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtPurchaseRate_Click(object sender, EventArgs e)
        {
            txtPurchaseRate.SelectAll();
        }

        private void txtSaleRate_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtSaleRate_Click(object sender, EventArgs e)
        {
            txtSaleRate.SelectAll();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                clearForm();
                this.Close();
                return true;
            }
            if (keyData == Keys.F1)
            {
                txtQty.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                saveButtonClick();
                e.Handled = true;
            }
        }
    }
}
