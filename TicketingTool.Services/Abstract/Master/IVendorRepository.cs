using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IVendorRepository
    {
        Task<List<master_vendor>> GetAll();
        Task<master_vendor> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_vendor request);
        Task<CommonDbResponse> Update(master_vendor request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
