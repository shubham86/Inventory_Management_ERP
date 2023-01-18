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

namespace JericoSmartGrocery.PaymentMaster
{
    public partial class frmSupplierPayment : Form
    {
        public string ID;
        public string SuppName;
        public decimal balanceAmt;
        public decimal advanceAmt;        

        public frmSupplierPayment()
        {
            InitializeComponent();
        }

        private void frmSupplierPayment_Load(object sender, EventArgs e)
        {
            lblName.Text = SuppName;
            txtPrevAdvance.Text = advanceAmt.ToString();
            txtPrevBalance.Text = balanceAmt.ToString();
            txtAdvanceAmt.Text = advanceAmt.ToString();
            txtBalanceAmt.Text = balanceAmt.ToString();

            txtPaidAmt.Enter += new EventHandler(txtPaidAmt_Focus);
            txtDiscount.Enter += new EventHandler(txtDiscount_Focus);
            txtRefrenceNo.Enter += new EventHandler(txtRefrenceNo_Focus);
            cmbPayMode.SelectedIndex = 0;
        }
        
        protected void txtPaidAmt_Focus(Object sender, EventArgs e)
        {
            lblPaidAmtError.Visible = false;
            txtPaidAmt.SelectAll();
        }

        protected void txtDiscount_Focus(Object sender, EventArgs e)
        {
            txtDiscount.SelectAll();
        }

        protected void txtRefrenceNo_Focus(Object sender, EventArgs e)
        {
            lblRefNoError.Visible = false;
        }

        private Boolean addPayment()
        {
            Boolean retVal = false;
            DAL.DAL_hashPurchase objDAL = new DAL.DAL_hashPurchase();
            BAL.BAL_hashPurchase objBAL = new BAL.BAL_hashPurchase();

            try
            {
                objDAL.supplierID = Convert.ToInt32(ID);
                objDAL.paymentDate = DateTime.Now;
                objDAL.payMode = cmbPayMode.Text;
                objDAL.refNumber = txtRefrenceNo.Text;
                objDAL.prevBalance = Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text);
                objDAL.balanceAmount = Convert.ToDecimal(txtBalanceAmt.Text == "" ? "0.00" : txtBalanceAmt.Text);
                objDAL.supplierAdvanceAmount = Convert.ToDecimal(txtAdvanceAmt.Text == "" ? "0.00" : txtAdvanceAmt.Text);
                objDAL.prevAdvanceAmount = Convert.ToDecimal(txtPrevAdvance.Text == "" ? "0.00" : txtPrevAdvance.Text);
                objDAL.nowPaidAmount = Convert.ToDecimal(txtPaidAmt.Text == "" ? "0.00" : txtPaidAmt.Text);
                objDAL.discount = Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text);
                objDAL.isSuccesful = cmbPayMode.Text == "Cheque" ? false : true;

                if (objBAL.addSupplierPayment(objDAL))
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPaidAmt.Text == "0.00")
            {
                lblPaidAmtError.Visible = true;
                txtPaidAmt.Focus();
                return;
            }
            else if (cmbPayMode.SelectedIndex == 1 && txtRefrenceNo.Text == "")
            {
                lblRefNoError.Visible = true;
                txtRefrenceNo.Focus();
                return;
            }
            else
            {
                if (addPayment())
                {
                    txtPaidAmt.Text = "0.00";
                    txtRefrenceNo.Text = "";
                    cmbPayMode.SelectedIndex = 0;
                    successAlert("Payment Save Successfully.");
                    btnExit.Focus();
                }
                else
                {
                    dangerAlert("Payment not Save !");
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtPaidAmt.Text = "0.00";
            txtDiscount.Text = "0.00";
            txtRefrenceNo.Text = "";
            cmbPayMode.SelectedIndex = 0;
            lblPaidAmtError.Visible = false;
            calculatAdvanceAndBalance();

            txtPaidAmt.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
               
        private void txtPaidAmt_TextChanged(object sender, EventArgs e)
        {
        }

        private void calculatAdvanceAndBalance()
        {
            if (Convert.ToDecimal(txtPrevAdvance.Text) > 0)
            {
                txtAdvanceAmt.Text = (Convert.ToDecimal(txtPaidAmt.Text == "" ? "0.00" : txtPaidAmt.Text) + Convert.ToDecimal(txtPrevAdvance.Text == "" ? "0.00" : txtPrevAdvance.Text)).ToString();
                txtBalanceAmt.Text = "0.00";
            }
            else if (Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text) > 0 && Convert.ToDecimal(txtPaidAmt.Text == "" ? "0.00" : txtPaidAmt.Text) >= Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text))
            {
                txtAdvanceAmt.Text = (Convert.ToDecimal(txtPaidAmt.Text == "" ? "0.00" : txtPaidAmt.Text) - Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text) + Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text)).ToString();
                txtBalanceAmt.Text = "0.00";
            }
            else if (Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text) > 0 && Convert.ToDecimal(txtPaidAmt.Text == "" ? "0.00" : txtPaidAmt.Text) < Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text) && Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text) <= Convert.ToDecimal(txtBalanceAmt.Text == "" ? "0.00" : txtBalanceAmt.Text))
            {
                txtBalanceAmt.Text = (Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text) - Convert.ToDecimal(txtPaidAmt.Text == "" ? "0.00" : txtPaidAmt.Text) - Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text)).ToString();
                txtAdvanceAmt.Text = "0.00";
            }
            else if (Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text) > 0 && Convert.ToDecimal(txtPaidAmt.Text == "" ? "0.00" : txtPaidAmt.Text) < Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text) && Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text) > Convert.ToDecimal(txtBalanceAmt.Text == "" ? "0.00" : txtBalanceAmt.Text))
            {
                txtAdvanceAmt.Text = Math.Abs((Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text) - Convert.ToDecimal(txtPaidAmt.Text == "" ? "0.00" : txtPaidAmt.Text) - Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text))).ToString();
                txtBalanceAmt.Text = "0.00";
            }
            else if (Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text) <= 0)
            {
                txtAdvanceAmt.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtPaidAmt.Text == "" ? "0.00" : txtPaidAmt.Text));
                txtBalanceAmt.Text = "0.00";
            }
        }

        private void txtPaidAmt_Leave(object sender, EventArgs e)
        {
            txtPaidAmt.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtPaidAmt.Text == "" ? "0.00" : txtPaidAmt.Text));
            txtPaidAmt.DeselectAll();
        }

        private void cmbPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPayMode.SelectedIndex != 1)
            {
                lblRefNoError.Visible = false;
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
                txtPaidAmt.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtPaidAmt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtPaidAmt_TextChanged_1(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";

            calculatAdvanceAndBalance();
        }

        private void txtPaidAmt_Click(object sender, EventArgs e)
        {
            txtPaidAmt.SelectAll();
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

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtDiscount_Click(object sender, EventArgs e)
        {
            txtDiscount.SelectAll();
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {

            if (Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text) <= Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text))
            {
                calculatAdvanceAndBalance();
            }
            else
            {
                MessageBox.Show("Discount should be less than balance amount.", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDiscount.Text = "0.00";
                txtDiscount.Focus();
            }

            txtDiscount.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text));
            txtDiscount.DeselectAll();
        }
    }
}
