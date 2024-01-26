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
using TicketingTool.Services.Abstract.User;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class GroupEmailMappingController : Controller
    {
        // GET: GroupEmailMapping
        private readonly IUserRepository _userRepository;
        private readonly Services.Abstract.Mapping.IGroupEmailRepositoryMapping _groupEmailRepositoryMapping;
        private readonly IGroupEmailRepository _groupEmailRepository;


        public GroupEmailMappingController(IUserRepository userRepository, Services.Abstract.Mapping.IGroupEmailRepositoryMapping groupEmailRepositoryMapping, IGroupEmailRepository groupEmailRepository)
        {
            this._userRepository = userRepository;
            this._groupEmailRepositoryMapping = groupEmailRepositoryMapping;
            _groupEmailRepository = groupEmailRepository;
        }
        public async Task<ActionResult> Index()
        {
            var cat1 = await _groupEmailRepository.GetAll();
            cat1.Insert(0, new master_email_group { id = Guid.Empty, group_name = "--Select--" });
            ViewBag.GroupNameList = cat1;

            var cat2 = await _userRepository.GetUsers((int)usp_ManageUser_Type.GetAll);
            //var customers = await _userRepository.GetUsersByName<vm_user_registration>((int)usp_ManageUser_Type.getbyname, prefix);
            cat2.Insert(0, new vm_user_registration { employee_id = "", employee_name = "--Select--" });
            ViewBag.EmployeeList = cat2;

            //var cat3 = await resCat3Repository.GetAll();
            //cat3.Insert(0, new master_res_cat_3 { id = Guid.Empty, res_cat3_name = "--Select--" });
            //ViewBag.ResCat3 = cat3;

            return View();
        }
        public async Task<JsonResult> Save(vw_GroupEmailMapping _vw_GroupEmailMapping)
        {
            string username = Session["user_name"].ToString();
            var result = await _groupEmailRepositoryMapping.Save<CommonDbResponse>((int)usp_GroupEmailMapping_Type.insert, _vw_GroupEmailMapping, username);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetAll(vw_GroupEmailMapping _vw_GroupEmailMapping)
        {
            var result = await _groupEmailRepositoryMapping.GetAll((int)usp_GroupEmailMapping_Type.getbyid, _vw_GroupEmailMapping);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _groupEmailRepositoryMapping.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}