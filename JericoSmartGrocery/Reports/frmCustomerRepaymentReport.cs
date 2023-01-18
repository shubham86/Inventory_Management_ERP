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
    public partial class frmCustomerRepaymentReport : Form
    {
        public frmCustomerRepaymentReport()
        {
            InitializeComponent();
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
                comm.CommandText = "Proc_CustomerRepayment_Fetch";

                comm.Connection = conn;

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
                    dgvCustomerRepayment.DataSource = DS.Tables[0];

                    int i = 1;
                    foreach (DataGridViewRow row in dgvCustomerRepayment.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;
                    }
                }

                if (DS.Tables[0].Rows.Count > 0)
                {
                    dgvCustomerRepayment.DataSource = DS.Tables[0];
                    lblPaidTotal.Text = DS.Tables[0].Rows[0]["nowPaidAmount"].ToString() == "" ? "0.00" : DS.Tables[0].Rows[0]["nowPaidAmount"].ToString();
                }

                if (DS.Tables[1].Rows.Count > 0)
                {
                    lblPaidTotal.Text = DS.Tables[1].Rows[0]["totalPaidAmt"].ToString() == "" ? "0.00" : DS.Tables[1].Rows[0]["totalPaidAmt"].ToString();
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

                dgvCustomerRepayment.ClearSelection();
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

        private void frmCustomerRepaymentReport_Load_1(object sender, EventArgs e)
        {

            this.dtpTo.ValueChanged -= new EventHandler(dtpTo_ValueChanged);
            dtpTo.Value = DateTime.Now;
            this.dtpTo.ValueChanged += new EventHandler(dtpTo_ValueChanged);
            this.dtpFrom.ValueChanged -= new EventHandler(dtpFrom_ValueChanged);
            //int year = DateTime.Now.Year;
            //DateTime fromDay = new DateTime(year, 1, 1);
            dtpFrom.Value = DateTime.Now;
            this.dtpFrom.ValueChanged += new EventHandler(dtpFrom_ValueChanged);

            LoadDatagrid();
        }
    }
}
