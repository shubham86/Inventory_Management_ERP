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
    public partial class frmSupplierBillReport : Form
    {
        public string billNo;
        public string purchasePayID;
        public string payType;

        public frmSupplierBillReport()
        {
            InitializeComponent();
        }

        private void frmSupplierBillReport_Load(object sender, EventArgs e)
        {
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
                comm.CommandText = "Proc_Purchase_FetchBillDetails";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("billNo", billNo);
                comm.Parameters.AddWithValue("purchasePayID", purchasePayID);
                comm.Parameters.AddWithValue("payType", payType);

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
                    lblAdvance.Text = DS.Tables[1].Rows[0]["supplierAdvanceAmount"].ToString();
                    lblBalance.Text = DS.Tables[1].Rows[0]["balanceAmount"].ToString();
                    lblBillAmt.Text = DS.Tables[1].Rows[0]["billAmount"].ToString();
                    lblBillNo.Text = billNo;
                    lblInvoice.Text = DS.Tables[1].Rows[0]["invoiceNumber"].ToString();
                    lblGrndTotal.Text = DS.Tables[1].Rows[0]["grandTotalAmount"].ToString();
                    lblGstAmt.Text = DS.Tables[1].Rows[0]["gstAmount"].ToString();
                    lblOtherCharge.Text = DS.Tables[1].Rows[0]["otherCharges"].ToString();
                    lblPaidAmt.Text = DS.Tables[1].Rows[0]["nowPaidAmount"].ToString();
                    lblPayMode.Text = DS.Tables[1].Rows[0]["paymentMode"].ToString();
                    lblPrevAdv.Text = DS.Tables[1].Rows[0]["prevAdvanceAmount"].ToString();
                    lblPrevBal.Text = DS.Tables[1].Rows[0]["prevBalance"].ToString();
                    lblDiscount.Text = DS.Tables[1].Rows[0]["discountAmount"].ToString();
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
    }
}
