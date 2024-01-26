using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketingTool.Data.Connection;
using TicketingTool.Data.Helper;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Models.Models;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Mapping;
using TicketingTool.Utilities;

namespace TicketingTool.Services.Concrete.Mapping
{
    public class ToolUseCasesMapping : IToolUseCasesMapping
    {
        public async Task<List<master_usecase>> GetMaster_UseCaseName()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", ToolUseCaseMapping_Type.getall) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUseCaseToolMapping, parameters);
            return CommonUtility.ConvertDataTableToList<master_usecase>(db_result.Tables[0]);
        }
        public async Task<List<master_usecase>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageUseCase_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUseCase, parameters);
            return CommonUtility.ConvertDataTableToList<master_usecase>(db_result.Tables[0]);
        }
        public async Task<T> Save<T>(int spType, vw_tool_usecases_mapping vw_tool_usecases_Mapping, string username)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@master_Tool_id",vw_tool_usecases_Mapping.toolid),
                    new SqlParameter("@master_UseCase_id",vw_tool_usecases_Mapping.usecaseid),
                    new SqlParameter("@project_name",vw_tool_usecases_Mapping.projectid),
                    new SqlParameter("@venderid",vw_tool_usecases_Mapping.venderid),
                    new SqlParameter("@technologyid",vw_tool_usecases_Mapping.technologyid),
                    new SqlParameter("@nodetypeid",vw_tool_usecases_Mapping.nodetypeid),
                    new SqlParameter("@created_By",username),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUseCaseToolMapping, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",(int)ProjectToolsMapping_Type.delete),
                    new SqlParameter("@id",id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUseCaseToolMapping, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<T>> GetById<T>(int spType, vw_tool_usecases_mapping request)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@master_Tool_id",request.toolid),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUseCaseToolMapping, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<List<T>> GetByProjectNameWithToolId<T>(int spType, Guid toolid, Guid projectid)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@master_Tool_id",toolid),
                    new SqlParameter("@project_name",projectid),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUseCaseToolMapping, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
