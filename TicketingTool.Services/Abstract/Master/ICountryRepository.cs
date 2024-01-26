using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;


namespace TicketingTool.Services.Abstract.Master
{
    public interface ICountryRepository
    {
        Task<List<master_Country>> GetAll();
        Task<master_Country> GetById(int? id);
        Task<CommonDbResponse> Save(master_Country request);
        Task<CommonDbResponse> Update(master_Country request);
        Task<CommonDbResponse> Delete(int? id);
    }
}
