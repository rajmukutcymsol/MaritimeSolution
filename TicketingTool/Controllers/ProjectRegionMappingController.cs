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
using TicketingTool.Services.Concrete.Mapping;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class ProjectRegionMappingController : Controller
    {
        private readonly IMasterRepository _masterRepository;
        private readonly IProjectRegionMapping _projectRegionMapping;

        public ProjectRegionMappingController(IMasterRepository masterRepository, IProjectRegionMapping projectRegionMapping)
        {
            this._masterRepository = masterRepository;
            this._projectRegionMapping = projectRegionMapping;
        }
        // GET: ProjectRegionMapping
        public async Task<ActionResult> Index()
        {
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            allMaster.projects.Insert(0, new master_project { id = null, project_name = "--Select--" });
            ViewBag.ProjectForUser = allMaster.projects;
            allMaster.regions.Insert(0, new master_region { id = null, region_name = "--Select--" });
            ViewBag.region = allMaster.regions;

            return View();
        }
        public async Task<JsonResult> Save(vm_project_region_mapping _vm_Project_Region_Mapping )
        {
            string username = Session["user_name"].ToString();
            var result = await _projectRegionMapping.Save<CommonDbResponse>((int)ProjectRegionMapping_Type.insert, _vm_Project_Region_Mapping, username);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(vm_project_region_mapping request)
        {
            var result = await _projectRegionMapping.GetById<vm_project_region_mapping>((int)ProjectCustomerMapping_Type.getall, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _projectRegionMapping.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}