using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketingTool.Models.ViewModel;

namespace TicketingTool.Services.Abstract.Master
{
    public interface IMasterRepository
    {
        Task<List<T>> GetMasterInfo<T>(int spType);
        Task<vm_master> GetAllMaster(int spType);
        Task<List<T>> GetData<T>(int spType,string procedureName);

    }
}
