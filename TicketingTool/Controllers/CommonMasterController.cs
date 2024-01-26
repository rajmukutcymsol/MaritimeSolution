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
    public class CommonMasterController : Controller
    {
        private readonly ICommonMasterRepository _commonMasterRepository;
        private readonly IMasterRepository _masterRepository;

        public CommonMasterController(ICommonMasterRepository icommonMasterRepository, IMasterRepository masterRepository)
        {
            this._commonMasterRepository = icommonMasterRepository;
            this._masterRepository = masterRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAll()
        {
            var result = await _commonMasterRepository.GetAll();
            //var result = await _commonMasterRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            return View(allMaster);
        }

        public async Task<JsonResult> GetById(Guid id)
        {
            

            var result = await _commonMasterRepository.GetById(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save(master_common_mstr request)
        {
            request.created_by = Session["user_name"].ToString();
            var result = await _commonMasterRepository.Save((int)usp_ManageCommonMaster.save,request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> SavePicInformation(master_pic_information request)
        {
            request.Created_By = Session["user_name"].ToString();
            var result = await _commonMasterRepository.SavePicInformation((int)usp_ManagePicInformation.save, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update(master_common_mstr request)
        {
            request.created_by = Session["user_name"].ToString();
            var result = await _commonMasterRepository.Save((int)usp_ManageCommonMaster.update, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _commonMasterRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetStateByID(master_common_mstr request)
        {
            var result = await _commonMasterRepository.GetStateById<master_State>((int)usp_ManageCommonMaster.getstatebyid, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetCityByID(master_common_mstr request)
        {
            var result = await _commonMasterRepository.GetCityByID<master_City>((int)usp_ManageCommonMaster.getstatebyid, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetPicData(master_pic_information request)
        {
            var result = await _commonMasterRepository.GetPicData(request);
            //var result = await _commonMasterRepository.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetRequirementDetail(Guid id)
        {
            var result = await _commonMasterRepository.GetRow<master_common_mstr>((int)usp_ManageCommonMaster.getById, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Id = id;
            var result = await _commonMasterRepository.GetRow<master_common_mstr>((int)usp_ManageCommonMaster.getById, id);
            ViewBag.aid = result.auto_id;
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);

            return View(allMaster);
        }
        public async Task<ActionResult> DeletePic(Guid id)
        {
            var result = await _commonMasterRepository.DeletePic(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}