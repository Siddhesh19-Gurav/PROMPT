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
    public partial class frmProductMaster : Form
    {
        public frmProductMaster()
        {
            InitializeComponent();
        }
        Database db = new Database("PROMPT");
        frmProductMastertModel model = new frmProductMastertModel();
        frmProductMasterController controller = new frmProductMasterController();

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                model.ProductName = txtProduct.Text.ToUpper();
                int result=controller.InsertProductMasterDetails(model);
                if (result == 2)
                {
                    MessageBox.Show("Record already exists.");
                    model.ProductId = 0;
                    txtProduct.Text = "";
                    return;
                }
                if (result == 1)
                {
                    MessageBox.Show("Record Updated.");
                    model.ProductId = 0;
                    txtProduct.Text = "";
                    dgvLocation.DataSource = controller.GetProductMasterDetails();
                    dgvLocation.Columns[0].Visible = false;
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

        private void frmLocationMaster_Load(object sender, EventArgs e)
        {
            try
            {
                dgvLocation.DataSource=controller.GetProductMasterDetails();
                dgvLocation.Columns[0].Visible = false;

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
                int currentMouseOverRow = dgvLocation.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    dgvLocation.Rows[currentMouseOverRow].Cells[1].Selected = true;
                    dgvLocation.Refresh();
                    contextMenuStrip1.Show(dgvLocation, e.X, e.Y);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLocation.CurrentRow.Index > -1)
            {
                DataGridViewRow row = dgvLocation.Rows[dgvLocation.CurrentCell.RowIndex];
                 if (DialogResult.Yes == MessageBox.Show("Do you want delete record??", "Message", MessageBoxButtons.YesNo))
                {
                    DbCommand dbcommand = db.GetSqlStringCommand("Delete tblProductMaster where ProductID='" + dgvLocation.Rows[dgvLocation.CurrentCell.RowIndex].Cells[0].Value.ToString().Trim().ToUpper() + "'");
                    int result = db.ExecuteNonQuery(dbcommand);
                    if (result > 0)
                    {
                        MessageBox.Show("Deleted sucessfully");
                        frmLocationMaster_Load(null, null);
                    }
                }
            }

        }

        private void dgvLocation_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLocation.CurrentRow.Index > -1)
            {
                DataGridViewRow row = dgvLocation.Rows[dgvLocation.CurrentCell.RowIndex];
                model.ProductId = Convert.ToInt32(dgvLocation.Rows[dgvLocation.CurrentCell.RowIndex].Cells[0].Value.ToString());
                txtProduct.Text = dgvLocation.Rows[dgvLocation.CurrentCell.RowIndex].Cells[1].Value.ToString().Trim().ToUpper();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProductMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }
    }
}
