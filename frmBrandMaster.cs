using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PROMPT.Model;
using PROMPT.Controller;
using System.Data.Common;

namespace PROMPT
{
    public partial class frmBrandMaster : Form
    {
        public frmBrandMaster()
        {
            InitializeComponent();
        }
        frmBrandMasterModel model = new frmBrandMasterModel();
        frmBrandMasterController controller = new frmBrandMasterController();
        Database db=new Database("PROMPT");
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                model.BrandName = txtBrand.Text.ToUpper();
                int result=controller.InsertBrandMasterDetails(model);
                if (result == 2)
                {
                    MessageBox.Show("Record already exists.");
                    model.BrandID = 0;
                    txtBrand.Text = "";
                    return;
                }
                if (result == 1)
                {
                    MessageBox.Show("Record Updated.");
                    model.BrandID = 0;
                    txtBrand.Text = "";
                    dgvBrand.DataSource = controller.GetBrandMasterDetails();
                    dgvBrand.Columns[0].Visible = false;
                }
                else
                {
                    MessageBox.Show("Record not Updated.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void frmLocationMaster_Load(object sender, EventArgs e)
        {
            try
            {
                dgvBrand.DataSource=controller.GetBrandMasterDetails();
                dgvBrand.Columns[0].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvLocation_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dgvBrand.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    dgvBrand.Rows[currentMouseOverRow].Cells[1].Selected = true;
                    dgvBrand.Refresh();
                    contextMenuStrip1.Show(dgvBrand, e.X, e.Y);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvBrand.CurrentRow.Index > -1)
            {
                DataGridViewRow row = dgvBrand.Rows[dgvBrand.CurrentCell.RowIndex];
                //txtBrand.Text = ;
                if(DialogResult.Yes== MessageBox.Show("Do you want delete record??", "Message", MessageBoxButtons.YesNo))
                {
                DbCommand dbcommand = db.GetSqlStringCommand("Delete tblBrandMaster where BrandID='" + dgvBrand.Rows[dgvBrand.CurrentCell.RowIndex].Cells[0].Value.ToString().Trim().ToUpper() + "'");
                int result = db.ExecuteNonQuery(dbcommand);
                if (result > 0)
                {
                    MessageBox.Show("Deleted sucessfully");
                    frmLocationMaster_Load(null, null);
                }}
            }

        }

        private void dgvLocation_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvBrand.CurrentRow.Index > -1)
            {
                DataGridViewRow row = dgvBrand.Rows[dgvBrand.CurrentCell.RowIndex];
                model.BrandID = Convert.ToInt32(dgvBrand.Rows[dgvBrand.CurrentCell.RowIndex].Cells[0].Value.ToString());
                txtBrand.Text = dgvBrand.Rows[dgvBrand.CurrentCell.RowIndex].Cells[1].Value.ToString().Trim().ToUpper();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBrandMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }
    }
}
