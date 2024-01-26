using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    public class MarksAndNosController : Controller
    {
        private readonly IMarksAndNosRepository _IMarksAndNosRepository;
        public MarksAndNosController(IMarksAndNosRepository IMarksAndNosRepository)
        {
            this._IMarksAndNosRepository = IMarksAndNosRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _IMarksAndNosRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _IMarksAndNosRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(marks_and_nos request)
        {
            var result = await _IMarksAndNosRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update(marks_and_nos request)
        {
            var result = await _IMarksAndNosRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _IMarksAndNosRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}