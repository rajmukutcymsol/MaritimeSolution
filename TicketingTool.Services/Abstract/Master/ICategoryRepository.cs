using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;


namespace TicketingTool.Services.Abstract.Master
{
   public interface ICategoryRepository
    {
        Task<List<master_category>> GetAll();
        Task<master_category> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_category request);
        Task<CommonDbResponse> Update(master_category request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
