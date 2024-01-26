using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IFunctionLevelRepository
    {
        Task<List<master_function_level>> GetAll();
        Task<master_function_level> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_function_level request);
        Task<CommonDbResponse> Update(master_function_level request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
