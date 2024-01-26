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
    public class CityRepository: ICityRepository
    {
        public async Task<List<master_City>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCity_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCity, parameters);
            return CommonUtility.ConvertDataTableToList<master_City>(db_result.Tables[0]);
        }
        public async Task<master_City> GetById(Guid id/*int? CountryID, int stateid*/)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCity_Type.getById), new SqlParameter("@id", id)/*,new SqlParameter("@stateid", stateid)*/ };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCity, parameters);
            return CommonUtility.GetObjectByRow<master_City>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Save(master_City request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageCity_Type.save),
                new SqlParameter("@country_id",request.CountryID),
                new SqlParameter("@state_id",request.stateId),
                new SqlParameter("@city_name",request.CityName),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCity, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Update(master_City request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@id",request.id),
                new SqlParameter("@spType",usp_ManageCity_Type.save),
                new SqlParameter("@country_id",request.CountryID),
                new SqlParameter("@state_id",request.stateId),
                new SqlParameter("@city_name",request.CityName),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCity, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Delete(Guid id /*int? countryID, int stateid*/)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageCity_Type.delete), new SqlParameter("@id", id)/*, new SqlParameter("@stateid", stateid)*/ };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCity, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

    }
}
