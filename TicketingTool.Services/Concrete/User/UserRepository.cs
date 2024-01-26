using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketingTool.Data.Connection;
using TicketingTool.Data.Helper;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Models;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.User;
using TicketingTool.Utilities;

namespace TicketingTool.Services.Concrete.User
{
    public class UserRepository :IUserRepository
    {
        public Task<CommonDbResponse> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<vm_user_registration> GetUserById(int spType, string employeeId)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@employee_id", employeeId) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUser, parameters);
            return CommonUtility.GetObjectByRow<vm_user_registration>(db_result.Tables[0].Rows[0]);
        }

        public async Task<List<vm_user_registration>> GetUsers(int spType)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUser, parameters);
            return CommonUtility.ConvertDataTableToList<vm_user_registration>(db_result.Tables[0]);
        }

        public async Task<CommonDbResponse> UpdateUser(vm_user_registration obj_vm_user_registration, int spType)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@employee_id",obj_vm_user_registration.employee_id),
                    new SqlParameter("@state",obj_vm_user_registration.state),
                    new SqlParameter("@access_role_id",obj_vm_user_registration.access_role_id),
                    new SqlParameter("@employee_name",obj_vm_user_registration.employee_name),
                    new SqlParameter("@employee_role",obj_vm_user_registration.user_role),
                    new SqlParameter("@display_name",obj_vm_user_registration.display_name),
                    new SqlParameter("@email_id",obj_vm_user_registration.email_address),
                    new SqlParameter("@location",obj_vm_user_registration.location),
                    new SqlParameter("@updated_by",obj_vm_user_registration.employee_id),
                    new SqlParameter("@password",obj_vm_user_registration.password),
                };
                var result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUser, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<List<vm_user_registration>> GetDeveloperUsers(int spType)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUser, parameters);
            return CommonUtility.ConvertDataTableToList<vm_user_registration>(db_result.Tables[0]);
        }
        public async Task<List<vm_user_registration>> GetDeveloperUsersforTask(int spType)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUser, parameters);
            return CommonUtility.ConvertDataTableToList<vm_user_registration>(db_result.Tables[0]);
        }

        public async Task<CommonDbResponse> UpdateDevTask(DeveloperTask obj_vm_user_registration, int spType)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@employee_id",obj_vm_user_registration.employee_id),
                    new SqlParameter("@state",obj_vm_user_registration.status),
                    new SqlParameter("@task_details",obj_vm_user_registration.task_details),
                    new SqlParameter("@auto_id",obj_vm_user_registration.auto_id),
                    new SqlParameter("@TaskComments",obj_vm_user_registration.TaskComments)
                };
                var result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDeveloperTaks, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<List<vm_user_registration>> GetTesterList(int spType)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUser, parameters);
            return CommonUtility.ConvertDataTableToList<vm_user_registration>(db_result.Tables[0]);
        }
        public async Task<List<vm_user_registration>> GetUsersByName<T>(int getbyname, string prefix)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", getbyname), new SqlParameter("@employee_name", prefix) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUser, parameters);
            return CommonUtility.ConvertDataTableToList<vm_user_registration>(db_result.Tables[0]);

        }
        public async Task<vm_user_registration> GetUserByName(int spType, string employee_name)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@employee_name", employee_name) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUser, parameters);
            return CommonUtility.GetObjectByRow<vm_user_registration>(db_result.Tables[0].Rows[0]);
        }
    }
}