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
    public class RoleRights:IRoleRightsRepository
    {
        public async Task<List<Auth>> GetAllRoles(int spType)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRoleRights, parameters);
            return CommonUtility.ConvertDataTableToList<Auth>(db_result.Tables[0]);
        }

        public async Task<Auth> GetRoleById(int spType, Guid? id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@RoleID", id) };
                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRoleRights, parameters);
                return CommonUtility.GetObjectByRow<Auth>(db_result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<CommonDbResponse> Save(int spType, Auth request)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@spType", spType),
                new SqlParameter("@RoleID", request.RoleID),
                new SqlParameter("@IsView", request.CanView),
                new SqlParameter("@IsDownload", request.IsDownload),
                new SqlParameter("@IsUpdate", request.CanUpdate),
                new SqlParameter("@IsDelete", request.CanDelete),
                new SqlParameter("@IsPrint", request.CanPrint),
                new SqlParameter("@IsVerify", request.CanVerify),
                new SqlParameter("@is_active", request.is_active),
                //new SqlParameter("@id", request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRoleRights, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(int spType, Auth request)
        {
            try
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@spType", spType),
                new SqlParameter("@RoleID", request.RoleID),
                new SqlParameter("@IsView", request.CanView),
                new SqlParameter("@IsDownload", request.IsDownload),
                new SqlParameter("@IsUpdate", request.CanUpdate),
                new SqlParameter("@IsDelete", request.CanDelete),
                new SqlParameter("@IsPrint", request.CanPrint),
                new SqlParameter("@IsVerify", request.CanVerify),
                new SqlParameter("@is_active", request.is_active),
                new SqlParameter("@updated_by", request.updated_by),
            };
                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRoleRights, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CommonDbResponse> DeleteRole(int spType, Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRoleRights, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

    }
}
