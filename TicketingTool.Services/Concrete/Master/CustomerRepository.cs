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
    public class CustomerRepository : ICustomerRepository
    {
        public async Task<List<master_customer>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCustomer_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCustomer, parameters);
            return CommonUtility.ConvertDataTableToList<master_customer>(db_result.Tables[0]);
        }

        public async Task<master_customer> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCustomer_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCustomer, parameters);
            return CommonUtility.GetObjectByRow<master_customer>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(master_customer request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageCustomer_Type.save),
                new SqlParameter("@customer_name",request.customer_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCustomer, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_customer request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageCustomer_Type.update),
               new SqlParameter("@customer_name",request.customer_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCustomer, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCustomer_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCustomer, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
