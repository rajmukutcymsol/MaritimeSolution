using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
    public class CategoryRepository: ICategoryRepository
    {
        public async Task<List<master_category>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCategory_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCategory, parameters);
            return CommonUtility.ConvertDataTableToList<master_category>(db_result.Tables[0]);
        }

        public async Task<master_category> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCategory_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCategory, parameters);
            return CommonUtility.GetObjectByRow<master_category>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(master_category request)
        {
            try
            {
                SqlParameter[] parameter =
                {
                new SqlParameter("@spType",usp_ManageCategory_Type.save),
                new SqlParameter("@category_name",request.category_name),
                new SqlParameter("@category_name_discription",request.category_name_discription),
                new SqlParameter("@is_active",request.is_active),
            };
                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCategory, parameter);

                return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CommonDbResponse> Update(master_category request)
        {
            SqlParameter[] parameter =
             {
               new SqlParameter("@spType",usp_ManageCategory_Type.update),
               new SqlParameter("@category_name",request.category_name),
               new SqlParameter("@category_name_discription",request.category_name_discription),
               new SqlParameter("@is_active",request.is_active),
               new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCategory, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCategory_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCategory, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
