using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IMarksAndNosRepository
    {
        Task<List<marks_and_nos>> GetAll();
        Task<marks_and_nos> GetById(Guid? id);
        Task<CommonDbResponse> Save(marks_and_nos request);
        Task<CommonDbResponse> Update(marks_and_nos request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
