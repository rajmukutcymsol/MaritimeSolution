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
    public class ProjectRegionMapping :IProjectRegionMapping
    {
        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",(int)ProjectRegionMapping_Type.delete),
                    new SqlParameter("@id",id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProjectRegionMapping, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<vm_project_region_mapping>> GetAll(int getall)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", ProjectRegionMapping_Type.getall) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProjectRegionMapping, parameters);
            return CommonUtility.ConvertDataTableToList<vm_project_region_mapping>(db_result.Tables[0]);
        }

        public async Task<List<T>> GetById<T>(int spType, vm_project_region_mapping request)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@project",request.project),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProjectRegionMapping, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<T> Save<T>(int spType, vm_project_region_mapping _vm_project_region_mapping, string username)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@project",_vm_project_region_mapping.project),
                    new SqlParameter("@region",_vm_project_region_mapping.region),

                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageProjectRegionMapping, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
