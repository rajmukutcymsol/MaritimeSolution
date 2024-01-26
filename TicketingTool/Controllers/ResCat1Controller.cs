using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class ResCat1Controller : Controller
    {
        private readonly IResCat1Repository _resCat1Repository;
        public ResCat1Controller(IResCat1Repository _IResCat1Repository)
        {
            this._resCat1Repository = _IResCat1Repository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _resCat1Repository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _resCat1Repository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_res_cat_1 request)
        {

            var result = await _resCat1Repository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_res_cat_1 request)
        {

            var result = await _resCat1Repository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _resCat1Repository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}