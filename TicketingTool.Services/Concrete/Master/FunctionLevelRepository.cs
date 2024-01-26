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
    public class FunctionLevelRepository : IFunctionLevelRepository
    {
        public async Task<List<master_function_level>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageFunctionLevel_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageFunctionLevel, parameters);
            return CommonUtility.ConvertDataTableToList<master_function_level>(db_result.Tables[0]);
        }

        public async Task<master_function_level> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageFunctionLevel_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageFunctionLevel, parameters);
            return CommonUtility.GetObjectByRow<master_function_level>(db_result.Tables[0].Rows[0]);
        }


        public async Task<CommonDbResponse> Save(master_function_level request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageFunctionLevel_Type.save),
                new SqlParameter("@function_level",request.function_level),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageFunctionLevel, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_function_level request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageFunctionLevel_Type.update),
                new SqlParameter("@function_level",request.function_level),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageFunctionLevel, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageFunctionLevel_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageFunctionLevel, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
