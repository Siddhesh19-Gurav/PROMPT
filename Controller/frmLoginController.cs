using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPT.Model;
using System.Data.Common;
using System.Data;

namespace PROMPT.Controller
{
    class frmLoginController
    {
        Database database = new Database("PROMPT");
        DataTable dt;
        int result;
        
        public DataTable GetLogin(frmLoginModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetLogin");
                database.AddInParameter(dbcommand, "@Password", DbType.String, model.Password);
                dt = database.ExecuteDataTable(dbcommand);
                return dt;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public int InsertLogDetails(frmLoginModel model)
        {
            try
            {
                DbCommand dbcommand = database.GetStoredPocCommand("SP_InsertLogDetails");
                database.AddInParameter(dbcommand, "@Password", DbType.String, model.Password);
                database.AddInParameter(dbcommand, "@Status", DbType.String, model.Status);
                database.AddInParameter(dbcommand, "@UserName", DbType.String, model.UserName);
                result =Convert.ToInt32(database.ExecuteScalar(dbcommand));
                return result;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
