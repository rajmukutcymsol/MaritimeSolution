using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketingTool.Data.Connection;
using TicketingTool.Data.Helper;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Mapping;
using TicketingTool.Utilities;

namespace TicketingTool.Services.Concrete.Mapping
{
    public class RoleMenuMapping : IRoleMenuMapping
    {
        public async Task<List<master_menu>> GetMaster_Menus()
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageRoleMenuMapping_Type.getRole) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRoleMenuMapping, parameters);
            return CommonUtility.ConvertDataTableToList<master_menu>(db_result.Tables[0]);
        }

        public async Task<List<vm_RoleMenu>> GetMappedRoleMenu(Guid id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageRoleMenuMapping_Type.getMappedMenu), new SqlParameter("@roleId",id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRoleMenuMapping, parameters);
            return CommonUtility.ConvertDataTableToList<vm_RoleMenu>(db_result.Tables[0]);
        }

        public async Task<CommonDbResponse> SaveRoleMenuMapping(List<vm_RoleMenu> roleMenu)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageRoleMenuMapping_Type.saveMapping), new SqlParameter("@tbl_role_menu_mapping", CommonUtility.CreateDataTableFromList(roleMenu)) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRoleMenuMapping, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }


    }
}
