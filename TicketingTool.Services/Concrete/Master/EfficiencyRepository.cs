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
    public class EfficiencyRepository : IEfficiencyRepository
    {
        public async Task<List<master_efficiency>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManagePriority_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Manageefficiency, parameters);
            return CommonUtility.ConvertDataTableToList<master_efficiency>(db_result.Tables[0]);
        }

        public async Task<master_efficiency> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManagePriority_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Manageefficiency, parameters);
            return CommonUtility.GetObjectByRow<master_efficiency>(db_result.Tables[0].Rows[0]);
        }


        public async Task<CommonDbResponse> Save(master_efficiency request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManagePriority_Type.save),
                new SqlParameter("@efficiency_name",request.efficiency_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Manageefficiency, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_efficiency request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManagePriority_Type.update),
                new SqlParameter("@efficiency_name",request.efficiency_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Manageefficiency, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManagePriority_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Manageefficiency, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
