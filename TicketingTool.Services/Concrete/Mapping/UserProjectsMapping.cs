using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketingTool.Data.Connection;
using TicketingTool.Data.Helper;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Models.Models;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Mapping;
using TicketingTool.Utilities;

namespace TicketingTool.Services.Concrete.Mapping
{
    public class UserProjectsMapping : IUserProjectsMapping
    {
        public async Task<List<master_project>> GetMaster_projects()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageProject_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProject, parameters);
            return CommonUtility.ConvertDataTableToList<master_project>(db_result.Tables[0]);
        }
        public async Task<List<vm_user_registration>> GetUsers(int spType)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUser, parameters);
            return CommonUtility.ConvertDataTableToList<vm_user_registration>(db_result.Tables[0]);
        }

        public async Task<T> Save<T>(int spType, vm_User_Projects_Mapping userProjectMapping, string username)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@id",userProjectMapping.id),
                    new SqlParameter("@employee_id",userProjectMapping.employee_id),
                    new SqlParameter("@created_By",username),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUserProjectsMapping, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<T>> GetUserProjectMappingByEmployeeId<T>(int spType, vw_user_projects_mapping user_Registration)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@employee_id",user_Registration.employee_id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUserProjectsMapping, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",(int)ManageUserProjectMapping_Type.delete),
                    new SqlParameter("@id",id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUserProjectsMapping, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
