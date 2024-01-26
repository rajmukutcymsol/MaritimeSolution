using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
   public interface IResoluionCategoryRepository
    {
        Task<List<master_resolution_category>> GetAll();
        Task<master_resolution_category> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_resolution_category request);
        Task<CommonDbResponse> Update(master_resolution_category request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
