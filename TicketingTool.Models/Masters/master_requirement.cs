using System;

namespace TicketingTool.Models.Masters
{
    public class master_requirement
    {
        public Guid id { get; set; }
        public string requirement_type { get; set; }
        public bool is_active { get; set; }
    }
}
