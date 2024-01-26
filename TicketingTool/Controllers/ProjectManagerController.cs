using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;
using TicketingTool.Services.Abstract.User;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class ProjectManagerController : Controller
    {
        // GET: ProjectManager
        private readonly IProjectManagerRepository _projectManagerRepository; 
        private readonly IUserRepository _userRepository;
        public ProjectManagerController(IProjectManagerRepository ProjectManagerRepository, IUserRepository userRepository)
        {
            this._projectManagerRepository = ProjectManagerRepository;
            this._userRepository = userRepository;

        }
        public async Task<ActionResult> Index()
        {

            var result = await _userRepository.GetUsers((int)usp_ManageUser_Type.GetAll);
            ViewBag.Developers = result;
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _projectManagerRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _projectManagerRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_project_manager request)
        {

            var result = await _projectManagerRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_project_manager request)
        {

            var result = await _projectManagerRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _projectManagerRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}