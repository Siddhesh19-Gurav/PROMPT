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
    public partial class frmAddCall : Form
    {
        frmAddCallController controller = new frmAddCallController();
        frmAddCallModel model = new frmAddCallModel();
        Database database = new Database("PROMPT");

        public frmAddCall(string ProductID, String CustId,string CallID,string FormName)
        {
            InitializeComponent();
            txtCustID.Text = CustId;
            txtProductID.Text = ProductID;
            txtCallID.Text = CallID;
            txtFormName.Text = FormName;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Clear()
        {
            txtDesc.Text = "";
            cmbCallStatus.SelectedIndex = 0;
            txtCharges.Text = "";
            //cmbEngineerType.SelectedIndex = 0;
            cmbEngineerName.SelectedIndex = 0;
            //txtThirdPartyName.Text = "";
            //txtThirdPartyName.Visible = false;
            txtEngineerDesc.Text = "";
            txtPaidAmount.Text = "";
 
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }


        

        private void frmAddCall_Load(object sender, EventArgs e)
        {
            cmbCallStatus.SelectedIndex = 0;
            dtpCallDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            model.CustId = Convert.ToString(txtCustID.Text);
           DataTable dtCustomeDetails = controller.GetCustomerEditDetails(model);
           txtName.Text = dtCustomeDetails.Rows[0]["customerName"].ToString().ToUpper();
           txtAddress.Text = dtCustomeDetails.Rows[0]["CustomerAddress"].ToString();
           txtPhoneNo.Text = dtCustomeDetails.Rows[0]["CustomerContactNo"].ToString();
           txtLocation.Text = dtCustomeDetails.Rows[0]["LocationName"].ToString();
           txtCustomerType.Text = dtCustomeDetails.Rows[0]["CustomerType"].ToString();
           DbCommand dbcommand = database.GetSqlStringCommand("select p.ProductName as Product,b.BrandName as Brand,cp.Capacity as Capacity" +
                                                                      ",[ProductStatus] as [Status],"+
                                                                      "e.EngineerName as Engineer,"+
                                                                      "case when c.ThirdPartyID=0 then 'None' else (select ThirdPartyName from tblThirdParty where ThirdPartyID=c.ThirdPartyID) end as 'Third Party'"+
                                                                      "from tblCustProductDetails c,tblBrandMaster b,tblProductMaster p,tblcapacityMaster cp,tblEngineerMaster e "+
                                                                      "where c.ProductID=p.ProductID and c.BrandID=b.BrandID" +
                                                                      " and c.CapacityId=cp.CapacityId and c.EngineerId=e.EngineerID and c.CustProductID='" + txtProductID.Text + "'");
           DataTable dtProductDetails = database.ExecuteDataTable(dbcommand);
           txtProductName.Text = dtProductDetails.Rows[0]["Product"].ToString().ToUpper();
           txtBrandName.Text = dtProductDetails.Rows[0]["Brand"].ToString().ToUpper();
           txtCapacity.Text = dtProductDetails.Rows[0]["Capacity"].ToString().ToUpper();
           //if (dtProductDetails.Rows[0]["Status"].ToString() == "1")
           //{
               txtStatus.Text = dtProductDetails.Rows[0]["Status"].ToString(); 
           //}
            //if (dtProductDetails.Rows[0]["Status"].ToString() == "AMC" || )
            //{
            //   //txtStatus.Text = "AMC";
            //}
           //else if (dtProductDetails.Rows[0]["Status"].ToString() == "0")
           //{
           //    txtStatus.Text = "Expired";
           //}
           txtThirdparty.Text = dtProductDetails.Rows[0]["Third Party"].ToString().ToUpper();
           DataSet bindDetails = controller.GetDataToBind();
           cmbEngineerName.DisplayMember = "EngineerName";
           cmbEngineerName.ValueMember = "EngineerID";
           cmbEngineerName.DataSource = bindDetails.Tables[3];

           cmbComplaint.DisplayMember = "Reason";
           cmbComplaint.ValueMember = "ReasonID";
           cmbComplaint.DataSource = bindDetails.Tables[5];
            int i = 0;
            foreach (DataRowView Value in cmbEngineerName.Items)
            {
                if (Value[1].ToString() == dtProductDetails.Rows[0]["Engineer"].ToString())
                {
                    cmbEngineerName.SelectedIndex = i;
                    break;
                }
                i++;
            }
            if (txtCallID.Text != "0")
           {
               dbcommand = database.GetSqlStringCommand("SELECT [CallLogId],[Calldate],[CustomerId],[ProductId],[ReasonCode],[CallDescription],[CallStatus],[Charges],[EngineerID],[EngineerDescription],[PaidAmount],CallCompleteDate FROM tblCallLogDetails where CallLogId='" + txtCallID.Text + "'");
                DataTable callDetails= database.ExecuteDataTable(dbcommand);
                dtpCallDate.Text = Convert.ToDateTime(callDetails.Rows[0]["Calldate"].ToString()).ToString("dd/MM/yyyy");
                txtDesc.Text = callDetails.Rows[0]["CallDescription"].ToString();
                txtEngineerDesc.Text = callDetails.Rows[0]["EngineerDescription"].ToString();
                txtCharges.Text = callDetails.Rows[0]["Charges"].ToString();
                txtPaidAmount.Text = callDetails.Rows[0]["PaidAmount"].ToString();
                cmbCallStatus.SelectedIndex = Convert.ToInt32(callDetails.Rows[0]["CallStatus"].ToString());
                dtpCallCompleteDate.Text = Convert.ToDateTime(callDetails.Rows[0]["CallCompleteDate"].ToString()).ToString("dd/MM/yyyy");
                //int i = 0;
                //foreach (DataRowView Value in cmbEngineerName.Items)
                //{
                //    if (Value[0].ToString() == callDetails.Rows[0]["EngineerID"].ToString())
                //    {
                //        cmbEngineerName.SelectedIndex = i;
                //        break;
                //    }
                //    i++;
                //}
                i = 0;
                foreach (DataRowView Value in cmbComplaint.Items)
                {
                    if (Value[0].ToString() == callDetails.Rows[0]["ReasonCode"].ToString())
                    {
                        cmbComplaint.SelectedIndex = i;
                        break;
                    }
                    i++;
                }
           }
            //cmbCallStatus.SelectedIndex = 0;
            ////cmbEngineerType.SelectedIndex = 0;
            //try
            //{
            //    DataSet dsBindCmb = controller.GetDataToBind();
            //    cmbEngineerName.DisplayMember = "EngineerName";
            //    cmbEngineerName.ValueMember = "EngineerID";
            //    cmbEngineerName.DataSource = dsBindCmb.Tables[3];
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
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
                if (cmbCallStatus.SelectedIndex <= 0)
                {
                    MessageBox.Show("Please select call status");
                    return;
                }
                if (txtStatus.Text == "Expired")
                {
                    if (Convert.ToDouble(txtPaidAmount.Text) < Convert.ToDouble(txtCharges.Text))
                    {
                        MessageBox.Show("Paid amount remaining.");
                        return;
                    }
                }
                DbCommand dbcommand;
                string Status;
                string Amount,Charges;
                if (txtCallID.Text == "0")
                {
                    Status = cmbCallStatus.SelectedIndex == 1 ? "1" : "2";
                    Amount = txtPaidAmount.Text == "" ? "0" : txtPaidAmount.Text;
                    Charges = txtCharges.Text == "" ? "0" : txtCharges.Text;
                    dbcommand = database.GetSqlStringCommand("INSERT INTO tblCallLogDetails values (convert(Datetime,'" + dtpCallDate.Text + "',103),'" + txtCustID.Text + "','" + txtProductID.Text + "','" + cmbComplaint.SelectedValue + "','" + txtDesc.Text + "','" + Status + "','" + Charges + "','" + cmbEngineerName.SelectedValue + "','" + txtEngineerDesc.Text + "','" + Amount + "',convert(Datetime,'" + dtpCallCompleteDate.Text + "',103),'"+txtStatus.Text+"')");
                    int result = database.ExecuteNonQuery(dbcommand);
                    if (result > 0)
                    {
                        MessageBox.Show("Inserted SucessFully");
                    }
                    else
                    {
                        MessageBox.Show("Recoard not inserted");
                    }
                    Clear();
                }
                else
                {
                     Status = cmbCallStatus.SelectedIndex == 1 ? "1" : "2";
                     dbcommand = database.GetSqlStringCommand("UPDATE tblCallLogDetails SET ReasonCode='" + cmbComplaint.SelectedValue + "',CallDescription='" + txtDesc.Text + "',CallStatus='" + Status + "',Charges='" + txtCharges.Text + "',EngineerID='" + cmbEngineerName.SelectedValue + "',EngineerDescription='" + txtEngineerDesc.Text + "',PaidAmount='" + txtPaidAmount.Text + "',CallCompleteDate=convert(Datetime,'" + dtpCallCompleteDate.Text + "',103) WHERE CallLogId='" + txtCallID.Text + "'");
                     int result = database.ExecuteNonQuery(dbcommand);
                     if (result > 0)
                     {
                         MessageBox.Show("Updated SucessFully");
                     }
                     else
                     {
                         MessageBox.Show("Recoard not Updated");
                     }
                     
                }
                this.Close();
                if ("frmListOfProduct" == txtFormName.Text)
                {
                    var principalForm = Application.OpenForms.OfType<frmListOfProduct>().Single();
                    principalForm.frmListOfProduct_Load(null, null);
                }
                else if ("frmMDI" == txtFormName.Text)
                {
                    var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
                    principalForm.frmMDI_Load(null, null);
                }
                else if ("frmCallList" == txtFormName.Text)
                {
                    var principalForm = Application.OpenForms.OfType<frmCallList>().Single();
                    principalForm.frmCallList_Load(null, null);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void frmAddCall_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }

        private void cmbCallStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCallStatus.SelectedIndex == 2)
            {
                dtpCallCompleteDate.Enabled = true;
            }
            else
            {
                dtpCallCompleteDate.Enabled = false;
            }
        }
    }
}
