using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketingTool.Data.Connection;
using TicketingTool.Data.Helper;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Masters;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Master;
using TicketingTool.Utilities;

namespace TicketingTool.Services.Concrete.Master
{
    public class MasterRepository : IMasterRepository
    {
        public async Task<vm_master> GetAllMaster(int spType)
        {
            vm_master masters = new vm_master();
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) };
            var dbResult = await SqlHelper.ExecuteDatasetAsync(ConnectionCofig.ConnectionStr, Procedures.usp_MasterInfo, parameters);
            if(dbResult.Tables[0].Rows.Count>0)
                masters.priorities= CommonUtility.ConvertDataTableToList<master_priority>(dbResult.Tables[0]);
            if (dbResult.Tables[1].Rows.Count > 0)
                masters.requirements = CommonUtility.ConvertDataTableToList<master_requirement>(dbResult.Tables[1]);
            if (dbResult.Tables[2].Rows.Count > 0)
                masters.statuses = CommonUtility.ConvertDataTableToList<master_status>(dbResult.Tables[2]);
            if (dbResult.Tables[3].Rows.Count > 0)
                masters.sdlcs = CommonUtility.ConvertDataTableToList<master_sdlc>(dbResult.Tables[3]);
            if (dbResult.Tables[4].Rows.Count > 0)
                masters.technologies = CommonUtility.ConvertDataTableToList<master_technology>(dbResult.Tables[4]);
            if (dbResult.Tables[5].Rows.Count > 0)
                masters.vendors = CommonUtility.ConvertDataTableToList<master_vendor>(dbResult.Tables[5]);
            if (dbResult.Tables[6].Rows.Count > 0)
                masters.boolean_dropdowns = CommonUtility.ConvertDataTableToList<BooleanDropdown>(dbResult.Tables[6]);
            if (dbResult.Tables[7].Rows.Count > 0)
                masters.tools = CommonUtility.ConvertDataTableToList<master_tool>(dbResult.Tables[7]);
                masters.tools.Insert(0, new master_tool { id = System.Guid.Empty, tool_name = "--Select--" });
            if (dbResult.Tables[8].Rows.Count > 0)
                masters.functions = CommonUtility.ConvertDataTableToList<master_function>(dbResult.Tables[8]);
            if (dbResult.Tables[9].Rows.Count > 0)
                masters.function_levels = CommonUtility.ConvertDataTableToList<master_function_level>(dbResult.Tables[9]);
            if (dbResult.Tables[10].Rows.Count > 0)
                masters.node_types = CommonUtility.ConvertDataTableToList<master_node_type>(dbResult.Tables[10]);
            if (dbResult.Tables[11].Rows.Count > 0)
                masters.customers = CommonUtility.ConvertDataTableToList<master_customer>(dbResult.Tables[11]);
            if (dbResult.Tables[12].Rows.Count > 0)
                masters.regions = CommonUtility.ConvertDataTableToList<master_region>(dbResult.Tables[12]);
            if (dbResult.Tables[13].Rows.Count > 0)
                masters.domains = CommonUtility.ConvertDataTableToList<master_domain>(dbResult.Tables[13]);
            if (dbResult.Tables[14].Rows.Count > 0)
                masters.projects = CommonUtility.ConvertDataTableToList<master_project>(dbResult.Tables[14]);
            if (dbResult.Tables[15].Rows.Count > 0)
                masters.efficiencies = CommonUtility.ConvertDataTableToList<master_efficiency>(dbResult.Tables[15]);
            if (dbResult.Tables[16].Rows.Count > 0)
                masters.usecases = CommonUtility.ConvertDataTableToList<master_usecase>(dbResult.Tables[16]);
                masters.usecases.Insert(0, new master_usecase { id = System.Guid.Empty, use_case_name = "--Select--" });

            
            if (dbResult.Tables[21].Rows.Count > 0)
                masters.mastercligui = CommonUtility.ConvertDataTableToList<master_cli_ui>(dbResult.Tables[21]);
            masters.mastercligui.Insert(0, new master_cli_ui { id = System.Guid.Empty, cli_ui_name = "--Select--" });

            //MTS
            if (dbResult.Tables[22].Rows.Count > 0)
                masters.mastercountry = CommonUtility.ConvertDataTableToList<master_Country>(dbResult.Tables[22]);
            masters.mastercountry.Insert(0, new master_Country { CountryID = 0, CountryName = "--Select--" });

            if (dbResult.Tables[23].Rows.Count > 0)
                masters.mastertype = CommonUtility.ConvertDataTableToList<master_category>(dbResult.Tables[23]);
            masters.mastertype.Insert(0, new master_category { id = System.Guid.Empty,  category_name= "--Select--" });

            if (dbResult.Tables[24].Rows.Count > 0)
                masters.mastervessel = CommonUtility.ConvertDataTableToList<master_vessel>(dbResult.Tables[24]);
            masters.mastervessel.Insert(0, new master_vessel { id = System.Guid.Empty, vesselName = "--Select--" });
            
            if (dbResult.Tables[25].Rows.Count > 0)
                masters.masterport = CommonUtility.ConvertDataTableToList<master_discharge_port>(dbResult.Tables[25]);
            masters.masterport.Insert(0, new master_discharge_port { id = System.Guid.Empty, discharge_port_name = "--Select--" });

            if (dbResult.Tables[26].Rows.Count > 0)
                masters.masterowners = CommonUtility.ConvertDataTableToList<vm_Owners>(dbResult.Tables[26]);
            masters.masterowners.Insert(0, new vm_Owners { id = System.Guid.Empty, owners_name = "--Select--" });

            if (dbResult.Tables[27].Rows.Count > 0)
                masters.masterreciver = CommonUtility.ConvertDataTableToList<vm_Receiver>(dbResult.Tables[27]);
            masters.masterreciver.Insert(0, new vm_Receiver { id = System.Guid.Empty, receiver_name = "--Select--" });

            if (dbResult.Tables[28].Rows.Count > 0)
                masters.mastersurveyor = CommonUtility.ConvertDataTableToList<vm_Surveyor>(dbResult.Tables[28]);
            masters.mastersurveyor.Insert(0, new vm_Surveyor { id = System.Guid.Empty, surveyor_name = "--Select--" });

            if (dbResult.Tables[29].Rows.Count > 0)
                masters.masterstevedore = CommonUtility.ConvertDataTableToList<vm_stevedore>(dbResult.Tables[29]);
            masters.masterstevedore.Insert(0, new vm_stevedore { id = System.Guid.Empty, stevedore_name = "--Select--" });

            if (dbResult.Tables[30].Rows.Count > 0)
                masters.vm_shipping = CommonUtility.ConvertDataTableToList<vm_shipping>(dbResult.Tables[30]);
            masters.vm_shipping.Insert(0, new vm_shipping { id = System.Guid.Empty, shipping_name = "--Select--" });

            if (dbResult.Tables[31].Rows.Count > 0)
                masters.vm_checker = CommonUtility.ConvertDataTableToList<vm_checker>(dbResult.Tables[31]);
            masters.vm_checker.Insert(0, new vm_checker { id = System.Guid.Empty, checker_name = "--Select--" });

            if (dbResult.Tables[32].Rows.Count > 0)
                masters.vm_shipper = CommonUtility.ConvertDataTableToList<vm_shipper>(dbResult.Tables[32]);
            masters.vm_shipper.Insert(0, new vm_shipper { id = System.Guid.Empty, shipper_name = "--Select--" });

            if (dbResult.Tables[33].Rows.Count > 0)
                masters.vm_marks_and_nos = CommonUtility.ConvertDataTableToList<marks_and_nos>(dbResult.Tables[33]);
            masters.vm_marks_and_nos.Insert(0, new marks_and_nos { id = System.Guid.Empty, marks_and_nos_name = "--Select--" });

            if (dbResult.Tables[34].Rows.Count > 0)
                masters.quantity_and_Kind_of_cargo = CommonUtility.ConvertDataTableToList<Quantity_and_Kind_of_cargo>(dbResult.Tables[34]);
            masters.quantity_and_Kind_of_cargo.Insert(0, new Quantity_and_Kind_of_cargo { id = System.Guid.Empty, Quantity_and_Kind_of_cargo_name = "--Select--" });

            if (dbResult.Tables[35].Rows.Count > 0)
                masters.Master_cargo_type = CommonUtility.ConvertDataTableToList<master_cargo_type>(dbResult.Tables[35]);
            masters.Master_cargo_type.Insert(0, new master_cargo_type { id = System.Guid.Empty, cargo_type_name = "--Select--" });

            if (dbResult.Tables[36].Rows.Count > 0)
                masters.Master_quantity_cargo = CommonUtility.ConvertDataTableToList<master_quantity_cargo>(dbResult.Tables[36]);
            masters.Master_quantity_cargo.Insert(0, new master_quantity_cargo { id = System.Guid.Empty, qtt_name = "--Select--" });

            if (dbResult.Tables[37].Rows.Count > 0)
                masters.FC_Stevedore_Name = CommonUtility.ConvertDataTableToList<master_FC_Stevedore_Operations>(dbResult.Tables[37]);
            masters.FC_Stevedore_Name.Insert(0, new master_FC_Stevedore_Operations { id = System.Guid.Empty, FC_Stevedore_Name = "--Select--" });


            return masters;
        }

        public async Task<List<T>> GetMasterInfo<T>(int spType)
        {
            SqlParameter[] parameters ={new SqlParameter("@spType",spType) };
            var dbResult = await SqlHelper.ExecuteDatasetAsync(ConnectionCofig.ConnectionStr, Procedures.usp_MasterInfo, parameters);
            return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
        }

        public async Task<List<T>> GetData<T>(int spType, string procedureName)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) };
            var dbResult = await SqlHelper.ExecuteDatasetAsync(ConnectionCofig.ConnectionStr, procedureName, parameters);
            return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
        }

        public async Task<List<T>> GetDataById<T>(int spType, string procedureName)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) };
            var dbResult = await SqlHelper.ExecuteDatasetAsync(ConnectionCofig.ConnectionStr, procedureName, parameters);
            return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
        }

        public async Task<T> SaveData<T>(string procedureName, SqlParameter[] parameters)
        {
            var dbResult = await SqlHelper.ExecuteDatasetAsync(ConnectionCofig.ConnectionStr, procedureName, parameters);
            return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
        }
    }
}
