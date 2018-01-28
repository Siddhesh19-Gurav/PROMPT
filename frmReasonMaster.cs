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
    public partial class frmReasonMaster : Form
    {
        Database database = new Database("PROMPT");
        DataTable dt = new DataTable();
        int result = 0;
        DbCommand dbcommand;
        public frmReasonMaster()
        {
            InitializeComponent();
        }

        public void GridBind()
        {
            dbcommand = database.GetSqlStringCommand("SELECT * FROM tblReasonMaster");
            dgvReasonData.DataSource = database.ExecuteDataTable(dbcommand);
            dgvReasonData.Columns[0].Visible = false;
        }

        private void frmReasonMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GridBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtReason.Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter reason");
                    return;
                }
                else if (txtReasonID.Text.Trim() == "")
                {
                    dbcommand = database.GetSqlStringCommand("INSERT INTO tblReasonMaster values('" + txtReason.Text.Trim().ToUpper() + "')");
                    result = database.ExecuteNonQuery(dbcommand);
                    if (result > 0)
                    {
                        MessageBox.Show("Inserted Successfully.");
                        GridBind();
                        txtReason.Text = "";
                        txtReasonID.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Record not inserted");
                    }
                }
                else
                {
                    dbcommand = database.GetSqlStringCommand("UPDATE tblReasonMaster SET Reason='" + txtReason.Text.Trim().ToUpper() + "' WHERE ReasonID='"+txtReasonID.Text.Trim()+"'");
                    result = database.ExecuteNonQuery(dbcommand);
                    if (result > 0)
                    {
                        MessageBox.Show("Updated Successfully.");
                        GridBind();
                        txtReason.Text = "";
                        txtReasonID.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Record not Updated");
                    }
 

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvReasonData_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dgvReasonData.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    dgvReasonData.Rows[currentMouseOverRow].Cells[1].Selected = true;
                    dgvReasonData.Refresh();
                    cmsRightClickMenu.Show(dgvReasonData, e.X, e.Y);
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvReasonData.CurrentRow.Index > -1)
                {
                    DataGridViewRow row = dgvReasonData.Rows[dgvReasonData.CurrentCell.RowIndex];
                    txtReasonID.Text = dgvReasonData.Rows[dgvReasonData.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    txtReason.Text = dgvReasonData.Rows[dgvReasonData.CurrentCell.RowIndex].Cells[1].Value.ToString().Trim().ToUpper();
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
                if (dgvReasonData.CurrentRow.Index > -1)
                {
                    DataGridViewRow row = dgvReasonData.Rows[dgvReasonData.CurrentCell.RowIndex];
                    txtReasonID.Text = dgvReasonData.Rows[dgvReasonData.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    //txtName.Text = dgvThirdPartyData.Rows[dgvThirdPartyData.CurrentCell.RowIndex].Cells[1].Value.ToString().Trim().ToUpper();
                }
                DialogResult value = MessageBox.Show("Are you sure want to delete? You may loss related Data.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (value == DialogResult.Yes)
                {
                    dbcommand = database.GetSqlStringCommand("Delete tblReasonMaster where ReasonID='" + txtReasonID.Text.Trim() + "'");
                    result = database.ExecuteNonQuery(dbcommand);
                    if (result > 0)
                    {
                        MessageBox.Show("Deleted Sucessfully.");
                        txtReasonID.Text = "";
                        txtReason.Text = "";
                        GridBind();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtReason.Text = "";
            txtReasonID.Text = "";
        }

        private void frmReasonMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }
    }
}
