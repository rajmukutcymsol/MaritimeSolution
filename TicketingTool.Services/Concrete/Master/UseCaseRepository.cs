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
    public class UseCaseRepository : IUseCaseRepository
    {
        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageUseCase_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUseCase, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);

        }

        public async Task<List<master_usecase>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageUseCase_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUseCase, parameters);
            return CommonUtility.ConvertDataTableToList<master_usecase>(db_result.Tables[0]);
        }

        public async Task<master_usecase> GetById(Guid? id)
        {
            
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageUseCase_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUseCase, parameters);
            return CommonUtility.GetObjectByRow<master_usecase>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(master_usecase request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageUseCase_Type.save),
                new SqlParameter("@use_case_name",request.use_case_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUseCase, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);

        }

        public async Task<CommonDbResponse> Update(master_usecase request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageUseCase_Type.update),
               new SqlParameter("@use_case_name",request.use_case_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUseCase, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);

        }
    }
}
