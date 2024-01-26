using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class ResCat2Controller : Controller
    {
        private readonly IResCat2Repository _resCat2Repository;
        public ResCat2Controller(IResCat2Repository _IResCat2Repository)
        {
            this._resCat2Repository = _IResCat2Repository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _resCat2Repository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _resCat2Repository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_res_cat_2 request)
        {

            var result = await _resCat2Repository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_res_cat_2 request)
        {

            var result = await _resCat2Repository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _resCat2Repository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}