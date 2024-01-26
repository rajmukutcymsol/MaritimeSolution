using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketingTool.Data.Connection;
using TicketingTool.Data.Helper;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Services.Abstract.Master;
using TicketingTool.Utilities;

namespace TicketingTool.Services.Concrete.Master
{
    public class VesselDataRepository:IVesselRepository
    {
        public async Task<List<master_vessel>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageVessel.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageVessel, parameters);
            return CommonUtility.ConvertDataTableToList<master_vessel>(db_result.Tables[0]);
        }
        public async Task<master_vessel> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageVessel.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageVessel, parameters);
            return CommonUtility.GetObjectByRow<master_vessel>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Save(master_vessel request)
        {
            SqlParameter[] parameters =
            {
        new SqlParameter("@spType", usp_ManageVessel.save),
        new SqlParameter("@id", request.id),
        new SqlParameter("@vesselName", request.vesselName),
        new SqlParameter("@vesselCode", request.vesselCode),
        new SqlParameter("@deadweight", request.deadweight),
        new SqlParameter("@loa", request.loa),
        new SqlParameter("@maxDraft", request.maxDraft),
        new SqlParameter("@beam", request.beam),
        new SqlParameter("@grt", request.grt),
        new SqlParameter("@nrt", request.nrt),
        new SqlParameter("@flags", request.flags),
        new SqlParameter("@callSign", request.callSign),
        new SqlParameter("@imoNumber", request.imoNumber),
        new SqlParameter("@hatchHolds", request.hatchHolds),
        new SqlParameter("@swl", request.swl),
        new SqlParameter("@vesselOthers", request.vesselOthers),
        new SqlParameter("@is_active", request.is_active),
        new SqlParameter("@is_deleted", request.is_deleted),
        new SqlParameter("@created_by", request.created_by),
       // new SqlParameter("@created_date", request.created_date),
        new SqlParameter("@updated_by", request.updated_by),
        //new SqlParameter("@updated_date", request.updated_date),
        new SqlParameter("@LastPortofcall", request.LastPortofcall),
        new SqlParameter("@piclub", request.piclub),
        new SqlParameter("@ClassificationSociety", request.ClassificationSociety),
        new SqlParameter("@Depth", request.Depth),
    };

            var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageVessel, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Update(int spType, master_vessel request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType", usp_ManageVessel.update),
                new SqlParameter("@id", request.id),
                new SqlParameter("@vesselName", request.vesselName),
                new SqlParameter("@vesselCode", request.vesselCode),
                new SqlParameter("@deadweight", request.deadweight),
                new SqlParameter("@loa", request.loa),
                new SqlParameter("@maxDraft", request.maxDraft),
                new SqlParameter("@beam", request.beam),
                new SqlParameter("@grt", request.grt),
                new SqlParameter("@nrt", request.nrt),
                new SqlParameter("@flags", request.flags),
                new SqlParameter("@callSign", request.callSign),
                new SqlParameter("@imoNumber", request.imoNumber),
                new SqlParameter("@hatchHolds", request.hatchHolds),
                new SqlParameter("@swl", request.swl),
                new SqlParameter("@vesselOthers", request.vesselOthers),
                new SqlParameter("@is_active", request.is_active),
                //new SqlParameter("@is_deleted", request.is_deleted),
                new SqlParameter("@created_by", request.created_by),
                //new SqlParameter("@created_date", request.created_date),
                new SqlParameter("@updated_by", request.updated_by),
                //new SqlParameter("@updated_date", request.updated_date),
                new SqlParameter("@LastPortofcall", request.LastPortofcall),
                new SqlParameter("@piclub", request.piclub),
                new SqlParameter("@ClassificationSociety", request.ClassificationSociety),
                new SqlParameter("@Depth", request.Depth),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageVessel, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageVessel.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageVessel, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
