using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _CategoryRepository;
        public CategoryController(ICategoryRepository iCategoryRepository)
        {
            this._CategoryRepository = iCategoryRepository;
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
        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _CategoryRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_category request)
        {
            var result = await _CategoryRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update(master_category request)
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