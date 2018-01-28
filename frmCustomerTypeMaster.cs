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
    public partial class frmCustomerTypeMaster : Form
    {
        public frmCustomerTypeMaster()
        {
            InitializeComponent();
        }
        frmCustomerTypeModel model = new frmCustomerTypeModel();
        frmCustomerTypeController controller = new frmCustomerTypeController();

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                model.CustomerType = txtCustomer.Text.ToUpper();
                int result=controller.InsertCustomerTypeDetails(model);
                if (result == 2)
                {
                    MessageBox.Show("Record already exists.");
                    model.CustomerTypeID = 0;
                    txtCustomer.Text = "";
                    return;
                }
                if (result == 1)
                {
                    MessageBox.Show("Record Updated.");
                    model.CustomerTypeID = 0;
                    txtCustomer.Text = "";
                    dgvCustmerType.DataSource = controller.GetCustomerTypeDetails();
                    dgvCustmerType.Columns[0].Visible = false;
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
                dgvCustmerType.DataSource = controller.GetCustomerTypeDetails();
                dgvCustmerType.Columns[0].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvCustmerType_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dgvCustmerType.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    dgvCustmerType.Rows[currentMouseOverRow].Cells[1].Selected = true;
                    dgvCustmerType.Refresh();
                    contextMenuStrip1.Show(dgvCustmerType, e.X, e.Y);
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {

                    if (dgvCustmerType.CurrentRow.Index > -1)
                    {
                        model.CustomerTypeID = Convert.ToInt32(dgvCustmerType.Rows[dgvCustmerType.CurrentCell.RowIndex].Cells[0].Value.ToString());
                        int result = controller.DeleteLCustomerDetails(model);
                        if (result == 1)
                        {
                            MessageBox.Show("Record deleted Successfully.");
                            model.CustomerTypeID = 0;
                            txtCustomer.Text = "";
                            dgvCustmerType.DataSource = controller.GetCustomerTypeDetails();
                            dgvCustmerType.Columns[0].Visible = false;
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

        private void dgvCustmerType_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCustmerType.CurrentRow.Index > -1)
            {
                DataGridViewRow row = dgvCustmerType.Rows[dgvCustmerType.CurrentCell.RowIndex];
                model.CustomerTypeID = Convert.ToInt32(dgvCustmerType.Rows[dgvCustmerType.CurrentCell.RowIndex].Cells[0].Value.ToString());
                txtCustomer.Text = dgvCustmerType.Rows[dgvCustmerType.CurrentCell.RowIndex].Cells[1].Value.ToString().Trim().ToUpper();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCustomerTypeMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }
    }
}
