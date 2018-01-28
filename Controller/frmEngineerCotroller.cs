using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PROMPT.Model;
using System.Data.Common;

namespace PROMPT.Controller
{
    class frmEngineerController
    {
        Database database = new Database("PROMPT");
        DataTable dt;
        int result;

        public DataTable getEngineerDetails(frmEngineerModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetEngineerDetails");
                database.AddInParameter(dbcommand, "@EngineerID", DbType.Int32, model.EngineerId);
                dt = database.ExecuteDataTable(dbcommand);
                return dt;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int InsertEngineerDetails(frmEngineerModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_InsertEngineerDetails");
                database.AddInParameter(dbcommand, "@EngineerID", DbType.String, model.EngineerId);
                database.AddInParameter(dbcommand, "@EngineerName", DbType.String, model.EngineerName);
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
