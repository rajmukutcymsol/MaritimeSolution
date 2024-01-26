using System;
using System.Collections.Generic;

namespace TicketingTool.Models.Models
{
    public class project_requirement
    {
        public Guid id { get; set; }
        public string auto_id { get; set; }
        public Guid? requirement_type { get; set; }
        public Guid? project_name { get; set; }
        public Guid? customer { get; set; }
        public string request_title { get; set; }
        public string request_description { get; set; }
        public string request_notes { get; set; }
        public Guid? domain { get; set; }
        public Guid? vendor { get; set; }
        public Guid? request_priority { get; set; }
        public decimal? cost_value { get; set; }
        public Guid? request_status { get; set; }
        public DateTime? date_of_state_changed { get; set; }
        public Guid? node_type { get; set; }
        public string requester { get; set; }
        public string lm_of_requested { get; set; }
        public DateTime? date_of_request { get; set; }
        public string project_manager { get; set; }
        public string solution_architect { get; set; }
        public decimal? expected_cost_of_development { get; set; }
        public int? expected_time_of_development_hour { get; set; }
        public decimal? actual_cost_of_development { get; set; }
        public int? actual_time_of_development_hour { get; set; }
        public string developer_name { get; set; }
        public string tester_name_sit { get; set; }
        public bool? is_uat_offered { get; set; }
        public bool? is_correction_during_uat { get; set; }
        public string correction_description_during_uat { get; set; }
        public bool? is_enhancement_during_uat { get; set; }
        public string enhancement_description_during_uat { get; set; }
        public string mop_or_sop { get; set; }
        public DateTime? updated_mop { get; set; }
        public bool? connectivity_availability { get; set; }
        public bool? crediential_availability { get; set; }
        public string attachment_for_mop_or_sop { get; set; }
        public bool? is_live { get; set; }
        public Guid? tool_solution_name { get; set; }
        public string use_case_name { get; set; }
        public Guid? sdlc_status { get; set; }
        public string resolution_offered { get; set; }
        public string support_engineer { get; set; }
        public string support_manager { get; set; }
        public int? expected_time_of_resolution_hour { get; set; }
        public int? actual_time_of_resolution_hour { get; set; }
        public bool? is_resolution_offered { get; set; }
        public string business_benifit { get; set; }
        public Guid? function_id { get; set; }
        public Guid? function_level { get; set; }
        public Guid? region { get; set; }
        public int expected_time_effort_hr { get; set; }
        public Guid? efficiency { get; set; }
        public string efficiency_value { get; set; }
        public List<Attachments> attachment { get; set; }
        public Guid? technology { get; set; }
        public string shairpoint_url { get; set; }
        public Guid? master_solution_architect_name { get; set; }
        public Guid? project_manager_name { get; set; }
        public string tester_name { get; set; }
        public string expected_time_of_resolution_hourtext { get; set; }
        public string employee_id { get; set; }
        public Guid? resolution_category { get; set; }
        public string resolution_category_comments { get; set; }
        public string employee_name { get; set; }
        public string date_of_request_string { get; set; }
        public DateTime? expected_time_effort_date { get; set; }
        public Guid? res_cat1 { get; set; }
        public Guid? res_cat2 { get; set; }
        public Guid? res_cat3 { get; set; }
        public string request_for { get; set; }
        public Guid? cli_ui_name { get; set; }
    }

    public class Attachments
    {
        public Guid id { get; set; }
        public string AttachementName { get; set; }
        public Guid NewRequestID { get; set; }
        public string AttachementType { get; set; }


    }
    public class DeveloperTask
    {
        public int Taskid { get; set; }
        public string employee_id { get; set; }
        public string status { get; set; }
        public string dateoftask { get; set; }
        public string auto_id { get; set; }
        public string task_details { get; set; }
        public string employee_name { get; set; }
        public string employeeid { get; set; }
        public string TaskComments { get; set; }
    }
}
