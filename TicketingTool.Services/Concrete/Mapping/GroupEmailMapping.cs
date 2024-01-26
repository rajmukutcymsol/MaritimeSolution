using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketingTool.Data.Connection;
using TicketingTool.Data.Helper;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Models.Models;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Mapping;
using TicketingTool.Utilities;

namespace TicketingTool.Services.Concrete.Mapping
{
    public class GroupEmailMapping:IGroupEmailRepositoryMapping
    {
        public async Task<T> Save<T>(int spType, vw_GroupEmailMapping _vw_GroupEmailMapping, string username)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@group_id",_vw_GroupEmailMapping.group_id),
                    new SqlParameter("@employee_id",_vw_GroupEmailMapping.employee_id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageGroupEmailMapping, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<vw_GroupEmailMapping>> GetAll(int getall)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", getall) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageGroupEmailMapping, parameters);
            return CommonUtility.ConvertDataTableToList<vw_GroupEmailMapping>(db_result.Tables[0]);
        }
        
        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",(int)usp_GroupEmailMapping_Type.delete),
                    new SqlParameter("@id",id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageGroupEmailMapping, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<vw_GroupEmailMapping>> GetAll(int getall, vw_GroupEmailMapping _vw_GroupEmailMapping)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",getall),
                    new SqlParameter("@group_id",_vw_GroupEmailMapping.group_id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageGroupEmailMapping, parameters);
                return CommonUtility.ConvertDataTableToList<vw_GroupEmailMapping>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
