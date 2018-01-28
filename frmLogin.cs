using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PROMPT.Model;
using PROMPT.Controller;

namespace PROMPT
{
    public partial class frmLogin : Form
    {
        frmLoginController controller = new frmLoginController();
        frmLoginModel model = new frmLoginModel();
        public frmLogin()
        {
            InitializeComponent();
        }
        DataTable dtLoginDetails;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                model.Password = txtPassword.Text.Trim();
                dtLoginDetails=controller.GetLogin(model);
                if (dtLoginDetails != null)
                {
                    if (dtLoginDetails.Rows.Count > 0)
                    {
                        MessageBox.Show("Login Sucessfully!");
                        Program.Session = true;
                        var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
                        principalForm.frmMDI_Load(null, null);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed! Wrong User Password");
                        txtPassword.Focus();
                    }
                }
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //SendKeys.Send("{TAB}");
                btnLogin_Click(null, null);
            }
        }
    }
}
