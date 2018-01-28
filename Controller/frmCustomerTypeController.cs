using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PROMPT.Model;
using System.Data.Common;

namespace PROMPT.Controller
{
    class frmCustomerTypeController
    {
        Database database = new Database("PROMPT");
        DataTable dt;
        int result;

        public int InsertCustomerTypeDetails(frmCustomerTypeModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_InsertCustomerTypeDetails");
                database.AddInParameter(dbcommand, "@CustomerType", DbType.String, model.CustomerType);
                database.AddInParameter(dbcommand, "@CustomerTypeID", DbType.Int32, model.CustomerTypeID);
                result = Convert.ToInt32(database.ExecuteScalar(dbcommand));
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeleteLCustomerDetails(frmCustomerTypeModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_DeleteCustomerType");
                database.AddInParameter(dbcommand, "@CustomerTypeId", DbType.Int32, model.CustomerTypeID);
                result = Convert.ToInt32(database.ExecuteScalar(dbcommand));
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetCustomerTypeDetails()
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetCustomerTypeDetails");
                dt = database.ExecuteDataTable(dbcommand);
                return dt;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
