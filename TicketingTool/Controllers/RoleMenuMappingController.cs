using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Constant;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Mapping;
using TicketingTool.Services.Abstract.Role;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class RoleMenuMappingController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleMenuMapping _roleMenuMappingRepository;
        public RoleMenuMappingController(IRoleRepository roleRepository, IRoleMenuMapping roleMenuMappingRepository)
        {
            this._roleRepository = roleRepository;
            this._roleMenuMappingRepository = roleMenuMappingRepository;
        }
        public async Task<ActionResult> Index()
        {
            vm_menu_mapping roleMenuMapping = new vm_menu_mapping();
            roleMenuMapping.roles= await _roleRepository.GetAllRoles((int)usp_ManageRole_Type.getAll);
            roleMenuMapping.master_Menu = await _roleMenuMappingRepository.GetMaster_Menus();
            return View(roleMenuMapping);
        }

        public async Task<JsonResult> GetMappedMenu(Guid id)
        {
            var result = await _roleMenuMappingRepository.GetMappedRoleMenu(id);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Save(List<vm_RoleMenu> menuMappingList)
        {
            var result = await _roleMenuMappingRepository.SaveRoleMenuMapping(menuMappingList);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}