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
    public interface IProjectCustomersMapping
    {
        Task<List<master_project>>GetMaster_projects();
        Task<T> Save<T>(int insert, vw_project_customers_mapping vw_project_customers_Mapping, string username);
        Task<List<T>> GetById<T>(int getAll, vw_project_customers_mapping request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
