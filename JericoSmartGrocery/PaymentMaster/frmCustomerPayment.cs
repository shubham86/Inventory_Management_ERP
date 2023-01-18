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

namespace JericoSmartGrocery.PaymentMaster
{
    public partial class frmCustomerPayment : Form
    {
        public string ID;
        public string CustName;
        public decimal balanceAmt;
        public decimal advanceAmt;
        public int salePaymentID;

        public frmCustomerPayment()
        {
            InitializeComponent();
        }

        private void frmCustomerPayment_Load(object sender, EventArgs e)
        {
            lblName.Text = CustName;
            txtPrevAdvance.Text = advanceAmt.ToString();
            txtPrevBalance.Text = balanceAmt.ToString();
            txtAdvanceAmt.Text = advanceAmt.ToString();
            txtBalanceAmt.Text = balanceAmt.ToString();
            
            txtPaidAmt.Enter += new EventHandler(txtPaidAmt_Focus);
            txtDiscount.Enter += new EventHandler(txtDiscount_Focus);
            txtRefrenceNo.Enter += new EventHandler(txtRefrenceNo_Focus);
            cmbPayMode.SelectedIndex = 0;
            
            this.dtpDate.ValueChanged -= new EventHandler(dtpDate_ValueChanged);
            dtpDate.Value = DateTime.Now;
            dtpDate.MaxDate = DateTime.Now;
            this.dtpDate.ValueChanged += new EventHandler(dtpDate_ValueChanged);

            LoadTotalSale();
            txtPaidAmt.Focus();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            dtpDate.MaxDate = DateTime.Now;
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

        private void LoadTotalSale()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Customer_FetchTotlaSale";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("custID", Convert.ToInt32(ID == "" ? "0" : ID));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    lblTotalSaleAmt.Text = string.Format("{0:0.00}", DT.Rows[0]["totalSaleAmt"].ToString());
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
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
            }
        }

        private Boolean addPayment()
        {
            Boolean retVal = false;
            DAL.DAL_hashSale objDAL = new DAL.DAL_hashSale();
            BAL.BAL_hashSale objBAL = new BAL.BAL_hashSale();

            try
            {
                objDAL.customerID = Convert.ToInt32(ID == "" ? "0" : ID);
                objDAL.paymentDate = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd}", dtpDate.Value) + " " + DateTime.Now.ToString("HH:mm:ss"));
                objDAL.payMode = cmbPayMode.Text;
                objDAL.refNumber = txtRefrenceNo.Text;
                objDAL.prevBalance = Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text);
                objDAL.balanceAmount = Convert.ToDecimal(txtBalanceAmt.Text == "" ? "0.00" : txtBalanceAmt.Text);
                objDAL.customerAdvanceAmount = Convert.ToDecimal(txtAdvanceAmt.Text == "" ? "0.00" : txtAdvanceAmt.Text);
                objDAL.prevAdvanceAmount = Convert.ToDecimal(txtPrevAdvance.Text == "" ? "0.00" : txtPrevAdvance.Text);
                objDAL.nowPaidAmount = Convert.ToDecimal(txtPaidAmt.Text == "" ? "0.00" : txtPaidAmt.Text);
                objDAL.discount = Convert.ToDecimal(txtDiscount.Text == "" ? "0.00" : txtDiscount.Text);
                objDAL.isSuccesful = cmbPayMode.Text == "Cash" ? true : false;

                int x = objBAL.addCustomerPayment(objDAL);
                if (x != 0)
                {
                    salePaymentID = x;
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
                txtRefrenceNo.Focus();
                lblRefNoError.Visible = true;
                return;
            }
            else
            {
                if (addPayment())
                {
                    txtPaidAmt.Text = "0.00";
                    txtRefrenceNo.Text = "";
                    txtDiscount.Text = "0.00";
                    cmbPayMode.SelectedIndex = 0;
                    successAlert("Payment Save Successfully.");
                    printInvoice();

                    Close();
                }
                else
                {
                    dangerAlert("Payment not Save !");
                    txtPaidAmt.Focus();
                }
            }
        }

        private void printInvoice()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataSet DS = new DataSet();
            crReports.rptCustomerPaymentInvoice rptCustomerPaymentInvoice = new crReports.rptCustomerPaymentInvoice();
            
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
                comm.CommandText = "select saleBillNumber, nowPaidAmount, balanceAmount, prevBalance, prevAdvanceAmount, customerAdvanceAmount, discountAmount from tb_salespaymentdetails where salePaymentID = '" + salePaymentID + "' and transactiontype = 'Customer Payment';";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_salespaymentdetails");

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select customerName from tb_customerdetails where customerID = '" + ID + "'";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_customerdetails");

                comm.CommandType = CommandType.Text;
                comm.CommandText = "Insert into tb_tempreport (date, time) values((SELECT DATE_FORMAT(paymentDate,'%d-%m-%y') FROM tb_salespaymentdetails where salePaymentID ='"+ salePaymentID + "'), (SELECT time_format(paymentDate,'%h:%i %p') FROM tb_salespaymentdetails where salePaymentID ='" + salePaymentID +"'))";
                comm.ExecuteNonQuery();

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select * from tb_tempreport";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_tempreport");


                rptCustomerPaymentInvoice.SetDataSource(DS);

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDialog1.AllowSomePages = true;
                    rptCustomerPaymentInvoice.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                    rptCustomerPaymentInvoice.PrintToPrinter(printDialog1.PrinterSettings.Copies, false, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);
                }

                comm.CommandType = CommandType.Text;
                comm.CommandText = "truncate tb_tempreport";
                comm.ExecuteNonQuery();
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

                rptCustomerPaymentInvoice.Close();
                rptCustomerPaymentInvoice.Dispose();
                comm.Dispose();
                conn.Dispose();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtPaidAmt.TextChanged -= new EventHandler(txtPaidAmt_TextChanged);
            txtPaidAmt.Text = "0.00";
            this.txtPaidAmt.TextChanged += new EventHandler(txtPaidAmt_TextChanged);
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
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";

            calculatAdvanceAndBalance();
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
            else if(Convert.ToDecimal(txtPrevBalance.Text == "" ? "0.00" : txtPrevBalance.Text) <= 0)
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
