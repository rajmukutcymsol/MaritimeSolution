using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class CargoTypeController : Controller
    {
        private readonly ICargoTypeRepository _cargoTypeRepository;
        public CargoTypeController(ICargoTypeRepository icargoTypeRepository)
        {
            this._cargoTypeRepository = icargoTypeRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _cargoTypeRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _cargoTypeRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_cargo_type request)
        {
            var result = await _cargoTypeRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update(master_cargo_type request)
        {
            var result = await _cargoTypeRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _cargoTypeRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}