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
    public partial class frmProfitLossStatment : Form
    {
        public frmProfitLossStatment()
        {
            InitializeComponent();
        }

        private void frmProfitLossStatment_Load(object sender, EventArgs e)
        {
            this.dtpToDate.ValueChanged -= new EventHandler(dtpToDate_ValueChanged);
            dtpToDate.Value = DateTime.Now;
            dtpToDate.MaxDate = DateTime.Now;
            this.dtpToDate.ValueChanged += new EventHandler(dtpToDate_ValueChanged);
            this.dtpFromDate.ValueChanged -= new EventHandler(dtpFromDate_ValueChanged);
            DateTime now = DateTime.Now;
            dtpFromDate.Value = new DateTime(now.Year, now.Month, 1);
            dtpFromDate.MaxDate = DateTime.Now;
            this.dtpFromDate.ValueChanged += new EventHandler(dtpFromDate_ValueChanged);

            
            
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
                comm.CommandText = "Proc_Sale_ProfitLoss";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("fromDate", dtpFromDate.Value);
                comm.Parameters.AddWithValue("toDate", dtpToDate.Value);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count >= 0)
                {
                    lblTotalSale.Text = DT.Rows[0]["totalSale"].ToString() == "" ? "0.00" : DT.Rows[0]["totalSale"].ToString();
                    lblSaleReturn.Text = DT.Rows[0]["totalSaleReturn"].ToString() == "" ? "0.00" : DT.Rows[0]["totalSaleReturn"].ToString();
                    lblTotalPurchase.Text = string.Format("{0:0.00}", DT.Rows[0]["totalPurchase"].ToString() == "" ? "0.00" : DT.Rows[0]["totalPurchase"]);
                    lblTotalPurchaseReturn.Text = string.Format("{0:0.00}",DT.Rows[0]["totalPurchaseReturn"].ToString() == "" ? "0.00" : DT.Rows[0]["totalPurchaseReturn"]);
                    lblDiscount.Text = DT.Rows[0]["totalDiscount"].ToString() == "" ? "0.00" : DT.Rows[0]["totalDiscount"].ToString();
                    lblExpenses.Text = DT.Rows[0]["expenses"].ToString() == "" ? "0.00" : DT.Rows[0]["expenses"].ToString();

                    decimal sale = 0;
                    decimal purchase = 0;

                    sale = Convert.ToDecimal(lblTotalSale.Text) - Convert.ToDecimal(lblSaleReturn.Text);
                    purchase = Convert.ToDecimal(lblTotalPurchase.Text) - Convert.ToDecimal(lblTotalPurchaseReturn.Text);

                    decimal result = sale - purchase - Convert.ToDecimal(lblDiscount.Text) - Convert.ToDecimal(lblExpenses.Text);

                    if (result > 0)
                    {
                        lblTotalProfit.Text = string.Format("{0:0.00}", result);
                        lblTotalLoss.Text = "0.00";
                    }
                    else
                    {
                        lblTotalProfit.Text = "0.00";
                        lblTotalLoss.Text = string.Format("{0:0.00}", Math.Abs(result));
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
                        
        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFromDate.Value > dtpToDate.Value)
            {
                dtpToDate.Value = dtpFromDate.Value;
            }
            dtpToDate.MaxDate = DateTime.Now;
            LoadDatagrid();
        }

        private void dtpFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            dtpFromDate.MaxDate = DateTime.Now;
            LoadDatagrid();
        }
    }
}
