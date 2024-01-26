using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IDomainRepository
    {
        Task<List<master_domain>> GetAll();
        Task<master_domain> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_domain request);
        Task<CommonDbResponse> Update(master_domain request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
