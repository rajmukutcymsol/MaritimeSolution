using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class DischargePortController : Controller
    {
        private readonly IDischargePortRipository _dischargePortRipository;
        public DischargePortController(IDischargePortRipository idischargePortRipository)
        {
            this._dischargePortRipository = idischargePortRipository;
        }
        // GET: DischargePort
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _dischargePortRipository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _dischargePortRipository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_discharge_port request)
        {

            var result = await _dischargePortRipository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_discharge_port request)
        {
            var result = await _dischargePortRipository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _dischargePortRipository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}