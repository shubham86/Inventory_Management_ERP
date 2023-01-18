using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JericoSmartGrocery.master
{
    public partial class frmSaleRatePopUp : Form
    {
        public Decimal Less5 = Convert.ToDecimal("0.00");
        public Decimal _5to10 = Convert.ToDecimal("0.00");
        public Decimal Greater10 = Convert.ToDecimal("0.00");
        public frmSaleRatePopUp()
        {
            InitializeComponent();
        }

        private void frmSaleRatePopUp_Load(object sender, EventArgs e)
        {
            txtLessThan5.Text = Less5.ToString();
            txt5to10.Text = _5to10.ToString();
            txtGreaterThan10.Text = Greater10.ToString();

            this.txtLessThan5.Enter += new EventHandler(txtLessThan5_Focus);
            this.txt5to10.Enter += new EventHandler(txt5to10_Focus);
            this.txtGreaterThan10.Enter += new EventHandler(txtGreaterThan10_Focus);

            txtLessThan5.Focus();
        }

        protected void txtLessThan5_Focus(Object sender, EventArgs e)
        {
            txtLessThan5.SelectAll();
        }

        protected void txt5to10_Focus(Object sender, EventArgs e)
        {
            txt5to10.SelectAll();
        }

        protected void txtGreaterThan10_Focus(Object sender, EventArgs e)
        {
            txtGreaterThan10.SelectAll();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            txtLessThan5.Focus();
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtLessThan5.Text = "0.00";
            txt5to10.Text = "0.00";
            txtGreaterThan10.Text = "0.00";

            txtLessThan5.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Less5 = Convert.ToDecimal(txtLessThan5.Text == "" ? "0.00" : txtLessThan5.Text);
            _5to10 = Convert.ToDecimal(txt5to10.Text == "" ? "0.00" : txt5to10.Text);
            Greater10 = Convert.ToDecimal(txtGreaterThan10.Text == "" ? "0.00" : txtGreaterThan10.Text);

            Close();
        }

        private void txtLessThan5_Leave(object sender, EventArgs e)
        {
            txtLessThan5.DeselectAll();
            txtLessThan5.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtLessThan5.Text == "" ? "0.00" : txtLessThan5.Text));

            if (txt5to10.Text == "0.00" && txtGreaterThan10.Text == "0.00")
            {
                txt5to10.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtLessThan5.Text == "" ? "0.00" : txtLessThan5.Text));
                txtGreaterThan10.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtLessThan5.Text == "" ? "0.00" : txtLessThan5.Text));
            }            
        }

        private void txt5to10_Leave(object sender, EventArgs e)
        {
            txt5to10.DeselectAll();
            txt5to10.Text = string.Format("{0:0.00}", Convert.ToDecimal(txt5to10.Text == "" ? "0.00" : txt5to10.Text));
        }

        private void txtGreaterThan10_Leave(object sender, EventArgs e)
        {
            txtGreaterThan10.DeselectAll();
            txtGreaterThan10.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtGreaterThan10.Text == "" ? "0.00" : txtGreaterThan10.Text));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                txtLessThan5.Focus();
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtLessThan5_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt5to10_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtGreaterThan10_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtLessThan5_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txt5to10_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtGreaterThan10_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        private void txtLessThan5_Click(object sender, EventArgs e)
        {
            txtLessThan5.SelectAll();
        }

        private void txt5to10_Click(object sender, EventArgs e)
        {
            txt5to10.SelectAll();
        }

        private void txtGreaterThan10_Click(object sender, EventArgs e)
        {
            txtGreaterThan10.SelectAll();
        }

        private void txtLessThan5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt5to10.Text == "0.00" && txtGreaterThan10.Text == "0.00")
                {
                    txtLessThan5.Text = string.Format("{0:0.00}", Convert.ToDecimal(txtLessThan5.Text == "" ? "0.00" : txtLessThan5.Text));
                    txt5to10.Text = txtLessThan5.Text;
                    txtGreaterThan10.Text = txtLessThan5.Text;
                }

                Less5 = Convert.ToDecimal(txtLessThan5.Text == "" ? "0.00" : string.Format("{0:0.00}", txtLessThan5.Text));
                _5to10 = Convert.ToDecimal(txt5to10.Text == "" ? "0.00" : string.Format("{0:0.00}", txt5to10.Text));
                Greater10 = Convert.ToDecimal(txtGreaterThan10.Text == "" ? "0.00" : string.Format("{0:0.00}", txtGreaterThan10.Text));

                Close();

                e.Handled = true;
            }
        }

        private void txt5to10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Less5 = Convert.ToDecimal(txtLessThan5.Text == "" ? "0.00" : string.Format("{0:0.00}", txtLessThan5.Text));
                _5to10 = Convert.ToDecimal(txt5to10.Text == "" ? "0.00" : string.Format("{0:0.00}", txt5to10.Text));
                Greater10 = Convert.ToDecimal(txtGreaterThan10.Text == "" ? "0.00" : string.Format("{0:0.00}", txtGreaterThan10.Text));

                Close();

                e.Handled = true;
            }
        }

        private void txtGreaterThan10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Less5 = Convert.ToDecimal(txtLessThan5.Text == "" ? "0.00" : string.Format( "{0:0.00}", txtLessThan5.Text));
                _5to10 = Convert.ToDecimal(txt5to10.Text == "" ? "0.00" : string.Format("{0:0.00}", txt5to10.Text));
                Greater10 = Convert.ToDecimal(txtGreaterThan10.Text == "" ? "0.00" : string.Format("{0:0.00}", txtGreaterThan10.Text));

                Close();

                e.Handled = true;
            }
        }
    }
}
