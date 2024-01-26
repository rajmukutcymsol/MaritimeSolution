using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingTool.Models.Masters
{
    public class master_response_time
    {
        public Guid responsetimeid { get; set; }
        public string TimetoResponse { get; set; }
        public bool is_active { get; set; }
        public Guid request_priority { get; set; }

    }
}
