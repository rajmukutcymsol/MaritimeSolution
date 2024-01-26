using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IProjectManagerRepository
    {
        Task<List<master_project_manager>> GetAll();
        Task<master_project_manager> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_project_manager request);
        Task<CommonDbResponse> Update(master_project_manager request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
