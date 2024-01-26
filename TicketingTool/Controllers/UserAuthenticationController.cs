using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using TicketingTool.Filters;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.ViewModel;
//using TicketingTool.oAuthService;
using TicketingTool.Services.Abstract.Authentication;
using TicketingTool.Services.Abstract.Master;
using TicketingTool.Models.Masters;
using TicketingTool.Utilities;
using System.Configuration;
using System.Web;

namespace TicketingTool.Controllers
{
    
    public class UserAuthenticationController : Controller
    {
        private readonly IMasterRepository _masterRepository;
        private readonly IUserAuthenticationRepository _userAuthenticationRepository;
        public UserAuthenticationController(IUserAuthenticationRepository userAuthenticationRepository, IMasterRepository masterRepository)
        {
            this._userAuthenticationRepository = userAuthenticationRepository;
            this._masterRepository = masterRepository;
        }
       
        [HttpGet]
        public ActionResult AuthenticateUser()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> AuthenticateUser(vm_user_authentication user)
        {
            var user_Detail =await _userAuthenticationRepository.GetUserInfo(user.username, (int)usp_ManageUser_Type.GetUserById, user.password);
            if (user_Detail.userDetail==null || string.IsNullOrEmpty(user_Detail.userDetail.user_role)|| string.IsNullOrEmpty(user_Detail.userDetail.access_role))
                ViewBag.Message = "The username/password combination used is invalid";
            else if(user_Detail.userDetail.state==0)
                ViewBag.Message = "User Is Not Active. Kindly contact admin for more details";
            else
            {
                    vm_employee result_vm_employee = new vm_employee();
                    //var userProfile = AuthenticationService.GetUser(user.username, user.password, user_Detail.userDetail.user_role);
                    result_vm_employee.Name = user_Detail.userDetail.employee_name;
                    result_vm_employee.EmailId = user_Detail.userDetail.email_address;
                    result_vm_employee.ManagerName = user_Detail.userDetail.manager_name;
                    Session["user_profile"] =JsonConvert.SerializeObject(result_vm_employee);
                    Session["user_name"] = user.username;
                    Session["menu"] = user_Detail.menuDetail;
                    Session["access_role"] = user_Detail.userDetail.access_role;
                    Session["access_role_id"] = user_Detail.userDetail.access_role_id;
                    return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        public ActionResult Register()
        {
            List<vm_location> locations = new List<vm_location>();
            locations.Add(new vm_location { Key = "Bangkok Head Office", Value = "Bangkok Head Office" });
            locations.Add(new vm_location { Key = "Pune, India", Value = "Pune, India" });

            List<vm_Role> roles = new List<vm_Role>();
            roles.Add(new vm_Role { Key = "Internal", Value = "Internal" });
            roles.Add(new vm_Role { Key = "External", Value = "External" });

            ViewBag.location = locations;
            ViewBag.roles = roles;
            var allMaster = _masterRepository.GetAllMaster((int)MasterType.all);
           List<master_project> items = new List<master_project>();
            
           foreach (var item in allMaster.Result.projects)
            {
            items.Add( new master_project { id = item.id, project_name = item.project_name });
            }
            ViewBag.Items = items;

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Register(vm_user_registration employee)
        {
            CommonDbResponse response = new CommonDbResponse();

            employee.employee_name = employee.display_name;
            response = await _userAuthenticationRepository.SaveUser<CommonDbResponse>(employee, (int)usp_ManageUser_Type.Insert_User);
                //if (response.STATUS == true)
                //{
                //    //string Dateofrequest = employee.Value.ToString("dd/MM/yyyy");
                //    var TKTcc = ConfigurationManager.AppSettings["TKTcc"];
                //    var NewRequestSubject = ConfigurationManager.AppSettings["Registration"];
                //                                                                                                                 //var NewRequestSubject = ConfigurationManager.AppSettings["NewRequestSubject"];
                //    string to = employee.email_address;
                //    string cc = TKTcc;
                //    string subject = NewRequestSubject;// + DateTime.Now.ToString("yyyy_MM_dd-HH");
                //    string emailBody = @"Dear User <br/></br>" +//+ authenticationResult.DISPLAYNAME + ",<br/></br>" +
                //                        "Congratulations! We are thrilled to inform you that your registration with GDC Resolution Hub has been successfully completed.<br/></br>" +
                //                        "Please note that your account will take maximum 2 working days to activate. <br/></br>" +
                //                        "Thank you for choosing GDC Resolution Hub. We appreciate your trust in us and are excited to have you on board!. <br/></br>" +
                //                        "Best Regards<br/>" +
                //                        "GDC Resolution Hub.";
                //    
                //}
                
            return Json(response,JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public JsonResult VarifyUser(vm_passord_varify_request_parameter request)
        //{
        //    vm_employee result_vm_employee = new vm_employee();
        //    var AuthenticationService = new oAuthService.oAuthService(); 
        //    var result = AuthenticationService.GetUser(request.UsrEmplyoeeCode, request.UsrPassword, request.UsrRole);
        //    if (result == null || result.UID == "")
        //    {
        //        result_vm_employee.Result = 0;
        //        result_vm_employee.Response = "Error Occurred. Please check the password.";
        //    }
        //    else
        //    {
        //        result_vm_employee.Name = result.GECOS;
        //        result_vm_employee.EmailId = result.NSNPRIMARYEMAILADDRESS;
        //        result_vm_employee.ManagerName = result.NSNBUSINESSPREFMGRNAME;
        //        result_vm_employee.Department = result.NSNALUDEPARTMENT;
        //        result_vm_employee.Result = 1; 
        //        result_vm_employee.Response = "Success";
        //    }
        //    return Json(result_vm_employee, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return RedirectToAction("AuthenticateUser", "UserAuthentication");
        }
    }
}