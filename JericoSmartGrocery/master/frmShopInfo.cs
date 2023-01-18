using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Globalization;

namespace JericoSmartGrocery.master
{
    public partial class frmShopInfo : Form
    {
        public frmShopInfo()
        {
            InitializeComponent();
        }

        private void clearForm()
        {
            txtShopName.Text = "";
            txtName.Text = "";
            txtMobileOne.Text = "";
            txtMobileTwo.Text = "";
            txtEmail.Text = "";
            txtGST.Text = "";
            txtAddress.Text = "";
            txtTermOne.Text = "";
            txtTermtwo.Text = "";
            txtTermThree.Text = "";
            txtName.Select();
        }

        private void loadData()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand com = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "Proc_aboutShop_Fetchin";

                com.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                DA = new MySqlDataAdapter(com);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    ID.Text = DT.Rows[0]["shopID"].ToString();
                    txtShopName.Text = DT.Rows[0]["shopName"].ToString();
                    txtName.Text = DT.Rows[0]["ownerName"].ToString();
                    txtMobileOne.Text = DT.Rows[0]["contactOne"].ToString();
                    txtMobileTwo.Text = DT.Rows[0]["contactTwo"].ToString();
                    txtAddress.Text = DT.Rows[0]["shopAddress"].ToString();
                    txtGST.Text = DT.Rows[0]["gstNumber"].ToString(); 
                    txtEmail.Text = DT.Rows[0]["emailID"].ToString();
                    txtTermOne.Text = DT.Rows[0]["termsConditions"].ToString();
                    txtTermtwo.Text = DT.Rows[0]["termsConditions2"].ToString();
                    txtTermThree.Text = DT.Rows[0]["termsConditions3"].ToString();

                    btnUpdate.Visible = true;
                    btnSave.Visible = false;

                }
                else
                {
                    btnUpdate.Visible = false;
                    btnSave.Visible = true;
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

                com.Dispose();
                conn.Dispose();

            }
        }

        private string validate()
        {
            string retValue = "";
            try
            {
                string email = txtEmail.Text;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);

                
                if (txtName.Text == "")
                {
                    lblNameError.Visible = true;
                    txtName.Focus();
                    retValue = "false";
                }

                else if (txtAddress.Text == "")
                {
                    lblAddress.Visible = true;
                    txtAddress.Focus();
                    retValue = "false";
                }

                else if (txtMobileOne.Text == "")
                {
                    lblContact.Visible = true;
                    txtMobileOne.Focus();
                    retValue = "false";
                }

                else if (txtMobileOne.TextLength < 10)
                {
                    lblInvalidContact.Visible = true;
                    lblContact.Visible = false;
                    txtMobileOne.Focus();
                    retValue = "false";
                }

                else if (txtMobileOne.Text == "")
                {
                    lblContact.Visible = true;
                    txtMobileOne.Focus();
                    retValue = "false";
                }

                else if (txtMobileOne.TextLength < 10)
                {
                    lblInvalidContact.Visible = true;
                    lblContact.Visible = false;
                    txtMobileOne.Focus();
                    retValue = "false";
                }

                else if (txtShopName.Text == "")
                {
                    lblShopName.Visible = true;
                    txtShopName.Focus();
                    retValue = "false";
                }
                else
                {
                    retValue = "true";
                }


            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                retValue = "error";

            }
            finally
            {
               
            }
            return retValue;
         }

        public void addShopInfo()
        {           
           BAL.BAL_AboutShop objBAL = new BAL.BAL_AboutShop();
           DAL.DAL_AboutShop objDAL = new DAL.DAL_AboutShop();

            try
            {
                objDAL.ownerName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtName.Text.ToLower())).Trim();
                objDAL.shopName = txtShopName.Text;
                objDAL.contactOne = txtMobileOne.Text;
                objDAL.contactTwo = txtMobileTwo.Text;
                objDAL.address = txtAddress.Text;
                objDAL.emailID = txtEmail.Text;
                objDAL.gst = txtGST.Text;
                objDAL.termOne = txtTermOne.Text;
                objDAL.termTwo = txtTermtwo.Text;
                objDAL.termThree = txtTermThree.Text;

                if (objBAL.addShopInfo(objDAL))
                {
                    clearForm();
                    successAlert("Add Successful");

                }
                else
                {
                    dangerAlert("Not Success");
                }


            }
            catch(Exception ex)
            {
                string x = ex.ToString();
                MessageBox.Show(x);
            }
        }

        public void updateShopInfo()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "PROC_aboutShopUpdate";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Id", ID.Text);
                comm.Parameters.AddWithValue("ownerName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtName.Text.ToLower())).Trim());
                comm.Parameters.AddWithValue("contactOne", txtMobileOne.Text);
                comm.Parameters.AddWithValue("contactTwo", txtMobileTwo.Text);
                comm.Parameters.AddWithValue("email", txtEmail.Text);
                comm.Parameters.AddWithValue("address", txtAddress.Text);
                comm.Parameters.AddWithValue("gstNumber", txtGST.Text);
                comm.Parameters.AddWithValue("shopName", txtShopName.Text);
                comm.Parameters.AddWithValue("termsConditions", txtTermOne.Text);
                comm.Parameters.AddWithValue("termsConditions2", txtTermtwo.Text);
                comm.Parameters.AddWithValue("termsConditions3", txtTermThree.Text);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                int x = comm.ExecuteNonQuery();
                if ( x> 0)
                {
                    successAlert("Update Success");
                }
                else
                {
                    dangerAlert("Update Unsuccessful");
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
                conn.Dispose();
                comm.Dispose();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            validate();
            string retValue = validate();
            if (retValue == "true")
            {
                addShopInfo();
            }
            else
            {
                dangerAlert("Entry Unsuccessful");
            }


        }

        private void frmShopInfo_Load(object sender, EventArgs e)
        {
            clearForm();
            loadData();

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            lblNameError.Visible = false;
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
            lblAddress.Visible = false;
        }

        private void txtMobileOne_TextChanged(object sender, EventArgs e)
        {
            lblContact.Visible = false;
            lblInvalidContact.Visible = false;
        }

        private void txtShopName_TextChanged(object sender, EventArgs e)
        {
            lblShopName.Visible = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            validate();
            string retValue = validate();
            if (retValue == "true")
            {
                updateShopInfo();
            }
            else
            {
                
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

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtName.DeselectAll();
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            txtAddress.DeselectAll();
        }

        private void txtMobileOne_Leave(object sender, EventArgs e)
        {
            txtMobileOne.DeselectAll();
        }

        private void txtMobileTwo_Leave(object sender, EventArgs e)
        {
            txtMobileTwo.DeselectAll();
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            txtEmail.DeselectAll();
        }

        private void txtGST_Leave(object sender, EventArgs e)
        {
            txtGST.DeselectAll();
        }

        private void txtShopName_Leave(object sender, EventArgs e)
        {
            txtShopName.DeselectAll();
        }

        private void txtTermOne_Leave(object sender, EventArgs e)
        {
            txtTermOne.DeselectAll();
        }

        private void txtTermtwo_TextChanged(object sender, EventArgs e)
        {
            txtTermtwo.DeselectAll();
        }

        private void txtTermThree_TextChanged(object sender, EventArgs e)
        {
            txtTermThree.DeselectAll();
        }
    }
}
