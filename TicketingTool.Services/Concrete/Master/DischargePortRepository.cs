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
    public class DischargePortRepository: IDischargePortRipository
    {
        public async Task<List<master_discharge_port>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCLIUI_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDischargePort, parameters);
            return CommonUtility.ConvertDataTableToList<master_discharge_port>(db_result.Tables[0]);
        }

        public async Task<master_discharge_port> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCLIUI_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDischargePort, parameters);
            return CommonUtility.GetObjectByRow<master_discharge_port>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(master_discharge_port request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageCLIUI_Type.save),
                new SqlParameter("@discharge_port_name",request.discharge_port_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDischargePort, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_discharge_port request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageCLIUI_Type.update),
               new SqlParameter("@discharge_port_name",request.discharge_port_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDischargePort, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCLIUI_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDischargePort, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
