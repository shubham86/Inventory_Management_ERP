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
    public partial class frmCustomerPaymentReport : Form
    {
        public string ID;
        public string CustName;
        public decimal balanceAmt;
        public decimal advanceAmt;

        public frmCustomerPaymentReport()
        {
            InitializeComponent();
        }

        private void frmCustomerPaymentReport_Load(object sender, EventArgs e)
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
                comm.CommandText = "Proc_Customer_PaymentReport_FetchinGridview";

                comm.Connection = conn;
                
                comm.Parameters.AddWithValue("custID", ID);
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
                    lblTotalSaleAmt.Text = string.Format("{0:0.00}", DS.Tables[1].Rows[0]["totalSaleAmt"].ToString()) == "" ? "0.00" : string.Format("{0:0.00}", DS.Tables[1].Rows[0]["totalSaleAmt"].ToString());
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

        private void btnPayNow_Click(object sender, EventArgs e)
        {
            PaymentMaster.frmCustomerPayment frmCustomerPayment = new PaymentMaster.frmCustomerPayment();
            frmCustomerPayment.ID = ID;
            frmCustomerPayment.CustName = CustName;
            frmCustomerPayment.balanceAmt = balanceAmt;
            frmCustomerPayment.advanceAmt = advanceAmt;

            Opacity = .50;
            frmCustomerPayment.ShowDialog();
            Opacity = 1;
            LoadDatagrid();
        }

        private void dgvPaymentDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if click is on new row or header row
            if (e.RowIndex == dgvPaymentDetails.NewRowIndex || e.RowIndex < 0)
            {
                return;
            }

            //Check if click is on specific column 
            if (e.ColumnIndex == 10)
            {
                frmCustomerBillReport frmCustomerBillReport = new frmCustomerBillReport();

                frmCustomerBillReport.billNo = dgvPaymentDetails.SelectedRows[0].Cells[2].Value.ToString();
                frmCustomerBillReport.salePaymentID = dgvPaymentDetails.SelectedRows[0].Cells[1].Value.ToString();
                frmCustomerBillReport.type = dgvPaymentDetails.SelectedRows[0].Cells[3].Value.ToString();

                Opacity = .50;
                frmCustomerBillReport.ShowDialog();
                Opacity = 1;
                LoadDatagrid();
            }
        }

        private void dgvPaymentDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                frmCustomerBillReport frmCustomerBillReport = new frmCustomerBillReport();

                frmCustomerBillReport.billNo = dgvPaymentDetails.SelectedRows[0].Cells[2].Value.ToString();
                frmCustomerBillReport.salePaymentID = dgvPaymentDetails.SelectedRows[0].Cells[1].Value.ToString();
                frmCustomerBillReport.type = dgvPaymentDetails.SelectedRows[0].Cells[3].Value.ToString();

                Opacity = .50;
                frmCustomerBillReport.ShowDialog();
                Opacity = 1;
                LoadDatagrid();

                e.Handled = true;
            }
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

        private void dtpTo_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dtpFrom_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
    }
}
