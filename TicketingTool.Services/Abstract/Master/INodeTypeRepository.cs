using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface INodeTypeRepository
    {
        Task<List<master_node_type>> GetAll();
        Task<master_node_type> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_node_type request);
        Task<CommonDbResponse> Update(master_node_type request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
