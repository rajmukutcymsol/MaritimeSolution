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
    public class StateRepository: IStateRepository
    {
        public async Task<List<master_State>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageState_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageState, parameters);
            return CommonUtility.ConvertDataTableToList<master_State>(db_result.Tables[0]);
        }
        public async Task<master_State> GetById( Guid id/*int? CountryID, int stateid*/)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageState_Type.getById), new SqlParameter("@id", id)/*,new SqlParameter("@stateid", stateid)*/ };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageState, parameters);
            return CommonUtility.GetObjectByRow<master_State>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Save(master_State request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageState_Type.save),
                new SqlParameter("@country_id",request.CountryID),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@state_name",request.StateName),

            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageState, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Update(master_State request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@id",request.id),
                new SqlParameter("@spType",usp_ManageState_Type.update),
                new SqlParameter("@country_id",request.CountryID),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@state_name",request.StateName),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageState, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Delete(Guid id /*int? countryID, int stateid*/)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageState_Type.delete), new SqlParameter("@id", id)/*, new SqlParameter("@stateid", stateid)*/ };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageState, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
