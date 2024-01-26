using System;

namespace TicketingTool.Models.Masters
{
    public class master_priority
    {
        public Guid? id { get; set; }
        public string priority_name { get; set; }
        public string priority_description { get; set; }
        public bool is_active { get; set; }
    }
}
