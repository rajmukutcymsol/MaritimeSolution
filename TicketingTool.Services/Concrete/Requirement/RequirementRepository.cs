using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TicketingTool.Data.Connection;
using TicketingTool.Data.Helper;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.Models;
using TicketingTool.Models.ViewModel;
using TicketingTool.Services.Abstract.Requirement;
using TicketingTool.Utilities;
using TicketingTool.Models.Masters;

namespace TicketingTool.Services.Concrete.Requirement
{
    public class RequirementRepository : IRequirementRepository
    {
        /// <summary>
        /// This method is used to post data (add or update) into project requirement table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spType"></param>
        /// <param name="project_Requirement"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public async Task<T> Save<T>(int spType, project_requirement project_Requirement, string createdBy)
        {
            string updatefor = project_Requirement.auto_id.Substring(0, 2);
            if (spType == (int)ManageRequirement_Type.update_requirement)
            {
                //isue-INC, NEW REC 
                if (updatefor == "RE")
                {
                    SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageUpdateHistory_Type.getByAutoId), new SqlParameter("@auto_id", project_Requirement.auto_id) };
                    var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUpdateHistory, parameters);
                    DataTable ds = (DataTable)dbResult.Tables[0];

                    List<KeyValuePair<string, string>> keyValueList = new List<KeyValuePair<string, string>>();
                    foreach (DataRow row in ds.Rows)
                    {
                        try
                        {
                            if (project_Requirement.auto_id != row["auto_id"].ToString())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("auto_id", row["auto_id"].ToString()));
                            }
                            if (project_Requirement.requirement_type.ToString().ToLower() != row["requirement_type"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("requirement_type", row["requirement_type"].ToString()));
                            }
                            if (project_Requirement.project_name.ToString().ToLower() != row["project_name"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("project_name", row["project_name"].ToString()));
                            }
                            if (project_Requirement.customer.ToString().ToLower() != row["customer"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("customer", row["customer"].ToString()));
                            }
                            if (Convert.ToString(project_Requirement.request_description) != Convert.ToString(row["request_description"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("request_description", row["request_description"].ToString() == "" ? project_Requirement.request_description : (row["request_description"].ToString() != project_Requirement.request_description ? project_Requirement.request_description : row["request_description"].ToString())));
                            }
                            if (Convert.ToString(project_Requirement.request_notes) != Convert.ToString(row["request_notes"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("request_notes", row["request_notes"].ToString() == "" ? project_Requirement.request_notes : (row["request_notes"].ToString() != project_Requirement.request_notes ? project_Requirement.request_notes : row["request_notes"].ToString())));
                            }
                            if (project_Requirement.domain.ToString().ToLower() != row["domain"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("domain", row["domain"].ToString()));
                            }
                            if (project_Requirement.vendor.ToString().ToLower() != row["vendor"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("vendor", Convert.ToString(row["vendor"]) == "" ? Convert.ToString(project_Requirement.vendor) : (Convert.ToString(row["vendor"]) != Convert.ToString(project_Requirement.vendor) ? Convert.ToString(project_Requirement.vendor) : Convert.ToString(row["vendor"]))));
                            }
                            if (project_Requirement.request_priority.ToString().ToLower() != row["request_priority"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("request_priority", Convert.ToString(row["request_priority"]) == "" ? Convert.ToString(project_Requirement.request_priority) : (Convert.ToString(row["request_priority"]) != Convert.ToString(project_Requirement.request_priority) ? Convert.ToString(project_Requirement.request_priority) : Convert.ToString(row["request_priority"]))));
                            }
                            if (Convert.ToString(project_Requirement.cost_value) != Convert.ToString(row["costvalue"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("cost_value", row["costvalue"].ToString() == "" ? Convert.ToString(project_Requirement.cost_value) : ((Convert.ToString(row["costvalue"]) != Convert.ToString(project_Requirement.cost_value) && Convert.ToString(row["costvalue"]) != "") ? row["costvalue"].ToString() : Convert.ToString(project_Requirement.cost_value))));
                            }
                            if (project_Requirement.request_status.ToString().ToLower() != row["request_status"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("request_status", Convert.ToString(row["request_status"]) == "" ? Convert.ToString(project_Requirement.request_status) : (Convert.ToString(row["request_status"]) != Convert.ToString(project_Requirement.request_status) ? Convert.ToString(project_Requirement.request_status) : Convert.ToString(row["request_status"]))));
                            }
                            //if (Convert.ToString(project_Requirement.date_of_state_changed) != Convert.ToString(row["date_of_state_changed"]))
                            //{
                            //    keyValueList.Add(new KeyValuePair<string, string>("date_of_state_changed", row["date_of_state_changed"].ToString() == "" ? Convert.ToString(project_Requirement.date_of_state_changed) : (Convert.ToString(row["date_of_state_changed"]) != Convert.ToString(project_Requirement.date_of_state_changed) ? Convert.ToString(project_Requirement.date_of_state_changed) : Convert.ToString(row["date_of_state_changed"]))));
                            //}
                            if (project_Requirement.node_type.ToString().ToLower() != row["node_type"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("node_type", Convert.ToString(row["node_type"]) == "" ? Convert.ToString(project_Requirement.node_type) : (Convert.ToString(row["node_type"]) != Convert.ToString(project_Requirement.node_type) ? Convert.ToString(project_Requirement.node_type) : Convert.ToString(row["node_type"]))));
                            }
                            if (Convert.ToString(project_Requirement.requester) != Convert.ToString(row["requester"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("requester", row["requester"].ToString() == "" ? project_Requirement.requester : (row["requester"].ToString() != project_Requirement.requester ? project_Requirement.requester : row["requester"].ToString())));
                            }
                            if (Convert.ToString(project_Requirement.lm_of_requested) != Convert.ToString(row["lm_of_requested"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("lm_of_requested", row["lm_of_requested"].ToString() == "" ? project_Requirement.lm_of_requested : (row["lm_of_requested"].ToString() != project_Requirement.lm_of_requested ? project_Requirement.lm_of_requested : row["lm_of_requested"].ToString())));
                            }
                            if (Convert.ToString(project_Requirement.date_of_request) != Convert.ToString(row["date_of_request"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("date_of_request", Convert.ToString(row["date_of_request"]) == "" ? Convert.ToString(project_Requirement.date_of_request) : (Convert.ToString(row["date_of_request"]) != Convert.ToString(project_Requirement.date_of_request) ? Convert.ToString(project_Requirement.date_of_request) : Convert.ToString(row["date_of_request"]))));
                            }
                            if (Convert.ToString(project_Requirement.project_manager) != Convert.ToString(row["project_manager"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("project_manager", row["project_manager"].ToString() == "" ? project_Requirement.project_manager : (row["project_manager"].ToString() != project_Requirement.project_manager ? project_Requirement.project_manager : row["project_manager"].ToString())));
                            }
                            if (Convert.ToString(project_Requirement.solution_architect) != Convert.ToString(row["solution_architect"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("solution_architect", row["solution_architect"].ToString() == "" ? project_Requirement.solution_architect : (row["solution_architect"].ToString() != project_Requirement.solution_architect ? project_Requirement.solution_architect : row["solution_architect"].ToString())));
                            }
                            if (Convert.ToString(project_Requirement.expected_cost_of_development) != Convert.ToString(row["expectedcostofdevelopment"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("expected_cost_of_development", Convert.ToString(row["expectedcostofdevelopment"]) == "" ? Convert.ToString(project_Requirement.expected_cost_of_development) : (Convert.ToString(row["expectedcostofdevelopment"]) != Convert.ToString(project_Requirement.expected_cost_of_development) ? Convert.ToString(project_Requirement.expected_cost_of_development) : Convert.ToString(row["expectedcostofdevelopment"]))));
                            }
                            if (Convert.ToString(project_Requirement.expected_time_of_development_hour) != Convert.ToString(row["expected_time_of_development_hour"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("expected_time_of_development_hour", Convert.ToString(row["expectedcostofdevelopment"]) == "" ? Convert.ToString(project_Requirement.expected_time_of_development_hour) : (Convert.ToString(row["expectedcostofdevelopment"]) != Convert.ToString(project_Requirement.expected_time_of_development_hour) ? Convert.ToString(project_Requirement.expected_time_of_development_hour) : Convert.ToString(row["expectedcostofdevelopment"]))));
                            }
                            if (Convert.ToString(project_Requirement.actual_time_of_development_hour) != Convert.ToString(row["actual_time_of_development_hour"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("actual_time_of_development_hour", Convert.ToString(row["actual_time_of_development_hour"]) == "" ? Convert.ToString(project_Requirement.actual_time_of_development_hour) : (Convert.ToString(row["actual_time_of_development_hour"]) != Convert.ToString(project_Requirement.actual_time_of_development_hour) ? Convert.ToString(project_Requirement.actual_time_of_development_hour) : Convert.ToString(row["actual_time_of_development_hour"]))));
                            }
                            if (Convert.ToString(project_Requirement.developer_name) != Convert.ToString(row["developer_name"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("developer_name", row["developer_name"].ToString() == "" ? project_Requirement.developer_name : (row["developer_name"].ToString() != project_Requirement.developer_name ? project_Requirement.developer_name : row["developer_name"].ToString())));
                            }
                            if (Convert.ToString(project_Requirement.tester_name_sit) != Convert.ToString(row["tester_name_sit"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("tester_name_sit", row["tester_name_sit"].ToString() == "" ? project_Requirement.tester_name_sit : (row["tester_name_sit"].ToString() != project_Requirement.tester_name_sit ? project_Requirement.tester_name_sit : row["tester_name_sit"].ToString())));
                            }
                            if (project_Requirement.is_uat_offered.ToString() != row["is_uat_offered"].ToString())//(row["is_uat_offered"].ToString())!="" ? row["is_uat_offered"]: "";
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("is_uat_offered", Convert.ToString(row["is_uat_offered"]) == "" ? Convert.ToString(project_Requirement.is_uat_offered) : (Convert.ToString(row["is_uat_offered"]) != Convert.ToString(project_Requirement.is_uat_offered) ? Convert.ToString(project_Requirement.is_uat_offered) : Convert.ToString(row["is_uat_offered"]))));
                            }
                            if (project_Requirement.is_correction_during_uat.ToString().ToLower() != row["is_correction_during_uat"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("is_correction_during_uat", Convert.ToString(row["is_correction_during_uat"]) == "" ? Convert.ToString(project_Requirement.is_correction_during_uat) : (Convert.ToString(row["is_correction_during_uat"]) != Convert.ToString(project_Requirement.is_correction_during_uat) ? Convert.ToString(project_Requirement.is_correction_during_uat) : Convert.ToString(row["is_correction_during_uat"]))));
                            }
                            if (Convert.ToString(project_Requirement.correction_description_during_uat) != Convert.ToString(row["correction_description_during_uat"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("correction_description_during_uat", Convert.ToString(row["correction_description_during_uat"]) == "" ? Convert.ToString(project_Requirement.correction_description_during_uat) : (Convert.ToString(row["correction_description_during_uat"]) != Convert.ToString(project_Requirement.correction_description_during_uat) ? Convert.ToString(project_Requirement.correction_description_during_uat) : Convert.ToString(row["correction_description_during_uat"]))));
                            }
                            if (project_Requirement.is_enhancement_during_uat.ToString().ToLower() != row["is_enhancement_during_uat"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("is_enhancement_during_uat", Convert.ToString(row["is_enhancement_during_uat"]) == "" ? Convert.ToString(project_Requirement.is_enhancement_during_uat) : (Convert.ToString(row["is_enhancement_during_uat"]) != Convert.ToString(project_Requirement.is_enhancement_during_uat) ? Convert.ToString(project_Requirement.is_enhancement_during_uat) : Convert.ToString(row["is_enhancement_during_uat"]))));
                            }
                            if (Convert.ToString(project_Requirement.enhancement_description_during_uat) != Convert.ToString(row["enhancement_description_during_uat"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("enhancement_description_during_uat", Convert.ToString(row["enhancement_description_during_uat"]) == "" ? Convert.ToString(project_Requirement.enhancement_description_during_uat) : (Convert.ToString(row["enhancement_description_during_uat"]) != Convert.ToString(project_Requirement.enhancement_description_during_uat) ? Convert.ToString(project_Requirement.enhancement_description_during_uat) : Convert.ToString(row["enhancement_description_during_uat"]))));
                            }
                            if (Convert.ToString(project_Requirement.mop_or_sop) != Convert.ToString(row["mop_or_sop"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("mop_or_sop", Convert.ToString(row["mop_or_sop"]) == "" ? Convert.ToString(project_Requirement.mop_or_sop) : (Convert.ToString(row["mop_or_sop"]) != Convert.ToString(project_Requirement.mop_or_sop) ? Convert.ToString(project_Requirement.mop_or_sop) : Convert.ToString(row["mop_or_sop"]))));
                            }
                            //if (project_Requirement.updated_mop.ToString().ToLower() != row["updated_mop"].ToString().ToLower())
                            //{
                            //    keyValueList.Add(new KeyValuePair<string, string>("updated_mop", row["updated_mop"].ToString()));
                            //}
                            if (project_Requirement.connectivity_availability.ToString().ToLower() != row["connectivity_availability"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("connectivity_availability", Convert.ToString(row["connectivity_availability"]) == "" ? Convert.ToString(project_Requirement.connectivity_availability) : (Convert.ToString(row["connectivity_availability"]) != Convert.ToString(project_Requirement.connectivity_availability) ? Convert.ToString(project_Requirement.connectivity_availability) : Convert.ToString(row["connectivity_availability"]))));
                            }
                            if (project_Requirement.crediential_availability.ToString().ToLower() != row["crediential_availability"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("crediential_availability", Convert.ToString(row["crediential_availability"]) == "" ? Convert.ToString(project_Requirement.crediential_availability) : (Convert.ToString(row["crediential_availability"]) != Convert.ToString(project_Requirement.crediential_availability) ? Convert.ToString(project_Requirement.crediential_availability) : Convert.ToString(row["crediential_availability"]))));
                            }
                            if (Convert.ToString(project_Requirement.attachment_for_mop_or_sop) != Convert.ToString(row["attachment_for_mop_or_sop"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("attachment_for_mop_or_sop", Convert.ToString(row["attachment_for_mop_or_sop"]) == "" ? Convert.ToString(project_Requirement.attachment_for_mop_or_sop) : (Convert.ToString(row["attachment_for_mop_or_sop"]) != Convert.ToString(project_Requirement.attachment_for_mop_or_sop) ? Convert.ToString(project_Requirement.attachment_for_mop_or_sop) : Convert.ToString(row["attachment_for_mop_or_sop"]))));
                            }
                            if (project_Requirement.is_live.ToString().ToLower() != row["is_live"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("is_live", Convert.ToString(row["is_live"]) == "" ? Convert.ToString(project_Requirement.is_live) : (Convert.ToString(row["is_live"]) != Convert.ToString(project_Requirement.is_live) ? Convert.ToString(project_Requirement.is_live) : Convert.ToString(row["is_live"]))));
                            }
                            if (Convert.ToString(project_Requirement.tool_solution_name) != Convert.ToString(row["tool_solution_name"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("tool_solution_name", Convert.ToString(row["tool_solution_name"]) == "" ? Convert.ToString(project_Requirement.tool_solution_name) : (Convert.ToString(row["tool_solution_name"]) != Convert.ToString(project_Requirement.tool_solution_name) ? Convert.ToString(project_Requirement.tool_solution_name) : Convert.ToString(row["tool_solution_name"]))));
                            }
                            if (Convert.ToString(project_Requirement.use_case_name) != Convert.ToString(row["use_case"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("use_case_name", Convert.ToString(row["use_case"]) == "" ? Convert.ToString(project_Requirement.use_case_name) : (Convert.ToString(row["use_case"]) != Convert.ToString(project_Requirement.use_case_name) ? Convert.ToString(project_Requirement.use_case_name) : Convert.ToString(row["use_case"]))));
                            }
                            if (Convert.ToString(project_Requirement.sdlc_status) != Convert.ToString(row["sdlc_status"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("sdlc_status", Convert.ToString(row["sdlc_status"]) == "" ? Convert.ToString(project_Requirement.sdlc_status) : (Convert.ToString(row["sdlc_status"]) != Convert.ToString(project_Requirement.sdlc_status) ? Convert.ToString(project_Requirement.sdlc_status) : Convert.ToString(row["sdlc_status"]))));
                            }
                            if (Convert.ToString(project_Requirement.resolution_offered) != Convert.ToString(row["resolution_offered"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("resolution_offered", Convert.ToString(row["resolution_offered"]) == "" ? Convert.ToString(project_Requirement.resolution_offered) : (Convert.ToString(row["resolution_offered"]) != Convert.ToString(project_Requirement.resolution_offered) ? Convert.ToString(project_Requirement.resolution_offered) : Convert.ToString(row["resolution_offered"]))));
                            }
                            if (Convert.ToString(project_Requirement.support_engineer) != Convert.ToString(row["support_engineer"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("support_engineer", Convert.ToString(row["support_engineer"]) == "" ? Convert.ToString(project_Requirement.support_engineer) : (Convert.ToString(row["support_engineer"]) != Convert.ToString(project_Requirement.support_engineer) ? Convert.ToString(project_Requirement.support_engineer) : Convert.ToString(row["support_engineer"]))));
                            }
                            if (Convert.ToString(project_Requirement.support_manager) != Convert.ToString(row["support_manager"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("support_manager", Convert.ToString(row["support_manager"]) == "" ? Convert.ToString(project_Requirement.support_manager) : (Convert.ToString(row["support_manager"]) != Convert.ToString(project_Requirement.support_manager) ? Convert.ToString(project_Requirement.support_manager) : Convert.ToString(row["support_manager"]))));
                            }
                            if (Convert.ToString(project_Requirement.expected_time_of_resolution_hour) != Convert.ToString(row["expected_time_of_resolution_hour"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("expected_time_of_resolution_hour", Convert.ToString(row["expected_time_of_resolution_hour"]) == "" ? Convert.ToString(project_Requirement.expected_time_of_resolution_hour) : (Convert.ToString(row["expected_time_of_resolution_hour"]) != Convert.ToString(project_Requirement.expected_time_of_resolution_hour) ? Convert.ToString(project_Requirement.expected_time_of_resolution_hour) : Convert.ToString(row["expected_time_of_resolution_hour"]))));
                            }
                            if (Convert.ToString(project_Requirement.actual_time_of_resolution_hour) != Convert.ToString(row["actual_time_of_resolution_hour"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("actual_time_of_resolution_hour", Convert.ToString(row["actual_time_of_resolution_hour"]) == "" ? Convert.ToString(project_Requirement.actual_time_of_resolution_hour) : (Convert.ToString(row["actual_time_of_resolution_hour"]) != Convert.ToString(project_Requirement.actual_time_of_resolution_hour) ? Convert.ToString(project_Requirement.actual_time_of_resolution_hour) : Convert.ToString(row["actual_time_of_resolution_hour"]))));
                            }
                            if (createdBy != row["created_by"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("created_by", row["created_by"].ToString()));
                            }
                            if (project_Requirement.is_resolution_offered.ToString().ToLower() != row["is_resolution_offered"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("is_resolution_offered", Convert.ToString(row["is_resolution_offered"]) == "" ? Convert.ToString(project_Requirement.is_resolution_offered) : (Convert.ToString(row["is_resolution_offered"]) != Convert.ToString(project_Requirement.is_resolution_offered) ? Convert.ToString(project_Requirement.is_resolution_offered) : Convert.ToString(row["is_resolution_offered"]))));
                            }
                            if (Convert.ToString(project_Requirement.business_benifit) != Convert.ToString(row["businessbenifit"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("business_benifit", Convert.ToString(row["businessbenifit"]) == "" ? Convert.ToString(project_Requirement.business_benifit) : (Convert.ToString(row["businessbenifit"]) != Convert.ToString(project_Requirement.business_benifit) ? Convert.ToString(project_Requirement.business_benifit) : Convert.ToString(row["businessbenifit"]))));
                            }
                            if (project_Requirement.function_id.ToString().ToLower() != row["function_id"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("function_id", Convert.ToString(row["function_id"]) == "" ? Convert.ToString(project_Requirement.business_benifit) : (Convert.ToString(row["function_id"]) != Convert.ToString(project_Requirement.function_id) ? Convert.ToString(project_Requirement.function_id) : Convert.ToString(row["function_id"]))));
                            }
                            if (project_Requirement.function_level.ToString().ToLower() != row["function_level"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("function_level", Convert.ToString(row["function_level"]) == "" ? Convert.ToString(project_Requirement.function_level) : (Convert.ToString(row["function_level"]) != Convert.ToString(project_Requirement.function_level) ? Convert.ToString(project_Requirement.function_level) : Convert.ToString(row["function_level"]))));
                            }
                            if (project_Requirement.region.ToString().ToLower() != row["region"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("region", Convert.ToString(row["region"]) == "" ? Convert.ToString(project_Requirement.region) : (Convert.ToString(row["region"]) != Convert.ToString(project_Requirement.region) ? Convert.ToString(project_Requirement.region) : Convert.ToString(row["region"]))));
                            }
                            if (Convert.ToString(project_Requirement.expected_time_effort_hr) != Convert.ToString(row["expected_time_effort_hr"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("expected_time_effort_hr", Convert.ToString(row["expected_time_effort_hr"]) == "" ? Convert.ToString(project_Requirement.expected_time_effort_hr) : (Convert.ToString(row["expected_time_effort_hr"]) != Convert.ToString(project_Requirement.expected_time_effort_hr) ? Convert.ToString(project_Requirement.expected_time_effort_hr) : Convert.ToString(row["expected_time_effort_hr"]))));
                            }
                            if (project_Requirement.efficiency.ToString().ToLower() != row["efficiency"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("efficiency", Convert.ToString(row["efficiency"]) == "" ? Convert.ToString(project_Requirement.efficiency) : (Convert.ToString(row["efficiency"]) != Convert.ToString(project_Requirement.efficiency) ? Convert.ToString(project_Requirement.efficiency) : Convert.ToString(row["efficiency"]))));
                            }
                            if (Convert.ToString(project_Requirement.efficiency_value) != Convert.ToString(row["efficiency_value"]))
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("efficiency_value", Convert.ToString(row["efficiency_value"]) == "" ? Convert.ToString(project_Requirement.efficiency_value) : (Convert.ToString(row["efficiency_value"]) != Convert.ToString(project_Requirement.efficiency_value) ? Convert.ToString(project_Requirement.efficiency_value) : Convert.ToString(row["efficiency_value"]))));
                            }
                            if (project_Requirement.technology.ToString().ToLower() != row["technology"].ToString().ToLower())
                            {
                                keyValueList.Add(new KeyValuePair<string, string>("technology", Convert.ToString(row["technology"]) == "" ? Convert.ToString(project_Requirement.technology) : (Convert.ToString(row["technology"]) != Convert.ToString(project_Requirement.technology) ? Convert.ToString(project_Requirement.technology) : Convert.ToString(row["technology"]))));
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    keyValueList.RemoveAll(kvp => kvp.Value == "");
                    keyValueList.RemoveAll(kvp => kvp.Value == null);
                    // toacces 
                    foreach (var kvp in keyValueList)
                    {
                        string values = kvp.Value.ToString();
                        if (kvp.Key.ToString() == "request_status")
                        {
                            Guid r = Guid.Parse(kvp.Value.ToString());
                            values = await GetStatusNameById(r);
                        }
                        else if (kvp.Key.ToString() == "sdlc_status")
                        {
                            Guid r = Guid.Parse(kvp.Value.ToString());
                            values = await GetSdlcNameById(r);
                        }
                        else if (kvp.Key.ToString() == "efficiency")
                        {
                            Guid r = Guid.Parse(kvp.Value.ToString());
                            values = await GetBenefitsById(r);
                        }
                        SqlParameter[] param =
                            {
                        new SqlParameter("@spType",usp_ManageUpdateHistory_Type.insert),
                        new SqlParameter("@FieldName",kvp.Key.ToString()),
                        new SqlParameter("@FieldValue",values),
                        new SqlParameter("@RequestType",1),//For New Request RequirementId
                        new SqlParameter("@auto_id",project_Requirement.auto_id),//For New Request 
                        new SqlParameter("@RequirementId",project_Requirement.id),//For New Request RequirementId
                        new SqlParameter("@Updated_By",createdBy),
                    };
                        var dbResults = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUpdateHistory, param);
                    }
                }
                if (updatefor == "IN")
                {
                    List<KeyValuePair<string, string>> CRkeyValueList = new List<KeyValuePair<string, string>>();
                    try
                    {
                        SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageUpdateHistory_Type.getByAutoId), new SqlParameter("@auto_id", project_Requirement.auto_id) };
                        var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUpdateHistory, parameters);
                        DataTable ds = (DataTable)dbResult.Tables[0];

                        foreach (DataRow row in ds.Rows)
                        {
                            if (project_Requirement.request_status.ToString().ToLower() != row["request_status"].ToString().ToLower())
                            {
                                CRkeyValueList.Add(new KeyValuePair<string, string>("request_status", Convert.ToString(row["request_status"]) == "" ? Convert.ToString(project_Requirement.request_status) : (Convert.ToString(row["request_status"]) != Convert.ToString(project_Requirement.request_status) ? Convert.ToString(project_Requirement.request_status) : Convert.ToString(row["request_status"]))));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    CRkeyValueList.RemoveAll(kvp => kvp.Value == "");
                    CRkeyValueList.RemoveAll(kvp => kvp.Value == null);

                    foreach (var kvp in CRkeyValueList)
                    {
                        string values = kvp.Value.ToString();
                        if (kvp.Key.ToString() == "request_status")
                        {
                            Guid r = Guid.Parse(kvp.Value.ToString());
                            values = await GetStatusNameById(r);
                        }

                        SqlParameter[] param =
                            {
                        new SqlParameter("@spType",usp_ManageUpdateHistory_Type.insert),
                        new SqlParameter("@FieldName",kvp.Key.ToString()),
                        new SqlParameter("@FieldValue",values),
                        new SqlParameter("@RequestType",3),//For New CR
                        new SqlParameter("@auto_id",project_Requirement.auto_id),//For New Request 
                        new SqlParameter("@RequirementId",project_Requirement.id),//For New Request RequirementId
                        new SqlParameter("@Updated_By",createdBy),
                    };
                        var dbResults = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUpdateHistory, param);
                    }
                }
                if (updatefor == "CR")
                {
                    List<KeyValuePair<string, string>> CRkeyValueList = new List<KeyValuePair<string, string>>();
                    try
                    {
                        SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageUpdateHistory_Type.getByAutoId), new SqlParameter("@auto_id", project_Requirement.auto_id) };
                        var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUpdateHistory, parameters);
                        DataTable ds = (DataTable)dbResult.Tables[0];

                        foreach (DataRow row in ds.Rows)
                        {
                            if (project_Requirement.request_status.ToString().ToLower() != row["request_status"].ToString().ToLower())
                            {
                                CRkeyValueList.Add(new KeyValuePair<string, string>("request_status", Convert.ToString(row["request_status"]) == "" ? Convert.ToString(project_Requirement.request_status) : (Convert.ToString(row["request_status"]) != Convert.ToString(project_Requirement.request_status) ? Convert.ToString(project_Requirement.request_status) : Convert.ToString(row["request_status"]))));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    CRkeyValueList.RemoveAll(kvp => kvp.Value == "");
                    CRkeyValueList.RemoveAll(kvp => kvp.Value == null);

                    foreach (var kvp in CRkeyValueList)
                    {
                        string values = kvp.Value.ToString();
                        if (kvp.Key.ToString() == "request_status")
                        {
                            Guid r = Guid.Parse(kvp.Value.ToString());
                            values = await GetStatusNameById(r);
                        }

                        SqlParameter[] param =
                            {
                        new SqlParameter("@spType",usp_ManageUpdateHistory_Type.insert),
                        new SqlParameter("@FieldName",kvp.Key.ToString()),
                        new SqlParameter("@FieldValue",values),
                        new SqlParameter("@RequestType",2),//For New CR
                        new SqlParameter("@auto_id",project_Requirement.auto_id),//For New Request 
                        new SqlParameter("@RequirementId",project_Requirement.id),//For New Request RequirementId
                        new SqlParameter("@Updated_By",createdBy),
                    };
                        var dbResults = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUpdateHistory, param);
                    }
                }
            }
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@auto_id",project_Requirement.auto_id),
                    new SqlParameter("@requirement_type",project_Requirement.requirement_type),
                    new SqlParameter("@project_name",project_Requirement.project_name),
                    new SqlParameter("@customer",project_Requirement.customer),
                    new SqlParameter("@request_title",project_Requirement.request_title),
                    new SqlParameter("@request_description",project_Requirement.request_description),
                    new SqlParameter("@request_notes",project_Requirement.request_notes),
                    new SqlParameter("@domain",project_Requirement.domain),
                    new SqlParameter("@vendor",project_Requirement.vendor),
                    new SqlParameter("@request_priority",project_Requirement.request_priority),
                    new SqlParameter("@cost_value",project_Requirement.cost_value),
                    new SqlParameter("@request_status",project_Requirement.request_status),
                    new SqlParameter("@date_of_state_changed",project_Requirement.date_of_state_changed),
                    new SqlParameter("@node_type",project_Requirement.node_type),
                    new SqlParameter("@requester",project_Requirement.requester),
                    new SqlParameter("@lm_of_requested",project_Requirement.lm_of_requested),
                    new SqlParameter("@date_of_request",project_Requirement.date_of_request),
                    new SqlParameter("@project_manager",project_Requirement.project_manager),
                    new SqlParameter("@solution_architect",project_Requirement.solution_architect),
                    new SqlParameter("@expected_cost_of_development",project_Requirement.expected_cost_of_development),
                    new SqlParameter("@expected_time_of_development_hour",project_Requirement.expected_time_of_development_hour),
                    new SqlParameter("@actual_cost_of_development",project_Requirement.actual_cost_of_development),
                    new SqlParameter("@actual_time_of_development_hour",project_Requirement.actual_time_of_development_hour),
                    new SqlParameter("@developer_name",project_Requirement.developer_name),
                    new SqlParameter("@tester_name_sit",project_Requirement.tester_name_sit),
                    new SqlParameter("@is_uat_offered",project_Requirement.is_uat_offered),
                    new SqlParameter("@is_correction_during_uat",project_Requirement.is_correction_during_uat),
                    new SqlParameter("@correction_description_during_uat",project_Requirement.correction_description_during_uat),
                    new SqlParameter("@is_enhancement_during_uat",project_Requirement.is_enhancement_during_uat),
                    new SqlParameter("@enhancement_description_during_uat",project_Requirement.enhancement_description_during_uat),
                    new SqlParameter("@mop_or_sop",project_Requirement.mop_or_sop),
                    new SqlParameter("@updated_mop",project_Requirement.updated_mop),
                    new SqlParameter("@connectivity_availability",project_Requirement.connectivity_availability),
                    new SqlParameter("@crediential_availability",project_Requirement.crediential_availability),
                    new SqlParameter("@attachment_for_mop_or_sop",project_Requirement.attachment_for_mop_or_sop),
                    new SqlParameter("@is_live",project_Requirement.is_live),
                    new SqlParameter("@tool_solution_name",project_Requirement.tool_solution_name),
                    new SqlParameter("@use_case",project_Requirement.use_case_name),
                    new SqlParameter("@sdlc_status",project_Requirement.sdlc_status),
                    new SqlParameter("@resolution_offered",project_Requirement.resolution_offered),
                    new SqlParameter("@support_engineer",project_Requirement.support_engineer),
                    new SqlParameter("@support_manager",project_Requirement.support_manager),
                    new SqlParameter("@expected_time_of_resolution_hour",project_Requirement.expected_time_of_resolution_hour),
                    new SqlParameter("@actual_time_of_resolution_hour",project_Requirement.actual_time_of_resolution_hour),
                    new SqlParameter("@created_By",createdBy),
                    new SqlParameter("@is_resolution_offered",project_Requirement.is_resolution_offered),
                    new SqlParameter("@business_benifit",project_Requirement.business_benifit),
                    new SqlParameter("@function_id",project_Requirement.function_id),
                    new SqlParameter("@function_level",project_Requirement.function_level),
                    new SqlParameter("@region",project_Requirement.region),
                    new SqlParameter("@expected_time_effort_hr",project_Requirement.expected_time_effort_hr),
                    new SqlParameter("@id",project_Requirement.id),
                    new SqlParameter("@efficiency",project_Requirement.efficiency),
                    new SqlParameter("@efficiency_value",project_Requirement.efficiency_value),
                    new SqlParameter("@technologyid",project_Requirement.technology),
                    new SqlParameter("@shairpoint_url", project_Requirement.shairpoint_url),
                    new SqlParameter("@project_manager_name", project_Requirement.project_manager_name),
                    new SqlParameter("@master_solution_architect_name", project_Requirement.master_solution_architect_name),
                    new SqlParameter("@tester_name", project_Requirement.tester_name),
                    new SqlParameter("@expected_time_of_resolution_hourtext", project_Requirement.expected_time_of_resolution_hourtext),
                    new SqlParameter("@employee_id", project_Requirement.employee_id),
                    new SqlParameter("@resolution_category_comments", project_Requirement.resolution_category_comments),
                    new SqlParameter("@resolution_category", project_Requirement.resolution_category),
                    new SqlParameter("@expected_time_effort_date", project_Requirement.expected_time_effort_date),
                    new SqlParameter("@res_cat1", project_Requirement.res_cat1),
                    new SqlParameter("@res_cat2", project_Requirement.res_cat2),
                    new SqlParameter("@res_cat3", project_Requirement.res_cat3),
                    new SqlParameter("@request_for", project_Requirement.request_for),
                    new SqlParameter("@cli_ui_name", project_Requirement.cli_ui_name),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRequirement, parameters);
                if (project_Requirement.attachment.Count > 0)
                {
                    foreach (var item in project_Requirement.attachment)
                    {
                        SqlParameter[] param =
                        {
                        new SqlParameter("@spType",usp_ManageAttachement_Type.newRequirement),
                        new SqlParameter("@AttachementName",item.AttachementName.ToString()),
                        new SqlParameter("@NewRequestID",dbResult.Tables[0].Rows[0]["id"].ToString()),
                        new SqlParameter("@AttachementType",item.AttachementType.ToString()),

                    };
                        var dbResults = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageAttachement, param);
                    }
                }
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<string> GetStatusNameById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageStatus_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageStatus, parameters);
            //return db_result.Tables[0].Rows[0][""].ToString();
            return db_result.Tables[0].Rows[0]["status_name"].ToString();
        }
        public async Task<string> GetSdlcNameById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageSDLC_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageSDLC, parameters);
            return db_result.Tables[0].Rows[0]["sdlc_status"].ToString();
        }
        public async Task<string> GetBenefitsById(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManagePriority_Type.getById), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Manageefficiency, parameters);
            return db_result.Tables[0].Rows[0]["efficiency_name"].ToString();

        }
        /// <summary>
        /// This method is used to get row data with different object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spType"></param>
        /// <returns></returns>
        public async Task<string> GenerateAutoId(int spType)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", spType) };
            var dbResult = await SqlHelper.ExecuteDatasetAsync(ConnectionCofig.ConnectionStr, Procedures.usp_GenerateAutoId, parameters);
            return dbResult.Tables[0].Rows[0]["auto_id"].ToString();
        }
        public async Task<List<T>> GetAll<T>(int spType, Guid requirementType, vm_search search)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@auto_id",search.auto_id),
                    new SqlParameter("@requirement_type",requirementType),
                    new SqlParameter("@request_title",search.request_title),
                    new SqlParameter("@request_priority",search.request_priority),
                    new SqlParameter("@request_status",search.request_status),
                    new SqlParameter("@fromDate",search.fromDate),
                    new SqlParameter("@toDate",search.toDate)
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRequirement, parameters);
                if (search.access_role != "admin" && search.access_role != "Developer")
                {
                    try
                    {
                        SqlParameter[] param =
                        {
                            new SqlParameter("@spType",ManageUserProjectMapping_Type.getall),
                            new SqlParameter("@employee_id",search.user_name),
                        };
                        var dbResult_Projects = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUserProjectsMapping, param);

                        for (int i = dbResult.Tables[0].Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow row = dbResult.Tables[0].Rows[i];
                            var project_name = row.Field<string>("project_name");
                            bool projectExists = false;
                            foreach (DataTable table in dbResult_Projects.Tables)
                            {
                                foreach (DataRow projectRow in table.Rows)
                                {
                                    var projectName = projectRow.Field<string>("project_name");

                                    if (projectName.Equals(project_name, StringComparison.OrdinalIgnoreCase))
                                    {
                                        projectExists = true;
                                        //break;
                                    }
                                }
                                //if (projectExists)
                                //{
                                //    break;
                                //}
                            }
                            if (!projectExists)
                            {
                                row.Delete();
                            }
                        }

                        dbResult.Tables[0].AcceptChanges();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    //var usern = search.user_name;

                    //for (int i = dbResult.Tables[0].Rows.Count - 1; i >= 0; i--)
                    //{
                    //    DataRow row = dbResult.Tables[0].Rows[i];
                    //    if (row.Field<string>("created_by") != usern)
                    //    {
                    //        dbResult.Tables[0].Rows.RemoveAt(i);
                    //    }
                    //}
                    //dbResult.Tables[0].AcceptChanges();
                }
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<T> GetRow<T>(int spType, Guid id)
        {
            try
            {
                SqlParameter[] parameters = { new SqlParameter("@spType", spType), new SqlParameter("@id", id) };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRequirement, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<CommonDbResponse> Delete(Guid? id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", ManageRequirement_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRequirement, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<List<T>> GetAllAttachements<T>(int spType, Attachments attachments)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@id",attachments.id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageAttachement, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<CommonDbResponse> DeleteAttachement(Guid id)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_ManageAttachement_Type.delete), new SqlParameter("@id", id) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageAttachement, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        public async Task<List<T>> GetProjectCustomers<T>(int getAll, Guid project_name, string user_name)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@user_id",user_name),
                    new SqlParameter("@project_id",project_name),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_GetProjectCustomersByUserID, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<T>> GetUseCasesByToolNameId<T>(int spType, Guid tool_id, Guid project_id)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@tool_solution_name",tool_id),
                    new SqlParameter("@project_guid",project_id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRequirement, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<T>> GetToolNameByProjectID<T>(int spType, Guid project_name)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@project_name",project_name),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRequirement, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<T>> GetProjectVendors<T>(int spType, Guid project_name, string user_name)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@project_name",project_name),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRequirement, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<T>> GetProjectTechnology<T>(int spType, Guid project_name, Guid vendor, string user_name)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@project_name",project_name),
                    new SqlParameter("@venderid",vendor),

                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRequirement, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<T>> GetProjectNodeType<T>(int spType, Guid project_name, Guid technology, Guid vendor, string user_name)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@project_name",project_name),
                    new SqlParameter("@venderid",vendor),
                    new SqlParameter("@technologyid",technology),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRequirement, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<T>> GetUpdateHistory<T>(int spType, master_update_history master_update_History)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@auto_id",master_update_History.auto_id),
                    new SqlParameter("@DateofUpdate",master_update_History.DateofUpdate),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUpdateHistory, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<T>> GetHistoryByDate<T>(int spType, master_update_history master_update_History)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@auto_id",master_update_History.auto_id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUpdateHistory, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<T>> GetStatusHistory<T>(int spType, master_update_history master_update_History)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@auto_id",master_update_History.auto_id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUpdateHistory, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public async Task<T> Save<T>
        //public async Task<T> UpdateMessageStatus<T>(int spType, message_update_status request)
        //{
        //    SqlParameter[] param =
        //               {
        //        new SqlParameter("@spType", spType),
        //        new SqlParameter("@message", request.message),
        //        new SqlParameter("@auto_id", request.auto_id),
        //        new SqlParameter("@created_by", request.created_by),

        //    };
        //    var dbResults = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Manage_Dreft_Pending_Request, param);
        //    return CommonUtility.GetObjectByRow<T>(dbResults.Tables[0].Rows[0]);
        //}

        //public Task UpdateMessageStatus(int insert, message_update_status request)
        //{
        //    SqlParameter[] param =
        //                {
        //        new SqlParameter("@spType", spType),
        //        new SqlParameter("@message", request.message),
        //        new SqlParameter("@auto_id", request.auto_id),
        //        new SqlParameter("@created_by", request.created_by),

        //    };
        //    var dbResults = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Manage_Dreft_Pending_Request, param);
        //    return CommonUtility.GetObjectByRow<T>(dbResults.Tables[0].Rows[0]);
        //}

        //public async Task<T> UpdateMessageStatus<T>(int spType, message_update_status request)
        //{
        //    SqlParameter[] param =
        //               {
        //        new SqlParameter("@spType", spType),
        //        new SqlParameter("@message", request.message),
        //        new SqlParameter("@auto_id", request.auto_id),
        //        new SqlParameter("@created_by", request.created_by),

        //    };
        //    var dbResults = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Manage_Dreft_Pending_Request, param);
        //    return CommonUtility.GetObjectByRow<T>(dbResults.Tables[0].Rows[0]);
        //}

        public async Task<CommonDbResponse> SaveUpdateMessageStatus(int spType, message_update_status request)
        {
            SqlParameter[] param =
                       {
                new SqlParameter("@spType", spType),
                new SqlParameter("@message", request.message),
                new SqlParameter("@auto_id", request.auto_id),
                new SqlParameter("@created_by", request.created_by),
                new SqlParameter("@request_status", request.request_status),
                new SqlParameter("@RequirementId", request.RequirementId)
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Manage_Dreft_Pending_Request, param);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);
        }

        //public async Task<string> GetUpdateMessage(int spType, message_update_status request)
        //{
        //    SqlParameter[] param =
        //              {
        //        new SqlParameter("@spType", spType),
        //        new SqlParameter("@auto_id", request.auto_id),
        //    };
        //    var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Manage_Dreft_Pending_Request, param);
        //    return db_result.Tables[0].Rows[0]["message"].ToString();
        //    //return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0][""]);

        //}

        public async Task<string> GetUpdateMessage(int getbyauto_id, string auto_id)
        {
            SqlParameter[] param =
                       {
                new SqlParameter("@spType", getbyauto_id),
                new SqlParameter("@auto_id", auto_id),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_Manage_Dreft_Pending_Request, param);
            return db_result.Tables[0].Rows[0]["messages"].ToString();
            //return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0][""]);

        }

        public async Task<T> UpdateOthers<T>(int spType, project_requirement project_Requirement, string createdBy)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType", spType),
                    new SqlParameter("@auto_id", project_Requirement.auto_id),
                   // new SqlParameter("@date_of_request", project_Requirement.date_of_request),
                    new SqlParameter("@requester", project_Requirement.requester),
                    new SqlParameter("@lm_of_requested", project_Requirement.lm_of_requested),
                    new SqlParameter("@request_priority", project_Requirement.request_priority),
                    new SqlParameter("@request_title", project_Requirement.request_title),
                    new SqlParameter("@requirement_type", project_Requirement.requirement_type),
                    new SqlParameter("@project_name", project_Requirement.project_name),
                    new SqlParameter("@customer", project_Requirement.customer),
                    new SqlParameter("@vendor", project_Requirement.vendor),
                    new SqlParameter("@technologyid",project_Requirement.technology),
                    new SqlParameter("@node_type", project_Requirement.node_type),
                    new SqlParameter("@domain", project_Requirement.domain),
                    new SqlParameter("@connectivity_availability", project_Requirement.connectivity_availability),
                    new SqlParameter("@crediential_availability", project_Requirement.crediential_availability),
                    
                    new SqlParameter("@mop_or_sop", project_Requirement.mop_or_sop),
                    new SqlParameter("@function_id",project_Requirement.function_id),
                    new SqlParameter("@function_level",project_Requirement.function_level),
                    new SqlParameter("@expected_time_effort_hr",project_Requirement.expected_time_effort_hr),
                    new SqlParameter("@is_resolution_offered", project_Requirement.is_resolution_offered),
                    new SqlParameter("@efficiency",project_Requirement.efficiency),
                    new SqlParameter("@efficiency_value",project_Requirement.efficiency_value),
                    new SqlParameter("@region",project_Requirement.region),

                    new SqlParameter("@request_description", project_Requirement.request_description),
                    new SqlParameter("@request_notes", project_Requirement.request_notes),
                    new SqlParameter("@id",project_Requirement.id),
                    new SqlParameter("@created_By",createdBy),
                    new SqlParameter("@cli_ui_name", project_Requirement.cli_ui_name),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRequirement, parameters);
                if (project_Requirement.attachment.Count > 0)
                {
                    foreach (var item in project_Requirement.attachment)
                    {
                        SqlParameter[] param =
                        {
                        new SqlParameter("@spType",usp_ManageAttachement_Type.newRequirement),
                        new SqlParameter("@AttachementName",item.AttachementName.ToString()),
                        new SqlParameter("@NewRequestID",dbResult.Tables[0].Rows[0]["id"].ToString()),
                        new SqlParameter("@AttachementType",item.AttachementType.ToString()),

                    };
                        var dbResults = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageAttachement, param);
                    }
                }
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<T> UpdateOthers_CR<T>(int spType, vmProjectRequirement project_Requirement, string created_by)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType", spType),
                    new SqlParameter("@auto_id", project_Requirement.auto_id),
                    //new SqlParameter("@date_of_request", project_Requirement.date_of_request),
                    new SqlParameter("@requester", project_Requirement.requester),
                    new SqlParameter("@lm_of_requested", project_Requirement.lm_of_requested),
                    new SqlParameter("@request_priority", project_Requirement.request_priority),
                    new SqlParameter("@request_title", project_Requirement.request_title),
                    new SqlParameter("@requirement_type", project_Requirement.requirement_type),
                    new SqlParameter("@project_name", project_Requirement.project_name),
                    new SqlParameter("@tool_solution_name", project_Requirement.tool_solution_name),
                    new SqlParameter("@use_case", project_Requirement.use_case_name),
                    new SqlParameter("@connectivity_availability", project_Requirement.connectivity_availability),
                    new SqlParameter("@crediential_availability", project_Requirement.crediential_availability),
                    new SqlParameter("@region",project_Requirement.region),
                    new SqlParameter("@cost_value",project_Requirement.cost_value),
                    new SqlParameter("@expected_time_of_resolution_hour", project_Requirement.expected_time_of_resolution_hour),
                    new SqlParameter("@business_benifit", project_Requirement.business_benifit),
                    new SqlParameter("@support_engineer", project_Requirement.support_engineer),
                    new SqlParameter("@support_manager",project_Requirement.support_manager),
                    new SqlParameter("@is_resolution_offered",project_Requirement.is_resolution_offered),

                    new SqlParameter("@function_id",project_Requirement.function_id),
                    new SqlParameter("@function_level", project_Requirement.function_level),
                    new SqlParameter("@expected_time_effort_hr",project_Requirement.expected_time_effort_hr),

                    new SqlParameter("@request_description", project_Requirement.request_description),
                    new SqlParameter("@request_notes", project_Requirement.request_notes),
                    new SqlParameter("@id",project_Requirement.id),
                    new SqlParameter("@created_By",created_by),
                    new SqlParameter("@mop_or_sop", project_Requirement.mop_or_sop),
                    new SqlParameter("@cli_ui_name", project_Requirement.cli_ui_name),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRequirement, parameters);
                if (project_Requirement.attachment.Count > 0)
                {
                    foreach (var item in project_Requirement.attachment)
                    {
                        SqlParameter[] param =
                        {
                        new SqlParameter("@spType",usp_ManageAttachement_Type.newRequirement),
                        new SqlParameter("@AttachementName",item.AttachementName.ToString()),
                        new SqlParameter("@NewRequestID",dbResult.Tables[0].Rows[0]["id"].ToString()),
                        new SqlParameter("@AttachementType",item.AttachementType.ToString()),

                    };
                        var dbResults = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageAttachement, param);
                    }
                }
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<T> UpdateOthers_IS<T>(int spType, vmProjectRequirement project_Requirement, string created_by)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType", spType),
                    new SqlParameter("@auto_id", project_Requirement.auto_id),
                    //new SqlParameter("@date_of_request", project_Requirement.date_of_request),
                    //new SqlParameter("@requester", project_Requirement.requester),
                    //new SqlParameter("@lm_of_requested", project_Requirement.lm_of_requested),
                    new SqlParameter("@request_priority", project_Requirement.request_priority),
                    new SqlParameter("@request_title", project_Requirement.request_title),
                    new SqlParameter("@project_name", project_Requirement.project_name),
                    new SqlParameter("@tool_solution_name", project_Requirement.tool_solution_name),
                    new SqlParameter("@use_case", project_Requirement.use_case_name),
                    //new SqlParameter("@expected_time_of_resolution_hour", project_Requirement.expected_time_of_resolution_hourtext),
                    new SqlParameter("@is_resolution_offered", project_Requirement.is_resolution_offered),
                    new SqlParameter("@business_benifit", project_Requirement.business_benifit),
                    new SqlParameter("@request_description", project_Requirement.request_description),
                    new SqlParameter("@request_notes", project_Requirement.request_notes),
                    new SqlParameter("@requirement_type", project_Requirement.requirement_type),
                    new SqlParameter("@id",project_Requirement.id),
                    new SqlParameter("@created_By",created_by),
                    new SqlParameter("@mop_or_sop", project_Requirement.mop_or_sop),
                    new SqlParameter("@cli_ui_name", project_Requirement.cli_ui_name),

                    //new SqlParameter("@connectivity_availability", project_Requirement.connectivity_availability),
                    //new SqlParameter("@crediential_availability", project_Requirement.crediential_availability),
                    //new SqlParameter("@expected_time_of_resolution_hour",project_Requirement.expected_time_of_resolution_hour),
                    //new SqlParameter("@support_engineer", project_Requirement.support_engineer),
                    
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRequirement, parameters);
                if (project_Requirement.attachment.Count > 0)
                {
                    foreach (var item in project_Requirement.attachment)
                    {
                        SqlParameter[] param =
                        {
                        new SqlParameter("@spType",usp_ManageAttachement_Type.newRequirement),
                        new SqlParameter("@AttachementName",item.AttachementName.ToString()),
                        new SqlParameter("@NewRequestID",dbResult.Tables[0].Rows[0]["id"].ToString()),
                        new SqlParameter("@AttachementType",item.AttachementType.ToString()),

                    };
                        var dbResults = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageAttachement, param);
                    }
                }
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<T> SaveDevTask<T>(int spType, DeveloperTask developerTask)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@auto_id",developerTask.auto_id),
                    new SqlParameter("@task_details",developerTask.task_details),
                    new SqlParameter("@employee_id",developerTask.employee_id),
                    //new SqlParameter("@status",developerTask.status),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDeveloperTaks, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<T>> GetDeveloperTaskData<T>(int spType, DeveloperTask _DeveloperTask)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@auto_id",_DeveloperTask.auto_id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDeveloperTaks, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<T> GetDevTaskByID<T>(int spType, string taskId)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@TaskId",taskId),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDeveloperTaks, parameters);
                return CommonUtility.GetObjectByRow<T>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CommonDbResponse> UpdateDevTask<T>(int spType, DeveloperTask obj_vm_user_registration)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@employee_id",obj_vm_user_registration.employee_id),
                    new SqlParameter("@status",obj_vm_user_registration.status),
                    new SqlParameter("@task_details",obj_vm_user_registration.task_details),
                    new SqlParameter("@Taskid",obj_vm_user_registration.Taskid),
                    new SqlParameter("@TaskComments",obj_vm_user_registration.TaskComments)
                };
                var result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDeveloperTaks, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(result.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<CommonDbResponse> PullChat<T>(int spType, ChatRecord chat)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@auto_id",chat.auto_id),
                    new SqlParameter("@ChatMessage",chat.ChatMessage),
                    new SqlParameter("@Chat_created_by",chat.ChatCreatedBy),
                    //new SqlParameter("@status",developerTask.status),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageChat, parameters);
                return CommonUtility.GetObjectByRow<CommonDbResponse>(dbResult.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<T>> GetChatData<T>(int spType, ChatRecord chat)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@spType",spType),
                    new SqlParameter("@auto_id",chat.auto_id),
                };
                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageChat, parameters);
                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CommonDbResponse> DeleteTask(string TaskId)
        {
            SqlParameter[] parameters = { new SqlParameter("@spType", usp_DeveloperTask_Type.delete), new SqlParameter("@TaskId", TaskId) };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageDeveloperTaks, parameters);
            return CommonUtility.GetObjectByRow<CommonDbResponse>(db_result.Tables[0].Rows[0]);

        }

        public async Task<string> GetResolutionHr<T>(int spType, Guid request_priority)
        {
            SqlParameter[] param =
                       {
                new SqlParameter("@spType", spType),
                new SqlParameter("@request_priority", request_priority),
            };
            var db_result = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageRequirement, param);
            return db_result.Tables[0].Rows[0]["TimetoResponse"].ToString();

        }

        public async Task<List<T>> GetAll_Filter<T>(int spType, Guid requirementId, List<Guid> request_status, vm_search search)
        {
            try
            {
                DataTable requestStatusTable = new DataTable();
                requestStatusTable.Columns.Add("StatusId", typeof(Guid));
                foreach (Guid statusId in request_status)
                {
                    requestStatusTable.Rows.Add(statusId);
                }

                SqlParameter[] parameters =
                {
                    new SqlParameter("@request_status_list", SqlDbType.Structured)
                    {
                        TypeName = "dbo.UniqueIdentifierList", 
                        Value = requestStatusTable
                    },
                    new SqlParameter("@requirement_type", requirementId)
                };

                var dbResult = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.GetFilteredRequestData, parameters);
                if (search.access_role != "admin" && search.access_role != "Developer")
                {
                    try
                    {
                        SqlParameter[] param =
                        {
                            new SqlParameter("@spType",ManageUserProjectMapping_Type.getall),
                            new SqlParameter("@employee_id",search.user_name),
                        };
                        var dbResult_Projects = await SqlHelper.ExecuteProc(ConnectionCofig.ConnectionStr, Procedures.usp_ManageUserProjectsMapping, param);

                        for (int i = dbResult.Tables[0].Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow row = dbResult.Tables[0].Rows[i];
                            var project_name = row.Field<string>("project_name");
                            bool projectExists = false;
                            foreach (DataTable table in dbResult_Projects.Tables)
                            {
                                foreach (DataRow projectRow in table.Rows)
                                {
                                    var projectName = projectRow.Field<string>("project_name");

                                    if (projectName.Equals(project_name, StringComparison.OrdinalIgnoreCase))
                                    {
                                        projectExists = true;
                                        //break;
                                    }
                                }
                                //if (projectExists)
                                //{
                                //    break;
                                //}
                            }
                            if (!projectExists)
                            {
                                row.Delete();
                            }
                        }

                        dbResult.Tables[0].AcceptChanges();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

                return CommonUtility.ConvertDataTableToList<T>(dbResult.Tables[0]);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
