using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class FunctionsController : Controller
    {
        private readonly IFunctionRepository _functionRepository;
        public FunctionsController(IFunctionRepository functionRepository)
        {
            this._functionRepository = functionRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _functionRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _functionRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_function request)
        {

            var result = await _functionRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_function request)
        {

            var result = await _functionRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _functionRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}