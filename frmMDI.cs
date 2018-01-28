using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;

namespace PROMPT
{
    
    public partial class frmMDI : Form
    {
        Database database = new Database("PROMPT");
        public frmMDI()
        {
            InitializeComponent();
        }

        public void Closeform()
        {
            List<Form> openForms = new List<Form>();

            foreach (Form frmobj in Application.OpenForms)
                openForms.Add(frmobj);

            foreach (Form frmobj in openForms)
            {
                if (frmobj.Name != "frmMDI")
                {
                    panel1.Visible = false;
                    return;
                }
                else
                {
                    panel1.Visible = true;
                    frmMDI_Load(null, null);
                }
            }
        }

        public void frmMDI_Load(object sender, EventArgs e)
        {
            if (Program.Session == false)
            {
                panel1.Visible = false;
                masterToolStripMenuItem.Visible = false;
                registrationToolStripMenuItem.Visible = false;
                productToolStripMenuItem.Visible = false;
                policyToolStripMenuItem.Visible = false;
                logInToolStripMenuItem.Visible = false;
                logOutToolStripMenuItem.Visible = false;
                resetPassowrdToolStripMenuItem.Visible = false;
                DbCommand dbcommand = database.GetSqlStringCommand("select * from tblSetup");
                DataTable dt = database.ExecuteDataTable(dbcommand);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ActivationType"].ToString() == "M")
                    {
                        if (Convert.ToDateTime(dt.Rows[0]["ActivationDate"].ToString()).AddMonths(1) <= Convert.ToDateTime(DateTime.Now.ToString()))
                        {
                            dbcommand = database.GetSqlStringCommand("Update tblSetup set Status='E'");
                            database.ExecuteNonQuery(dbcommand);
                        }
                    }
                    else if (dt.Rows[0]["ActivationType"].ToString() == "Y")
                    {
                        if (Convert.ToDateTime(dt.Rows[0]["ActivationDate"].ToString()).AddYears(1) <= Convert.ToDateTime(DateTime.Now.ToString()))
                        {
                            dbcommand = database.GetSqlStringCommand("Update tblSetup set Status='E'");
                            database.ExecuteNonQuery(dbcommand);
                        }
                    }
                    if (dt.Rows[0]["Status"].ToString().ToUpper() != "A")
                    {
                        MessageBox.Show("License Expire");
                        frmActivation objA = new frmActivation();
                        objA.MdiParent = this;
                        objA.Show();
                        return;
                    }
                }
                else
                {
                    frmActivation objA = new frmActivation();
                    objA.MdiParent = this;
                    objA.Show();
                    return;
                }


                panel1.Visible = false;
                masterToolStripMenuItem.Visible = false;
                registrationToolStripMenuItem.Visible = false;
                productToolStripMenuItem.Visible = false;
                policyToolStripMenuItem.Visible = false;
                logInToolStripMenuItem.Visible = true;
                logOutToolStripMenuItem.Visible = false;
                dashboardToolStripMenuItem.Visible = false;
                

                frmLogin obj = new frmLogin();
                obj.MdiParent = this;
                obj.Show();
                
            }
            else
            {
                BindExipredDetails();
                CallDetails();
                GetAmountDetails();
                panel1.Visible = true;
                masterToolStripMenuItem.Visible = true;
                registrationToolStripMenuItem.Visible = true;
                productToolStripMenuItem.Visible = true;
                policyToolStripMenuItem.Visible = true;
                logInToolStripMenuItem.Visible = false;
                logOutToolStripMenuItem.Visible = true;
                resetPassowrdToolStripMenuItem.Visible = true;
                dashboardToolStripMenuItem.Visible = true;
            }
        }

        public void BindExipredDetails()
        {
            DbCommand dbcommand = database.GetSqlStringCommand("Update tblCustProductDetails set ProductStatus='No Warrenty' where CONVERT(datetime,ExpiryDate,103) <= CONVERT(Datetime,'" + DateTime.Now.ToString("dd/MM/yyyy") + "',103)");
            database.ExecuteNonQuery(dbcommand);

            dbcommand = database.GetSqlStringCommand("select [CustProductID]"+
      ",c.[CustomerID]"+
     ",cust.CustomerName as [Cust. Name],p.ProductName as Product" +
     ",b.BrandName as Brand"+
     ",cp.Capacity as Capacity"+
     ",[PurchesDate] as [Date]"+
     ",[PurchesAmount] as Amount"+
     ",[ExpiryDate] as [Expiry Date]"+
     ",[ProductStatus] as [Status],"+
     " e.EngineerName as Engineer,"+
     " case when c.ThirdPartyID=0 then 'None' else (select ThirdPartyName from tblThirdParty where ThirdPartyID=c.ThirdPartyID) end as 'Third Party',DATEDIFF(DAY,CONVERT(datetime,ExpiryDate,103),CONVERT(datetime,getdate(),103)) as [Expirey Days]" +
     " from tblCustProductDetails c,tblBrandMaster b,tblProductMaster p,tblcapacityMaster cp,tblCustomerDetails cust,tblEngineerMaster e"+
     " where c.ProductID=p.ProductID and cust.CustomerID=c.CustomerID and c.BrandID=b.BrandID "+
     " and c.CapacityId=cp.CapacityId and c.EngineerId=e.EngineerID and CONVERT(datetime,ExpiryDate,103) <= CONVERT(Datetime,'" + DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy") + "',103) and DATEDIFF(DAY,CONVERT(datetime,ExpiryDate,103),CONVERT(datetime,getdate(),103)) < 30");
            DataTable dt = database.ExecuteDataTable(dbcommand);
            dgvExpiredDetails.DataSource = dt;
            dgvExpiredDetails.Columns[0].Visible = false;
            dgvExpiredDetails.Columns[1].Visible = false;
            dgvExpiredDetails.Columns[6].Visible = false;
            dgvExpiredDetails.Columns[7].Visible = false;
            dgvExpiredDetails.Columns[10].Visible = false;
            dgvExpiredDetails.Columns[11].Visible = false;
            foreach (DataGridViewRow row in dgvExpiredDetails.Rows)
            {
                if (Convert.ToInt32(row.Cells["Expirey Days"].Value) >= 10)
                {
                    row.DefaultCellStyle.ForeColor = Color.Green;
                }
                if (Convert.ToInt32(row.Cells["Expirey Days"].Value) >= 20)
                {
                    row.DefaultCellStyle.ForeColor = Color.Yellow;
                }
                if (Convert.ToInt32(row.Cells["Expirey Days"].Value) >= 30)
                {
                    row.DefaultCellStyle.ForeColor = Color.Red;
                }
            }
            //dgvExpiredDetails.Rows[-1].Cells[2].Selected = true;
        }
        public void CallDetails()
        {
            DbCommand dbcommand = database.GetSqlStringCommand("select CallLogId,a.CustomerId,a.ProductId,CONVERT(varchar,Calldate,103) as [Date],cust.CustomerName AS Name,p.ProductName as Product,re.Reason as 'Complaint' ,CallDescription as [Description],case when CallStatus=1 then 'Pending' when CallStatus=2 then 'Completed' end as [Status], Charges,eng.EngineerName as [Eng Name],EngineerDescription as [Engineer Description]  from tblCallLogDetails a,tblCustomerDetails cust,tblCustProductDetails custp,tblEngineerMaster eng, tblBrandMaster b,tblProductMaster p,tblcapacityMaster c,tblReasonMaster re where re.ReasonID=a.ReasonCode and a.CustomerId=cust.CustomerID and a.ProductId=custp.CustProductID and eng.EngineerID=a.EngineerID and custp.ProductID=p.ProductID and custp.BrandID=b.BrandID and custp.CapacityId=c.CapacityId and CallStatus=1  order by 1 desc");
            DataTable dt = database.ExecuteDataTable(dbcommand);
            dgvCallDetails.DataSource = dt;
            dgvCallDetails.Columns[0].Visible = false;
            dgvCallDetails.Columns[1].Visible = false;
            dgvCallDetails.Columns[2].Visible = false;
            dgvCallDetails.Columns[7].Visible = false;
            dgvCallDetails.Columns[9].Visible = false;
            dgvCallDetails.Columns[10].Visible = false;
            dgvCallDetails.Columns[11].Visible = false;
        }

        public void GetAmountDetails()
        {
            DbCommand dbcommand = database.GetSqlStringCommand("select cust.CustomerID,cpd.CustProductID,cust.CustomerName as 'Cust Name',(convert(varchar,p.ProductName)+'-'+Convert(varchar,cpm.Capacity)+'-'+convert(varchar,b.BrandName)) as Product,amc.AMCCost as 'AMC Cost',amc.AMCPaidAmount as 'Paid Amount',(Convert(money,amc.AMCCost)-convert(money,amc.AMCPaidAmount)) as 'Bal Amount' from tblAMCDetails amc,tblCustProductDetails cpd,tblCustomerDetails cust,tblProductMaster p,tblcapacityMaster cpm," +
" tblBrandMaster b where AMCCost>AMCPaidAmount and cpd.AMCID=amc.AMCID and cpd.CustomerID=cust.CustomerID and cpd.ProductID=p.ProductID and cpd.CapacityId=cpm.CapacityId"+
" and cpd.BrandID=b.BrandID");
            DataTable dt = database.ExecuteDataTable(dbcommand);
            dgvPendingBillDetails.DataSource = dt;
            dgvPendingBillDetails.Columns[0].Visible = false;
            dgvPendingBillDetails.Columns[1].Visible = false;
        }

        private void logInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin obj = new frmLogin();
            obj.MdiParent=this;
            obj.Show();

        }

        public void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Form> openForms = new List<Form>();

            foreach (Form frmobj in Application.OpenForms)
                openForms.Add(frmobj);

            foreach (Form frmobj in openForms)
            {
                if (frmobj.Name != "frmMDI")
                    frmobj.Close();
            }
            Program.Session = false;
            frmMDI_Load(null, null);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Are you sure?", "Conform", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void listOfCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmListofCustomer obj = new frmListofCustomer();
            obj.MdiParent=this;
            obj.Show();
        }

        private void saleProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmAddProduct obj = new frmAddProduct("");
            obj.MdiParent = this;
            obj.Show();
        }

        private void locationMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmLocationMaster obj = new frmLocationMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void customerTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmCustomerTypeMaster obj = new frmCustomerTypeMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void productMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmProductMaster obj = new frmProductMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void brandMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmBrandMaster obj = new frmBrandMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void capacityMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmCapacityMaster obj = new frmCapacityMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        public void addCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmAddCustomer obj = new frmAddCustomer(0,"");
            obj.MdiParent = this;
            obj.Show();
        }

        private void engineerMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmEngineerMaster obj = new frmEngineerMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void addCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmAddCall obj = new frmAddCall();
            //obj.MdiParent = this;
            //obj.Show();
        }

        private void listOfCallsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmCallList obj = new frmCallList();
            obj.MdiParent = this;
            obj.Show();
        }

        private void thirdPartyMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmThirdPartyMaster obj = new frmThirdPartyMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void taxDetailsMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmTaxDetails obj = new frmTaxDetails();
            obj.MdiParent = this;
            obj.Show();
        }

        private void reasonMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmReasonMaster obj = new frmReasonMaster();
            obj.MdiParent = this;
            obj.Show();

        }

        private void dgvCallDetails_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCallDetails.Rows[dgvCallDetails.CurrentRow.Index].Cells["Status"].Value.ToString() == "Completed")
            {
                MessageBox.Show("Call already updated.");
                return;
            }
            string ProductID = dgvCallDetails.Rows[dgvCallDetails.CurrentRow.Index].Cells["ProductId"].Value.ToString();
            string CustomerID = dgvCallDetails.Rows[dgvCallDetails.CurrentRow.Index].Cells["CustomerId"].Value.ToString();
            string CallId = dgvCallDetails.Rows[dgvCallDetails.CurrentRow.Index].Cells["CallLogId"].Value.ToString();
            frmAddCall obj = new frmAddCall(ProductID, CustomerID, CallId,"frmMDI");
            obj.ShowDialog();
        }

        private void dgvExpiredDetails_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvExpiredDetails.CurrentRow.Index > -1)
            {
                DataGridViewRow row = dgvExpiredDetails.Rows[dgvExpiredDetails.CurrentCell.RowIndex];
                frmListOfProduct obj = new frmListOfProduct(Convert.ToInt32(row.Cells["CustomerID"].Value));
                obj.ShowDialog();
            }
        }

        private void dgvPendingBillDetails_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvPendingBillDetails.CurrentRow.Index > -1)
            {
                DataGridViewRow row = dgvPendingBillDetails.Rows[dgvPendingBillDetails.CurrentCell.RowIndex];
                frmListOfProduct obj = new frmListOfProduct(Convert.ToInt32(row.Cells["CustomerID"].Value));
                obj.ShowDialog();
            }
        }

        private void updateCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvPendingBillDetails.CurrentRow.Index > -1)
            {

                DataGridViewRow row = dgvPendingBillDetails.Rows[dgvPendingBillDetails.CurrentCell.RowIndex];
                frmAMCDetails obj = new frmAMCDetails(row.Cells[1].Value.ToString(), row.Cells[0].Value.ToString(), 0,"frmMDI");
                obj.ShowDialog();
            }
        }

        private void dgvPendingBillDetails_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dgvPendingBillDetails.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    dgvPendingBillDetails.Rows[currentMouseOverRow].Cells[4].Selected = true;
                    dgvPendingBillDetails.Refresh();
                    contextMenuStrip1.Show(dgvPendingBillDetails, e.X, e.Y);
                }
            }
        }

        private void resetPassowrdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            frmResetPassword obj = new frmResetPassword();
            obj.MdiParent = this;
            obj.Show();
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Show();
        }
    }
}
