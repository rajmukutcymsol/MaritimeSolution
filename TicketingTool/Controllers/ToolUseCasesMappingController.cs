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
    public class ToolUseCasesMappingController : Controller
    {
        private readonly IToolUseCasesMapping _toolUseCasesMapping;
        private readonly ISolutionToolRepository _solutionToolRepository;
        private readonly IProjectsToolsMapping _projectToolRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly ITechnologyRepository _technologyRepository;
        private readonly INodeTypeRepository _nodeTypeRepository;

        public ToolUseCasesMappingController(IToolUseCasesMapping ToolUseCaseMapping, ISolutionToolRepository toolSolution, IProjectsToolsMapping projectToolRepository, IProjectRepository projectRepository, IVendorRepository vendorRepository, ITechnologyRepository technologyRepository, INodeTypeRepository nodeTypeRepository)
        {
            this._toolUseCasesMapping = ToolUseCaseMapping;
            this._solutionToolRepository = toolSolution;
            this._projectToolRepository = projectToolRepository;
            this._projectRepository = projectRepository;
            this._vendorRepository = vendorRepository;
            this._technologyRepository = technologyRepository;
            this._nodeTypeRepository = nodeTypeRepository;
        }
        // GET: ToolUseCasesMapping
        public async Task<ActionResult> Index()
        {
            var projectList = await _projectRepository.GetAll();
            projectList.Insert(0, new master_project { id = Guid.Empty, project_name = "--Select--" });
            ViewBag.ProjectList = projectList;

            var toolList = await _solutionToolRepository.GetAll();
            toolList.Insert(0, new master_tool { id = Guid.Empty, tool_name = "--Select--" });
            ViewBag.ToolList = toolList;

            var usecaseList = await _toolUseCasesMapping.GetAll();
            usecaseList.Insert(0, new master_usecase { id = Guid.Empty, use_case_name = "--Select--" });
            ViewBag.UsecaseList = usecaseList;

            var venderList = await _vendorRepository.GetAll();
            venderList.Insert(0, new master_vendor { id = Guid.Empty, vendor_name = "--Select--" });
            ViewBag.VenderList = venderList;

            var technologyList = await _technologyRepository.GetAll();
            technologyList.Insert(0, new master_technology { id = Guid.Empty, technology_name = "--Select--" });
            ViewBag.TechnologyList = technologyList;

            var nodeType = await _nodeTypeRepository.GetAll();
            nodeType.Insert(0, new master_node_type { id = Guid.Empty, node_type_name = "--Select--" });
            ViewBag.NodeType = nodeType;

            return View();
        }
        public async Task<JsonResult> Save(vw_tool_usecases_mapping vw_tool_usecases_Mapping)
        {
            string username = Session["user_name"].ToString();
            var result = await _toolUseCasesMapping.Save<CommonDbResponse>((int)ToolUseCaseMapping_Type.insert, vw_tool_usecases_Mapping, username);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetById(vw_tool_usecases_mapping request)
        {
            var result = await _toolUseCasesMapping.GetById<vw_tool_usecases_mapping>((int)ToolUseCaseMapping_Type.getall, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _toolUseCasesMapping.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GeToolstByProjectId(vw_project_tools_mapping request)
        {
            var result = await _projectToolRepository.GetById<vw_project_tools_mapping>((int)ToolUseCaseMapping_Type.getbyprojectID, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // vender
        public async Task<JsonResult> GetAll()
        {
            var result = await _vendorRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetByProjectNameWithToolId(Guid toolid, Guid projectid)
        {
            var result = await _toolUseCasesMapping.GetByProjectNameWithToolId<vw_tool_usecases_mapping>((int)ProjectToolsMapping_Type.getbyprojectwithTool, toolid, projectid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}