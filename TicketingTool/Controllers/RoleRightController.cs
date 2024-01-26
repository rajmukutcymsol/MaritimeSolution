using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Role;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]

    public class RoleRightController : Controller
    {
        private readonly IRoleRightsRepository _roleRightsRepository;
        public RoleRightController(IRoleRightsRepository roleRepository)
        {
            this._roleRightsRepository = roleRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetRoles()
        {
            var result = await _roleRightsRepository.GetAllRoles((int)usp_ManageRoleRight_Type.getAll);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetRoleById(Guid id)
        {
            var result = await _roleRightsRepository.GetRoleById((int)usp_ManageRoleRight_Type.getRoleById, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(Auth request)
        {
            var result = await _roleRightsRepository.Save((int)usp_ManageRoleRight_Type.saveRole, request);
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(Auth request)
        {
            var result = await _roleRightsRepository.Update((int)usp_ManageRoleRight_Type.saveRole, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteRole(Guid id)
        {
            var result = await _roleRightsRepository.DeleteRole((int)usp_ManageRoleRight_Type.delete, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}