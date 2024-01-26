using System;

namespace TicketingTool.Models.Masters
{
    public class master_function
    {
        public Guid? id { get; set; }
        public string function_name { get; set; }
        public bool is_active { get; set; }
    }
}
