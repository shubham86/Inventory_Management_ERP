using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JericoSmartGrocery.purchaseMaster
{
    public partial class frmRefrenceNoPopup : Form
    {        
        public frmRefrenceNoPopup()
        {
            InitializeComponent();
        }

        private void frmRefrenceNoPopup_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            refNumber.refrenceNo = txtRefrenceNo.Text;
            Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtRefrenceNo.Text = "";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                txtRefrenceNo.Focus();
                this.Close();
                return true;
            }
            else if (keyData == Keys.F1)
            {
                txtRefrenceNo.Focus();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
