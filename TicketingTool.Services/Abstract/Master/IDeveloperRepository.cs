using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IDeveloperRepository
    {
        Task<List<master_developer>> GetAll();
        Task<master_developer> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_developer request);
        Task<CommonDbResponse> Update(master_developer request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
