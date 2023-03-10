namespace JericoSmartGrocery.PaymentMaster
{
    partial class frmCustomerCashReturn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlAlert = new System.Windows.Forms.Panel();
            this.lblAlert = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrevAdvance = new System.Windows.Forms.TextBox();
            this.txtAdvanceAmt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtPrevBalance = new System.Windows.Forms.TextBox();
            this.txtBalanceAmt = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtReturnAmt = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.lblPaidAmtError = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.panel2.SuspendLayout();
            this.pnlAlert.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(188, 429);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 35);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(440, 429);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(106, 35);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(315, 429);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(106, 35);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.panel2.Controls.Add(this.pnlAlert);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 499);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(736, 36);
            this.panel2.TabIndex = 138;
            // 
            // pnlAlert
            // 
            this.pnlAlert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.pnlAlert.Controls.Add(this.lblAlert);
            this.pnlAlert.Location = new System.Drawing.Point(222, 0);
            this.pnlAlert.Name = "pnlAlert";
            this.pnlAlert.Size = new System.Drawing.Size(292, 36);
            this.pnlAlert.TabIndex = 63;
            this.pnlAlert.Visible = false;
            // 
            // lblAlert
            // 
            this.lblAlert.AutoSize = true;
            this.lblAlert.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlert.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblAlert.Location = new System.Drawing.Point(63, 11);
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(35, 14);
            this.lblAlert.TabIndex = 0;
            this.lblAlert.Text = "Alert";
            this.lblAlert.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(279, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(189, 19);
            this.label6.TabIndex = 1;
            this.label6.Text = "Customers Return Cash";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(736, 36);
            this.panel3.TabIndex = 139;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.lblName.Location = new System.Drawing.Point(252, 54);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(112, 15);
            this.lblName.TabIndex = 141;
            this.lblName.Text = "Customer Name";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label1);
            this.panel5.Controls.Add(this.txtPrevAdvance);
            this.panel5.Controls.Add(this.txtAdvanceAmt);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.label18);
            this.panel5.Controls.Add(this.txtPrevBalance);
            this.panel5.Controls.Add(this.txtBalanceAmt);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Location = new System.Drawing.Point(61, 88);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(613, 209);
            this.panel5.TabIndex = 140;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(358, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 14);
            this.label1.TabIndex = 106;
            this.label1.Text = "Prev Advance Amount";
            // 
            // txtPrevAdvance
            // 
            this.txtPrevAdvance.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtPrevAdvance.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrevAdvance.HideSelection = false;
            this.txtPrevAdvance.Location = new System.Drawing.Point(338, 30);
            this.txtPrevAdvance.MaxLength = 100;
            this.txtPrevAdvance.Name = "txtPrevAdvance";
            this.txtPrevAdvance.Size = new System.Drawing.Size(177, 25);
            this.txtPrevAdvance.TabIndex = 105;
            this.txtPrevAdvance.TabStop = false;
            this.txtPrevAdvance.Text = "0.00";
            this.txtPrevAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtAdvanceAmt
            // 
            this.txtAdvanceAmt.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtAdvanceAmt.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdvanceAmt.HideSelection = false;
            this.txtAdvanceAmt.Location = new System.Drawing.Point(338, 116);
            this.txtAdvanceAmt.Name = "txtAdvanceAmt";
            this.txtAdvanceAmt.ReadOnly = true;
            this.txtAdvanceAmt.Size = new System.Drawing.Size(177, 33);
            this.txtAdvanceAmt.TabIndex = 103;
            this.txtAdvanceAmt.TabStop = false;
            this.txtAdvanceAmt.Text = "0.00";
            this.txtAdvanceAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(125, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 14);
            this.label2.TabIndex = 67;
            this.label2.Text = "Prev Balance Amount";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label18.Location = new System.Drawing.Point(349, 162);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(154, 14);
            this.label18.TabIndex = 104;
            this.label18.Text = "Current Advance Amount";
            // 
            // txtPrevBalance
            // 
            this.txtPrevBalance.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtPrevBalance.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrevBalance.HideSelection = false;
            this.txtPrevBalance.Location = new System.Drawing.Point(103, 30);
            this.txtPrevBalance.MaxLength = 100;
            this.txtPrevBalance.Name = "txtPrevBalance";
            this.txtPrevBalance.ReadOnly = true;
            this.txtPrevBalance.Size = new System.Drawing.Size(177, 25);
            this.txtPrevBalance.TabIndex = 66;
            this.txtPrevBalance.TabStop = false;
            this.txtPrevBalance.Text = "0.00";
            this.txtPrevBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtBalanceAmt
            // 
            this.txtBalanceAmt.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtBalanceAmt.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalanceAmt.HideSelection = false;
            this.txtBalanceAmt.Location = new System.Drawing.Point(103, 116);
            this.txtBalanceAmt.Name = "txtBalanceAmt";
            this.txtBalanceAmt.ReadOnly = true;
            this.txtBalanceAmt.Size = new System.Drawing.Size(177, 33);
            this.txtBalanceAmt.TabIndex = 100;
            this.txtBalanceAmt.TabStop = false;
            this.txtBalanceAmt.Text = "0.00";
            this.txtBalanceAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label16.Location = new System.Drawing.Point(108, 161);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(167, 14);
            this.label16.TabIndex = 101;
            this.label16.Text = "Remaining Balance Amount";
            // 
            // txtReturnAmt
            // 
            this.txtReturnAmt.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtReturnAmt.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnAmt.HideSelection = false;
            this.txtReturnAmt.Location = new System.Drawing.Point(280, 339);
            this.txtReturnAmt.Name = "txtReturnAmt";
            this.txtReturnAmt.Size = new System.Drawing.Size(177, 33);
            this.txtReturnAmt.TabIndex = 1;
            this.txtReturnAmt.Text = "0.00";
            this.txtReturnAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtReturnAmt.Click += new System.EventHandler(this.txtReturnAmt_Click);
            this.txtReturnAmt.TextChanged += new System.EventHandler(this.txtReturnAmt_TextChanged);
            this.txtReturnAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReturnAmt_KeyPress);
            this.txtReturnAmt.Leave += new System.EventHandler(this.txtReturnAmt_Leave);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label17.Location = new System.Drawing.Point(308, 381);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(127, 14);
            this.label17.TabIndex = 143;
            this.label17.Text = "Return Cash Amount";
            // 
            // lblPaidAmtError
            // 
            this.lblPaidAmtError.AutoSize = true;
            this.lblPaidAmtError.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblPaidAmtError.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaidAmtError.ForeColor = System.Drawing.Color.DarkRed;
            this.lblPaidAmtError.Location = new System.Drawing.Point(402, 349);
            this.lblPaidAmtError.Name = "lblPaidAmtError";
            this.lblPaidAmtError.Size = new System.Drawing.Size(50, 13);
            this.lblPaidAmtError.TabIndex = 144;
            this.lblPaidAmtError.Text = "Required";
            this.lblPaidAmtError.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(525, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 14);
            this.label4.TabIndex = 146;
            this.label4.Text = "Date";
            // 
            // dtpDate
            // 
            this.dtpDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(571, 52);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(103, 22);
            this.dtpDate.TabIndex = 145;
            // 
            // frmCustomerCashReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(53)))), ((int)(((byte)(55)))));
            this.ClientSize = new System.Drawing.Size(736, 535);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblPaidAmtError);
            this.Controls.Add(this.txtReturnAmt);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnReset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCustomerCashReturn";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmCustomerCashReturn_Load);
            this.panel2.ResumeLayout(false);
            this.pnlAlert.ResumeLayout(false);
            this.pnlAlert.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlAlert;
        private System.Windows.Forms.Label lblAlert;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrevAdvance;
        private System.Windows.Forms.TextBox txtAdvanceAmt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtPrevBalance;
        private System.Windows.Forms.TextBox txtBalanceAmt;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtReturnAmt;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblPaidAmtError;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDate;
    }
}