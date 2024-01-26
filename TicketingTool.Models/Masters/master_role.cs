using System;

namespace TicketingTool.Models.Masters
{
    public class master_role
    {
        public Guid? id { get; set; }
        public string role_name { get; set; }
        public bool is_active { get; set; }
    }
}
