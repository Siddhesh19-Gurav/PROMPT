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
    public partial class frmAMCDetails : Form
    {
        frmAddCallController controller = new frmAddCallController();
        frmAddCallModel model = new frmAddCallModel();
        Database database = new Database("PROMPT");
        int NEWAMC=0;
        string frmName = "";
        public frmAMCDetails(string ProductID, String CustId,int AMCType,string frmInName)
        {
            InitializeComponent();
            txtCustID.Text = CustId;
            txtProductID.Text = ProductID;
            //txtCallID.Text = CallID;'
            NEWAMC = AMCType;
            frmName = frmInName;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            //txtDesc.Text = "";
            //cmbCallStatus.SelectedIndex = 0;
            txtCharges.Text = "";
            //cmbEngineerType.SelectedIndex = 0;
            //cmbEngineerName.SelectedIndex = 0;
            //txtThirdPartyName.Text = "";
            //txtThirdPartyName.Visible = false;
            //txtEngineerDesc.Text = "";
            txtPaidAmount.Text = "";
 
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }


        

        private void frmAddCall_Load(object sender, EventArgs e)
        {
                //cmbCallStatus.SelectedIndex = 0;
                txtCallDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //dtpFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                model.CustId = Convert.ToString(txtCustID.Text);
                DataTable dtCustomeDetails = controller.GetCustomerEditDetails(model);
                txtName.Text = dtCustomeDetails.Rows[0]["customerName"].ToString().ToUpper();
                txtAddress.Text = dtCustomeDetails.Rows[0]["CustomerAddress"].ToString();
                txtPhoneNo.Text = dtCustomeDetails.Rows[0]["CustomerContactNo"].ToString();
                txtLocation.Text = dtCustomeDetails.Rows[0]["LocationName"].ToString();
                txtCustomerType.Text = dtCustomeDetails.Rows[0]["CustomerType"].ToString();
                txtContactPerson.Text = dtCustomeDetails.Rows[0]["ContactPerson"].ToString();
                DbCommand dbcommand = database.GetSqlStringCommand("select p.ProductName as Product,b.BrandName as Brand,cp.Capacity as Capacity" +
                                                                           ",[ProductStatus] as [Status]," +
                                                                           "e.EngineerName as Engineer," +
                                                                           "case when c.ThirdPartyID=0 then 'None' else (select ThirdPartyName from tblThirdParty where ThirdPartyID=c.ThirdPartyID) end as 'Third Party'" +
                                                                           "from tblCustProductDetails c,tblBrandMaster b,tblProductMaster p,tblcapacityMaster cp,tblEngineerMaster e " +
                                                                           "where c.ProductID=p.ProductID and c.BrandID=b.BrandID" +
                                                                           " and c.CapacityId=cp.CapacityId and c.EngineerId=e.EngineerID and c.CustProductID='" + txtProductID.Text + "'");
                DataTable dtProductDetails = database.ExecuteDataTable(dbcommand);
                txtProductName.Text = dtProductDetails.Rows[0]["Product"].ToString().ToUpper();
                txtBrandName.Text = dtProductDetails.Rows[0]["Brand"].ToString().ToUpper();
                txtCapacity.Text = dtProductDetails.Rows[0]["Capacity"].ToString().ToUpper();
                txtStatus.Text = dtProductDetails.Rows[0]["Status"].ToString().ToUpper(); ;
                txtThirdparty.Text = dtProductDetails.Rows[0]["Third Party"].ToString().ToUpper();
                if (NEWAMC != 1)
                {
                    dbcommand = database.GetSqlStringCommand("select amc.* from tblAMCDetails amc,tblCustProductDetails cpd where amc.AMCID=cpd.AMCID and cpd.CustProductID='" + txtProductID.Text + "'");
                    DataTable AMCDetails = database.ExecuteDataTable(dbcommand);
                    if (AMCDetails.Rows.Count > 0)
                    {
                        dtpFromDate.Enabled = false;
                        txtAmcYear.Enabled = false;
                        txtAMCId.Text = AMCDetails.Rows[0]["AMCID"].ToString();
                        dtpFromDate.Text = Convert.ToDateTime(AMCDetails.Rows[0]["AMCFromDate"].ToString()).ToString("dd/MM/yyyy");
                        txtAmcYear.Text = Convert.ToInt32(((Convert.ToDateTime(AMCDetails.Rows[0]["AMCToDate"].ToString()) - Convert.ToDateTime(AMCDetails.Rows[0]["AMCFromDate"].ToString())).TotalDays / 365)).ToString();
                        if (Convert.ToDouble(AMCDetails.Rows[0]["AMCCost"].ToString()) == Convert.ToDouble(AMCDetails.Rows[0]["AMCPaidAmount"].ToString()))
                        {
                            txtCharges.Enabled = false;
                            txtPaidAmount.Enabled = false;
                            btnClear.Enabled = false;
                            txtCharges.Text = AMCDetails.Rows[0]["AMCCost"].ToString();
                            txtPaidAmount.Text = AMCDetails.Rows[0]["AMCPaidAmount"].ToString();
                            btnSavePrint.Enabled = false;
                            btnSave.Enabled = false;
                        }
                        else
                        {
                            txtCharges.Enabled = false;
                            btnSavePrint.Enabled = true;
                            btnSave.Enabled = true;
                            btnClear.Enabled = true;
                            txtPaidAmount.Enabled = true;
                            txtCharges.Text = AMCDetails.Rows[0]["AMCCost"].ToString();
                            txtOldPaidAmount.Text = AMCDetails.Rows[0]["AMCPaidAmount"].ToString();
                        }
                    }
                }
                else
                {
                    dtpFromDate.Enabled = true;
                    txtAmcYear.Enabled = true;
                    txtCharges.Enabled = true;
                    txtPaidAmount.Enabled = true;
                }
           
        }

        private void cmbEngineerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbEngineerType.SelectedIndex == 1)
            //{
            //    cmbEngineerName.Visible = true;
            //    txtThirdPartyName.Visible = false;

            //}
            //else if (cmbEngineerType.SelectedIndex == 2)
            //{
            //    cmbEngineerName.Visible = false;
            //    txtThirdPartyName.Visible = true;
            //}
            //else
            //{
            //    cmbEngineerName.Visible = false;
            //    txtThirdPartyName.Visible = false;
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                
                if (NEWAMC == 1)
                {
                    if (txtCharges.Text.Trim() == "")
                    {
                        MessageBox.Show("Please input AMC Changes.");
                        txtCharges.Focus();
                        return;
                    }
                    if (txtPaidAmount.Text.Trim() == "")
                    {
                        MessageBox.Show("Please input paid amount.");
                        txtPaidAmount.Focus();
                        return;
                    }
                    int Year, month;
                    if (txtAmcYear.Text.IndexOf('.') == 1)
                    {
                        string[] addval = txtAmcYear.Text.Split('.');
                        Year = Convert.ToInt32(addval[0]);
                        month = Convert.ToInt32(addval[1] == "" ? "0" : addval[1]);
                    }
                    else
                    {
                        Year = Convert.ToInt32(txtAmcYear.Text.Trim());
                        month = 0;

                    }
                    txtToDate.Text = Convert.ToDateTime(dtpFromDate.Text).AddYears(Year).AddMonths(month).ToString("dd/MM/yyyy");
                    DbCommand dbcommand = database.GetSqlStringCommand("Insert into tblAMCDetails values ('" + txtProductID.Text + "',convert(datetime,'" + dtpFromDate.Text + "',103),convert(datetime,'" + txtToDate.Text + "',103),'" + txtCharges.Text + "','" + txtPaidAmount.Text + "') select @@IDENTITY");
                    int result = Convert.ToInt32(database.ExecuteScalar(dbcommand));
                    if (result > 0)
                    {
                        string ReminderDate = Convert.ToDateTime(txtToDate.Text).AddMonths(-1).ToString("dd/MM/yyyy");
                        string ExiperdDate = Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy");
                        dbcommand = database.GetSqlStringCommand("Update tblCustProductDetails set ReminderDate='" + ReminderDate + "',ExpiryDate='" + ExiperdDate + "',ProductStatus='AMC',AMCID='" + result + "' where CustProductID='" + txtProductID.Text + "'");
                        int Update = database.ExecuteNonQuery(dbcommand);
                        if (Update > 0)
                        {
                            dbcommand = database.GetSqlStringCommand("insert into tblPayment values ('" + result + "',convert(datetime,'" + txtCallDate.Text + "',103),'" + txtCharges.Text + "','" + txtPaidAmount.Text + "','0','A') select @@IDENTITY");
                            int Payment = Convert.ToInt32(database.ExecuteScalar(dbcommand));

                            dbcommand = database.GetSqlStringCommand("update tblPayment set BalanceAmout=(" + txtCharges.Text + "-(select sum(cast(PaidAmount as DECIMAL(9,2))) from tblPayment where PaymentType='A' and AmcProductId='" + result + "')) where PaymentId='" + Payment + "'");
                            int value = database.ExecuteNonQuery(dbcommand);
                            if (value > 0)
                            {
                                MessageBox.Show("Record Inserted Successfully.");
                                var principalForm = Application.OpenForms.OfType<frmListOfProduct>().Single();
                                principalForm.frmListOfProduct_Load(null, null);
                                this.Close();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Record not Inserted.");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Record not Inserted.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Record not Inserted.");
                    }
                }
                else
                {
                    if((Convert.ToDouble(txtPaidAmount.Text)+Convert.ToDouble(txtOldPaidAmount.Text)) > Convert.ToDouble(txtCharges.Text))
                    {
                        MessageBox.Show("Paid Amount is greater then AMC amount Please verify.");
                        return;
                    }
                    DbCommand dbcommand = database.GetSqlStringCommand("Update tblAMCDetails  set AMCPaidAmount='" + (Convert.ToDouble(txtPaidAmount.Text)+Convert.ToDouble(txtOldPaidAmount.Text)) + "' where AMCID='"+txtAMCId.Text+"'");
                    int result = Convert.ToInt32(database.ExecuteNonQuery(dbcommand));
                    if (result > 0)
                    {
                        dbcommand = database.GetSqlStringCommand("insert into tblPayment values ('" + txtAMCId.Text + "',convert(datetime,'" + txtCallDate.Text + "',103),'" + txtCharges.Text + "','" + txtPaidAmount.Text + "','0','A')");
                            int Payment = Convert.ToInt32(database.ExecuteNonQuery(dbcommand));
                            if (Payment > 0)
                            {
                                MessageBox.Show("Record Inserted Successfully.");
                                if (frmName == "frmListOfProduct")
                                {
                                    var principalForm = Application.OpenForms.OfType<frmListOfProduct>().Single();
                                    principalForm.frmListOfProduct_Load(null, null);
                                    this.Close();
                                    return;
                                }
                                else
                                {
                                    var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
                                    principalForm.frmMDI_Load(null, null);
                                    this.Close();
                                    return;
                                }
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Record not Inserted.");
                            return;
                        }
                    }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try{  
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnSavePrint_Click(object sender, EventArgs e)
        {
            try
            {


                if (NEWAMC == 1)
                {
                    //if(txtFromDate.Text.Trim() == "  /  /")
                    //{
                    //    MessageBox.Show("Please input from date.");
                    //    txtFromDate.Focus();
                    //    return;
                    //}
                    //if (txtToDate.Text.Trim() == "  /  /")
                    //{
                    //    MessageBox.Show("Please input To date.");
                    //    txtToDate.Focus();
                    //    return;
                    //}
                    if (txtCharges.Text.Trim() == "")
                    {
                        MessageBox.Show("Please input AMC Changes.");
                        txtCharges.Focus();
                        return;
                    }
                    if (txtPaidAmount.Text.Trim() == "")
                    {
                        MessageBox.Show("Please input paid amount.");
                        txtPaidAmount.Focus();
                        return;
                    }
                    //if (txtCallDate.Text.Trim() == "  /  /")
                    //{
                    //    MessageBox.Show("Please input Date.");
                    //    txtCallDate.Focus();
                    //    return;
                    //}
                    int Year, month;
                    if (txtAmcYear.Text.IndexOf('.') == 1)
                    {
                        string[] addval = txtAmcYear.Text.Split('.');
                        Year = Convert.ToInt32(addval[0]);
                        month = Convert.ToInt32(addval[1] == "" ? "0" : addval[1]);
                    }
                    else
                    {
                        Year = Convert.ToInt32(txtAmcYear.Text.Trim());
                        month = 0;

                    }
                    txtToDate.Text = Convert.ToDateTime(dtpFromDate.Text).AddYears(Year).AddMonths(month).ToString("dd/MM/yyyy");
                    DbCommand dbcommand = database.GetSqlStringCommand("Insert into tblAMCDetails values ('" + txtProductID.Text + "',convert(datetime,'" + dtpFromDate.Text + "',103),convert(datetime,'" + txtToDate.Text + "',103),'" + txtCharges.Text + "','" + txtPaidAmount.Text + "') select @@IDENTITY");
                    int result = Convert.ToInt32(database.ExecuteScalar(dbcommand));
                    if (result > 0)
                    {
                        string ReminderDate = Convert.ToDateTime(txtToDate.Text).AddMonths(-1).ToString("dd/MM/yyyy");
                        string ExiperdDate = Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy");
                        dbcommand = database.GetSqlStringCommand("Update tblCustProductDetails set ReminderDate='" + ReminderDate + "',ExpiryDate='" + ExiperdDate + "',ProductStatus='AMC',AMCID='" + result + "' where CustProductID='" + txtProductID.Text + "'");
                        int Update = database.ExecuteNonQuery(dbcommand);
                        if (Update > 0)
                        {
                            dbcommand = database.GetSqlStringCommand("insert into tblPayment values ('" + result + "',convert(datetime,'" + txtCallDate.Text + "',103),'" + txtCharges.Text + "','" + txtPaidAmount.Text + "','0','A') select @@IDENTITY");
                            int Payment = Convert.ToInt32(database.ExecuteScalar(dbcommand));

                            dbcommand = database.GetSqlStringCommand("update tblPayment set BalanceAmout=(" + txtCharges.Text + "-(select sum(cast(PaidAmount as DECIMAL(9,2))) from tblPayment where PaymentType='A' and AmcProductId='" + result + "')) where PaymentId='" + Payment + "'");
                            int value = database.ExecuteNonQuery(dbcommand);
                            if (value > 0)
                            {
                                MessageBox.Show("Record Inserted Successfully.");
                                var principalForm = Application.OpenForms.OfType<frmListOfProduct>().Single();
                                principalForm.frmListOfProduct_Load(null, null);
                                this.Close();
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Record not Inserted.");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Record not Inserted.");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Record not Inserted.");
                    }
                }
                else
                {
                    DbCommand dbcommand = database.GetSqlStringCommand("Update tblAMCDetails  set AMCPaidAmount='" + txtPaidAmount.Text + "' where AMCID='" + txtAMCId.Text + "'");
                    int result = Convert.ToInt32(database.ExecuteNonQuery(dbcommand));
                    if (result > 0)
                    {
                        dbcommand = database.GetSqlStringCommand("insert into tblPayment values ('" + txtAMCId.Text + "',convert(datetime,'" + txtCallDate.Text + "',103),'" + txtCharges.Text + "','" + txtPaidAmount.Text + "','0','A')");
                        int Payment = Convert.ToInt32(database.ExecuteNonQuery(dbcommand));
                        if (Payment > 0)
                        {
                            MessageBox.Show("Record Inserted Successfully.");
                            if (frmName == "frmListOfProduct")
                            {
                                var principalForm = Application.OpenForms.OfType<frmListOfProduct>().Single();
                                principalForm.frmListOfProduct_Load(null, null);
                                this.Close();
                                return;
                            }
                            else
                            {
                                var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
                                principalForm.frmMDI_Load(null, null);
                                this.Close();
                                return;
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Record not Inserted.");
                        return;
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }

        private void frmAMCDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }
    }
}
