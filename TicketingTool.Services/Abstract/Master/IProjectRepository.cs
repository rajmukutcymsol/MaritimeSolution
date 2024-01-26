using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IProjectRepository
    {
        Task<List<master_project>> GetAll();
        Task<master_project> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_project request);
        Task<CommonDbResponse> Update(master_project request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
