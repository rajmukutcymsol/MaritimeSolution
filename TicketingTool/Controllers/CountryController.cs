using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _CategoryRepository;
        public CountryController(ICountryRepository ICountryRepository)
        {
            this._CategoryRepository = ICountryRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _CategoryRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetById(int CountryID)
        {
            var result = await _CategoryRepository.GetById(CountryID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_Country request)
        {
            var result = await _CategoryRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update(master_Country request)
        {
            var result = await _CategoryRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Delete(int CountryID)
        {
            var result = await _CategoryRepository.Delete(CountryID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}