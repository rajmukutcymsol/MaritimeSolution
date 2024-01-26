using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
   public interface ICityRepository
    {
        Task<List<master_City>> GetAll();
        Task<master_City> GetById(Guid id);
        Task<CommonDbResponse> Save(master_City request);
        Task<CommonDbResponse> Update(master_City request);
        Task<CommonDbResponse> Delete(Guid id);
    }
}
