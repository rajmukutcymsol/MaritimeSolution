using System;

namespace TicketingTool.Models.Masters
{
    public class master_status
    {
        public Guid id { get; set; }
        public string status_name { get; set; }
        public bool is_active { get; set; }
    }
}
