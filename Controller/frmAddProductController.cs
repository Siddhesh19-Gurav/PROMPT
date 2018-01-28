using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPT.Model;

namespace PROMPT.Controller
{
    class frmAddProductController
    {
        Database database = new Database("PROMPT");
        DataTable dt;
        DataSet ds;
        int result;
        public DataSet GetDataToBind()
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetDataToBind");
                ds = database.ExecuteDataSet(dbcommand);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public DataTable GetCutomerDetails(frmAddProductModel model)
        {
            try
            {

                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetCutomerDetails");
                database.AddInParameter(dbcommand, "@SearchText", DbType.String, model.CustmoerName);
                dt = database.ExecuteDataTable(dbcommand);
                return dt;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable GetProductDeatilsofCustomer(frmAddProductModel model)
        {
            try
            {

                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetProductDeatilsofCustomer");
                database.AddInParameter(dbcommand, "@CustomerID", DbType.String, model.CustId);
                dt = database.ExecuteDataTable(dbcommand);
                return dt;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable GetCustomerEditDetails(frmAddProductModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetCustomerEditDetails");
                database.AddInParameter(dbcommand, "@CustID", DbType.String, model.CustId);
                dt = database.ExecuteDataTable(dbcommand);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public DataTable GetPaymentDetails(frmAddProductModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("Sp_GetPaymentDetails");
                database.AddInParameter(dbcommand, "@CustProdcutID", DbType.String, model.CustProductId);
                dt = database.ExecuteDataTable(dbcommand);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public int InsertProductDeatils(frmAddProductModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_InsertProductDeatils");
                database.AddInParameter(dbcommand, "@Warrenty", DbType.Int32, model.warrenty);
                database.AddInParameter(dbcommand, "@CustProductID", DbType.Int32, model.CustProductId);
                database.AddInParameter(dbcommand, "@CustId", DbType.Int32, model.CustId);
                database.AddInParameter(dbcommand, "@ProductID", DbType.Int32, model.ProductID);
                database.AddInParameter(dbcommand, "@BrandID", DbType.Int32, model.BrandID);
                database.AddInParameter(dbcommand, "@CapacityID", DbType.Int32, model.CapacityID);
                database.AddInParameter(dbcommand, "@ProductCost", DbType.String, model.ProductCost);
                database.AddInParameter(dbcommand, "@ReminderDate", DbType.String, model.ReminderDate);
                database.AddInParameter(dbcommand, "@PurchesDate", DbType.String, model.PurchesDate);
                database.AddInParameter(dbcommand, "@EngineerID", DbType.Int32, model.EngineerID);
                database.AddInParameter(dbcommand, "@ThirdPartyID", DbType.Int32, model.ThirdPartyID);
                result = database.ExecuteNonQuery(dbcommand);
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}