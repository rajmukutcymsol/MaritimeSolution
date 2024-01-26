using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface ISDLCRepository
    {
        Task<List<master_sdlc>> GetAll();
        Task<master_sdlc> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_sdlc request);
        Task<CommonDbResponse> Update(master_sdlc request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
