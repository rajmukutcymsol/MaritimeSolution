using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketingTool.Data.Connection;
using TicketingTool.Data.Helper;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Role;
using TicketingTool.Utilities;

namespace TicketingTool.Services.Concrete.RoleRepository
{
    public class RoleRepository : IRoleRepository
    {
        public async Task<List<master_role>> GetAllRoles(int spType)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRole, parameters);
            return CommonUtility.ConvertDataTableToList<master_role>(db_result.Tables[0]);
        }

        public async Task<master_role> GetRoleById(int spType,Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRole, parameters);
            return CommonUtility.GetObjectByRow<master_role>(db_result.Tables[0].Rows[0]);
        }


        public async Task<CommonDbResponse> Save(int spType,master_role request)
        {
            SqlParameter[] parameters =
            {
               new SqlParameter("@spType", spType),
               new SqlParameter("@role_name", request.role_name),
               new SqlParameter("@is_active", request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRole, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(int spType, master_role request)
        {
            SqlParameter[] parameters =
            {
               new SqlParameter("@spType", spType),
               new SqlParameter("@role_name", request.role_name),
               new SqlParameter("@is_active", request.is_active),
               new SqlParameter("@id", request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRole, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> DeleteRole(int spType, Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRole, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
