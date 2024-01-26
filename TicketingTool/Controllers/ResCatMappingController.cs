using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Mapping;
using TicketingTool.Services.Abstract.Role;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class ResCatMappingController : Controller
    {
        private readonly IResCat1Repository resCat1Repository;
        private readonly IResCat2Repository resCat2Repository;
        private readonly IResCat3Repository resCat3Repository;
        private readonly IResCatMapping resCatMapping;

        public ResCatMappingController(IResCat1Repository ResCat1Repository, IResCat2Repository ResCat2Repository, IResCat3Repository ResCat3Repository, IResCatMapping ResCatMapping)
        {
            this.resCat1Repository = ResCat1Repository;
            this.resCat2Repository = ResCat2Repository;
            this.resCat3Repository = ResCat3Repository;
            this.resCatMapping = ResCatMapping;
        }
        public async Task<ActionResult> Index()
        {
            var cat1 = await resCat1Repository.GetAll();
            cat1.Insert(0, new master_res_cat_1 { id = Guid.Empty, res_cat1_name = "--Select--" });
            ViewBag.ResCat1 = cat1;

            var cat2 = await resCat2Repository.GetAll();
            cat2.Insert(0, new master_res_cat_2 { id = Guid.Empty, res_cat2_name = "--Select--" });
            ViewBag.ResCat2 = cat2;

            var cat3 = await resCat3Repository.GetAll();
            cat3.Insert(0, new master_res_cat_3 { id = Guid.Empty, res_cat3_name = "--Select--" });
            ViewBag.ResCat3 = cat3;

            return View();
        }
        public async Task<JsonResult> Save(vw_ResCatMapping vw_ResCatMapping)
        {
            string username = Session["user_name"].ToString();
            var result = await resCatMapping.Save<CommonDbResponse>((int)ResCatMapping_Type.insert, vw_ResCatMapping, username);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await resCatMapping.GetAll((int)ResCatMapping_Type.getall);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await resCatMapping.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}