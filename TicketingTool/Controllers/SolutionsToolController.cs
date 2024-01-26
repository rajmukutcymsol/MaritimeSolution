using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class SolutionsToolController : Controller
    {
        private readonly ISolutionToolRepository _solutionToolRepository;
        public SolutionsToolController(ISolutionToolRepository solutionToolRepository)
        {
            this._solutionToolRepository = solutionToolRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _solutionToolRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _solutionToolRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_tool request)
        {

            var result = await _solutionToolRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_tool request)
        {

            var result = await _solutionToolRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _solutionToolRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}