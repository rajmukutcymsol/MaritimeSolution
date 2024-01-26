using System;

namespace TicketingTool.Models.Masters
{
    public class master_customer
    {
        public Guid id { get; set; }
        public string customer_name { get; set; }
        public bool is_active { get; set; }
    }
}
