using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Models;
using TicketingTool.Models.ViewModel;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Abstract.Requirement
{
    public interface IInboundRepository
    {
        Task<T> Save<T>(int spType, master_inbound_requirement project_Requirement, string createdBy);
        Task<T> save_sof<T>(int spType, sof project_Requirement, string createdBy);
        Task<string> GenerateAutoId(int spType);
        //Task GetAll<T>(int getAll, T request);
        Task<List<master_inbound_requirement>> GetAll<T>(int spType);
        Task<CommonDbResponse> Update(master_inbound_requirement request);
        Task<T> SaveManiFest<T>(int spType, master_inbound_manifest request, string createdBy);
        Task<T> GetManiFestById<T>(int getById, string taskId);
        Task<List<master_inbound_manifest>> GetManiFestByAutoId<T>(int spType, string taskId);

        Task<T> GetRow<T>(int spType, Guid id);
        Task<CommonDbResponse> UpdateAll<T>(int spType, master_inbound_requirement projectRequirement, string created_by);
        Task<T> GetRowManiFest<T>(int spType, Guid id);
        Task<T> UpdateManiFest<T>(int spType, master_inbound_manifest projectRequirement, string created_by);
        Task<CommonDbResponse> Delete(Guid? id);
        Task<CommonDbResponse> Delete_InBound(Guid? id);
        Task<CommonDbResponse> Approved(string auto_id, string user_name, Guid? id);
        Task<T> GetApprovalData<T>(int spType, Guid id);
        Task<List<master_inbound_manifest>> GetCargoMenifestById<T>(int spType, string auto_id);
        Task<T> GetStowagePlan_VesselDetails<T>(int spType, string proc,Guid id);
        Task<CommonDbResponse> Save_Stowage_info<T>(int sp_Type, Models.ViewModel.vm_Stowage_Plan_Info request);
        Task<List<vm_Stowage_Plan_Info>> GetAllStowage_info<T>(int spType, string auto_id);
        Task<T> GetRow_StowagePlanInfo<T>(int spType, Guid id);
        Task<CommonDbResponse> Update_Stowage_info<T>(int sp_Type, Models.ViewModel.vm_Stowage_Plan_Info request);
        Task<CommonDbResponse> DeleteStowsInfo(Guid? id);
        //Task<CommonDbResponse> Update_Stowage_Info<T>(int sp_Type, Models.ViewModel.vm_Stowage_Main_Info request);
        Task<CommonDbResponse> Stowage_Main_Info<T>(int sp_Type, Models.ViewModel.vm_Stowage_Main_Info request);
        Task<T> GetRowPlanInfo<T>(int getPlainInfo, Guid id);
        Task<CommonDbResponse> ApprovePlanInfo(Guid id, string auto_id);
        //Task<T> GetArrivalCondition_MainInfo<T>(int spType, Guid id);
        Task<T> GetArrivalCondition_MainInfo<T>(int spType, Guid id) where T : new();
        Task<T> GetDepartureCondition_MainInfo<T>(int spType, Guid id);

        Task<CommonDbResponse> Save_ArrivalSailingCondition<T>(int save, vm_ArrivalCondition arrival);
        Task<T> GetRowArrivalInfo<T>(int getPlainInfo, Guid id);
        Task<T> GetRowDepartureInfo<T>(int getPlainInfo, Guid id);

        Task<CommonDbResponse> Approved_Arrival(string auto_id, string user_name, Guid? id);
        Task<CommonDbResponse> Approved_Departure(string auto_id, string user_name, Guid? id);

        Task<CommonDbResponse> Save_DepartureSailingCondition<T>(int save, vm_ArrivalCondition arrival);
        Task<CommonDbResponse> Save_CargoDayStatus<T>(int sp_Type, CargoStatusOfDay request);
        Task<List<CargoStatusOfDay>> GetGargoByID<T>(int spType, string taskId, String report_date, string HH_report_date, string MM_report_date);
        Task<T> Edit_CargoStatusDay<T>(int spType, Guid id);

        Task<T> UpdateCargoStatusDay<T>(int spType, CargoStatusOfDay projectRequirement, string created_by);
        Task<CommonDbResponse> Delete_CargoStatusDay(Guid? id);

        Task<CommonDbResponse> Save_Remarks<T>(int save, cargo_status_of_day_remarks arrival);
        Task<CommonDbResponse> Update_Remarks<T>(int save, cargo_status_of_day_remarks arrival);
        Task<List<cargo_status_of_day_remarks>>GetRemarksDate<T>(int get_date_group, Guid id, string auto_id, string date_of_action);
        Task<List<cargo_status_of_day_remarks>> GetRemarks<T>(int get_date_group, Guid id, string auto_id, string date_of_action);

        //Task<List<CargoStatusOfDay>> GetGargoByID<T>(int spType, string taskId);
        Task<CommonDbResponse> InsertDailyReport<T>(int save, master_daily_report arrival);
        Task<CommonDbResponse> UpdateDailyReport<T>(int save, master_daily_report arrival);

        Task<List<master_daily_report>> GetAllDailyReports<T>(int spType, Guid id);
        Task<T> GetDailyReportInfo<T>(int spType, Guid id);

        Task<T> GetAllPreInfo<T>(int spType, Guid id);
        Task<T> GetAllPreviousInfo<T>(int spType, Guid id, string report_date);
        Task<T> GetAllPreviousInfo_by_auto_id<T>(int spType, string auto_id, string report_date);

        Task<T> GetUpdateDailyReportInfo<T>(int spType, Guid id);
        Task<T> get_Remarks<T>(int getremarksbyid, cargo_status_of_day_remarks projectRequirement);
        Task<CommonDbResponse> DeleteRemarks(Guid? id);
        Task<CommonDbResponse> DeleteDailyReport(Guid? id);

        Task<T> UpdateGetAllPreInfo<T>(int spType, Guid id);
        Task<T> GetsofData<T>(int spType, Guid id, string auto_id);

    }
}
