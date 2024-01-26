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
    public class QuantityandKindofcargoRepository: IQuantityandKindofcargoname
    {
        public async Task<List<Quantity_and_Kind_of_cargo>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_Quantity_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageQuantityAndKindOfCargo, parameters);
            return CommonUtility.ConvertDataTableToList<Quantity_and_Kind_of_cargo>(db_result.Tables[0]);
        }

        public async Task<Quantity_and_Kind_of_cargo> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_Quantity_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageQuantityAndKindOfCargo, parameters);
            return CommonUtility.GetObjectByRow<Quantity_and_Kind_of_cargo>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(Quantity_and_Kind_of_cargo request)
        {
            try
            {
                SqlParameter[] parameter =
                {
                new SqlParameter("@spType",usp_Quantity_Type.save),
                new SqlParameter("@quantity_and_kind_name",request.Quantity_and_Kind_of_cargo_name),
                new SqlParameter("@is_active",request.is_active),
            };
                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageQuantityAndKindOfCargo, parameter);

                return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CommonDbResponse> Update(Quantity_and_Kind_of_cargo request)
        {
            SqlParameter[] parameter =
             {
               new SqlParameter("@spType",usp_Quantity_Type.update),
               new SqlParameter("@quantity_and_kind_name",request.Quantity_and_Kind_of_cargo_name),
               new SqlParameter("@is_active",request.is_active),
               new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageQuantityAndKindOfCargo, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_Quantity_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageQuantityAndKindOfCargo, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
