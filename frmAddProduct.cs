using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PROMPT.Model;
using PROMPT.Controller;
using System.Data.Common;

namespace PROMPT
{
    public partial class frmAddProduct : Form
    {
        frmAddProductModel model = new frmAddProductModel();
        frmAddProductController controller = new frmAddProductController();
        //int CustomerID = 0;
        Database database = new Database("PROMPT");
        string ProductId = "";
        public frmAddProduct(string ProdId)
        {
            ProductId = ProdId;
            InitializeComponent();
        }
        DataSet dsBindCmb;
       // DataTable dtCustDetais;
        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            try
            {
                cmbProductType.SelectedIndex = 0;
                cmbPaymentType.SelectedIndex = 0;
                dsBindCmb = controller.GetDataToBind();
                if (dsBindCmb != null)
                {
                    cmbBrand.DisplayMember = "BrandName";
                    cmbBrand.ValueMember = "BrandID";
                    cmbBrand.DataSource = dsBindCmb.Tables[0];

                    cmbProduct.DisplayMember = "ProductName";
                    cmbProduct.ValueMember = "ProductID";
                    cmbProduct.DataSource = dsBindCmb.Tables[1];

                    cmbCapacity.DisplayMember = "Capacity";
                    cmbCapacity.ValueMember = "CapacityID";
                    cmbCapacity.DataSource = dsBindCmb.Tables[2];

                    cmbEngineer.DisplayMember = "EngineerName";
                    cmbEngineer.ValueMember = "EngineerID";
                    cmbEngineer.DataSource = dsBindCmb.Tables[3];

                    cmbThirdParty.DisplayMember = "ThirdPartyName";
                    cmbThirdParty.ValueMember = "ThirdPartyID";
                    cmbThirdParty.DataSource = dsBindCmb.Tables[4];

                    if (ProductId != "")
                    {
                      DbCommand dbcommand= database.GetSqlStringCommand("select cust.CustomerName,cust.CustomerID,custp.ProductID,custp.BrandID,custp.CapacityId,custp.PurchesAmount,custp.PurchesDate,DATEDIFF(yy,Convert(date,custp.PurchesDate,103),convert(date,custp.ExpiryDate,103) ) as Warrenty " +
                                                     " from tblCustomerDetails cust,tblCustProductDetails custp " +
                                                     " where cust.CustomerID=custp.CustomerID and custp.CustProductID='" + ProductId + "'");
                      DataTable dt = database.ExecuteDataTable(dbcommand);
                      if (dt.Rows.Count > 0)
                      {
                          btnSave.Text = "UPDATE";
                          txtCustomerName.Text = dt.Rows[0]["CustomerName"].ToString();
                          txtCustID.Text = dt.Rows[0]["CustomerID"].ToString();
                          cmbProduct.SelectedValue = dt.Rows[0]["ProductID"].ToString();
                          cmbBrand.SelectedValue = dt.Rows[0]["BrandID"].ToString();
                          cmbCapacity.SelectedValue = dt.Rows[0]["CapacityId"].ToString();
                          txtProducCost.Text = dt.Rows[0]["PurchesAmount"].ToString();
                          txtWarrenty.Text = dt.Rows[0]["Warrenty"].ToString();
                          dtpPurchesDate.Text = Convert.ToDateTime(dt.Rows[0]["PurchesDate"].ToString()).ToString();
                      }
                      else
                      {
                          MessageBox.Show("Unable to edit");
                          this.Close();
                      }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            
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
                        //dgvCustomerName.Columns[0].Visible = false;
                        dgvCustomerName.Columns[2].Visible = false;
                        dgvCustomerName.Columns[3].Visible = false;
                        dgvCustomerName.Columns[4].Visible = false;
                        dgvCustomerName.Columns[5].Visible = false;
                        dgvCustomerName.Columns["CustomerId"].Visible = false;
                        dgvCustomerName.Columns["EngineerId"].Visible = false;
                    }
                }
                else
                {
                    dgvCustomerName.Visible = false;
                    dgvCustomerName.DataSource = null;
                    dgvProductDetails.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvCustomerName_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCustomerName.CurrentRow.Index >= 0)
            {
                txtCustID.Text = dgvCustomerName.Rows[dgvCustomerName.CurrentCell.RowIndex].Cells[0].Value.ToString();
                txtEngID.Text = dgvCustomerName.Rows[dgvCustomerName.CurrentCell.RowIndex].Cells["EngineerId"].Value.ToString();
                txtCustomerName.Text = dgvCustomerName.Rows[dgvCustomerName.CurrentCell.RowIndex].Cells[1].Value.ToString().ToUpper();
                model.CustId = txtCustID.Text.ToString();
                dgvProductDetails.DataSource = controller.GetProductDeatilsofCustomer(model);
                //dgvProductDetails.Columns[0].Visible = false;
                //dgvProductDetails.Columns[1].Visible = false;
                dgvProductDetails.Columns["CustomerID"].Visible = false;
                dgvCustomerName.DataSource = null;
                dgvCustomerName.Visible = false;
            }
        }

        private void txtPaidAmount_Leave(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

            if (txtCustomerName.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Customer name.");
                txtCustomerName.Focus();
                return;
            }
            if (cmbProduct.SelectedIndex == 0)
            {
                MessageBox.Show("Please select Product.");
                cmbProduct.Focus();
                return; 
            }
            if (cmbBrand.SelectedIndex == 0)
            {
                MessageBox.Show("Please select Brand.");
                cmbBrand.Focus();
                return; 

            }
            if (cmbCapacity.SelectedIndex == 0)
            {
                MessageBox.Show("Please select Capacity.");
                cmbProduct.Focus();
                return; 
            }
            if (txtProducCost.Text == "") // || Convert.ToDouble(txtProducCost.Text) == 0)
            {
                //MessageBox.Show("Please Enter Product Cost");
                //cmbProduct.Focus();
                //return; 
                model.ProductCost = "0";
            }
            //if (cmbPaymentType.SelectedIndex == 0)
            //{
            //    MessageBox.Show("Please Enter Payment Type.");
            //    cmbPaymentType.Focus();
            //    return;
 
            //}
            DateTime dt;
            if(DateTime.TryParse(dtpPurchesDate.Text,out dt)==false)
            {
                MessageBox.Show("Please Enter valid Date.");
                cmbPaymentType.Focus();
                return;
            }
            if (cmbEngineer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Engineer");
                cmbEngineer.Focus();
                return;

            }
            if (cmbPaymentType.SelectedIndex == 1)
            {
                model.ReminderDate = Convert.ToDateTime(dtpPurchesDate.Text).AddMonths(1).ToString("dd/MM/yyyy");
            }
            else if (cmbPaymentType.SelectedIndex == 2)
            {
                model.ReminderDate = Convert.ToDateTime(dtpPurchesDate.Text).AddMonths(3).ToString("dd/MM/yyyy");
            }
            else if (cmbPaymentType.SelectedIndex == 3)
            {
                model.ReminderDate = Convert.ToDateTime(dtpPurchesDate.Text).AddMonths(6).ToString("dd/MM/yyyy");
            }
            else if (cmbPaymentType.SelectedIndex == 4)
            {
                model.ReminderDate = Convert.ToDateTime(dtpPurchesDate.Text).AddYears(1).ToString("dd/MM/yyyy");
            }
            if (cmbProductType.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Product Type");
                txtCustomerName.Focus();
                return;
            }
            if (cmbProductType.SelectedItem.ToString() == "AMC")
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
            }


            model.EngineerID = cmbEngineer.SelectedValue.ToString() ;
            model.CustProductId = txtCustProductID.Text;
            model.CustId = txtCustID.Text.Trim();
            model.ProductID = cmbProduct.SelectedValue.ToString();
            model.BrandID = cmbBrand.SelectedValue.ToString();
            model.CapacityID = cmbCapacity.SelectedValue.ToString();
            model.ProductCost = txtProducCost.Text.ToString();
            model.PurchesDate = dtpPurchesDate.Text;
            if (txtWarrenty.Text.Trim()=="")
            {
               model.warrenty = "0";
            }
            else if (Convert.ToDouble(txtWarrenty.Text) > 0)
            {
                model.warrenty = "1";
            }
            else
            {
                model.warrenty = "0";
 
            }
            int Year, month;
            if (txtWarrenty.Text.IndexOf('.')== 1)
            {
                string[] addval = txtWarrenty.Text.Split('.');
                Year = Convert.ToInt32(addval[0]);
                month = Convert.ToInt32(addval[1]==""?"0":addval[1]);
            }
            else
            {
                Year =Convert.ToInt32(txtWarrenty.Text.Trim());
                month = 0;

            }
            model.PurchesDate = dtpPurchesDate.Text;
            model.ThirdPartyID = cmbThirdParty.SelectedValue.ToString();
DbCommand dbcommand;
if (btnSave.Text == "UPDATE")
{
    dbcommand = database.GetSqlStringCommand("Update tblCustProductDetails set " +
"[ProductID]='" + model.ProductID + "'" +
",[BrandID]='" + model.BrandID + "'" +
",[CapacityId]='" + model.CapacityID + "'" +
",[PurchesDate]='" + model.PurchesDate + "'" +
",[PurchesAmount]='" + model.ProductCost + "'" +
",[ExpiryDate]=CONVERT(varchar,'" + Convert.ToDateTime(model.PurchesDate).AddYears(Year).AddMonths(month).ToString("dd/MM/yyyy") + "',103)" +
",[ProductStatus]=case when " + model.warrenty + "=0 then 'No Warrenty' when " + model.warrenty + "=1 then 'Warrenty' else 'AMC' end" +
",[ReminderDate]='" + model.ReminderDate + "',[EngineerID]=(select EngineerId from tblCustomerDetails where CustomerID='" + txtCustID.Text.Trim() + "'),[ThirdPartyID]=(select DelaredId from tblCustomerDetails where CustomerID='" + txtCustID.Text.Trim() + "')" +
"WHERE CustProductID='" + ProductId + "'");

}
else
{
    dbcommand = database.GetSqlStringCommand("insert into tblCustProductDetails (" +
"[CustomerID]" +
",[ProductID]" +
",[BrandID]" +
",[CapacityId]" +
",[PurchesDate]" +
",[PurchesAmount]" +
",[ExpiryDate]" +
",[ProductStatus]" +
",[ReminderDate], "+
"[EngineerID],[ThirdPartyID]) values ('" + txtCustID.Text.Trim() + "','" + model.ProductID + "','" + model.BrandID + "','" + model.CapacityID + "','" + model.PurchesDate + "','" + model.ProductCost + "'," +
"CONVERT(varchar,'" + Convert.ToDateTime(model.PurchesDate).AddYears(Year).AddMonths(month).ToString("dd/MM/yyyy") + "',103),case when " + model.warrenty + "=0 then 'No Warrenty' when " + model.warrenty + "=1 then 'Warrenty' else 'AMC' end,'" + model.ReminderDate + "',(select EngineerId from tblCustomerDetails where CustomerID='" + txtCustID.Text.Trim() + "'),(select DelaredId from tblCustomerDetails where CustomerID='" + txtCustID.Text.Trim() + "') " +
") select @@IDENTITY");
}
            
            //model.PaidAmount = txtPaidAmount.Text.ToString();
            //model.BalanceAmount = txtBalanceAmount.Text.Trim();
            int val=Convert.ToInt32(database.ExecuteScalar(dbcommand));
            if (val >0)
            {
                if (cmbProductType.SelectedItem == "AMC") 
                {
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
                    dbcommand = database.GetSqlStringCommand("Insert into tblAMCDetails values ('"+val+"',convert(datetime,'" + dtpFromDate.Text + "',103),convert(datetime,'" + txtToDate.Text + "',103),'" + txtCharges.Text + "','" + txtPaidAmount.Text + "') select @@IDENTITY");
                    int result = Convert.ToInt32(database.ExecuteScalar(dbcommand));
                    if (result > 0)
                    {
                        string ReminderDate = Convert.ToDateTime(txtToDate.Text).AddMonths(-1).ToString("dd/MM/yyyy");
                        string ExiperdDate = Convert.ToDateTime(txtToDate.Text).ToString("dd/MM/yyyy");
                        dbcommand = database.GetSqlStringCommand("Update tblCustProductDetails set ReminderDate='" + ReminderDate + "',ExpiryDate='" + ExiperdDate + "',ProductStatus='AMC',AMCID='" + result + "' where CustProductID='"+val+"'");
                        int Update = database.ExecuteNonQuery(dbcommand);
                        if (Update > 0)
                        {
                            dbcommand = database.GetSqlStringCommand("insert into tblPayment values ('" + result + "',convert(datetime,'" + DateTime.Now.ToString() + "',103),'" + txtCharges.Text + "','" + txtPaidAmount.Text + "','0','A') select @@IDENTITY");
                            int Payment = Convert.ToInt32(database.ExecuteScalar(dbcommand));

                            dbcommand = database.GetSqlStringCommand("update tblPayment set BalanceAmout=(" + txtCharges.Text + "-(select sum(cast(PaidAmount as DECIMAL(9,2))) from tblPayment where PaymentType='A' and AmcProductId='" + result + "')) where PaymentId='" + Payment + "'");
                            int value = database.ExecuteNonQuery(dbcommand);
                            if (value > 0)
                            {
                                //MessageBox.Show("Record Inserted Successfully.");
                                //var principalForm = Application.OpenForms.OfType<frmListOfProduct>().Single();
                                //principalForm.frmListOfProduct_Load(null, null);
                                //this.Close();
                                //return;
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

                MessageBox.Show("Product Add Sucessfully.");
                //dbcommand = database.GetSqlStringCommand("Update tblCustomerDetails set EngineerId='" + cmbEngineer.SelectedValue.ToString() + "' where CustomerID='" + txtCustID.Text.Trim() + "' ");
                //database.ExecuteNonQuery(dbcommand);
                dgvProductDetails.DataSource = controller.GetProductDeatilsofCustomer(model);
                dgvProductDetails.Columns["CustomerID"].Visible = false;
                dgvCustomerName.DataSource = null;
                dgvCustomerName.Visible = false;
                //txtCustomerName.Text = "";
                //txtPaidAmount.Text = "";
                txtProducCost.Text = "";
                //txtBalanceAmount.Text = "";
                cmbBrand.SelectedIndex = 0;
                cmbProduct.SelectedIndex = 0;
                cmbCapacity.SelectedIndex = 0;
                cmbPaymentType.SelectedIndex = 0;             
                //txtPurchesDate.Text = "";
                cmbEngineer.SelectedIndex = 0;
                txtWarrenty.Text = "";
                grpAMC.Visible = false;
                txtAmcYear.Text = "";
                txtCharges.Text = "";
                txtPaidAmount.Text = "";

            }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCustomerName_Leave(object sender, EventArgs e)
        {
            try
            {
                model.CustId=txtCustID.Text.ToString();
                dgvProductDetails.DataSource = controller.GetProductDeatilsofCustomer(model);
                //dgvProductDetails.Columns[0].Visible=false;
                //dgvProductDetails.Columns[1].Visible = false;
                dgvProductDetails.Columns["CustomerID"].Visible = false;
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvCustomerName.DataSource = null;
            dgvCustomerName.Visible = false;
            txtCustID.Text = "";
            txtCustomerName.Text = "";
            //txtPaidAmount.Text = "";
            txtProducCost.Text = "";
            //txtBalanceAmount.Text = "";
            cmbBrand.SelectedIndex = 0;
            cmbProduct.SelectedIndex = 0;
            cmbCapacity.SelectedIndex = 0;
            cmbPaymentType.SelectedIndex = 0;
            dgvProductDetails.DataSource = null;
            //txtPurchesDate.Text = "";
        }

        private void txtPurchesDate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            frmAddCustomer obj = new frmAddCustomer(0, "Product");
            obj.ShowDialog();
        }

        private void frmAddProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (dgvCustomerName.Visible == true)
                {
                    dgvCustomerName.Focus();
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvCustomerName.CurrentRow != null)
                {
                    if (dgvCustomerName.CurrentRow.Index >= 0)
                    {
                        txtCustomerName.Text = dgvCustomerName.Rows[dgvCustomerName.CurrentRow.Index].Cells[1].Value.ToString().ToUpper();
                        txtCustID.Text = dgvCustomerName.Rows[dgvCustomerName.CurrentRow.Index].Cells[0].Value.ToString();
                        txtEngID.Text = dgvCustomerName.Rows[dgvCustomerName.CurrentRow.Index].Cells["EngineerId"].Value.ToString();
                        model.CustId = txtCustID.Text.ToString();
                        dgvProductDetails.DataSource = controller.GetProductDeatilsofCustomer(model);
                        dgvProductDetails.Columns[1].Visible = false;
                        dgvCustomerName.DataSource = null;
                        dgvCustomerName.Visible = false;
                    }
                }
            }
        }

        private void dgvCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvCustomerName.CurrentRow.Index >= 0)
                {
                    txtCustomerName.Text = dgvCustomerName.Rows[dgvCustomerName.CurrentRow.Index].Cells[1].Value.ToString().ToUpper();
                    txtCustID.Text = dgvCustomerName.Rows[dgvCustomerName.CurrentRow.Index].Cells[0].Value.ToString();
                    txtEngID.Text = dgvCustomerName.Rows[dgvCustomerName.CurrentRow.Index].Cells["EngineerId"].Value.ToString();
                    model.CustId = txtCustID.Text.ToString();
                    dgvProductDetails.DataSource = controller.GetProductDeatilsofCustomer(model);
                    //dgvProductDetails.Columns[0].Visible = false;
                    dgvProductDetails.Columns[1].Visible = false;
                    //dgvProductDetails.Columns["CustomerID"].Visible = false;
                    dgvCustomerName.DataSource = null;
                    dgvCustomerName.Visible = false;
                }
            }
        }

        private void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProductType.SelectedItem == "No Warrenty")
            {
                txtWarrenty.Text = "0";
            }
            else
            {
                txtWarrenty.Text = "";
            }
            if (cmbProductType.SelectedItem.ToString() == "AMC")
            {
                txtWarrenty.Text = "0";
                txtWarrenty.Enabled = false;
                grpAMC.Visible = true;

            }
            else
            {
                txtWarrenty.Text = "0";
                txtWarrenty.Enabled = true;
                grpAMC.Visible = false;
            }
        }

        private void dgvCustomerName_MouseClick(object sender, MouseEventArgs e)
        {
            int currentMouseOverRow = dgvProductDetails.HitTest(e.X, e.Y).RowIndex;

            if (currentMouseOverRow >= 0)
            {
                dgvProductDetails.Rows[currentMouseOverRow].Cells[4].Selected = true;
                dgvProductDetails.Refresh();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cmbProductType.SelectedIndex = 0;
                cmbPaymentType.SelectedIndex = 0;
                dsBindCmb = controller.GetDataToBind();
                if (dsBindCmb != null)
                {
                    cmbBrand.DisplayMember = "BrandName";
                    cmbBrand.ValueMember = "BrandID";
                    cmbBrand.DataSource = dsBindCmb.Tables[0];

                    cmbProduct.DisplayMember = "ProductName";
                    cmbProduct.ValueMember = "ProductID";
                    cmbProduct.DataSource = dsBindCmb.Tables[1];

                    cmbCapacity.DisplayMember = "Capacity";
                    cmbCapacity.ValueMember = "CapacityID";
                    cmbCapacity.DataSource = dsBindCmb.Tables[2];

                    cmbEngineer.DisplayMember = "EngineerName";
                    cmbEngineer.ValueMember = "EngineerID";
                    cmbEngineer.DataSource = dsBindCmb.Tables[3];

                    cmbThirdParty.DisplayMember = "ThirdPartyName";
                    cmbThirdParty.ValueMember = "ThirdPartyID";
                    cmbThirdParty.DataSource = dsBindCmb.Tables[4];
                }
        }

    }
}
