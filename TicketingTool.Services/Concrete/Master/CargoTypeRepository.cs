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
    public class CargoTypeRepository: ICargoTypeRepository
    {
        public async Task<List<master_cargo_type>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCargoType_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoType, parameters);
            return CommonUtility.ConvertDataTableToList<master_cargo_type>(db_result.Tables[0]);
        }

        public async Task<master_cargo_type> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCargoType_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoType, parameters);
            return CommonUtility.GetObjectByRow<master_cargo_type>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(master_cargo_type request)
        {
            try
            {
                SqlParameter[] parameter =
                {
                new SqlParameter("@spType",usp_ManageCargoType_Type.save),
                new SqlParameter("@cargo_type_name",request.cargo_type_name),
                new SqlParameter("@cargo_type_discription",request.cargo_type_discription),
                new SqlParameter("@is_active",request.is_active),
            };
                var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoType, parameter);

                return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw; 
            }
        }

        public async Task<CommonDbResponse> Update(master_cargo_type request)
        {
            SqlParameter[] parameter =
             {
               new SqlParameter("@spType",usp_ManageCargoType_Type.update),
               new SqlParameter("@cargo_type_name",request.cargo_type_name),
               new SqlParameter("@cargo_type_discription",request.cargo_type_discription),
               new SqlParameter("@is_active",request.is_active),
               new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoType, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCargoType_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCargoType, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
