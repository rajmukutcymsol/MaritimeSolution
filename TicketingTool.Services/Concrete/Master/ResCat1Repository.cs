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
    public class ResCat1Repository: IResCat1Repository
    {
        public async Task<List<master_res_cat_1>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageResCat1_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat1, parameters);
            return CommonUtility.ConvertDataTableToList<master_res_cat_1>(db_result.Tables[0]);
        }

        public async Task<master_res_cat_1> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageResCat1_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat1, parameters);
            return CommonUtility.GetObjectByRow<master_res_cat_1>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(master_res_cat_1 request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageResCat1_Type.save),
                new SqlParameter("@res_cat1_name",request.res_cat1_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat1, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Update(master_res_cat_1 request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageResCat1_Type.update),
                new SqlParameter("@res_cat1_name",request.res_cat1_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat1, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageResCat1_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat1, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
