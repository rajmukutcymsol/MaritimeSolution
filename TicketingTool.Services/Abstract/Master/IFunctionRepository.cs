using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IFunctionRepository
    {
        Task<List<master_function>> GetAll();
        Task<master_function> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_function request);
        Task<CommonDbResponse> Update(master_function request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
