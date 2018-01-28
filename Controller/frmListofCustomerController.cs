using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PROMPT.Model;
using System.Data.Common;

namespace PROMPT.Controller
{
    class frmListofCustomerController
    {
        Database database = new Database("PROMPT");
        DataTable dt;
        //int result;

        public DataTable GetCutomerDetails(frmListofCustomerMode model)
        {
            try
            {

                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetCutomerDetails");
                database.AddInParameter(dbcommand, "@SearchText", DbType.String, model.SearchText);
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
