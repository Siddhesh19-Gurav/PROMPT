using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PROMPT.Controller;
using PROMPT.Model;
using System.Data.Common;

namespace PROMPT
{
    public partial class frmListOfProduct : Form
    {
        public int CustomerId;
        frmAddProductModel model = new frmAddProductModel();
        frmAddProductController controller = new frmAddProductController();
        Database database = new Database("PROMPT");
        public frmListOfProduct(int CustID)
        {
            InitializeComponent();
            CustomerId = CustID;
        }

        DataTable dtCustomeDetails;
        public void frmListOfProduct_Load(object sender, EventArgs e)
        {
            try
            {
                model.CustId = Convert.ToString(CustomerId);
                dtCustomeDetails = controller.GetCustomerEditDetails(model);
                dgvProductDetails.DataSource = controller.GetProductDeatilsofCustomer(model);
                txtName.Text = dtCustomeDetails.Rows[0]["customerName"].ToString().ToUpper();
                txtAddress.Text = dtCustomeDetails.Rows[0]["CustomerAddress"].ToString();
                txtPhoneNo.Text = dtCustomeDetails.Rows[0]["CustomerContactNo"].ToString();
                txtLocation.Text = dtCustomeDetails.Rows[0]["LocationName"].ToString();
                txtCustomerType.Text = dtCustomeDetails.Rows[0]["CustomerType"].ToString();
                txtContactPerson.Text = dtCustomeDetails.Rows[0]["ContactPerson"].ToString();
                txtEngineerName.Text = dtCustomeDetails.Rows[0]["EngineerName"].ToString();
                txtDelareName.Text = dtCustomeDetails.Rows[0]["ThirdPartyName"].ToString();
                //dgvProductDetails.Columns[10].Visible = false;
                //dgvProductDetails.Columns[0].Visible = false;
                dgvProductDetails.Columns[1].Visible = false;
                if (dgvAmcDetails.Rows.Count > 0)
                {
                    DbCommand dbcommand = database.GetSqlStringCommand("select isnull(SUM(cast(charges as money)),0) from tblCallLogDetails WHERE ProductId='" + dgvAmcDetails.Rows[dgvAmcDetails.CurrentRow.Index].Cells["ProductId"].Value.ToString() + "' AND" +
                                " CONVERT(dATETIME,Calldate,103) between CONVERT(datetime,'" + dgvAmcDetails.Rows[dgvAmcDetails.CurrentRow.Index].Cells["From Date"].Value.ToString() + "',103) and CONVERT(datetime,'" + dgvAmcDetails.Rows[dgvAmcDetails.CurrentRow.Index].Cells["To Date"].Value.ToString() + "',103)");
                    txtExpense.Text = database.ExecuteScalar(dbcommand).ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductDetails.CurrentRow.Index > -1)
                {
                    DataGridViewRow row = dgvProductDetails.Rows[dgvProductDetails.CurrentCell.RowIndex];
                    frmAddCall obj = new frmAddCall(row.Cells[0].Value.ToString(), model.CustId,"0","frmListOfProduct");
                    obj.ShowDialog();
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
                //throw;
            }
        }

        private void aDDAMCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvProductDetails.CurrentRow.Index > -1)
            {
                
                DataGridViewRow row = dgvProductDetails.Rows[dgvProductDetails.CurrentCell.RowIndex];
                if (row.Cells["Status"].Value.ToString().ToLower() == "warrenty")
                {
                    MessageBox.Show("Product already in warrenty.");
                    return;
                }
                if (row.Cells["Status"].Value.ToString().ToLower() == "amc")
                {
                    MessageBox.Show("Product already in AMC.");
                    return;
                }
                frmAMCDetails obj = new frmAMCDetails(row.Cells[0].Value.ToString(), model.CustId, 1, "frmListOfProduct");
                obj.ShowDialog();
            }
        }

        private void dgvProductDetails_SelectionChanged(object sender, EventArgs e)
        {
            DbCommand dbcommand = database.GetSqlStringCommand("select CallLogId,a.CustomerId,a.ProductId,CONVERT(varchar,Calldate,103) as [Date],re.Reason,CallDescription as [Description],case when CallStatus=1 then 'Pending' ELSE 'Completed' end as [Status],"+
        " EngineerDescription as [Engineer Description],Charges,PaidAmount as 'Paid Amount'" +
		" from tblCallLogDetails a,tblCustomerDetails cust,tblCustProductDetails custp,tblEngineerMaster eng,"+
		" tblBrandMaster b,tblProductMaster p,tblcapacityMaster c,tblReasonMaster re"+
		" where"+
		" a.CustomerId=cust.CustomerID and a.ProductId=custp.CustProductID and eng.EngineerID=a.EngineerID"+
        " and custp.ProductID=p.ProductID and custp.BrandID=b.BrandID and custp.CapacityId=c.CapacityId and a.ReasonCode=re.ReasonID and a.ProductId='" + dgvProductDetails.Rows[dgvProductDetails.CurrentRow.Index].Cells[0].Value.ToString() + "'" +
		 " order by 1 desc");
            dgvCallDetails.DataSource = database.ExecuteDataTable(dbcommand);
            dgvCallDetails.Columns[0].Visible = false;
            dgvCallDetails.Columns[1].Visible = false;
            dgvCallDetails.Columns[2].Visible = false;
            dgvCallDetails.Columns["Engineer Description"].Visible = false;
            dgvCallDetails.Columns["Description"].Visible = false;
            dbcommand = database.GetSqlStringCommand("SELECT AMCID,[ProductId],CONVERT(varchar,AMCFromDate,103) as [From Date],CONVERT(varchar,AMCToDate,103) as [To Date],[AMCCost] as [AMC Cost],[AMCPaidAmount] as [Paid Amount] FROM [tblAMCDetails] where [ProductId]='" + dgvProductDetails.Rows[dgvProductDetails.CurrentRow.Index].Cells[0].Value.ToString() + "' order by 1 desc");
            dgvAmcDetails.DataSource = database.ExecuteDataTable(dbcommand);
            dgvAmcDetails.Columns[0].Visible = false;
            dgvAmcDetails.Columns[1].Visible = false;
        }

        private void dgvCallDetails_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dgvCallDetails.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    dgvCallDetails.Rows[currentMouseOverRow].Cells[4].Selected = true;
                    dgvCallDetails.Refresh();
                    contextMenuStrip1.Show(dgvCallDetails, e.X, e.Y);
                }
            }
        }

        private void updateCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCallDetails.Rows[dgvCallDetails.CurrentRow.Index].Cells["Status"].Value.ToString() == "Completed")
                {
                    MessageBox.Show("Call already updated.");
                    return;
                }
            string ProductID = dgvCallDetails.Rows[dgvCallDetails.CurrentRow.Index].Cells[2].Value.ToString();
            string CustomerID = dgvCallDetails.Rows[dgvCallDetails.CurrentRow.Index].Cells[1].Value.ToString();
            string CallId = dgvCallDetails.Rows[dgvCallDetails.CurrentRow.Index].Cells[0].Value.ToString();
            frmAddCall obj = new frmAddCall(ProductID, CustomerID, CallId,"frmListOfProduct");
            obj.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmListOfProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }

        private void dgvProductDetails_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dgvProductDetails.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    dgvProductDetails.Rows[currentMouseOverRow].Cells[4].Selected = true;
                    dgvProductDetails.Refresh();
                    if (dgvProductDetails.Rows[currentMouseOverRow].Cells["Status"].Value.ToString().ToUpper() == "AMC")
                    {
                        aDDAMCToolStripMenuItem.Visible=false;
                        updateAMCToolStripMenuItem.Visible=true;
                    }
                    else
                    {
                        aDDAMCToolStripMenuItem.Visible=true;
                        updateAMCToolStripMenuItem.Visible=false;
                    }
                    cmsRightClickMenu.Show(dgvProductDetails, e.X, e.Y);
                }
            }
        }

        private void dgvCallDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string ProductID = dgvCallDetails.Rows[dgvCallDetails.CurrentRow.Index].Cells[2].Value.ToString();
            string CustomerID = dgvCallDetails.Rows[dgvCallDetails.CurrentRow.Index].Cells[1].Value.ToString();
            string CallId = dgvCallDetails.Rows[dgvCallDetails.CurrentRow.Index].Cells[0].Value.ToString();
            ViewCall obj = new ViewCall(ProductID, CustomerID, CallId, "frmListOfProduct");
            obj.ShowDialog();
        }

        private void dgvAmcDetails_SelectionChanged(object sender, EventArgs e)
        {
           // DbCommand dbcommand = database.GetSqlStringCommand("select isnull(SUM(cast(charges as money)),0) from tblCallLogDetails WHERE ProductId='" + dgvAmcDetails.Rows[dgvAmcDetails.CurrentRow.Index].Cells["ProductId"].Value.ToString() + "' AND" +
           //                     " CONVERT(dATETIME,Calldate,103) between CONVERT(datetime,'" + dgvAmcDetails.Rows[dgvAmcDetails.CurrentRow.Index].Cells["From Date"].Value.ToString() + "',103) and CONVERT(datetime,'" + dgvAmcDetails.Rows[dgvAmcDetails.CurrentRow.Index].Cells["To Date"].Value.ToString() + "',103)");
           //txtExpense.Text = database.ExecuteScalar(dbcommand).ToString();
        }

        private void dgvAmcDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DbCommand dbcommand = database.GetSqlStringCommand("select isnull(SUM(cast(charges as money)),0) from tblCallLogDetails WHERE ProductId='" + dgvAmcDetails.Rows[dgvAmcDetails.CurrentRow.Index].Cells["ProductId"].Value.ToString() + "' AND" +
                                " CONVERT(dATETIME,Calldate,103) between CONVERT(datetime,'" + dgvAmcDetails.Rows[dgvAmcDetails.CurrentRow.Index].Cells["From Date"].Value.ToString() + "',103) and CONVERT(datetime,'" + dgvAmcDetails.Rows[dgvAmcDetails.CurrentRow.Index].Cells["To Date"].Value.ToString() + "',103)");
            txtExpense.Text = database.ExecuteScalar(dbcommand).ToString();
        }

        private void updateAMCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvProductDetails.CurrentRow.Index > -1)
            {

                DataGridViewRow row = dgvProductDetails.Rows[dgvProductDetails.CurrentCell.RowIndex];
                frmAMCDetails obj = new frmAMCDetails(row.Cells[0].Value.ToString(), model.CustId,0,"frmMDI");
                obj.ShowDialog();
            }
        }

        private void editProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvProductDetails.CurrentRow.Index > -1)
                {
                    frmAddProduct obj = new frmAddProduct(dgvProductDetails.Rows[dgvProductDetails.CurrentRow.Index].Cells["Product Id"].Value.ToString());
                    obj.ShowDialog();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmAmcRenewal obj = new frmAmcRenewal();
            //obj.ShowDialog();
        }
    }
}
 