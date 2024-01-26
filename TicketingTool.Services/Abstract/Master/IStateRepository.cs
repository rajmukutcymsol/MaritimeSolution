using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IStateRepository
    {
        Task<List<master_State>> GetAll();
        Task<master_State> GetById(Guid id);
        Task<CommonDbResponse> Save(master_State request);
        Task<CommonDbResponse> Update(master_State request);
        Task<CommonDbResponse> Delete(Guid id);
    }
}
