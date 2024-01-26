using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IQuantityandKindofcargoname
    {
        Task<List<Quantity_and_Kind_of_cargo>> GetAll();
        Task<Quantity_and_Kind_of_cargo> GetById(Guid? id);
        Task<CommonDbResponse> Save(Quantity_and_Kind_of_cargo request);
        Task<CommonDbResponse> Update(Quantity_and_Kind_of_cargo request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
