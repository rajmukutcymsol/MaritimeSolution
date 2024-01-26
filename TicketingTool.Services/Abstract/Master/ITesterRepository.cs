using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface ITesterRepository
    {
        Task<List<master_tester>> GetAll();
        Task<master_tester> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_tester request);
        Task<CommonDbResponse> Update(master_tester request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
