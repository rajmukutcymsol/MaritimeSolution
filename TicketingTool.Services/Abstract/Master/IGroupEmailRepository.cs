using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IGroupEmailRepository
    {
        Task<List<master_email_group>> GetAll();
        Task<master_email_group> GetById(Guid? id);
        Task<CommonDbResponse> Save(master_email_group request);
        Task<CommonDbResponse> Update(master_email_group request);
        Task<CommonDbResponse> Delete(Guid? id);
    }
}
