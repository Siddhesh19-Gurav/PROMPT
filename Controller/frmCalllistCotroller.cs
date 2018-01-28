using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using PROMPT.Model;

namespace PROMPT.Controller
{
    class frmCalllistCotroller
    {
        Database database = new Database("PROMPT");
        DataTable dt;
        public DataTable GetCutomerDetails(frmCalllistModel model)
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
        public DataTable GetcallList(frmCalllistModel model)
        {
            try
            {

                DbCommand dbcommand = database.GetStoredPocCommand("SP_GetCallDetails");
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
