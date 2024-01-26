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
    public class CountryRepository:ICountryRepository
    {
        public async Task<List<master_Country>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageDomain_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCountry, parameters);
            return CommonUtility.ConvertDataTableToList<master_Country>(db_result.Tables[0]);
        }
        public async Task<master_Country> GetById(int? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageDomain_Type.getById), new SqlParameter("@CountryID", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCountry, parameters);
            return CommonUtility.GetObjectByRow<master_Country>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Save(master_Country request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageDomain_Type.save),
                new SqlParameter("@country_name",request.CountryName),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCountry, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Update(master_Country request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageDomain_Type.update),
                new SqlParameter("@country_name",request.CountryName),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@CountryID",request.CountryID),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCountry, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
        public async Task<CommonDbResponse> Delete(int? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageDomain_Type.delete), new SqlParameter("@CountryID", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageCountry, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}
