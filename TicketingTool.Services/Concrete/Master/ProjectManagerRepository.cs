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
    public class ProjectManagerRepository:IProjectManagerRepository
    {
        public async Task<List<master_project_manager>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageDeveloper_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProjectManager, parameters);
            return CommonUtility.ConvertDataTableToList<master_project_manager>(db_result.Tables[0]);
        }
        public async Task<master_project_manager> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageDeveloper_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProjectManager, parameters);
            return CommonUtility.GetObjectByRow<master_project_manager>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Save(master_project_manager request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageDeveloper_Type.save),
                new SqlParameter("@project_manager_name",request.employee_name),
                new SqlParameter("@employee_id",request.employee_id),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProjectManager, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Update(master_project_manager request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageDeveloper_Type.update),
                new SqlParameter("@project_manager_name",request.employee_name),
                new SqlParameter("@employee_id",request.employee_id),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProjectManager, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageDeveloper_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProjectManager, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
