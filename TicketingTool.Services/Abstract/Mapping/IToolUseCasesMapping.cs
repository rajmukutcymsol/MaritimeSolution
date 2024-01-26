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
    public interface IToolUseCasesMapping
    {
        Task<List<master_usecase>> GetMaster_UseCaseName();
        Task<List<master_usecase>> GetAll();
        Task<T> Save<T>(int insert, vw_tool_usecases_mapping vw_tool_usecases_Mapping, string username);
        Task<List<T>> GetById<T>(int getAll, vw_tool_usecases_mapping request);
        Task<CommonDbResponse> Delete(Guid? id);
        Task<List<T>> GetByProjectNameWithToolId<T>(int spType, Guid toolid, Guid projectid);
    }
}
