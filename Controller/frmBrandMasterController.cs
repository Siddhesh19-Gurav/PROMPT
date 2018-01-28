using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PROMPT.Model;
using System.Data.Common;

namespace PROMPT.Controller
{
    class frmBrandMasterController
    {
        Database database = new Database("PROMPT");
        DataTable dt;
        int result;

        public int InsertBrandMasterDetails(frmBrandMasterModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_InsertBrandMasterDetails");
                database.AddInParameter(dbcommand, "@BrandName", DbType.String, model.BrandName);
                database.AddInParameter(dbcommand, "@BrandID", DbType.Int32, model.BrandID);
                result = Convert.ToInt32(database.ExecuteScalar(dbcommand));
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetBrandMasterDetails()
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetBrandMasterDetails");
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
