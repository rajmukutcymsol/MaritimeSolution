using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TicketingTool.Filters;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Models.Models;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Master;
using TicketingTool.Services.Abstract.Requirement;
using TicketingTool.Services.Abstract.Mapping;
using System.Configuration;
using TicketingTool.Utilities;
using TicketingTool.Services.Abstract.User;
using TicketingTool.Services.Concrete.Mapping;
using TicketingTool.Services.Abstract.Role;

namespace TicketingTool.Controllers
{
    public class InBoundController : Controller
    {
        private readonly IMasterRepository _masterRepository;
        private readonly IInboundRepository _inboundRepository;
        private readonly IRoleRightsRepository _roleRightsRepository;
        private readonly IInboundReportsRepository _inboundReportsRepository;
        private readonly IMarksAndNosRepository _marksAndNosRepository;
        private readonly ICargoTypeRepository _cargoTypeRepository;
        private readonly IVesselRepository _vesselRepository;
        public InBoundController( IMasterRepository masterRepository, IInboundRepository inboundRepository, IRoleRightsRepository roleRepository, IInboundReportsRepository inboundReportsRepository, IMarksAndNosRepository marksAndNosRepository, ICargoTypeRepository cargoTypeRepository, IVesselRepository vesselRepository)
        {
            this._masterRepository = masterRepository;
            this._inboundRepository = inboundRepository;
            this._roleRightsRepository = roleRepository;
            this._inboundReportsRepository = inboundReportsRepository;
            this._marksAndNosRepository = marksAndNosRepository;
            this._cargoTypeRepository = cargoTypeRepository;
            this._vesselRepository = vesselRepository;
        }
        // GET: InBound
        public async Task<ActionResult> Index()
        {
            var result = await _roleRightsRepository.GetRoleById((int)usp_ManageRoleRight_Type.getRoleById, (Guid)Session["access_role_id"]);
            
            return View();
        }
        public async Task<ActionResult> Create()
        {
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            ViewBag.auto_id = await _inboundRepository.GenerateAutoId((int)Generate_AutoId.NewRequest);

            return View(allMaster);
        }
        [HttpPost]
        public async Task<JsonResult> Save(master_inbound_requirement projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.Save<CommonDbResponse>((int)ManageInboundRequirement_Type.insert_requirement, projectRequirement, projectRequirement.created_by);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
        public async Task<JsonResult> GetAll()
        {
            var result = await _inboundRepository.GetAll<master_inbound_requirement>((int)ManageInboundRequirement_Type.getAll);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Update(master_inbound_requirement request)
        {
            var result = await _inboundRepository.Update(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> SaveManiFest(master_inbound_manifest projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.SaveManiFest<CommonDbResponse>((int)ManageInboundManiFest_Type.save, projectRequirement, projectRequirement.created_by);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetManiFestByAutoId(string auto_id)
        {
            var result = await _inboundRepository.GetManiFestByAutoId<master_inbound_manifest>((int)ManageInboundManiFest_Type.getById, auto_id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.Id = id;
            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            return View(allMaster);
        }
        public async Task<JsonResult> GetRequirementDetail(Guid id)
        {
            var result = await _inboundRepository.GetRow<master_inbound_requirement>((int)ManageInboundManiFest_Type.getById, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateAll(master_inbound_requirement projectRequirement)
        {
            try
            {
                projectRequirement.created_by = Session["user_name"].ToString();
                var result = await _inboundRepository.UpdateAll<CommonDbResponse>((int)ManageInboundRequirement_Type.updateall, projectRequirement, projectRequirement.created_by);
            return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<JsonResult> Edit_ManiFest(Guid id)
        {
            var result = await _inboundRepository.GetRowManiFest<master_inbound_manifest>((int)ManageInboundManiFest_Type.updatemainefest, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> UpdateManiFest(master_inbound_manifest projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.UpdateManiFest<CommonDbResponse>((int)ManageInboundManiFest_Type.update, projectRequirement, projectRequirement.created_by);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _inboundRepository.Delete(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Delete_InBound(Guid id)
        {
            var result = await _inboundRepository.Delete_InBound(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Approved(string auto_id, Guid id)
        {
            var result = await _inboundRepository.Approved(auto_id, Session["user_name"].ToString(), id); ;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Details(Guid id)
        {
            var result = await _inboundReportsRepository.GetRow<vm_Report_InBound_Details>((int)ManageInbound_Reports_Type.getDetailsById, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Manifest(Guid id)
        {
            var result = new { id_ref = id };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetApprovalData(Guid id)
        {
            var result = await _inboundRepository.GetApprovalData<master_inbound_requirement>((int)ManageInboundManiFest_Type.GetApprovalData_VesselInfo, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetCargoMenifestById(string auto_id)
        {
            var result = await _inboundRepository.GetCargoMenifestById<master_inbound_manifest>((int)ManageInboundManiFest_Type.GetMeniFest_ById, auto_id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> CreateStowagePlan(Guid id)
        {
            var marksnos = await _marksAndNosRepository.GetAll();
            marksnos.Insert(0, new marks_and_nos { id = Guid.Empty, marks_and_nos_name = "Empty" });
            ViewBag.vm_marks_and_nos = marksnos;

            //cargo_type_name

            var cargotype = await _cargoTypeRepository.GetAll();
            cargotype.Insert(0, new master_cargo_type { id = Guid.Empty, cargo_type_name = "Empty" });
            ViewBag.Master_cargo_type = cargotype;
            //masterreciver

            var allMaster = await _masterRepository.GetAllMaster((int)MasterType.all);
            allMaster.masterreciver.Insert(0, new vm_Receiver { id = Guid.Empty, receiver_name = "Empty" });
            ViewBag.masterreciver = allMaster.masterreciver;
            
            var result = await _inboundRepository.GetStowagePlan_VesselDetails<vm_StowagePlan>((int)usp_StowagePlan.getById, Procedures.usp_GetStowagePlan, id);

            var Vesselresult = await _vesselRepository.GetAll();
            List<vm_StowagePlan> roles = new List<vm_StowagePlan>();
            for (int i = 1; i <= Convert.ToInt32(result.capacities); i++)
            {
                roles.Add(new vm_StowagePlan { Key = "Hold " + i, Value = "Hold " + i });
            }
            //roles.Add(new vm_StowagePlan { Key = "D/K", Value = "D/K " });
            ViewBag.Capi = roles;


            var GetMainPlanInfo = await _inboundRepository.GetRowPlanInfo<vm_Stowage_Main_Info>((int)usp_StowagePlan.GetPlainInfo, id);
            ViewBag._mainPlan = GetMainPlanInfo ?? new vm_Stowage_Main_Info { approved = false, arrival_date = null, sailedon_date=null };
            return View(result);
        }
      
        public async Task<ActionResult> GetMarksnos()
        {
            var marksnos = await _marksAndNosRepository.GetAll();
            return Json(marksnos, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Save_Stowage_info(vm_Stowage_Plan_Info request)
        {
            request.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.Save_Stowage_info<CommonDbResponse>((int)usp_StowagePlan.PlanInfo, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetAllStowage_info(string auto_id)
        {
            var result = await _inboundRepository.GetAllStowage_info<vm_Stowage_Plan_Info>((int)usp_StowagePlan.GetPlanInfo, auto_id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> Edit_Stowage_Info(Guid id)
        {
            var result = await _inboundRepository.GetRow_StowagePlanInfo<vm_Stowage_Plan_Info>((int)usp_StowagePlan.GetRowByID, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Update_Stowage_info(vm_Stowage_Plan_Info request)
        {
            request.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.Update_Stowage_info<CommonDbResponse>((int)usp_StowagePlan.Update, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> DeleteStowsInfo(Guid id)
        {
            var result = await _inboundRepository.DeleteStowsInfo(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Stowage_Main_Info(vm_Stowage_Main_Info request)
        {
            request.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.Stowage_Main_Info<CommonDbResponse>((int)usp_StowagePlan.save_Stowage_Plan_Info, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> ApprovePlanInfo(Guid id, string auto_id)
        {
            var result = await _inboundRepository.ApprovePlanInfo(id, auto_id);
            var GetMainPlanInfo = await _inboundRepository.GetRowPlanInfo<vm_Stowage_Main_Info>((int)usp_StowagePlan.GetPlainInfo, id);
            ViewBag._mainPlan = GetMainPlanInfo;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Stowage_Plan_Approval_Report(Guid id, string auto_id)
        {
            var result = new { id_ref = id , auto_id = auto_id };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ArrivalCondition(Guid id)
        {
            //vm_ArrivalCondition vm = new vm_ArrivalCondition();
            var result = await _inboundRepository.GetArrivalCondition_MainInfo<vm_ArrivalCondition>((int)usp_Arrival_Condition.getInfoById, id);
            if (result.auto_id != null)
            {
                ViewBag.results = result;

                var GetInfo = await _inboundRepository.GetRowArrivalInfo<vm_ArrivalCondition>((int)usp_Arrival_Condition.getInfo, id);
                if (GetInfo != null)
                {
                    ViewBag.EAT_MM_completed_discharge_cargo = GetInfo.EAT_MM_completed_discharge_cargo;
                    ViewBag.EAT_HH_completed_discharge_cargo = GetInfo.EAT_HH_completed_discharge_cargo;
                    ViewBag.EAT_MM_commenced_discharge_cargo = GetInfo.EAT_MM_commenced_discharge_cargo;
                    ViewBag.EAT_HH_commenced_discharge_cargo = GetInfo.EAT_HH_commenced_discharge_cargo;
                    ViewBag.EAT_MM_dropped_anchor = GetInfo.EAT_MM_dropped_anchor;
                    ViewBag.EAT_HH_dropped_anchor = GetInfo.EAT_HH_dropped_anchor;
                    ViewBag.EAT_MM_pilot_on_board_arrival = GetInfo.EAT_MM_pilot_on_board_arrival;

                    ViewBag.EAT_HH_pilot_on_board_arrival = GetInfo.EAT_HH_pilot_on_board_arrival;
                    ViewBag.approved = GetInfo.approved;
                    ViewBag.commenced_discharge_cargo = GetInfo.commenced_discharge_cargo;

                    ViewBag.readiness_accepted = GetInfo.readiness_accepted;
                    ViewBag.pilot_on_board_arrival = GetInfo.pilot_on_board_arrival;
                    ViewBag.dropped_anchor = GetInfo.dropped_anchor;
                    ViewBag.completed_discharge_cargo = GetInfo.commenced_discharge_cargo;
                    ViewBag.commenced_discharge_cargo = GetInfo.commenced_discharge_cargo;

                    //
                    ViewBag.fwd = GetInfo.fwd;
                    ViewBag.aft = GetInfo.aft;
                    ViewBag.fo = GetInfo.fo;
                    ViewBag.doo = GetInfo.doo;

                    ViewBag.fw = GetInfo.fw;

                }
                else
                {
                    ViewBag.approved = false;
                    ViewBag.EAT_MM_completed_discharge_cargo = "0";
                    ViewBag.EAT_HH_completed_discharge_cargo = "0";
                    ViewBag.EAT_HH_commenced_discharge_cargo = "0";
                    ViewBag.EAT_MM_dropped_anchor = "0";
                    ViewBag.EAT_HH_dropped_anchor = "0";
                    ViewBag.EAT_MM_pilot_on_board_arrival = "0";
                    ViewBag.EAT_HH_pilot_on_board_arrival = "0";

                    ViewBag.fwd = "";
                    ViewBag.aft = "";
                    ViewBag.fo = "";
                    ViewBag.doo = "";
                    ViewBag.fw = "";

                    //
                    ViewBag.readiness_accepted = "";
                    ViewBag.pilot_on_board_arrival = "";
                    ViewBag.dropped_anchor = "";
                    ViewBag.completed_discharge_cargo = "";

                    ViewBag.commenced_discharge_cargo = "";

                }
                return View();
            }
            else {
                return View("~/Views/InBound/NoAction.cshtml");
            }
        }
        public async Task<ActionResult> DepartureCondition(Guid id)
        {
            var result = await _inboundRepository.GetArrivalCondition_MainInfo<vm_ArrivalCondition>((int)usp_Arrival_Condition.getInfoById, id);
            if (result.auto_id != null)
            {
                ViewBag.results = result;

                var GetInfo = await _inboundRepository.GetDepartureCondition_MainInfo<vm_ArrivalCondition>((int)usp_Arrival_Condition.getInfo, id);
                if (GetInfo != null)
                {
                    ViewBag.EAT_MM_departure_from = GetInfo.EAT_MM_departure_from;
                    ViewBag.EAT_HH_departure_from = GetInfo.EAT_HH_departure_from;
                    ViewBag.EAT_MM_pilot_on_board_departure = GetInfo.EAT_MM_pilot_on_board_departure;
                    ViewBag.EAT_HH_pilot_on_board_departure = GetInfo.EAT_HH_pilot_on_board_departure;
                    ViewBag.approved = GetInfo.approved;
                    ViewBag.fwd = GetInfo.fwd;
                    ViewBag.aft = GetInfo.aft;
                    ViewBag.fo = GetInfo.fo;
                    ViewBag.doo = GetInfo.doo;
                    ViewBag.fw = GetInfo.fw;

                    //
                    ViewBag.pilot_on_board_departure = GetInfo.pilot_on_board_departure;
                    ViewBag.dropped_anchor = GetInfo.dropped_anchor;
                    ViewBag.bunker_eta_next_port = GetInfo.bunker_eta_next_port;
                    ViewBag.bunker_fuel_oil = GetInfo.bunker_fuel_oil;

                    ViewBag.bunker_diesel_oil = GetInfo.bunker_diesel_oil;
                    ViewBag.bunker_fresh_water = GetInfo.bunker_fresh_water;
                    ViewBag.other_watch_man = GetInfo.other_watch_man;
                    ViewBag.other_police_man = GetInfo.other_police_man;
                    ViewBag.other_cash_advance = GetInfo.other_cash_advance;
                    ViewBag.bunker_fuel_oil = GetInfo.bunker_fuel_oil;
                    ViewBag.departue_from = GetInfo.departue_from;
                    ViewBag.departue_from_port_name = GetInfo.departue_from_port_name;
                    ViewBag.ETA_Next_Port_Date = GetInfo.ETA_Next_Port_Date;
                    ViewBag.ETA_Next_Port_MM = GetInfo.ETA_Next_Port_MM;
                    ViewBag.ETA_Next_Port_HH = GetInfo.ETA_Next_Port_HH;
                    ViewBag.ETA_Next_Port_AMPM = GetInfo.ETA_Next_Port_AMPM;
                }
                else
                {
                    ViewBag.EAT_MM_departure_from = "0";
                    ViewBag.EAT_HH_departure_from = "0";
                    ViewBag.EAT_MM_pilot_on_board_departure = "0";
                    ViewBag.EAT_HH_pilot_on_board_departure = "0";
                    ViewBag.approved = false;

                    ViewBag.fwd = "";
                    ViewBag.aft = "";
                    ViewBag.fo = "";
                    ViewBag.doo = "";
                    ViewBag.fw = "";

                    ViewBag.pilot_on_board_departure = "";
                    ViewBag.dropped_anchor = "";
                    ViewBag.bunker_eta_next_port = "";
                    ViewBag.bunker_fuel_oil = "";

                    ViewBag.bunker_diesel_oil = "";
                    ViewBag.bunker_fresh_water = "";
                    ViewBag.other_watch_man = "";
                    ViewBag.other_police_man = "";
                    ViewBag.other_cash_advance = "";
                    ViewBag.bunker_fuel_oil = "";
                    ViewBag.dropped_anchor = "";

                    ViewBag.ETA_Next_Port_Date = "";
                    ViewBag.ETA_Next_Port_MM = "0";
                    ViewBag.ETA_Next_Port_HH = "0";
                    ViewBag.ETA_Next_Port_AMPM = "0";

                }

                return View();
            }
            else
            {
                return View("~/Views/InBound/NoAction.cshtml");
            }
            }
        [HttpPost]
        public async Task<JsonResult> Save_ArrivalSailingCondition(vm_ArrivalCondition projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.Save_ArrivalSailingCondition<CommonDbResponse>((int)usp_Arrival_Condition.save, projectRequirement);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Approved_Arrival(vm_ArrivalCondition request)
        {
            var result = await _inboundRepository.Approved_Arrival(request.auto_id, Session["user_name"].ToString(), request.id); ;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Approved_Departure(vm_ArrivalCondition request)
        {
            var result = await _inboundRepository.Approved_Departure(request.auto_id, Session["user_name"].ToString(), request.id); ;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Arrival_Approval_Report(Guid id, string auto_id)
        {
            var result = new { id_ref = id, auto_id = auto_id };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Statementof_Fact(Guid id, string auto_id)
        {
            var result = new { id_ref = id, auto_id = auto_id };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> Departure_Approval_Report(Guid id, string auto_id)
        {
            var result = new { id_ref = id, auto_id = auto_id };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Save_DepartureSailingCondition(vm_ArrivalCondition projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.Save_DepartureSailingCondition<CommonDbResponse>((int)usp_Arrival_Condition.save_departure, projectRequirement);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> DailyReport(Guid id)
        {
           
            return View();
        }
        public async Task<ActionResult> StatementofFact(Guid id)
        {
            return View();
        }
        public async Task<ActionResult> CreateDailyReport(Guid id, string report_date)
        {
            var result = await _inboundRepository.GetArrivalCondition_MainInfo<vm_ArrivalCondition>((int)usp_Arrival_Condition.getInfoById, id);
            ViewBag.results = result;

            var capacity = await _inboundRepository.GetStowagePlan_VesselDetails<vm_StowagePlan>((int)usp_StowagePlan.getById, Procedures.usp_GetStowagePlan, id);

            List<vm_StowagePlan> roles = new List<vm_StowagePlan>();
            for (int i = 1; i <= Convert.ToInt32(capacity.capacities); i++)
            {
                roles.Add(new vm_StowagePlan { Key = "Hold " + i, Value = "Hold " + i });
            }
            //roles.Add(new vm_StowagePlan { Key = "D/K", Value = "D/K " });
            ViewBag.Capi = roles;

            var allInfo = await _inboundRepository.GetAllPreInfo<master_daily_report>((int)usp_DailyReport.getAllInfoForPri, id);
            ViewBag.allInfo = allInfo;
            if (report_date == "")
            {
                report_date = DateTime.Now.ToString("yyyy-MM-dd");
                var PreviousInfo = await _inboundRepository.GetAllPreviousInfo<master_daily_report>((int)usp_DailyReport.PreviousInfo, id, report_date);
                ViewBag.PreviousInfo = PreviousInfo;
            }
            
            return View();
        }
        public async Task<JsonResult> CreateDailyReportForDate(string auto_id, string report_date)
        {
            //report_date = DateTime.Now.ToString("yyyy-MM-dd");
            var result = await _inboundRepository.GetAllPreviousInfo_by_auto_id<master_daily_report>((int)usp_DailyReport.PreviousInfo_byautoid, auto_id, report_date);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Save_CargoDayStatus(CargoStatusOfDay request)
        {
            request.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.Save_CargoDayStatus<CommonDbResponse>((int)usp_CargoStausDay.save, request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetGargoByID(string auto_id, String report_date, string HH_report_date, string MM_report_date)
        {
            var result = await _inboundRepository.GetGargoByID<usp_CargoStausDay>((int)usp_CargoStausDay.getbyai, auto_id,report_date, HH_report_date, MM_report_date);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> Edit_CargoStatusDay(Guid id)
        {
            var result = await _inboundRepository.Edit_CargoStatusDay<CargoStatusOfDay>((int)usp_CargoStausDay.getstatusdetailbyId, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateCargoStatusDay(CargoStatusOfDay projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.UpdateCargoStatusDay<CommonDbResponse>((int)usp_CargoStausDay.update_status, projectRequirement, projectRequirement.created_by);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> Delete_CargoStatusDay(Guid id)
        {
            var result = await _inboundRepository.Delete_CargoStatusDay(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       
        public async Task<JsonResult> Save_Remarks(cargo_status_of_day_remarks projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.Save_Remarks<CommonDbResponse>((int)usp_Cargo_Remarks.save, projectRequirement);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetRemarksData(Guid id, string auto_id, string report_date)
        {
            string htmlcode = "";
            //report_date = Session["report_date"].ToString();
            List<cargo_status_of_day_remarks> lp = new List<cargo_status_of_day_remarks>();
            lp = await _inboundRepository.GetRemarksDate<cargo_status_of_day_remarks>((int)usp_Cargo_Remarks.get_date_group, id, auto_id, report_date);
            if (lp != null)
            {
                foreach (var item in lp)
                {
                    htmlcode += "<div style =\"font-weight: bold; margin-top: 20px;\">";
                    htmlcode += ""+item.date_of_action+"";
                    //htmlcode += "<button class=\"btn btn-link delete-btn\" data-toggle=\"modal\"><i class=\"fa fa-trash\"></i></button>";
                    htmlcode += "</div>";


                    List<cargo_status_of_day_remarks> listofRemarks = new List<cargo_status_of_day_remarks>();
                    listofRemarks = await _inboundRepository.GetRemarks<cargo_status_of_day_remarks>((int)usp_Cargo_Remarks.getremarks, id, auto_id, item.date_of_action);
                    foreach (var items in listofRemarks)
                    {
                        htmlcode += "<div style =\"margin-left: 20px;\">";
                        htmlcode += "" + items.remarks_comments + "";
                        htmlcode += "<button id=" + items.id + " class=\"btn btn-sm btn-success_s\">Edit</i></button>";
                        htmlcode += "</div>";
                    }
                }
            }
           
            var result = new { html = htmlcode };
            Session["date_of_action"] = null;
            return Json(result, JsonRequestBehavior.AllowGet);
        }//GetHistoryByDate
        [HttpPost]
        public async Task<JsonResult> InsertDailyReport(master_daily_report projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();
            var data = await _inboundRepository.InsertDailyReport<CommonDbResponse>((int)usp_DailyReport.save, projectRequirement);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetAllDailyReports(Guid id_ref)
        {
            var result = await _inboundRepository.GetAllDailyReports<master_daily_report>((int)usp_DailyReport.getall, id_ref);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> DeleteDailyReport(Guid id)
        {
            var result = await _inboundRepository.DeleteDailyReport(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<ActionResult> EditDailyReport(Guid id)
        {
            var result = await _inboundRepository.GetUpdateDailyReportInfo<master_daily_report>((int)usp_DailyReport.getreportinfo_id, id);
            ViewBag.results = result;
            ViewBag.HH_report_date = result.HH_report_date;
            ViewBag.MM_report_date = result.MM_report_date;
            Session["report_date"] = result.report_date;

            var allInfo = await _inboundRepository.UpdateGetAllPreInfo<master_daily_report>((int)usp_DailyReport.getupdateinfo, id);
            ViewBag.allInfo = allInfo;

            var allInfor = await _inboundRepository.GetAllPreInfo<master_daily_report>((int)usp_DailyReport.getAllInfoForPri, allInfo.id_ref);
            ViewBag.allInfor = allInfor;

            return View();
        }
        [HttpPost]
        public async Task<JsonResult> UpdateRemarks(cargo_status_of_day_remarks projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.Update_Remarks<CommonDbResponse>((int)usp_Cargo_Remarks.updateremarks, projectRequirement);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetRemarksByID(cargo_status_of_day_remarks projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.get_Remarks<cargo_status_of_day_remarks>((int)usp_Cargo_Remarks.getremarksbyid, projectRequirement);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> DeleteRemarks(Guid id)
        {
            var result = await _inboundRepository.DeleteRemarks(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateDailyReport(master_daily_report projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();
            var data = await _inboundRepository.UpdateDailyReport<CommonDbResponse>((int)usp_DailyReport.updatedaily, projectRequirement);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> save_sof(sof projectRequirement)
        {
            projectRequirement.created_by = Session["user_name"].ToString();
            var result = await _inboundRepository.save_sof<CommonDbResponse>((int)ManageSOF_Type.save, projectRequirement, projectRequirement.created_by);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetsofData(Guid id, string auto_id)
        {
            var result = await _inboundRepository.GetsofData<sof>((int)ManageSOF_Type.getall, id, auto_id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //410000148893
    }
}