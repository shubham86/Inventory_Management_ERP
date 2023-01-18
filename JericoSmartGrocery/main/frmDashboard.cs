using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JericoSmartGrocery.Main
{
    public partial class frmDashboard : Form
    {
        public string userName;

        public frmDashboard()
        {
            InitializeComponent();
        }
        
        private void frmDashboard_Load(object sender, EventArgs e)
        {
            if (Global.userRole == "Operator")
            {
                addUserToolStripMenuItem.Visible = false;
                aboutShopToolStripMenuItem.Visible = false;
                purchaseMasterToolStripMenuItem.Visible = false;
                addProductToolStripMenuItem.Visible = false;
                supplierDetailsToolStripMenuItem.Visible = false;
                btnPurchase.Visible = false;
                btnPurchaseReturn.Visible = false;
                supplierPaymentReportToolStripMenuItem.Visible = false;
                supplierBalanceAndAdvanceToolStripMenuItem.Visible = false;
                purchaseLedgerToolStripMenuItem.Visible = false;
                supplierLedgerToolStripMenuItem.Visible = false;
                profitLossStatmentToolStripMenuItem.Visible = false;
            }            
            lblUserName.Text = userName;
            lblUserName.DeselectAll();
            btnDashboard.Focus();
            panelHide.Top = btnDashboard.Top;
            LoadDashboardInfo();
        }

        private void LoadDashboardInfo()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Dashboard_FetchInfo";

                comm.Connection = conn;
                
                comm.Parameters.AddWithValue("minStockRange", ConfigurationManager.AppSettings["MinStockRange"].ToString());

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    lblBalanceAmount.Text = DT.Rows[0]["totalBalance"].ToString();
                    lblAdvanceAmount.Text = DT.Rows[0]["totalAdvance"].ToString();
                    lblTotalSales.Text = DT.Rows[0]["todaysSale"].ToString();
                    lblPurchaseAmount.Text = DT.Rows[0]["todaysPurchase"].ToString();
                    lblSaleCash.Text = DT.Rows[0]["todaysSaleCash"].ToString();
                    lblPurCash.Text = DT.Rows[0]["todaysPurchaseCash"].ToString();
                    lblSaleCredite.Text = DT.Rows[0]["todaysCredit"].ToString().ToString();
                    lblPurCredite.Text = (Convert.ToDecimal(DT.Rows[0]["todaysPurchase"]) - Convert.ToDecimal(DT.Rows[0]["todaysPurchaseCash"])).ToString();
                    lblProductsOutOfStock.Text = string.Format("{0:00}", DT.Rows[0]["outOfStock"]);
                    lblPendingPayments.Text = string.Format("{0:00}", DT.Rows[0]["PendingPayments"]);
                    lblSaleRecradit.Text = string.Format("{0:0.00}", DT.Rows[0]["todaysSaleRecredit"]);
                    lblPurchaseRecredit.Text = string.Format("{0:0.00}", DT.Rows[0]["todaysPurchaseRecredit"]);

                    DateTime x = Convert.ToDateTime(DT.Rows[0]["maxDate"]);

                    if (x.Date >= DateTime.Now.Date)
                    {
                        updateAdvBalToolStripMenuItem.Visible = true;
                        updatePurchasePriceToolStripMenuItem.Visible = true;
                    }
                    else
                    {
                        updateAdvBalToolStripMenuItem.Visible = false;
                        updatePurchasePriceToolStripMenuItem.Visible = false;
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
            }
        }

        private void supplierDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.frmAddSupplier addSuplier = new master.frmAddSupplier();
            addSuplier.ShowDialog();
        }

        private void customerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.frmAddCustomer addCustomer = new master.frmAddCustomer();
            addCustomer.ShowDialog();
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.frmAddProduct addProduct = new master.frmAddProduct();
            addProduct.ShowDialog();
        }

        private void addUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.frmAddUnits addUnits = new master.frmAddUnits();
            addUnits.ShowDialog();
        }

        private void addFinancialYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.frmAddFinancialYear addFinancialYear = new master.frmAddFinancialYear();
            addFinancialYear.ShowDialog();
        }

        private void addBrandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.frmAddBrand addBrand = new master.frmAddBrand();
            addBrand.ShowDialog();
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.frmAddUser addUser = new master.frmAddUser();
            addUser.ShowDialog();
        }

        private void addGSTSlabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.frmAddGstSlab addGstSlab = new master.frmAddGstSlab();
            addGstSlab.ShowDialog();
        }

        private void aboutShopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.frmShopInfo frmShopInfo = new master.frmShopInfo();
            frmShopInfo.ShowDialog();
        }

        private void purchaseEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            purchaseMaster.frmPurchase frmPurchase = new purchaseMaster.frmPurchase();
            frmPurchase.ShowDialog();
            LoadDashboardInfo();
        }

        private void salePOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saleMaster.frmSale frmSale = new saleMaster.frmSale();
            frmSale.ShowDialog();
            LoadDashboardInfo();
        }
        
        private void supplierPaymentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PaymentMaster.frmSupplierPaymentList frmSupplierPaymentList = new PaymentMaster.frmSupplierPaymentList();
            frmSupplierPaymentList.ShowDialog();
            LoadDashboardInfo();
        }

        private void customerPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaymentMaster.frmCustomerPaymentList frmCustomerPaymentList = new PaymentMaster.frmCustomerPaymentList();
            frmCustomerPaymentList.ShowDialog();
            LoadDashboardInfo();
        }

        private void customerPaymentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Reports.frmCustomerList frmCustomerList = new Reports.frmCustomerList();
            frmCustomerList.ShowDialog();
        }

        private void supplierPaymentReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmSupplierList frmSupplierList = new Reports.frmSupplierList();
            frmSupplierList.ShowDialog();
        }

        private void purchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            purchaseMaster.frmPurchaseReturn frmPurchaseReturn = new purchaseMaster.frmPurchaseReturn();
            frmPurchaseReturn.ShowDialog();
            LoadDashboardInfo();
        }

        private void saleReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saleMaster.frmSaleReturn frmSaleReturn = new saleMaster.frmSaleReturn();
            frmSaleReturn.ShowDialog();
            LoadDashboardInfo();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {            
           stockMaster.frmStockUpdate frmStockUpdate = new stockMaster.frmStockUpdate();
            frmStockUpdate.ShowDialog();
        }

        private void stockLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmProductStock frmProductStock = new Reports.frmProductStock();
            frmProductStock.ShowDialog();
        }

        private void pendigCustomerPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaymentMaster.frmCustomerPendingPayments frmCustomerPendingPayments = new PaymentMaster.frmCustomerPendingPayments();
            frmCustomerPendingPayments.ShowDialog();
        }

        private void pendigSuppliersPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PaymentMaster.frmSupplierPendingPayments frmSupplierPendingPayments = new PaymentMaster.frmSupplierPendingPayments();
            frmSupplierPendingPayments.ShowDialog();
        }

        private void payGSTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GST.frmGSTpayment frmGSTpayment = new GST.frmGSTpayment();
            frmGSTpayment.ShowDialog();
        }

        private void pendingGSTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GST.frmPendingGST frmPendingGST = new GST.frmPendingGST();
            frmPendingGST.ShowDialog();
        }
        
        private void btnPurchase_Click(object sender, EventArgs e)
        {
            panelHide.Top = btnPurchase.Top;
            btnPurchase.FlatAppearance.BorderColor = Color.FromArgb(0, 49, 53, 55);
            purchaseMaster.frmPurchase frmPurchase = new purchaseMaster.frmPurchase();
            frmPurchase.ShowDialog();
            LoadDashboardInfo();

            panelHide.Top = btnDashboard.Top;            
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            panelHide.Top = btnSale.Top;
            btnSale.FlatAppearance.BorderColor = Color.FromArgb(0, 49,53,55);
            saleMaster.frmSale frmSale = new saleMaster.frmSale();
            frmSale.ShowDialog();
            panelHide.Top = btnDashboard.Top;
            LoadDashboardInfo();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            panelHide.Top = btnDashboard.Top;

        }
    
        private void btnPurchaseReturn_Click(object sender, EventArgs e)
        {
            panelHide.Top = btnPurchaseReturn.Top;
            btnPurchaseReturn.FlatAppearance.BorderColor = Color.FromArgb(0, 49, 53, 55);
            purchaseMaster.frmPurchaseReturn frmPurchaseReturn = new purchaseMaster.frmPurchaseReturn();
            frmPurchaseReturn.ShowDialog();
            panelHide.Top = btnDashboard.Top;
            LoadDashboardInfo();
        }

        private void btnSaleReturn_Click(object sender, EventArgs e)
        {
            panelHide.Top = btnSaleReturn.Top;
            btnSaleReturn.FlatAppearance.BorderColor = Color.FromArgb(0, 49, 53, 55);
            saleMaster.frmSaleReturn frmSaleReturn = new saleMaster.frmSaleReturn();
            frmSaleReturn.ShowDialog();
            panelHide.Top = btnDashboard.Top;
            LoadDashboardInfo();
        }

        private void saleLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmSaleReport frmSaleReport = new Reports.frmSaleReport();
            frmSaleReport.ShowDialog();
        }

        private void purchaseLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmPurchaseReport frmPurchaseReport = new Reports.frmPurchaseReport();
            frmPurchaseReport.ShowDialog();
            LoadDashboardInfo();
        }

        private void customerLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmCustomerListForLedger frmCustomerListForLedger = new Reports.frmCustomerListForLedger();
            frmCustomerListForLedger.ShowDialog();
        }

        private void supplierLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmSupplierListForLedger frmSupplierListForLedger = new Reports.frmSupplierListForLedger();
            frmSupplierListForLedger.ShowDialog();
        }

        private void customerBalanceAdvanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmCustomerBalance_Advance frmCustomerBalance_Advance = new Reports.frmCustomerBalance_Advance();
            frmCustomerBalance_Advance.ShowDialog();
        }

        private void supplierBalanceAndAdvanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmSupplierBalance_Advance frmSupplierBalance_Advance = new Reports.frmSupplierBalance_Advance();
            frmSupplierBalance_Advance.ShowDialog();
        }

        private void ovalShape1_Click(object sender, EventArgs e)
        {
            Reports.frmProductStock frmProductStock = new Reports.frmProductStock();
            frmProductStock.ShowDialog();
            LoadDashboardInfo();
        }

        private void lblProductsOutOfStock_Click(object sender, EventArgs e)
        {
            Reports.frmProductStock frmProductStock = new Reports.frmProductStock();
            frmProductStock.ShowDialog();            
            LoadDashboardInfo();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Reports.frmProductStock frmProductStock = new Reports.frmProductStock();
            frmProductStock.ShowDialog();
            LoadDashboardInfo();
        }
        private void label11_Click(object sender, EventArgs e)
        {
            Reports.frmProductStock frmProductStock = new Reports.frmProductStock();
            frmProductStock.ShowDialog();
            LoadDashboardInfo();
        }

        private void ovalShape2_Click(object sender, EventArgs e)
        {
            PaymentMaster.frmCustomerPendingPayments frmCustomerPendingPayments = new PaymentMaster.frmCustomerPendingPayments();
            frmCustomerPendingPayments.ShowDialog();
            LoadDashboardInfo();
        }

        private void lblPendingPayments_Click(object sender, EventArgs e)
        {
            PaymentMaster.frmCustomerPendingPayments frmCustomerPendingPayments = new PaymentMaster.frmCustomerPendingPayments();
            frmCustomerPendingPayments.ShowDialog();
            LoadDashboardInfo();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            PaymentMaster.frmCustomerPendingPayments frmCustomerPendingPayments = new PaymentMaster.frmCustomerPendingPayments();
            frmCustomerPendingPayments.ShowDialog();
            LoadDashboardInfo();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            PaymentMaster.frmCustomerPendingPayments frmCustomerPendingPayments = new PaymentMaster.frmCustomerPendingPayments();
            frmCustomerPendingPayments.ShowDialog();
            LoadDashboardInfo();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            main.AboutApp aboutApp = new main.AboutApp();
            aboutApp.ShowDialog();
        }

        private void takeDBbackupToPC()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            string file = ConfigurationManager.AppSettings["BackupPath"] + DateTime.Now.ToString("dd-MM-yyyy") + ".sql";
            string file_external = ConfigurationManager.AppSettings["BackupPath_External"] + DateTime.Now.ToString("dd-MM-yyyy") + ".sql"; //
            string drive = Path.GetPathRoot("G:\\");  
            try
            {

                comm.Connection = conn;
                conn.Open();

                if (Directory.Exists(drive))
                {
                    if (!System.IO.Directory.Exists(@"G:\\JSG_Backup"))
                    {
                        System.IO.Directory.CreateDirectory(@"G:\\JSG_Backup");
                    }

                    using (MySqlBackup mb = new MySqlBackup(comm))
                    {
                        mb.ExportInfo.ExportProcedures = false;
                        mb.ExportToFile(file_external);
                    }
                }
                else
                {
                    MessageBox.Show("Pen Drive is not connected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (!System.IO.Directory.Exists(@"F:\\JSG_Backup"))
                {
                    System.IO.Directory.CreateDirectory(@"F:\\JSG_Backup");
                }

                using (MySqlBackup mb = new MySqlBackup(comm))
                {
                    mb.ExportInfo.ExportProcedures = false;
                    mb.ExportToFile(file);
                }

                conn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);//"Pen Drive is not connected"
            }
        }
        
        private void frmDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to close this application?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
            else {
                takeDBbackupToPC();
                main.frmLogin frmLogin = new main.frmLogin();
                frmLogin.Close();
            }           
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            takeDBbackupToPC();
            MessageBox.Show("Backup saved successfully.","Success.",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            takeDBbackupToPC();
            MessageBox.Show("Backup saved successfully.", "Success.", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void profitLossStatmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmProfitLossStatment frmProfitLossStatment = new Reports.frmProfitLossStatment();
            frmProfitLossStatment.ShowDialog();
        }
        
        private void toolStripSeparator1_Paint(object sender, PaintEventArgs e)
        {
            ToolStripSeparator sep = (ToolStripSeparator)sender;

            e.Graphics.FillRectangle(new SolidBrush(Color.Gainsboro), 0, 0, sep.Width, sep.Height);

            e.Graphics.DrawLine(new Pen(Color.Silver), 10, sep.Height / 2, sep.Width - 8, sep.Height / 2);
        }

        private void toolStripSeparator2_Paint(object sender, PaintEventArgs e)
        {
            ToolStripSeparator sep = (ToolStripSeparator)sender;

            e.Graphics.FillRectangle(new SolidBrush(Color.Gainsboro), 0, 0, sep.Width, sep.Height);

            e.Graphics.DrawLine(new Pen(Color.Silver), 10, sep.Height / 2, sep.Width - 8, sep.Height / 2);
        }

        private void toolStripSeparator3_Paint(object sender, PaintEventArgs e)
        {
            ToolStripSeparator sep = (ToolStripSeparator)sender;

            e.Graphics.FillRectangle(new SolidBrush(Color.Gainsboro), 0, 0, sep.Width, sep.Height);

            e.Graphics.DrawLine(new Pen(Color.Silver), 10, sep.Height / 2, sep.Width - 8, sep.Height / 2);
        }

        private void toolStripSeparator4_Paint(object sender, PaintEventArgs e)
        {
            ToolStripSeparator sep = (ToolStripSeparator)sender;

            e.Graphics.FillRectangle(new SolidBrush(Color.Gainsboro), 0, 0, sep.Width, sep.Height);

            e.Graphics.DrawLine(new Pen(Color.Silver), 10, sep.Height / 2, sep.Width - 8, sep.Height / 2);
        }

        private void toolStripSeparator6_Paint(object sender, PaintEventArgs e)
        {
            ToolStripSeparator sep = (ToolStripSeparator)sender;

            e.Graphics.FillRectangle(new SolidBrush(Color.Gainsboro), 0, 0, sep.Width, sep.Height);

            e.Graphics.DrawLine(new Pen(Color.Silver), 10, sep.Height / 2, sep.Width - 8, sep.Height / 2);
        }

        private void toolStripSeparator5_Paint(object sender, PaintEventArgs e)
        {
            ToolStripSeparator sep = (ToolStripSeparator)sender;

            e.Graphics.FillRectangle(new SolidBrush(Color.Gainsboro), 0, 0, sep.Width, sep.Height);

            e.Graphics.DrawLine(new Pen(Color.Silver), 10, sep.Height / 2, sep.Width - 8, sep.Height / 2);
        }

        private void updateAdvBalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.TempCustomerAdv_Bal tempCustomerAdv_Bal = new master.TempCustomerAdv_Bal();
            tempCustomerAdv_Bal.ShowDialog();
        }

        private void updatePurchasePriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.frmTempPurchasePrice frmTempPurchasePrice = new master.frmTempPurchasePrice();
            frmTempPurchasePrice.ShowDialog();
        }

        private void creditRepaymentReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmCustomerRepaymentReport frmCustomerRepaymentReport = new Reports.frmCustomerRepaymentReport();
            frmCustomerRepaymentReport.ShowDialog();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Reports.frmProductSale frmProductSale = new Reports.frmProductSale();
            frmProductSale.ShowDialog();
        }

        //Short-cut
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.R))
            {
                Reports.frmSaleReport frmSaleReport = new Reports.frmSaleReport();
                frmSaleReport.ShowDialog();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.P))
            {
                PaymentMaster.frmCustomerPaymentList frmCustomerPaymentList = new PaymentMaster.frmCustomerPaymentList();
                frmCustomerPaymentList.ShowDialog();
                LoadDashboardInfo();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.U))
            {
                stockMaster.frmStockUpdate frmStockUpdate = new stockMaster.frmStockUpdate();
                frmStockUpdate.ShowDialog();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void testProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            master.Form1 form1 = new master.Form1();
            form1.ShowDialog();
        }

        private void saleReturnReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reports.frmSaleReturnReport frmSaleReturnReport = new Reports.frmSaleReturnReport();
            frmSaleReturnReport.ShowDialog();
        }

        private void expensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            main.frmExpenses frmExpenses = new main.frmExpenses();
            frmExpenses.ShowDialog();
        }
    }
}
