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
    public class FunctionRepository : IFunctionRepository
    {
        public async Task<List<master_function>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageFunction_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageFunction, parameters);
            return CommonUtility.ConvertDataTableToList<master_function>(db_result.Tables[0]);
        }

        public async Task<master_function> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageFunction_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageFunction, parameters);
            return CommonUtility.GetObjectByRow<master_function>(db_result.Tables[0].Rows[0]);
        }


        public async Task<CommonDbResponse> Save(master_function request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageFunction_Type.save),
                new SqlParameter("@function_name",request.function_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageFunction, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_function request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageFunction_Type.update),
                new SqlParameter("@function_name",request.function_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageFunction, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageFunction_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageFunction, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
