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
   public class QttRepository : IQttRepository
    {
        public async Task<List<master_quantity_cargo>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageQtt_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageQTT, parameters);
            return CommonUtility.ConvertDataTableToList<master_quantity_cargo>(db_result.Tables[0]);
        }

        public async Task<master_quantity_cargo> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageQtt_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageQTT, parameters);
            return CommonUtility.GetObjectByRow<master_quantity_cargo>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(master_quantity_cargo request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageQtt_Type.save),
                new SqlParameter("@qtt_name",request.qtt_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageQTT, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Update(master_quantity_cargo request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageQtt_Type.update),
                new SqlParameter("@qtt_name",request.qtt_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageQTT, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageQtt_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageQTT, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
