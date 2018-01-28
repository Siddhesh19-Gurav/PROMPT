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
    public partial class frmEngineerMaster : Form
    {
        frmEngineerController controller = new frmEngineerController();
        frmEngineerModel model = new frmEngineerModel();
        public frmEngineerMaster()
        {
            InitializeComponent();
        }

        private void frmEngineerMaster_Load(object sender, EventArgs e)
        {
            try
            {
                model.EngineerId = 0;
                dgvEngineer.DataSource=controller.getEngineerDetails(model);
                dgvEngineer.Columns[0].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                model.EngineerName = txtEngineerName.Text.Trim().ToUpper();
                int result= controller.InsertEngineerDetails(model);
                if (result >= 1)
                {
                    MessageBox.Show("Data Inserted Sucessfully");
                    model.EngineerId = 0;
                    txtEngineerName.Text = "";
                    dgvEngineer.DataSource = controller.getEngineerDetails(model);
                    dgvEngineer.Columns[0].Visible = false;
                }
                else
                {
                    MessageBox.Show("Data not Inserted Sucessfully");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEngineer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEngineer.CurrentRow.Index > -1)
            {
                DataGridViewRow row = dgvEngineer.Rows[dgvEngineer.CurrentCell.RowIndex];
                model.EngineerId = Convert.ToInt32(row.Cells[0].Value.ToString());
                txtEngineerName.Text=row.Cells[1].Value.ToString();
            }
        }

        private void frmEngineerMaster_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }
    }
}
