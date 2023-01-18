using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JericoSmartGrocery.PaymentMaster
{
    public partial class frmCustomerCashReturn : Form
    {
        public string ID;
        public string CustName;
        public decimal balanceAmt;
        public decimal advanceAmt;

        public frmCustomerCashReturn()
        {
            InitializeComponent();
        }
        
        private void frmCustomerCashReturn_Load(object sender, EventArgs e)
        {

            this.dtpDate.ValueChanged -= new EventHandler(dtpDate_ValueChanged);
            dtpDate.Value = DateTime.Now;
            dtpDate.MaxDate = DateTime.Now;
            this.dtpDate.ValueChanged += new EventHandler(dtpDate_ValueChanged);

            lblName.Text = CustName;
            txtPrevAdvance.Text = advanceAmt.ToString();
            txtPrevBalance.Text = balanceAmt.ToString();
            txtAdvanceAmt.Text = advanceAmt.ToString();
            txtBalanceAmt.Text = balanceAmt.ToString();

            txtReturnAmt.Enter += new EventHandler(txtReturnAmt_Focus);

            txtReturnAmt.Focus();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDate.MaxDate = DateTime.Now;
        }

        protected void txtReturnAmt_Focus(Object sender, EventArgs e)
        {
            lblPaidAmtError.Visible = false;
            txtReturnAmt.SelectAll();
        }

        private Boolean addCashReturn()
        {
            Boolean retVal = false;
            DAL.DAL_hashSale objDAL = new DAL.DAL_hashSale();
            BAL.BAL_hashSale objBAL = new BAL.BAL_hashSale();

            try
            {
                objDAL.customerID = Convert.ToInt32(ID == "" ? "0" : ID);
                objDAL.paymentDate = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd}", dtpDate.Value));
                objDAL.prevBalance = Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text);
                objDAL.balanceAmount = Convert.ToDecimal(txtBalanceAmt.Text == "" ? "0.00" : txtBalanceAmt.Text);
                objDAL.customerAdvanceAmount = Convert.ToDecimal(txtAdvanceAmt.Text == "" ? "0.00" : txtAdvanceAmt.Text);
                objDAL.prevAdvanceAmount = Convert.ToDecimal(txtPrevAdvance.Text == "" ? "0.00" : txtPrevAdvance.Text);
                objDAL.nowPaidAmount =  Convert.ToDecimal(txtReturnAmt.Text == "" ? "0.00" : txtReturnAmt.Text) * -1;

                if (objBAL.addCustomerReturnCash(objDAL))
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
            if (txtReturnAmt.Text == "0.00")
            {
                lblPaidAmtError.Visible = true;
                txtReturnAmt.Focus();
                return;
            }
            else if (Convert.ToDecimal(txtReturnAmt.Text == "" ? "0.00" : txtReturnAmt.Text) > Convert.ToDecimal(advanceAmt))
            {
                MessageBox.Show("Return amount should be less than advance amount.", "Alert !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReturnAmt.Text = "0.00";
            }
            else
            {
                var confirmResult = MessageBox.Show("Are you sure to save this payment ?", "Confirm Save !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    if (addCashReturn())
                    {
                        successAlert("Payment Save Successfully.");
                        txtReturnAmt.Text = "0.00";
                        Close();
                    }
                    else
                    {
                        dangerAlert("Payment not Save !");
                        txtReturnAmt.Focus();
                    }
                }
                else
                {
                    return;
                }

            }
        }

        private void txtReturnAmt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtReturnAmt_Click(object sender, EventArgs e)
        {
            txtReturnAmt.SelectAll();
        }

        private void txtReturnAmt_Leave(object sender, EventArgs e)
        {
            txtReturnAmt.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtReturnAmt.Text == "" ? "0.00" : txtReturnAmt.Text));
            txtReturnAmt.DeselectAll();
        }

        private void txtReturnAmt_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
            
            txtAdvanceAmt.Text = (Convert.ToDecimal(txtPrevAdvance.Text == "" ? "0.00" : txtPrevAdvance.Text) - Convert.ToDecimal(txtReturnAmt.Text == "" ? "0.00" : txtReturnAmt.Text)).ToString();
            txtBalanceAmt.Text = "0.00";
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtReturnAmt.TextChanged -= new EventHandler(txtReturnAmt_TextChanged);
            txtReturnAmt.Text = "0.00";
            this.txtReturnAmt.TextChanged += new EventHandler(txtReturnAmt_TextChanged);
            lblPaidAmtError.Visible = false;

            txtReturnAmt.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                txtReturnAmt.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
