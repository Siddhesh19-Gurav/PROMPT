using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;

namespace PROMPT
{
    public partial class frmResetPassword : Form
    {
        Database database = new Database("PROMPT");
        public frmResetPassword()
        {
            InitializeComponent();
        }

        private void frmResetPassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {

            if(txtOldPassword.Text.Trim() == "" || txtNewPassword.Text.Trim() == "" || txtConformPassword.Text.Trim()=="")
            {
                MessageBox.Show("Please enter all details first.");
                return;
            }
            if (txtNewPassword.Text.Trim() != txtConformPassword.Text.Trim())
            {
                MessageBox.Show("New password and conform password not match.");
                return;
            }
            DbCommand dbcommand= database.GetSqlStringCommand("select * from tblUserDetails where UserName='PROMPT'");
            DataTable dt = database.ExecuteDataTable(dbcommand);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Passowrd"].ToString().Trim() != txtOldPassword.Text.Trim())
                {
                    MessageBox.Show("Old Password not match.");
                    return;
                }

                dbcommand = database.GetSqlStringCommand("update tblUserDetails set Passowrd='" + txtConformPassword.Text + "' where UserName='PROMPT'");
                database.ExecuteNonQuery(dbcommand);
                MessageBox.Show("Password Change Sucessfully.");
                var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
                principalForm.logOutToolStripMenuItem_Click(null, null);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
