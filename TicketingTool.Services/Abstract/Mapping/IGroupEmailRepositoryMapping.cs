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
    public interface IGroupEmailRepositoryMapping
    {
        Task<T> Save<T>(int insert, vw_GroupEmailMapping vw_GroupEmailMapping, string username);
        Task<List<vw_GroupEmailMapping>> GetAll(int getall);
        Task<CommonDbResponse> Delete(Guid? id);
        Task<List<vw_GroupEmailMapping>> GetAll(int getall, vw_GroupEmailMapping vw_GroupEmailMapping);
    }
}
