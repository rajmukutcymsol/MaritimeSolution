using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;
using TicketingTool.Models.ViewModel;

namespace TicketingTool.Services.Abstract.Mapping
{
    public interface IResCatMapping
    {
        Task<T> Save<T>(int insert, vw_ResCatMapping vw_ResCatMapping, string username);
        Task<List<vw_ResCatMapping>> GetAll(int getall);
        Task<CommonDbResponse> Delete(Guid? id);

    }
}
