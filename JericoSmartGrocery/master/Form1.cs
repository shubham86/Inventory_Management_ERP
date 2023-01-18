using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JericoSmartGrocery.master
{
    public partial class Form1 : Form
    {
        frmSaleRatePopUp frmSaleRatePopUp = new frmSaleRatePopUp();
        public Boolean varFromPurchase = false;
        public Decimal oldStock;
        string oldRate;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnUnitAdd.FlatAppearance.BorderColor = Color.FromArgb(177, 177, 177);
            btnBrandAdd.FlatAppearance.BorderColor = Color.FromArgb(177, 177, 177);
            btnGstAdd.FlatAppearance.BorderColor = Color.FromArgb(177, 177, 177);

            txtProductName.Select();
            fillBrandCmb();
            fillUnitCmb();
            fillGSTCmb();
            LoadDatagrid(1);

            cmbBrand.SelectedIndex = 0;
            cmbUnit.SelectedIndex = 0;
            cmbGst.SelectedIndex = 0;

            if (cmbGst.Items.Count > 1)
            {
                cmbGst.SelectedIndex = 1;
            }

            this.txtStock.Enter += new EventHandler(txtStock_Focus);
            this.txtSaleRate.Enter += new EventHandler(txtSlaeRate_Focus);
        }

        protected void txtStock_Focus(Object sender, EventArgs e)
        {
            txtStock.SelectAll();
        }

        protected void txtSlaeRate_Focus(Object sender, EventArgs e)
        {
            Opacity = .50;
            frmSaleRatePopUp.ShowDialog();
            Opacity = 1;

            txtSaleRate.Text = frmSaleRatePopUp.Less5 + " / " + frmSaleRatePopUp._5to10 + " / " + frmSaleRatePopUp.Greater10;
        }

        public void fillBrandCmb()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            DT.Columns.Add("brandName", typeof(string));
            DT.Columns.Add("brandID", typeof(int));
            DataRow row = DT.NewRow();
            row["brandID"] = "0";
            row["brandName"] = "-- Select Brand --";
            DT.Rows.InsertAt(row, 0);

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Brand_FetchinCmb";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count >= 0)
                {
                    cmbBrand.DataSource = DT;
                    cmbBrand.ValueMember = "brandID";
                    cmbBrand.DisplayMember = "brandName";
                }

            }
            catch (Exception ex)
            {
                cmbBrand.DataSource = DT;
                cmbBrand.ValueMember = "brandID";
                cmbBrand.DisplayMember = "brandName";

                string x = ex.ToString();
                dangerAlert("Brands Loading Error!");
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

        public void fillUnitCmb()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            DT.Columns.Add("unit", typeof(string));
            DT.Columns.Add("unitID", typeof(int));
            DataRow row = DT.NewRow();
            row["unitID"] = "0";
            row["unit"] = "-- Select Unit --";
            DT.Rows.InsertAt(row, 0);

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Unit_FetchinCmb";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count >= 0)
                {
                    cmbUnit.DataSource = DT;
                    cmbUnit.ValueMember = "unitID";
                    cmbUnit.DisplayMember = "unit";
                }

            }
            catch (Exception ex)
            {
                cmbUnit.DataSource = DT;
                cmbUnit.ValueMember = "unitID";
                cmbUnit.DisplayMember = "unit";

                string x = ex.ToString();
                dangerAlert("Unit Loading Error!");
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

        public void fillGSTCmb()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            DT.Columns.Add("slabName", typeof(string));
            DT.Columns.Add("gstID", typeof(int));
            DataRow row = DT.NewRow();
            row["gstID"] = "0";
            row["slabName"] = "-- Select GST Slab --";
            DT.Rows.InsertAt(row, 0);

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_gstSlab_FetchinCmb";

                comm.Connection = conn;

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count >= 0)
                {
                    cmbGst.DataSource = DT;
                    cmbGst.ValueMember = "gstID";
                    cmbGst.DisplayMember = "slabName";
                }

            }
            catch (Exception ex)
            {
                cmbGst.DataSource = DT;
                cmbGst.ValueMember = "gstID";
                cmbGst.DisplayMember = "slabName";

                string x = ex.ToString();
                dangerAlert("GST Loading Error!");
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveBtnClick();
        }

        private void saveBtnClick()
        {
            if (txtProductName.Text == "")
            {
                lblNameError.Visible = true;
                txtProductName.Focus();
            }
            else if (txtMarathiName.Text == "")
            {
                lblMarathiError.Visible = true;
                txtMarathiName.Focus();
            }
            else if (cmbUnit.SelectedIndex == 0)
            {
                lblUnitError.Visible = true;
                cmbUnit.Focus();
            }
            else
            {
                string retVal = addValidation();
                if (retVal == "true")
                {
                    addProduct();
                    if (varFromPurchase)
                    {
                        Close();
                    }
                }
                else if (retVal == "false")
                {
                    warningAlert("Product is Allready Exist.");
                }
                else
                {
                    dangerAlert("Error! Please Try Again.");
                }
                txtProductName.Focus();
            }
        }

        int PageSize = 100;
        private void LoadDatagrid(int PageIndex)
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataSet DS = new DataSet();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Product_FetchinGridview1";
                comm.Connection = conn;

                comm.Parameters.AddWithValue("pageIndex", PageIndex);
                comm.Parameters.AddWithValue("pageSize", PageSize);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DS);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    dgvProductDetails.DataSource = DS.Tables[1];
                    //int i = 1;
                    //foreach (DataGridViewRow row in dgvProductDetails.Rows)
                    //{
                    //    row.Cells[0].Value = i;
                    //    i++;
                    //}

                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        PopulatePager(Convert.ToInt32(DS.Tables[0].Rows[0]["rowCount"]), PageIndex);
                    }
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

                dgvProductDetails.ClearSelection();
            }
        }

        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;
            int pagerSpan = 5;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
            endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
            if (currentPage > pagerSpan % 2)
            {
                if (currentPage == 2)
                {
                    endIndex = 5;
                }
                else
                {
                    endIndex = currentPage + 2;
                }
            }
            else
            {
                endIndex = (pagerSpan - currentPage) + 1;
            }

            if (endIndex - (pagerSpan - 1) > startIndex)
            {
                startIndex = endIndex - (pagerSpan - 1);
            }

            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "First", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = "Last", Value = pageCount.ToString() });
            }

            //Clear existing Pager Buttons.
            pnlPager.Controls.Clear();

            //Loop and add Buttons for Pager.
            int count = 0;
            foreach (Page page in pages)
            {
                Button btnPage = new Button();
                btnPage.Location = new System.Drawing.Point(38 * count, 5);
                btnPage.Size = new System.Drawing.Size(38, 24);
                btnPage.Name = page.Value;
                btnPage.Text = page.Text;
                btnPage.ForeColor = Color.White;
                btnPage.Padding = new Padding(2, 2, 2, 2);
                btnPage.Enabled = !page.Selected;
                btnPage.Click += new System.EventHandler(this.Page_Click);
                pnlPager.Controls.Add(btnPage);
                count++;
            }
        }

        private void Page_Click(object sender, EventArgs e)
        {
            Button btnPager = (sender as Button);
            this.LoadDatagrid(int.Parse(btnPager.Name));

        }

        public class Page
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public bool Selected { get; set; }
        }

        private string addValidation()
        {
            string retVal = "";

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Product_Addvalidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Pname", txtProductName.Text);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    retVal = "false";
                }
                else
                {
                    retVal = "true";
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                retVal = "error";
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                comm.Dispose();
                conn.Dispose();

                dgvProductDetails.ClearSelection();
            }
            return retVal;
        }

        private void addProduct()
        {
            DAL.DAL_Product objDAL = new DAL.DAL_Product();
            BAL.BAL_Product objBAL = new BAL.BAL_Product();

            try
            {
                objDAL.productName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtProductName.Text.ToLower()).Trim());
                objDAL.marathiName = txtMarathiName.Text == "" ? "" : txtMarathiName.Text;
                objDAL.brandID = Convert.ToInt32(cmbBrand.SelectedValue.ToString() == "" ? "0" : cmbBrand.SelectedValue);
                objDAL.unitID = Convert.ToInt32(cmbUnit.SelectedValue.ToString() == "" ? "0" : cmbUnit.SelectedValue);
                objDAL.currentStock = Convert.ToDecimal(txtStock.Text == "" ? "0.00" : txtStock.Text);
                objDAL.rateLess5 = frmSaleRatePopUp.Less5;
                objDAL.rate5to10 = frmSaleRatePopUp._5to10;
                objDAL.rateGreater10 = frmSaleRatePopUp.Greater10;
                objDAL.gstID = Convert.ToInt32(cmbGst.SelectedValue.ToString() == "" ? "0" : cmbGst.SelectedValue);
                objDAL.description = txtDescription.Text == "" ? "" : txtDescription.Text;
                objDAL.purchaseRate = txtPurchaseRate.Text == "" ? Convert.ToDecimal("0.00") : Convert.ToDecimal(txtPurchaseRate.Text);

                if (objBAL.addProduct(objDAL))
                {
                    ClearForm();
                    LoadDatagrid(1);
                    successAlert("Product Added Successfully.");
                }
                else
                {
                    dangerAlert("Error! Please Try Again.");
                }
            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                dangerAlert("Error! Please Try Again.");
                MessageBox.Show(x);
            }
        }

        private string updateValidation()
        {
            string retVal = "";

            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Product_UpdateValidation";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("Id", lblID.Text);
                comm.Parameters.AddWithValue("Pname", txtProductName.Text);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    retVal = "false";
                }
                else
                {
                    retVal = "true";
                }

            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                retVal = "error";
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                comm.Dispose();
                conn.Dispose();

                dgvProductDetails.ClearSelection();
            }
            return retVal;
        }

        private void updateProduct()
        {
            DAL.DAL_Product objDAL = new DAL.DAL_Product();
            BAL.BAL_Product objBAL = new BAL.BAL_Product();

            try
            {
                objDAL.productID = Convert.ToInt32(lblID.Text);
                objDAL.productName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtProductName.Text.ToLower()).Trim());
                objDAL.marathiName = txtMarathiName.Text == "" ? "" : txtMarathiName.Text;
                objDAL.brandID = Convert.ToInt32(cmbBrand.SelectedValue.ToString() == "" ? "0" : cmbBrand.SelectedValue);
                objDAL.unitID = Convert.ToInt32(cmbUnit.SelectedValue.ToString() == "" ? "0" : cmbUnit.SelectedValue);
                objDAL.gstID = Convert.ToInt32(cmbGst.SelectedValue.ToString() == "" ? "0" : cmbGst.SelectedValue);
                objDAL.description = txtDescription.Text;

                if (objBAL.updateProduct(objDAL))
                {
                    LoadDatagrid(1);
                    successAlert("Product Updated Successfully.");
                }
                else
                {
                    dangerAlert("Error! Please Try Again.");
                }
            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                dangerAlert("Error! Please Try Again.");
                MessageBox.Show(x);
            }
        }

        private void updateProductPrice()
        {
            DAL.DAL_Product objDAL = new DAL.DAL_Product();
            BAL.BAL_Product objBAL = new BAL.BAL_Product();

            try
            {
                objDAL.productID = Convert.ToInt32(lblID.Text);
                objDAL.rateLess5 = frmSaleRatePopUp.Less5;
                objDAL.rate5to10 = frmSaleRatePopUp._5to10;
                objDAL.rateGreater10 = frmSaleRatePopUp.Greater10;


                if (objBAL.updateProductPrice(objDAL))
                {
                    successAlert("Product stock updated successfully.");
                }
                else
                {
                    dangerAlert("Error! Please Try Again.");
                }
            }
            catch (Exception ex)
            {
                string x = ex.ToString();
                dangerAlert("Error! Please Try Again.");
                MessageBox.Show(x);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updatebtnClick();
        }

        private void updatebtnClick()
        {
            if (txtProductName.Text == "")
            {
                lblNameError.Visible = true;
                txtProductName.Focus();
            }
            else if (txtMarathiName.Text == "")
            {
                lblMarathiError.Visible = true;
                txtMarathiName.Focus();
            }
            else if (cmbUnit.SelectedIndex == 0)
            {
                lblUnitError.Visible = true;
                cmbUnit.Focus();
            }
            else
            {
                string retVal = updateValidation();
                if (retVal == "true")
                {
                    updateProduct();
                    btnUpdate.Visible = false;
                }
                else if (retVal == "false")
                {
                    warningAlert("Product is Allready Exist.");
                }
                else
                {
                    dangerAlert("Error! Please Try Again.");
                }

                if (oldRate != txtSaleRate.Text.Replace(" ", string.Empty))
                {
                    updateProductPrice();
                }

                ClearForm();
                LoadDatagrid(1);
                txtProductName.Focus();
            }
        }

        private void searchProduct()
        {
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DBCONN_Grocery"].ConnectionString);
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DataTable DT = new DataTable();

            try
            {
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "Proc_Product_Search";

                comm.Connection = conn;

                comm.Parameters.AddWithValue("product", CultureInfo.CurrentCulture.TextInfo.ToTitleCase((txtProductName.Text.ToLower()).Trim()));

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                DA = new MySqlDataAdapter(comm);
                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    dgvProductDetails.DataSource = DT;

                    int i = 1;
                    foreach (DataGridViewRow row in dgvProductDetails.Rows)
                    {
                        row.Cells[0].Value = i;
                        i++;
                    }
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
            }
        }

        public void ClearForm()
        {
            btnSave.TabIndex = 10;
            btnUpdate.TabIndex = 13;
            txtProductName.Text = "";
            txtMarathiName.Text = "";
            txtDescription.Text = "";
            txtStock.Text = "0.00";
            txtSaleRate.Text = "0.00";
            txtPurchaseRate.Text = "0.00";
            cmbBrand.SelectedIndex = 0;
            cmbGst.SelectedIndex = 0;
            cmbUnit.SelectedIndex = 0;

            lblNameError.Visible = false;
            lblSaleRateError.Visible = false;
            lblMarathiError.Visible = false;
            lblUnitError.Visible = false;

            frmSaleRatePopUp.Less5 = Convert.ToDecimal(0.00);
            frmSaleRatePopUp._5to10 = Convert.ToDecimal(0.00);
            frmSaleRatePopUp.Greater10 = Convert.ToDecimal(0.00);

            LoadDatagrid(1);
            txtProductName.Focus();
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

        private void dgvProductDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                lblID.Text = dgvProductDetails.Rows[e.RowIndex].Cells[1].Value.ToString();

                this.txtProductName.TextChanged -= new EventHandler(txtProductName_TextChanged);
                txtProductName.Text = dgvProductDetails.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.txtProductName.TextChanged += new EventHandler(txtProductName_TextChanged);

                txtMarathiName.Text = dgvProductDetails.Rows[e.RowIndex].Cells[3].Value.ToString();
                cmbBrand.Text = dgvProductDetails.Rows[e.RowIndex].Cells[4].Value.ToString() == "-" ? "-- Select Brand --" : dgvProductDetails.Rows[e.RowIndex].Cells[4].Value.ToString();
                cmbUnit.Text = dgvProductDetails.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtStock.Text = dgvProductDetails.Rows[e.RowIndex].Cells[7].Value.ToString();
                oldStock = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells[7].Value.ToString() == "" ? "0.00" : dgvProductDetails.Rows[e.RowIndex].Cells[7].Value);
                cmbGst.Text = dgvProductDetails.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtSaleRate.Text = dgvProductDetails.Rows[e.RowIndex].Cells[8].Value.ToString().Replace("/", " / ");
                oldRate = dgvProductDetails.Rows[e.RowIndex].Cells[8].Value.ToString();
                txtDescription.Text = dgvProductDetails.Rows[e.RowIndex].Cells[9].Value.ToString() == "-" ? "" : dgvProductDetails.Rows[e.RowIndex].Cells[9].Value.ToString();

                frmSaleRatePopUp.Less5 = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells[10].Value.ToString() == "" ? "0.00" : dgvProductDetails.Rows[e.RowIndex].Cells[10].Value);
                frmSaleRatePopUp._5to10 = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells[11].Value.ToString() == "" ? "0.00" : dgvProductDetails.Rows[e.RowIndex].Cells[11].Value);
                frmSaleRatePopUp.Greater10 = Convert.ToDecimal(dgvProductDetails.Rows[e.RowIndex].Cells[12].Value.ToString() == "" ? "0.00" : dgvProductDetails.Rows[e.RowIndex].Cells[12].Value);

                btnUpdate.Visible = true;
                txtPurchaseRate.Enabled = false;
                txtStock.Enabled = false;
                txtProductName.Select();
            }

            if (btnUpdate.Visible)
            {
                btnSave.TabIndex = 13;
                btnUpdate.TabIndex = 10;
            }
            else
            {
                btnSave.TabIndex = 10;
                btnUpdate.TabIndex = 13;
            }

            txtProductName.Focus();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            btnUpdate.Visible = false;
            txtStock.Enabled = true;
            txtPurchaseRate.Enabled = true;
            txtSaleRate.Enabled = true;
            dgvProductDetails.ClearSelection();

            fillBrandCmb();
            fillGSTCmb();
            fillUnitCmb();

            cmbGst.SelectedIndex = 1;
            txtProductName.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtStock_Leave(object sender, EventArgs e)
        {

        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtProductName_Leave(object sender, EventArgs e)
        {
            txtProductName.DeselectAll();
        }

        private void txtMarathiName_Leave(object sender, EventArgs e)
        {
            txtMarathiName.DeselectAll();
        }

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            txtDescription.DeselectAll();
        }

        private void btnBrandAdd_Click(object sender, EventArgs e)
        {
            frmAddBrand frmAddBrand = new frmAddBrand();
            frmAddBrand.varFromProduct = true;
            Opacity = .50;
            frmAddBrand.ShowDialog();
            Opacity = 1;

            fillBrandCmb();
            cmbBrand.Focus();
        }

        private void btnUnitAdd_Click(object sender, EventArgs e)
        {
            frmAddUnits frmAddUnits = new frmAddUnits();
            frmAddUnits.varFromProduct = true;
            Opacity = .50;
            frmAddUnits.ShowDialog();
            Opacity = 1;

            fillUnitCmb();
            cmbUnit.Focus();
        }

        private void btnGstAdd_Click(object sender, EventArgs e)
        {
            frmAddGstSlab frmAddGstSlab = new frmAddGstSlab();
            frmAddGstSlab.varFromProduct = true;
            Opacity = .50;
            frmAddGstSlab.ShowDialog();
            Opacity = 1;

            fillGSTCmb();
            cmbGst.Focus();
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            searchProduct();
        }

        private void txtMarathiName_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblMarathiError.Visible = false;
        }

        private void txtSlaeRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblSaleRateError.Visible = false;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtSlaeRate_Leave(object sender, EventArgs e)
        {
            txtSaleRate.DeselectAll();
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblUnitError.Visible = false;
        }

        private void txtProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblNameError.Visible = false;
        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgvProductDetails.ClearSelection();
                if (dgvProductDetails.RowCount > 0)
                {
                    dgvProductDetails.Rows[0].Selected = true;
                    dgvProductDetails.Select();
                }
            }
        }

        private void dgvProductDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lblID.Text = dgvProductDetails.SelectedRows[0].Cells[1].Value.ToString();

                this.txtProductName.TextChanged -= new EventHandler(txtProductName_TextChanged);
                txtProductName.Text = dgvProductDetails.SelectedRows[0].Cells[2].Value.ToString();
                this.txtProductName.TextChanged += new EventHandler(txtProductName_TextChanged);

                txtMarathiName.Text = dgvProductDetails.SelectedRows[0].Cells[3].Value.ToString();
                cmbBrand.Text = dgvProductDetails.SelectedRows[0].Cells[4].Value.ToString() == "-" ? "-- Select Brand --" : dgvProductDetails.SelectedRows[0].Cells[4].Value.ToString();
                cmbUnit.Text = dgvProductDetails.SelectedRows[0].Cells[5].Value.ToString();
                txtStock.Text = dgvProductDetails.SelectedRows[0].Cells[7].Value.ToString();
                oldStock = Convert.ToDecimal(dgvProductDetails.SelectedRows[0].Cells[7].Value.ToString() == "");
                cmbGst.Text = dgvProductDetails.SelectedRows[0].Cells[6].Value.ToString();
                txtSaleRate.Text = dgvProductDetails.SelectedRows[0].Cells[8].Value.ToString().Replace("/", " / ");
                oldRate = dgvProductDetails.SelectedRows[0].Cells[8].Value.ToString();
                txtDescription.Text = dgvProductDetails.SelectedRows[0].Cells[9].Value.ToString() == "-" ? "" : dgvProductDetails.SelectedRows[0].Cells[9].Value.ToString();

                frmSaleRatePopUp.Less5 = Convert.ToDecimal(dgvProductDetails.SelectedRows[0].Cells[10].Value.ToString() == "" ? "0.00" : dgvProductDetails.SelectedRows[0].Cells[10].Value);
                frmSaleRatePopUp._5to10 = Convert.ToDecimal(dgvProductDetails.SelectedRows[0].Cells[11].Value.ToString() == "" ? "0.00" : dgvProductDetails.SelectedRows[0].Cells[11].Value);
                frmSaleRatePopUp.Greater10 = Convert.ToDecimal(dgvProductDetails.SelectedRows[0].Cells[12].Value.ToString() == "" ? "0.00" : dgvProductDetails.SelectedRows[0].Cells[12].Value);

                btnUpdate.Visible = true;
                txtStock.Enabled = false;
                txtPurchaseRate.Enabled = false;
                txtProductName.Select();

                if (btnUpdate.Visible)
                {
                    btnSave.TabIndex = 13;
                    btnUpdate.TabIndex = 10;
                }
                else
                {
                    btnSave.TabIndex = 10;
                    btnUpdate.TabIndex = 13;
                }

                dgvProductDetails.ClearSelection();
                txtProductName.Focus();

                e.Handled = true;
            }
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (btnUpdate.Visible)
                {
                    updatebtnClick();
                }
                else
                {
                    saveBtnClick();
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (varFromPurchase)
                {
                    varFromPurchase = false;
                    this.Close();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtStock_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtSaleRate_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtStock_Click(object sender, EventArgs e)
        {
            txtStock.SelectAll();
        }

        private void cmbBrand_Leave(object sender, EventArgs e)
        {
            int index = cmbBrand.FindString(cmbBrand.Text);
            if (index < 0)
            {
                cmbBrand.SelectedIndex = 0;
            }
            else
            {
                cmbBrand.SelectedIndex = index;
            }
        }

        private void cmbUnit_Leave(object sender, EventArgs e)
        {
            int index = cmbUnit.FindString(cmbUnit.Text);
            if (index < 0)
            {
                cmbUnit.SelectedIndex = 0;
            }
            else
            {
                cmbUnit.SelectedIndex = index;
            }
        }

        private void cmbGst_Leave(object sender, EventArgs e)
        {

        }

        private void txtPurchaseRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblPurchaseRateError.Visible = false;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPurchaseRate_Leave(object sender, EventArgs e)
        {
            txtPurchaseRate.DeselectAll();
        }

        private void txtMarathiName_TextChanged(object sender, EventArgs e)
        {
                
        }
    }
}
