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
    public partial class frmThirdPartyMaster : Form
    {
        Database db = new Database("PROMPT");
        DbCommand dbcommand;
        DataTable dt = new DataTable();
        public frmThirdPartyMaster()
        {
            InitializeComponent();
        }
        int result = 0;

        public void BindGridDetails()
        {
            dbcommand = db.GetSqlStringCommand("Select ThirdPartyID ,ThirdPartyName as [Dealer Name] from tblThirdParty");
            dt = db.ExecuteDataTable(dbcommand);
            dgvThirdPartyData.DataSource = dt;
            dgvThirdPartyData.Columns[0].Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter Name.");
                    return;
                }
                else
                {
                    if (txtThirdPartyId.Text == "")
                    {
                        dbcommand = db.GetSqlStringCommand("SELECT ThirdPartyName as [Dealer Name] FROM tblThirdParty WHERE ThirdPartyName='" + txtName.Text.ToUpper() + "'");
                        dt = db.ExecuteDataTable(dbcommand);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Record already Exits");
                            return;
                        }

                        dbcommand = db.GetSqlStringCommand("INSERT INTO tblThirdParty (ThirdPartyName) values ('" + txtName.Text.ToUpper() + "')");
                        result = db.ExecuteNonQuery(dbcommand);
                        if (result > 0)
                        {
                            MessageBox.Show("Inserted Sucessfully.");
                            BindGridDetails();
                            txtThirdPartyId.Text = "";
                            txtName.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Record not Inserted.");
                            txtThirdPartyId.Text = "";
                            txtName.Text = "";
                        }
                    }
                    else
                    {
                        dbcommand = db.GetSqlStringCommand("Update tblThirdParty set ThirdPartyName='" + txtName.Text.ToUpper() + "' where ThirdPartyID='"+txtThirdPartyId.Text.Trim()+"'");
                        result = db.ExecuteNonQuery(dbcommand);
                        if (result > 0)
                        {
                            MessageBox.Show("Updated Sucessfully.");
                            BindGridDetails();
                            txtThirdPartyId.Text = "";
                            txtName.Text = ""; ;
                        }
                        else
                        {
                            MessageBox.Show("Record not Updated.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmThirdPartyMaster_Load(object sender, EventArgs e)
        {
            try
            {
                txtThirdPartyId.Text = "";
                BindGridDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvThirdPartyData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgvThirdPartyData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvThirdPartyData_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvThirdPartyData_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dgvThirdPartyData.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    dgvThirdPartyData.Rows[currentMouseOverRow].Cells[1].Selected = true;
                    dgvThirdPartyData.Refresh();
                    cmsRightClickMenu.Show(dgvThirdPartyData, e.X, e.Y);
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvThirdPartyData.CurrentRow.Index > -1)
                {
                    DataGridViewRow row = dgvThirdPartyData.Rows[dgvThirdPartyData.CurrentCell.RowIndex];
                    txtThirdPartyId.Text = dgvThirdPartyData.Rows[dgvThirdPartyData.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    txtName.Text = dgvThirdPartyData.Rows[dgvThirdPartyData.CurrentCell.RowIndex].Cells[1].Value.ToString().Trim().ToUpper();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvThirdPartyData.CurrentRow.Index > -1)
                {
                    DataGridViewRow row = dgvThirdPartyData.Rows[dgvThirdPartyData.CurrentCell.RowIndex];
                    txtThirdPartyId.Text = dgvThirdPartyData.Rows[dgvThirdPartyData.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    //txtName.Text = dgvThirdPartyData.Rows[dgvThirdPartyData.CurrentCell.RowIndex].Cells[1].Value.ToString().Trim().ToUpper();
                }
                DialogResult value=MessageBox.Show("Are you sure want to delete? You may loss related Data.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (value == DialogResult.Yes)
                {
                    dbcommand = db.GetSqlStringCommand("Delete tblThirdParty where ThirdPartyID='" + txtThirdPartyId.Text.Trim() + "'");
                    result = db.ExecuteNonQuery(dbcommand);
                    if (result > 0)
                    {
                        MessageBox.Show("Deleted Sucessfully.");
                        txtThirdPartyId.Text = "";
                        txtName.Text = "";
                        BindGridDetails();
                    }
                    else
                    {
                        MessageBox.Show("Record not Deleted.");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "")
            {
                txtThirdPartyId.Text = "";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtThirdPartyId.Text = "";
            txtName.Text = "";
        }

        

        private void frmThirdPartyMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }
    }
}
