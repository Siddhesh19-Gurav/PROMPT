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

namespace PROMPT
{
    public partial class frmLocationMaster : Form
    {
        public frmLocationMaster()
        {
            InitializeComponent();
        }
        frmLocationModel model=new frmLocationModel();
        frmLocationController controller = new frmLocationController();

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                model.LocationName = txtLocation.Text.ToUpper();
                int result=controller.InsertLocationDetails(model);
                if (result == 2)
                {
                    MessageBox.Show("Record already exists.");
                    model.LocationId = 0;
                    txtLocation.Text = "";
                    return;
                }
                if (result == 1)
                {
                    MessageBox.Show("Record Updated.");
                    model.LocationId = 0;
                    txtLocation.Text = "";
                    dgvLocation.DataSource = controller.GetLocationDetails();
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
                dgvLocation.DataSource=controller.GetLocationDetails();
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
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    if (dgvLocation.CurrentRow.Index > -1)
                    {
                        model.LocationId = Convert.ToInt32(dgvLocation.Rows[dgvLocation.CurrentCell.RowIndex].Cells[0].Value.ToString());
                        int result = controller.DeleteLocationDetails(model);
                        if (result == 1)
                        {
                            MessageBox.Show("Record deleted Successfully");
                            model.LocationId = 0;
                            txtLocation.Text = "";
                            dgvLocation.DataSource = controller.GetLocationDetails();
                            dgvLocation.Columns[0].Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("Record in use.You cant delete Record");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvLocation_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLocation.CurrentRow.Index > -1)
            {
                DataGridViewRow row = dgvLocation.Rows[dgvLocation.CurrentCell.RowIndex];
                model.LocationId = Convert.ToInt32(dgvLocation.Rows[dgvLocation.CurrentCell.RowIndex].Cells[0].Value.ToString());
                txtLocation.Text = dgvLocation.Rows[dgvLocation.CurrentCell.RowIndex].Cells[1].Value.ToString().Trim().ToUpper();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLocationMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }
    }
}
