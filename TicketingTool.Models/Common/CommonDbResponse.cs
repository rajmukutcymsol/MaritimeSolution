using System;

namespace TicketingTool.Models.Common
{
    public class CommonDbResponse
    {
        public bool STATUS { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string project_name { get; set; }
        public string to_email_user { get; set; }
        public string progressResponce { get; set; }
        public string Requester { get; set; }
        public string tool_name { get; set; }
        public string use_case_name { get; set; }
        public string sent_user_activation_email { get; set; }
        public string developers_group_email { get; set; }
        public string employee_name { get; set; }
        public string date_of_request { get; set; }
        public string AssigntoPersonEmail { get; set; }
        public string AssigntoPerson_Name { get; set; }
        public int auto_id { get; set; }
    }
}
