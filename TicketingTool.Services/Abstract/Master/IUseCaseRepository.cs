using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;
namespace TicketingTool.Services.Abstract.Master
{
   public interface IUseCaseRepository
    {
        Task<List<master_usecase>> GetAll();
        Task<master_usecase> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_usecase request);
        Task<CommonDbResponse> Update(master_usecase request);
        Task<CommonDbResponse> Delete(Guid? id);

    }
}
