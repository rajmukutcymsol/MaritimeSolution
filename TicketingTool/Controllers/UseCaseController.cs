using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]

    public class UseCaseController : Controller
    {
        private readonly IUseCaseRepository _usecaseRepository;
        public UseCaseController(IUseCaseRepository usecaseRepository)
        {
            this._usecaseRepository = usecaseRepository;
        }
        // GET: UseCase
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll()
        {
            var result = await _usecaseRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            Session["uID"] = null;
            Session["uID"] = id;
            var result = await _usecaseRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_usecase request)
        {

            var result = await _usecaseRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_usecase request)
        {
            request.id =(Guid)Session["uID"];
            var result = await _usecaseRepository.Update(request);
            Session["uID"] = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _usecaseRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}