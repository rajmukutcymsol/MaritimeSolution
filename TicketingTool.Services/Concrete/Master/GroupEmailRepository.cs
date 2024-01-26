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
    public class GroupEmailRepository:IGroupEmailRepository
    {
        public async Task<List<master_email_group>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageGroupEmail_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageGroupEmail, parameters);
            return CommonUtility.ConvertDataTableToList<master_email_group>(db_result.Tables[0]);
        }

        public async Task<master_email_group> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageGroupEmail_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageGroupEmail, parameters);
            return CommonUtility.GetObjectByRow<master_email_group>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(master_email_group request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageGroupEmail_Type.save),
                new SqlParameter("@group_name",request.group_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@created_by",request.created_by),

            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageGroupEmail, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_email_group request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageGroupEmail_Type.update),
               new SqlParameter("@group_name",request.group_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
                new SqlParameter("@updated_by",request.updated_by),

            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageGroupEmail, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageGroupEmail_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageGroupEmail, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
