using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Role
{
    public interface IRoleRepository
    {
        Task<List<master_role>> GetAllRoles(int spType);
        Task<master_role> GetRoleById(int spType, Guid? id);
        Task<CommonDbResponse> Save(int spType, master_role request);
        Task<CommonDbResponse> Update(int spType, master_role request);
        Task<CommonDbResponse> DeleteRole(int spType, Guid? id);
    }
}
