using System;

namespace TicketingTool.Models.Masters
{
    public class master_technology
    {
        public Guid id { get; set; }
        public string technology_name { get; set; }
        public bool is_active { get; set; }
    }
}
