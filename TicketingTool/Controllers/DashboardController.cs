using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Dashboard;
using TicketingTool.Services.Abstract.Master;
using TicketingTool.Services.Abstract.Role;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IMasterRepository _masterRepository;
        private readonly IDomainRepository _domainRepository;
        private readonly IRoleRightsRepository _roleRightsRepository;

        public DashboardController(IDashboardRepository dashboardRepository, IMasterRepository _IMasterRepository, IDomainRepository domainRepository, IRoleRightsRepository roleRepository)
        {
            this._dashboardRepository = dashboardRepository;
            this._masterRepository = _IMasterRepository;
            this._domainRepository = domainRepository;
            this._roleRightsRepository = roleRepository;
        }
        public async Task<ActionResult> Index()
        {
            //var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            //allMaster.projects.Insert(0, new master_project { id = null, project_name = "--Select--" });
            //ViewBag.ProjectForUser = allMaster.projects;
            var results = await _roleRightsRepository.GetRoleById((int)usp_ManageRoleRight_Type.getRoleById, (Guid)(Session["access_role_id"]));
            ViewBag.CanView = results.CanView == true ? "true" : "false";
            ViewBag.IsDownload = results.IsDownload == true ? "true" : "false";
            Session["CanUpdate"] = results.CanUpdate == true ? "true" : "false";
            Session["CanDelete"] = results.CanDelete == true ? "true" : "false";
            ViewBag.CanPrint = results.CanPrint == true ? "true" : "false";
            Session["CanVerify"] = results.CanVerify == true ? "true" : "false";

            return View();
        }
        
        //[ChildActionOnly]
        public async Task<PartialViewResult> PartialViewAction()
        {
            var result = await _dashboardRepository.GetDashboardDataCount<vm_dashboard_data_count>((int)usp_Dashbaord_Type.GetDataCount);
            return PartialView("~/Views/Shared/PartialView/Dashboard/DataCountView.cshtml", result);
        }

        [ChildActionOnly]
        public async Task<PartialViewResult> DashBoard_NR()
        {
            string employee_id = Session["user_name"].ToString();
            string access_role = Session["access_role"].ToString();
            string project_name = null;
            string domain_name = null;
            if (Session["project_name"] != null)
            {
                project_name = Session["project_name"].ToString();
            }
            if (Session["domain_name"] != null)
            {
                domain_name = Session["domain_name"].ToString();
            }
            var result = await _dashboardRepository.GetDashboardDataCount_NR<vm_dashboard_data_count>((int)usp_Dashbaord_Type_Info.GetDataCount_nr, employee_id, access_role, project_name, domain_name);
            var nr = await _dashboardRepository.GetSum((int)usp_Dashbaord_Type_Info.nrSum,employee_id,access_role, project_name, domain_name);
            ViewBag.newReqCount = nr;
           // Session["project_name"] = null;
            return PartialView("~/Views/Dashboard/DataCountView_nr.cshtml", result);
        }
        [ChildActionOnly]
        public async Task<PartialViewResult> DashBoard_CR()
        {
            string employee_id = Session["user_name"].ToString();
            string access_role = Session["access_role"].ToString();
            string project_name = null;
            string domain_name = null;

            if (Session["project_name"] != null)
            {
                project_name = Session["project_name"].ToString();
            }
            if (Session["domain_name"] != null)
            {
                domain_name = Session["domain_name"].ToString();
            }
            var result = await _dashboardRepository.GetDashboardDataCount_CR<vm_dashboard_data_count>((int)usp_Dashbaord_Type_Info.GetDataCount_cr, employee_id, access_role, project_name, domain_name);
            var cr = await _dashboardRepository.GetSum((int)usp_Dashbaord_Type_Info.crSum, employee_id, access_role, project_name, domain_name);
            ViewBag.crReqCount = cr;
           // Session["project_name"] = null;

            return PartialView("~/Views/Dashboard/DataCountView_cr.cshtml", result);
        }
        [ChildActionOnly]
        public async Task<PartialViewResult> DashBoard_IR()
        {
            string employee_id = Session["user_name"].ToString();
            string access_role = Session["access_role"].ToString();
            string project_name = null;
            string domain_name = null;

            if (Session["project_name"] != null)
            {
                project_name = Session["project_name"].ToString();
            }
            if (Session["domain_name"] != null)
            {
                domain_name = Session["domain_name"].ToString();
            }
            var result = await _dashboardRepository.GetDashboardDataCount_IR<vm_dashboard_data_count>((int)usp_Dashbaord_Type_Info.GetDataCount_ir, employee_id, access_role, project_name, domain_name);
            var ir = await _dashboardRepository.GetSum((int)usp_Dashbaord_Type_Info.irSum, employee_id, access_role, project_name, domain_name);
            ViewBag.irReqCount = ir;
            Session["project_name"] = null;
            Session["domain_name"] = null;
            return PartialView("~/Views/Dashboard/DataCountView_ir.cshtml", result);
        }
        //Projectsforusers
        public async Task<ActionResult> Projectsforusers(vm_user_registration request)
        {
            var result = await _dashboardRepository.GetProjectByUserId<master_project>((int)ProjectUser_Type.getbyuser, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SetProjectName(string projectName)
        {
            // Set the selected value into the session variable
            Session["Project_name"] = projectName;

            // Return a success response (optional)
            return Json(new { success = true });
        }
        public async Task<ActionResult> DomainNameforusers()
        {
            var result = await _domainRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SetDomainName(string domain_name)
        {
            Session["domain_name"] = domain_name;
            return Json(new { success = true });
        }
    }
}