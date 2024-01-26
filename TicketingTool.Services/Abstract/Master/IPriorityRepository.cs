using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IPriorityRepository
    {
        Task<List<master_priority>> GetAll();
        Task<master_priority> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_priority request);
        Task<CommonDbResponse> Update(master_priority request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
