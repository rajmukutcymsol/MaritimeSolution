using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;
using TicketingTool.Models.ViewModel;

namespace TicketingTool.Services.Concrete.Mapping
{
    public interface IProjectRegionMapping
    {
        Task<T> Save<T>(int insert, vm_project_region_mapping _vm_project_region_mapping, string username);
        Task<List<vm_project_region_mapping>> GetAll(int getall);
        Task<CommonDbResponse> Delete(Guid? id);
        Task<List<T>> GetById<T>(int getAll, vm_project_region_mapping request);
        //Task<List<T> GetAll();
    }
}
