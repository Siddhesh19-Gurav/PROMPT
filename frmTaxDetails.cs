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
    public partial class frmTaxDetails : Form
    {
        Database db = new Database("PROMPT");
        DbCommand dbcommand;
        DataTable dt = new DataTable();
        int result = 0;
        public frmTaxDetails()
        {
            InitializeComponent();
        }

        private void txtVatt_TextChanged(object sender, EventArgs e)
        {
            if (txtTax.Text.Trim() == "")
            {
                if (txtVatt.Text.Trim() == "")
                {
                    txtTaxDetailsId.Text = "";
                }
            }
        }

        private void txtTax_TextChanged(object sender, EventArgs e)
        {
            if (txtVatt.Text.Trim() == "")
            {
                if (txtTax.Text.Trim() == "")
                {
                    txtTaxDetailsId.Text = "";
                }
            }
        }

        public void BindGridDetails()
        {
            dbcommand = db.GetSqlStringCommand("Select TaxDetailsID,Tax as [Tax],Vat as [Vatt] from tblTaxDetails");
            dt = db.ExecuteDataTable(dbcommand);
            dgvTaxDetails.DataSource = dt;
            dgvTaxDetails.Columns[0].Visible = false;
        }

        private void btvSave_Click(object sender, EventArgs e)
        {
            if (txtTax.Text == "")
            {
                MessageBox.Show("Please enter tax details");
                return;
            }
            if (txtVatt.Text == "")
            {
                MessageBox.Show("Please enter vatt details");
                return;
            }
            if (txtTaxDetailsId.Text == "")
            {
                dbcommand = db.GetSqlStringCommand("SELECT * FROM tblTaxDetails");
                dt = db.ExecuteDataTable(dbcommand);
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("Record already Exits.Please Update or Delete first.");
                    txtVatt.Text = "";
                    txtTax.Text = "";
                    txtTaxDetailsId.Text = "";
                    return;
                }
                dbcommand = db.GetSqlStringCommand("INSERT INTO tblTaxDetails (Tax,Vat) values ('" + txtTax.Text + "','"+txtVatt.Text+"')");
                result = db.ExecuteNonQuery(dbcommand);
                if (result > 0)
                {
                    MessageBox.Show("Inserted Sucessfully.");
                    BindGridDetails();
                    txtTaxDetailsId.Text = "";
                    txtTax.Text = "";
                    txtVatt.Text = "";
                }
                else
                {
                    MessageBox.Show("Record not Inserted.");
                    txtTaxDetailsId.Text = "";
                    txtTax.Text = "";
                    txtVatt.Text = "";
                }
            }
            else
            {
                dbcommand = db.GetSqlStringCommand("Update tblTaxDetails set Tax='" + txtTax.Text + "',Vat='" + txtVatt.Text + "' where TaxDetailsID='" + txtTaxDetailsId.Text.Trim() + "'");
                result = db.ExecuteNonQuery(dbcommand);
                if (result > 0)
                {
                    MessageBox.Show("Updated Sucessfully.");
                    BindGridDetails();
                    txtTaxDetailsId.Text = "";
                    txtTax.Text = "";
                    txtVatt.Text = "";
                }
                else
                {
                    MessageBox.Show("Record not Updated.");
                }
            }
        }

        private void dgvTaxDetails_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dgvTaxDetails.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    dgvTaxDetails.Rows[currentMouseOverRow].Cells[1].Selected = true;
                    dgvTaxDetails.Refresh();
                    cmsRightClickMenu.Show(dgvTaxDetails, e.X, e.Y);
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTaxDetails.CurrentRow.Index > -1)
                {
                    DataGridViewRow row = dgvTaxDetails.Rows[dgvTaxDetails.CurrentCell.RowIndex];
                    txtTaxDetailsId.Text = dgvTaxDetails.Rows[dgvTaxDetails.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    txtTax.Text = dgvTaxDetails.Rows[dgvTaxDetails.CurrentCell.RowIndex].Cells[1].Value.ToString().Trim().ToUpper();
                    txtVatt.Text = dgvTaxDetails.Rows[dgvTaxDetails.CurrentCell.RowIndex].Cells[2].Value.ToString().Trim().ToUpper();
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
                if (dgvTaxDetails.CurrentRow.Index > -1)
                {
                    DataGridViewRow row = dgvTaxDetails.Rows[dgvTaxDetails.CurrentCell.RowIndex];
                    txtTaxDetailsId.Text = dgvTaxDetails.Rows[dgvTaxDetails.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    //txtName.Text = dgvThirdPartyData.Rows[dgvThirdPartyData.CurrentCell.RowIndex].Cells[1].Value.ToString().Trim().ToUpper();
                }
                //DialogResult value = MessageBox.Show("Are you sure want to delete? You may loss related Data.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (value == DialogResult.Yes)
                //{
                dbcommand = db.GetSqlStringCommand("Delete tblTaxDetails where TaxDetailsID='" + txtTaxDetailsId.Text.Trim() + "'");
                    result = db.ExecuteNonQuery(dbcommand);
                    if (result > 0)
                    {
                        MessageBox.Show("Deleted Sucessfully.");
                        txtVatt.Text = "";
                        txtTax.Text = "";
                        txtTaxDetailsId.Text = "";
                        BindGridDetails();
                    }
                    else
                    {
                        MessageBox.Show("Record not Deleted.");
                    }
                //}
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void frmTaxDetails_Load(object sender, EventArgs e)
        {
            try
            {
                BindGridDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmTaxDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }
    }
}
