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
using System.Data.Common;
using System.IO;

namespace PROMPT
{
    public partial class frmListofCustomer : Form
    {
        frmListofCustomerMode model = new frmListofCustomerMode();
        frmListofCustomerController controller = new frmListofCustomerController();
        cls_Excel_Import_Export excel = new cls_Excel_Import_Export();
        Database database = new Database("PROMPT");

        public frmListofCustomer()
        {
            InitializeComponent();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        { 
            frmAddCustomer obj = new frmAddCustomer(0,"List");
            obj.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void frmListofCustomer_Load(object sender, EventArgs e)
        {
            try
            {
                model.SearchText = "";
                DbCommand dbcommnad = database.GetSqlStringCommand("select CustomerID ,CustomerName as Name,CustomerAddress,loc.LocationName as Location,custty.CustomerType as [Customer Type],CustomerContactNo as [Contact No],en.EngineerName as [Engineer Name],th.ThirdPartyName as [Delare Name]" +
                                                                    " from tblCustomerDetails cust,tblCustomerType custty,tblLocation loc,tblEngineerMaster en,tblThirdParty th" +
                                                                    " where" +
                                                                    " cust.CustomerLocationID=loc.LocationID " +
                                                                    " and cust.CustomerTypeID=custty.CustomerTypeID and cust.EngineerID=en.EngineerID and th.ThirdPartyID=cust.DelaredId" +
                                                                    " Order by CustomerName");
                dgvListOfCustomer.DataSource = database.ExecuteDataTable(dbcommnad);
                this.dgvListOfCustomer.DefaultCellStyle.Font = new Font("Arial", 11);
                dgvListOfCustomer.Columns[0].Visible = false;
                
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
                if (cmbSearchBy.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select search by option");
                    return;
                }
                string Conditon = "";
                if (cmbSearchBy.SelectedItem.ToString().Trim().ToUpper() == "NAME")
                {
                    Conditon = " AND CustomerName like '"+txtSearch.Text+"%'";
                }
                if (cmbSearchBy.SelectedItem.ToString().Trim().ToUpper() == "ADDRESS")
                {
                    Conditon = " AND CustomerAddress like '%" + txtSearch.Text + "%'";
                }
                if (cmbSearchBy.SelectedItem.ToString().Trim().ToUpper() == "LOCATION")
                {
                    Conditon = " AND loc.LocationName like '" + txtSearch.Text + "%'";
                }
                if (cmbSearchBy.SelectedItem.ToString().Trim().ToUpper() == "CUSTOMER TYPE")
                {
                    Conditon = " AND custty.CustomerType like '" + txtSearch.Text + "%'";
                }
                if (cmbSearchBy.SelectedItem.ToString().Trim().ToUpper() == "CONTACT NO")
                {
                    Conditon = " AND CustomerContactNo like '" + txtSearch.Text + "%'";
                }
                //if (cmbSearchBy.SelectedItem.ToString().Trim().ToUpper() == "THIRD PARTY")
                //{
                //    Conditon = " AND CustomerContactNo like '%" + txtSearch.Text + "%'";
                //}
                if (cmbSearchBy.SelectedItem.ToString().Trim() == "Engineer Name")
                {
                    Conditon = " AND en.EngineerName like '" + txtSearch.Text + "%'";
                }
                if (cmbSearchBy.SelectedItem.ToString() == "Delare Name")
                {
                    Conditon = " AND th.ThirdPartyName like '" + txtSearch.Text + "%'";
                }

                //DbCommand dbcommnad = database.GetSqlStringCommand("select CustomerID ,CustomerName as Name,CustomerAddress,loc.LocationName as Location,custty.CustomerType as [Customer Type],CustomerContactNo as [Contact No]"+
                //                                                    " from tblCustomerDetails cust,tblCustomerType custty,tblLocation loc"+
                //                                                    " where"+
                //                                                    " cust.CustomerLocationID=loc.LocationID "+
                //                                                    " and cust.CustomerTypeID=custty.CustomerTypeID " +
                //                                                    " "+ Conditon +" ");

                DbCommand dbcommnad = database.GetSqlStringCommand("select CustomerID ,CustomerName as Name,CustomerAddress,loc.LocationName as Location,custty.CustomerType as [Customer Type],CustomerContactNo as [Contact No],en.EngineerName as [Engineer Name],th.ThirdPartyName as [Delare Name]" +
                                                                    " from tblCustomerDetails cust,tblCustomerType custty,tblLocation loc,tblEngineerMaster en,tblThirdParty th" +
                                                                    " where" +
                                                                    " cust.CustomerLocationID=loc.LocationID " +
                                                                    " and cust.CustomerTypeID=custty.CustomerTypeID and cust.EngineerID=en.EngineerID and th.ThirdPartyID=cust.DelaredId" +
                                                                    " "+ Conditon +" "+
                                                                    " Order by CustomerName");
                dgvListOfCustomer.DataSource = database.ExecuteDataTable(dbcommnad);
                this.dgvListOfCustomer.DefaultCellStyle.Font = new Font("Arial", 11);
                dgvListOfCustomer.Columns[0].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #region OldbySp
            //try
            //{
            //    model.SearchText = txtSearch.Text.Trim();
            //    dgvListOfCustomer.DataSource = controller.GetCutomerDetails(model);
            //    this.dgvListOfCustomer.DefaultCellStyle.Font = new Font("Arial", 11);
            //    dgvListOfCustomer.Columns[0].Visible = false;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            #endregion
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            frmListofCustomer_Load(null, null);
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if(dgvListOfCustomer.CurrentRow.Index >= 0)
            {
                frmAddCustomer obj = new frmAddCustomer(Convert.ToInt32(dgvListOfCustomer.Rows[dgvListOfCustomer.CurrentRow.Index].Cells["Cust ID"].Value.ToString()),"List");
                obj.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select Row First.");
            }
        }

        private void AddProduct_Click(object sender, EventArgs e)
        {
            if (dgvListOfCustomer.CurrentRow.Index >= 0)
            {
                //frmAddProduct obj = new frmAddProduct(Convert.ToInt32(dgvListOfCustomer.Rows[dgvListOfCustomer.CurrentRow.Index].Cells["Cust ID"].Value.ToString()));
                //obj.ShowDialog();
            }
            else
            {
                MessageBox.Show("Select Row First.");
            }
        }

        private void btnEditCustomer_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void dgvListOfCustomer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dgvListOfCustomer.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0)
                {
                    dgvListOfCustomer.Rows[currentMouseOverRow].Cells[1].Selected = true;
                    dgvListOfCustomer.Refresh();
                    contextMenuStrip1.Show(dgvListOfCustomer, e.X, e.Y);
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvListOfCustomer.CurrentRow.Index > -1)
            {
                DataGridViewRow row = dgvListOfCustomer.Rows[dgvListOfCustomer.CurrentCell.RowIndex];
                frmAddCustomer obj = new frmAddCustomer(Convert.ToInt32(row.Cells["CustomerID"].Value),"");
                obj.ShowDialog();
            }
        }

        private void listOfProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvListOfCustomer.CurrentRow.Index > -1)
            {
                DataGridViewRow row = dgvListOfCustomer.Rows[dgvListOfCustomer.CurrentCell.RowIndex];
                frmListOfProduct obj = new frmListOfProduct(Convert.ToInt32(row.Cells["CustomerID"].Value));
                obj.ShowDialog();
            }
        }

        private void dgvListOfCustomer_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvListOfCustomer.CurrentRow.Index > -1)
            {
                DataGridViewRow row = dgvListOfCustomer.Rows[dgvListOfCustomer.CurrentCell.RowIndex];
                frmListOfProduct obj = new frmListOfProduct(Convert.ToInt32(row.Cells["CustomerID"].Value));
                obj.ShowDialog();
            }

        }

        private void frmListofCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principalForm = Application.OpenForms.OfType<frmMDI>().Single();
            principalForm.Closeform();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //if (cmbSearchBy.SelectedItem.ToString().Trim() == "")
                //{
                //    MessageBox.Show("Please select search by option");
                //    return;
                //}
                string Conditon = "";
                if (cmbSearchBy.SelectedIndex > 0)
                {
                    if (cmbSearchBy.SelectedItem.ToString().Trim().ToUpper() == "NAME")
                    {
                        Conditon = " AND CustomerName like '" + txtSearch.Text + "%'";
                    }
                    if (cmbSearchBy.SelectedItem.ToString().Trim().ToUpper() == "ADDRESS")
                    {
                        Conditon = " AND CustomerAddress like '%" + txtSearch.Text + "%'";
                    }
                    if (cmbSearchBy.SelectedItem.ToString().Trim().ToUpper() == "LOCATION")
                    {
                        Conditon = " AND loc.LocationName like '" + txtSearch.Text + "%'";
                    }
                    if (cmbSearchBy.SelectedItem.ToString().Trim().ToUpper() == "CUSTOMER TYPE")
                    {
                        Conditon = " AND custty.CustomerType like '" + txtSearch.Text + "%'";
                    }
                    if (cmbSearchBy.SelectedItem.ToString().Trim().ToUpper() == "CONTACT NO")
                    {
                        Conditon = " AND CustomerContactNo like '" + txtSearch.Text + "%'";
                    }
                    //if (cmbSearchBy.SelectedItem.ToString().Trim().ToUpper() == "THIRD PARTY")
                    //{
                    //    Conditon = " AND CustomerContactNo like '%" + txtSearch.Text + "%'";
                    //}
                    if (cmbSearchBy.SelectedItem.ToString().Trim() == "Engineer Name")
                    {
                        Conditon = " AND en.EngineerName like '" + txtSearch.Text + "%'";
                    }
                    if (cmbSearchBy.SelectedItem.ToString() == "Delare Name")
                    {
                        Conditon = " AND th.ThirdPartyName like '" + txtSearch.Text + "%'";
                    }
                }
                

                //DbCommand dbcommnad = database.GetSqlStringCommand("select CustomerID ,CustomerName as Name,CustomerAddress,loc.LocationName as Location,custty.CustomerType as [Customer Type],CustomerContactNo as [Contact No]"+
                //                                                    " from tblCustomerDetails cust,tblCustomerType custty,tblLocation loc"+
                //                                                    " where"+
                //                                                    " cust.CustomerLocationID=loc.LocationID "+
                //                                                    " and cust.CustomerTypeID=custty.CustomerTypeID " +
                //                                                    " "+ Conditon +" ");

                String Query = "select CustomerName as Name,CustomerAddress,loc.LocationName as Location,custty.CustomerType as [Customer Type],CustomerContactNo as [Contact No],en.EngineerName as [Engineer Name],th.ThirdPartyName as [Delare Name]" +
                                                                    " from tblCustomerDetails cust,tblCustomerType custty,tblLocation loc,tblEngineerMaster en,tblThirdParty th" +
                                                                    " where" +
                                                                    " cust.CustomerLocationID=loc.LocationID " +
                                                                    " and cust.CustomerTypeID=custty.CustomerTypeID and cust.EngineerID=en.EngineerID and th.ThirdPartyID=cust.DelaredId" +
                                                                    " "+ Conditon +" "+
                                                                    " Order by CustomerName";

                //string Query = "select CustomerName as Name,CustomerAddress,loc.LocationName as Location,custty.CustomerType as [Customer Type],CustomerContactNo as [Contact No]" +
                //                                                    " from tblCustomerDetails cust,tblCustomerType custty,tblLocation loc" +
                //                                                    " where" +
                //                                                    " cust.CustomerLocationID=loc.LocationID " +
                //                                                    " and cust.CustomerTypeID=custty.CustomerTypeID " +
                //                                                    " " + Conditon + " Order by CustomerName asc";
                if (!Directory.Exists(@"D:\PROMPT\Reports\"+DateTime.Now.ToString("ddMMyyyy")))
                {
                    Directory.CreateDirectory(@"D:\PROMPT\Reports\"+DateTime.Now.ToString("ddMMyyyy"));
                }
                excel.Fn_Excel_Export(@"D:\PROMPT\Reports\" + DateTime.Now.ToString("ddMMyyyy") + @"\CustomerList" + DateTime.Now.ToString("mmss") + ".xlsx", "Provider=sqloledb;Data Source=SID-PC;Initial Catalog=dbPROMPT;user id=sa;password=sa2008", Query, true, "Customer Details");

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

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                 DbCommand dbcommmand;
                if (dgvListOfCustomer.CurrentRow.Index > -1)
                {
                   dbcommmand= database.GetSqlStringCommand("DELETE FROM tblCallLogDetails WHERE CustomerId='"+dgvListOfCustomer.Rows[dgvListOfCustomer.CurrentRow.Index].Cells["CustomerID"].Value.ToString()+"'");
                   database.ExecuteNonQuery(dbcommmand);
                   dbcommmand = database.GetSqlStringCommand("DELETE FROM tblCustomerDetails WHERE CustomerId='" + dgvListOfCustomer.Rows[dgvListOfCustomer.CurrentRow.Index].Cells["CustomerID"].Value.ToString() + "'");
                   database.ExecuteNonQuery(dbcommmand);
                   dbcommmand = database.GetSqlStringCommand("DELETE FROM tblCustProductDetails WHERE CustomerId='" + dgvListOfCustomer.Rows[dgvListOfCustomer.CurrentRow.Index].Cells["CustomerID"].Value.ToString() + "'");
                   database.ExecuteNonQuery(dbcommmand);
                   MessageBox.Show("Deleted Sucessfully.");
                   frmListofCustomer_Load(null, null);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //database.GetSqlStringCommand("DELETE FROM tblAMCDetails WHERE CustomerId='"++"'");
            
                
                
                    
        }

        private void dgvListOfCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
    }
}
