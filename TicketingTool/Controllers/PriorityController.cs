using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class PriorityController : Controller
    {
        private readonly IPriorityRepository _priorityRepository;
        public PriorityController(IPriorityRepository priorityRepository)
        {
            this._priorityRepository = priorityRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll()
        {
            var result = await _priorityRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _priorityRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_priority request)
        {

            var result = await _priorityRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_priority request)
        {

            var result = await _priorityRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _priorityRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}