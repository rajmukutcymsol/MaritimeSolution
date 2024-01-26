using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IDischargePortRipository
    {
        Task<List<master_discharge_port>> GetAll();
        Task<master_discharge_port> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_discharge_port request);
        Task<CommonDbResponse> Update(master_discharge_port request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
