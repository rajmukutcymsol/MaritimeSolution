using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface ITechnologyRepository
    {
        Task<List<master_technology>> GetAll();
        Task<master_technology> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_technology request);
        Task<CommonDbResponse> Update(master_technology request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
