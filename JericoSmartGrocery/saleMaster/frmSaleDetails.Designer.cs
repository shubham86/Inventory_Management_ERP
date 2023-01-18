namespace JericoSmartGrocery.saleMaster
{
    partial class frmSaleDetails
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
            this.label6 = new System.Windows.Forms.Label();
            this.txtSaleQty = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblCustomerID = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblUnit1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtgstAmount = new System.Windows.Forms.TextBox();
            this.lblBrandName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSaleRate = new System.Windows.Forms.TextBox();
            this.lblQtyError = new System.Windows.Forms.Label();
            this.txtReturnQty = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSaleQty = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblgst = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAdvanceAmount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBalanceAmount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPaidAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalPrice = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.pnlAlert = new System.Windows.Forms.Panel();
            this.lblAlert = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblPrevBalance = new System.Windows.Forms.TextBox();
            this.lblPrevAdvance = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlAlert.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(373, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 19);
            this.label6.TabIndex = 1;
            this.label6.Text = "Sale Details";
            // 
            // txtSaleQty
            // 
            this.txtSaleQty.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtSaleQty.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaleQty.HideSelection = false;
            this.txtSaleQty.Location = new System.Drawing.Point(12, 59);
            this.txtSaleQty.MaxLength = 50;
            this.txtSaleQty.Name = "txtSaleQty";
            this.txtSaleQty.Size = new System.Drawing.Size(36, 25);
            this.txtSaleQty.TabIndex = 194;
            this.txtSaleQty.Text = "0.00";
            this.txtSaleQty.Visible = false;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblID.Location = new System.Drawing.Point(897, 59);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(18, 14);
            this.lblID.TabIndex = 176;
            this.lblID.Text = "id";
            this.lblID.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(833, 36);
            this.panel3.TabIndex = 174;
            // 
            // lblCustomerID
            // 
            this.lblCustomerID.AutoSize = true;
            this.lblCustomerID.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.lblCustomerID.Location = new System.Drawing.Point(810, 584);
            this.lblCustomerID.Name = "lblCustomerID";
            this.lblCustomerID.Size = new System.Drawing.Size(14, 14);
            this.lblCustomerID.TabIndex = 199;
            this.lblCustomerID.Text = "0";
            this.lblCustomerID.Visible = false;
            // 
            // dtpDate
            // 
            this.dtpDate.CalendarMonthBackground = System.Drawing.SystemColors.ControlLight;
            this.dtpDate.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F);
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(596, 48);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(102, 25);
            this.dtpDate.TabIndex = 6;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            this.dtpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpDate_KeyDown);
            // 
            // lblUnit1
            // 
            this.lblUnit1.AutoSize = true;
            this.lblUnit1.BackColor = System.Drawing.Color.Transparent;
            this.lblUnit1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnit1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.lblUnit1.Location = new System.Drawing.Point(685, 89);
            this.lblUnit1.Name = "lblUnit1";
            this.lblUnit1.Size = new System.Drawing.Size(29, 15);
            this.lblUnit1.TabIndex = 209;
            this.lblUnit1.Text = "unit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(164, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 14);
            this.label1.TabIndex = 208;
            this.label1.Text = "GST Amount";
            // 
            // txtgstAmount
            // 
            this.txtgstAmount.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtgstAmount.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgstAmount.HideSelection = false;
            this.txtgstAmount.Location = new System.Drawing.Point(117, 119);
            this.txtgstAmount.MaxLength = 50;
            this.txtgstAmount.Name = "txtgstAmount";
            this.txtgstAmount.ReadOnly = true;
            this.txtgstAmount.Size = new System.Drawing.Size(175, 25);
            this.txtgstAmount.TabIndex = 207;
            this.txtgstAmount.TabStop = false;
            this.txtgstAmount.Text = "0.00";
            this.txtgstAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblBrandName
            // 
            this.lblBrandName.AutoSize = true;
            this.lblBrandName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrandName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.lblBrandName.Location = new System.Drawing.Point(200, 52);
            this.lblBrandName.Name = "lblBrandName";
            this.lblBrandName.Size = new System.Drawing.Size(43, 14);
            this.lblBrandName.TabIndex = 206;
            this.lblBrandName.Text = "Brand";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(151, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 14);
            this.label5.TabIndex = 205;
            this.label5.Text = "Sale  Return Rate";
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.lblProductName.Location = new System.Drawing.Point(339, 52);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(90, 14);
            this.lblProductName.TabIndex = 204;
            this.lblProductName.Text = "Product Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(557, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 14);
            this.label3.TabIndex = 202;
            this.label3.Text = "Return Qty.";
            // 
            // txtSaleRate
            // 
            this.txtSaleRate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtSaleRate.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaleRate.HideSelection = false;
            this.txtSaleRate.Location = new System.Drawing.Point(117, 24);
            this.txtSaleRate.MaxLength = 50;
            this.txtSaleRate.Name = "txtSaleRate";
            this.txtSaleRate.ReadOnly = true;
            this.txtSaleRate.Size = new System.Drawing.Size(175, 25);
            this.txtSaleRate.TabIndex = 201;
            this.txtSaleRate.TabStop = false;
            this.txtSaleRate.Text = "0.00";
            this.txtSaleRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblQtyError
            // 
            this.lblQtyError.AutoSize = true;
            this.lblQtyError.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblQtyError.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtyError.ForeColor = System.Drawing.Color.DarkRed;
            this.lblQtyError.Location = new System.Drawing.Point(625, 90);
            this.lblQtyError.Name = "lblQtyError";
            this.lblQtyError.Size = new System.Drawing.Size(50, 13);
            this.lblQtyError.TabIndex = 203;
            this.lblQtyError.Text = "Required";
            this.lblQtyError.Visible = false;
            // 
            // txtReturnQty
            // 
            this.txtReturnQty.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtReturnQty.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnQty.HideSelection = false;
            this.txtReturnQty.Location = new System.Drawing.Point(507, 80);
            this.txtReturnQty.MaxLength = 50;
            this.txtReturnQty.Name = "txtReturnQty";
            this.txtReturnQty.Size = new System.Drawing.Size(175, 33);
            this.txtReturnQty.TabIndex = 1;
            this.txtReturnQty.Text = "0.00";
            this.txtReturnQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtReturnQty.Click += new System.EventHandler(this.txtReturnQty_Click);
            this.txtReturnQty.TextChanged += new System.EventHandler(this.txtReturnQty_TextChanged);
            this.txtReturnQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtReturnQty_KeyPress);
            this.txtReturnQty.Leave += new System.EventHandler(this.txtReturnQty_Leave);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblSaleQty);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.lblgst);
            this.panel1.Controls.Add(this.txtgstAmount);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblUnit1);
            this.panel1.Controls.Add(this.txtSaleRate);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtReturnQty);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblQtyError);
            this.panel1.Location = new System.Drawing.Point(31, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 193);
            this.panel1.TabIndex = 211;
            // 
            // lblSaleQty
            // 
            this.lblSaleQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(53)))), ((int)(((byte)(55)))));
            this.lblSaleQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblSaleQty.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaleQty.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblSaleQty.HideSelection = false;
            this.lblSaleQty.Location = new System.Drawing.Point(507, 59);
            this.lblSaleQty.MaxLength = 50;
            this.lblSaleQty.Name = "lblSaleQty";
            this.lblSaleQty.ReadOnly = true;
            this.lblSaleQty.Size = new System.Drawing.Size(175, 16);
            this.lblSaleQty.TabIndex = 172;
            this.lblSaleQty.TabStop = false;
            this.lblSaleQty.Text = "0.00";
            this.lblSaleQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label12.Location = new System.Drawing.Point(224, 101);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 14);
            this.label12.TabIndex = 164;
            this.label12.Text = "%";
            // 
            // lblgst
            // 
            this.lblgst.AutoSize = true;
            this.lblgst.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblgst.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblgst.Location = new System.Drawing.Point(184, 101);
            this.lblgst.Name = "lblgst";
            this.lblgst.Size = new System.Drawing.Size(32, 14);
            this.lblgst.TabIndex = 143;
            this.lblgst.Text = "0.00";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.AutoSize = true;
            this.txtCustomerName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.txtCustomerName.Location = new System.Drawing.Point(300, 20);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(101, 14);
            this.txtCustomerName.TabIndex = 226;
            this.txtCustomerName.Text = "Customer Name";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label11.Location = new System.Drawing.Point(151, 103);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 14);
            this.label11.TabIndex = 225;
            this.label11.Text = "Advance Amount";
            // 
            // txtAdvanceAmount
            // 
            this.txtAdvanceAmount.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtAdvanceAmount.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdvanceAmount.HideSelection = false;
            this.txtAdvanceAmount.Location = new System.Drawing.Point(117, 70);
            this.txtAdvanceAmount.MaxLength = 50;
            this.txtAdvanceAmount.Name = "txtAdvanceAmount";
            this.txtAdvanceAmount.ReadOnly = true;
            this.txtAdvanceAmount.Size = new System.Drawing.Size(175, 25);
            this.txtAdvanceAmount.TabIndex = 224;
            this.txtAdvanceAmount.TabStop = false;
            this.txtAdvanceAmount.Text = "0.00";
            this.txtAdvanceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Location = new System.Drawing.Point(153, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 14);
            this.label8.TabIndex = 222;
            this.label8.Text = "Balance Amount";
            // 
            // txtBalanceAmount
            // 
            this.txtBalanceAmount.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtBalanceAmount.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBalanceAmount.HideSelection = false;
            this.txtBalanceAmount.Location = new System.Drawing.Point(117, 171);
            this.txtBalanceAmount.MaxLength = 50;
            this.txtBalanceAmount.Name = "txtBalanceAmount";
            this.txtBalanceAmount.ReadOnly = true;
            this.txtBalanceAmount.Size = new System.Drawing.Size(175, 25);
            this.txtBalanceAmount.TabIndex = 221;
            this.txtBalanceAmount.TabStop = false;
            this.txtBalanceAmount.Text = "0.00";
            this.txtBalanceAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(554, 197);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 14);
            this.label7.TabIndex = 220;
            this.label7.Text = "Paid Amount";
            // 
            // txtPaidAmount
            // 
            this.txtPaidAmount.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtPaidAmount.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaidAmount.HideSelection = false;
            this.txtPaidAmount.Location = new System.Drawing.Point(507, 155);
            this.txtPaidAmount.MaxLength = 50;
            this.txtPaidAmount.Name = "txtPaidAmount";
            this.txtPaidAmount.Size = new System.Drawing.Size(175, 33);
            this.txtPaidAmount.TabIndex = 2;
            this.txtPaidAmount.Text = "0.00";
            this.txtPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPaidAmount.Click += new System.EventHandler(this.txtPaidAmount_Click);
            this.txtPaidAmount.TextChanged += new System.EventHandler(this.txtPaidAmount_TextChanged);
            this.txtPaidAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPaidAmount_KeyPress);
            this.txtPaidAmount.Leave += new System.EventHandler(this.txtPaidAmount_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(560, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 14);
            this.label4.TabIndex = 219;
            this.label4.Text = "Total Price";
            // 
            // txtTotalPrice
            // 
            this.txtTotalPrice.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtTotalPrice.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPrice.HideSelection = false;
            this.txtTotalPrice.Location = new System.Drawing.Point(507, 70);
            this.txtTotalPrice.MaxLength = 50;
            this.txtTotalPrice.Name = "txtTotalPrice";
            this.txtTotalPrice.ReadOnly = true;
            this.txtTotalPrice.Size = new System.Drawing.Size(175, 33);
            this.txtTotalPrice.TabIndex = 216;
            this.txtTotalPrice.TabStop = false;
            this.txtTotalPrice.Text = "0.00";
            this.txtTotalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(375, 568);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(106, 35);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlAlert
            // 
            this.pnlAlert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.pnlAlert.Controls.Add(this.lblAlert);
            this.pnlAlert.Location = new System.Drawing.Point(276, 0);
            this.pnlAlert.Name = "pnlAlert";
            this.pnlAlert.Size = new System.Drawing.Size(292, 36);
            this.pnlAlert.TabIndex = 218;
            this.pnlAlert.Visible = false;
            // 
            // lblAlert
            // 
            this.lblAlert.AutoSize = true;
            this.lblAlert.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlert.ForeColor = System.Drawing.Color.White;
            this.lblAlert.Location = new System.Drawing.Point(74, 10);
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(35, 14);
            this.lblAlert.TabIndex = 0;
            this.lblAlert.Text = "Alert";
            this.lblAlert.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(250, 568);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 35);
            this.btnSave.TabIndex = 3;
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
            this.btnExit.Location = new System.Drawing.Point(500, 568);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(106, 35);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.panel2.Controls.Add(this.pnlAlert);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 611);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(833, 36);
            this.panel2.TabIndex = 217;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.txtCustomerName);
            this.panel4.Controls.Add(this.lblPrevBalance);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.txtPaidAmount);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.lblPrevAdvance);
            this.panel4.Controls.Add(this.txtTotalPrice);
            this.panel4.Controls.Add(this.txtAdvanceAmount);
            this.panel4.Controls.Add(this.txtBalanceAmount);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Location = new System.Drawing.Point(31, 293);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(781, 254);
            this.panel4.TabIndex = 227;
            // 
            // lblPrevBalance
            // 
            this.lblPrevBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(53)))), ((int)(((byte)(55)))));
            this.lblPrevBalance.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblPrevBalance.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrevBalance.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblPrevBalance.HideSelection = false;
            this.lblPrevBalance.Location = new System.Drawing.Point(118, 149);
            this.lblPrevBalance.MaxLength = 50;
            this.lblPrevBalance.Name = "lblPrevBalance";
            this.lblPrevBalance.ReadOnly = true;
            this.lblPrevBalance.Size = new System.Drawing.Size(175, 16);
            this.lblPrevBalance.TabIndex = 170;
            this.lblPrevBalance.TabStop = false;
            this.lblPrevBalance.Text = "0.00";
            this.lblPrevBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPrevAdvance
            // 
            this.lblPrevAdvance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(53)))), ((int)(((byte)(55)))));
            this.lblPrevAdvance.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblPrevAdvance.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrevAdvance.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.lblPrevAdvance.HideSelection = false;
            this.lblPrevAdvance.Location = new System.Drawing.Point(118, 48);
            this.lblPrevAdvance.MaxLength = 50;
            this.lblPrevAdvance.Name = "lblPrevAdvance";
            this.lblPrevAdvance.ReadOnly = true;
            this.lblPrevAdvance.Size = new System.Drawing.Size(175, 16);
            this.lblPrevAdvance.TabIndex = 169;
            this.lblPrevAdvance.TabStop = false;
            this.lblPrevAdvance.Text = "0.00";
            this.lblPrevAdvance.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmSaleDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(53)))), ((int)(((byte)(55)))));
            this.ClientSize = new System.Drawing.Size(833, 647);
            this.ControlBox = false;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblBrandName);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblCustomerID);
            this.Controls.Add(this.txtSaleQty);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSaleDetails";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmSaleDetails_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlAlert.ResumeLayout(false);
            this.pnlAlert.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSaleQty;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblCustomerID;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblUnit1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtgstAmount;
        private System.Windows.Forms.Label lblBrandName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSaleRate;
        private System.Windows.Forms.Label lblQtyError;
        private System.Windows.Forms.TextBox txtReturnQty;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox lblSaleQty;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblgst;
        private System.Windows.Forms.Label txtCustomerName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAdvanceAmount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBalanceAmount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPaidAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotalPrice;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel pnlAlert;
        private System.Windows.Forms.Label lblAlert;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox lblPrevBalance;
        private System.Windows.Forms.TextBox lblPrevAdvance;
    }
}