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
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class ProjectToolsMappingController : Controller
    {
        private readonly IProjectsToolsMapping _projectsToolsMapping;
        private readonly ISolutionToolRepository _solutionToolRepository;
        public ProjectToolsMappingController(IProjectsToolsMapping UserProjectsMappingRepository, ISolutionToolRepository solutionToolRepository)
        {
            this._projectsToolsMapping = UserProjectsMappingRepository;
            this._solutionToolRepository = solutionToolRepository;

        }

        // GET: ProjectToolsMapping
        public async Task<ActionResult> Index()
        {
            var projectList = await _projectsToolsMapping.GetMaster_projects();
            projectList.Insert(0, new master_project { id = null, project_name = "--Select--" });
            ViewBag.ProjectList = projectList;

            var toolList = await _solutionToolRepository.GetAll();
            toolList.Insert(0, new master_tool { id=Guid.Empty, tool_name = "--Select--"});
            ViewBag.ToolList = toolList;
            return View();
        }
        public async Task<JsonResult> Save(vw_project_tools_mapping vw_project_tools_Mapping)
        {
            string username = Session["user_name"].ToString();
            var result = await _projectsToolsMapping.Save<CommonDbResponse>((int)ProjectToolsMapping_Type.insert, vw_project_tools_Mapping, username);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetById(vw_project_tools_mapping request)
        {
            var result = await _projectsToolsMapping.GetById<vw_project_tools_mapping>((int)ProjectToolsMapping_Type.getall, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _projectsToolsMapping.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}