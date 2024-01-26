using System;

namespace TicketingTool.Models.Masters
{
    public class master_project
    {
        public Guid? id { get; set; }
        public string project_name { get; set; }
        public bool is_active { get; set; }
    }
}
