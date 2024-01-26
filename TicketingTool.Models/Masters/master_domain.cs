using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_domain
    {
        public Guid id { get; set; }
        public string domain_name { get; set; }
        public bool is_active { get; set; }
    }
}
