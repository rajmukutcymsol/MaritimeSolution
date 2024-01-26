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
    public class TechnologyRepository : ITechnologyRepository
    {
        public async Task<List<master_technology>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageTechnology_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTechnology, parameters);
            return CommonUtility.ConvertDataTableToList<master_technology>(db_result.Tables[0]);
        }

        public async Task<master_technology> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageTechnology_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTechnology, parameters);
            return CommonUtility.GetObjectByRow<master_technology>(db_result.Tables[0].Rows[0]);
        }


        public async Task<CommonDbResponse> Save(master_technology request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageTechnology_Type.save),
                new SqlParameter("@technology_name",request.technology_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTechnology, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_technology request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageTechnology_Type.update),
                new SqlParameter("@technology_name",request.technology_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTechnology, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageTechnology_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTechnology, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
