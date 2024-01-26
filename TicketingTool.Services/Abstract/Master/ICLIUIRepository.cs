using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface ICLIUIRepository
    {
        Task<List<master_cli_ui>> GetAll();
        Task<master_cli_ui> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_cli_ui request);
        Task<CommonDbResponse> Update(master_cli_ui request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
