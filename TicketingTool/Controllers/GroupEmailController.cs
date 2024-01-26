using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    public class GroupEmailController : Controller
    {
        // GET: GroupEmail
        private readonly IGroupEmailRepository _groupEmailRepository;
        public GroupEmailController(IGroupEmailRepository groupEmailRepository)
        {
            this._groupEmailRepository = groupEmailRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _groupEmailRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _groupEmailRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_email_group request)
        {
            request.created_by = Session["user_name"].ToString();
            var result = await _groupEmailRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_email_group request)
        {
            request.updated_by = Session["user_name"].ToString();
            var result = await _groupEmailRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _groupEmailRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}