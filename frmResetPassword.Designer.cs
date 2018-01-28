namespace PROMPT
{
    partial class frmResetPassword
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChange = new System.Windows.Forms.Button();
            this.txtConformPassword = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Old Password :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 72);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "New Password :";
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(187, 162);
            this.btnChange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(100, 25);
            this.btnChange.TabIndex = 3;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // txtConformPassword
            // 
            this.txtConformPassword.Location = new System.Drawing.Point(167, 118);
            this.txtConformPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtConformPassword.MaxLength = 8;
            this.txtConformPassword.Name = "txtConformPassword";
            this.txtConformPassword.PasswordChar = '*';
            this.txtConformPassword.Size = new System.Drawing.Size(206, 22);
            this.txtConformPassword.TabIndex = 2;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(167, 69);
            this.txtNewPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtNewPassword.MaxLength = 8;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(206, 22);
            this.txtNewPassword.TabIndex = 1;
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Location = new System.Drawing.Point(167, 26);
            this.txtOldPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtOldPassword.MaxLength = 8;
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '*';
            this.txtOldPassword.Size = new System.Drawing.Size(206, 22);
            this.txtOldPassword.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 121);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 14);
            this.label3.TabIndex = 6;
            this.label3.Text = "Conform Password :";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(306, 162);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 25);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // frmResetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 214);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOldPassword);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.txtConformPassword);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmResetPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reset Password";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmResetPassword_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.TextBox txtConformPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
    }
}