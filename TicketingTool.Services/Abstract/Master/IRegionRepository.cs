using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IRegionRepository
    {
        Task<List<master_region>> GetAll();
        Task<master_region> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_region request);
        Task<CommonDbResponse> Update(master_region request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
