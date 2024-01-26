using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Models;
using TicketingTool.Models.ViewModel;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Requirement
{
    public interface IInboundReportsRepository
    {
        Task<T> GetRow<T>(int spType, Guid id);
    }
}
