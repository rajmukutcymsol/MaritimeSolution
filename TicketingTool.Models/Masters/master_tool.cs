using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_tool
    {
        public Guid id { get; set; }
        public string tool_name { get; set; }
        public bool is_active { get; set; }
    }
}
