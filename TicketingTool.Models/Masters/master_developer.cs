using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_developer
    {
        public Guid id { get; set; }
        public string developer_name { get; set; }
        public bool is_active { get; set; }
        public string employee_id { get; set; }
        public string employee_name { get; set; }

    }
}
