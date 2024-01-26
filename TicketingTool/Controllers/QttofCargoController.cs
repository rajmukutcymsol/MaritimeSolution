using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class QttofCargoController : Controller
    {
        private readonly IQttRepository _qttRepository;
        public QttofCargoController(IQttRepository qttRepository)
        {
            this._qttRepository = qttRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll()
        {
            var result = await _qttRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _qttRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_quantity_cargo request)
        {

            var result = await _qttRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_quantity_cargo request)
        {

            var result = await _qttRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _qttRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}