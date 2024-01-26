using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.ViewModel;

namespace TicketingTool.Services.Abstract.User
{
    public interface IUserRepository
    {
        Task<List<vm_user_registration>> GetUsers(int spType);
        Task<vm_user_registration> GetUserById(int spType, string employeeId);
        Task<CommonDbResponse> UpdateUser(vm_user_registration obj_vm_user_registration, int spType);
        Task<CommonDbResponse> Delete(string id);
        Task<List<vm_user_registration>>GetDeveloperUsers(int spType);
        Task<List<vm_user_registration>> GetDeveloperUsersforTask(int spType);
        Task<List<vm_user_registration>> GetTesterList(int spType);
        Task<List<vm_user_registration>> GetUsersByName<T>(int getbyname, string prefix);
        Task<vm_user_registration> GetUserByName(int spType, string employeeId);
    }
}
