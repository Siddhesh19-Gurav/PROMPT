namespace PROMPT
{
    partial class frmMDI
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMDI));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.registrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCustomerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listOfCustomerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saleProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.policyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listOfCallsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.masterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locationMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customerTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brandMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capacityMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.engineerMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thirdPartyMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taxDetailsMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reasonMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetPassowrdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvExpiredDetails = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvCallDetails = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvPendingBillDetails = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.updateCallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dashboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpiredDetails)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCallDetails)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingBillDetails)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dashboardToolStripMenuItem,
            this.registrationToolStripMenuItem,
            this.productToolStripMenuItem,
            this.policyToolStripMenuItem,
            this.masterToolStripMenuItem,
            this.resetPassowrdToolStripMenuItem,
            this.logInToolStripMenuItem,
            this.logOutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1186, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // registrationToolStripMenuItem
            // 
            this.registrationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCustomerToolStripMenuItem,
            this.listOfCustomerToolStripMenuItem});
            this.registrationToolStripMenuItem.Name = "registrationToolStripMenuItem";
            this.registrationToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.registrationToolStripMenuItem.Text = "&Registration";
            // 
            // addCustomerToolStripMenuItem
            // 
            this.addCustomerToolStripMenuItem.Name = "addCustomerToolStripMenuItem";
            this.addCustomerToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.addCustomerToolStripMenuItem.Text = "Add &Customer";
            this.addCustomerToolStripMenuItem.Click += new System.EventHandler(this.addCustomerToolStripMenuItem_Click);
            // 
            // listOfCustomerToolStripMenuItem
            // 
            this.listOfCustomerToolStripMenuItem.Name = "listOfCustomerToolStripMenuItem";
            this.listOfCustomerToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.listOfCustomerToolStripMenuItem.Text = "&List of Customer";
            this.listOfCustomerToolStripMenuItem.Click += new System.EventHandler(this.listOfCustomerToolStripMenuItem_Click);
            // 
            // productToolStripMenuItem
            // 
            this.productToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saleProductToolStripMenuItem});
            this.productToolStripMenuItem.Name = "productToolStripMenuItem";
            this.productToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.productToolStripMenuItem.Text = "&Product";
            // 
            // saleProductToolStripMenuItem
            // 
            this.saleProductToolStripMenuItem.Name = "saleProductToolStripMenuItem";
            this.saleProductToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.saleProductToolStripMenuItem.Text = "Add &Product";
            this.saleProductToolStripMenuItem.Click += new System.EventHandler(this.saleProductToolStripMenuItem_Click);
            // 
            // policyToolStripMenuItem
            // 
            this.policyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCallToolStripMenuItem,
            this.listOfCallsToolStripMenuItem});
            this.policyToolStripMenuItem.Name = "policyToolStripMenuItem";
            this.policyToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.policyToolStripMenuItem.Text = "&Call Log";
            // 
            // addCallToolStripMenuItem
            // 
            this.addCallToolStripMenuItem.Name = "addCallToolStripMenuItem";
            this.addCallToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addCallToolStripMenuItem.Text = "Add &Call";
            this.addCallToolStripMenuItem.Visible = false;
            this.addCallToolStripMenuItem.Click += new System.EventHandler(this.addCallToolStripMenuItem_Click);
            // 
            // listOfCallsToolStripMenuItem
            // 
            this.listOfCallsToolStripMenuItem.Name = "listOfCallsToolStripMenuItem";
            this.listOfCallsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.listOfCallsToolStripMenuItem.Text = "&List of Calls";
            this.listOfCallsToolStripMenuItem.Click += new System.EventHandler(this.listOfCallsToolStripMenuItem_Click);
            // 
            // masterToolStripMenuItem
            // 
            this.masterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locationMasterToolStripMenuItem,
            this.customerTypeToolStripMenuItem,
            this.productMasterToolStripMenuItem,
            this.brandMasterToolStripMenuItem,
            this.capacityMasterToolStripMenuItem,
            this.engineerMasterToolStripMenuItem,
            this.thirdPartyMasterToolStripMenuItem,
            this.taxDetailsMasterToolStripMenuItem,
            this.reasonMasterToolStripMenuItem});
            this.masterToolStripMenuItem.Name = "masterToolStripMenuItem";
            this.masterToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.masterToolStripMenuItem.Text = "&Master";
            // 
            // locationMasterToolStripMenuItem
            // 
            this.locationMasterToolStripMenuItem.Name = "locationMasterToolStripMenuItem";
            this.locationMasterToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.locationMasterToolStripMenuItem.Text = "&Location Master";
            this.locationMasterToolStripMenuItem.Click += new System.EventHandler(this.locationMasterToolStripMenuItem_Click);
            // 
            // customerTypeToolStripMenuItem
            // 
            this.customerTypeToolStripMenuItem.Name = "customerTypeToolStripMenuItem";
            this.customerTypeToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.customerTypeToolStripMenuItem.Text = "&Customer Type  Master";
            this.customerTypeToolStripMenuItem.Click += new System.EventHandler(this.customerTypeToolStripMenuItem_Click);
            // 
            // productMasterToolStripMenuItem
            // 
            this.productMasterToolStripMenuItem.Name = "productMasterToolStripMenuItem";
            this.productMasterToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.productMasterToolStripMenuItem.Text = "&Product Master";
            this.productMasterToolStripMenuItem.Click += new System.EventHandler(this.productMasterToolStripMenuItem_Click);
            // 
            // brandMasterToolStripMenuItem
            // 
            this.brandMasterToolStripMenuItem.Name = "brandMasterToolStripMenuItem";
            this.brandMasterToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.brandMasterToolStripMenuItem.Text = "&Brand Master";
            this.brandMasterToolStripMenuItem.Click += new System.EventHandler(this.brandMasterToolStripMenuItem_Click);
            // 
            // capacityMasterToolStripMenuItem
            // 
            this.capacityMasterToolStripMenuItem.Name = "capacityMasterToolStripMenuItem";
            this.capacityMasterToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.capacityMasterToolStripMenuItem.Text = "Ca&pacity Master";
            this.capacityMasterToolStripMenuItem.Click += new System.EventHandler(this.capacityMasterToolStripMenuItem_Click);
            // 
            // engineerMasterToolStripMenuItem
            // 
            this.engineerMasterToolStripMenuItem.Name = "engineerMasterToolStripMenuItem";
            this.engineerMasterToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.engineerMasterToolStripMenuItem.Text = "&Engineer Master";
            this.engineerMasterToolStripMenuItem.Click += new System.EventHandler(this.engineerMasterToolStripMenuItem_Click);
            // 
            // thirdPartyMasterToolStripMenuItem
            // 
            this.thirdPartyMasterToolStripMenuItem.Name = "thirdPartyMasterToolStripMenuItem";
            this.thirdPartyMasterToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.thirdPartyMasterToolStripMenuItem.Text = "&Dealer Master";
            this.thirdPartyMasterToolStripMenuItem.Click += new System.EventHandler(this.thirdPartyMasterToolStripMenuItem_Click);
            // 
            // taxDetailsMasterToolStripMenuItem
            // 
            this.taxDetailsMasterToolStripMenuItem.Name = "taxDetailsMasterToolStripMenuItem";
            this.taxDetailsMasterToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.taxDetailsMasterToolStripMenuItem.Text = "&Tax Details Master";
            this.taxDetailsMasterToolStripMenuItem.Click += new System.EventHandler(this.taxDetailsMasterToolStripMenuItem_Click);
            // 
            // reasonMasterToolStripMenuItem
            // 
            this.reasonMasterToolStripMenuItem.Name = "reasonMasterToolStripMenuItem";
            this.reasonMasterToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.reasonMasterToolStripMenuItem.Text = "&Reason Master";
            this.reasonMasterToolStripMenuItem.Click += new System.EventHandler(this.reasonMasterToolStripMenuItem_Click);
            // 
            // resetPassowrdToolStripMenuItem
            // 
            this.resetPassowrdToolStripMenuItem.Name = "resetPassowrdToolStripMenuItem";
            this.resetPassowrdToolStripMenuItem.Size = new System.Drawing.Size(124, 20);
            this.resetPassowrdToolStripMenuItem.Text = "Reset Password";
            this.resetPassowrdToolStripMenuItem.Click += new System.EventHandler(this.resetPassowrdToolStripMenuItem_Click);
            // 
            // logInToolStripMenuItem
            // 
            this.logInToolStripMenuItem.Name = "logInToolStripMenuItem";
            this.logInToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.logInToolStripMenuItem.Text = "Log &In";
            this.logInToolStripMenuItem.Click += new System.EventHandler(this.logInToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.logOutToolStripMenuItem.Text = "Log &out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(602, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(576, 664);
            this.panel1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(569, 649);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvExpiredDetails);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(561, 618);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Expired Details";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvExpiredDetails
            // 
            this.dgvExpiredDetails.AllowUserToAddRows = false;
            this.dgvExpiredDetails.AllowUserToDeleteRows = false;
            this.dgvExpiredDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExpiredDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExpiredDetails.Location = new System.Drawing.Point(7, 6);
            this.dgvExpiredDetails.MultiSelect = false;
            this.dgvExpiredDetails.Name = "dgvExpiredDetails";
            this.dgvExpiredDetails.ReadOnly = true;
            this.dgvExpiredDetails.RowHeadersVisible = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvExpiredDetails.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvExpiredDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExpiredDetails.Size = new System.Drawing.Size(548, 611);
            this.dgvExpiredDetails.TabIndex = 6;
            this.dgvExpiredDetails.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExpiredDetails_CellContentDoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvCallDetails);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(561, 618);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Pending Call Details";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvCallDetails
            // 
            this.dgvCallDetails.AllowUserToAddRows = false;
            this.dgvCallDetails.AllowUserToDeleteRows = false;
            this.dgvCallDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCallDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCallDetails.Location = new System.Drawing.Point(6, 6);
            this.dgvCallDetails.MultiSelect = false;
            this.dgvCallDetails.Name = "dgvCallDetails";
            this.dgvCallDetails.ReadOnly = true;
            this.dgvCallDetails.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvCallDetails.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCallDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCallDetails.Size = new System.Drawing.Size(547, 611);
            this.dgvCallDetails.TabIndex = 7;
            this.dgvCallDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCallDetails_CellContentDoubleClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvPendingBillDetails);
            this.tabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(561, 618);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Pending Bill Details";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvPendingBillDetails
            // 
            this.dgvPendingBillDetails.AllowUserToAddRows = false;
            this.dgvPendingBillDetails.AllowUserToDeleteRows = false;
            this.dgvPendingBillDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPendingBillDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPendingBillDetails.Location = new System.Drawing.Point(6, 6);
            this.dgvPendingBillDetails.MultiSelect = false;
            this.dgvPendingBillDetails.Name = "dgvPendingBillDetails";
            this.dgvPendingBillDetails.ReadOnly = true;
            this.dgvPendingBillDetails.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPendingBillDetails.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPendingBillDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPendingBillDetails.Size = new System.Drawing.Size(548, 611);
            this.dgvPendingBillDetails.TabIndex = 11;
            this.dgvPendingBillDetails.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPendingBillDetails_CellMouseDoubleClick);
            this.dgvPendingBillDetails.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvPendingBillDetails_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(260, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 14);
            this.label3.TabIndex = 10;
            this.label3.Text = "Pending Bill Details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(122, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 14);
            this.label2.TabIndex = 9;
            this.label2.Text = "Pending Call Details";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 14);
            this.label1.TabIndex = 8;
            this.label1.Text = "Expired Details";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateCallToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 26);
            // 
            // updateCallToolStripMenuItem
            // 
            this.updateCallToolStripMenuItem.Name = "updateCallToolStripMenuItem";
            this.updateCallToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.updateCallToolStripMenuItem.Text = "Update AMC";
            this.updateCallToolStripMenuItem.Click += new System.EventHandler(this.updateCallToolStripMenuItem_Click);
            // 
            // dashboardToolStripMenuItem
            // 
            this.dashboardToolStripMenuItem.Name = "dashboardToolStripMenuItem";
            this.dashboardToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.dashboardToolStripMenuItem.Text = "Dashboard";
            this.dashboardToolStripMenuItem.Click += new System.EventHandler(this.dashboardToolStripMenuItem_Click);
            // 
            // frmMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1186, 691);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMDI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PROMPT MANAGEMENT SYSTEM";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMDI_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExpiredDetails)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCallDetails)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingBillDetails)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem logInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem policyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem masterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listOfCustomerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saleProductToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locationMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customerTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brandMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem capacityMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCustomerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem engineerMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listOfCallsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thirdPartyMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taxDetailsMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reasonMasterToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvPendingBillDetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCallDetails;
        private System.Windows.Forms.DataGridView dgvExpiredDetails;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem updateCallToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStripMenuItem resetPassowrdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dashboardToolStripMenuItem;

    }
}