using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class DomainsController : Controller
    {
        private readonly IDomainRepository _domainRepository;
        public DomainsController(IDomainRepository domainRepository)
        {
            this._domainRepository = domainRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll()
        {
            var result = await _domainRepository.GetAll();
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _domainRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_domain request)
        {
            
            var result = await _domainRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_domain request)
        {

            var result = await _domainRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _domainRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}