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
    public class ProjectCustomersMappingController : Controller
    {
        private readonly IProjectCustomersMapping _IProjectCustomersMapping;
        private readonly ICustomerRepository _customerRepository;
        public ProjectCustomersMappingController(IProjectCustomersMapping ProjectCustomersMappingRepository, ICustomerRepository customerRepository)
        {
            this._IProjectCustomersMapping = ProjectCustomersMappingRepository;
            this._customerRepository = customerRepository;
        }
        // GET: ProjectCustomersMapping
        public async Task<ActionResult> Index()
        {
            var projectList = await _IProjectCustomersMapping.GetMaster_projects();
            projectList.Insert(0, new master_project { id = null, project_name = "--Select--" });
            ViewBag.ProjectList = projectList;

            var customerList = await _customerRepository.GetAll();
            customerList.Insert(0, new master_customer { id = Guid.Empty, customer_name = "--Select--" });
            ViewBag.CustomerList = customerList;
            return View();
        }
        public async Task<JsonResult> Save(vw_project_customers_mapping vw_project_customers_Mapping)
        {
            string username = Session["user_name"].ToString();
            var result = await _IProjectCustomersMapping.Save<CommonDbResponse>((int)ProjectCustomerMapping_Type.insert, vw_project_customers_Mapping, username);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetById(vw_project_customers_mapping request)
        {
            var result = await _IProjectCustomersMapping.GetById<vw_project_customers_mapping>((int)ProjectCustomerMapping_Type.getall, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _IProjectCustomersMapping.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}