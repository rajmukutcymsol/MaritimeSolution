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
    public class ResCat2Repository : IResCat2Repository
    {
        public async Task<List<master_res_cat_2>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageResCat2_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat2, parameters);
            return CommonUtility.ConvertDataTableToList<master_res_cat_2>(db_result.Tables[0]);
        }

        public async Task<master_res_cat_2> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageResCat2_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat2, parameters);
            return CommonUtility.GetObjectByRow<master_res_cat_2>(db_result.Tables[0].Rows[0]);
        }

        
        public async Task<CommonDbResponse> Save(master_res_cat_2 request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageResCat2_Type.save),
                new SqlParameter("@res_cat2_name",request.res_cat2_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat2, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Update(master_res_cat_2 request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageResCat2_Type.update),
                new SqlParameter("@res_cat2_name",request.res_cat2_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat2, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageResCat2_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat2, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        //public async Task<List<master_res_cat_2>> GetAll()
        //{
        //    SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageResCat2_Type.getAll) };
        //    var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat2, parameters);
        //    return CommonUtility.ConvertDataTableToList<master_res_cat_2>(db_result.Tables[0]);
        //}

        public async Task<List<master_res_cat_2>> GetByCat1Id<T>(Guid res_cat1)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageResCat2_Type.getbyCat1Id), new SqlParameter("@id", res_cat1) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat2, parameters);
            return CommonUtility.ConvertDataTableToList<master_res_cat_2>(db_result.Tables[0]);
        }

        public async Task<List<master_res_cat_3>> GetByCat3Id<T>(Guid res_cat1, Guid res_cat2)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageResCat2_Type.getbyCat2Id), new SqlParameter("@id", res_cat1), new SqlParameter("@res_cat2", res_cat2) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageResCat2, parameters);
            return CommonUtility.ConvertDataTableToList<master_res_cat_3>(db_result.Tables[0]);

        }
    }
}
