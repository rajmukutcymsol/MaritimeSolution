using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IStatusRepository
    {
        Task<List<master_status>> GetAll();
        Task<master_status> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_status request);
        Task<CommonDbResponse> Update(master_status request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
