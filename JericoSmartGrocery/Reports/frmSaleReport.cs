using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JericoSmartGrocery.Reports
{
    public partial class frmSaleReport : Form
    {
        public ReportDocument rd = new ReportDocument();

        public frmSaleReport()
        {
            InitializeComponent();
        }              
        
        private void frmSaleReport_Load(object sender, EventArgs e)
        {
            cmbType.SelectedIndex = 0;

            this.dtpTo.ValueChanged -= new EventHandler(dtpTo_ValueChanged);            
            dtpTo.Value = DateTime.Now;
            dtpTo.MaxDate = DateTime.Now;
            this.dtpTo.ValueChanged += new EventHandler(dtpTo_ValueChanged);
            this.dtpFrom.ValueChanged -= new EventHandler(dtpFrom_ValueChanged);            
            DateTime now = DateTime.Now;
            dtpFrom.Value = DateTime.Now;//new DateTime(now.Year, now.Month, 1);
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
                comm.CommandText = "Proc_Sale_FetchLedger";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("fromDate", dtpFrom.Value);
                comm.Parameters.AddWithValue("toDate", dtpTo.Value);
                comm.Parameters.AddWithValue("billType", cmbType.SelectedIndex);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS);

                if (DS.Tables[0].Rows.Count >= 0)
                {
                    dgvSaleLedger.DataSource = DS.Tables[0];

                    int i = 1;
                    foreach (DataGridViewRow row in dgvSaleLedger.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;

                        if (Convert.ToInt32(row.Cells[11].Value) > 0)
                        {
                            DataGridViewButtonCell btn = (DataGridViewButtonCell)row.Cells[9];
                            btn.Style.ForeColor = Color.FromArgb(255,87,87);
                            btn.Style.SelectionForeColor = Color.DarkRed;
                        }
                    }
                }

                if (DS.Tables[1].Rows.Count >= 0)
                {
                    lblTotalAmt.Text = DS.Tables[1].Rows[0]["totalAmount"].ToString() == "" ? "0.00" : DS.Tables[1].Rows[0]["totalAmount"].ToString();
                    lblPaidTotal.Text = DS.Tables[1].Rows[0]["paidAmount"].ToString() == "" ? "0.00" : DS.Tables[1].Rows[0]["paidAmount"].ToString();
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

                dgvSaleLedger.ClearSelection();
            }
        }

        private void searchBillNo()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            string nameKeyword;
            string billkeyword;

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Sale_SearchBill_SaleLedger";

                comm.Connection = conn;


                int parsedvalue;
                if (int.TryParse(txtSearch.Text, out parsedvalue))
                {
                    billkeyword = txtSearch.Text;
                    nameKeyword = "";
                }
                else
                {
                    billkeyword = "";
                    nameKeyword = txtSearch.Text;
                }

                comm.Parameters.AddWithValue("billkeyword", billkeyword);
                comm.Parameters.AddWithValue("nameKeyword", nameKeyword);

                comm.Parameters.AddWithValue("fromDate", string.Format("{0:yyyy-MM-dd}", dtpFrom.Value));
                comm.Parameters.AddWithValue("toDate", string.Format("{0:yyyy-MM-dd}", dtpTo.Value));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvSaleLedger.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvSaleLedger.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;

                        if (Convert.ToInt32(row.Cells[11].Value) > 0)
                        {
                            DataGridViewButtonCell btn = (DataGridViewButtonCell)row.Cells[9];
                            btn.Style.ForeColor = Color.FromArgb(255, 87, 87);
                            btn.Style.SelectionForeColor = Color.DarkRed;
                        }
                    }
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

                dgvSaleLedger.ClearSelection();
            }
        }

        private void printReport()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataSet DS = new DataSet();
            crReports.rptSaleLedger rptSaleLedger = new crReports.rptSaleLedger();

            try
            {
                comm.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select shopName,contactOne,contactTwo,shopAddress from tb_aboutshop";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_aboutshop");


                comm.CommandType = CommandType.Text;

                if (cmbType.SelectedIndex == 1)
                {
                    comm.CommandText = "select date_format(paymentDate, '%d-%m-%Y') AS paymentDate, ifnull(c.customerName, '-') as customerName, saleBillNumber, billAmount, otherCharges, discountAmount, (billAmount + otherCharges - discountAmount) as grandTotalAmount, nowPaidAmount from tb_salespaymentdetails sp left join tb_customerdetails c on sp.customerID = c.customerID  where CAST(paymentDate AS DATE) between CAST('" + dtpFrom.Value.ToString("yyyy-MM-dd") + "'AS DATE) and  CAST('" + dtpTo.Value.ToString("yyyy-MM-dd") + "' AS DATE) and transactiontype = 'Sale' and isBalanceBill = 1";
                }
                else if (cmbType.SelectedIndex == 2)
                {
                    comm.CommandText = "select date_format(paymentDate, '%d-%m-%Y') AS paymentDate, ifnull(c.customerName, '-') as customerName, saleBillNumber, billAmount, otherCharges, discountAmount, (billAmount + otherCharges - discountAmount) as grandTotalAmount, nowPaidAmount from tb_salespaymentdetails sp left join tb_customerdetails c on sp.customerID = c.customerID  where CAST(paymentDate AS DATE) between CAST('" + dtpFrom.Value.ToString("yyyy-MM-dd") + "'AS DATE) and  CAST('" + dtpTo.Value.ToString("yyyy-MM-dd") + "' AS DATE) and transactiontype = 'Sale' and isAdvanceBill = 1";
                }
                else
                {
                    comm.CommandText = "select date_format(paymentDate, '%d-%m-%Y') AS paymentDate, ifnull(c.customerName, '-') as customerName, saleBillNumber, billAmount, otherCharges, discountAmount, (billAmount + otherCharges - discountAmount) as grandTotalAmount, nowPaidAmount from tb_salespaymentdetails sp left join tb_customerdetails c on sp.customerID = c.customerID  where CAST(paymentDate AS DATE) between CAST('" + dtpFrom.Value.ToString("yyyy-MM-dd") + "'AS DATE) and  CAST('" + dtpTo.Value.ToString("yyyy-MM-dd") + "' AS DATE) and transactiontype = 'Sale'";
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_salespaymentdetails");

                comm.CommandType = CommandType.Text;
                comm.CommandText = "Insert into tb_tempreport (fromDate, toDate) values('" + dtpFrom.Value.ToString("dd/MM/yyyy") + "', '" + dtpTo.Value.ToString("dd/MM/yyyy") + "' )";
                comm.ExecuteNonQuery();

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select * from tb_tempreport";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_tempreport");

                rptSaleLedger.SetDataSource(DS);

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDialog1.AllowSomePages = true;
                    rptSaleLedger.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;                    
                    rptSaleLedger.PrintToPrinter(printDialog1.PrinterSettings.Copies, false, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);                    
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

                rptSaleLedger.Close();
                rptSaleLedger.Dispose();
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvSaleLedger.Rows.Count > 0)
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

        private void dtpTo_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dtpFrom_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
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

        // Edit Bill

        private void dgvSaleLedger_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
            if(e.RowIndex == dgvSaleLedger.NewRowIndex || e.RowIndex < 0)
            {
                return;
            }
                        
            if (e.ColumnIndex == 9)
            {
                if (Convert.ToInt32(dgvSaleLedger.SelectedRows[0].Cells[11].Value) <= 0)
                {
                    saleMaster.frmSale frmSale = new saleMaster.frmSale();

                    frmSale.editBillNo = Convert.ToInt32(dgvSaleLedger.SelectedRows[0].Cells[3].Value);
                    frmSale.editCustomerID = Convert.ToInt32(dgvSaleLedger.SelectedRows[0].Cells[13].Value);
                    frmSale.edit = true;
                    Opacity = .50;
                    frmSale.ShowDialog();
                    Opacity = 1;

                    LoadDatagrid();
                }
            }
            else if (e.ColumnIndex == 10)
            {
                frmCustomerBillReport frmCustomerBillReport = new frmCustomerBillReport();

                frmCustomerBillReport.billNo = dgvSaleLedger.SelectedRows[0].Cells[3].Value.ToString();
                frmCustomerBillReport.salePaymentID = dgvSaleLedger.SelectedRows[0].Cells[1].Value.ToString();
                frmCustomerBillReport.type = "Sale";
                Opacity = .50;
                frmCustomerBillReport.ShowDialog();
                Opacity = 1;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            searchBillNo();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvSaleLedger.ClearSelection();
                if (dgvSaleLedger.RowCount > 0)
                {
                    dgvSaleLedger.Rows[0].Selected = true;
                    dgvSaleLedger.Select();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                txtSearch.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDatagrid();
        }
    }
}
