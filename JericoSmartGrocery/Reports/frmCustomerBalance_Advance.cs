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
    public partial class frmCustomerBalance_Advance : Form
    {
        public frmCustomerBalance_Advance()
        {
            InitializeComponent();
        }

        private void frmCustomerBalance_Advance_Load(object sender, EventArgs e)
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
                comm.CommandText = "Proc_Customer_BalanceAndAdvance_Report";

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
                    dgvCustomerDetails.DataSource = DS.Tables[0];

                    //int i = 1;
                    //foreach (DataGridViewRow row in dgvCustomerDetails.Rows)
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

                dgvCustomerDetails.ClearSelection();
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
            if (dgvCustomerDetails.Rows.Count > 0)
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
            crReports.rptCustomerBalAndAdv rptCustomerBalAndAdv = new crReports.rptCustomerBalAndAdv();

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
                    comm.CommandText = "SELECT c.customerID, ifnull(c.customerName, '-') as customerName, ifnull(c.customerMobile, '-') as customerMobile, if (c.pageNo IS NULL or pageNo = '','-', pageNo) as pageNo, ifnull(sp.customerAdvanceAmount, '0.00') as customerAdvanceAmount, ifnull(sp.balanceAmount, '0.00') as balanceAmount FROM tb_customerdetails c inner join tb_salespaymentdetails sp on sp.customerID = c.customerID WHERE c.isActive = 1 and sp.isActiveBalance = 1 and sp.balanceAmount != 0.00 ORDER BY customerName;";
                    DA = new MySqlDataAdapter(comm);
                    DA.Fill(DS, "tb_salespaymentdetails");
                }
                else if (cmbType.SelectedIndex == 2)
                {
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "SELECT c.customerID, ifnull(c.customerName, '-') as customerName, ifnull(c.customerMobile, '-') as customerMobile, if (c.pageNo IS NULL or pageNo = '','-', pageNo) as pageNo, ifnull(sp.customerAdvanceAmount, '0.00') as customerAdvanceAmount, ifnull(sp.balanceAmount, '0.00') as balanceAmount FROM tb_customerdetails c inner join tb_salespaymentdetails sp on sp.customerID = c.customerID WHERE c.isActive = 1 and sp.isActiveBalance = 1 and sp.customerAdvanceAmount != 0.00 ORDER BY customerName;";
                    DA = new MySqlDataAdapter(comm);
                    DA.Fill(DS, "tb_salespaymentdetails");
                }
                else
                {
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "SELECT c.customerID, ifnull(c.customerName, '-') as customerName, ifnull(c.customerMobile, '-') as customerMobile, if (c.pageNo IS NULL or pageNo = '','-', pageNo) as pageNo, ifnull(sp.customerAdvanceAmount, '0.00') as customerAdvanceAmount, ifnull(sp.balanceAmount, '0.00') as balanceAmount FROM tb_customerdetails c inner join tb_salespaymentdetails sp on sp.customerID = c.customerID WHERE c.isActive = 1 and sp.isActiveBalance = 1 ORDER BY customerName;";
                    DA = new MySqlDataAdapter(comm);
                    DA.Fill(DS, "tb_salespaymentdetails");
                }

                    //comm.CommandType = CommandType.StoredProcedure;
                    //comm.CommandText = "Proc_Customer_BalanceAndAdvance_Report";
                    //comm.Parameters.AddWithValue("rType", cmbType.SelectedIndex);
                    //DA = new MySqlDataAdapter(comm);
                    //DA.Fill(DS, "tb_salespaymentdetails");

                comm.CommandType = CommandType.Text;
                comm.CommandText = "Insert into tb_tempreport (balanceAmt, advanceAmt) values(" + lblBalAmt.Text + "," + lblAdvAmt.Text + ");";
                comm.ExecuteNonQuery();

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select * from tb_tempreport";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_tempreport");

                rptCustomerBalAndAdv.SetDataSource(DS);

                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDialog1.AllowSomePages = true;
                    rptCustomerBalAndAdv.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
                    rptCustomerBalAndAdv.PrintToPrinter(printDialog1.PrinterSettings.Copies, false, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);
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

                rptCustomerBalAndAdv.Close();
                rptCustomerBalAndAdv.Dispose();
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
