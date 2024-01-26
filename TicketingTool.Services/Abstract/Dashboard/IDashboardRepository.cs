using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Masters;
using TicketingTool.Models.ViewModel;

namespace TicketingTool.Services.Abstract.Dashboard
{
    public interface IDashboardRepository
    {
       Task<List<T>> GetDashboardDataCount<T>(int spType);
       Task<List<T>> GetDashboardDataCount_NR<T>(int spType, string employee_id, string access_role, string project_name, string domain_name);
        Task<string> GetSum(int spType, string employee_id, string access_role, string project_name, string domain_name);
       Task<List<T>> GetDashboardDataCount_CR<T>(int spType, string employee_id, string access_role, string project_name, string domain_name);
        Task<List<T>> GetDashboardDataCount_IR<T>(int spType, string employee_id, string access_role, string project_name, string domain_name);
       Task<List<master_project>> GetProjectByUserId<T>(int spType , vm_user_registration request);
    }
}
