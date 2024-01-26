using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketingTool.Models.Common;
using TicketingTool.Models.Models;
using TicketingTool.Models.ViewModel;
using TicketingTool.Models.Masters;
using System.Data.SqlClient;
using TicketingTool.Data.Helper;
using TicketingTool.Data.Connection;
using TicketingTool.Models.Constant;
using TicketingTool.Services.Abstract.Requirement;
using TicketingTool.Utilities;

namespace TicketingTool.Services.Concrete.Requirement
{
    public class InBoundReportsRepository:IInboundReportsRepository
    {
        public async Task<T> GetRow<T>(int spType, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id", id)};
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.Inbound_Reports, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
