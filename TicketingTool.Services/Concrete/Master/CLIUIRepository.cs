using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Data.Connection;
using TicketingTool.Data.Helper;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;
using TicketingTool.Utilities;

namespace TicketingTool.Services.Concrete.Master
{
    public class CLIUIRepository: ICLIUIRepository
    {
        public async Task<List<master_cli_ui>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCLIUI_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCLIUI, parameters);
            return CommonUtility.ConvertDataTableToList<master_cli_ui>(db_result.Tables[0]);
        }

        public async Task<master_cli_ui> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCLIUI_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCLIUI, parameters);
            return CommonUtility.GetObjectByRow<master_cli_ui>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(master_cli_ui request)
            {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType", usp_ManageCLIUI_Type.save), // Assuming you have an enum usp_ManageCLIUI_Type with a value "Save"
                new SqlParameter("@cli_ui_name", request.cli_ui_name),
                new SqlParameter("@is_active", request.is_active),
                new SqlParameter("@fulladdress", request.fulladdress), // Add this parameter for the fulladdress property
                new SqlParameter("@fax", request.fax),               // Add this parameter for the fax property
                new SqlParameter("@email", request.email),                                                                          
                // Add this parameter for the email property
                new SqlParameter("@phone", request.phone),
                new SqlParameter("@logoupload", request.logoupload), // Add this parameter for the logoupload property
                };

            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCLIUI, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }


        public async Task<CommonDbResponse> Update(master_cli_ui request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageCLIUI_Type.update),
               new SqlParameter("@cli_ui_name",request.cli_ui_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
                new SqlParameter("@fulladdress", request.fulladdress), // Add this parameter for the fulladdress property
                new SqlParameter("@fax", request.fax),               // Add this parameter for the fax property
                new SqlParameter("@email", request.email),    
                new SqlParameter("@phone", request.phone),
                // Add this parameter for the email property
                new SqlParameter("@logoupload", request.logoupload),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCLIUI, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCLIUI_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCLIUI, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
