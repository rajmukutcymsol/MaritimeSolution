using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Constant;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Role;
using TicketingTool.Services.Abstract.User;
using TicketingTool.Utilities;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UsersController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.role = await _roleRepository.GetAllRoles((int)usp_ManageRole_Type.getAll);
            return View();
        }

        public async Task<JsonResult> GetUserList()
        {
            var result = await _userRepository.GetUsers((int)usp_ManageUser_Type.GetAll);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetUserById(string employeeId)
        {
            var result = await _userRepository.GetUserById((int)usp_ManageUser_Type.GetUserByIdNew, employeeId);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateUser(vm_user_registration request)
        {
            //string emailsentstatus = "0";
            //var useractivationresult = await _userRepository.GetUserById((int)usp_ManageUser_Type.GetUserByIdNew, request.employee_id);
            //if (request.state == 0 && useractivationresult.state == 1)//for D-activation done
            //{
            //    emailsentstatus ="1";
            //    // de activation
            //}
            //else if (request.state == 1 && useractivationresult.state == 0)
            //{
            //    // sent email for activation done
            //    emailsentstatus ="2";
            //}
            //else {
            //    emailsentstatus = "0";
            //}
            var result = await _userRepository.UpdateUser(request,(int)usp_ManageUser_Type.UpdateUser);
            //if (result.STATUS == true)
            //{
            //    if (emailsentstatus == "2") // activation done
            //    {
            //        var TKTcc = ConfigurationManager.AppSettings["TKTcc"];
            //        var NewRequestSubject = "Resolution HUB -Account Activation Successful";
            //        string to = useractivationresult.email_address;
            //        string cc = TKTcc;
            //        string subject = NewRequestSubject;// + DateTime.Now.ToString("yyyy_MM_dd-HH");
            //        string emailBody = @"Dear " + useractivationresult.employee_name + ",<br/></br>" +
            //                          "We are pleased to inform you that your account has been successfully activated. You can now access all the features and services associated with your account.<br/>" +
            //                          "If you have any questions or require further assistance, please feel free to contact our support team. We are here to help you. </br></br>" +
            //                          "Thank you for choosing our platform. We look forward to serving you.<br/></br>" +
            //                          "<b>Best Regards<br/>" +
            //                          "<b>GDC Resolution Hub.";
            //        
            //    }
            //    else if (emailsentstatus == "1")
            //    {
            //        var TKTcc = ConfigurationManager.AppSettings["TKTcc"];
            //        var NewRequestSubject = "Resolution HUB -Account Deactivation Notice";
            //        string to = useractivationresult.email_address;
            //        string cc = TKTcc;
            //        string subject = NewRequestSubject;
            //        string emailBody = @"Dear " + useractivationresult.employee_name + ",<br/></br>" +
            //                          "We regret to inform you that your account has been deactivated. This means that you will no longer have access to your account and its associated features.<br/>" +
            //                          "Should you wish to reactivate your account in the future, please contact our support team for further assistance.</br></br>" +
            //                          "Thank you for your previous association with us.<br/></br>" +
            //                          "<b>Best Regards<br/>" +
            //                          "<b>GDC Resolution Hub.";
            //        
            //    }
            //}
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Delete(string employee_id)
        {
            var result = await _userRepository.Delete(employee_id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
    }
}