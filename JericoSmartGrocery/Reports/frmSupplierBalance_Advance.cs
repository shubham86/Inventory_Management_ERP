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
    public partial class frmSupplierBalance_Advance : Form
    {
        public frmSupplierBalance_Advance()
        {
            InitializeComponent();
        }

        private void frmSupplierBalance_Advance_Load(object sender, EventArgs e)
        {
            cmbType.SelectedIndex = 0;
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
                comm.CommandText = "Proc_Supplier_BalanceAndAdvance_Report";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("rType", cmbType.SelectedIndex);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS);

                if (DS.Tables[0].Rows.Count >= 0)
                {
                    dgvSupplierDetails.DataSource = DS.Tables[0];

                    //int i = 1;
                    //foreach (DataGridViewRow row in dgvSupplierDetails.Rows)
                    //{
                    //    row.Cells[0].Value = i;
                    //    i++;
                    //}
                }

                if (DS.Tables[1].Rows.Count > 0)
                {
                    lblBalAmt.Text = DS.Tables[1].Rows[0]["totalBalance"].ToString();
                    lblAdvAmt.Text = DS.Tables[1].Rows[0]["totalAdvance"].ToString();
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

                dgvSupplierDetails.ClearSelection();
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
            if (dgvSupplierDetails.Rows.Count > 0)
            {
                printReport();
            }
            else
            {
                warningAlert("Report is empty !");
            }
        }

        private void printReport()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataSet DS = new DataSet();
            crReports.rptSupplierBalAndAdv rptSupplierBalAndAdv = new crReports.rptSupplierBalAndAdv();

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

                if (cmbType.SelectedIndex == 1)
                {
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "SELECT s.supplierID, ifnull(s.supplierName, '-') as supplierName, ifnull(s.supplierMobile, '-') as supplierMobile, if (s.pageNo IS NULL or s.pageNo = '', '-', s.pageNo) as pageNo, ifnull(pp.supplierAdvanceAmount, '0.00') as supplierAdvanceAmount, ifnull(pp.balanceAmount, '0.00') as balanceAmount FROM tb_supplierdetails s INNER JOIN	tb_purchasepaymentdetails pp on pp.supplierID = s.supplierID WHERE s.isActive = 1 and pp.isActiveBalance = 1 and pp.balanceAmount != 0.00 ORDER BY supplierName;";
                    DA = new MySqlDataAdapter(comm);
                    DA.Fill(DS, "tb_purchasepaymentdetails");
                }
                else if (cmbType.SelectedIndex == 2)
                {
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = " SELECT s.supplierID, ifnull(s.supplierName, '-') as supplierName, ifnull(s.supplierMobile, '-') as supplierMobile, if (s.pageNo IS NULL or s.pageNo = '', '-', s.pageNo) as pageNo, ifnull(pp.supplierAdvanceAmount, '0.00') as supplierAdvanceAmount, ifnull(pp.balanceAmount, '0.00') as balanceAmount FROM tb_supplierdetails s INNER JOIN	tb_purchasepaymentdetails pp on pp.supplierID = s.supplierID WHERE s.isActive = 1 and pp.isActiveBalance = 1 and pp.supplierAdvanceAmount != 0.00 ORDER BY supplierName;";
                    DA = new MySqlDataAdapter(comm);
                    DA.Fill(DS, "tb_purchasepaymentdetails");
                }
                else
                {
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "SELECT s.supplierID, ifnull(s.supplierName, '-') as supplierName, ifnull(s.supplierMobile, '-') as supplierMobile, if (s.pageNo IS NULL or s.pageNo = '', '-', s.pageNo) as pageNo, ifnull(pp.supplierAdvanceAmount, '0.00') as supplierAdvanceAmount, ifnull(pp.balanceAmount, '0.00') as balanceAmount FROM tb_supplierdetails s INNER JOIN tb_purchasepaymentdetails pp on pp.supplierID = s.supplierID WHERE s.isActive = 1 and pp.isActiveBalance = 1 ORDER BY supplierName;";
                    DA = new MySqlDataAdapter(comm);
                    DA.Fill(DS, "tb_purchasepaymentdetails");
                }

                //comm.CommandType = CommandType.StoredProcedure;
                //comm.CommandText = "Proc_Supplier_BalanceAndAdvance_Report";
                //comm.Parameters.AddWithValue("rType", cmbType.SelectedIndex);
                //DA = new MySqlDataAdapter(comm);
                //DA.Fill(DS, "tb_purchasepaymentdetails");

                comm.CommandType = CommandType.Text;
                comm.CommandText = "Insert into tb_tempreport (balanceAmt, advanceAmt) values(" + lblBalAmt.Text + "," + lblAdvAmt.Text + ");";
                comm.ExecuteNonQuery();

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select * from tb_tempreport";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_tempreport");


                rptSupplierBalAndAdv.SetDataSource(DS);

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDialog1.AllowSomePages = true;
                    rptSupplierBalAndAdv.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                    rptSupplierBalAndAdv.PrintToPrinter(printDialog1.PrinterSettings.Copies, false, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);
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

                rptSupplierBalAndAdv.Close();
                rptSupplierBalAndAdv.Dispose();
                comm.Dispose();
                conn.Dispose();
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDatagrid();
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
