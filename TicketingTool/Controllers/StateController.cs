using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    public class StateController : Controller
    {
        private readonly IStateRepository _CategoryRepository;
        private readonly IMasterRepository _masterRepository;
        public StateController(IStateRepository ICountryRepository, IMasterRepository masterRepository)
        {
            this._CategoryRepository = ICountryRepository;
            this._masterRepository = masterRepository;
        }
        public async Task<ActionResult> Index()
        {
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            ViewBag.CountryList = allMaster.mastercountry;
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _CategoryRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetById(Guid id /*int CountryID, int stateid*/)
        {
            var result = await _CategoryRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_State request)
        {
            var result = await _CategoryRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update(master_State request)
        {
            var result = await _CategoryRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _CategoryRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}