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
    public class DeveloperController : Controller
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IUserRepository _userRepository;
        public DeveloperController(IDeveloperRepository DeveloperRepository, IUserRepository userRepository)
        {
            this._developerRepository = DeveloperRepository;
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
            var result = await _developerRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _developerRepository.GetById(id);
            Session["edit_id"] = result.id;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_developer request)
        {

            var result = await _developerRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update(master_developer request)
        {
            request.id =(Guid)Session["edit_id"];
            var result = await _developerRepository.Update(request);
            Session["edit_id"] = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _developerRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}