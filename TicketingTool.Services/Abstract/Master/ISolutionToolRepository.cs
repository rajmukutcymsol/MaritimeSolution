using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface ISolutionToolRepository
    {
        Task<List<master_tool>> GetAll();
        Task<master_tool> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_tool request);
        Task<CommonDbResponse> Update(master_tool request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
