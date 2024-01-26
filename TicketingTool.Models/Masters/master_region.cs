using System;

namespace TicketingTool.Models.Masters
{
    public class master_region
    {
        public Guid? id { get; set; }
        public string region_name { get; set; }
        public bool is_active { get; set; }
    }
}
