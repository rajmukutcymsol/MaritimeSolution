using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Role
{
    public interface IRoleRightsRepository
    {
        Task<List<Auth>> GetAllRoles(int spType);
        Task<Auth> GetRoleById(int spType, Guid? id);
        Task<CommonDbResponse> Save(int spType, Auth request);
        Task<CommonDbResponse> Update(int spType, Auth request);
        Task<CommonDbResponse> DeleteRole(int spType, Guid? id);
    }
}
