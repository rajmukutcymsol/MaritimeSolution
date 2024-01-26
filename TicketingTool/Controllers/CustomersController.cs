using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomersController(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll()
        {
            var result = await _customerRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _customerRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_customer request)
        {

            var result = await _customerRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_customer request)
        {
            var result = await _customerRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _customerRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}