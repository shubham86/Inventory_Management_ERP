using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JericoSmartGrocery.Reports
{
    public partial class frmCustomeReturnBillReport : Form
    {
        public string billNo;
        public string salePaymentID;
        public string type;

        public frmCustomeReturnBillReport()
        {
            InitializeComponent();
        }

        private void frmCustomerBillReport_Load(object sender, EventArgs e)
        {
            LoadDatagrid();

            if (type == "Sale Return")
            {
                lblreturn.Visible = true;
            }
            else
            {
                lblreturn.Visible = false;
            }
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
                comm.CommandText = "Proc_SaleReturn_FetchBillDetails";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("billNo", billNo);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    dgvBillDetails.DataSource = DS.Tables[0];

                    int i = 1;
                    foreach (DataGridViewRow row in dgvBillDetails.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;
                    }
                }

                if (DS.Tables[1].Rows.Count > 0)
                {
                    lblDate.Text = DS.Tables[1].Rows[0]["paymentDate"].ToString();
                    lblReturnPriceAmount.Text = DS.Tables[1].Rows[0]["returnProductAmt"].ToString();
                    lblBillNo.Text = billNo;
                    lblRetunrPaidAmount.Text = DS.Tables[1].Rows[0]["returnedAmt"].ToString();
                    lblSaleBillAmount.Text = DS.Tables[1].Rows[0]["saleBillAmount"].ToString();
                    lblCustomerID.Text = DS.Tables[1].Rows[0]["customerID"].ToString();
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

                dgvBillDetails.ClearSelection();
            }
        }

        private void insertTempRepordData()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            comm.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            comm.Parameters.AddWithValue("saleBillNo", billNo);

            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "proc_tb_tempreport_insert";
            comm.ExecuteNonQuery();

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            comm.Dispose();
            conn.Dispose();
        }

        private void truncateTempTbl()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            comm.Connection = conn;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            comm.CommandType = CommandType.Text;
            comm.CommandText = "truncate tb_tempreport";
            comm.ExecuteNonQuery();

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }

            comm.Dispose();
            conn.Dispose();
        }

        //private void printInvoice()
        //{
        //    MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
        //    MySqlCommand comm = new MySqlCommand();
        //    MySqlDataAdapter DA = new MySqlDataAdapter();
        //    DataSet DS = new DataSet();
        //    crReports.rptSaleInvoice rptSaleInvoice = new crReports.rptSaleInvoice();

        //    try
        //    {
        //        comm.Connection = conn;
        //        if (conn.State == ConnectionState.Closed)
        //        {
        //            conn.Open();
        //        }

        //        comm.CommandType = CommandType.Text;
        //        comm.CommandText = "select shopName,contactOne,contactTwo,shopAddress,gstNumber,termsConditions,termsConditions2 from tb_aboutshop";
        //        DA = new MySqlDataAdapter(comm);
        //        DA.Fill(DS, "tb_aboutshop");

        //        if (type == "Sale")
        //        {
        //            comm.CommandType = CommandType.Text;
        //            comm.CommandText = "SELECT  marathiName, saleQty, saleRate, totalPrice,unit FROM tb_sales WHERE saleBillNumber ='" + billNo + "'";
        //            DA = new MySqlDataAdapter(comm);
        //            DA.Fill(DS, "tb_sales");
        //            DA.Fill(DS, "tb_productdetails");
        //        }
        //        else
        //        {
        //            //comm.CommandType = CommandType.Text;
        //            //comm.CommandText = "SELECT  (select marathiName from tb_productdetails where productID = tb_salereturn.productID) as marathiName,  (returnQty) as  saleQty, (select saleRate from tb_sales where saleBillNumber = tb_salereturn.saleBillNumber and productID = tb_salereturn.productID) as saleRate, ((select saleRate from tb_sales where saleBillNumber = tb_salereturn.saleBillNumber and productID = tb_salereturn.productID) * returnQty) as totalPrice FROM tb_salereturn WHERE saleBillNumber ='" + billNo + "'";
        //            //DA = new MySqlDataAdapter(comm);
        //            //DA.Fill(DS, "tb_sales");
        //            //DA.Fill(DS, "tb_productdetails");
        //        }

        //        comm.CommandType = CommandType.Text;
        //        comm.CommandText = "select saleBillNumber,otherCharges, nowPaidAmount, round(billAmount + otherCharges - discountAmount) as grandTotalAmount,balanceAmount,customerAdvanceAmount,discountAmount, prevBalance, prevAdvanceAmount from tb_salespaymentdetails where saleBillNumber = '" + billNo + "' and transactiontype = '" + type + "';";
        //        DA = new MySqlDataAdapter(comm);
        //        DA.Fill(DS, "tb_salespaymentdetails");

        //        comm.CommandType = CommandType.Text;
        //        comm.CommandText = "select customerName from tb_customerdetails where customerID = '" + lblCustomerID.Text + "'";
        //        DA = new MySqlDataAdapter(comm);
        //        DA.Fill(DS, "tb_customerdetails");

        //        comm.CommandType = CommandType.Text;
        //        comm.CommandText = "select date,time,billStatus from tb_tempreport";
        //        DA = new MySqlDataAdapter(comm);
        //        DA.Fill(DS, "tb_tempreport");

        //        rptSaleInvoice.SetDataSource(DS);            

        //        if (printDialog1.ShowDialog() == DialogResult.OK)
        //        {
        //            printDialog1.AllowSomePages = true;
        //            rptSaleInvoice.PrintOptions.PrinterName = printDialog1.PrinterSettings.PrinterName;
        //            rptSaleInvoice.PrintToPrinter(printDialog1.PrinterSettings.Copies, false, printDialog1.PrinterSettings.FromPage, printDialog1.PrinterSettings.ToPage);
        //        }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        string x = ex.ToString();
        //        dangerAlert("Data Loading Error!");
        //        MessageBox.Show(x);
        //        //MessageBox.Show("Overrided Report is open in other app.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }

        //    finally
        //    {
        //        if (conn.State == ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }

        //        rptSaleInvoice.Close();
        //        rptSaleInvoice.Dispose();
        //        comm.Dispose();
        //        conn.Dispose();
        //    }
        //}


        static TableLogOnInfo crTableLogonInfo;
        static ConnectionInfo crConnectionInfo;
        static Tables crTables;
        static Database crDatabase;

        public static void ReportLogin(ReportDocument crDoc, string Server, string Database, string UserID, string Password)
        {
            crConnectionInfo = new ConnectionInfo();
            crConnectionInfo.ServerName = Server;
            crConnectionInfo.DatabaseName = Database;
            crConnectionInfo.UserID = UserID;
            crConnectionInfo.Password = Password;
            crDatabase = crDoc.Database;
            crTables = crDatabase.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
            {
                crTableLogonInfo = crTable.LogOnInfo;
                crTableLogonInfo.ConnectionInfo = crConnectionInfo;
                crTable.ApplyLogOnInfo(crTableLogonInfo);
            }
        }

        private void printPaymentInvoice()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataSet DS = new DataSet();
            crReports.rptCustomerPaymentInvoice rptCustomerPaymentInvoice = new crReports.rptCustomerPaymentInvoice();

            //string strServer = "localhost";//ConfigurationManager.AppSettings["ServerName"].ToString();
            //string strDatabase = "smartgrocery";//ConfigurationManager.AppSettings["DataBaseName"].ToString();
            //string strUserID = "root";//ConfigurationManager.AppSettings["UserId"].ToString();
            //string strPwd = "root";//ConfigurationManager.AppSettings["Password"].ToString();

            //rptCustomerPaymentInvoice.DataSourceConnections[0].IntegratedSecurity = false;
            //rptCustomerPaymentInvoice.DataSourceConnections[0].SetConnection(strServer, strDatabase, strUserID, strPwd);

            try
            {
                comm.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select shopName,contactOne,contactTwo,shopAddress,gstNumber,termsConditions from tb_aboutshop";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_aboutshop");

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select saleBillNumber, nowPaidAmount, balanceAmount, prevBalance, prevAdvanceAmount, customerAdvanceAmount, discountAmount from tb_salespaymentdetails where salePaymentID = '" + salePaymentID + "' and transactiontype = 'Customer Payment';";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_salespaymentdetails");

                comm.CommandType = CommandType.Text;
                comm.CommandText = "select customerName from tb_customerdetails where customerID = '"+ lblCustomerID.Text + "'";
                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS, "tb_customerdetails");

                comm.CommandType = CommandType.Text;
                comm.CommandText = "Insert into tb_tempreport (date, time) values((SELECT DATE_FORMAT(paymentDate,'%d-%m-%y') FROM tb_salespaymentdetails where salePaymentID ='" + salePaymentID + "'), (SELECT time_format(paymentDate,'%h:%i %p') FROM tb_salespaymentdetails where salePaymentID ='" + salePaymentID + "'))";
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
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (type == "Sale")
            {
                if (dgvBillDetails.Rows.Count > 0)
                {
                    insertTempRepordData();
                    //printInvoice();
                    truncateTempTbl();
                }
                else
                {
                    warningAlert("Bill is empty !");
                }
            }
            else if (type == "Customer Payment")
            {
                printPaymentInvoice();
            }
            else if (type == "New Entry")
            {
                warningAlert("Bill is empty !");
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
