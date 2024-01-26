using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IVesselRepository
    {
        Task<List<master_vessel>> GetAll();
        Task<CommonDbResponse> Save(master_vessel request);
        Task<master_vessel> GetById(Guid? id);
        Task<CommonDbResponse> Update(int spType, master_vessel request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
