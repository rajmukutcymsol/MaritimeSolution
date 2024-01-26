using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface ICustomerRepository
    {
        Task<List<master_customer>> GetAll();
        Task<master_customer> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_customer request);
        Task<CommonDbResponse> Update(master_customer request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
