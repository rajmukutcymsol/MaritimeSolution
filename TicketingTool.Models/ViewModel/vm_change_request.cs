using System;

namespace TicketingTool.Models.ViewModel
{
    public class vm_change_request
    {
        public Guid id { get; set; }
        public string auto_id { get; set; }
        public string request_title { get; set; }
        public string requester { get; set; }
        public string date_of_request { get; set; }
        public string is_resolution_offered { get; set; }
        public string priority_name { get; set; }
        public string status_name { get; set; }
        public string tool_name { get; set; }
        public string project_name { get; set; }


    }
}
