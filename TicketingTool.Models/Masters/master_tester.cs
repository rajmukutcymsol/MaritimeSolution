using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_tester
    {
        public Guid id { get; set; }
        public bool is_active { get; set; }
        public string employee_id { get; set; }
        public string employee_name { get; set; }
        public string tester_name { get; set; }

    }
}
