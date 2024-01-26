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
    public class SDLCRepository : ISDLCRepository
    {
        public async Task<List<master_sdlc>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageSDLC_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageSDLC, parameters);
            return CommonUtility.ConvertDataTableToList<master_sdlc>(db_result.Tables[0]);
        }

        public async Task<master_sdlc> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageSDLC_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageSDLC, parameters);
            return CommonUtility.GetObjectByRow<master_sdlc>(db_result.Tables[0].Rows[0]);
        }


        public async Task<CommonDbResponse> Save(master_sdlc request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageSDLC_Type.save),
                new SqlParameter("@sdlc_status",request.sdlc_status),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageSDLC, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_sdlc request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageSDLC_Type.update),
                new SqlParameter("@sdlc_status",request.sdlc_status),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageSDLC, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageSDLC_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageSDLC, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
