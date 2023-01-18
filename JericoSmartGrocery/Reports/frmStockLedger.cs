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
    public partial class frmStockLedger : Form
    {
        public string productName;
        public string currentStock;
        public string proID;

        public frmStockLedger()
        {
            InitializeComponent();
        }

        private void frmStockLedger_Load(object sender, EventArgs e)
        {
            this.dtpTo.ValueChanged -= new EventHandler(dtpTo_ValueChanged);
            dtpTo.Value = DateTime.Now;
            dtpTo.MaxDate = DateTime.Now;
            this.dtpTo.ValueChanged += new EventHandler(dtpTo_ValueChanged);
            this.dtpFrom.ValueChanged -= new EventHandler(dtpFrom_ValueChanged);
            DateTime now = DateTime.Now;
            dtpFrom.Value = new DateTime(now.Year, now.Month, 1);
            dtpFrom.MaxDate = DateTime.Now;
            this.dtpFrom.ValueChanged += new EventHandler(dtpFrom_ValueChanged);

            lblProductName.Text = productName;
            lblStock.Text = currentStock;
            LoadDatagrid();
        }

        private void LoadDatagrid()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_StockLedger_FetchInGrigview";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("proID", proID);
                comm.Parameters.AddWithValue("fromDate", dtpFrom.Value);
                comm.Parameters.AddWithValue("toDate", dtpTo.Value);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count >= 0)
                {
                    dgvStockLedger.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvStockLedger.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;
                    }
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

                dgvStockLedger.ClearSelection();
            }
        }

        private void printReport()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();
            //crReports.trial trial = new crReports.trial();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_StockLedger_FetchInGrigview";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("proID", proID);
                comm.Parameters.AddWithValue("fromDate", dtpFrom.Value);
                comm.Parameters.AddWithValue("toDate", dtpTo.Value);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count >= 0)
                {
                    //trial.SetDataSource(DT);

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
                       // trial.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                       // trial.PrintToPrinter(printDialog1.PrinterSettings.Copies, false, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);
                    }
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
            if (dgvStockLedger.Rows.Count > 0)
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
