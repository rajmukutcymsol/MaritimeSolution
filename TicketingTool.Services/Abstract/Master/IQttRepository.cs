using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IQttRepository
    {
        Task<List<master_quantity_cargo>> GetAll();
        Task<master_quantity_cargo> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_quantity_cargo request);
        Task<CommonDbResponse> Update(master_quantity_cargo request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
