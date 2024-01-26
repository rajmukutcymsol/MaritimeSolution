using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_FC_Stevedore_Operations
    {
        public Guid id { get; set; }
        public string FC_Stevedore_Name { get; set; }
        public bool is_active { get; set; }
    }
}
