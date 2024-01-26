using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingTool.Models.ViewModel;

namespace TicketingTool.Services.Abstract.Authentication
{
    public interface IUserAuthenticationRepository
    {
        Task<vm_user_detail> GetUserInfo(string id,int spType, string password);
        Task<T> SaveUser<T>(vm_user_registration obj_vm_user_registration,int spType);
    }
}
