using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface ISolutionArchitectRepository
    {
        Task<List<master_solution_architect>> GetAll();
        Task<master_solution_architect> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_solution_architect request);
        Task<CommonDbResponse> Update(master_solution_architect request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
