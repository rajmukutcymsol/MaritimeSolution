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
    public class ResCat3Repository : IResCat3Repository
    {
        public async Task<List<master_res_cat_3>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageResCat3_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat3, parameters);
            return CommonUtility.ConvertDataTableToList<master_res_cat_3>(db_result.Tables[0]);
        }

        public async Task<master_res_cat_3> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageResCat3_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat3, parameters);
            return CommonUtility.GetObjectByRow<master_res_cat_3>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(master_res_cat_3 request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageResCat3_Type.save),
                new SqlParameter("@res_cat3_name",request.res_cat3_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat3, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Update(master_res_cat_3 request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageResCat3_Type.update),
                new SqlParameter("@res_cat3_name",request.res_cat3_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat3, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageResCat3_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat3, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
