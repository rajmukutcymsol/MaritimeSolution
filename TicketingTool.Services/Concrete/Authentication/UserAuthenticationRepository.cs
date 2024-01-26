using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketingTool.Data.Connection;
using TicketingTool.Data.Helper;
using TicketingTool.Models.Constant;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Authentication;
using TicketingTool.Utilities;

namespace TicketingTool.Services.Concrete.Authentication
{
    public class UserAuthenticationRepository : IUserAuthenticationRepository
    {
        public async Task<vm_user_detail> GetUserInfo(string id, int spType, string password)
        {
            vm_user_detail result = new vm_user_detail();
            SqlParameter[] parameters =
            {
                new SqlParameter("@spType",spType),
                new SqlParameter("@employee_id",id),
                new SqlParameter("@password",password)
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUser, parameters);
            if(db_result.Tables[0].Rows.Count>0)
                result.userDetail = CommonUtility.GetObjectByRow<vm_user_registration>(db_result.Tables[0].Rows[0]);
            if(db_result.Tables[1].Rows.Count > 0)
                result.menuDetail = CommonUtility.ConvertDataTableToList<vm_menu>(db_result.Tables[1]);
            return result;
        }

        public async Task<T> SaveUser<T>(vm_user_registration obj_vm_user_registration, int spType)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@employee_id",obj_vm_user_registration.employee_id),
                    new SqlParameter("@employee_name",obj_vm_user_registration.employee_name),
                    new SqlParameter("@employee_role",obj_vm_user_registration.user_role),
                    new SqlParameter("@display_name",obj_vm_user_registration.display_name),
                    new SqlParameter("@email_id",obj_vm_user_registration.email_address),
                    new SqlParameter("@username",obj_vm_user_registration.employee_id),
                    new SqlParameter("@location",obj_vm_user_registration.location),
                   // new SqlParameter("@manager_employee_id",obj_vm_user_registration.manager_employee_id),
                    //new SqlParameter("@manager_username",obj_vm_user_registration.manager_username),
                    //new SqlParameter("@manager_name",obj_vm_user_registration.manager_name),
                    //new SqlParameter("@cost_center",obj_vm_user_registration.cost_center),
                    //new SqlParameter("@department",obj_vm_user_registration.department),
                    new SqlParameter("@updated_by",obj_vm_user_registration.employee_id),
                    new SqlParameter("@state",obj_vm_user_registration.state),
                    new SqlParameter("@password",obj_vm_user_registration.password),

                };

                var result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUser, parameters);
                return CommonUtility.GetObjectByRow<T>(result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
