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
    public class VendorRepository : IVendorRepository
    {
        public async Task<List<master_vendor>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageVendor_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageVendor, parameters);
            return CommonUtility.ConvertDataTableToList<master_vendor>(db_result.Tables[0]);
        }

        public async Task<master_vendor> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageVendor_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageVendor, parameters);
            return CommonUtility.GetObjectByRow<master_vendor>(db_result.Tables[0].Rows[0]);
        }


        public async Task<CommonDbResponse> Save(master_vendor request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageVendor_Type.save),
                new SqlParameter("@vendor_name",request.vendor_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageVendor, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_vendor request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageVendor_Type.update),
                new SqlParameter("@vendor_name",request.vendor_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageVendor, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageVendor_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageVendor, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
