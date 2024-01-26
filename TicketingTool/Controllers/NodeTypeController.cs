using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class NodeTypeController : Controller
    {
        private readonly INodeTypeRepository _nodeTypeRepository;
        public NodeTypeController(INodeTypeRepository nodeTypeRepository)
        {
            this._nodeTypeRepository = nodeTypeRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _nodeTypeRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _nodeTypeRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_node_type request)
        {

            var result = await _nodeTypeRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_node_type request)
        {

            var result = await _nodeTypeRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _nodeTypeRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}