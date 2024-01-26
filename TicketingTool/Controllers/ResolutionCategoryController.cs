using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
   
    public class ResolutionCategoryController : Controller
    {
        private readonly IResoluionCategoryRepository _resoluionCategoryRepository;
        public ResolutionCategoryController(IResoluionCategoryRepository ResoluionCategoryRepository)
        {
            this._resoluionCategoryRepository = ResoluionCategoryRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _resoluionCategoryRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _resoluionCategoryRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_resolution_category request)
        {

            var result = await _resoluionCategoryRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_resolution_category request)
        {

            var result = await _resoluionCategoryRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _resoluionCategoryRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}