using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketingTool.Data.Connection;
using TicketingTool.Data.Helper;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Dashboard;
using TicketingTool.Utilities;

namespace TicketingTool.Services.Concrete.Dashboard
{
    public class DashboardRepository : IDashboardRepository
    {
        public async Task<List<T>> GetDashboardDataCount<T>(int spType)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) };
            var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Dashbaord, parameters);
            return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
        }

        public async Task<List<T>> GetDashboardDataCount_NR<T>(int spType, string employee_id, string access_role, string project_name, string domain_name)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) , new SqlParameter("@employee_id", employee_id), new SqlParameter("@access_role", access_role) , new SqlParameter("@project_name", project_name), new SqlParameter("@domain_name", domain_name) };
            var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Dashbaord_info, parameters);
            return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
        }
        public async Task<List<T>> GetDashboardDataCount_CR<T>(int spType, string employee_id, string access_role, string project_name, string domain_name)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@employee_id", employee_id), new SqlParameter("@access_role", access_role), new SqlParameter("@project_name", project_name), new SqlParameter("@domain_name", domain_name) };
            var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Dashbaord_info, parameters);
            return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
        }
        public async Task<string> GetSum(int spType, string employee_id, string access_role,string project_name, string domain_name)
        {
            SqlParameter[] param =
                       {
                new SqlParameter("@spType", spType),
                new SqlParameter("@employee_id", employee_id),
                new SqlParameter("@access_role", access_role),
                new SqlParameter("@project_name", project_name),
                new SqlParameter("@domain_name", domain_name),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Dashbaord_info, param);
            return db_result.Tables[0].Rows[0]["total_sum_indivisual"].ToString();

        }
        public async Task<List<T>> GetDashboardDataCount_IR<T>(int spType, string employee_id, string access_role,string project_name, string domain_name)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@employee_id", employee_id), new SqlParameter("@access_role", access_role), new SqlParameter("@project_name", project_name), new SqlParameter("@domain_name", domain_name) };
            var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Dashbaord_info, parameters);
            return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
        }

        public async Task<List<master_project>> GetProjectByUserId<T>(int spType, vm_user_registration request)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@employee_id", request.employee_id), new SqlParameter("@access_role", request.user_role) };
            var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Dashbaord_SearchInfo, parameters);
            return CommonUtility.ConvertDataTableToList<master_project>(dbResult.Tables[0]);
        }
    }
}
