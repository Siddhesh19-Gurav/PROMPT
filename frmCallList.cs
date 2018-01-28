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
using System.IO;

namespace PROMPT
{
    public partial class frmCallList : Form
    {
        frmCalllistModel model = new frmCalllistModel();
        frmCalllistCotroller controller = new frmCalllistCotroller();
        cls_Excel_Import_Export excel = new cls_Excel_Import_Export();
        Database database = new Database("PROMPT");
        public frmCallList()
        {
            InitializeComponent();
        }

        public void frmCallList_Load(object sender, EventArgs e)
        {
            try
            {
                cmbEngineerID.ValueMember = "EngineerID";
                cmbEngineerID.DisplayMember = "EngineerName";
                cmbEngineerID.DataSource = controller.GetEngineerList();
                if (cmbEngineerID.Items.Count > 0)
                {
                    cmbEngineerID.SelectedIndex = 0;
                }
                cmbThirdParty.ValueMember = "ThirdPartyID";
                cmbThirdParty.DisplayMember = "ThirdPartyName";
                DbCommand dbcommand = database.GetSqlStringCommand("select '0' as ThirdPartyID,'All' as ThirdPartyName union select ThirdPartyID,ThirdPartyName from tblThirdParty order by 1 asc");
                cmbThirdParty.DataSource = database.ExecuteDataTable(dbcommand);
                cmbCallStatus.SelectedIndex = 0;
                model.CustId = "0";
                model.EngineerId = 0;
                model.Status = "0";
                model.fromDate = dtpFromDate.Text;
                model.ToDate = dtpToDate.Text;
                dgvCallList.DataSource = controller.GetcallList(model);
                dgvCallList.Columns[0].Visible = false;
                dgvCallList.Columns[1].Visible = false;
                dgvCallList.Columns[2].Visible = false;
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerName.Text.Trim() != "")
                {
                    model.CustmoerName = txtCustomerName.Text;
                    
                    dgvCustomerName.DataSource = controller.GetCutomerDetails(model);
                    if (dgvCustomerName.Rows.Count > 0)
                    {
                        dgvCustomerName.Visible = true;
                        dgvCustomerName.Columns[0].Visible = false;
                        dgvCustomerName.Columns[2].Visible = false;
                        dgvCustomerName.Columns[3].Visible = false;
                        dgvCustomerName.Columns[4].Visible = false;
                        dgvCustomerName.Columns[5].Visible = false;
                        dgvCustomerName.Columns[6].Visible = false;
                    }
                }
                else
                {
                    dgvCustomerName.Visible = false;
                    dgvCustomerName.DataSource = null;
                    txtCustID.Text="";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvCustomerName_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvCustomerName.CurrentRow.Index >= 0)
                {
                    txtCustomerName.Text = dgvCustomerName.Rows[dgvCustomerName.CurrentRow.Index].Cells[1].Value.ToString().ToUpper();
                    txtCustID.Text = dgvCustomerName.Rows[dgvCustomerName.CurrentRow.Index].Cells[0].Value.ToString();
                    model.CustId = txtCustID.Text.ToString();
                    dgvCustomerName.DataSource = null;
                    dgvCustomerName.Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string Condition = "";
                if (txtCustomerName.Text.Trim() != "")
                {
                    Condition += "AND CustomerName LIKE '%"+txtCustomerName.Text+"%'";
                }
                if (cmbEngineerID.SelectedIndex > 0)
                {
                    Condition += " AND a.EngineerID='" + cmbEngineerID.SelectedValue+"'";
                }
                if (cmbCallStatus.SelectedIndex > 0)
                {
                    Condition += " AND CallStatus='"+cmbCallStatus.SelectedIndex+"'";
                }
                if (cmbThirdParty.SelectedIndex > 0)
                {
                    Condition += " And th.ThirdPartyID='" + cmbThirdParty.SelectedValue + "'";
                }
                if (dtpFromDate.Text != "" && dtpToDate.Text != "")
                {
                    Condition += " And CONVERT(datetime,Calldate,103) between CONVERT(Datetime,'" + dtpFromDate.Text + "',103) and CONVERT(Datetime,'" + dtpFromDate.Text + "',103) ";
                }

                DbCommand dbcommand = database.GetSqlStringCommand("select CallLogId,a.CustomerId,a.ProductId,CONVERT(varchar,Calldate,103) as [Date],cust.CustomerName AS Name,re.Reason as 'Complaint',CallDescription as [Job Done],case when CallStatus=1 then 'Prening' when CallStatus=2 then 'Completed' end as [Status]," +
                                                                    " Charges,PaidAmount as 'Paid Amount',eng.EngineerName as [Eng Name],p.ProductName as Product,c.Capacity as Capcity, " +
                                                                    " EngineerDescription as [Engineer Description],th.ThirdPartyName as 'Dealer Name',CallCompleteDate as [Completed Date] " +
		                                                            " from tblCallLogDetails a,tblCustomerDetails cust,tblCustProductDetails custp,tblEngineerMaster eng,"+
		                                                            " tblBrandMaster b,tblProductMaster p,tblcapacityMaster c,tblReasonMaster re,tblThirdParty th where re.ReasonID=a.ReasonCode and "+
		                                                            " a.CustomerId=cust.CustomerID and a.ProductId=custp.CustProductID and eng.EngineerID=a.EngineerID"+
		                                                            " and custp.ProductID=p.ProductID and custp.BrandID=b.BrandID and custp.CapacityId=c.CapacityId and th.ThirdPartyID=custp.ThirdPartyID "+Condition+" order by 1 desc");
                dgvCallList.DataSource = database.ExecuteDataTable(dbcommand);
                dgvCallList.Columns[0].Visible = false;
                dgvCallList.Columns[1].Visible = false;
                dgvCallList.Columns[2].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomerName.Text = "";
            cmbCallStatus.SelectedIndex = 0;
            cmbEngineerID.SelectedIndex = 0;
            cmbThirdParty.SelectedIndex = 0;
            btnSearch_Click(null, null);
        }

        private void dgvCallList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dgvCallList.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    dgvCallList.Rows[currentMouseOverRow].Cells[5].Selected = true;
                    dgvCallList.Refresh();
                    cmsRightClickMenu.Show(dgvCallList, e.X, e.Y);

                }
            }
        }

        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvCallList.Rows[dgvCallList.CurrentRow.Index].Cells["Status"].Value.ToString() == "Completed")
            {
                MessageBox.Show("Call already updated.");
                return;
            }
            string ProductID=dgvCallList.Rows[dgvCallList.CurrentRow.Index].Cells[2].Value.ToString();
            string CustomerID=dgvCallList.Rows[dgvCallList.CurrentRow.Index].Cells[1].Value.ToString();
            string CallId=dgvCallList.Rows[dgvCallList.CurrentRow.Index].Cells[0].Value.ToString();
            frmAddCall obj = new frmAddCall(ProductID, CustomerID, CallId,"frmCallList");
            obj.ShowDialog();
        }

        private void frmCallList_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
            Cursor.Current = Cursors.WaitCursor;
            string Condition = "";
            if (txtCustomerName.Text.Trim() != "")
            {
                Condition += "AND CustomerName LIKE '%" + txtCustomerName.Text + "%'";
            }
            if (cmbEngineerID.SelectedIndex > 0)
            {
                Condition += " AND a.EngineerID='" + cmbEngineerID.SelectedValue + "'";
            }
            if (cmbCallStatus.SelectedIndex > 0)
            {
                Condition += " AND CallStatus='" + cmbCallStatus.SelectedIndex + "'";
            }
            if (cmbThirdParty.SelectedIndex > 0)
            {
                Condition += " And th.ThirdPartyID='" + cmbThirdParty.SelectedValue + "'";
            }

            string Query = "select CONVERT(varchar,Calldate,103) as [Date],cust.CustomerName AS Name,re.Reason as 'Complaint',CallDescription as [Job Done],case when CallStatus=1 then 'Prening' when CallStatus=2 then 'Completed' end as [Status]," +
		                                                            " Charges,PaidAmount as 'Paid Amount',eng.EngineerName as [Eng Name],p.ProductName as Product,"+
                                                                    " EngineerDescription as [Engineer Description],th.ThirdPartyName as 'Dealer Name',CallCompleteDate as [Completed Date] " +
		                                                            " from tblCallLogDetails a,tblCustomerDetails cust,tblCustProductDetails custp,tblEngineerMaster eng,"+
		                                                            " tblBrandMaster b,tblProductMaster p,tblcapacityMaster c,tblReasonMaster re,tblThirdParty th where re.ReasonID=a.ReasonCode and "+
		                                                            " a.CustomerId=cust.CustomerID and a.ProductId=custp.CustProductID and eng.EngineerID=a.EngineerID"+
		                                                            " and custp.ProductID=p.ProductID and custp.BrandID=b.BrandID and custp.CapacityId=c.CapacityId and th.ThirdPartyID=custp.ThirdPartyID "+Condition+" order by 1 desc";
            if (!Directory.Exists(@"D:\PROMPT\Reports\" + DateTime.Now.ToString("ddMMyyyy")))
            {
                Directory.CreateDirectory(@"D:\PROMPT\Reports\" + DateTime.Now.ToString("ddMMyyyy"));
            }
            excel.Fn_Excel_Export(@"D:\PROMPT\Reports\" + DateTime.Now.ToString("ddMMyyyy")+@"\CustomerCallList"+DateTime.Now.ToString("mmss")+".xlsx", "Data Source=SID-PC;Initial Catalog=dbPROMPT;user id=sa;password=sa2008", Query, true, "Call Log Details");
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Report Generate Sucessfully.", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);

            System.Diagnostics.Process.Start(@"D:\PROMPT\Reports\" + DateTime.Now.ToString("ddMMyyyy"));

            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvCallList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvCallList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string ProductID = dgvCallList.Rows[dgvCallList.CurrentRow.Index].Cells[2].Value.ToString();
            string CustomerID = dgvCallList.Rows[dgvCallList.CurrentRow.Index].Cells[1].Value.ToString();
            string CallId = dgvCallList.Rows[dgvCallList.CurrentRow.Index].Cells[0].Value.ToString();
            ViewCall obj = new ViewCall(ProductID, CustomerID, CallId, "frmCallList");
            obj.ShowDialog();
        }
    }
}
