using System;
using System.Collections.Generic;
using TicketingTool.Models.Common;
using TicketingTool.Models.Constant;
using TicketingTool.Models.ViewModel;
using TicketingTool.Models.Masters;

namespace TicketingTool.Models.ViewModel
{
    public class vm_user_registration
    {
        public string employee_id { get; set; }
        public string employee_name { get; set; }
        public string user_role { get; set; }
        public string email_address { get; set; }
        public string mobile { get; set; }
        public string location { get; set; }
        public string department { get; set; }
        public string display_name { get; set; }
        public int state { get; set; }
        public string manager_employee_id { get; set; }
        public string manager_username { get; set; }
        public string manager_name { get; set; }
        public string cost_center { get; set; }
        public string access_role { get; set; }
        public Guid? access_role_id { get; set; }
        public List<master_project> projects_for_user { get; set; }
        public Guid? project_name { get; set; }
        public string employeeid { get; set; }
        public int Taskid { get; set; }
        public string taskdetails { get; set; }
        public string taskstatus { get; set; }
        public string TaskComments { get; set; }
        public string tester_name { get; set; }
        public string requester { get; set; }
        public string request_for { get; set;}
        public string password { get; set; }
        
    }
}
