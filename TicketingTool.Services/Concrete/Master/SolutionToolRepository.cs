using System;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public class SolutionToolRepository: ISolutionToolRepository
    {
        public async Task<List<master_tool>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageTool_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTool, parameters);
            return CommonUtility.ConvertDataTableToList<master_tool>(db_result.Tables[0]);
        }

        public async Task<master_tool> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageTool_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTool, parameters);
            return CommonUtility.GetObjectByRow<master_tool>(db_result.Tables[0].Rows[0]);
        }


        public async Task<CommonDbResponse> Save(master_tool request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageTool_Type.save),
                new SqlParameter("@tool_name",request.tool_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTool, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_tool request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageTool_Type.update),
                new SqlParameter("@tool_name",request.tool_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTool, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageTool_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTool, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
