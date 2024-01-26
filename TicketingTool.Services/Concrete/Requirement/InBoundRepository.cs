using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Models;
using TicketingTool.Models.ViewModel;
using TicketingTool.Models.Masters;
using System.Data.SqlClient;
using TicketingTool.Data.Helper;
using TicketingTool.Data.Connection;
using TicketingTool.Models.Constant;
using TicketingTool.Services.Abstract.Requirement;
using TicketingTool.Utilities;
using System.Globalization;

namespace TicketingTool.Services.Concrete.Requirement
{
    public class InBoundRepository: IInboundRepository
    {
        public async Task<string> GenerateAutoId(int spType)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) };
            var dbResult = await SqlHelper.ExecuteDatasetAsync(ConnectionCofig.ConnectionStr, Procedures.usp_GenerateAutoId, parameters);
            return dbResult.Tables[0].Rows[0]["auto_id"].ToString();
        }

        public async Task<List<master_inbound_requirement>> GetAll<T>(int spType)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", ManageInboundRequirement_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundRequirement, parameters);
            return CommonUtility.ConvertDataTableToList<master_inbound_requirement>(db_result.Tables[0]);
        }

        public async Task<T> Save<T>(int spType, master_inbound_requirement projectRequirement, string createdBy)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType", spType),
                    new SqlParameter("@auto_id", projectRequirement.auto_id),
                    new SqlParameter("@vessel_name", projectRequirement.vessel_name),
                    new SqlParameter("@discharge_port", projectRequirement.discharge_port),
                    new SqlParameter("@EAT_HH", projectRequirement.EAT_HH),
                    new SqlParameter("@EAT_MM", projectRequirement.EAT_MM),
                    new SqlParameter("@EAT_Date", projectRequirement.EAT_Date),
                    new SqlParameter("@refNo", projectRequirement.refNo),
                    new SqlParameter("@rcn", projectRequirement.rcn),
                    new SqlParameter("@LoadPort", projectRequirement.LoadPort),
                    new SqlParameter("@created_by", projectRequirement.created_by),
                    // new SqlParameter("@requirement_type", "In-Bound"),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundRequirement, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<CommonDbResponse> Update(master_inbound_requirement request)
        {
            SqlParameter[] parameter =
                  {
                    new SqlParameter("@spType", usp_ManageDeveloper_Type.update),
                    new SqlParameter("@mastername", request.mastername),
                    new SqlParameter("@mastercontactnumber", request.mastercontactnumber),
                    new SqlParameter("@masteremail", request.masteremail),
                    new SqlParameter("@owners_name", request.owners_name),
                    new SqlParameter("@receiver_name", request.receiver_name),
                    new SqlParameter("@surveyor_name", request.surveyor_name),
                    new SqlParameter("@stevedore_name", request.stevedore_name),
                    new SqlParameter("@shipping_name", request.shipping_name),
                    new SqlParameter("@checker_name", request.checker_name),
                    new SqlParameter("@auto_id", request.auto_id),
                    new SqlParameter("@VoyageNo", request.VoyageNo),
                    new SqlParameter("@LastPortofCall", request.LastPortofCall),
                    new SqlParameter("@FC_Stevedore_Name", request.FC_Stevedore_Name),
                    new SqlParameter("@refNo", request.refNo),
                    new SqlParameter("@rcn", request.rcn),
                    new SqlParameter("@eta_notice", Convert.ToDateTime(request.eta_notice)),
                    new SqlParameter("@EAT_HH_eta_notice", Convert.ToInt32(request.EAT_HH_eta_notice)),
                    new SqlParameter("@EAT_MM_eta_notice", Convert.ToInt32(request.EAT_MM_eta_notice)),
                    new SqlParameter("@EAT_HH_commence_to_discharge_cargo",Convert.ToInt32(request.EAT_HH_commence_to_discharge_cargo)),
                    new SqlParameter("@commence_to_discharge_cargo", Convert.ToDateTime(request.commence_to_discharge_cargo)),
                    new SqlParameter("@EAT_MM_commence_to_discharge_cargo", Convert.ToInt32(request.EAT_MM_commence_to_discharge_cargo)),
                    new SqlParameter("@est_to_complete_loading_cargo", Convert.ToDateTime(request.est_to_complete_loading_cargo)),
                    new SqlParameter("@plan_of_sailing",Convert.ToDateTime(request.plan_of_sailing)),
                    new SqlParameter("@vessels_stay",Convert.ToInt32(request.vessels_stay)),
                    new SqlParameter("@lat",request.lat),
                    new SqlParameter("@longt",request.nor_notice),
                    new SqlParameter("@nor_notice",Convert.ToDateTime(request.nor_notice)),
                    new SqlParameter("@EAT_HH_nor_notice",Convert.ToInt32(request.EAT_HH_nor_notice)),
                    new SqlParameter("@EAT_MM_nor_notice",Convert.ToInt32(request.EAT_MM_nor_notice)),

                };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundRequirement, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        //public async Task<T> SaveManiFest<T>(int spType, master_inbound_manifest projectRequirement, string createdBy)
        //{
        //    try
        //    {
        //        SqlParameter[] parameters =
        //        {
        //            new SqlParameter("@spType", spType),
        //            new SqlParameter("@auto_id", projectRequirement.auto_id),
        //            new SqlParameter("@blno", projectRequirement.blno),
        //            new SqlParameter("@shipper_name", projectRequirement.shipper_name),
        //            new SqlParameter("@consignee", projectRequirement.consignee),
        //            new SqlParameter("@notify", projectRequirement.notify),
        //            new SqlParameter("@marks_and_nos_name", projectRequirement.marks_and_nos_name),
        //            new SqlParameter("@quantity_and_kind_of_cargo_name", projectRequirement.quantity_and_kind_of_cargo_name),
        //            new SqlParameter("@quantity", projectRequirement.quantity),
        //            new SqlParameter("@cargotype_desc", projectRequirement.cargotype_desc),
        //            new SqlParameter("@is_active", projectRequirement.is_active),
        //            new SqlParameter("@is_deleted", projectRequirement.is_deleted),
        //            new SqlParameter("@created_by", projectRequirement.created_by),
        //            new SqlParameter("@created_date", projectRequirement.created_date),
        //            new SqlParameter("@updated_by", projectRequirement.updated_by),
        //            new SqlParameter("@updated_date", projectRequirement.updated_date)
        //        };
        //        var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundManifest, parameters);
        //        return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public async Task<T> SaveManiFest<T>(int spType, master_inbound_manifest projectRequirement, string createdBy)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType", spType),
                    new SqlParameter("@auto_id", projectRequirement.auto_id),
                    new SqlParameter("@blno", projectRequirement.blno),
                    new SqlParameter("@shipper_name", projectRequirement.shipper_name),
                    new SqlParameter("@consignee", projectRequirement.consignee),
                    new SqlParameter("@notify", projectRequirement.notify),
                    new SqlParameter("@marks_and_nos_name", projectRequirement.marks_and_nos_name),
                    new SqlParameter("@quantity_and_kind_of_cargo_name", projectRequirement.quantity_and_kind_of_cargo_name),
                    new SqlParameter("@quantity", projectRequirement.quantity),
                    new SqlParameter("@cargotype_desc", projectRequirement.cargotype_desc),
                    new SqlParameter("@is_active", projectRequirement.is_active),
                    new SqlParameter("@created_by", projectRequirement.created_by),
                    new SqlParameter("@is_cleanonBoard", projectRequirement.is_cleanonBoard),
                    new SqlParameter("@cargo_type_name", projectRequirement.cargo_type_name),
                    new SqlParameter("@qtt_name", projectRequirement.qtt_name),
                    };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundManifest, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<T> GetManiFestById<T>(int spType, string auto_id)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@auto_id",auto_id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundManifest, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<master_inbound_manifest>> GetManiFestByAutoId<T>(int spType, string auto_id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@auto_id", auto_id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundManifest, parameters);
            return CommonUtility.ConvertDataTableToList<master_inbound_manifest>(db_result.Tables[0]);
        }
        public async Task<T> GetRow<T>(int spType, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundRequirement, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<CommonDbResponse> UpdateAll<T>(int spType, master_inbound_requirement request, string created_by)
        {
            SqlParameter[] parameter =
           {
                    new SqlParameter("@spType", spType),
                    new SqlParameter("@mastername", request.mastername),
                    new SqlParameter("@mastercontactnumber", request.mastercontactnumber),
                    new SqlParameter("@masteremail", request.masteremail),
                    new SqlParameter("@owners_name", request.owners_name),
                    new SqlParameter("@receiver_name", request.receiver_name),
                    new SqlParameter("@surveyor_name", request.surveyor_name),
                    new SqlParameter("@stevedore_name", request.stevedore_name),
                    new SqlParameter("@shipping_name", request.shipping_name),
                    new SqlParameter("@checker_name", request.checker_name),
                    new SqlParameter("@auto_id", request.auto_id),
                    new SqlParameter("@vessel_name", request.vessel_name),
                    new SqlParameter("@discharge_port", request.discharge_port),
                    new SqlParameter("@EAT_Date", request.EAT_Date),
                    new SqlParameter("@EAT_HH", request.EAT_HH),
                    new SqlParameter("@EAT_MM", request.EAT_MM),
                    new SqlParameter("@RefNo", request.refNo),
                    new SqlParameter("@Rcn", request.rcn),
                    new SqlParameter("@LoadPort", request.LoadPort),
                    new SqlParameter("@is_active", request.is_active),
                    new SqlParameter("@updated_by", request.created_by),
                    new SqlParameter("@VoyageNo", request.VoyageNo),
                    new SqlParameter("@LastPortofCall", request.LastPortofCall),
                    new SqlParameter("@FC_Stevedore_Name", request.FC_Stevedore_Name),
                    new SqlParameter("@eta_notice", Convert.ToDateTime(request.eta_notice)),
                    new SqlParameter("@EAT_HH_eta_notice", Convert.ToInt32(request.EAT_HH_eta_notice)),
                    new SqlParameter("@EAT_MM_eta_notice", Convert.ToInt32(request.EAT_MM_eta_notice)),
                    new SqlParameter("@EAT_HH_commence_to_discharge_cargo",Convert.ToInt32(request.EAT_HH_commence_to_discharge_cargo)),
                    new SqlParameter("@commence_to_discharge_cargo", Convert.ToDateTime(request.commence_to_discharge_cargo)),
                    new SqlParameter("@EAT_MM_commence_to_discharge_cargo", Convert.ToInt32(request.EAT_MM_commence_to_discharge_cargo)),
                    new SqlParameter("@est_to_complete_loading_cargo", Convert.ToDateTime(request.est_to_complete_loading_cargo)),
                    new SqlParameter("@plan_of_sailing",Convert.ToDateTime(request.plan_of_sailing)),
                    new SqlParameter("@vessels_stay",Convert.ToInt32(request.vessels_stay)),
                    new SqlParameter("@lat",request.lat),
                    new SqlParameter("@longt",request.longt),
                     new SqlParameter("@nor_notice",Convert.ToDateTime(request.nor_notice)),
                    new SqlParameter("@EAT_HH_nor_notice",Convert.ToInt32(request.EAT_HH_nor_notice)),
                    new SqlParameter("@EAT_MM_nor_notice",Convert.ToInt32(request.EAT_MM_nor_notice)),

                };
            try
            {
                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundRequirement, parameter);

                return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public async Task<T> GetRowManiFest<T>(int spType, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundManifest, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<T> UpdateManiFest<T>(int spType, master_inbound_manifest projectRequirement, string createdBy)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType", spType),
                    new SqlParameter("@auto_id", projectRequirement.auto_id),
                    new SqlParameter("@blno", projectRequirement.blno),
                    new SqlParameter("@shipper_name", projectRequirement.shipper_name),
                    new SqlParameter("@consignee", projectRequirement.consignee),
                    new SqlParameter("@notify", projectRequirement.notify),
                    new SqlParameter("@marks_and_nos_name", projectRequirement.marks_and_nos_name),
                    new SqlParameter("@quantity_and_kind_of_cargo_name", projectRequirement.quantity_and_kind_of_cargo_name),
                    new SqlParameter("@quantity", projectRequirement.quantity),
                    new SqlParameter("@cargotype_desc", projectRequirement.cargotype_desc),
                    new SqlParameter("@is_active", projectRequirement.is_active),
                    new SqlParameter("@created_by", projectRequirement.created_by),
                    new SqlParameter("@id", projectRequirement.id),
                    new SqlParameter("@is_cleanonBoard", projectRequirement.is_cleanonBoard),
                    new SqlParameter("@cargo_type_name", projectRequirement.cargo_type_name),
                    new SqlParameter("@qtt_name", projectRequirement.qtt_name),

                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundManifest, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", ManageInboundManiFest_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundManifest, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Delete_InBound(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", ManageInboundManiFest_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundRequirement, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Approved(string auto_id, string user_name, Guid? id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", ManageInboundRequirement_Type.approved), new SqlParameter("@auto_id", auto_id), new SqlParameter("@id", id), new SqlParameter("@created_by", user_name), new SqlParameter("@is_approved", true) };
                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundRequirement, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetApprovalData<T>(int spType, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundRequirement, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<List<master_inbound_manifest>> GetCargoMenifestById<T>(int spType, string auto_id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@auto_id", auto_id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageInboundManifest, parameters);
            return CommonUtility.ConvertDataTableToList<master_inbound_manifest>(db_result.Tables[0]);
        }
        public async Task<T> GetStowagePlan_VesselDetails<T>(int spType, string proc, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@Id", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr,proc, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<CommonDbResponse> Save_Stowage_info<T>(int sp_Type, vm_Stowage_Plan_Info request)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType", sp_Type),
                    new SqlParameter("@auto_id", request.auto_id), // Generate a new GUID for auto_id
                    new SqlParameter("@id_ref", request.id_ref),
                    new SqlParameter("@destination", request.destination),
                    new SqlParameter("@hold", request.hold),
                    new SqlParameter("@holdQuantity", request.holdQuantity),
                    new SqlParameter("@marks_and_nos_name", request.marks_and_nos_name),
                    new SqlParameter("@cargo_type_name", request.cargo_type_name),
                    new SqlParameter("@receiver_name", request.receiver_name),
                    new SqlParameter("@otherinfo", request.otherinfo),
                    new SqlParameter("@is_active", true), // Assuming is_active should be set to true
                    new SqlParameter("@created_by", request.created_by),
                };

                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_GetStowagePlan, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }    
        public async Task<List<vm_Stowage_Plan_Info>> GetAllStowage_info<T>(int spType, string auto_id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_StowagePlan.GetPlanInfo), new SqlParameter("@auto_id",auto_id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_GetStowagePlan, parameters);
            return CommonUtility.ConvertDataTableToList<vm_Stowage_Plan_Info>(db_result.Tables[0]);
        }

        public async Task<T> GetRow_StowagePlanInfo<T>(int spType, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@Id", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_GetStowagePlan, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<CommonDbResponse> Update_Stowage_info<T>(int sp_Type, vm_Stowage_Plan_Info request)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType", sp_Type),
                    new SqlParameter("@auto_id", request.auto_id), // Generate a new GUID for auto_id
                    new SqlParameter("@id_ref", request.id_ref),
                    new SqlParameter("@destination", request.destination),
                    new SqlParameter("@hold", request.hold),
                    new SqlParameter("@holdQuantity", request.holdQuantity),
                    new SqlParameter("@marks_and_nos_name", request.marks_and_nos_name),
                    new SqlParameter("@cargo_type_name", request.cargo_type_name),
                    new SqlParameter("@receiver_name", request.receiver_name),
                    new SqlParameter("@otherinfo", request.otherinfo),
                    new SqlParameter("@is_active", true), // Assuming is_active should be set to true
                    new SqlParameter("@created_by", request.created_by),
                    new SqlParameter("@Id", request.id),
                };

                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_GetStowagePlan, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<CommonDbResponse> DeleteStowsInfo(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_StowagePlan.Delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_GetStowagePlan, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Stowage_Main_Info<T>(int sp_Type, vm_Stowage_Main_Info request)
        {
            try
            {
            SqlParameter[] parameters = {
            new SqlParameter("@spType", sp_Type),
            new SqlParameter("@id", request.id),  // You'll need the 'id' parameter to identify the row to update
            new SqlParameter("@auto_id", request.auto_id),
            new SqlParameter("@vesselname", request.vesselname),
            new SqlParameter("@loabeamdeapth", request.loabeamdeapth),
            new SqlParameter("@capacities", request.capacities),
            new SqlParameter("@deadweight", request.deadweight),
            new SqlParameter("@arrival_date",ConvertToSqlDateFormat( request.arrival_date)),
            new SqlParameter("@sailedon_date", ConvertToSqlDateFormat(request.sailedon_date)),
            new SqlParameter("@updated_by", request.created_by),
            new SqlParameter("@updated_date", request.updated_date),
            new SqlParameter("@id_ref", request.id_ref),
        };

                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_GetStowagePlan, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<T> GetRowPlanInfo<T>(int getPlainInfo, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", usp_StowagePlan.GetPlainInfo), new SqlParameter("@id_ref", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_GetStowagePlan, parameters);
                if(dbResult.Tables[0].Rows.Count >0)
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
                else
                    return default(T);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        //public Task<T> GetRowPlanInfo<T>(int getPlainInfo, Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<CommonDbResponse> ApprovePlanInfo(Guid id, string auto_id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_StowagePlan.approved), new SqlParameter("@id_ref", id), new SqlParameter("@auto_id", auto_id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_GetStowagePlan, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);

        }

        public async Task<T> GetArrivalCondition_MainInfo<T>(int spType, Guid id) where T : new()
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@Id", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Arrival_sailing, parameters);

                if (dbResult.Tables[0].Rows.Count > 0)
                    return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
                else
                    return new T(); // Return a new instance of the class with a parameterless constructor
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> GetDepartureCondition_MainInfo<T>(int spType, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id_ref", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDepartureSailingCondition, parameters);
                if(dbResult.Tables[0].Rows.Count>0)
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
                else
                    return default;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<CommonDbResponse> Save_ArrivalSailingCondition<T>(int sp_Type, vm_ArrivalCondition request)
        {
            try
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@spType", sp_Type),
                new SqlParameter("@auto_id", request.auto_id), // Generate a new GUID for auto_id
                new SqlParameter("@id", request.id),
                new SqlParameter("@id_ref", request.id_ref),
                new SqlParameter("@vesselname", request.vessel_name),
                new SqlParameter("@flags", request.flags),
                new SqlParameter("@type_of_cargo", request.type_of_cargo),
                new SqlParameter("@gross_weight_of_cargo",request.gross_weight_of_cargo),
                new SqlParameter("@qtt_name", request.qtt_name),
                new SqlParameter("@readiness_tendered", request.readiness_tendered),
                new SqlParameter("@readiness_accepted", request.readiness_accepted),
                new SqlParameter("@arrival_at", request.arrival_at),
                new SqlParameter("@pilot_on_board_arrival",ConvertToSqlDateFormat( request.pilot_on_board_arrival)),
                new SqlParameter("@dropped_anchor", ConvertToSqlDateFormat(request.dropped_anchor)),
                //new SqlParameter("@bunker_rob_on_arrival", request.bunker_rob_on_arrival),
                new SqlParameter("@commenced_discharge_cargo", ConvertToSqlDateFormat(request.commenced_discharge_cargo)),
                new SqlParameter("@completed_discharge_cargo", ConvertToSqlDateFormat(request.completed_discharge_cargo)),
                new SqlParameter("@lat", request.lat),
                new SqlParameter("@longi", request.longt),



                 new SqlParameter("@fwd", request.fwd),
                new SqlParameter("@aft", request.aft),
                 new SqlParameter("@fo", request.fo),
                new SqlParameter("@doo", request.doo),
                 new SqlParameter("@fw", request.fw),

                //new SqlParameter("@pilot_on_board_departure", request.pilot_on_board_departure),
                //new SqlParameter("@departue_from", request.departue_from),
                //new SqlParameter("@draft_on_departure", request.draft_on_departure),
                //new SqlParameter("@bunker_rob_on_departure", request.bunker_rob_on_departure),
                //new SqlParameter("@bunker_fuel_oil", request.bunker_fuel_oil),
                //new SqlParameter("@bunker_diesel_oil", request.bunker_diesel_oil),
                //new SqlParameter("@bunker_fresh_water", request.bunker_fresh_water),
                //new SqlParameter("@bunker_eta_next_port", request.bunker_eta_next_port),
                //new SqlParameter("@other_watch_man", request.other_watch_man),
                //new SqlParameter("@other_police_man", request.other_police_man),
                //new SqlParameter("@other_cash_advance", request.other_cash_advance),
                //new SqlParameter("@EAT_HH_pilot_on_board_departure", request.EAT_HH_pilot_on_board_departure),
                //new SqlParameter("@EAT_MM_pilot_on_board_departure", request.EAT_MM_pilot_on_board_departure),
                //new SqlParameter("@EAT_HH_departure_from", request.EAT_HH_departure_from),
                //new SqlParameter("@EAT_MM_departure_from", request.EAT_MM_departure_from),
                
                 new SqlParameter("@EAT_HH_pilot_on_board_arrival",Convert.ToInt32(request.EAT_HH_pilot_on_board_arrival)),
                new SqlParameter("@EAT_MM_pilot_on_board_arrival", Convert.ToInt32(request.EAT_MM_pilot_on_board_arrival)),
                new SqlParameter("@EAT_HH_dropped_anchor", Convert.ToInt32(request.EAT_HH_dropped_anchor)),
                new SqlParameter("@EAT_MM_dropped_anchor", Convert.ToInt32(request.EAT_MM_dropped_anchor)),
                new SqlParameter("@EAT_HH_commenced_discharge_cargo", Convert.ToInt32(request.EAT_HH_commenced_discharge_cargo)),
                new SqlParameter("@EAT_MM_commenced_discharge_cargo", Convert.ToInt32(request.EAT_MM_commenced_discharge_cargo)),
                new SqlParameter("@EAT_HH_completed_discharge_cargo", Convert.ToInt32(request.EAT_HH_completed_discharge_cargo)),
                new SqlParameter("@EAT_MM_completed_discharge_cargo", Convert.ToInt32(request.EAT_MM_completed_discharge_cargo)),
                
                new SqlParameter("@created_by", request.created_by),
                new SqlParameter("@updated_by", request.created_by),
               // new SqlParameter("@updated_date", request.updated_date),
                //new SqlParameter("@created_date", request.created_date),
                //new SqlParameter("@approved", request.approved),
                new SqlParameter("@is_active", request.is_active),
                //new SqlParameter("@is_deleted", request.is_deleted),
            };

                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageArrivalSailingCondition, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<T> GetRowArrivalInfo<T>(int getPlainInfo, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", getPlainInfo), new SqlParameter("@id_ref", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageArrivalSailingCondition, parameters);
                if (dbResult.Tables[0].Rows.Count > 0)
                    return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
                else
                    return default(T);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<T> GetRowDepartureInfo<T>(int getPlainInfo, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", getPlainInfo), new SqlParameter("@id_ref", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDepartureSailingCondition, parameters);
                if (dbResult.Tables[0].Rows.Count > 0)
                    return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
                else
                    return default(T);
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public async Task<CommonDbResponse> Approved_Arrival(string auto_id, string user_name, Guid? id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", usp_Arrival_Condition.approved), new SqlParameter("@auto_id", auto_id), new SqlParameter("@id_ref", id), new SqlParameter("@created_by", user_name), new SqlParameter("@approve", true) };
                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageArrivalSailingCondition, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CommonDbResponse> Approved_Departure(string auto_id, string user_name, Guid? id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", usp_Arrival_Condition.approved), new SqlParameter("@auto_id", auto_id), new SqlParameter("@id_ref", id), new SqlParameter("@created_by", user_name), new SqlParameter("@approve", true) };
                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDepartureSailingCondition, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Add other methods as needed based on your requirements
        // ...
        public async Task<CommonDbResponse> Save_DepartureSailingCondition<T>(int sp_Type, vm_ArrivalCondition request)
        {
            try
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@spType", sp_Type),
                new SqlParameter("@auto_id", request.auto_id), // Generate a new GUID for auto_id
                new SqlParameter("@id", request.id),
                new SqlParameter("@id_ref", request.id_ref),
                new SqlParameter("@vesselname", request.vessel_name),
                new SqlParameter("@flags", request.flags),
                new SqlParameter("@type_of_cargo", request.type_of_cargo),
                new SqlParameter("@gross_weight_of_cargo",request.gross_weight_of_cargo),
                new SqlParameter("@qtt_name", request.qtt_name),
                new SqlParameter("@pilot_on_board_departure",ConvertToSqlDateFormat( request.pilot_on_board_departure)),
                new SqlParameter("@departue_from", ConvertToSqlDateFormat(request.departue_from)),
                new SqlParameter("@draft_on_departure", request.draft_on_departure),
                new SqlParameter("@bunker_rob_on_departure", request.bunker_rob_on_departure),
                new SqlParameter("@bunker_fuel_oil", request.bunker_fuel_oil),
                new SqlParameter("@bunker_diesel_oil", request.bunker_diesel_oil),
                new SqlParameter("@bunker_fresh_water", request.bunker_fresh_water),
                new SqlParameter("@bunker_eta_next_port", request.bunker_eta_next_port),
                new SqlParameter("@other_watch_man", request.other_watch_man),
                new SqlParameter("@other_police_man", request.other_police_man),
                new SqlParameter("@other_cash_advance", request.other_cash_advance),
                new SqlParameter("@EAT_HH_pilot_on_board_departure", request.EAT_HH_pilot_on_board_departure),
                new SqlParameter("@EAT_MM_pilot_on_board_departure", request.EAT_MM_pilot_on_board_departure),
                new SqlParameter("@EAT_HH_departure_from", request.EAT_HH_departure_from),
                new SqlParameter("@EAT_MM_departure_from", request.EAT_MM_departure_from),
                new SqlParameter("@created_by", request.created_by),
                new SqlParameter("@updated_by", request.created_by),
                new SqlParameter("@is_active", request.is_active),
                new SqlParameter("@fwd", request.fwd),
                new SqlParameter("@aft", request.aft),
                new SqlParameter("@fo", request.fo),
                new SqlParameter("@doo", request.doo),
                new SqlParameter("@fw", request.fw),
                new SqlParameter("@departue_from_port_name",request.departue_from_port_name),
                 new SqlParameter("@ETA_Next_Port_Date",ConvertToSqlDateFormat(request.ETA_Next_Port_Date)),
                  new SqlParameter("@ETA_Next_Port_MM",request.ETA_Next_Port_MM),
                   new SqlParameter("@ETA_Next_Port_HH",request.ETA_Next_Port_HH),
                    new SqlParameter("@ETA_Next_Port_AMPM",request.ETA_Next_Port_AMPM)

            };

                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDepartureSailingCondition, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<CommonDbResponse> Save_CargoDayStatus<T>(int sp_Type, CargoStatusOfDay cargoStatus)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType", sp_Type),
                    new SqlParameter("@id", cargoStatus.id),
                    new SqlParameter("@id_ref", cargoStatus.id_ref),
                    new SqlParameter("@auto_id", cargoStatus.auto_id),

                    new SqlParameter("@selectTime", cargoStatus.selectTime),
                    new SqlParameter("@from_HH_cargo_status_daytime", cargoStatus.from_HH_cargo_status_daytime),
                    new SqlParameter("@to_MM_cargo_status_daytime", cargoStatus.to_MM_cargo_status_daytime),
                    new SqlParameter("@from_HH_cargo_status_first", cargoStatus.from_HH_cargo_status_first),
                    new SqlParameter("@to_MM_cargo_status_first", cargoStatus.to_MM_cargo_status_first),
                    new SqlParameter("@gang", cargoStatus.gang),
                    new SqlParameter("@hold", cargoStatus.hold),
                    new SqlParameter("@total_out",Convert.ToDecimal(cargoStatus.total_out)),
                    new SqlParameter("@created_by", cargoStatus.created_by),
                    new SqlParameter("@report_date",Convert.ToDateTime(cargoStatus.report_date)),
                    new SqlParameter("@HH_report_date", cargoStatus.HH_report_date),
                    new SqlParameter("@MM_report_date", cargoStatus.MM_report_date),
                    };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoStatusOfDay, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<CargoStatusOfDay>> GetGargoByID<T>(int spType, string auto_id, String report_date, string HH_report_date, string MM_report_date)
        {
            try
            {
                SqlParameter[] parameters = {
                new SqlParameter("@auto_id", auto_id),
                new SqlParameter("@spType", spType),
                new SqlParameter("@report_date",Convert.ToDateTime(report_date)),
                new SqlParameter("@HH_report_date",Convert.ToInt32( HH_report_date)),
                new SqlParameter("@MM_report_date", Convert.ToInt32(MM_report_date))
            };
                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoStatusOfDay, parameters);
                return CommonUtility.ConvertDataTableToList<CargoStatusOfDay>(db_result.Tables[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<T> Edit_CargoStatusDay<T>(int spType, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoStatusOfDay, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<T> UpdateCargoStatusDay<T>(int spType, CargoStatusOfDay projectRequirement, string createdBy)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType", spType),
                    new SqlParameter("@auto_id", projectRequirement.auto_id),
                    new SqlParameter("@selectTime", projectRequirement.selectTime),
                    new SqlParameter("@from_HH_cargo_status_daytime", projectRequirement.from_HH_cargo_status_daytime),
                    new SqlParameter("@to_MM_cargo_status_daytime", projectRequirement.to_MM_cargo_status_daytime),
                    new SqlParameter("@from_HH_cargo_status_first", projectRequirement.from_HH_cargo_status_first),
                    new SqlParameter("@to_MM_cargo_status_first", projectRequirement.to_MM_cargo_status_first),
                    new SqlParameter("@gang", projectRequirement.gang),
                    new SqlParameter("@hold", projectRequirement.hold),
                    new SqlParameter("@total_out", projectRequirement.total_out),
                    new SqlParameter("@id", projectRequirement.id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoStatusOfDay, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<CommonDbResponse> Delete_CargoStatusDay(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_CargoStausDay.del_by_status_id), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoStatusOfDay, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save_Remarks<T>(int sp_Type, cargo_status_of_day_remarks cargoStatus)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType", sp_Type),
                    new SqlParameter("@id_ref", cargoStatus.id_ref),
                    new SqlParameter("@auto_id", cargoStatus.auto_id),
                    new SqlParameter("@date_of_action",ConvertToSqlDateFormat(cargoStatus.date_of_action)),
                    new SqlParameter("@HH_date_of_action", cargoStatus.HH_date_of_action),
                    new SqlParameter("@MM_date_of_action", cargoStatus.MM_date_of_action),
                    new SqlParameter("@remarks_comments", cargoStatus.remarks_comments),
                    new SqlParameter("@created_by", cargoStatus.created_by),
                    new SqlParameter("@to_HH_date_of_action", cargoStatus.to_HH_date_of_action),
                    new SqlParameter("@to_MM_date_of_action", cargoStatus.to_MM_date_of_action),
                    new SqlParameter("@report_date",ConvertToSqlDateFormat(cargoStatus.report_date)),
                    new SqlParameter("@HH_report_date", cargoStatus.HH_report_date),
                    new SqlParameter("@MM_report_date", cargoStatus.MM_report_date),
                    };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoStatusOfDayRemarks, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<cargo_status_of_day_remarks>> GetRemarksDate<T>(int spType, Guid id, string auto_id, string report_date)
        {
            try
            {
                DateTime parsedDate;

                if (DateTime.TryParseExact(report_date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    // Parsing successful, now you can format it as needed for the SqlParameter
                    string formattedDate = parsedDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

                    SqlParameter[] parameters = {
                    new SqlParameter("@spType", spType),
                    new SqlParameter("@auto_id", auto_id),
                    new SqlParameter("@id_ref", id),
                    new SqlParameter("@report_date", formattedDate)
                };
                    var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoStatusOfDayRemarks, parameters);

                    return CommonUtility.ConvertDataTableToList<cargo_status_of_day_remarks>(db_result.Tables[0]);
                }
                else {
                    return new List<cargo_status_of_day_remarks>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<cargo_status_of_day_remarks>> GetRemarks<T>(int spType, Guid id, string auto_id, string date_of_action)
        {
            DateTime parsedDate;

            if (DateTime.TryParseExact(date_of_action, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                string formattedDate = parsedDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@auto_id", auto_id), new SqlParameter("@id_ref", id), new SqlParameter("@date_of_action", formattedDate) };
                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoStatusOfDayRemarks, parameters);
                return CommonUtility.ConvertDataTableToList<cargo_status_of_day_remarks>(db_result.Tables[0]);
            }
            else {
                return new List<cargo_status_of_day_remarks>();
            }

        }
        public async Task<CommonDbResponse> InsertDailyReport<T>(int spType, master_daily_report dailyReport)
        {
            try
            {
                SqlParameter[] parameters =
        {
            new SqlParameter("@spType", spType),
            new SqlParameter("@id_ref", dailyReport.id_ref),
            new SqlParameter("@auto_id", dailyReport.auto_id),
            new SqlParameter("@vessel_name", dailyReport.vessel_name),
            new SqlParameter("@flags", dailyReport.flags),
            new SqlParameter("@discharge_port_name", dailyReport.discharge_port_name),
            new SqlParameter("@lat", dailyReport.lat),
            new SqlParameter("@longt", dailyReport.longt),
            new SqlParameter("@weather", dailyReport.weather),
            new SqlParameter("@report_date", ConvertToSqlDateFormat(dailyReport.report_date)),
            new SqlParameter("@HH_report_date", dailyReport.HH_report_date),
            new SqlParameter("@MM_report_date", dailyReport.MM_report_date),
            new SqlParameter("@from_daily_discharge_cargo", ConvertToSqlDateFormat(dailyReport.from_daily_discharge_cargo)),
            new SqlParameter("@from_HH_daily_discharge_cargo", dailyReport.from_HH_daily_discharge_cargo),
            new SqlParameter("@from_MM_daily_discharge_cargo", dailyReport.from_MM_daily_discharge_cargo),
            new SqlParameter("@to_daily_discharge_cargo", ConvertToSqlDateFormat(dailyReport.to_daily_discharge_cargo)),
            new SqlParameter("@to_HH_daily_discharge_cargo", dailyReport.to_HH_daily_discharge_cargo),
            new SqlParameter("@to_MM_daily_discharge_cargo", dailyReport.to_MM_daily_discharge_cargo),
            new SqlParameter("@fwt", dailyReport.fwt),
            new SqlParameter("@ft", dailyReport.ft),
            new SqlParameter("@fo", dailyReport.fo),
            new SqlParameter("@doo", dailyReport.doo),
            new SqlParameter("@fw", dailyReport.fw),
            new SqlParameter("@created_by", dailyReport.created_by),

            // New parameters
            new SqlParameter("@loading_text", dailyReport.loading_text),
            new SqlParameter("@loading_text_date", dailyReport.loading_text_date),
            new SqlParameter("@from_HH_loading_text", dailyReport.from_HH_loading_text),
            new SqlParameter("@to_MM_loading_text", dailyReport.to_MM_loading_text),
            new SqlParameter("@loading_text_until", dailyReport.loading_text_until),

            new SqlParameter("@comp_loading", dailyReport.comp_loading),
            new SqlParameter("@from_HH_comp_loading", dailyReport.from_HH_comp_loading),
            new SqlParameter("@from_MM_comp_loading", dailyReport.from_MM_comp_loading),
            new SqlParameter("@to_comp_loading", dailyReport.to_comp_loading),
            new SqlParameter("@to_HH_comp_loading", dailyReport.to_HH_comp_loading),
            new SqlParameter("@to_MM_comp_loading", dailyReport.to_MM_comp_loading),

            new SqlParameter("@comm_loading", dailyReport.comm_loading),
            new SqlParameter("@from_HH_comm_loading", dailyReport.from_HH_comm_loading),
            new SqlParameter("@from_MM_comm_loading", dailyReport.from_MM_comm_loading),
            new SqlParameter("@to_comm_loading", dailyReport.to_comm_loading),
            new SqlParameter("@to_HH_comm_loading", dailyReport.to_HH_comm_loading),
            new SqlParameter("@to_MM_comm_loading", dailyReport.to_MM_comm_loading),

            new SqlParameter("@hold_1_total", dailyReport.hold_1_total),
            new SqlParameter("@hold_2_total", dailyReport.hold_2_total),
            new SqlParameter("@hold_3_total", dailyReport.hold_3_total),
            new SqlParameter("@hold_4_total", dailyReport.hold_4_total),
            new SqlParameter("@hold_5_total", dailyReport.hold_5_total),

            new SqlParameter("@from_HH_daytime", dailyReport.from_HH_daytime),
            new SqlParameter("@from_MM_daytime", dailyReport.from_MM_daytime),
            new SqlParameter("@to_HH_daytime", dailyReport.to_HH_daytime),
            new SqlParameter("@to_MM_daytime", dailyReport.to_MM_daytime),

            new SqlParameter("@gang_daytime", dailyReport.gang_daytime),
            new SqlParameter("@daytime_hold1_out", dailyReport.daytime_hold1_out),
            new SqlParameter("@daytime_hold2_out", dailyReport.daytime_hold2_out),
            new SqlParameter("@daytime_hold3_out", dailyReport.daytime_hold3_out),
            new SqlParameter("@daytime_hold4_out", dailyReport.daytime_hold4_out),
            new SqlParameter("@daytime_hold5_out", dailyReport.daytime_hold5_out),
            new SqlParameter("@daytime_total_out", dailyReport.daytime_total_out),

            new SqlParameter("@from_HH_first", dailyReport.from_HH_first),
            new SqlParameter("@from_MM_first", dailyReport.from_MM_first),
            new SqlParameter("@to_HH_first", dailyReport.to_HH_first),
            new SqlParameter("@to_MM_first", dailyReport.to_MM_first),

            new SqlParameter("@gang_first", dailyReport.gang_first),
            new SqlParameter("@first_hold1_out", dailyReport.first_hold1_out),
            new SqlParameter("@first_hold2_out", dailyReport.first_hold2_out),
            new SqlParameter("@first_hold3_out", dailyReport.first_hold3_out),
            new SqlParameter("@first_hold4_out", dailyReport.first_hold4_out),
            new SqlParameter("@first_hold5_out", dailyReport.first_hold5_out),
            new SqlParameter("@first_total_out", dailyReport.first_total_out),

            new SqlParameter("@from_HH_second", dailyReport.from_HH_second),
            new SqlParameter("@from_MM_second", dailyReport.from_MM_second),
            new SqlParameter("@to_HH_second", dailyReport.to_HH_second),
            new SqlParameter("@to_MM_second", dailyReport.to_MM_second),

            new SqlParameter("@gang_second", dailyReport.gang_second),
            new SqlParameter("@second_hold1_out", dailyReport.second_hold1_out),
            new SqlParameter("@second_hold2_out", dailyReport.second_hold2_out),
            new SqlParameter("@second_hold3_out", dailyReport.second_hold3_out),
            new SqlParameter("@second_hold4_out", dailyReport.second_hold4_out),
            new SqlParameter("@second_hold5_out", dailyReport.second_hold5_out),
            new SqlParameter("@second_total_out", dailyReport.second_total_out),

            new SqlParameter("@gang_total", dailyReport.gang_total),
            new SqlParameter("@total_hold1_out", dailyReport.total_hold1_out),
            new SqlParameter("@total_hold2_out", dailyReport.total_hold2_out),
            new SqlParameter("@total_hold3_out", dailyReport.total_hold3_out),
            new SqlParameter("@total_hold4_out", dailyReport.total_hold4_out),
            new SqlParameter("@total_hold5_out", dailyReport.total_hold5_out),
            new SqlParameter("@total_total", dailyReport.total_total),

            new SqlParameter("@gang_previous", dailyReport.gang_previous),
            new SqlParameter("@previous_hold1_out", dailyReport.previous_hold1_out),
            new SqlParameter("@previous_hold2_out", dailyReport.previous_hold2_out),
            new SqlParameter("@previous_hold3_out", dailyReport.previous_hold3_out),
            new SqlParameter("@previous_hold4_out", dailyReport.previous_hold4_out),
            new SqlParameter("@previous_hold5_out", dailyReport.previous_hold5_out),
            new SqlParameter("@previous_total", dailyReport.previous_total),

            new SqlParameter("@gang_grand_total", dailyReport.gang_grand_total),
            new SqlParameter("@grand_hold1_out", dailyReport.grand_hold1_out),
            new SqlParameter("@grand_hold2_out", dailyReport.grand_hold2_out),
            new SqlParameter("@grand_hold3_out", dailyReport.grand_hold3_out),
            new SqlParameter("@grand_hold4_out", dailyReport.grand_hold4_out),
            new SqlParameter("@grand_hold5_out", dailyReport.grand_hold5_out),
            new SqlParameter("@grand_total", dailyReport.grand_total),

            new SqlParameter("@balance_cargo_hold1", dailyReport.balance_cargo_hold1),
            new SqlParameter("@balance_cargo_hold2", dailyReport.balance_cargo_hold2),
            new SqlParameter("@balance_cargo_hold3", dailyReport.balance_cargo_hold3),
            new SqlParameter("@balance_cargo_hold4", dailyReport.balance_cargo_hold4),
            new SqlParameter("@balance_cargo_hold5", dailyReport.balance_cargo_hold5),
            new SqlParameter("@balance_total", dailyReport.balance_total),

            new SqlParameter("@working_time", dailyReport.working_time),
            new SqlParameter("@working_through", dailyReport.working_through),
        };

                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDailyReport, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<CommonDbResponse> UpdateDailyReport<T>(int spType, master_daily_report dailyReport)
        {
            try
            {
                SqlParameter[] parameters =
        {
            new SqlParameter("@spType", spType),
            new SqlParameter("@id", dailyReport.id),
            new SqlParameter("@auto_id", dailyReport.auto_id),
            new SqlParameter("@vessel_name", dailyReport.vessel_name),
            new SqlParameter("@flags", dailyReport.flags),
            new SqlParameter("@discharge_port_name", dailyReport.discharge_port_name),
            new SqlParameter("@lat", dailyReport.lat),
            new SqlParameter("@longt", dailyReport.longt),
            new SqlParameter("@weather", dailyReport.weather),
            new SqlParameter("@report_date", ConvertToSqlDateFormat(dailyReport.report_date)),
            new SqlParameter("@HH_report_date", dailyReport.HH_report_date),
            new SqlParameter("@MM_report_date", dailyReport.MM_report_date),
            new SqlParameter("@from_daily_discharge_cargo", ConvertToSqlDateFormat(dailyReport.from_daily_discharge_cargo)),
            new SqlParameter("@from_HH_daily_discharge_cargo", dailyReport.from_HH_daily_discharge_cargo),
            new SqlParameter("@from_MM_daily_discharge_cargo", dailyReport.from_MM_daily_discharge_cargo),
            new SqlParameter("@to_daily_discharge_cargo", ConvertToSqlDateFormat(dailyReport.to_daily_discharge_cargo)),
            new SqlParameter("@to_HH_daily_discharge_cargo", dailyReport.to_HH_daily_discharge_cargo),
            new SqlParameter("@to_MM_daily_discharge_cargo", dailyReport.to_MM_daily_discharge_cargo),
            new SqlParameter("@fwt", dailyReport.fwt),
            new SqlParameter("@ft", dailyReport.ft),
            new SqlParameter("@fo", dailyReport.fo),
            new SqlParameter("@doo", dailyReport.doo),
            new SqlParameter("@fw", dailyReport.fw),
            new SqlParameter("@created_by", dailyReport.created_by),

            // New parameters
            new SqlParameter("@loading_text", dailyReport.loading_text),
            new SqlParameter("@loading_text_date", dailyReport.loading_text_date),
            new SqlParameter("@from_HH_loading_text", dailyReport.from_HH_loading_text),
            new SqlParameter("@to_MM_loading_text", dailyReport.to_MM_loading_text),
            new SqlParameter("@loading_text_until", dailyReport.loading_text_until),

            new SqlParameter("@comp_loading", dailyReport.comp_loading),
            new SqlParameter("@from_HH_comp_loading", dailyReport.from_HH_comp_loading),
            new SqlParameter("@from_MM_comp_loading", dailyReport.from_MM_comp_loading),
            new SqlParameter("@to_comp_loading", dailyReport.to_comp_loading),
            new SqlParameter("@to_HH_comp_loading", dailyReport.to_HH_comp_loading),
            new SqlParameter("@to_MM_comp_loading", dailyReport.to_MM_comp_loading),

            new SqlParameter("@comm_loading", dailyReport.comm_loading),
            new SqlParameter("@from_HH_comm_loading", dailyReport.from_HH_comm_loading),
            new SqlParameter("@from_MM_comm_loading", dailyReport.from_MM_comm_loading),
            new SqlParameter("@to_comm_loading", dailyReport.to_comm_loading),
            new SqlParameter("@to_HH_comm_loading", dailyReport.to_HH_comm_loading),
            new SqlParameter("@to_MM_comm_loading", dailyReport.to_MM_comm_loading),

            new SqlParameter("@hold_1_total", dailyReport.hold_1_total),
            new SqlParameter("@hold_2_total", dailyReport.hold_2_total),
            new SqlParameter("@hold_3_total", dailyReport.hold_3_total),
            new SqlParameter("@hold_4_total", dailyReport.hold_4_total),
            new SqlParameter("@hold_5_total", dailyReport.hold_5_total),

            new SqlParameter("@from_HH_daytime", dailyReport.from_HH_daytime),
            new SqlParameter("@from_MM_daytime", dailyReport.from_MM_daytime),
            new SqlParameter("@to_HH_daytime", dailyReport.to_HH_daytime),
            new SqlParameter("@to_MM_daytime", dailyReport.to_MM_daytime),

            new SqlParameter("@gang_daytime", dailyReport.gang_daytime),
            new SqlParameter("@daytime_hold1_out", dailyReport.daytime_hold1_out),
            new SqlParameter("@daytime_hold2_out", dailyReport.daytime_hold2_out),
            new SqlParameter("@daytime_hold3_out", dailyReport.daytime_hold3_out),
            new SqlParameter("@daytime_hold4_out", dailyReport.daytime_hold4_out),
            new SqlParameter("@daytime_hold5_out", dailyReport.daytime_hold5_out),
            new SqlParameter("@daytime_total_out", dailyReport.daytime_total_out),

            new SqlParameter("@from_HH_first", dailyReport.from_HH_first),
            new SqlParameter("@from_MM_first", dailyReport.from_MM_first),
            new SqlParameter("@to_HH_first", dailyReport.to_HH_first),
            new SqlParameter("@to_MM_first", dailyReport.to_MM_first),

            new SqlParameter("@gang_first", dailyReport.gang_first),
            new SqlParameter("@first_hold1_out", dailyReport.first_hold1_out),
            new SqlParameter("@first_hold2_out", dailyReport.first_hold2_out),
            new SqlParameter("@first_hold3_out", dailyReport.first_hold3_out),
            new SqlParameter("@first_hold4_out", dailyReport.first_hold4_out),
            new SqlParameter("@first_hold5_out", dailyReport.first_hold5_out),
            new SqlParameter("@first_total_out", dailyReport.first_total_out),

            new SqlParameter("@from_HH_second", dailyReport.from_HH_second),
            new SqlParameter("@from_MM_second", dailyReport.from_MM_second),
            new SqlParameter("@to_HH_second", dailyReport.to_HH_second),
            new SqlParameter("@to_MM_second", dailyReport.to_MM_second),

            new SqlParameter("@gang_second", dailyReport.gang_second),
            new SqlParameter("@second_hold1_out", dailyReport.second_hold1_out),
            new SqlParameter("@second_hold2_out", dailyReport.second_hold2_out),
            new SqlParameter("@second_hold3_out", dailyReport.second_hold3_out),
            new SqlParameter("@second_hold4_out", dailyReport.second_hold4_out),
            new SqlParameter("@second_hold5_out", dailyReport.second_hold5_out),
            new SqlParameter("@second_total_out", dailyReport.second_total_out),

            new SqlParameter("@gang_total", dailyReport.gang_total),
            new SqlParameter("@total_hold1_out", dailyReport.total_hold1_out),
            new SqlParameter("@total_hold2_out", dailyReport.total_hold2_out),
            new SqlParameter("@total_hold3_out", dailyReport.total_hold3_out),
            new SqlParameter("@total_hold4_out", dailyReport.total_hold4_out),
            new SqlParameter("@total_hold5_out", dailyReport.total_hold5_out),
            new SqlParameter("@total_total", dailyReport.total_total),

            new SqlParameter("@gang_previous", dailyReport.gang_previous),
            new SqlParameter("@previous_hold1_out", dailyReport.previous_hold1_out),
            new SqlParameter("@previous_hold2_out", dailyReport.previous_hold2_out),
            new SqlParameter("@previous_hold3_out", dailyReport.previous_hold3_out),
            new SqlParameter("@previous_hold4_out", dailyReport.previous_hold4_out),
            new SqlParameter("@previous_hold5_out", dailyReport.previous_hold5_out),
            new SqlParameter("@previous_total", dailyReport.previous_total),

            new SqlParameter("@gang_grand_total", dailyReport.gang_grand_total),
            new SqlParameter("@grand_hold1_out", dailyReport.grand_hold1_out),
            new SqlParameter("@grand_hold2_out", dailyReport.grand_hold2_out),
            new SqlParameter("@grand_hold3_out", dailyReport.grand_hold3_out),
            new SqlParameter("@grand_hold4_out", dailyReport.grand_hold4_out),
            new SqlParameter("@grand_hold5_out", dailyReport.grand_hold5_out),
            new SqlParameter("@grand_total", dailyReport.grand_total),

            new SqlParameter("@balance_cargo_hold1", dailyReport.balance_cargo_hold1),
            new SqlParameter("@balance_cargo_hold2", dailyReport.balance_cargo_hold2),
            new SqlParameter("@balance_cargo_hold3", dailyReport.balance_cargo_hold3),
            new SqlParameter("@balance_cargo_hold4", dailyReport.balance_cargo_hold4),
            new SqlParameter("@balance_cargo_hold5", dailyReport.balance_cargo_hold5),
            new SqlParameter("@balance_total", dailyReport.balance_total),

            new SqlParameter("@working_time", dailyReport.working_time),
            new SqlParameter("@working_through", dailyReport.working_through),
        };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDailyReport, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static string ConvertToSqlDateFormat(string inputDate)
        {
            if (DateTime.TryParseExact(inputDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            else
            {
                // Handle invalid date format gracefully, log or throw an exception
                throw new ArgumentException("Invalid date format.");
            }
        }
        public async Task<List<master_daily_report>> GetAllDailyReports<T>(int spType , Guid id_ref)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_DailyReport.getall),
                new SqlParameter("@id_ref", id_ref),
                //new SqlParameter("@auto_id", auto_id), 
                };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDailyReport, parameters);
            return CommonUtility.ConvertDataTableToList<master_daily_report>(db_result.Tables[0]);
        }
        public async Task<T> GetDailyReportInfo<T>(int spType, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", usp_DailyReport.getforreport),
                new SqlParameter("@id", id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDailyReport, parameters);
                if (dbResult.Tables[0].Rows.Count > 0)
                    return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
                else
                    return default(T);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<T> GetAllPreInfo<T>(int spType, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id_ref", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDailyReport, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<T> UpdateGetAllPreInfo<T>(int spType, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDailyReport, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<T> GetUpdateDailyReportInfo<T>(int spType, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDailyReport, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<CommonDbResponse> Update_Remarks<T>(int sp_Type, cargo_status_of_day_remarks cargoStatus)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType", sp_Type),
                    new SqlParameter("@id_ref", cargoStatus.id_ref),
                    new SqlParameter("@auto_id", cargoStatus.auto_id),
                    new SqlParameter("@date_of_action",Convert.ToDateTime(cargoStatus.date_of_action)),
                    new SqlParameter("@HH_date_of_action", cargoStatus.HH_date_of_action),
                    new SqlParameter("@MM_date_of_action", cargoStatus.MM_date_of_action),
                    new SqlParameter("@remarks_comments", cargoStatus.remarks_comments),
                    new SqlParameter("@created_by", cargoStatus.created_by),
                    new SqlParameter("@to_HH_date_of_action", cargoStatus.to_HH_date_of_action),
                    new SqlParameter("@to_MM_date_of_action", cargoStatus.to_MM_date_of_action),
                    new SqlParameter("@report_date",Convert.ToDateTime(cargoStatus.report_date)),
                    new SqlParameter("@HH_report_date", cargoStatus.HH_report_date),
                    new SqlParameter("@MM_report_date", cargoStatus.MM_report_date),
                    new SqlParameter("@id", cargoStatus.id),

                    };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoStatusOfDayRemarks, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<T> get_Remarks<T>(int spType, cargo_status_of_day_remarks cr)
        {
            try
            {
                SqlParameter[] parameters = 
                    {
                    new SqlParameter("@spType", spType), 
                    new SqlParameter("@id_ref", cr.id) ,
                     new SqlParameter("@auto_id", cr.auto_id)
                    };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoStatusOfDayRemarks, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<CommonDbResponse> DeleteRemarks(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_Cargo_Remarks.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoStatusOfDayRemarks, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> DeleteDailyReport(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_DailyReport.deletedailyreport), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDailyReport, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<T> GetAllPreviousInfo<T>(int spType, Guid id_ref, string report_date)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id_ref", id_ref), new SqlParameter("@report_date", report_date) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDailyReport, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<T> GetAllPreviousInfo_by_auto_id<T>(int spType, string auto_id, string report_date)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@auto_id", auto_id), new SqlParameter("@report_date", report_date) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDailyReport, parameters);
                if (dbResult.Tables[0].Rows.Count > 0)
                {
                    return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
                }
                else
                {
                    return default(T); 
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<T> save_sof<T>(int spType, sof inboundSOF, string createdBy)
        {
            try
            {
                SqlParameter[] parameters =
                {
            new SqlParameter("@spType", spType),
            new SqlParameter("@auto_id", inboundSOF.auto_id),
            new SqlParameter("@id", inboundSOF.id),
            new SqlParameter("@id_ref", inboundSOF.id_ref),
            new SqlParameter("@departure_from_port", inboundSOF.departure_from_port),
            new SqlParameter("@departure_date", Convert.ToDateTime(inboundSOF.departure_date)),
            new SqlParameter("@departure_date_HH", inboundSOF.departure_date_HH),
            new SqlParameter("@departure_date_MM", inboundSOF.departure_date_MM),
            new SqlParameter("@ETA_Next_Port_Name", inboundSOF.ETA_Next_Port_Name),
            new SqlParameter("@ETA_Next_Port_Date",Convert.ToDateTime(inboundSOF.ETA_Next_Port_Date)),
            new SqlParameter("@ETA_Next_Port_MM", inboundSOF.ETA_Next_Port_MM),
            new SqlParameter("@ETA_Next_Port_HH", inboundSOF.ETA_Next_Port_HH),
            new SqlParameter("@ETA_Next_Port_AMPM", inboundSOF.ETA_Next_Port_AMPM),
            new SqlParameter("@created_by", createdBy),
        };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_InsertInboundSOF, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                throw;
            }
        }
        public async Task<T> GetsofData<T>(int spType, Guid id, string auto_id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id_ref", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_InsertInboundSOF, parameters);
                if (dbResult.Tables[0].Rows.Count > 0)
                {
                    return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
                }
                else
                {
                    return default(T);
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
