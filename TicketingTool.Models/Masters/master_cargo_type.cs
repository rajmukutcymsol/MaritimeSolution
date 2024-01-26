using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_cargo_type
    {
        public Guid id { get; set; }
        public string cargo_type_name { get; set; }
        public string cargo_type_discription { get; set; }
        public bool is_active { get; set; }
    }
}
