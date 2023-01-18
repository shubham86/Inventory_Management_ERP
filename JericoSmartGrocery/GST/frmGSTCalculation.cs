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

namespace JericoSmartGrocery.GST
{
    public partial class frmGSTCalculation : Form
    {
        public string month;
        public frmGSTCalculation()
        {
            InitializeComponent();
        }

        private void frmGSTCalculation_Load(object sender, EventArgs e)
        {
            for (int i = (DateTime.Now.Year - 4); i <= DateTime.Now.Year; i++)
            {
                cmbYear.Items.Add(i);
            }

            string[] ssize = month.Split(' ', '\t');
            this.cmbMonth.SelectedIndexChanged -= new EventHandler(cmbMonth_SelectedIndexChanged);
            cmbMonth.Text = ssize[0];
            this.cmbMonth.SelectedIndexChanged += new EventHandler(cmbMonth_SelectedIndexChanged);
            this.cmbYear.SelectedIndexChanged -= new EventHandler(cmbYear_SelectedIndexChanged);
            cmbYear.Text = ssize[1];
            this.cmbYear.SelectedIndexChanged += new EventHandler(cmbYear_SelectedIndexChanged);

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
                comm.CommandText = "Proc_GST_fetchMonthlyRecord";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("mName", cmbMonth.Text);
                comm.Parameters.AddWithValue("yName", cmbYear.Text);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS);

                if (DS.Tables.Count > 0)
                {
                    dgvSale.DataSource = DS.Tables[0];
                    dgvSaleReturn.DataSource = DS.Tables[1];

                    int i = 1;
                    foreach (DataGridViewRow row in dgvSale.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;
                    }

                    int j = 1;
                    foreach (DataGridViewRow row in dgvSaleReturn.Rows)
                    {
                        row.Cells[0].Value = j;
                        j++;
                    }

                    lblGstPurAmt.Text = string.Format("{0:0.00}", DS.Tables[2].Rows[0]["PurGST"]);
                    lblGstSaleAmt.Text = DS.Tables[2].Rows[0]["SaleGST"].ToString();
                    lblGstPurReturn.Text = DS.Tables[2].Rows[0]["PurReturnGST"].ToString();
                    lblGstSaleReturn.Text = DS.Tables[2].Rows[0]["SaleRetutnGST"].ToString();

                    lblTotalPur.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGstPurAmt.Text == "" ? "0.00" : lblGstPurAmt.Text) - Convert.ToDecimal(lblGstPurReturn.Text == "" ? "0.00" : lblGstPurReturn.Text)));
                    lblTotalSale.Text = string.Format("{0:0.00}", (Convert.ToDecimal(lblGstSaleAmt.Text == "" ? "0.00" : lblGstSaleAmt.Text) - Convert.ToDecimal(lblGstSaleReturn.Text == "" ? "0.00" : lblGstSaleReturn.Text)));

                    lblGSTPaid.Text = (Convert.ToDecimal(lblTotalSale.Text == "" ? "0.00" : lblTotalSale.Text) - Convert.ToDecimal(lblTotalPur.Text == "" ? "0.00" : lblTotalPur.Text)).ToString();
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

                dgvSale.ClearSelection();
                dgvSaleReturn.ClearSelection();
            }
        }

        private void addGSTEntry()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_GST_AddReturnEntry";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("mName", cmbMonth.Text + " " + cmbYear.Text);
                comm.Parameters.AddWithValue("amount", lblGSTPaid.Text);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                if (comm.ExecuteNonQuery() > 0)
                {
                    successAlert("GST Return entry successfull.");
                }
                else
                {
                    dangerAlert("Paid Entry Error!");
                }
            }

            catch (Exception ex)
            {
                string x = ex.ToString();
                dangerAlert("Paid Entry Error!");
                MessageBox.Show(x);
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Dispose();
                comm.Dispose();
            }
        }

        private async void successAlert(string msg)
        {
            lblAlert.Visible = true;
            pnlAlert.Visible = true;

            lblAlert.Text = msg;
            pnlAlert.BackColor = Color.FromArgb(48, 70, 38);
            lblAlert.ForeColor = Color.DarkSeaGreen;

            await Task.Delay(Convert.ToInt32(ConfigurationManager.AppSettings["alertTime"]));

            lblAlert.Visible = false;
            pnlAlert.Visible = false;
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

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDatagrid();
        }
        
        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDatagrid();
        }
        
        private void btnPaid_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure GST paid?", "Confirm Paid!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                addGSTEntry();
                Close();
            }
            else
            {
                return;
            }
        }
    }
}
