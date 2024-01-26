using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            this._projectRepository = projectRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _projectRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _projectRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_project request)
        {
            var result = await _projectRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_project request)
        {
            var result = await _projectRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _projectRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}