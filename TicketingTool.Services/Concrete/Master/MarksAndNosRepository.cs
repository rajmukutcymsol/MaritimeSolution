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
    public class MarksAndNosRepository: IMarksAndNosRepository
    {
        public async Task<List<marks_and_nos>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageMarkandNos_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageMarksAndNos, parameters);
            return CommonUtility.ConvertDataTableToList<marks_and_nos>(db_result.Tables[0]);
        }

        public async Task<marks_and_nos> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageMarkandNos_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageMarksAndNos, parameters);
            return CommonUtility.GetObjectByRow<marks_and_nos>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(marks_and_nos request)
        {
            try
            {
                SqlParameter[] parameter =
                {
                new SqlParameter("@spType",usp_ManageMarkandNos_Type.save),
                new SqlParameter("@marks_and_nos_name",request.marks_and_nos_name),
                new SqlParameter("@is_active",request.is_active),
            };
                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageMarksAndNos, parameter);

                return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CommonDbResponse> Update(marks_and_nos request)
        {
            SqlParameter[] parameter =
             {
               new SqlParameter("@spType",usp_ManageMarkandNos_Type.update),
               new SqlParameter("@marks_and_nos_name",request.marks_and_nos_name),
               new SqlParameter("@is_active",request.is_active),
               new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageMarksAndNos, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageMarkandNos_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageMarksAndNos, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
