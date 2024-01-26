using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_resolution_category
    {
        public Guid id { get; set; }
        public string resolution_category_name { get; set; }
        public bool is_active { get; set; }
    }
}
