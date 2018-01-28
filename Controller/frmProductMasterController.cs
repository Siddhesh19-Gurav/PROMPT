using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PROMPT.Model;
using System.Data.Common;

namespace PROMPT.Controller
{
    class frmProductMasterController
    {
        Database database = new Database("PROMPT");
        DataTable dt;
        int result;

        public int InsertProductMasterDetails(frmProductMastertModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_InsertProductMasterDetails");
                database.AddInParameter(dbcommand, "@Product", DbType.String, model.ProductName);
                database.AddInParameter(dbcommand, "@ProductId", DbType.Int32, model.ProductId);
                result = Convert.ToInt32(database.ExecuteScalar(dbcommand));
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable GetProductMasterDetails()
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetProductMasterDetails");
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
