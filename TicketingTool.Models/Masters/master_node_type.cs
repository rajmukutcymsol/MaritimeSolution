using System;

namespace TicketingTool.Models.Masters
{
    public class master_node_type
    {
        public Guid id { get; set; }
        public string node_type_name { get; set; }
        public bool is_active { get; set; }
    }
}
