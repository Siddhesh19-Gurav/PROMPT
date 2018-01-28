using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PROMPT.Model;
using System.Data.Common;

namespace PROMPT.Controller
{
    class frmCapacityMasterController
    {
        Database database = new Database("PROMPT");
        DataTable dt;
        int result;

        public int InsertCapacityMasterDetails(frmCapacityMasterModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_InsertCapacityMasterDetails");
                database.AddInParameter(dbcommand, "@Capacity", DbType.String, model.Capacity);
                database.AddInParameter(dbcommand, "@CapacityId", DbType.Int32, model.CapacityID);
                result = Convert.ToInt32(database.ExecuteScalar(dbcommand));
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetCapacityMasterDetails()
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetCapacityMasterDetails");
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
