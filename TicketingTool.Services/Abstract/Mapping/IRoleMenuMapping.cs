using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;
using TicketingTool.Models.ViewModel;

namespace TicketingTool.Services.Abstract.Mapping
{
    public interface IRoleMenuMapping
    {
        Task<List<master_menu>> GetMaster_Menus();
        Task<List<vm_RoleMenu>> GetMappedRoleMenu(Guid id);
        Task<CommonDbResponse> SaveRoleMenuMapping(List<vm_RoleMenu> roleMenu);
    }
}
