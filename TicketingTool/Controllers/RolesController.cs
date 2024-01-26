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
    public class RolesController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        public RolesController(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetRoles()
        {
            var result = await _roleRepository.GetAllRoles((int)usp_ManageRole_Type.getAll);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetRoleById(Guid id)
        {
            var result = await _roleRepository.GetRoleById((int)usp_ManageRole_Type.getRoleById,id);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_role request)
        {
            var result = await _roleRepository.Save((int)usp_ManageRole_Type.saveRole, request);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_role request)
        {
            var result = await _roleRepository.Update((int)usp_ManageRole_Type.updateRole, request);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeleteRole(Guid id)
        {
            var result = await _roleRepository.DeleteRole((int)usp_ManageRole_Type.delete, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}