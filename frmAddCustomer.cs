using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PROMPT.Controller;
using PROMPT.Model;

namespace PROMPT
{
    public partial class frmAddCustomer : Form
    {
        frmAddCustomerController controller = new frmAddCustomerController();
        frmAddCustomerModel model = new frmAddCustomerModel();
        int id;
        public string location;
        DataTable dtCustDetais;
        public frmAddCustomer(int Custid,string Destination)
        {
            InitializeComponent();
            id = Custid;
            location = Destination;
        }

        private void frmAddCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                cmbCustomerType.DisplayMember = "CustomerType";
                cmbCustomerType.ValueMember = "CustomerTypeID";
                cmbCustomerType.DataSource = controller.GetCustomerType();

                cmbLocation.DisplayMember = "LocationName";
                cmbLocation.ValueMember = "LocationID";
                cmbLocation.DataSource = controller.GetLocationDetails();

                cmbEngineerID.ValueMember = "EngineerID";
                cmbEngineerID.DisplayMember = "EngineerName";
                DataSet   dsBindCmb = controller.GetDataToBind();
                cmbEngineerID.DataSource = dsBindCmb.Tables[3];
                cmbThirdParty.DisplayMember = "ThirdPartyName";
                cmbThirdParty.ValueMember = "ThirdPartyID";
                cmbThirdParty.DataSource = dsBindCmb.Tables[4];

               
                txtCustomerID.Text = id.ToString();
                if (id!=0)
                {
                    model.CustId=id;
                    txtCustomerID.Text = id.ToString();
                    dtCustDetais=controller.GetCustomerEditDetails(model);
                    if (dtCustDetais.Rows.Count > 0)
                    {
                        txtCustomerName.Text = dtCustDetais.Rows[0][0].ToString();
                        txtCustomerAddress.Text = dtCustDetais.Rows[0][1].ToString();
                        int i=0;
                        foreach (DataRowView items in cmbCustomerType.Items)
                        {
                            if (items[1].ToString().ToUpper() == dtCustDetais.Rows[0][3].ToString().ToUpper())
                            {
                                cmbCustomerType.SelectedIndex = i;
                                break;
                            }
                            i++;
                        }
                        i = 0;
                        foreach (DataRowView items in cmbLocation.Items)
                        {
                            if (items[1].ToString().ToUpper() == dtCustDetais.Rows[0][2].ToString().ToUpper())
                            {
                                cmbLocation.SelectedIndex = i;
                                break;
                            }
                            i++;
                        }
                        i = 0;
                        foreach (DataRowView items in cmbEngineerID.Items)
                        {
                            if (items[1].ToString().ToUpper() == dtCustDetais.Rows[0]["EngineerName"].ToString().ToUpper())
                            {
                                cmbEngineerID.SelectedIndex = i;
                                break;
                            }
                            i++;
                        }
                        foreach (DataRowView items in cmbThirdParty.Items)
                        {
                            if (items[1].ToString().ToUpper() == dtCustDetais.Rows[0]["ThirdPartyName"].ToString().ToUpper())
                            {
                                cmbThirdParty.SelectedIndex = i;
                                break;
                            }
                            i++;
                        }
                        txtCustomerContactNo.Text = dtCustDetais.Rows[0][4].ToString();
                        txtContactPerson.Text = dtCustDetais.Rows[0][5].ToString();

                    }
 
                }
                

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
                if (txtCustomerName.Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter Customer Name.");
                    txtCustomerName.Focus();
                    return;
 
                }
                if (txtCustomerAddress.Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter Customer Address.");
                    txtCustomerAddress.Focus();
                    return;
 
                }
                if (cmbCustomerType.SelectedIndex == 0)
                {
                    MessageBox.Show("Please Select Customer Type.");
                    cmbCustomerType.Focus();
                    return;
                }
                if (cmbLocation.SelectedIndex == 0)
                {
                    MessageBox.Show("Please Select Location.");
                    cmbLocation.Focus();
                    return;
                }
                if (cmbEngineerID.SelectedIndex == 0)
                {
                    MessageBox.Show("Please Select Engineer.");
                    cmbEngineerID.Focus();
                    return;
                }
                if (cmbThirdParty.SelectedIndex == 0)
                {
                    MessageBox.Show("Please Select Thired Party.");
                    cmbThirdParty.Focus();
                    return;
                }
                
                model.CustomerName = txtCustomerName.Text.Trim().ToUpper();
                model.CustomerAddress = txtCustomerAddress.Text.Trim();
                model.Location = cmbLocation.SelectedValue.ToString();
                model.ContactNo=txtCustomerContactNo.Text.Trim();
                model.CustomerType = cmbCustomerType.SelectedValue.ToString();
                model.CustId = Convert.ToInt32(txtCustomerID.Text.Trim());
                model.ContactPerson = txtContactPerson.Text.Trim().ToUpper();
                model.EngineerID = cmbEngineerID.SelectedValue.ToString();
                model.DelareID = cmbThirdParty.SelectedValue.ToString();
                if (1 == controller.ReqCustomerDetails(model))
                {
                   
                        MessageBox.Show("Registered Sucessfully");
                }
                else
                {
                        MessageBox.Show("Updated Sucessfully");
                }

                if (location == "List")
                {
                    var principalForm = Application.OpenForms.OfType<frmListofCustomer>().Single();
                    principalForm.frmListofCustomer_Load(null, null);
                    this.Close();
                }
                else
                {
                    //frmAddProduct obj = new frmAddProduct();
                    //obj.ShowDialog();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCustomerAddress.Text = txtCustomerContactNo.Text = "";
                //txtCustomerLocation.Text = txtCustomerName.Text = "";
            cmbCustomerType.SelectedIndex = 0;
            btnSave.Text = "SAVE";
            txtCustomerName.Text = "";
            cmbLocation.SelectedIndex = 0;
            txtCustomerContactNo.Text = "";
        }

        private void frmAddCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }

        private void cmbEngineerID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
