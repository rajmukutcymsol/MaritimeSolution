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
    public class NodeTypeRepository : INodeTypeRepository
    {
        public async Task<List<master_node_type>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageNodeType_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageNodeType, parameters);
            return CommonUtility.ConvertDataTableToList<master_node_type>(db_result.Tables[0]);
        }

        public async Task<master_node_type> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageNodeType_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageNodeType, parameters);
            return CommonUtility.GetObjectByRow<master_node_type>(db_result.Tables[0].Rows[0]);
        }


        public async Task<CommonDbResponse> Save(master_node_type request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageNodeType_Type.save),
                new SqlParameter("@node_type_name",request.node_type_name),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageNodeType, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_node_type request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageNodeType_Type.update),
                new SqlParameter("@node_type_name",request.node_type_name),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageNodeType, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageNodeType_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageNodeType, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
