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
    public class ProjectToolsMapping:IProjectsToolsMapping
    {
       
        public async Task<List<master_project>> GetMaster_projects()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageProject_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProject, parameters);
            return CommonUtility.ConvertDataTableToList<master_project>(db_result.Tables[0]);
        }

        public async Task<T> Save<T>(int spType, vw_project_tools_mapping vw_project_tools_Mapping, string username)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@master_Projects_id",vw_project_tools_Mapping.projectid),
                    new SqlParameter("@master_Tools_id",vw_project_tools_Mapping.toolid),
                    new SqlParameter("@created_By",username),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProjectToolsMapping, parameters);
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
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProjectToolsMapping, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<T>> GetById<T>(int spType, vw_project_tools_mapping request)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@master_Projects_id",request.projectid),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProjectToolsMapping, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
