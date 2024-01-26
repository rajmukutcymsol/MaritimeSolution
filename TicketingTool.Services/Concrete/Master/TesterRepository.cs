﻿using System;
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
   public class TesterRepository : ITesterRepository
    {
        public async Task<List<master_tester>> GetAll()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageTester_Type.getAll) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTester, parameters);
            return CommonUtility.ConvertDataTableToList<master_tester>(db_result.Tables[0]);
        }
        public async Task<master_tester> GetById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageTester_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTester, parameters);
            return CommonUtility.GetObjectByRow<master_tester>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Save(master_tester request)
        {
            SqlParameter[] parameter =
            {
                new SqlParameter("@spType",usp_ManageTester_Type.save),
                new SqlParameter("@tester_name",request.tester_name),
                 new SqlParameter("@employee_id",request.employee_id),
                new SqlParameter("@is_active",request.is_active),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTester, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Update(master_tester request)
        {
            SqlParameter[] parameter =
             {
                new SqlParameter("@spType",usp_ManageTester_Type.update),
                new SqlParameter("@tester_name",request.tester_name),
                new SqlParameter("@employee_id",request.employee_id),
                new SqlParameter("@is_active",request.is_active),
                new SqlParameter("@id",request.id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTester, parameter);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageTester_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageTester, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }
    }
}

