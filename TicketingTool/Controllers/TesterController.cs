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
    public class TesterController : Controller
    {
        private readonly ITesterRepository _testerRepository;
        private readonly IUserRepository _userRepository;
        public TesterController(ITesterRepository testerRepository, IUserRepository userRepository)
        {
            this._testerRepository = testerRepository;
            this._userRepository = userRepository;
        }
        public async Task<ActionResult> Index()
        {
            var result = await _userRepository.GetUsers((int)usp_ManageUser_Type.GetAll);
            ViewBag.Testers = result;
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _testerRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _testerRepository.GetById(id);
            Session["edit_id"] = result.id;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_tester request)
        {

            var result = await _testerRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update(master_tester request)
        {
            request.id = (Guid)Session["edit_id"];
            var result = await _testerRepository.Update(request);
            Session["edit_id"] = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _testerRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}