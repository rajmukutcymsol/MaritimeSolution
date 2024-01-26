using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface ICargoTypeRepository
    {
        Task<List<master_cargo_type>> GetAll();
        Task<master_cargo_type> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_cargo_type request);
        Task<CommonDbResponse> Update(master_cargo_type request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
