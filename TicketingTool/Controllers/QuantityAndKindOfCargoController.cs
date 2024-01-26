using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;
namespace TicketingTool.Controllers
{
    public class QuantityAndKindOfCargoController : Controller
    {
        // GET: QuantityAndKindOfCargo
      
        private readonly IQuantityandKindofcargoname quantityandKindofcargoname;
        public QuantityAndKindOfCargoController(IQuantityandKindofcargoname IQuantityandKindofcargoname)
        {
            this.quantityandKindofcargoname = IQuantityandKindofcargoname;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await quantityandKindofcargoname.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await quantityandKindofcargoname.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(Quantity_and_Kind_of_cargo request)
        {
            var result = await quantityandKindofcargoname.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update(Quantity_and_Kind_of_cargo request)
        {
            var result = await quantityandKindofcargoname.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await quantityandKindofcargoname.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}