using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;


namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class VesselController : Controller
    {
        private readonly IVesselRepository _vesselRepository;
        public VesselController(IVesselRepository vesselRepository)
        {
            this._vesselRepository = vesselRepository;
        }

        // GET: Vessel
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> Save(master_vessel request)
        {
            var result = await _vesselRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _vesselRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _vesselRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update(master_vessel formData)
        {
            var result = await _vesselRepository.Update((int)usp_ManageVessel.update, formData);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _vesselRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}