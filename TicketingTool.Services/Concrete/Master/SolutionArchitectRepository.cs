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
    public class SolutionArchitectRepository :ISolutionArchitectRepository
    {
        public async Task<List<master_solution_architect>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_Solution_Architect_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageSolutionArchitect, parameters);
            return CommonUtility.ConvertDataTableToList<master_solution_architect>(db_result.Tables[0]);
        }

        public async Task<master_solution_architect> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_Solution_Architect_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageSolutionArchitect, parameters);
            return CommonUtility.GetObjectByRow<master_solution_architect>(db_result.Tables[0].Rows[0]);
        }


        public async Task<CommonDbResponse> Save(master_solution_architect request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_Solution_Architect_Type.save),
                new SqlParameter("@master_solution_architect_name",request.employee_name),
                new SqlParameter("@employee_id",request.employee_id),

                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageSolutionArchitect, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_solution_architect request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_Solution_Architect_Type.update),
                new SqlParameter("@master_solution_architect_name",request.employee_name),
                new SqlParameter("@employee_id",request.employee_id),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageSolutionArchitect, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_Solution_Architect_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageSolutionArchitect, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
