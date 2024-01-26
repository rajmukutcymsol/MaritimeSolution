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
    public class PriorityRepository : IPriorityRepository
    {
        public async Task<List<master_priority>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManagePriority_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManagePriority, parameters);
            return CommonUtility.ConvertDataTableToList<master_priority>(db_result.Tables[0]);
        }

        public async Task<master_priority> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManagePriority_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManagePriority, parameters);
            return CommonUtility.GetObjectByRow<master_priority>(db_result.Tables[0].Rows[0]);
        }


        public async Task<CommonDbResponse> Save(master_priority request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManagePriority_Type.save),
                new SqlParameter("@priority_name",request.priority_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManagePriority, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_priority request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManagePriority_Type.update),
                new SqlParameter("@priority_name",request.priority_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManagePriority, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManagePriority_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManagePriority, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
