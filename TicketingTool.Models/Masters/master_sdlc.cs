using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_sdlc
    {
        public Guid id { get; set; }
        public string sdlc_status { get; set; }
        public bool is_active { get; set; }
    }
}
