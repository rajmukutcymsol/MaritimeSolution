using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;
using TicketingTool.Models.Models;
using TicketingTool.Models.ViewModel;

namespace TicketingTool.Services.Abstract.Mapping
{
    public interface IUserProjectsMapping
    {
        Task<List<master_project>> GetMaster_projects();
        Task<List<vm_user_registration>> GetUsers(int spType);
        Task <T>Save<T>(int insert, vm_User_Projects_Mapping userProjectMapping, string username);
        Task<List<T>> GetUserProjectMappingByEmployeeId<T>(int getAll, vw_user_projects_mapping request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
