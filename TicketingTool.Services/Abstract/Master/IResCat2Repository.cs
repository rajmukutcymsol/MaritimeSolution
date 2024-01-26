using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IResCat2Repository
    {
        Task<List<master_res_cat_2>> GetAll();
        Task<master_res_cat_2> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_res_cat_2 request);
        Task<CommonDbResponse> Update(master_res_cat_2 request);
        Task<CommonDbResponse> Delete(Guid? id);
       // Task<List<master_res_cat_2>>GetByCat1Id(Guid? id);
        Task<List<master_res_cat_2>> GetByCat1Id<T>(Guid res_cat1);
        Task<List<master_res_cat_3>>GetByCat3Id<T>(Guid res_cat1, Guid res_cat2);
    }
}
