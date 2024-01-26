using System;

namespace TicketingTool.Models.Masters
{
    public class master_efficiency
    {
        public Guid? id { get; set; }
        public string efficiency_name { get; set; }
        public bool is_active { get; set; }
    }
}
