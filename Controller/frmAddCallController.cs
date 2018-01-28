using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace PROMPT.Controller
{
    class frmAddCallController
    {
        Database database = new Database("PROMPT");
        DataTable dt;


        public DataTable GetCustomerEditDetails(frmAddCallModel model)
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
        public DataTable GetCutomerDetails(frmAddCallModel model)
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
        public string GetEngineerIdByProductId(frmAddCallModel model)
        {
            try
            {

                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetEngineerIdByProductId");
                database.AddInParameter(dbcommand, "@ProductId", DbType.String, model.ProductID);
                string val =  database.ExecuteScalar(dbcommand).ToString();
                return val;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable GetProductDeatilsofCustomer(frmAddCallModel model)
        {
            try
            {

                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetProductofCustomer");
                database.AddInParameter(dbcommand, "@CustomerID", DbType.String, model.CustId);
                dt = database.ExecuteDataTable(dbcommand);
                return dt;

            }
            catch (Exception)
            {

                throw;
            }
        }
        DataSet ds;
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
        int result;
        public int InserCalltData(frmAddCallModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_InsertCallDetails");
                database.AddInParameter(dbcommand, "@CallDate", DbType.String, model.CallDate);
                database.AddInParameter(dbcommand, "@CustId", DbType.String, model.CustId);
                database.AddInParameter(dbcommand, "@ProductId", DbType.String, model.ProductID);
                database.AddInParameter(dbcommand, "@CallDescription", DbType.String, model.CallDescription);
                database.AddInParameter(dbcommand, "@CallStatus", DbType.String, model.CallStatus);
                database.AddInParameter(dbcommand, "@Charges", DbType.String, model.Charges);
                database.AddInParameter(dbcommand, "@EngineerID", DbType.String, model.EngineerID);
                database.AddInParameter(dbcommand, "@EngineerDescription", DbType.String, model.EngineerDescription);
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
