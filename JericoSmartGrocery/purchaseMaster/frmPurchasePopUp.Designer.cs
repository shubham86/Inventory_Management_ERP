namespace JericoSmartGrocery.purchaseMaster
{
    partial class frmPurchasePopUp
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
            this.txtQty = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblAlert = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            this.pnlAlert = new System.Windows.Forms.Panel();
            this.lblQtyError = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.txtPurchaseRate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPRateError = new System.Windows.Forms.Label();
            this.txtSaleRate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalPrice = new System.Windows.Forms.TextBox();
            this.lblPrevPurchaseRate = new System.Windows.Forms.Label();
            this.lblPrevSaleRate = new System.Windows.Forms.Label();
            this.lblBrandName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtgstAmount = new System.Windows.Forms.TextBox();
            this.lblgst = new System.Windows.Forms.Label();
            this.lblUnit1 = new System.Windows.Forms.Label();
            this.lblUnit2 = new System.Windows.Forms.Label();
            this.lblUnit3 = new System.Windows.Forms.Label();
            this.lblSaleRateError = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.pnlAlert.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtQty.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.HideSelection = false;
            this.txtQty.Location = new System.Drawing.Point(208, 99);
            this.txtQty.MaxLength = 50;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(241, 25);
            this.txtQty.TabIndex = 1;
            this.txtQty.Text = "0.00";
            this.txtQty.Click += new System.EventHandler(this.txtQty_Click);
            this.txtQty.TextChanged += new System.EventHandler(this.txtQty_TextChanged);
            this.txtQty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQty_KeyDown);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            this.txtQty.Leave += new System.EventHandler(this.txtQty_Leave);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 438);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(592, 36);
            this.panel2.TabIndex = 88;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(91, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 14);
            this.label3.TabIndex = 87;
            this.label3.Text = "Purchase Qty.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(242, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 19);
            this.label6.TabIndex = 1;
            this.label6.Text = "Product Details";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Location = new System.Drawing.Point(128, 347);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 35);
            this.btnSave.TabIndex = 4;
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
            this.btnExit.Location = new System.Drawing.Point(379, 347);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(106, 35);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(19)))));
            this.panel3.Controls.Add(this.label6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(592, 36);
            this.panel3.TabIndex = 89;
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
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(254, 347);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(106, 35);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblID.Location = new System.Drawing.Point(541, 347);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(18, 14);
            this.lblID.TabIndex = 92;
            this.lblID.Text = "id";
            this.lblID.Visible = false;
            // 
            // pnlAlert
            // 
            this.pnlAlert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(53)))), ((int)(((byte)(55)))));
            this.pnlAlert.Controls.Add(this.lblAlert);
            this.pnlAlert.Location = new System.Drawing.Point(160, 395);
            this.pnlAlert.Name = "pnlAlert";
            this.pnlAlert.Size = new System.Drawing.Size(292, 36);
            this.pnlAlert.TabIndex = 91;
            this.pnlAlert.Visible = false;
            // 
            // lblQtyError
            // 
            this.lblQtyError.AutoSize = true;
            this.lblQtyError.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblQtyError.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtyError.ForeColor = System.Drawing.Color.DarkRed;
            this.lblQtyError.Location = new System.Drawing.Point(395, 105);
            this.lblQtyError.Name = "lblQtyError";
            this.lblQtyError.Size = new System.Drawing.Size(50, 13);
            this.lblQtyError.TabIndex = 93;
            this.lblQtyError.Text = "Required";
            this.lblQtyError.Visible = false;
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.lblProductName.Location = new System.Drawing.Point(208, 59);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(90, 14);
            this.lblProductName.TabIndex = 94;
            this.lblProductName.Text = "Product Name";
            // 
            // txtPurchaseRate
            // 
            this.txtPurchaseRate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtPurchaseRate.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPurchaseRate.HideSelection = false;
            this.txtPurchaseRate.Location = new System.Drawing.Point(208, 140);
            this.txtPurchaseRate.MaxLength = 50;
            this.txtPurchaseRate.Name = "txtPurchaseRate";
            this.txtPurchaseRate.Size = new System.Drawing.Size(241, 25);
            this.txtPurchaseRate.TabIndex = 2;
            this.txtPurchaseRate.Text = "0.00";
            this.txtPurchaseRate.Click += new System.EventHandler(this.txtPurchaseRate_Click);
            this.txtPurchaseRate.TextChanged += new System.EventHandler(this.txtPurchaseRate_TextChanged);
            this.txtPurchaseRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPurchaseRate_KeyPress);
            this.txtPurchaseRate.Leave += new System.EventHandler(this.txtPurchaseRate_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(91, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 14);
            this.label5.TabIndex = 99;
            this.label5.Text = "Purchase Rate";
            // 
            // lblPRateError
            // 
            this.lblPRateError.AutoSize = true;
            this.lblPRateError.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblPRateError.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPRateError.ForeColor = System.Drawing.Color.DarkRed;
            this.lblPRateError.Location = new System.Drawing.Point(395, 146);
            this.lblPRateError.Name = "lblPRateError";
            this.lblPRateError.Size = new System.Drawing.Size(50, 13);
            this.lblPRateError.TabIndex = 100;
            this.lblPRateError.Text = "Required";
            this.lblPRateError.Visible = false;
            // 
            // txtSaleRate
            // 
            this.txtSaleRate.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtSaleRate.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaleRate.HideSelection = false;
            this.txtSaleRate.Location = new System.Drawing.Point(208, 181);
            this.txtSaleRate.MaxLength = 50;
            this.txtSaleRate.Name = "txtSaleRate";
            this.txtSaleRate.ReadOnly = true;
            this.txtSaleRate.Size = new System.Drawing.Size(241, 25);
            this.txtSaleRate.TabIndex = 3;
            this.txtSaleRate.Text = "0.00";
            this.txtSaleRate.Click += new System.EventHandler(this.txtSaleRate_Click);
            this.txtSaleRate.TextChanged += new System.EventHandler(this.txtSaleRate_TextChanged);
            this.txtSaleRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSaleRate_KeyPress);
            this.txtSaleRate.Leave += new System.EventHandler(this.txtSaleRate_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(91, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 14);
            this.label2.TabIndex = 102;
            this.label2.Text = "Sale Rate";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(91, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 14);
            this.label4.TabIndex = 104;
            this.label4.Text = "Total Price";
            // 
            // txtTotalPrice
            // 
            this.txtTotalPrice.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtTotalPrice.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalPrice.HideSelection = false;
            this.txtTotalPrice.Location = new System.Drawing.Point(208, 281);
            this.txtTotalPrice.MaxLength = 50;
            this.txtTotalPrice.Name = "txtTotalPrice";
            this.txtTotalPrice.ReadOnly = true;
            this.txtTotalPrice.Size = new System.Drawing.Size(241, 25);
            this.txtTotalPrice.TabIndex = 10;
            this.txtTotalPrice.TabStop = false;
            this.txtTotalPrice.Text = "0.00";
            this.txtTotalPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotalPrice_KeyPress);
            this.txtTotalPrice.Leave += new System.EventHandler(this.txtTotalPrice_Leave);
            // 
            // lblPrevPurchaseRate
            // 
            this.lblPrevPurchaseRate.AutoSize = true;
            this.lblPrevPurchaseRate.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrevPurchaseRate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblPrevPurchaseRate.Location = new System.Drawing.Point(455, 147);
            this.lblPrevPurchaseRate.Name = "lblPrevPurchaseRate";
            this.lblPrevPurchaseRate.Size = new System.Drawing.Size(32, 14);
            this.lblPrevPurchaseRate.TabIndex = 109;
            this.lblPrevPurchaseRate.Text = "0.00";
            // 
            // lblPrevSaleRate
            // 
            this.lblPrevSaleRate.AutoSize = true;
            this.lblPrevSaleRate.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrevSaleRate.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblPrevSaleRate.Location = new System.Drawing.Point(455, 189);
            this.lblPrevSaleRate.Name = "lblPrevSaleRate";
            this.lblPrevSaleRate.Size = new System.Drawing.Size(29, 12);
            this.lblPrevSaleRate.TabIndex = 110;
            this.lblPrevSaleRate.Text = "0.00";
            // 
            // lblBrandName
            // 
            this.lblBrandName.AutoSize = true;
            this.lblBrandName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrandName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(168)))), ((int)(((byte)(128)))));
            this.lblBrandName.Location = new System.Drawing.Point(91, 59);
            this.lblBrandName.Name = "lblBrandName";
            this.lblBrandName.Size = new System.Drawing.Size(43, 14);
            this.lblBrandName.TabIndex = 111;
            this.lblBrandName.Text = "Brand";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(91, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 14);
            this.label1.TabIndex = 113;
            this.label1.Text = "GST";
            // 
            // txtgstAmount
            // 
            this.txtgstAmount.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtgstAmount.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgstAmount.HideSelection = false;
            this.txtgstAmount.Location = new System.Drawing.Point(208, 240);
            this.txtgstAmount.MaxLength = 50;
            this.txtgstAmount.Name = "txtgstAmount";
            this.txtgstAmount.ReadOnly = true;
            this.txtgstAmount.Size = new System.Drawing.Size(241, 25);
            this.txtgstAmount.TabIndex = 112;
            this.txtgstAmount.TabStop = false;
            this.txtgstAmount.Text = "0.00";
            // 
            // lblgst
            // 
            this.lblgst.AutoSize = true;
            this.lblgst.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblgst.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblgst.Location = new System.Drawing.Point(455, 248);
            this.lblgst.Name = "lblgst";
            this.lblgst.Size = new System.Drawing.Size(32, 14);
            this.lblgst.TabIndex = 114;
            this.lblgst.Text = "0.00";
            // 
            // lblUnit1
            // 
            this.lblUnit1.AutoSize = true;
            this.lblUnit1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblUnit1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnit1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblUnit1.Location = new System.Drawing.Point(395, 104);
            this.lblUnit1.Name = "lblUnit1";
            this.lblUnit1.Size = new System.Drawing.Size(29, 15);
            this.lblUnit1.TabIndex = 115;
            this.lblUnit1.Text = "unit";
            // 
            // lblUnit2
            // 
            this.lblUnit2.AutoSize = true;
            this.lblUnit2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblUnit2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnit2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblUnit2.Location = new System.Drawing.Point(395, 146);
            this.lblUnit2.Name = "lblUnit2";
            this.lblUnit2.Size = new System.Drawing.Size(29, 15);
            this.lblUnit2.TabIndex = 116;
            this.lblUnit2.Text = "unit";
            // 
            // lblUnit3
            // 
            this.lblUnit3.AutoSize = true;
            this.lblUnit3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblUnit3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnit3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblUnit3.Location = new System.Drawing.Point(395, 186);
            this.lblUnit3.Name = "lblUnit3";
            this.lblUnit3.Size = new System.Drawing.Size(29, 15);
            this.lblUnit3.TabIndex = 117;
            this.lblUnit3.Text = "unit";
            // 
            // lblSaleRateError
            // 
            this.lblSaleRateError.AutoSize = true;
            this.lblSaleRateError.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblSaleRateError.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaleRateError.ForeColor = System.Drawing.Color.DarkRed;
            this.lblSaleRateError.Location = new System.Drawing.Point(252, 187);
            this.lblSaleRateError.Name = "lblSaleRateError";
            this.lblSaleRateError.Size = new System.Drawing.Size(0, 13);
            this.lblSaleRateError.TabIndex = 118;
            this.lblSaleRateError.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label7.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label7.Location = new System.Drawing.Point(410, 245);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 15);
            this.label7.TabIndex = 150;
            this.label7.Text = "₹";
            // 
            // frmPurchasePopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(53)))), ((int)(((byte)(55)))));
            this.ClientSize = new System.Drawing.Size(592, 474);
            this.ControlBox = false;
            this.Controls.Add(this.lblPRateError);
            this.Controls.Add(this.lblQtyError);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblSaleRateError);
            this.Controls.Add(this.lblUnit3);
            this.Controls.Add(this.lblUnit2);
            this.Controls.Add(this.lblUnit1);
            this.Controls.Add(this.lblgst);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtgstAmount);
            this.Controls.Add(this.lblBrandName);
            this.Controls.Add(this.lblPrevSaleRate);
            this.Controls.Add(this.lblPrevPurchaseRate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.pnlAlert);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtPurchaseRate);
            this.Controls.Add(this.txtSaleRate);
            this.Controls.Add(this.txtTotalPrice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPurchasePopUp";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmPurchasePopUp_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlAlert.ResumeLayout(false);
            this.pnlAlert.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblAlert;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Panel pnlAlert;
        private System.Windows.Forms.Label lblQtyError;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.TextBox txtPurchaseRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPRateError;
        private System.Windows.Forms.TextBox txtSaleRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotalPrice;
        private System.Windows.Forms.Label lblPrevPurchaseRate;
        private System.Windows.Forms.Label lblPrevSaleRate;
        private System.Windows.Forms.Label lblBrandName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtgstAmount;
        private System.Windows.Forms.Label lblgst;
        private System.Windows.Forms.Label lblUnit1;
        private System.Windows.Forms.Label lblUnit2;
        private System.Windows.Forms.Label lblUnit3;
        private System.Windows.Forms.Label lblSaleRateError;
        private System.Windows.Forms.Label label7;
    }
}