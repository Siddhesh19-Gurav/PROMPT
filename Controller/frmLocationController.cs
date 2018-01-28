using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PROMPT.Model;
using System.Data.Common;

namespace PROMPT.Controller
{
    class frmLocationController
    {
        Database database = new Database("PROMPT");
        DataTable dt;
        int result;

        public int InsertLocationDetails(frmLocationModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_InsertLocationDetails");
                database.AddInParameter(dbcommand, "@Location", DbType.String, model.LocationName);
                database.AddInParameter(dbcommand, "@LocationId", DbType.Int32, model.LocationId);
                result = Convert.ToInt32(database.ExecuteScalar(dbcommand));
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int DeleteLocationDetails(frmLocationModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_DeleteLocation");
                database.AddInParameter(dbcommand, "@LocationId", DbType.Int32, model.LocationId);
                result = Convert.ToInt32(database.ExecuteScalar(dbcommand));
                return result;
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
                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetLocationDetails");
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
