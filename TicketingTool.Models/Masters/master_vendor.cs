using System;

namespace TicketingTool.Models.Masters
{
    public class master_vendor
    {
        public Guid id { get; set; }
        public string vendor_name { get; set; }
        public bool is_active { get; set; }
    }
}
