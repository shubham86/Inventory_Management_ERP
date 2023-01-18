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

namespace JericoSmartGrocery.main
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
        }

        private void verifyLogin()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Login_Verification";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("UID", txtUserId.Text);
                comm.Parameters.AddWithValue("pass", txtPassword.Text);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    Main.frmDashboard frmDashboard = new Main.frmDashboard();

                    frmDashboard.userName = DT.Rows[0]["fullName"].ToString();
                    Global.userRole = DT.Rows[0]["userRole"].ToString();

                    Hide();
                    frmDashboard.ShowDialog();
                    Dispose();
                }
                else
                {
                    dangerAlert("Invalid username and password.");
                    txtPassword.Text = "";
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserId.Text == "")
            {
                lblUserNameError.Visible = true;
            }
            else if (txtPassword.Text == "")
            {
                lblPaswordError.Visible = true;
            }
            else
            {
                verifyLogin();
            }            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtUserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblUserNameError.Visible = false;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblPaswordError.Visible = false;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUserId.Text == "")
                {
                    lblUserNameError.Visible = true;
                }
                else if (txtPassword.Text == "")
                {
                    lblPaswordError.Visible = true;
                }
                else
                {
                    verifyLogin();
                }
            }
        }
    }
}

public static class Global
{
    public static string userRole;
}