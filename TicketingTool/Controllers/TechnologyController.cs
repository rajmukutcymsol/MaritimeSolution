using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class TechnologyController : Controller
    {
        private readonly ITechnologyRepository _technologyRepository;
        public TechnologyController(ITechnologyRepository technologyRepository)
        {
            this._technologyRepository = technologyRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _technologyRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _technologyRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_technology request)
        {

            var result = await _technologyRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_technology request)
        {

            var result = await _technologyRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _technologyRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}