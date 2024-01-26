using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;
using TicketingTool.Services.Abstract.User;

namespace TicketingTool.Controllers
{
    [UserAuthenticationFilter]
    public class SolutionArchitectController : Controller
    {
        private readonly ISolutionArchitectRepository _iSolutionArchitectRepository;
        private readonly IUserRepository _userRepository;
        public SolutionArchitectController(ISolutionArchitectRepository SolutionArchitectRepository,IUserRepository userRepository)
        {
            this._iSolutionArchitectRepository = SolutionArchitectRepository;
            this._userRepository = userRepository;
        }
        public async Task<ActionResult> Index()
        {

            var result = await _userRepository.GetUsers((int)usp_ManageUser_Type.GetAll);
            ViewBag.Developers = result;
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            

            var result = await _iSolutionArchitectRepository.GetAll();
           
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _iSolutionArchitectRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_solution_architect request)
        {

            var result = await _iSolutionArchitectRepository.Save(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_solution_architect request)
        {

            var result = await _iSolutionArchitectRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _iSolutionArchitectRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
    }
}