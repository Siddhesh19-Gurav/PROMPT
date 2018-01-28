namespace PROMPT
{
    partial class frmTaxDetails
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVatt = new System.Windows.Forms.TextBox();
            this.txtTax = new System.Windows.Forms.TextBox();
            this.btvSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvTaxDetails = new System.Windows.Forms.DataGridView();
            this.txtTaxDetailsId = new System.Windows.Forms.TextBox();
            this.cmsRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxDetails)).BeginInit();
            this.cmsRightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vatt :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tax :";
            // 
            // txtVatt
            // 
            this.txtVatt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVatt.Location = new System.Drawing.Point(76, 10);
            this.txtVatt.Name = "txtVatt";
            this.txtVatt.Size = new System.Drawing.Size(100, 22);
            this.txtVatt.TabIndex = 0;
            this.txtVatt.TextChanged += new System.EventHandler(this.txtVatt_TextChanged);
            // 
            // txtTax
            // 
            this.txtTax.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTax.Location = new System.Drawing.Point(76, 42);
            this.txtTax.Name = "txtTax";
            this.txtTax.Size = new System.Drawing.Size(100, 22);
            this.txtTax.TabIndex = 1;
            this.txtTax.TextChanged += new System.EventHandler(this.txtTax_TextChanged);
            // 
            // btvSave
            // 
            this.btvSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btvSave.Location = new System.Drawing.Point(124, 85);
            this.btvSave.Name = "btvSave";
            this.btvSave.Size = new System.Drawing.Size(75, 23);
            this.btvSave.TabIndex = 2;
            this.btvSave.Text = "Save";
            this.btvSave.UseVisualStyleBackColor = true;
            this.btvSave.Click += new System.EventHandler(this.btvSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(218, 85);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dgvTaxDetails
            // 
            this.dgvTaxDetails.AllowUserToAddRows = false;
            this.dgvTaxDetails.AllowUserToDeleteRows = false;
            this.dgvTaxDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaxDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTaxDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTaxDetails.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTaxDetails.Location = new System.Drawing.Point(12, 114);
            this.dgvTaxDetails.MultiSelect = false;
            this.dgvTaxDetails.Name = "dgvTaxDetails";
            this.dgvTaxDetails.ReadOnly = true;
            this.dgvTaxDetails.Size = new System.Drawing.Size(291, 135);
            this.dgvTaxDetails.TabIndex = 6;
            this.dgvTaxDetails.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvTaxDetails_MouseClick);
            // 
            // txtTaxDetailsId
            // 
            this.txtTaxDetailsId.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTaxDetailsId.Location = new System.Drawing.Point(182, 10);
            this.txtTaxDetailsId.Name = "txtTaxDetailsId";
            this.txtTaxDetailsId.Size = new System.Drawing.Size(100, 22);
            this.txtTaxDetailsId.TabIndex = 7;
            this.txtTaxDetailsId.Visible = false;
            // 
            // cmsRightClickMenu
            // 
            this.cmsRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.cmsRightClickMenu.Name = "cmsRightClickMenu";
            this.cmsRightClickMenu.Size = new System.Drawing.Size(108, 48);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // frmTaxDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 261);
            this.Controls.Add(this.txtTaxDetailsId);
            this.Controls.Add(this.dgvTaxDetails);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btvSave);
            this.Controls.Add(this.txtTax);
            this.Controls.Add(this.txtVatt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmTaxDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tax Details";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTaxDetails_FormClosed);
            this.Load += new System.EventHandler(this.frmTaxDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxDetails)).EndInit();
            this.cmsRightClickMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVatt;
        private System.Windows.Forms.TextBox txtTax;
        private System.Windows.Forms.Button btvSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvTaxDetails;
        private System.Windows.Forms.TextBox txtTaxDetailsId;
        private System.Windows.Forms.ContextMenuStrip cmsRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}