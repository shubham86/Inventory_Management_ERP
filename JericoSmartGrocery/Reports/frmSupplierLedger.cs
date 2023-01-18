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

namespace JericoSmartGrocery.Reports
{
    public partial class frmSupplierLedger : Form
    {
        public string ID;
        public string CustName;
        public decimal balanceAmt;
        public decimal advanceAmt;

        public frmSupplierLedger()
        {
            InitializeComponent();
        }

        private void frmSupplierLedger_Load(object sender, EventArgs e)
        {
            lblName.Text = CustName;
            lblBalAmt.Text = balanceAmt.ToString();
            lblAdvAmt.Text = advanceAmt.ToString();

            this.dtpTo.ValueChanged -= new EventHandler(dtpTo_ValueChanged);
            dtpTo.Value = DateTime.Now;
            dtpTo.MaxDate = DateTime.Now;
            this.dtpTo.ValueChanged += new EventHandler(dtpTo_ValueChanged);
            this.dtpFrom.ValueChanged -= new EventHandler(dtpFrom_ValueChanged);
            int year = DateTime.Now.Year;
            DateTime fromDay = new DateTime(year, 1, 1);
            dtpFrom.Value = fromDay;
            dtpFrom.MaxDate = DateTime.Now;
            this.dtpFrom.ValueChanged += new EventHandler(dtpFrom_ValueChanged);

            LoadDatagrid();
        }

        private void LoadDatagrid()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataSet DS = new DataSet();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Supplier_PaymentReport_FetchinGridview";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("suppID", ID);
                comm.Parameters.AddWithValue("fromDate", dtpFrom.Value);
                comm.Parameters.AddWithValue("toDate", dtpTo.Value);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS);

                if (DS.Tables[0].Rows.Count >= 0)
                {
                    dgvPaymentDetails.DataSource = DS.Tables[0];

                    int i = 1;
                    foreach (DataGridViewRow row in dgvPaymentDetails.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;
                    }
                }

                if (DS.Tables[1].Rows.Count >= 0)
                {
                    lblTotalPurchaseAmt.Text= string.Format("{0:0.00}", DS.Tables[1].Rows[0]["totalPrurchaseAmt"].ToString()) =="" ? "0.00" : string.Format("{0:0.00}", DS.Tables[1].Rows[0]["totalPrurchaseAmt"].ToString());
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

                dgvPaymentDetails.ClearSelection();
            }
        }

        private void printReport()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataSet DS = new DataSet();
            crReports.rptSupplierLedger rptSupplierLedger = new crReports.rptSupplierLedger();

            try
            {
                comm.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select shopName,contactOne,contactTwo,shopAddress from tb_aboutshop;";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_aboutshop");
                               
                comm.CommandType = CommandType.Text;
                comm.CommandText = "Insert into tb_tempreport (fromDate, toDate, customerName, balanceAmt, advanceAmt) values('" + dtpFrom.Value.ToString("dd/MM/yyyy") + "', '" + dtpTo.Value.ToString("dd/MM/yyyy") + "','" + CustName + "'," + balanceAmt + "," + advanceAmt + ");";
                comm.ExecuteNonQuery();

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select * from tb_tempreport";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_tempreport");

                comm.CommandType = CommandType.Text;
                comm.CommandText = "SELECT purchaseBillNumber, transactiontype, date_format(paymentDate, '%d-%m-%Y') AS paymentDate, (billAmount + otherCharges - discountAmount) as grandTotalAmount, nowPaidAmount, supplierAdvanceAmount, balanceAmount FROM tb_purchasepaymentdetails WHERE supplierID ='" + ID + "'and CAST(paymentDate AS DATE) between CAST('" + dtpFrom.Value.ToString("yyyy-MM-dd") + "'AS DATE) and CAST('" + dtpTo.Value.ToString("yyyy-MM-dd") + "'AS DATE);";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_purchasepaymentdetails");

                rptSupplierLedger.SetDataSource(DS);

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDialog1.AllowSomePages = true;
                    rptSupplierLedger.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                    rptSupplierLedger.PrintToPrinter(printDialog1.PrinterSettings.Copies, false, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);
                }

                comm.CommandType = CommandType.Text;
                comm.CommandText = "truncate tb_tempreport";
                comm.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                //dangerAlert("Data Loading Error!");
                //MessageBox.Show(x);
                MessageBox.Show("Overrided Report is open in other app.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                rptSupplierLedger.Close();
                rptSupplierLedger.Dispose();
                comm.Dispose();
                conn.Dispose();
            }
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

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            dtpFrom.MaxDate = DateTime.Now;
            LoadDatagrid();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFrom.Value > dtpTo.Value)
            {
                dtpTo.Value = dtpFrom.Value;
            }
            dtpTo.MaxDate = DateTime.Now;
            LoadDatagrid();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dtpFrom_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dtpTo_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvPaymentDetails.Rows.Count > 0)
            {
                printReport();
            }
            else
            {
                warningAlert("Report is empty !");
            }
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
    }
}
