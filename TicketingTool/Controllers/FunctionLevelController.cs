using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class FunctionLevelController : Controller
    {
        private readonly IFunctionLevelRepository _functionLevelRepository;
        public FunctionLevelController(IFunctionLevelRepository functionLevelRepository)
        {
            this._functionLevelRepository = functionLevelRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _functionLevelRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _functionLevelRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_function_level request)
        {

            var result = await _functionLevelRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_function_level request)
        {

            var result = await _functionLevelRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _functionLevelRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}