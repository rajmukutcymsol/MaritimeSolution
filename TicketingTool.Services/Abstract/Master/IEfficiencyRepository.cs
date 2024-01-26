using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IEfficiencyRepository
    {
        Task<List<master_efficiency>> GetAll();
        Task<master_efficiency> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_efficiency request);
        Task<CommonDbResponse> Update(master_efficiency request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
