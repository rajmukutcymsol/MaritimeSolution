using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class ResCat3Controller : Controller
    {
        private readonly IResCat3Repository _resCat3Repository;
        public ResCat3Controller(IResCat3Repository _IResCat3Repository)
        {
            this._resCat3Repository = _IResCat3Repository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _resCat3Repository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _resCat3Repository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_res_cat_3 request)
        {

            var result = await _resCat3Repository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_res_cat_3 request)
        {

            var result = await _resCat3Repository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _resCat3Repository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}