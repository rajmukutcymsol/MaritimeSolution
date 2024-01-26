using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IResCat1Repository
    {
        Task<List<master_res_cat_1>> GetAll();
        Task<master_res_cat_1> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_res_cat_1 request);
        Task<CommonDbResponse> Update(master_res_cat_1 request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
