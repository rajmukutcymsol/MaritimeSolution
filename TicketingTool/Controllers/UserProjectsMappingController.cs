using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Mapping;
using TicketingTool.Services.Abstract.Role;
using TicketingTool.Models.Masters;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class UserProjectsMappingController : Controller
    {
        // GET: UserProjectsMapping
        private readonly IUserProjectsMapping _UserProjectsMappingRepository;
        public UserProjectsMappingController(IUserProjectsMapping UserProjectsMappingRepository)
        {
            this._UserProjectsMappingRepository = UserProjectsMappingRepository;
        }
        public async Task<ActionResult> Index()
        {
            var userList = await _UserProjectsMappingRepository.GetUsers((int)usp_ManageUser_Type.GetAll);
            userList.Insert(0, new vm_user_registration { employee_id = "0", employee_name = "--Select--" });
            ViewBag.UserList = userList;

            var projectList = await _UserProjectsMappingRepository.GetMaster_projects();
            ViewBag.ProjectList = projectList;

            return View();
        }
        public async Task<JsonResult> Save(vm_User_Projects_Mapping userProjectMapping)
        {
            string username = Session["user_name"].ToString();
            var result = await _UserProjectsMappingRepository.Save<CommonDbResponse>((int)ManageUserProjectMapping_Type.insert, userProjectMapping, username);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> GetUserProjectMappingByEmployeeId(vw_user_projects_mapping request)
        {
            var result = await _UserProjectsMappingRepository.GetUserProjectMappingByEmployeeId<vw_user_projects_mapping>((int)ManageUserProjectMapping_Type.getall, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _UserProjectsMappingRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}