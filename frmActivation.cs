using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PROMPT
{
    public partial class frmActivation : Form
    {
        Database database = new Database("PROMPT");
        public frmActivation()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            DbCommand dbcommand;
            dbcommand = database.GetSqlStringCommand("Select count(*) from tblSetup");
            int Rows=Convert.ToInt32(database.ExecuteScalar(dbcommand));

            if (txtKey.Text == "@sid89837873@")
            {
                if (Rows == 0)
                {
                    dbcommand = database.GetSqlStringCommand("insert into tblsetup values('A','N',getdate())");
                    database.ExecuteNonQuery(dbcommand);
                }
                else
                {
                    dbcommand = database.GetSqlStringCommand("Update tblsetup set Status='A',ActivationType='N',activationDate=getdate()");
                    database.ExecuteNonQuery(dbcommand);
                }
            }
            else if (txtKey.Text == "@sid88981798@")
            {
                if (Rows == 0)
                {
                    dbcommand = database.GetSqlStringCommand("insert into tblsetup values('A','Y',getdate())");
                    database.ExecuteNonQuery(dbcommand);
                }
                else
                {
                    dbcommand = database.GetSqlStringCommand("Update tblsetup set Status='A',ActivationType='Y',activationDate=getdate()");
                    database.ExecuteNonQuery(dbcommand);
                }
            }
            else if (txtKey.Text == "@sid997567@")
            {
                if (Rows == 0)
                {
                    dbcommand = database.GetSqlStringCommand("insert into tblsetup values('A','M',getdate())");
                    database.ExecuteNonQuery(dbcommand);
                }
                else
                {
                    dbcommand = database.GetSqlStringCommand("Update tblsetup set Status='A',ActivationType='M',activationDate=getdate()");
                    database.ExecuteNonQuery(dbcommand);
                }
            }
            this.Close();
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.frmMDI_Load(null, null);
        }
    }
}
