using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class EfficiencyController : Controller
    {
        private readonly IEfficiencyRepository _efficiencyRepository;
        public EfficiencyController(IEfficiencyRepository efficiencyRepository)
        {
            this._efficiencyRepository = efficiencyRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _efficiencyRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _efficiencyRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_efficiency request)
        {

            var result = await _efficiencyRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_efficiency request)
        {

            var result = await _efficiencyRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _efficiencyRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}