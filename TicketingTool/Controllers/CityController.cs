using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;
using PagedList;

namespace TicketingTool.Controllers
{
    public class CityController : Controller
    {
        // GET: City
        private readonly IStateRepository _CategoryRepository;
        private readonly IMasterRepository _masterRepository;
        private readonly ICityRepository _cityRepository;

        public CityController(IStateRepository ICountryRepository, IMasterRepository masterRepository, ICityRepository cityRepository)
        {
            this._CategoryRepository = ICountryRepository;
            this._masterRepository = masterRepository;
            this._cityRepository = cityRepository;
        }
        public async Task<ActionResult> Index()
        {
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            ViewBag.CountryList = allMaster.mastercountry;
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _cityRepository.GetAll();
             var jsoneResult= Json(result, JsonRequestBehavior.AllowGet);
            jsoneResult.MaxJsonLength = int.MaxValue;
            return jsoneResult;
        }
        public async Task<JsonResult> GetById(Guid id /*int CountryID, int stateid*/)
        {
            var result = await _cityRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_City request)
        {
            var result = await _cityRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update(master_City request)
        {
            var result = await _cityRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _cityRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}