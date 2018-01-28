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
    class frmAddCustomerController
    {
        Database database = new Database("PROMPT");
        DataTable dt;
        int result;

        public DataTable GetEngineerList()
        {
            try
            {

                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetEngineerID");
                dt = database.ExecuteDataTable(dbcommand);
                return dt;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable GetCustomerType()
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetCustomerType");
                dt = database.ExecuteDataTable(dbcommand);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public DataTable GetLocationDetails()
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetLocation");
                dt = database.ExecuteDataTable(dbcommand);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public DataSet GetDataToBind()
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetDataToBind");
                DataSet ds = database.ExecuteDataSet(dbcommand);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public DataTable GetCustomerEditDetails(frmAddCustomerModel model)
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
        public int ReqCustomerDetails(frmAddCustomerModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_UpdateCustomerDetails");
                database.AddInParameter(dbcommand, "@CustomerName", DbType.String, model.CustomerName);
                database.AddInParameter(dbcommand, "@CustomerAddress", DbType.String, model.CustomerAddress);
                database.AddInParameter(dbcommand, "@Location", DbType.String, model.Location);
                database.AddInParameter(dbcommand, "@ContactNo", DbType.String, model.ContactNo);
                database.AddInParameter(dbcommand, "@CustId", DbType.String, model.CustId);
                database.AddInParameter(dbcommand, "@CustomerType", DbType.String, model.CustomerType);
                database.AddInParameter(dbcommand, "@ContactPerson", DbType.String, model.ContactPerson);
                database.AddInParameter(dbcommand, "@engineerid", DbType.String, model.EngineerID);
                database.AddInParameter(dbcommand, "@delareId", DbType.String, model.DelareID); 
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
